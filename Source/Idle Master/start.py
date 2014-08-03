import requests
import cookielib
import bs4
import time
import subprocess
import sys
import os
import json

try:
	authData={}
	authData["sort"]=""
	execfile("./settings.txt",authData)
	myProfileURL = "http://steamcommunity.com/profiles/"+authData["steamLogin"][:17]
except:
	print time.strftime("%X - ") + "Error loading config file"
	os.system('pause')
	sys.exit()
	
if not authData["sessionid"]:
	print time.strftime("%X - ") + "No sessionid set"
	os.system('pause')
	sys.exit()
	
if not authData["steamLogin"]:
	print time.strftime("%X - ") + "No steamLogin set"
	os.system('pause')
	sys.exit()

def generateCookies():
	global authData
	try:
		cookies = dict(sessionid=authData["sessionid"], steamLogin=authData["steamLogin"], steamparental=authData["steamparental"])
	except:
		print time.strftime("%X - ") + "Error setting cookies"
		os.system('pause')
		sys.exit()

	return cookies

def dropDelay(numDrops):
	if numDrops>1:
		baseDelay = (15*60)
	else:
		baseDelay = (5*60)
	return baseDelay
	
def idleOpen(appID):
	try:
		print time.strftime("%X - ") + "Starting game " + getAppName(appID) + " to idle cards"
		subprocess.Popen("steam-idle.exe "+str(appID))
	except:
		print time.strftime("%X - ") + "Error launching steam-idle with game ID "+str(appID)
		os.system('pause')
		sys.exit()

def idleClose(appID):
	try:
		print time.strftime("%X - ") + "Closing game " + getAppName(appID) 
		os.system("taskkill.exe -im steam-idle.exe /F")
	except:
		print time.strftime("%X - ") + "Error closing game. Exiting."
		os.system('pause')
		sys.exit()

def chillOut(appID):
	print time.strftime("%X - ") + "Suspending operation for "+getAppName(appID)
	idleClose(appID)
	stillDown = True
	while stillDown:
		print time.strftime("%X - ") + "Sleeping for 5 minutes."
		time.sleep(5*60)
		try:
			rBadge = requests.get(myProfileURL+"/gamecards/"+str(appID)+"/",cookies=cookies)
			indBadgeData = bs4.BeautifulSoup(rBadge.text)
			badgeLeftString = indBadgeData.find_all("span",{"class": "progress_info_bold"})[0].contents[0]
			if "card drops" in badgeLeftString:
				stillDown = False
		except:
			print time.strftime("%X - ") + "Still unable to find drop info."
	# Resume operations.
	idleOpen(appID)
	
def getAppName(appID):
	try:
		api = requests.get("http://store.steampowered.com/api/appdetails/?appids="+str(appID)+"&filters=basic")
		api_data = json.loads(api.text)
		return str(unicode(api_data[str(appID)]["data"]["name"]))
	except:
		return "app "+str(appID)

def get_blacklist():
	try:
		with open('blacklist.txt', 'r') as f:
			lines = f.readlines()
		blacklist = [int(n.strip()) for n in lines]		
	except:
		blacklist = [];

	if not blacklist:
		print time.strftime("%X - ") + "No games have been blacklisted"

	return blacklist

print time.strftime("%X - ") + "Finding games that have card drops remaining"

try:
	cookies = generateCookies()
	r = requests.get(myProfileURL+"/badges/",cookies=cookies)
except:
	print time.strftime("%X - ") + "Error reading badge page"
	os.system('pause')
	sys.exit()

try:
	badgesLeft = {}
	badgePageData = bs4.BeautifulSoup(r.text)
	badgeSet = badgePageData.find_all("div",{"class": "badge_title_stats"})
except:
	print time.strftime("%X - ") + "Error finding drop info"
	os.system('pause')
	sys.exit()

blacklist = get_blacklist()

for badge in badgeSet:
	try:
		dropCount = badge.find_all("span",{"class": "progress_info_bold"})[0].contents[0]
		if "No card drops" in dropCount:
			continue
		else:
			# Remaining drops
			dropCountInt, junk = dropCount.split(" ",1)
			dropCountInt = int(dropCountInt)
			linkGuess = badge.find_parent().find_parent().find_parent().find_all("a")[0]["href"]
			junk, badgeId = linkGuess.split("/gamecards/",1)
			badgeId = int(badgeId.replace("/",""))
			if badgeId in blacklist:
				print time.strftime("%X - ") + getAppName(badgeId) + " on blacklist, skipping game"
				continue
			else:
				badgesLeft[badgeId] = dropCountInt
	except:
		continue

def getKey(item):
	if authData["sort"]=="mostcards" or authData["sort"]=="leastcards":
		return item[1]
	else:
		return item[0]

if authData["sort"]=="":
	games = badgesLeft.items()
if authData["sort"]=="mostcards":
	games = sorted(badgesLeft.items(), key=getKey, reverse=True)
if authData["sort"]=="leastcards":
	games = sorted(badgesLeft.items(), key=getKey, reverse=False)

print time.strftime("%X - ") + "Idle Master needs to idle " + str(len(badgesLeft)) + " games"

for k, v in games:
	delay = dropDelay(int(v))
	stillHaveDrops=1
	numCycles=50
	maxFail=2
	
	idleOpen(k)

	while stillHaveDrops==1:
		try:
			print time.strftime("%X - ") + "Sleeping for "+str(delay / 60)+" minutes"
			time.sleep(delay)
			numCycles-=1
			if numCycles<1: # Sanity check against infinite loop
				stillHaveDrops=0

			print time.strftime("%X - ") + "Checking to see if "+getAppName(k)+" has remaining card drops"
			rBadge = requests.get(myProfileURL+"/gamecards/"+str(k)+"/",cookies=cookies)
			indBadgeData = bs4.BeautifulSoup(rBadge.text)
			badgeLeftString = indBadgeData.find_all("span",{"class": "progress_info_bold"})[0].contents[0]
			if "No card drops" in badgeLeftString:
				print time.strftime("%X - ") + "No card drops remaining"
				stillHaveDrops=0
			else:
				dropCountInt, junk = badgeLeftString.split(" ",1)
				dropCountInt = int(dropCountInt)
				delay = dropDelay(dropCountInt)
				print time.strftime("%X - ") + getAppName(k) + " has "+str(dropCountInt)+" card drops remaining"
		except:
			if maxFail>0:
				print time.strftime("%X - ") + "Error checking if drops are done, number of tries remaining: "+str(maxFail)
				maxFail-=1
			else:
				# Suspend operations until Steam can be reached.
				chillOut(k)
				maxFail+=1
				break

	idleClose(k)
	print time.strftime("%X - ") + "Successfully completed idling cards for "+getAppName(k)

print time.strftime("%X - ") + "Successfully completed idling process"
os.system('pause')
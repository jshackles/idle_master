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
	execfile("./settings.txt",authData)
	myProfileURL = "http://steamcommunity.com/profiles/"+authData["steamLogin"][:17]
except:
	print "Error loading config file"
	os.system('pause')
	sys.exit()
	
if not authData["sessionid"]:
	print "No sessionid set"
	os.system('pause')
	sys.exit()
	
if not authData["steamLogin"]:
	print "No steamLogin set"
	os.system('pause')
	sys.exit()

def generateCookies():
	global authData
	try:
		cookies = dict(sessionid=authData["sessionid"], steamLogin=authData["steamLogin"], steamparental=authData["steamparental"])
	except:
		print "Error setting cookies"
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
		print "Starting game " + getAppName(appID) + " to idle cards"
		subprocess.Popen("steam-idle.exe "+str(appID))
	except:
		print "Error launching steam-idle with game ID "+str(appID)
		os.system('pause')
		sys.exit()

def idleClose(appID):
	try:
		print "Closing game " + getAppName(appID) 
		os.system("taskkill.exe -im steam-idle.exe /F")
	except:
		print "Error closing game. Exiting."
		os.system('pause')
		sys.exit()

def chillOut(appID):
	print "Suspending operation for "+getAppName(appID)
	idleClose(appID)
	stillDown = True
	while stillDown:
		print "Sleeping for 5 minutes."
		time.sleep(5*60)
		try:
			rBadge = requests.get(myProfileURL+"/gamecards/"+str(appID)+"/",cookies=cookies)
			indBadgeData = bs4.BeautifulSoup(rBadge.text)
			badgeLeftString = indBadgeData.find_all("span",{"class": "progress_info_bold"})[0].contents[0]
			if "card drops" in badgeLeftString:
				stillDown = False
		except:
			print "Still unable to find drop info."
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
		print "No games have been blacklisted"

	return blacklist

try:
	cookies = generateCookies()
	r = requests.get(myProfileURL+"/badges/",cookies=cookies)
except:
	print "Error reading badge page"
	os.system('pause')
	sys.exit()
	
print "Finding games that have card drops remaining"

try:
	badgesLeft = {}
	badgePageData = bs4.BeautifulSoup(r.text)
	badgeSet = badgePageData.find_all("div",{"class": "badge_title_stats"})
except:
	print "Error finding drop info"
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
				print getAppName(badgeId) + " on blacklist, skipping game"
				continue
			else:
				badgesLeft[badgeId] = dropCountInt
	except:
		continue
		
print "Idle Master needs to idle " + str(len(badgesLeft)) + " games"

for k, v in badgesLeft.items():
	delay = dropDelay(int(v))
	stillHaveDrops=1
	numCycles=50
	maxFail=2
	
	idleOpen(k)

	while stillHaveDrops==1:
		try:
			print "Sleeping for "+str(delay / 60)+" minutes"
			time.sleep(delay)
			numCycles-=1
			if numCycles<1: # Sanity check against infinite loop
				stillHaveDrops=0

			print "Checking to see if "+getAppName(k)+" has remaining card drops"
			rBadge = requests.get(myProfileURL+"/gamecards/"+str(k)+"/",cookies=cookies)
			indBadgeData = bs4.BeautifulSoup(rBadge.text)
			badgeLeftString = indBadgeData.find_all("span",{"class": "progress_info_bold"})[0].contents[0]
			if "No card drops" in badgeLeftString:
				print "No card drops remaining"
				stillHaveDrops=0
			else:
				dropCountInt, junk = badgeLeftString.split(" ",1)
				dropCountInt = int(dropCountInt)
				delay = dropDelay(dropCountInt)
				print getAppName(k) + " has "+str(dropCountInt)+" card drops remaining"
		except:
			if maxFail>0:
				print "Error checking if drops are done, number of tries remaining: "+str(maxFail)
				maxFail-=1
			else:
				# Suspend operations until Steam can be reached.
				chillOut(k)
				maxFail+=1
			break

	idleClose(k)
	print "Successfully completed idling cards for "+getAppName(k)

print "Successfully completed idling process"
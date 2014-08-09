import requests
import cookielib
import bs4
import time
import subprocess
import sys
import os
import json

os.chdir(os.path.abspath(os.path.dirname(sys.argv[0])))

try:
	authData={}
	authData["sort"]=""
	execfile("./settings.txt",authData)
	myProfileURL = "http://steamcommunity.com/profiles/"+authData["steamLogin"][:17]
except:
	print "Error loading config file"
	raw_input("Press Enter to continue...")
	sys.exit()
	
if not authData["sessionid"]:
	print "No sessionid set"
	raw_input("Press Enter to continue...")
	sys.exit()
	
if not authData["steamLogin"]:
	print "No steamLogin set"
	raw_input("Press Enter to continue...")
	sys.exit()

def generateCookies():
	global authData
	try:
		cookies = dict(sessionid=authData["sessionid"], steamLogin=authData["steamLogin"], steamparental=authData["steamparental"])
	except:
		print "Error setting cookies"
		raw_input("Press Enter to continue...")
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
		global process_idle
		if sys.platform.startswith('win32'):
			process_idle = subprocess.Popen("steam-idle.exe "+str(appID))
		elif sys.platform.startswith('darwin'):
			process_idle = subprocess.Popen(["./steam-idle", str(appID)])
		elif sys.platform.startswith('linux'):
			process_idle = subprocess.Popen(["python2", "steam-idle.py", str(appID)])
	except:
		print "Error launching steam-idle with game ID "+str(appID)
		raw_input("Press Enter to continue...")
		sys.exit()

def idleClose(appID):
	try:
		print "Closing game " + getAppName(appID) 
		process_idle.terminate()
	except:
		print "Error closing game. Exiting."
		raw_input("Press Enter to continue...")
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

print "Finding games that have card drops remaining"

try:
	cookies = generateCookies()
	r = requests.get(myProfileURL+"/badges/",cookies=cookies)
except:
	print "Error reading badge page"
	raw_input("Press Enter to continue...")
	sys.exit()

try:
	badgesLeft = []
	badgePageData = bs4.BeautifulSoup(r.text)
	badgeSet = badgePageData.find_all("div",{"class": "badge_title_stats"})
except:
	print "Error finding drop info"
	raw_input("Press Enter to continue...")
	sys.exit()

userinfo = badgePageData.find("div",{"class": "user_avatar"})
if not userinfo:
	print "Invalid cookie data, cannot log in to Steam"
	raw_input("Press Enter to continue...")
	sys.exit()

blacklist = get_blacklist()

if authData["sort"]=="mostvalue" or authData["sort"]=="leastvalue":
	print "Getting card values, please wait..."

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
				if authData["sort"]=="mostvalue" or authData["sort"]=="leastvalue":
					gameValue = requests.get("http://api.enhancedsteam.com/market_data/average_card_price/?appid="+str(badgeId)+"&cur=usd")
					push = [badgeId, dropCountInt, float(str(gameValue.text)) * dropCountInt]
					badgesLeft.append(push)
				else:
					push = [badgeId, dropCountInt, 0]
					badgesLeft.append(push)
	except:
		continue

print "Idle Master needs to idle " + str(len(badgesLeft)) + " games"

def getKey(item):
	if authData["sort"]=="mostcards" or authData["sort"]=="leastcards":
		return item[1]
	elif authData["sort"]=="mostvalue" or authData["sort"]=="leastvalue":
		return item[2]
	else:
		return item[0]

sortValues = ["", "mostcards", "leastcards", "mostvalue", "leastvalue"]
if authData["sort"] in sortValues:
	if authData["sort"]=="":
		games = badgesLeft
	if authData["sort"]=="mostcards" or authData["sort"]=="mostvalue":
		games = sorted(badgesLeft, key=getKey, reverse=True)
	if authData["sort"]=="leastcards" or authData["sort"]=="leastvalue":
		games = sorted(badgesLeft, key=getKey, reverse=False)
else:
	print "Invalid sort value"
	raw_input("Press Enter to continue...")
	sys.exit()

for appID, drops, value in games:
	delay = dropDelay(int(drops))
	stillHaveDrops=1
	numCycles=50
	maxFail=2
	
	idleOpen(appID)

	while stillHaveDrops==1:
		try:
			print "Sleeping for "+str(delay / 60)+" minutes"
			time.sleep(delay)
			numCycles-=1
			if numCycles<1: # Sanity check against infinite loop
				stillHaveDrops=0

			print "Checking to see if "+getAppName(appID)+" has remaining card drops"
			rBadge = requests.get(myProfileURL+"/gamecards/"+str(appID)+"/",cookies=cookies)
			indBadgeData = bs4.BeautifulSoup(rBadge.text)
			badgeLeftString = indBadgeData.find_all("span",{"class": "progress_info_bold"})[0].contents[0]
			if "No card drops" in badgeLeftString:
				print "No card drops remaining"
				stillHaveDrops=0
			else:
				dropCountInt, junk = badgeLeftString.split(" ",1)
				dropCountInt = int(dropCountInt)
				delay = dropDelay(dropCountInt)
				print getAppName(appID) + " has "+str(dropCountInt)+" card drops remaining"
		except:
			if maxFail>0:
				print "Error checking if drops are done, number of tries remaining: "+str(maxFail)
				maxFail-=1
			else:
				# Suspend operations until Steam can be reached.
				chillOut(appID)
				maxFail+=1
				break

	idleClose(appID)
	print "Successfully completed idling cards for "+getAppName(appID)

print "Successfully completed idling process"
raw_input("Press Enter to continue...")
import sys
import subprocess
import time

def idleOpen(appID):
    print "Starting game " + str(appID) + " to idle cards"
    global process_idle
    if sys.platform.startswith('win32'):
        process_idle = subprocess.Popen("steam-idle.exe "+str(appID))
    elif sys.platform.startswith('darwin'):
        process_idle = subprocess.Popen(["./steam-idle", str(appID)])
    elif sys.platform.startswith('linux'):
        process_idle = subprocess.Popen(["python2", "steam-idle.py", str(appID)])


def idleClose(appID):
    print "Closing game " + str(appID)
    process_idle.terminate()

if __name__ == '__main__':
    appID = 440
    idleOpen(appID)
    time.sleep(10)
    idleClose(appID)
    
    time.sleep(5)
    
    appID = 570
    idleOpen(appID)
    time.sleep(10)
    idleClose(appID)
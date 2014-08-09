from __future__ import print_function
import os
import sys
import platform
import io
from PIL import Image, ImageTk
from ctypes import CDLL
try: #Python 2
    from urllib2 import urlopen
except ImportError: # Python 3
    from urllib.request import urlopen
try:
    import Tkinter as tk
except ImportError:
    import tkinter as tk

def get_steam_api():
    if sys.platform.startswith('win32'):
        print('Loading Windows library')
        steam_api = CDLL('steam_api.dll')
    elif sys.platform.startswith('linux'):
        if platform.architecture()[0].startswith('32bit'):
            print('Loading Linux 32bit library')
            steam_api = CDLL('./libsteam_api32.so')
        elif platform.architecture()[0].startswith('64bit'):
            print('Loading Linux 64bit library')
            steam_api = CDLL('./libsteam_api64.so')
        else:
            print('Linux architecture not supported')
    elif sys.platform.startswith('darwin'):
        print('Loading OSX library')
        steam_api = CDLL('./libsteam_api.dylib')
    else:
        print('Operating system not supported')
        sys.exit()
        
    return steam_api

    
def init_gui(str_app_id):
    gui = tk.Tk()
    gui.title('App ' + str_app_id)
    gui.resizable(0,0)
    try:
        url = "http://cdn.akamai.steamstatic.com/steam/apps/" + str_app_id + "/header_292x136.jpg"
        image_bytes = urlopen(url).read()
        data_stream = io.BytesIO(image_bytes)
        pil_image = Image.open(data_stream)
        tk_image = ImageTk.PhotoImage(pil_image)
        label = tk.Label(gui, image=tk_image)
        label.image = tk_image
    except:
        label = tk.Label(gui, text="Couldn't load image")
        
    label.pack()
    return gui
    
if __name__ == '__main__':
    if len(sys.argv) != 2:
        print("Wrong number of arguments")
        sys.exit()
        
    str_app_id = sys.argv[1]
    
    os.environ["SteamAppId"] = str_app_id
    try:
        get_steam_api().SteamAPI_Init()
    except:
        print("Couldn't initialize Steam API")
        sys.exit()
        
    gui = init_gui(str_app_id)
    gui.mainloop()
    
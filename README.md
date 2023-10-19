# Grimalkin 
A 3D immersive game built in Unity teaches users the danger of gaze tracking through devices in the home, including phones, computers, tablets and security cameras.  Throughout the game, the user will experience many direct or indirect interactions with devices based on the provided scenario.  Each of the interactions targets a specific exploitation of gaze data in real life that is evidenced by research.  Some examples of the risks taught are price and compulsive behaviour manipulation, data theft, personal data inference and contextualized advertising.
- [Grimalkin](#grimalkin)
- [Run](#run)
  - [Run pre-compiled version (RECOMMENDED)](#run-pre-compiled-version-recommended)
  - [Run self-compiled version](#run-self-compiled-version)
- [Code base structure](#code-base-structure)
  - [code guide](#code-guide)
  - [Script structure](#script-structure)
- [Getting started](#getting-started)
- [Security](#security)
- [External sources used](#external-sources-used)

# Run
## Run pre-compiled version (RECOMMENDED)
1. Navigate to **_/Grimalkin_compiled/_**
2. Open the folder corresponding to your operating system of choice and follow the next steps:
---
**WINDOWS:**
- Open the _/Grimalkin_compiled/windows/_ folder
- Run _'Grimalkin.exe'_
- You should now be playing! Press [ESC] at any time to bring up a menu to quit to desktop
---
**MAC:**
- Open the _/Grimalkin_compiled/mac/_ folder
- Run the app within this folder
- You should now be playing! Press [ESC] at any time to bring up a menu to quit to desktop
---
**LINUX:**
- Open the _/Grimalkin_compiled/linux/_ folder
- Run _'grimalkin'_
- You should now be playing! Press [ESC] at any time to bring up a menu to quit to desktop

## Run self-compiled version
1. Download Unity HUB from https://unity.com/download for your required operating system
2. Download the Unity EDITOR version _"2021.3.20f1"_ from https://unity.com/releases/editor/archive for your required operating system
3. Check both have been downloaded correctly
---
4. After those two programs are downloaded open the Unity hub
5. Click the drop-down arrow next to the grey open button
6. Click "Add project from disk"
7. Navigate to the _Codebase/Grimalkin/_ folder which should be in the same folder as this README
8. Select that to open in unity
---
9. Now the project should appear in your Unity Hub
10. Click on the Grimalkin project in Unity Hub
11. This should open the Unity editor
---
12. Once the project opens click on "file" on the top bar of the editor
13. Click on "build settings" from the drop-down
14. Specify your build operating system under "target platform"
15. Click Build and select any location that you want to build to
16. Go to the location that you selected that you wanted to build to and run the file following the "pre-complied" version instructions


_PLEASE NOTE: Due to certain protection settings on files that Unity imposes when you manually zip a project it can sometimes result in meta files not having the correct permissions and not being able to be read. If you are running into problems with the zipped version of the project please contact the dev team as we can give you access to the git repo to pull and compile as this will not have these issues. (This is a KNOWN issue with zipping up the unity project however this is the method of submission)_

# Code base structure
The full code base can be found under **_/Grimalkin/_**
- However, NOTE that due to this being a unity project a lot of the files are just config or asset files and will therefore be unreadable
- To find the scripts that were developed to produce the game please go to **_/Grimalkin/Assets/Scripts_**

## code guide
- __player scripts__: scripts that handle general player input and game progression 
  - player_controller.cs 
  - player_observer.cs 
  - story_controller.cs
- __interaction scripts__ : scripts that execute a specific interaction  
- __UI scripts__: scripts that handle GUI interactions
  - UI_notification_controller.cs
  - UI_pause_menu_controller.cs
- __miscellanies scripts__ : short utility scripts, used to serve one function  
  - door_hover_controler.cs
  - door_sliding_controller.cs
  - bed_controller.cs
  - boxes_controller.cs
  - camping_item_controller.cs
  - coffee_controller.cs
  - observable_object



## Script structure 
Every script is started with a header explaining its purpose and the objects it is attached to:

        /* 
        * Project Grimalkin
        * Author: 
        * 
        * Purpose:
        * - the purpose of the script
        *   
        * 
        * Attached to objects in-game scene:
        * - The unity game objects this script is attached to
        */

        code according to the C# standards 
Look at player_observer.cs for examples, in case of any confusion or contact the dev team for further clarification.

# Getting started
- ## Creating a new script
  - open the project in unity
  - navigate to **Assets/Scripts_** in unity 
    - if the new script runs throughout the game it should be created at **Assets/Scripts/player**
    -  if the script is for an interaction it should be created at **Assets/Scripts/objects_interactions**
    -  if it UI UI-related it should be created at **Assets/Scripts/UI**
    -  otherwise, it should be created at **Assets/Scripts/objects_misc**
  - write the script according to [Script structure ](#script-structure)
  - follow the [Security](#security) structure if the script is using user data and update the README 
  - add the script to the relevant objects
- ## Adding a new observable object
    An observable object is an object that has a list of topics and whenever the player looks at the object the list of topics is added to a database. The database is used by other parts of the game like the targeted advertising  
  - add the new object 3d model to **Assets/Models**
  - add the new object to the scene 
  - add an observable_object component to it 
  - add the topics related to the object under the info



# Security
several scripts deal with data collected from the user. To ensure their privacy, the following structure is used when handling such data.

    //*******************************
    // user data in the use section start 
    //*******************************
    code using the data 
    //-------------------------------
    // user data in use section end
    //-------------------------------

Scripts that include that structure
- player_observer.cs
- SM_phone_controller.cs
- demo_profile_controller.cs 

# External sources used
this is a list of all the assets used in the project, including 3D models, code, and other miscellaneous. 
  - https://assetstore.unity.com/packages/3d/props/electronics/web-camera-164934
  - https://assetstore.unity.com/packages/3d/environments/apartment-kit-124055
  - https://assetstore.unity.com/packages/3d/props/electronics/next-gen-camera-37365
  - https://assetstore.unity.com/packages/3d/props/electronics/free-pbr-security-camera-70061
  - https://assetstore.unity.com/packages/3d/props/furniture/washing-machine-a01-70644
  - https://assetstore.unity.com/packages/3d/props/electronics/free-laptop-90315
  - https://assetstore.unity.com/packages/3d/props/tools/simple-keys-231162
  - https://assetstore.unity.com/packages/3d/environments/sci-fi/free-sci-fi-office-pack-195067
  - https://assetstore.unity.com/packages/3d/vegetation/plants/cactus-pack-11547
  - https://assetstore.unity.com/packages/3d/environments/minimalist-archviz-bedroom-131093
  - https://assetstore.unity.com/packages/3d/environments/modern-archviz-leafless-108308


Written by: Tim Ryall, Fahed Alhanaee

# Grimalkin 
A 3D immersive game built in Unity teaches users the danger of gaze tracking through devices in the home, including phones, computers, tablets and security cameras.  Throughout the game, the user will experience many direct or indirect interactions with devices based on the provided scenario.  Each of the interactions targets a specific exploitation of gaze data in real life that is evidenced by research.  Some examples of the risks taught are price and compulsive behavior manipulation, data theft, personal data inference and contextualized advertising.

- If you wish to play the game for yourself in its current state please head to [Run pre-compiled version (RECOMMENDED)](#run-pre-compiled-version-recommended)
- If you wish to view a gameplay demo of the game please view **_Grimalkin_gameplay_demo.mp4_**

This document was designed for a seamless handover between teams. It will cover the installation, running, unity basics, project code structure and an overview of the current code base. Head to [Getting started](#getting-started) if this is your first time using this code base and you wish to contribute to development. 
- [Grimalkin](#grimalkin)
- [Run](#run)
  - [Run pre-compiled version (RECOMMENDED)](#run-pre-compiled-version-recommended)
  - [Run self-compiled version](#run-self-compiled-version)
- [Code base structure](#code-base-structure)
- [Getting started](#getting-started)
- [Common design patterns used](#common-design-patterns-used)
- [Getting started examples](#getting-started-examples)
  - [Adding an observable object](#adding-an-observable-object)
  - [Script naming conventions](#script-naming-conventions)
  - [Code guide](#code-guide)
  - [Script structure](#script-structure)
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

_NOTE that in some cases do to configuration settings on your system there may be blocks on running exe / apps from unknow sources. Please ensure you disable such settings or this will not work. There could be any number of reasons why it doesn't work as zipping in the format required for submission is not recommended for unity projects. For futher advice please see [Trusting a file](https://support.microsoft.com/en-us/windows/add-an-exclusion-to-windows-security-811816c0-4dfd-af4a-47e4-c301afe13b26) and please do not hesitate to reach out to the team._

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
- However, NOTE that due to this being a unity project a lot of the files are just config or asset files and will therefore be unreadable.
- To find the scripts that were developed to produce the game please go to **_/Grimalkin/Assets/Scripts_**.

# Getting started
- To get started with editing the project for your self you will first need to open up the game scene that the game is played within at **_/Grimalkin/Assets/Final_scene.Unity_**.
- All the objects within the game will here be in the scene. 
- This means to add any object to the game all you have to do is add it to the scene.
- To navigate the scene and place objects please see [Unity scene interaction documentation](https://docs.unity3d.com/Manual/SceneViewNavigation.html).
- To see how objects in the scene are nested within each other look to the left of the screen at the object hierarchy.
- In our project all: 
  - environment based objects should be placed under 'Environment'.
  - UI based objects should be placed under 'Canvas'.
- You can toggle the active state of the objects in the scene by right clicking and clicking 'toggle active state' please see [Unity object documentation](https://docs.unity3d.com/Manual/GameObjects.html) for more details.
- You should hopefully feel a bit more comfortable with the scene that the game is played within and how to directly manipulate what is around you.
- You can now implement interactions within the game in any way you see fit, however we will run through a couple of common design patterns in the next section.

# Common design patterns used
   -  ## Adding a new observable object
        An observable object is an object that has a list of topics and whenever the player looks at the object the list of topics is added to a database. The database is used by other parts of the game like the targeted advertising  
        - Add the new object 3d model to **Assets/Models**
        - Add the new object to the scene 
        - Position the object in the scene
        - Add an observable_object component to it by using the inspector on the left 
        - Add the topics related to the object under the info

  - ## Creating a new script
     - Open the project in unity
     - Navigate to **Assets/Scripts_** in unity 
       - If the new script runs throughout the game it should be created at **Assets/Scripts/player**
       -  If the script is for an interaction it should be created at **Assets/Scripts/objects_interactions**
       -  If it UI UI-related it should be created at **Assets/Scripts/UI**
       -  Otherwise, it should be created at **Assets/Scripts/objects_misc**
     - Write the script according to [Script structure ](#script-structure)
     - Follow the [Security](#security) structure if the script is using user data and update the README 
     - Add the script to the relevant objects by attaching it within the Unity editor (for more info see the [Unity Script Documentation](https://docs.unity3d.com/Manual/CreatingAndUsingScripts.html))

# Getting started examples
This section is to help people get the project running and run through some example to get more familiar with editing the project and adding to it.

Prior basic knowledge of unity will be helpful but not necessary. Going through [Unity getting started](https://unity.com/learn/get-started) will help. Each unity   

## Adding an observable object
This example will run through how to add an observable object to the scene. For this simple example we will just be adding a cube, but it can be any object you would like.

1. Start by following the [Run self-compiled version](#run-self-compiled-version) instructions, once you have it running you can move to the next step.
2. We will now add our 'observable cube', a cube can be added to the scene by right-clicking on the hierarchy, and you will find Cube under 3D Object. 
3. To test that the cube is working as intended, will create a new script to fetch the current observation and see if it contains a cube. You will need to follow the [Creating a new script](#creating-a-new-script) instructions and modify the new script to match the following.

        // the player_observer class is in charge of
        // keeping track of the objects that have been observed
        public player_observer myplayer_observer;

        void Update()
        {
            // try to get a value for the topic "cube"
            myObs.observations.total.TryGetValue("cube", out var count);
            // if count is 0 then it must not have been looked at this play thorough  
            if (count <= 0){
                Debug.Log("have not looked at the cube")
            } else {
                Debug.Log("the cube has been looked at")
            }
        }

4. remember to use [Security](#security) structure since where fetching user data, the script should look like this 
   
        // the player_observer class is in charge of
        // keeping track of the objects that have been observed
        public player_observer myplayer_observer;

        //*******************************
        // user data in the use section start 
        //*******************************
        void Update()
        {
            // try to get a value for the topic "cube"
            myObs.observations.total.TryGetValue("cube", out var count);
            // if count is 0 then it must not have been looked at this play thorough  
            if (count <= 0){
                Debug.Log("have not looked at the cube")
            } else {
                Debug.Log("the cube has been looked at")
            }
        }
        //-------------------------------
        // user data in use section end
        //-------------------------------
5. Attaching the script to an object, we could attach it to the player object since this script will be running non-stop in the game.
6. Add the reference to player_observer to the script in the inspector, the player_observer is attached to the observer object under the player. To Add the reference drag the observer object to the slot in the script in the inspector
7. after step 6, if you start the game you should get a bunch of "have not looked at the cube" messages in console and once you look at the cube the messages should turn to "the cube has been looked at".
   
You should be somewhat familiar with the code structure and how it works with unity at this point. To learn more about the whole code base look at [Code base structure](#code-base-structure)

## Script naming conventions 
This section will explain all the Script naming conventions used in the project
 - *_controller.cs is used to describe a script that controls a behaver in the game.
 - AT_* script that is used in the alarm tablet interaction
 - OS_* script that is used in the online shopping interaction
 - PP_* script that is used in the passcode phone interaction
- SM_* script that is used in the social media interaction

## Code guide
The code base can be split into 4 main parts, Player scripts, Interaction scripts, UI scripts and Miscellanies scripts. This section will explain what each part dose and list the script that belong to them.

- ### __Player__:
   Scripts that handle general player input including biosensor, mouse and keyboard then process them and also handles game progression  
  - player_controller.cs 
  - player_observer.cs 
  - story_controller.cs


- ### __Interaction scripts__ : 
  Scripts that execute a specific interaction, these scripts get enabled by the story_controller as the story progress.
  - alarm tablet - all files that start with AT_
  - online shopping - all files that start with OS_
  - passcode phone - all files that start with PP_
  - SM_phone_controller.cs
  - demo_object_controller.cs
  - demo_profile_controller.cs
- ###  __UI scripts__:
  Scripts that handle GUI interactions
  - UI_notification_controller.cs
  - UI_pause_menu_controller.cs
- ### __Miscellanies scripts__ :
   Short utility scripts, used to serve one function or interface with other scripts 
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

# Security
Several scripts deal with data collected from the user. To ensure transparency and easy location of data practices used, the following structure is used when handling such data.

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

Each script will detail how data privacy and security is ensured and describe how and why we use the data in this section. 

*(Note that in the current version this is not the case as Tobii support is not in this version)*

# External sources used
This is a list of all the assets used within the code base of the project, including 3D models, code, and other miscellaneous. 
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

Written by: Tim Ryall

# Play pre-compiled version (RECOMMENED)
1. Navigate to **_/Grimalkin_compiled/_**
2. Open the folder corressponding to your operating system of choice and follow the next steps:
---
**WINDOWS:**
- Open the _/Grimalkin_compiled/windows/_ folder
- Run _'Grimalkin.exe'_
- You should now be playing! Press [ESC] at any time to bring up menu to quit to desktop
---
**MAC:** _NOTE: this version is not fully up to date as the Mac developers on the team are currerently on holiday_
- Open the _/Grimalkin_compiled/mac/_ folder
- Run the app within this folder
- You should now be playing! Press [ESC] at any time to bring up menu to quit to desktop
---
**LINUX:**
- Open the _/Grimalkin_compiled/linux/_ folder
- Run _'grimalkin'_
- You should now be playing! Press [ESC] at any time to bring up menu to quit to desktop

# Play self-compiled version
1. Download Unity HUB from https://unity.com/download for your required operating system
2. Download the Unity EDITOR version _"2021.3.20f1"_ from https://unity.com/releases/editor/archive for your required operating system
3. Check both have downloaded correctly
---
4. After those two programs are downloaded open Unity hub
5. Click the drop down arrow next to the grey open button
6. Click "Add project from disk"
7. Navigate to the _Codebase/Grimalkin/_ folder that should be in the same folder as this README
8. Select that to open in unity
---
9. Now the project should appear in your Unity Hub
10. Click on the Grimalkin project in Unity Hub
11. This should open the Unity editor
---
12. Once the project opens click on "file" on the top bar of the editor
13. Click on "build settings" from the drop down
14. Specify your bulid operating system under "target platform"
15. Click Build and select any location that you want to build to

16. Go to the location that you selected that you wanted to build to and run the file following the "pre-complied" version instructions

# Code base structure
The full code base can be found under **_/Grimalkin/_**
- However NOTE that due to this being a unity project alot of the files are just config or asset files and will therefore be unreadable
- To find the scripts that were developed to produce the game please go to **_/Grimalkin/Assets/Scripts_**


# DEV INFO
**main** branch: is the main dev branch

**working-preview** branch: is the branch that we merge to ONLY after the changes in main are working

# NEW FEATURES:
For every new feature developed pelase created a new feature branch called:

"feature/{feature-name}"

And then make a pull request to merge back into the main branch

# COMMITS
For each commit please have it in the form:
- fix: bla bla (for a fix commit)
- feat: bla bla (for a feature commit)
- wip: bla bla (for a commit that is a work in progress)


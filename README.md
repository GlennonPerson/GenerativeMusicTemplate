# GenerativeMusicTemplate 

## Requirements
- Unity 2022.3.X LTS
- FMOD Studio 2.02.XX
- Packages (pre-installed):
  - FMOD Unity Integration
  - Cinemachine

## Structure
- Unity Project folder: Contains the Unity project with working demo scene
- FMOD Project folder: Contains FMOD Studio project and audio assets

## Setup Instructions
1. Download and unzip the template
2. Open Unity Hub > Add project from disk > GenerativeMusicTemplate > GenerativeMusic_Unity 
3. Open unity project
4. Ensure FMOD is importated in the package manager (this may require restarting Unity)
5. Go to Menu -> FMOD -> Edit Settings -> Studio Project Path -> GenerativeMusicTemplate > GenerativeMusic_FMOD > Generative Music (Ludic Sound Lab).fspro
6. Open FMOD Project (Generative Music (Ludic Sound Lab).fspro) in FMOD Studio
7. Go to File -> Build. This rebuilds the master bank locally on your device.

## Features
- FMOD Sound Integration
- Three instruments (Sound1, Sound2, Sound3). Each features a multi-instrument FMOD event that plays harmonically related notes at random from a list upon collision. 

## Troubleshooting
- Ensure that FMOD is properly imported in the package manager of the Untiy project
- Check Unity - > Menu -> FMOD -> Edit Settings to make sure the .fspro file is connected
- Unity may need to restart after initially opening the project.
- It will be necessary to rebuild the FMOD banks. Go to the FMOD project, File -> Build. 
- If Unity project appears blank go to Assets -> Scenes and select LudicGameLab


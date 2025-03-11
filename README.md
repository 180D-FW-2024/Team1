# BruinRush Readme

## Unity Project
This folder contains the actual Unity Project which implements the entire game and integrates all of the other features.
### Assets
This folder contains all of the assets and objects used in the game, and also the scripts within the Scripts folder

### Build
This folder contains the built executable file that is the output of this project and can be launched without having Unity installed.

## bt_drivers and run_script
This folder contains the source code for the IMU and bluetooth drivers. Many of the bluetooth drivers come from btferret, an open-source HID emulation library. run_script is the OS service which runs on boot for the controller. 
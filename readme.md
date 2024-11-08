# AAU project mediaology 7th semester

project description

## Usage
The project exists in two parts. This would prefferably be run on two different machines, however two bat files are provided in case one wishes to run the project on the same machine. This however is not recommended, as we run two itensive systems on the graphics card, and may lead to lag inside the vr experience.

To run the project, download from https://github.com/aaumta7/mta7/releases. You will get two folders. Inside the unity folder, run the program as any normal unity project, and connect any OpenXR compatible headset. On another machine, open the python folder and run 

`pip install -r ./requirements.txt`



After this, open the install.bat file, and insert your huggingface.co oauth token. Finally run the install.bat file.

Finally, choose between the run.bat and runlocally.bat files.

This will give you a command face with an ip for the SD models. Insert this inside the game in unity, and thereafter pick up the camera, pose the model, and press the trigger.


run this `New-NetFirewallRule -DisplayName "Allow Inbound Port 54321" -Direction Inbound -Protocol TCP -LocalPort 54321 -Action Allow` to open port
# CoVoX 
### Cloud enabled library providing a customizable voice-interface for your application or your device

![DraftDraw](assets/CoVoXGraph.png)

Covox allows the interaction with an application or device through voice.
You provide a list of _Commands_, i.e. operations that can be invoked via the voice interface,
Covox then listens to the audio and when a command matches with the spoken words, it's executed.
It also has multi-language support!

![DraftDraw](assets/CoVoX_MLGraph.png)

With some imagination, you could control a calculator, or a virtual assistant, or even CRM application!

## Sample

Taking a simple usecase (light switch application): the client application must use Covox and configure it with a set of commands (i.e. switch on the light, switch off the light). Each of the commands will have a unique identifier and a set of voice triggers.  

| Identifier   |      Voice Triggers
|----------|:-------------:
| COMMAND_TURN_ON |  switch on the light, turn on light ...
| COMMAND_TURN_OFF |    switch off the light, turn off light ...

<br>
The application starts the Covox engine audio recognition that listens for voice triggers. 
When Covox detects that a voice trigger matches one of the given commands voice trigger [i.e. switch on the light], it returns the corresponding command (i.e. TURN_ON_COMMAND) to the client application.

Then the application will perform the action matching with the recognized command.

## The Engine

With the use of cloud cognitive services, this engine enables to convert voice instructions into actions or intents defined by a use case.

The engine follow these stages:

1) **listening:** translates voice into text<br/>
  using [Speech to Text](https://azure.microsoft.com/en-us/services/cognitive-services/speech-to-text/)

2) **translating:** allows the user to speak in any language, by transforming the sentence into a base language (english)<br/>
  using [Text Translation](https://azure.microsoft.com/en-us/services/cognitive-services/translator)<br/>
  using [Speech Translation](https://azure.microsoft.com/en-us/services/cognitive-services/speech-translation/)

3) **understanding:** matches the intent to an available command given the current context<br/>
  using [Language Understanding
](https://azure.microsoft.com/en-us/services/cognitive-services/language-understanding-intelligent-service/)

4) **executing:** invokes the command using the use case's logic

## Use case proposals

<details>
  <summary><b>Base use case</b></summary>
  
## Overview

Basic showcase of the engine and commands invocation.

## Commands:
- turn on the lights<br/>
  output: "I turned on the lights"
- turn off the lights<br/>
  output: "I turned off the lights"

## Technologies
- CoVoX engine

<hr/>

</details>

<details>
  <summary><b>Robobutler</b></summary>
  
## Overview
Robobutler is a robot capable of executing voice triggered actions based on its perception of the current environment. The idea is that an operator can tell the robot to "Bring me the yellow box" and the robot will in this case do the following:
1. Confirm/Repeat the task the robot was told to do
2. Go to the yellow box
3. Pick it up
4. Bring it to the operator

## Other possible scenarios:
- Placing a box on top of another
- Basic movements (Stop, rotate, etc)
- Spatial awarness (e.g. go to the nearest corner)

## Robo to use

https://www.dji.com/de/robomaster-s1

The desired configuration would be an industrial arm on top of a body with wheels to represent a valid scenario for the industry.

## Technologies
- CoVoX engine
- [Azure computer vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)
- Python (to control the robot)

<hr/>

</details>

<details>
  <summary><b>Pac-Scream</b></summary>
  
## Overview

Pac-Scream is a variant on the popular game Pac-Man, in which movements are defined via voice commands instead of keys press.

![image](https://user-images.githubusercontent.com/8939890/106443307-9e549e00-647c-11eb-921f-dd25ed5d0bfb.png)

## Commands:
- left / move left
- right / move right
- up / move up
- down / move down
- *(proposal)* stop / cancel / no<br/>
  *to cancel the previous command*

## Technologies
- CoVoX engine
- ASP.NET Core 5
- SignalR
- WebGL

<hr/>

</details>

<details>
  <summary><b>Guess-Who</b></summary>

## Overview 
Guess Who is a game for 2 players.  Each player has a "playing field" with different people and a fixed person, which must be guessed by the opponent, by exclusion questions.  
Via Voice commands you should be able to ask a question, such as, "Does the woman have red hair?" 
Image recognition should then return the answer yes / no. 

![image](https://user-images.githubusercontent.com/8939890/106584417-716cbd80-6546-11eb-8fe4-40b047dee3c4.png)

## Procedure:
1. Asking a Question via Voice Command
2. Recognize and process question
3. Looking at e.g. Image and detect the answer 
4. Returning Answer (Yes / No)

## Technologies
- CoVoX engine
- Python / Tensorflow 
- [Face](https://azure.microsoft.com/en-us/services/cognitive-services/face/)

<hr/>

</details>

<details>
  <summary><b>Find-It</b></summary>

## Overview 
Find-it it's a Mobile App that is able to recognize objects in an image, or in a video, from user voice request.
Given an image or a video, if the user requests to see a particular object, the application will create a box around the object that match the description.


## Technologies
- CoVoX engine
- [Flutter](https://flutter.dev/?gclid=CjwKCAiAgc-ABhA7EiwAjev-j209M2n1IrpNH86tVHhSkPU5ED2KyUM6Rj8IkBVu2N8kD-fgoxIC_RoCuI4QAvD_BwE&gclsrc=aw.ds)
- [Azure computer vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)

<hr/>

</details>

<details>
  <summary><b>Voice-Unlock</b></summary>

## Overview 
Voice-Unlock showcases the voice recognition service from azure. An application will display a locked lock. If the authorized user says "Unlock", the lock should unlock. Instead, if an unauthorized users says "Unlock" the background flashes a few seconds in red.


## Technologies
- CoVoX engine
- [Speaker Recognition](https://azure.microsoft.com/en-us/services/cognitive-services/speaker-recognition/)
- VueJS application

<hr/>

</details>

## Technologies
The library is developed in .NET 5 and uses the Azure's Cognitive Services.

## RoadMap
#### Fist Release
- Simple commands
- No commands-context
- Multi-language support
- Dll only

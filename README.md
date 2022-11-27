# Doodle Bounce

[Doodle Bounce Gameplay Video](https://youtu.be/XllRptleswA)

## Team Members
Sally Zhao

Fabrice Wong Kwok

Herman Wang

Winnie Chen



## Brief explanation of the game

The aim of the game is to complete levels by bouncing off platforms to reach the top. Levels involve controlling a player who bounces off of a variety of different platform types to get higher and higher, dodging/defeating monsters along the way and eventually reaching the end of the level. Players advance through increasingly difficult levels which introduce more advanced platform types, and eventually reach the edge of space, to the tune of “The Final Countdown”.


## How to Play/Use it

### Title Screen

Once the game launches, the player can press the start button on the title screen to go into level select. 

### Level Select

There are 6 levels in total. Level 0 and Level 1 are unlocked at the start, with every passing of a level unlocking the next level. Clicking on a level launches the level. 

The levels of the game are themed as follows:  

1. Tutorial (Blue Gem)
2. Lava
3. Underwater
4. Mountains
5. Sky
6. Space

The level themes are chosen from the centre of the earth to space, which gives a feeling of jumping further and further. 


### In-Game

Once in-game, the player takes control of the character, controls for simple movement and projectile shooting are introduced during the tutorial level. 


#### Controls

The playable character is constantly jumping, with the height depending on the set “bounciness” of the platform it lands on.

To move the character left or right, the player must use either the “A” or “D” or “Left” or “Right” arrow keys on a standard QWERTY keyboard. 

Using the mouse to aim, the player can shoot using the left mouse button in a cone above the character. The projectile is used to shoot down monsters trying to block the player from going up.

In addition, “Left Shift” and “Space Bar” can be held down to speed up the horizontal movement of the player. 


#### Platforms

There are a variety of different platforms. Which are made up of traits shown below. As players progress through the different levels, along with experiencing an epic soundtrack, they also encounter more difficult platform and platform modifier combinations.


##### Platform Type (exactly one of)



*   Static (don’t move)
*   Zoom (zoom in from the side of the screen)
*   Pop-out (pop out for the player to land on)
*   Moving (moves from side to side)


##### Platform Modifiers (stackable)



*   Super Bouncy
*   Breakable (will disappear after 1 bounce)
*   Smaller (about as wide as the player model)


#### “Water”

Each level has ‘toxic’ water that rises as the player’s y coordinate increases. If the player misses a block and falls down far enough to come in contact with the water, the player will ‘die’ and be shown the ‘game over menu’ and given the option to restart the level.


#### Enemies

Enemies are also randomly and procedurally spawned into the level. Players must either dodge them or shoot them with the mouse to despawn them. If players touch an alive monster, they will also ‘die’ akin to touching the ‘toxic water’.


#### User Interface


##### Time

Current time elapsed is shown at the top of the screen. 


##### Progress bar

How far away the player is currently from the end of the level. Progress can also be gauged audibly, as the soundtrack pitch subtly increases in pitch as players’ height increases.


##### Pause

Press the Escape key or the pause button at the top left corner to stop the game and press the “resume” button or escape key once again to return the game. The pause menu shows the time of the game and the progress that has been completed as well as two buttons to return the game or back to the menu.


##### Return to the main menu

The player can stop the game at any time and exit to the main menu by clicking 

the top left to go back button.


###### Level complete

Character successfully finishes the level.

The player goes back to the main menu.


###### Game Over

If the player either falls into the rising water/lava or hits a monster. The game over screen is displayed, the button restart restarts the level and the button main menu returns you to the level select screen.


## How objects and entities were modelled


### Player Character

The main playable character model is from the asset store, a custom cell shading shader is used on the character to fit the low poly aesthetic of the game. 

The bounding box of the character was chosen to be a cube that bounds the legs and lower body, this allows for some leniency on hitting monsters physically and losing the game, while also accurately reflecting the losing state of touching the rising water/lava. As the game speed is quite fast, the decision was made to use continuous collision detection in order to remove tunnelling issues between the rising water/magma and the player. The game overall is quite light, so the extra computing power needed for a robust collision system was worth it in our minds.

By freezing the rigidbody component in the Z-axis, and freezing all rotation, the 3d game is locked into 2d movement. Built-in gravity was used on the player, and the rigidbodies’ velocity was manipulated for the player’s non stop bouncing. Additional parameters were added in order to speed up the fall and rise of the player during it’s jump movement. Depending on the platform, the player’s added velocity upwards would differ, with the super platform giving a jump pad like boost up to the player, to help better illustrate this, a trail was added in the vertical direction for when the player hit such a jump pad.

The controls were chosen to adhere to the widely used WASD as directions convention in gaming. Additionally, the arrow keys are also available to those who wish to do so. From there, the additional speed up modifier to complement the WASD control scheme was made available via either the left shift or space bar. The firing of the projectiles in the game for comfort was given to the right hand, through the use of a mouse to aim, and the most used left mouse button to shoot. Particle effects were added to highlight when the shift modifier was on, and accentuate the projectile’s motion. 

To communicate the gamestate through audio, sound effects were applied both on when the player is jumping on a normal platform, a super platform, and hitting its head on the bottom of a static platform, this helps give an audio differentiator as well as the visual one to help with the player’s decision making. 


### Platforms

The 3d modelling of the platforms was relatively simple, due to the low-poly art style we decided to go with. As a result, they are just cubes that were stretched out a little bit to make them a bit wider. However, we still needed to give platforms different behaviour and convey the different behaviours to the players.

All the platforms share the same prefab type (except for small and large, however, I suspect they could be combined for more efficiency), and all the differences are handled by the platformScript, which changes the platform's attributes based on variables that can be adjusted in the inspector.

Platform types (modifiers that can’t be stacked) are conveyed through colour. For example, static platforms that don’t move are red and platforms that ‘zoom’ in from the side are green.

Platform modifiers such as bounciness and breakability, however, could not be shown with colour, as they are stackable with other modifiers and types. If it were to override the colour, players may find it difficult to tell different platform types apart. Instead, other visual cues were used, such as a ‘bubble’ particle effect to show a super bouncy platform and a flashing one which to convey that it is breakable.

The bubble effect was achieved by adding a particle generator on _all _platforms, but only enabled when the _isSuperBounce_ variable was set to _true_. Flashing was achieved by toggling the _emission _on the platform’s material on and off every second or so (if the correct boolean variable was set to _true_).

Platforms are randomly spawned on a _middles _prefab, except for the start and end blocks. The _middles_ prefab is a 20 by 20 plane that get procedurally generated and stacked as the player climbs existing platforms. As players are unable to fall too far downwards, each 20 by 20 blocks is also automatically despawned as soon as the player is high enough away from it so that the camera cannot see it to save on memory. This, in theory, allows for an infinitely high (or as high as the unity engine can handle) procedurally generated level.


### Rising Water/Lava

The chasing water/lava was modelled using a plane and a script. Using fitting textures and shaders to model the thing that would be chasing. The script simply follows the player at a set distance below it. In the event of a collision between the player and chaser, the game ends. This is done via the player’s onCollisionEnter method and all chasers are set to the water layer to differentiate between normal platform collisions and chaser collisions.


### Background/World

Backgrounds for all scenes are designed by ourselves using models from unity asset store. A lot of time was spent to design backgrounds that suit the topic for each level and have a consistent aesthetic at the same time.


### User Interface

The aim of the User Interface is to have logical and unambiguous design and attract users’ attention at the right time.


##### start menu

Game name and ”start” button are positioned at the middle of the screen when <span style="text-decoration:underline;">maximizing on play</span> to attract users’ attention. Also, white is chosen as the colour of the text and button to contrast with the black background.


##### In-game: Pause & Return to the main menu & time

To have a logical and unambiguous design of in-game UI, we used colours of white in the periphery for the "back to menu" and "pause" button in-game. They are positioned at the top left corner which is similar to most games so that users are used to it. “Time” is positioned at the top so users can check the time easily while playing.


##### Progress bar 

"progress bar" has the colour of white and red and was put at the right edge of the in-game UI. The aim is to tell users the progress of the game and not attract too much attention while the user is playing. 


##### Guiding/warning messages  

For the guiding messages of the tutorial level, we added a background colour of blood orange for those messages and display at the beginning of the tutorial progressively to attract the attention of the player. The same thing was done for warning messages.


##### Level Complete/Game Over/Pause menu

While the level is completed/game is over/pause is pressed, the timeScale would be set to zero and the corresponding panel will pop up to show the status of the game. The background of the pop-up panel is dark grey with some transparency and the colour of the text is white. The aim is to attract the attention of the user.


### Monsters

To guarantee the consistency of the theme in each level, we decided to create different monsters in the different levels. Level 1 is lava theme, the monster is globules of once-molten rock; level 2 is the underwater theme, so we choose a monster which is kind of like turtle; the monster in level 3 is stone monster since level 3’s theme is mountain; level 4 is eagle to fit the theme of the sky; in level 5, we chose a monster that looks like an alien because players are fighting in space in level 5.

All the monsters are low-poly 3D models from the asset store to fit the low-poly art style. All the monsters are prefab and create depending on the distance between two platforms. If the distance between the new platform generated and the old platform is larger than 11.5, the monster will create, this will give the player enough time to react and kill the monster. 

In level 1 and 2, the monster is static and appears between two platforms; in level 3, 4 and 5, the monster will appear a little far away from the platforms and move towards the player with a certain speed. This difference is handled by the monsterController script which can make the game become more difficult progressively. 


## Graphics Pipeline

For the rising water in our levels, we wanted to emulate a wave-like animation to make it look a little less static. To achieve this, we wanted to make a simple sinusoidal up and down motion for each point in the water plane. This, however, could not have realistically been achieved with scripts to manually move individual nodes in the water plane. If there were not enough nodes in the plane, waves would appear ‘clunky’ with a lot of straight, jagged edges. However, if the surface was too subdivided, the CPU would have a difficult time updating each individual node while keeping a reasonable framerate. Instead, we used an implementation of a vertex shader on the rendering pipeline so that no nodes have to be moved.

Most non-trivial objects in the game were taken from the asset store, where they most likely were modelled with triangles. More trivial objects such as the plane used for the rising lava/water feature, used unity’s built-in mesh. We left the default backface culling on to reduce the number of vertices to be rendered in the scene. Overall, as the game involved mostly non-trivial objects, the frame rate was able to maintain it’s high value throughout gameplay and rendering of levels.


## Camera Motion

Camera motion was relatively simple to model. At some point in the project, we tried to implement a more “dynamic” camera model which would follow the player on the x and y axes and also tilt to smooth out more sudden movements, however, due to the high motion nature of the game, we found it to be quite motion-sick-inducing. In the end, we decided on a linear boom motion of the camera, which followed the character’s y coordinate. Changes in the character’s x coordinate do not move the camera.


## Shading

Cell shading and water shader. The water shader in the vertex shader component, displaces the vertices in the y-axis by a factor of time and the x-axis in order to simulate a horizontal wave. This is beneficial in that the CPU doesn’t have to also handle the movement of the vertices, it instead is handled by the GPU, reducing overall strain on the CPU. The water shader also allows for shadows on the surface, a guide on unity for fragment shaders was consulted for the shader. The texture used fits the aesthetic of the game and is scrolled by a script to seem more believable. 

The Toonshader uses a heavily modified adaptation of the Phong illumination model, as well as being heavily guided by a cell shading guide by RoyStan. No modifications were made to the vertices in the vertex shader for the player. In the fragment shader, the ambient component was replaced by a colour to represent the ambient light. The diffuse component assumed the diffuse constant as 1, leaving attenuation as a parameter to be modified. The specular component was ignored, as the player character this shader is to be used on would not be specular in order to give a more cartoony look. 


## Evaluation Techniques

After an afternoon of testing and evaluating, the feedback from both methods of the evaluation was then consolidated into a list of improvements noting the participants’ level of familiarity with gaming when compiling that list and deciding on which feedback to act upon. The compiled list of feedback is attached.


#### Observational methods

[link to compiled data](https://docs.google.com/document/d/1Vc-q_XLCfa6NezDwGTLNVXp2eftxGsjrbl2s_NodayQ/edit)

8 young adult university students of varying levels of gaming experience.

We had participants play the game while streaming it over discord, relaying their thoughts on the game as they played it. Questions were also freely exchanged between us and the participants. The participants would play until they either finished all the available levels or got stuck on a level to the point where they did not wish to continue. Notes were taken throughout the testing session regarding the players’ thoughts and responses to the questions asked.

Although we were unable to get through as many participants with the Observational method, we were able to get more detailed and specific information. An example would be different bugs/issues found in the game. As each participant had a slightly different play-style, we were able to discover issues that we would otherwise have not discovered on our own. The player’s experience was more obvious through hearing our participants speak on their thoughts on different parts of the game. This along with audible frustration heard through a call, allows us  to extrapolate emotional responses to certain parts of the game such as confusion, enjoyment, or even surprise.


#### Querying methods

[Link to raw data (Unimelb google account login required.)](https://docs.google.com/spreadsheets/d/16udFmqvVHk18yaytABKXNW8cFJgrpJXNmU8Y-dAbNuo/edit?usp=sharing)

[Link to survey (Unimelb google account login required.)](https://forms.gle/GkCezrvLVm2r7DRZ6)

12 young adult university students of varying levels of gaming experience and skill.

![survey data](https://i.imgur.com/IrXRj1n.png)

Before we commenced testing, as a group, we made a survey on google forms that we planned to give testers after they had an opportunity to test out our game. Unlike observational feedback, we were able to go through more testers with the querying method as we were not required to be present while testers were playing the game. This allowed us to get more accurate feedback on more subjective information such as difficulty, enjoyability and visual appeal, as we were able to average out extreme views or even biases.


## Changes from feedback


### Tutorial

From the questionnaire sent out to testers, we found that many people did not notice the tutorial that was flashed on the screen briefly on the first level. 

To solve this, we created a special tutorial level that the user has to complete before being introduced to the ‘real’ game. This forces the players to try the features and platforms at least once before they see it in-game.


### Platforms

While we were observationally getting feedback from testers, we found that players were not given the opportunity to properly learn the different platform types. As a result, players felt a bit overwhelmed by the sheer amount of different platforms introduced at once. This was also reflected in the survey we sent out, where “too many platforms” was one of the primary causes of the game being too difficult to play (30% of players).

From this feedback, in addition to adding a special tutorial stage, we also decided to progressively introduce different platform types to the player, level by level. This prevents the player from being overwhelmed as they only need to learn one or two platform types per level. This also gives players an increased feeling of progression, as well as diversity as they progress through the levels.


### Water

Through the feedback, the threat of losing the game by hitting the rising water was not made clear. To fix this, the water/lava was made to look more threatening through textures and particle effects. This in addition with the first level giving a warning to the player in the form of very readable text, should allow the player to understand that hitting the water/lava kills. The textures/particle effects were also chosen in such a way to make it stand out and be always visible, allowing the player to gauge how far it is from the player at all times.


### Sound

During testing, we also discovered that players found the sound design rather poor and quite repetitive, especially with the background music. Although some players found the pitch changing with the player’s height quite jarring they also reported that it was not a bad idea.

As a fix, we made the change in pitch far more subtle to make it less jarring, while also getting more soundtracks for different levels to keep repetition to a minimum.


### UI

A few issues with the user interface were also discovered during our testing phase, such as some graphical elements overlapping and other elements being too small/difficult to see.

Resolving this issue was rather simple, it was just a question of increasing the font size of some elements and rearranging others to ensure the player can easily see and make out the different elements.


### General bugs/glitches

Observational testing allowed us to find many different bugs and issues that we would not have encountered.

From this, we were able to compile and arrange a list of bugs that we needed to fix in order of priority, allowing us to efficiently and quickly resolve them. A list of all feedback found and fixed is attached, as mentioned above.


## Member Contributions


### Fabrice



*   Level generation scripts
*   Platform generation and behavior scripts
*   Audio-related scripts
*   Feedback and evaluation gathering

### Herman



*   Creation of custom shaders
*   Particle effects
*   Player logic 
*   Projectile logic
*   Feedback and evaluation gathering


### Sally



*   Pause Menu; Game Over Canvas
*   Camera script; Monster Controller; Game Over Canvas Controller and Pause Menu Controller
*   Feedback and evaluation gathering


### Winnie



*   Built 6 scenes including 1 menu scene, 1 level selecting scene, 4 level scenes
*   HUD canvas, game complete canvas, main menu canvas, tutorial canvas
*   Game logic scripts including GameCompleteCanvasController, GlobalOptions, HUDController, LevelsController, MainMenuController, TutorialCompleteController
*   Platforms pop-out effect script
*   Feedback and evaluation gathering


## References/Credit



*   iTween (unity asset) - used for some smooth movements of platforms
    *   [https://assetstore.unity.com/packages/tools/animation/itween-84](https://assetstore.unity.com/packages/tools/animation/itween-84)
*   RoyStan tutorials - used as guides for shader making 
    *   [https://roystan.net/articles/toon-shader.html](https://roystan.net/articles/toon-shader.html)
*   “Abhinav a.k.a Demkeys” particle effect tutorial
    *   [https://www.youtube.com/watch?v=agr-QEsYwD0](https://www.youtube.com/watch?v=agr-QEsYwD0)
*   Unity writing vertex fragment shaders guide
    *   [https://docs.unity3d.com/Manual/SL-ShaderPrograms.html](https://docs.unity3d.com/Manual/SL-ShaderPrograms.html)

### Audio

#### Sound Effects

All sound effects taken from: [https://freesound.org/](https://freesound.org/)

#### Music

[Daryl Hall & John Oates - Out Of Touch](https://www.youtube.com/watch?v=D00M2KZH1J0)

[Bill Conti - Gonna Fly Now](https://www.youtube.com/watch?v=ioE_O7Lm0I4)

[The Beach Boys - Surfin' USA](https://www.youtube.com/watch?v=EDb303T-B1w)

[Dick Dale - Miserlou](https://www.youtube.com/watch?v=sYjo__4COIo)

[Stan Bush - The Touch](https://www.youtube.com/watch?v=sS6RvWE8KdY)

[Europe - The Final Countdown](https://www.youtube.com/watch?v=9jK-NcRmVcw)


### Unity asset store
#### Used for building background scenes

[https://assetstore.unity.com/packages/3d/props/simple-gems-ultimate-animated-customizable-pack-73764](https://assetstore.unity.com/packages/3d/props/simple-gems-ultimate-animated-customizable-pack-73764)

[https://assetstore.unity.com/packages/3d/environments/low-poly-style-environment-72471](https://assetstore.unity.com/packages/3d/environments/low-poly-style-environment-72471)

[https://assetstore.unity.com/packages/3d/environments/fantasy/low-poly-lava-world-87505](https://assetstore.unity.com/packages/3d/environments/fantasy/low-poly-lava-world-87505)

[https://assetstore.unity.com/packages/3d/environments/sci-fi/low-poly-cosmos-project-planets-142199](https://assetstore.unity.com/packages/3d/environments/sci-fi/low-poly-cosmos-project-planets-142199)

[https://assetstore.unity.com/packages/2d/textures-materials/space-star-field-backgrounds-109689](https://assetstore.unity.com/packages/2d/textures-materials/space-star-field-backgrounds-109689)

[https://assetstore.unity.com/packages/3d/environments/landscapes/stylized-earth-94673](https://assetstore.unity.com/packages/3d/environments/landscapes/stylized-earth-94673)

[https://assetstore.unity.com/packages/3d/environments/low-poly-s-pack-vol-2-46375](https://assetstore.unity.com/packages/3d/environments/low-poly-s-pack-vol-2-46375)

[https://assetstore.unity.com/packages/vfx/particles/environment/forest-mist-111204](https://assetstore.unity.com/packages/vfx/particles/environment/forest-mist-111204)

[https://assetstore.unity.com/packages/3d/3le-low-poly-cloud-pack-65911](https://assetstore.unity.com/packages/3d/3le-low-poly-cloud-pack-65911)

[https://assetstore.unity.com/packages/3d/environments/low-poly-underwater-scene-assets-112672]
(https://assetstore.unity.com/packages/3d/environments/low-poly-underwater-scene-assets-112672)



#### Used for monsters

[https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-partners-pbr-polyart-168251?fbclid=IwAR1UlX8Rm4g_nKWbPCqMCG0FRQDRroztIDuO0-zg8aJEY7xtbcsotfiWTPM#content](https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-partners-pbr-polyart-168251?fbclid=IwAR1UlX8Rm4g_nKWbPCqMCG0FRQDRroztIDuO0-zg8aJEY7xtbcsotfiWTPM#content)

[https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-duo-pbr-polyart-157762?fbclid=IwAR0pBRWVp2TM4CL_9TSv9mn--3EF-BpV5bUXcxXGSr08nH72unb_Gu4KQbI](https://assetstore.unity.com/packages/3d/characters/creatures/rpg-monster-duo-pbr-polyart-157762?fbclid=IwAR0pBRWVp2TM4CL_9TSv9mn--3EF-BpV5bUXcxXGSr08nH72unb_Gu4KQbI)

[https://assetstore.unity.com/packages/3d/characters/animals/birds/egypt-pack-eagle-140079?fbclid=IwAR3ioxN-Y5pU63l9ZoozyuUrgx4pv-f36f-GWJoxvteAUl9CJopz3Jc-DhQ](https://assetstore.unity.com/packages/3d/characters/animals/birds/egypt-pack-eagle-140079?fbclid=IwAR3ioxN-Y5pU63l9ZoozyuUrgx4pv-f36f-GWJoxvteAUl9CJopz3Jc-DhQ)

[https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/mini-legion-rock-golem-pbr-hp-polyart-94707?fbclid=IwAR3l8EHuBwviXniDp_zqk_c3rW3H3q3hkBNNPN0LenpzRvcSp_wUeP9q0NY](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/mini-legion-rock-golem-pbr-hp-polyart-94707?fbclid=IwAR3l8EHuBwviXniDp_zqk_c3rW3H3q3hkBNNPN0LenpzRvcSp_wUeP9q0NY)

[https://assetstore.unity.com/packages/3d/characters/stone-monster-101433](https://assetstore.unity.com/packages/3d/characters/stone-monster-101433)



#### Used for text font

[https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059#reviews](https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059#reviews)



#### Used for player

[https://assetstore.unity.com/packages/3d/characters/meshtint-free-boximon-fiery-mega-toon-series-153958](https://assetstore.unity.com/packages/3d/characters/meshtint-free-boximon-fiery-mega-toon-series-153958)

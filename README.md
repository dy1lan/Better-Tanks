# Better-Tanks
A simple tank game, initially based off of Unity's Tank game for tutorials.


# Introduction 

This assignment was to create our own 3 Dimensional Tank game, which contains enemies, health and realistic physics, for the most part.  This program was supposed to be based off of the "Tank!" Game tutorial created by Unity. The following are the minimum requirements for the game: 
    
1. Design a battle scene that has at least the following objects. 
   1. Terrain.  Use the Terrain tool in Unity to generate the terrain with a minimum size of 200 m × 200 m.  Attach texture(s) to the terrain. 
   1. Tank.  You must use the tank model provided by the instructor.  You need to generate a collider for the tank.  A box collider is recommended for the tank. 
   1. Still objects such as buildings and trees and your enemies are hidden in these objects.  You can generate the models in Unity or obtain the models from other           sources as described in next task.  Generate necessary colliders for most objects in your scene. 
2. Obtain free or paid 3D models from various sources.  
3. You need to use standard exported 3D files.  Unity supports FBX, dae (Collada), 3DS, dxf, and obj files, you need to convert your 3D models to one of these             formats if necessary.  You can use the following tools to convert your 3D models.
   1. Google SketchUp Pro (You can have 8 hours of trial use)
   1. Autodesk Maya (free for students)
   1. Microsoft Visual Studio
4. Generate an overhead camera that captures the entire scene and a third person camera for the tank controlled by the player.  The two camera views can be switched by pressing the F(f) key.
Game play
5. The overall goal is to shoot your hidden enemies, which can also shoot you from different locations.
6. The player controls her/his own tank using the following keys: W (move forward), S (move backward), A (turn left), D (turn right), Left (rotate turret left), Right (rotate turret right), Up (raise cannon), Down (lower down cannon), Space (fire).
7. Write a script to control the tank movement.  Note that front and rear wheels have different sizes and thus they must rotate at different speeds.  The front (steering) wheels must not deviate from their central positions for more than 45 degrees.  Similarly, the cannon must move in the range from its reset position to 90 degrees vertically. The tank’s orientation is controlled by the front wheels and the tank’s movement is very similar to that of a car, e.g., the tank can only turn when the tank is moving, while the front wheels can turn when the car is still.  The tank’s longitudinal movement must be in synchronization with the wheel rotation.  In other words, the tank must not appear sliding without enough wheel rotation or stalling with too much wheel rotation. 
You need to attach a Rigidbody to the tank and move the tank using Rigidbody.MovePosition() and Rigidbody.MoveRotation() methods in the FixedUpdate() in the tank controller. Using Rigidbody instead of Transform to move the tank produces correct physics interactions between the tank and other objects. Also, do not move the tank by adding forces.  Note that you cannot use the Wheel Collider provided by Unity in this assignment.  Refer to the Unity Tanks! Tutorial for the overall movement of the tank.
8. The shells fired by your tank (by pressing spacebar) should follow a trajectory determined by a projectile motion.  Enemy fires can follow projectile motions or straight-line motions depending on their ammunitions.  Add sound effects for the shells and enemy ammunitions.
9. Write a script to control the enemy fires.  Enemy fires occur only when your tank appears within a certain range from the enemy in terms of distance and direction (angle).  Enemy fire speed and frequency should be adjustable as well.
10.	When an enemy fires a bullet, the orientation of the bullet must be consistent with its traveling direction, i.e., its motion must be longitudinal.
11.	Generate a Unity prefab for the tank shells and use that prefab in your game.  Similarly generate prefabs for other necessary game objects.
12.	Use particle system to simulate the fire and smoke in shell explosions.
13.	Your tank has a life of 20, that is, it will die after being shot for 20 times.  Your enemies only have a life of 3, that is, they will die after being shot three times.
14.	Display the lives of the player (tank) remaining and press key R(r) to restart the game.
15.	Generate different folders for different types of assets in your games.

These tasks were all handled and carried out efficiently and sometimes even exceeding the basic expectation of the tasks at hand.  

# Game Design and Implementation 
 
 For Doxygen, summaries for each task have been added. My .chm file is located inside Better Tanks and is called Project_4.chm, my Doxygen configuration file is also located inside Better Tanks and is called Doxygen Config. The Doxygen documentation results are inside Better Tanks->Documentation. 

 For my game, I only went with 1 Scene and tried to make it more enjoyable to play. I started off by creating some steep, mountainous range around the map to prevent the player from going over the edge and to give it a more realistic feel. Then, I added some trees, grass, and even water to the map for the nature feel and began adding the desert buildings with enemies on top or in front of the buildings. The game is enjoyable to play and even a bit difficult with the default enemy fire frequency. I plan to add more and expand on it in the future. 
 
 All of my 3D model prefabs were sourced from the Unity Asset store for free and implemented to the best of my current ability. To reference the Assets used:
-	“Standard Assets (for Unity 2018.4)” by Unity Technologies
-	 “Rockets, Missiles & Bombs - Cartoon Low Poly Pack” by AurynSky
-	“Ammunition pack (demo)” by Evgeny Korchuganov
-	“Grenade Sound FX” by MGWSoundDesign
-	“Toon Soldiers WW2 demo” by Polygon Blacksmith
-	“Desert Buildings” by Lukas Bobor
-	“Grass Flowers Pack Free” by ALP8310

User Controls: 
- “W” = Up/Forward 
- “S” = Down/Reverse 
- “A” = Left 
- “D” = Right 
- Arrow keys Left and Right = move turret Left and Right 
- Arrow keys Up and Down = move canon Up and Down 
- “F” = Change between Camera Views 
- “R” = Restart Game 
- “Space” = Fire Missile 
- “Escape” = Quit Game 

# Results

 For this Assignment I got started by looking over all the code provided for the Tanks game that Unity had created. I, then, created a brand new scene and started with adding the terrain feature and molding it to fit my liking: 
 
![Figure 1: Empty Terrain](https://user-images.githubusercontent.com/22224999/180853870-1d80c239-fa09-4d93-923c-196258d4ce3a.png "Figure 1: Empty Terrain") 

Once I got the terrain to be as smooth as I needed for a box collider to slide over it, I began adding in the player’s tank. I added a Box collider and Rigidbody to the Tank and then began writing a script to move the tank with the correct inputs that followed the requirement listed above. One of the hardest things to accomplish was getting the front wheels to turn to a certain angle when pressed. It is still very touchy but works in the beginning as shown: 

 ![Figure 2: Wheels Turning Left](https://user-images.githubusercontent.com/22224999/180854093-fed2339b-8e4b-4d40-8a8e-d22f0cd62e53.png "Figure 2: Wheels Turning Left") 

![Figure 3: Wheels Turning Right](https://user-images.githubusercontent.com/22224999/180854601-57104d1e-943c-4cfe-8725-da83c3b14c1c.png "Figure 3: Wheels Turning Right") 

After getting the main players tank to work and move around properly, I began to add the extra buildings, trees and enemies into the scene in random location and make it look decent. I used the building from the Desert Building asset that was download because this already had the textures and everything needed to be implemented easily, I just had to add in the Box colliders. 

![Figure 4: Desert Building with Box Colliders](https://user-images.githubusercontent.com/22224999/180854698-4ae01133-29ef-4ef5-b77f-8eeae2366f6c.png "Figure 4: Desert Building with Box Colliders") 
 
 The enemies came from the Toon Soldiers asset with the prefabs and animations already in it. I added a few enemies throughout the map and gave the prefab a Rigidbody and Box Collider to tell when a missile from the player hits them. I then added an Enemy controller to control the shooting and health of the enemy, I will talk more about that later on in the document. 
 
  To generate an overhead camera view, I simply added another camera component to the scene that followed the position and rotation of the Tank. The overhead camera couldn’t fit the whole terrain in it because you wouldn’t have been able to see the Tank because it would be so small to the camera. 
  
![Figure 5: Overhead Camera View](https://user-images.githubusercontent.com/22224999/180854772-4694d294-8480-4497-a97c-634f5adb75ef.png "Figure 5: Overhead Camera View") 

The next task, was to get the Tank to shoot rockets and the enemies to shoot back at the tank. This took a lot of troubleshooting to get the rocket to fire like a projectile and explode on impact and not register multiple impacts from one impact. The rocket also produces a particle system on impact and makes a sound that comes exactly from where it was exploded. The bullet mimics the rocket, but it follows a straight line, this was mainly because I created characters across the water and if they shoot at the Tank, their bullets drop to the water and never hit the tank. The bullets make a minor sound effect, not a big explosion like the tank rocket does. 

 ![Figure 6: Rocket Model](https://user-images.githubusercontent.com/22224999/180855014-dcbabbd6-c74f-4f9e-a1df-e2d6318e4f84.png "Figure 6: Rocket Model") ![Figure 7: Bullet Model](https://user-images.githubusercontent.com/22224999/180855212-4b166e64-ebfb-4bb1-82ec-213c29629606.png "Figure 7: Bullet Model")


**Not To Scale 

The final task, was to create a way to display the health of the Tank and the Enemies. I decided to do this by making a simple UI health bar that I put over the enemies and one on top of the Tank. This was simple to create using sliders and google. I followed the tutorial posted on: https://github.com/Brackeys/Health-Bar. 

![Figure 8: Player with Health Bar](https://user-images.githubusercontent.com/22224999/180855427-77b07d53-6eb4-4a84-b208-3494e5c6b15c.png "Figure 8: Player with Health Bar") 

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

# 

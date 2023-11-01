## Task Description
As part of a contract with a fictional spy agency, create a piece of software that assists field agents in gathering information from highly secure facilities. The software must represent a menu allowing an agent to specify obstacles to their mission and provide information to the agent upon request including the ability to determine a safe path to an objective.

This task requires the use of object oriented programming within a .Net 6.0 runtime using C#. The codebase must be well documented and feature proper use of exception handling. 

| Est. Completion Time | Weighting |
|----------------------|-----------|
| 25-50 hours          | 50%       |

## User Interface
The program will present a menu to the user with a series of options, each consisting of a one-letter code and a short description like so: 
```
Select one of the following options  
g) Add 'Guard' obstacle  
f) Add 'Fence' obstacle  
s) Add 'Sensor' obstacle  
c) Add 'Camera' obstacle  
d) Show safe directions  
m) Display obstacle map  
p) Find safe path  
x) Exit  
Enter code:
```
Each menu option will have its own action to perform, and may ask the user for further inputs. For instance, if the user were to input ***g*** then they will be prompted to add a guard's location:
```
Enter code:  
g  
Enter the guard's location (X,Y):  
2,1
```
The program may also be used for navigation. The simplest form is option ***d*** which shows safe directions an agent may travel in (assuming the guard is still located at ***2,1*** as specified above): 
```
Enter code:  
d  
Enter your current location (X,Y):  
2,2  
You can safely take any of the following directions: SEW
```
However, agents can also request a safe path to an objective. With no obstacles, this will create as straight a path as possible to an objective.
```
Enter code:  
p  
Enter your current location (X,Y):  
3,5  
Enter the location of your objective (X,Y):  
7,1  
The following path will take you to the objective:  
NNNNEEEE
```
This takes the agent four clicks North, and then four clicks East. However, most grids will feature some form of obstacle(s). For instance, if the agent observes a fence:
```
Enter code:  
f  
Enter the location where the fence starts (X,Y):  
3,3  
Enter the location where the fence ends (X,Y):  
7,3
...
Enter code:  
p  
Enter your current location (X,Y):  
3,5  
Enter the location of your objective (X,Y):  
7,1  
The following path will take you to the objective:  
NEEEEENNNW
```
The program must take into account any and all obstacles when determining a path for the agent. In this case, the program routes the agent around the fence by travelling East along row 4, until reaching column 8, then travelling North until finally moving one square West to reach their objective. 

The software must also be able to produce a simple ASCII representation of the obstacles added thus far. Assuming the user has added two guards and a fence to the program, they can request a map with ***m*** and should receive something similar to this:
```
Enter code:  
m  
Enter the location of the top-left cell of the map (X,Y):  
0,0  
Enter the location of the bottom-right cell of the map (X,Y):  
8,7
.........  
....g.g..  
.........  
...fffff.  
.........  
.........  
.........  
.........
```
### The Menu
Unlike the horror/thriller movie of the same name, the menu in this program is far less tantalizing. The menu may differ slightly from the norm if a custom obstacle is implemented (more on this later) but each menu item must be the same format
`a) Short Description of Function`
Breaking this down further, the first component is a single, lower-case letter, which is followed by a closing bracket ***")"*** and finally a brief description. 

After displaying the `Enter code:` text, the user must respond with a valid menu option. If they do not, and instead respond with an invalid option, the program must return a message `Invalid option.` (note: including the full stop at the end) and then display `Enter code:` again, until the user inputs a valid option. 

`Invalid option.` should also be displayed if a user inputs an incorrect value for any of the potential inputs requested by the program. Adding a guard, fence, sensor or camera will prompt the user to add further information, typically an integer pair `0,0`, etc. or a direction `N, S, E, W` for instance. If the user inputs an incorrect value then the `Invalid input.` message should be displayed. 

When the user asks the program to ***show safe directions*** they are prompted to enter their location. If a user inputs their location within an obstacle, for instance if a guard's location is `2,1` and the user enters their position as `2,1` then the message `Agent, your location is compromised. Abort mission.` should be displayed. If an agent is blocked on all four sides by obstacles, then the message `You cannot safely move in any direction. Abort mission.` should be displayed. Otherwise, the program will display a list of directions that the user can move in (***Important! The list of directions must be in order N, S, E, W and presented as NSEW with no spaces/commas***) and then display a message, if all four directions are clear: `You can safely take any of the following directions: NSEW` but for example if there were obstacles to the North and East of the agent's location: `You can safely take any of the following directions: SW` would be displayed.

## UML

The program is made up of many different obstacles, each of which should be designed with an object oriented approach. This is a list of the obstacles in this program, and their intended functionality. 
### g) Guard
When the guard is added, the following prompt will be shown:
`Enter the guard's location (X,Y):`
The guard will obstruct the agent from entering the specified square, they do not have a field of vision or take up more than one square. They do not patrol. Their behaviour does not require an especially complex sub-class, included in their properties is a name because I find it amusing to play god.
### f) Fence
Much like the humble guard, the fence is a stationary obstacle however it must take up more than one square. It requires a starting point and an end point. Users will be prompted with the messages:
`Enter the location where the fence starts (X,Y):`
`Enter the location where the fence ends (X,Y):`
Then after entering these values, the fence will be placed on the map. A fence cannot occupy just one square and can only be vertical or horizontal. If either of these rules are not adhered to, the message: `Fences must be horizontal or vertical.` should be displayed. 
### s) Sensor
A sensor has a static location but also features a range of detection. If an agent moves within this range, they will be caught. Users will be prompted to enter the position in a X,Y integer pair and then the range, which is a ***floating point*** value, any squares within this range are considered to be obstructed. The range is measured in Euclidean distance. The prompts are as follows:
`Enter the sensor's location (X,Y):`
`Enter the sensor's range (in klicks):`
The range of the sensor is circular, and in order to detect whether a sensor's range covers a portion of a square on the grid we use the Pythagorean formula to compute the distance between each square and the sensor.

$$\sqrt{\left(x_1-x_2\right)^2+\left(y_1-y_2\right)^2}$$

So for example if a sensor is at ***(4,4)*** and has a range of ***2.5*** clicks, we can check if ***(2,6)*** is within the range of the sensor like so:

$$\sqrt{\left(2-4\right)^2+\left(6-4\right)^2}=2.828\ldots$$

2.828... is greater than 2.5, so ***(2,6)*** is not within the sensors range. Now consider ***(3,6)***:

$$\sqrt{\left(3-4\right)^2+\left(6-4\right)^2}=2.236\ldots$$

2.236... is less than 2.5, so ***(3,6)*** is within the sensors range, and therefor the agent cannot enter that square. Using this method we can determine the squares that the sensor can cover, consider the following diagram as a reference:
### c) Camera
Cameras are the final obstacle type that must be included. They are similar to sensors, in that they have a range of detection. Cameras have a position and a direction they must face. Their range of vision is infinite, extending to the bounds of the grid area. The user is prompted to enter the camera's position and then its direction:
`Enter the camera's location (X,Y):`
`Enter the direction the camera is facing (n, s, e or w):`
If the user enters an invalid direction such as `f` then the response: `Invalid direction.` should be shown, then they will be prompted again. Cameras will obstruct the agent entering its 90 degree cone of vision, which extends from 45 degrees clockwise from the camera's direction to 45 degrees anticlockwise from the same direction.
## Custom Obstacle Types
The program should be extensible and allow for custom obstacles to be added post-launch. This is achieved by creating a new class that inherits from the base IObstacle interface. Two new obstacles with varying functionality have been added to provide an example.
### s) Spotlight
The spotlight is a stationary obstacle that operates similarly to a sensor in that it can provide a circular area of detection and even uses the same formulae as the sensor the build that area. Unlike the sensor, the spotlight's range of detection is offset from its origin point and has a direction. The user must provide the position, direction and offset for the spotlight. The prompts are as follows:
`Enter the spotlight's location (X,Y):`,
`Enter the direction the spotlight is facing in (n, s, e or w):`,
`Enter the spotlight's range (in klicks):`. The spotlight's diameter is static. 
### q) Quicksand
Unlike every other obstacle so far, quicksand is a traversable obstacle. The quicksand can be walked over by the agent, and thus in the pathfinding algorithm it is not considered an obstacle. However, the quicksand can have a higher movement cost that a base cell, when taken into account by the algorithm, the agent will prefer to take a path without quicksand but if is faster to get to the objective by walking through quicksand, then the agent will do so. Additionally, the quicksand will be spread randomly across the board, based on its range. It should have a higher density towards the origin. The user is prompted to enter the position of the quicksand, its range and then the movement cost (depth). The prompts are as follows: 
`Enter the quicksand's location (X,Y):`, 
`Enter the range of the quicksand (in klicks).`,
`Please enter the depth of the quicksand (in metres) to determine the difficulty to cross.`.

# Implementation
Obstacles should inherit from a base class that holds the majority of their information:

## Obstacle Class
An interface that holds the shared members of each inheriting class, such as its positions and character code. 

| IObstacle (***Interface***)                    |
|:-----------------------------------------------|
| ***Fields/Properties***                        |
| + Positions: List<Coordinate>                  |
| + MovementCost: int                            |
| + LetterCode: Char                             |
| + Type: string                                 |
| + Priority: int                                |
| ***Methods***                                  |
| AddObstacle(ref Cell): void                    |
| IntersectsWithCell(Coordinate): bool           |

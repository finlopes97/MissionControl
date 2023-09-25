## Task Description
As part of a contract with a fictional spy agency, create a piece of software that assists field agents in gathering information from highly secure facilities. The software must represent a menu allowing an agent to specify obstacles to their mission and provide information to the agent upon request including the ability to determine a safe path to an objective.

This task requires the use of object oriented programming within a .Net 6.0 runtime using C#. The codebase must be well documented and feature proper use of exception handling. 

| Est. Completion Time | Weighting |
| -------------------- | --------- |
| 25-50 hours | 50% 

## What I Need to Do
---
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
However, agents can also request a safe path to an objective. With no obstacles, this will create as straight a path as possible to an objective, for instance on a grid such as this:
![[assessment2-grid.png]]
The following will be generated from a user's input:
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
Which would look something like this:
![[assessment2-grid-2.png]]
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
## User Interface
---
This program will be tested via Gradescope, so you will be tailoring your program for an unfeeling artificial intelligence, Jokes and japes will not be appreciated by our robot overlords, and certainly no references to spies in popular culture Agent 86. Grammatical and spelling mistakes can *spell* certain doom (pun intended) so be sure to be precise. You may submit to Gradescope as many times as you like and at any time, which is an excellent way to make sure you are conforming to the AI's skewed version of reality where programmers are unfeeling machines that don't put funny jokes in their exception tracebacks.  

### The Menu
Unlike the thriller movie of the same name, the menu in this program is far less tantalizing. Your implemented menu may differ slightly from the norm if a custom obstacle is implemented (more on this later) but each menu item must be the same format
`a) Short Description of Function`
Breaking this down further, the first component is a single, lower-case letter, which is followed by a closing bracket ***")"*** and finally a brief description. 

After displaying the `Enter code:` text, the user must respond with a valid menu option. If they do not, and instead respond with an invalid option, the program must return a message `Invalid option.` (note: including the full stop at the end) and then display `Enter code:` again, until the user inputs a valid option. 

`Invalid option.` should also be displayed if a user inputs an incorrect value for any of the potential inputs requested by the program. Adding a guard, fence, sensor or camera will prompt the user to add further information, typically an integer pair `0,0`, etc. or a direction `N, S, E, W` for instance. If the user inputs an incorrect value then the `Invalid input.` message should be displayed. 

When the user asks the program to ***show safe directions*** they are prompted to enter their location. If a user inputs their location within an obstacle, for instance if a guard's location is `2,1` and the user enters their position as `2,1` then the message `Agent, your location is compromised. Abort mission.` should be displayed. If an agent is blocked on all four sides by obstacles, then the message `You cannot safely move in any direction. Abort mission.` should be displayed. Otherwise, the program will display a list of directions that the user can move in (***Important! The list of directions must be in order N, S, E, W and presented as NSEW with no spaces/commas***) and then display a message, if all four directions are clear: `You can safely take any of the following directions: NSEW` but for example if there were obstacles to the North and East of the agent's location: `You can safely take any of the following directions: SW` would be displayed.

# Avenging Shadows
## Introduction
Avenging Shadows is a stealth game where the main character goes through various locations seeking for revenge for his mother's murder.

## Features
### AI
During this project we used different types of AI such as path finding, state machine and decorator design pattern:

#### Path Finding
##### NavMesh
 NavMesh is a powerful tool in Unity that allows the creation of navigation maps for characters, facilitating their movement in the game environment. The NavMesh can be generated automatically or manually, depending on the project's needs.

> **To automatically generate a NavMesh:**

1. Go to Window > AI > Navigation to open the navigation window.
2. Select the Bake tab.
3. Adjust the settings as necessary.
4. Click Bake to generate the NavMesh.

> **To use NavMesh for a character:**

1. Add a NavMeshAgent component to the character object.
2. Use the SetDestination() method to set the character's destination.

![navmeshComponent](https://github.com/Gominha123/Avenging_Shadows/assets/101401818/f76625fb-769f-45f1-a8cf-b6aff9d18f41)

![navmeshSetDestination](https://github.com/Gominha123/Avenging_Shadows/assets/101401818/42487988-5325-4304-af9b-35e46319187d)

> **A*** **in Unity's NavMesh:**

In the context of Unity's NavMesh system:

- Graph Representation: The NavMesh generates a navigation graph representing walkable surfaces in the game environment.

- Nodes: Nodes in the graph typically represent positions on the NavMesh.

- Cost Function: The cost function used by A* in Unity's NavMesh considers factors like distance, terrain cost, and any other custom parameters set in the NavMesh settings.

- Heuristic: A* uses a heuristic to estimate the remaining cost from a node to the goal. In Unity's NavMesh, this often involves calculating the straight-line distance between nodes.

- Pathfinding: The A* algorithm is applied by the NavMesh system to find the optimal path from the starting point to the destination, considering the cost function and heuristic.

- NavMeshAgent: The NavMeshAgent component, attached to game objects, uses the A* algorithm results to navigate through the NavMesh to reach the specified destination efficiently.

#### State Machine
##### Simple AI State Machine 
This Unity script implements a simple state machine for enemy AI behavior. A state machine is a computational model used to represent the various states an entity can be in and the transitions between these states. In this script, the AI has different states such as patrolling, following, searching, attacking, and dead.

> **What is a State Machine?**

 A state machine is a concept used in computer science and game development to model the behavior of an entity based on its current state and the events that trigger transitions between states. In this script, the AI can be in different states, and its behavior is determined by the current state. State machines help organize complex behaviors, making it easier to manage and understand the logic governing an entity's actions. States are defined as distinct conditions, and transitions between states occur based on specific criteria or events.

> **State Enumeration:**

The AIState enum represents the possible states of the AI:

- Patrolling: The AI moves between specified patrol points, rotating at each point.
- Waiting: After reaching a patrol point, the AI waits for a defined period before proceeding.
- Following: The AI pursues a target within its field of view and a defined pursuit range.
- SearchingLostTarget: When the target is lost, the AI navigates to the last known position.
- Attacking: The AI attacks the target if within the attack range and cooldown period has elapsed.
- Dead: The AI enters this state upon death, disabling certain components and behaviors.

![StateMachine](https://github.com/Gominha123/Avenging_Shadows/assets/101401818/f18beea8-a6a8-46b9-8fd4-edc5ab61250f)

>**State Functions:**

Each state has a corresponding state function, such as Patrolling(), Waiting(), etc. These functions define the behavior of the AI in each state and are invoked based on the current state during the Update() loop.

> **Usage:**
- Attach this script to the enemy GameObject.
- Configure the patrol points, ranges, and other parameters in the Unity Editor.
- Ensure the FOVEnemies component is attached to handle the field of view.
- Run the scene and observe the AI's behavior based on the defined states.

#### Decorator
The weapon upgrade system uses the Decorator Design Pattern which essentially allows you to change the components of an object in a class without actually changing the class in question.  Said that, this design pattern is initialized with an interface or an abstract class to which the weapons class inherits from. Also, there exists a decorator that inherits from that interface but also contains an object from that interface, and a concrete decorator which inherits from the previous decorator, meaning that it technically is from the interface type. For visual reference, the following picture shows a diagram where we can see all the classes and their connection in terms of inheritance. 

![Decorator1](https://github.com/Gominha123/Avenging_Shadows/assets/101401818/56ea82c6-1b25-48b5-8309-acc67628e94b)

Besides the classes described above there also is a class called WeaponController that contains all weapons and upgrade items, and it is in this class that the concrete decorator or the weapons class depending on the object in question. Whatever is created is stored in a variable of the interface type. This process is shown in the following diagram. 

![Decorator2](https://github.com/Gominha123/Avenging_Shadows/assets/101401818/b42d0310-db71-4b41-9720-463c3352f50f)

According to the decorator pattern to do the sum of the damage, the concrete decorator wraps around the weapons class, adding its value to the total weapons damage. Relative to the implementation of this process in code, it’s necessary to define that the object of type IWeapon of the concrete decorator is equal to the selected weapons class and that the object of type IWeapon of the WeaponController class  is going to be equal to that same decorator because it will have the total of the sum.

It is also relevant to say that the interface IWeapon imposes the obligation to use the function UpdateDamage(). This function in the concrete decorators class returns the value of the function UpdateDamage() of the object of type IWeapon which corresponds to the damage of the object in the weapons class, plus the damage value of the decorator. To see the full process in code you can access the following scripts: “WeaponController”, “WeaponDecorator”, “Weapon”, “WeaponManager” e “WeaponSwitch”, more specifically in the function “UpgradeWeapon()”, where process of the sum initializes.

## Modular AI Behavior Tree System

I wanted to build something scalable. I developed a custom Behavior Tree (BT) framework from scratch to handle complex guard behaviors like patrolling, detecting a player through a field of view, and chasing.

### 1 The Brain (Custom BT Engine)
I chose a **Composite Design Pattern** to build the tree. This means the AI doesn't just react it evaluates a hierarchy of needs.
* **The Selector:** Acts as the AI's "Priority List." It tries to Chase first; if it can't, it defaults to Patrolling.
* **The Sequence:** Acts as a To Do List. For example, the Chase sequence must first check the FOV, then set the speed, and only then move to the target.

### 2 Senses & Perception
Instead of cheating and letting the AI "know" where the player is, I engineered a **Spatial Perception System**:
* **FOV Cones:** Uses trigonometric math to calculate a 90° vision cone.
* **Raycasting:** Prevents the guard from seeing through walls (Obstacle Layer masking).
* **Optimization:** I used "sqrMagnitude" for distance checks to save CPU cycles.

### 3 Data Sharing (The Blackboard)
Nodes need to talk to each other. I implemented a **Blackboard** so that if the "Perception Node" finds a player, it stores that "Transform" in a central place where the "Chase Node" can instantly grab it.


## Project Structure
* **/AI/Core**: The reusable engine (Nodes, Selectors, Sequences).
* **/AI/Nodes**: The specific "Leaves" (Patrol, Chase, FOV Check).
* **/Controllers**: The character's specific logic.
* **/Scenes**: showcase the guard logic in a live environment.

## How to Run
1. Clone the repo: git clone https://github.com/sirin-gh/Modular_AI_Behavior_Tree_System.git
2. Open in **Unity 2022.3 LTS**.
3. Open the scenes located in Assets/Scenes/.



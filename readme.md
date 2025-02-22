This is a 2D game developed in Unity
* featuring a dynamic enemy AI, a state-driven 
* player movement system, interactive mechanics 
* combat mechanics,
* resource management,
* and item interactions 
* inventory system


### Important Notes
 
#### Unity Version
Unity 2022.3.51f1
* It will be updated to the latest version in the future.

   
## Enemy AI & Behavior

* Uses an advanced State Machine for AI behavior:
  * Chase: Follows the player when detected.
  * Attack: Engages the player in melee or ranged combat.
  * Idle: Stays in place or performs minimal movement.
  * Patrol: Moves within a defined area when the player is not in sight.

* Abilities:
  * speed boosts, AOE attacks, ranged projectiles
  * Enemies attack within a defined range, and a debug line is drawn using Debug.DrawLine().

## Health & Hunger System

* Implemented Health and Hunger mechanics:
  * UI health bar.
  * When hunger reaches zero, health starts decreasing.

Players can consume food to replenish hunger and consume health packs to regain health.


## Item System & Inventory

* Items can be picked up, used, and dropped.

## Unity Version

Developed in Unity 2022.3.51f1.

Will be updated to the latest LTS version in the future.

## Video
https://youtu.be/UNXQtCltBoI
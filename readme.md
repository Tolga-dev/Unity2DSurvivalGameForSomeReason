This is a 2D game developed in Unity
### Features
* featuring a dynamic enemy AI, a state-driven 
* player movement system, interactive mechanics 
* combat mechanics,
* resource management,
* and item interactions 
* inventory system

### Important Notes

## Enemy AI & Behavior

* Uses a basic State Machine for AI behavior:
  * Chase: Follows the player when detected. 
    * in specific range, the enemy will start attack the player.
    * in %50 percent change it can do speed boost
  * Attack: Engages the player in melee or ranged combat.
    * it can do AOE attack
    * it can do ranged attack
  * Idle: Stays in place or performs minimal movement.
    * chilling
  * Patrol: Moves within a defined area when the player is not in sight.
    * it find a random point in the area and go there by using navmesh agent
* Health Bar
  * UI health bar.
    * When health reaches zero, the enemy dies.

## Player Movement & Combat

* pick items from ground
  * pick up items by pressing the `E` key.
* drop items
  * drop items by moving the item out of slots. - q can be used maybe, i was tired
* Implemented Health and Hunger mechanics:
  * UI health bar.
  * When hunger reaches zero, health starts decreasing.
* Player uses Bow
  * shoot arrows by pressing the `Left Mouse Button`.
  * it needs arrows to shoot
* Player uses Sword
  * attack enemies by pressing the `Left Mouse Button`.
* consume food and health packs:
  * attack enemies by pressing the `Left Mouse Button`. 
  * food to reduces hunger and consume health packs to regain health.

## Inventory System

* Slots
  * Hot slots
    * 1 - 9 slots
    * can be switch with mouse wheel
  * Gear slots
    * 4 slots
    * only gear items can be placed
    * each gear gives protection in combat 
  * Inventory slots
    * 20 slots
    * can be used to store items
* Items can be stacked 
  * some of them cannot be - like gear items
* Items can be picked up, used, and dropped.
* Items can be equipped and unequipped.


#### Unity Version
Unity 2022.3.51f1
* It will be updated to the latest version in the future.

## Video
https://youtu.be/UNXQtCltBoI
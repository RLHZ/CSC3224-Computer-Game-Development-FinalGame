# Protect the Village!
##### CSC3224 Computer Game Development - Final Game 
https://rlhz.github.io/CSC3224-Computer-Game-Development-FinalGame/?fbclid=IwAR1Waq8nrQGZcuGqEXL15OfZzGWfncCtD3RchKxVJpskFG19K65oSLch9xQ

##### Gameplay:
The map has 6 buildings, and a series of enemies will try to destroy them.
The game is lost if ALL the buildings are destroyed or if the player dies. The game is won if the player and the buildings survive for 5 minutes the continuous 
waves of enemies.

The spawned enemies will target a building, but if the player is nearby, they'll target the player.
##### How to Play:
Movement: Left-Click on a point in the map and the character will move there

Attack: Right-Click on an enemy, and the player will automatically go towards it and engage in combat against it.

Camera Movement:
```sh
	"W": Detach camera from player and move it forwards
	"S": Detach camera from player and move it backwards
	"A": Detach camera from player and move it to the left
	"D": Detach camera from player and move it to the right
	"Q": Pivot the camera counter-clockwise
	"E": Pivot the camera clockwise 
	"F": Attach the camera to the player again so it will follow it.

```			

Heal:
```sh
    	"G": Use a potion (I've any available and health is not at its maximum level)
```	
Buy Items:
```sh
	"Tab": Opens the store.
	Select the quantity desired of each item and buy it if there are available funds.
```	
Menu:
```sh
	"ESC": Opens the menu and pauses the game.
	Press "Restart Game": Restarts the Game.
	Press "Quit Game": Ends the Game.
```

Shop items:
```sh
	- Health Potions: They provide the same effect as potions spawned sometimes after killing an enemy.
			  They add +30 health to the character's current health, or the amount left until
			  the max health if the health is above 70.

	- Armour: Each piece of armour bought reduces the damage received from the enemies by 1 point. There's a 
		  maximum of armour that can be bought.

	- Attack: Each piece of attack bought increases the damage dealt to enemies by 2 points. There's a 
		  maximum of attack that can be bought.

	- Allies: Buying an ally instantly spawns a friendly character that will target enemies and engage in
		  combat with them. Only two allies can be alive in the map at any time of the game.
```
		  
##### Developer tools:
When toggling the Dev mode, we get insights like the number of enemies in the scene and that will be spawned in the current wave, aswell as information about the performance.
	
The following numbers are in the Alphanumeric section of the keyboard, not the keypad.
```sh
    	"Left-Shift + 0": Toggle Dev mode - Required to do any of the following:
```
Immunity & Ignore:
```sh
	"Left-Shift + 1": Make building number 1 immune to enemy attacks.  
	"Left-Shift + 2": Make building number 2 immune to enemy attacks.
	"Left-Shift + 3": Make building number 3 immune to enemy attacks.
	"Left-Shift + 4": Make building number 4 immune to enemy attacks.
	"Left-Shift + 5": Make building number 5 immune to enemy attacks.
	"Left-Shift + 6": Make building number 6 immune to enemy attacks.

	"Left-Shift + C": Make player immune to enemy attacks.
	"Left-Shift + I": Enemies will ignore the player ("they won't attack the player").
```	
Destroy buildings & Kill Player (to test losing condition when all buildings are destroyed):
```sh
    	"Left-Ctrl + Left-Alt + 1": Destroy building number 1.  
    	"Left-Ctrl + Left-Alt + 2": Destroy building number 2.
    	"Left-Ctrl + Left-Alt + 3": Destroy building number 3.
    	"Left-Ctrl + Left-Alt + 4": Destroy building number 4.
    	"Left-Ctrl + Left-Alt + 5": Destroy building number 5.
    	"Left-Ctrl + Left-Alt + 6": Destroy building number 6.
    	
    	"Left-Ctrl + Left-Alt + X": Kill the player (to test losing condition when the player dies)
```	
```sh
Others:	
    	"Left-Ctrl + Left-Alt + C": Add 100 Coins.
    	"Left-Ctrl + Left-Alt + V": Add 10 Healing Potions.
    	"Left-Ctrl + Left-Alt + B": Recover 50 health points.
    	"Left-Ctrl + Left-Alt + N": Add 10 points of armour.
    	"Left-Ctrl + Left-Alt + M": Add 10 points of attack.
    	
    	"Left-Ctrl + Left-Alt + R": Double game speed (good to test winning condition of surviving 5 minutes.
    	"Left-Ctrl + Left-Alt + T": Divide game speed by 2
```	

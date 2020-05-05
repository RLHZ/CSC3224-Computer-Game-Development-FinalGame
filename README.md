# Protect the Village!
##### CSC3224 Computer Game Development - Prototype

##### Gameplay:
The map has 6 buildings, and a series of enemies will try to destroy them.
The game is lost if ALL the buildings are destroyed or if the player dies. The game is won if the player and the buildings survive for 5 minutes the continuous 
waves of enemies.

The spawned enemies will target a building, but if the player is nearby, they'll target the player.
##### How to Play:
Movement:
```sh
	"W": Forward
	"S": Backwards
	"A": Left
	"D": Right
```			
Rotation and look around:
```sh
    	"Move the mouse in the direction where you want the player to face."
```	
Attack:
```sh
    	"Left mouse click" to attack.
```	
Heal:
```sh
    	"Q": Use a potion (Only if there are potions stored and if the health is not already at its maximum level)
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

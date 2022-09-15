# QuantumZombieEscape
## 2D platform shooter

I made this game for my A-Level computer science coursework in 2020 and achieved an A*.

The player is a zombie who is trying to escape a laboratory in which they are being held captive and experimented on by quantum scientists. These scientists have been testing with beings existing as both alive and dead, as well as containers that have multiple different objects in simultaneously, resulting in the weapon crates the player can pick up.

The coursework specification indicated that students would have to implement specific complex algorithms in order to obtain higher marks. To achieve this, I wrote a script for an A* pathfinding algorithm used by the regular flying enemies, that uses a generated grid to map out a path to the player that is constantly updating. I also wrote a script for pixel-based collision detection, adapted from an example I found online. The third complex algorithm is a Finite-State-Machine (FSM) to control the boss. It allows the enemy to transition to different states and attack methods depending on the situation.

This repository also includes a file called 'Coursework Wray_Lewis.docx' that details the development process of my coursework. 

To run the game, go into the 'builds' folder. From here, you will find several builds of the game, the most recent of which will be the best. Download the folder and run the .exe file to play the game.

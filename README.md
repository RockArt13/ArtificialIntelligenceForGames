# ArtificialIntelligenceForGames
Chasing game with strong focus on AI solutions for games


# #AboutTheProject

Here is the key feature of this chasing game:
- The area has some surface surrounded by fences.
- There is a gaol on one side.
- There are two teams: purple and green.
- The players can only move on the game surface.
- The play time is limited.
- Purple players chase green players.
- A captured green player is taken to the gaol and remains there for the rest of the game.
- The purple team win if they capture all green players before the end of the game, otherwise the green team win.


The goal of the project

## 1. Moving
Spawn and set the players moving.

## 2. Chasing
Purple players chase green players and catch them if they reach them.


## 3. Escorting
Caught green players are escorted by the purple player to the gaol. When the green player has been deposited, the purple player starts chasing again.

## 4. Borders
Players cannot pass through the fence (without using Unity colliders).

## 5. Winning condition
Detect if either team has won.

## 6. State Machines
The two teams have fully implemented state machines.

## 7. Individual values
The players are initialised with individual values for maximum speed and turn speed.

## 8. Repulsion-Attraction Model
The players move on the playground according to the repulsion-attraction model, including obstacles and boundary fences.

# #AboutAssets

All assets are from Unity or downloaded from free assets.

# #FurtherContributionsAndCurrentBugs
- Player.Move(List<GameObject>, bool) should limit motion by maxSpeed and maxRotationSpeed (current stacking of vectors is improper, since it may lead to arbitrarily rapid movement).
- Player.isJailed does not just check if a player is in jail, but first moves the player. 
- Declare two float constants instead of Player.ForceType.
- Have a variable of float, instead of the boolean rep to indicate whether to repel or attract.
- Player.RandomMove() is only called once, by Player.Start(). Some adjustments are needed.
- Use larger values for maxSpeed and currentSpeed, instead of multiplying those with numNormalizer.
- GreenPlayer.captured is not needed.
- GreenIdleState immediately transfers to either GreenRunningState or GreenEscortedState. Some adjustments are needed.


# #RunTheGame
A build still needs to be created. Feel free to run the game from Unity.


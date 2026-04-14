# CS_580_Assignment2
Group Members: Cooper Myton, Simon Casey

Dot Product Implementation (Simon): There is now a goal arrow floating above JohnLemon's head, which uses Dot product to determine the angle to point at the goal from wherever JohnLemon is and update when he moves around. The implementation is in GoalArrowPoint.cs.

Linear Interpolation Implementation (Cooper): While approaching enemies, there will now be a light that turns from green when safe, to yellow when in a warning distance to red when caught through a Lerp. Lerp allows the color to smoothly transition rather than snap. See observer.cs for implementation.

Particle Effect Implementation (Simon): Reaching the end goal felt very sudden, so I added a new trigger box before the GameEnding that triggers some flashy "fireworks" particle emitters as you approach the game ending trigger. Implementation is found in ShowFireworks.cs as well as the new Fireworks particle emitter objects.

Sound Effect Implementation (Cooper): Added a thud sound effect when the character runs into a wall. To make the sound happen just run into a wall.

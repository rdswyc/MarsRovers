# Mars Rovers

NASA intends to land robotic rovers on Mars to explore a particularly curious-looking plateau. The rovers must navigate this rectangular plateau in a way so that their on board cameras can get a complete image of the surrounding terrain to send back to Earth.

A simple two-dimensional coordinate grid is mapped to the plateau to aid in rover navigation. Each point on the grid is represented by a pair of numbers X Y which correspond to the number of points East or North, respectively, from the origin. The origin of the grid is represented by 0 0 which corresponds to the southwest corner of the plateau. 0 1 is the point directly north of 0 0, 1 1 is the point immediately east of 0 1, etc. A rover’s current position and heading are represented by a triple X Y Z consisting of its current grid position X Y plus a letter Z corresponding to one of the four cardinal compass points, N E S W. For example, 0 0 N indicates that the rover is in the very southwest corner of the plateau, facing north.

NASA remotely controls rovers via instructions consisting of strings of letters. Possible instruction letters are L, R, and M. L and R instruct the rover to turn 90 degrees left or right, respectively (without moving from its current spot), while M instructs the rover to move forward one grid point along its current heading.

This application takes the test input (instructions from NASA) and provides the expected output (the feedback from the rovers to NASA). Each rover will move in series, i.e. the next rover will not start moving until the one preceding it finishes.

## Some design considerations

The following restrictions where applied to the design of the solution:

- The solution was implemented as a console app.
- The GRID is limited to 255 x 255 for simplicity and memory optimization.
- The GRID input is: X Y, for example: 5 5.
- The ROVER input is: X Y Z, for example: 1 2 N.
- Heading values are limited to N, E, S, W.
- Move values are limited to L, M, R.

## Test input

```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM 
```

## Expected output

```
1 3 N
5 1 E
```

## Technologies

- .NET 5
- .NET CLI
- xUnit

## How to start and test

First, build the application using the bellow command on the root folder of the solution. Next, start it or find the .exe file and execute it on your prefered command line tool.
```
dotnet build
dotnet run --project .\MarsRovers\MarsRovers.csproj
```

There is also a test project with 75 test cases available for you to run. You can check it by running the following command, allong with the code coverage report.
```
dotnet test --collect:"XPlat Code Coverage"
```

## App structure

The full solution is composed of two projects: the MarsRovers command line app and a Tests project.
For the main app, all the classes are in the root of the project, for simplicity. Here is the detailed contents:

* **Coordinate.cs**
The basic coordinate base class with X and Y pair.
This class is not abstract due to direct instantiation by other classes.
It is also not a struct to allow inheritance.

* **Enumerations.cs**
Basic enumeration with the four possible headings: North, East, South and West.

* **Grid.cs**
The exploration plateau for the app. The origin is a fixed coordinate, but the upper right boundary can be set.
It also includes a dedicated unit test class, `GridTest.cs`.

* **Program.cs**
The main entry point of the console app.
It will display a header and start asking for user input, and the handles the program logic.
There is an addRover flag, followed by a while loop to allow for multiple rovers to be added based on user input.
It will handle any out of range exception and output the messages to the console.
Finally, it should output the list of rover positions.

* **Rover.cs**
An representation of each rover. It inherits coordinates and adds a heading property.
It also has a move method based on the instructions available.
It also includes a dedicated unit test class, `RoverTest.cs`.

* **RoverNavigation.cs**
Main application logic separated from the Main Program flow.
It has a private Grid instance and a queue of the rovers.
The choice for a queue is to be able to enqueue rovers sequentially, and discard them later after output (FIFO approach).
It also includes a dedicated unit test class, `RoverNavigationTest.cs`.

* **RoverOutOfRangeException.cs**
Custom exception to map cases where the rover is set outside the grid bounds or moves towards it.
It includes the three constructors as a best practice:
The parameterless, one with an error message and one with a message and inner exeption.

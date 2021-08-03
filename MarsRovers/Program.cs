using System;

namespace MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new string('*', 48));
            Console.WriteLine(new string('*', 48));
            Console.WriteLine("******** Welcome to the Mars Rover app! ********");
            Console.WriteLine(new string('*', 48));
            Console.WriteLine(new string('*', 48));
            Console.WriteLine("\nHere are some ground rules to start of.");
            Console.WriteLine("* The GRID input is: X Y, for example: 5 5");
            Console.WriteLine("* The GRID is limited to 255 x 255 by design");
            Console.WriteLine("* The ROVER input is: X Y Z, for example: 1 2 N");
            Console.WriteLine("* Heading values are limited to N, E, S, W");
            Console.WriteLine("* Move values are limited to L, M, R");

            Console.WriteLine("\n\nCool, now please type the grid bounds.");

            var grid = InitializeGrid();
            var navigation = new RoverNavigation(grid);

            Console.WriteLine("\nOk, now you will set some rovers.");

            bool addRover = true;

            while (addRover)
            {
                var rover = InitializeRoverPosition();
                var instructions = InitializeRoverInstructions();

                try
                {
                    navigation.AddRover(rover, instructions);
                }
                catch (RoverOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                Console.Write("\nDo you wish to add another rover? Y or N: ");
                if (Console.ReadLine() == "N") addRover = false;
            }

            Console.WriteLine("\n\nHere is the Mars Rover output!");

            foreach (var roverPosition in navigation.RoverPositions())
            {
                Console.WriteLine(roverPosition);
            }
        }

        private static Grid InitializeGrid()
        {
            Console.Write("Type the grid bounds, and then press Enter: ");
            string input = Console.ReadLine();
            Grid grid;

            while (!Grid.TryParse(input, out grid))
            {
                Console.Write("Oops, this is not valid input. Please try again: ");
                input = Console.ReadLine();
            }

            return grid;
        }

        private static string InitializeRoverInstructions()
        {
            Console.Write("Type the rover instructions, and then press Enter: ");
            string input = Console.ReadLine();

            while (RoverNavigation.IsInvalidTurnMoveInstructions(input))
            {
                Console.Write("Oops, this is not valid input. Please try again: ");
                input = Console.ReadLine();
            }

            return input;
        }

        private static Rover InitializeRoverPosition()
        {
            Console.Write("Type the rover postion, and then press Enter: ");
            string input = Console.ReadLine();
            Rover rover;

            while (!Rover.TryParse(input, out rover))
            {
                Console.Write("Oops, this is not valid input. Please try again: ");
                input = Console.ReadLine();
            }

            return rover;
        }
    }
}

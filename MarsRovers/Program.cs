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

            Console.WriteLine("\n\nCool, now please type the grid bounds.");

            var grid = InitializeGrid();

            Console.WriteLine("\nOk, now you will set some rovers.");

            bool addRover = true;

            while (addRover)
            {
                var rover = InitializeRoverPosition();
                var instructions = InitializeRoverInstructions();

                Console.Write("\nDo you wish to add another rover? Y or N: ");
                if (Console.ReadLine() == "N") addRover = false;
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

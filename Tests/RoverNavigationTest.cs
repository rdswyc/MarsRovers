using MarsRovers;
using Xunit;

namespace Tests
{
    public class RoverNavigationTest
    {
        [Theory]
        [InlineData(10, 2, Heading.N)]
        [InlineData(1, 10, Heading.N)]
        [InlineData(10, 10, Heading.N)]
        public void AddRover_WithInvalidRover_ShouldThrowError(byte x, byte y, Heading z)
        {
            var grid = new Grid(5, 5);
            var navigation = new RoverNavigation(grid);
            var rover = new Rover(x, y, z);

            void action() => navigation.AddRover(rover, "");

            Assert.Throws<RoverOutOfRangeException>(action);
        }

        [Theory]
        [InlineData(1, 2, Heading.N, "LMM")]
        [InlineData(3, 3, Heading.E, "RMMMM")]
        [InlineData(1, 1, Heading.S, "LMMMMM")]
        [InlineData(1, 1, Heading.W, "RMMMMM")]
        public void AddRover_WithInvalidMoves_ShouldThrowError(byte x, byte y, Heading z, string instructions)
        {
            var grid = new Grid(5, 5);
            var navigation = new RoverNavigation(grid);
            var rover = new Rover(x, y, z);

            void action() => navigation.AddRover(rover, instructions);

            Assert.Throws<RoverOutOfRangeException>(action);
        }

        [Theory]
        [InlineData(1, 2, Heading.N, "LMLMLMLMM", "1 3 N")]
        [InlineData(3, 3, Heading.E, "MMRMMRMRRM", "5 1 E")]
        public void EndToEndTest_WithValidInput_ShouldMoveRover(byte x, byte y, Heading z, string instructions, string output)
        {
            var grid = new Grid(5, 5);
            var navigation = new RoverNavigation(grid);
            var rover = new Rover(x, y, z);

            navigation.AddRover(rover, instructions);
            var postitions = navigation.RoverPositions();

            Assert.Collection(postitions,
                e => Assert.Equal(output, e)
            );
        }

        [Fact]
        public void EndToEndTest_WithMultipleInput_ShouldMoveInOrder()
        {
            var grid = new Grid(5, 5);
            var navigation = new RoverNavigation(grid);
            var rover1 = new Rover(1, 2, Heading.N);
            var rover2 = new Rover(3, 3, Heading.E);

            navigation.AddRover(rover1, "LMLMLMLMM");
            navigation.AddRover(rover2, "MMRMMRMRRM");

            var postitions = navigation.RoverPositions();

            Assert.Collection(postitions,
                e => Assert.Equal("1 3 N", e),
                e => Assert.Equal("5 1 E", e)
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("LMT")]
        [InlineData("12")]
        public void IsInvalidTurnMoveInstructions_WithInvalidInput_ReturnTrue(string input)
        {
            var result = RoverNavigation.IsInvalidTurnMoveInstructions(input);

            Assert.True(result);
        }

        [Theory]
        [InlineData("LMR")]
        [InlineData("LLL")]
        [InlineData("RRRRRRM")]
        [InlineData("LMLMMLMRM")]
        public void IsInvalidTurnMoveInstructions_WithValidInput_ReturnFalse(string input)
        {
            var result = RoverNavigation.IsInvalidTurnMoveInstructions(input);

            Assert.False(result);
        }
    }
}

using MarsRovers;
using Xunit;

namespace Tests
{
    public class RoverTest
    {
        [Theory]
        [InlineData(Heading.N, Heading.W)]
        [InlineData(Heading.E, Heading.N)]
        [InlineData(Heading.S, Heading.E)]
        [InlineData(Heading.W, Heading.S)]
        public void Move_Left_ShouldUpdateHeading(Heading heading, Heading newHeading)
        {
            var rover = new Rover(1, 2, heading);

            rover.Move(Instruction.L);

            Assert.Equal(newHeading, rover.Z);
        }

        [Theory]
        [InlineData(Heading.N, Heading.E)]
        [InlineData(Heading.E, Heading.S)]
        [InlineData(Heading.S, Heading.W)]
        [InlineData(Heading.W, Heading.N)]
        public void Move_Right_ShouldUpdateHeading(Heading heading, Heading newHeading)
        {
            var rover = new Rover(1, 2, heading);

            rover.Move(Instruction.R);

            Assert.Equal(newHeading, rover.Z);
        }

        [Theory]
        [InlineData(Instruction.L)]
        [InlineData(Instruction.R)]
        public void Move_LeftOrRight_ShouldNotChangePosition(Instruction instruction)
        {
            var rover = new Rover(1, 2, Heading.N);

            rover.Move(instruction);

            Assert.Equal(1, rover.X);
            Assert.Equal(2, rover.Y);
        }

        [Theory]
        [InlineData(Heading.N, 1, 3)]
        [InlineData(Heading.E, 2, 2)]
        [InlineData(Heading.S, 1, 1)]
        [InlineData(Heading.W, 0, 2)]
        public void Move_Forward_ShouldUpdatePosition(Heading heading, byte newX, byte newY)
        {
            var rover = new Rover(1, 2, heading);

            rover.Move(Instruction.M);

            Assert.Equal(newX, rover.X);
            Assert.Equal(newY, rover.Y);
        }

        [Theory]
        [InlineData(Heading.N)]
        [InlineData(Heading.E)]
        [InlineData(Heading.S)]
        [InlineData(Heading.W)]
        public void Move_Forward_ShouldNotUpdateHeading(Heading heading)
        {
            var rover = new Rover(1, 2, heading);

            rover.Move(Instruction.M);

            Assert.Equal(heading, rover.Z);
        }

        [Theory]
        [InlineData("3 5 E")]
        [InlineData("0 0 N")]
        [InlineData("1 10 W")]
        [InlineData("200 100 S")]
        [InlineData("255 255 W")]
        public void TryParse_WithValidInput_ReturnTrue(string input)
        {
            var parse = Rover.TryParse(input, out Rover rover);

            Assert.True(parse);
        }

        [Theory]
        [InlineData("3 5 E", 3, 5, Heading.E)]
        [InlineData("0 0 N", 0, 0, Heading.N)]
        [InlineData("1 10 W", 1, 10, Heading.W)]
        [InlineData("200 100 S", 200, 100, Heading.S)]
        [InlineData("255 255 W", 255, 255, Heading.W)]
        public void TryParse_WithValidInput_ShouldSetRover(string input, byte x, byte y, Heading z)
        {
            Rover rover;

            Rover.TryParse(input, out rover);

            Assert.Equal(x, rover.X);
            Assert.Equal(y, rover.Y);
            Assert.Equal(z, rover.Z);
        }

        [Theory]
        [InlineData("0 o N")]
        [InlineData("1 -10 W")]
        [InlineData("200 100 L")]
        [InlineData("200N")]
        [InlineData("255 W")]
        public void TryParse_WithInvalidInput_ReturnFalse(string input)
        {
            var parse = Rover.TryParse(input, out Rover rover);

            Assert.False(parse);
        }

        [Theory]
        [InlineData("0 o N")]
        [InlineData("1 -10 W")]
        [InlineData("200 100 L")]
        [InlineData("200N")]
        [InlineData("255 W")]
        public void TryParse_WithInvalidInput_ShouldNotSetRover(string input)
        {
            Rover rover;

            Rover.TryParse(input, out rover);

            Assert.Null(rover);
        }
    }
}

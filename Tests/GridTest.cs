using MarsRovers;
using Xunit;

namespace Tests
{
    public class GridTest
    {
        [Theory]
        [InlineData("2 7")]
        [InlineData("0 0")]
        [InlineData("10 1")]
        [InlineData("100 200")]
        [InlineData("250 255")]
        public void TryParse_WithValidInput_ReturnTrue(string input)
        {
            var parse = Grid.TryParse(input, out Grid grid);

            Assert.True(parse);
        }

        [Theory]
        [InlineData("2 7", 2, 7)]
        [InlineData("0 0", 0, 0)]
        [InlineData("10 1", 10, 1)]
        [InlineData("100 200", 100, 200)]
        [InlineData("250 255", 250, 255)]
        public void TryParse_WithValidInput_ShouldSetGrid(string input, byte x, byte y)
        {
            Grid grid;

            Grid.TryParse(input, out grid);

            Assert.Equal(x, grid.Bound.X);
            Assert.Equal(y, grid.Bound.Y);
        }

        [Theory]
        [InlineData("a 2")]
        [InlineData("7 b")]
        [InlineData("1 -1")]
        [InlineData("250 400")]
        [InlineData("255")]
        public void TryParse_WithInvalidInput_ReturnFalse(string input)
        {
            var parse = Grid.TryParse(input, out Grid grid);

            Assert.False(parse);
        }

        [Theory]
        [InlineData("a 2")]
        [InlineData("7 b")]
        [InlineData("1 -1")]
        [InlineData("250 400")]
        [InlineData("255")]
        public void TryParse_WithInvalidInput_ShouldNotSetGrid(string input)
        {
            Grid grid;

            Grid.TryParse(input, out grid);

            Assert.Null(grid);
        }
    }
}

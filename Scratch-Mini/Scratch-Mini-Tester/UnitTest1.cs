using Newtonsoft.Json.Bson;
using Scratch_Mini;
using ScratchMini;

namespace Scratch_Mini_Tester

{
    public class MoveCommandTests
    {
        [Fact]
        public void Move_WithinBounds_UpdatesPosition() //gwn checken of ie wel beweegt als ie binnen de bounds is
        {
            var field = new Field();
            var moveCommand = new MoveCommand(1);
            field = moveCommand.executeCommand(field);
            Assert.IsType<Player>(field.Grid[0, 1]);
        }

        [Fact]
        public void Move_OutOfBounds_DoesNotMove() //checken of ie niet beweegt als ie buiten de bounds zou gaan
        {
            var field = new Field();
            var player = (Player)field.Grid[0, 0];
            player.CardinalDirection = CardinalDirection.West;
            var moveCommand = new MoveCommand(1);
            field = moveCommand.executeCommand(field);
            Assert.IsType<Player>(field.Grid[0, 0]);
        }

        [Fact]
        public void Move_IntoOccupiedSpace_DoesNotMove() //checken of ie niet beweegt als er een obstakel is
        {
            var field = new Field();
            field.Grid[0, 1] = new Wall();
            var moveCommand = new MoveCommand(1);
            field = moveCommand.executeCommand(field);
            Assert.IsType<Player>(field.Grid[0, 0]);
        }
    }

    public class TurnCommandTests
    {
        [Theory]
        [InlineData(CardinalDirection.North, 'R', CardinalDirection.East)]
        [InlineData(CardinalDirection.East, 'R', CardinalDirection.South)]
        [InlineData(CardinalDirection.South, 'R', CardinalDirection.West)]
        [InlineData(CardinalDirection.West, 'R', CardinalDirection.North)]
        [InlineData(CardinalDirection.North, 'L', CardinalDirection.West)]
        [InlineData(CardinalDirection.West, 'L', CardinalDirection.South)]
        [InlineData(CardinalDirection.South, 'L', CardinalDirection.East)]
        [InlineData(CardinalDirection.East, 'L', CardinalDirection.North)]

        public void Turn_CheckDirection(CardinalDirection startDirection, char turn, CardinalDirection expectedDirection)
        {
            var field = new Field();
            var player = (Player)field.Grid[0, 0];
            player.CardinalDirection = startDirection;

            var turnCommand = new TurnCommand(turn);
            field = turnCommand.executeCommand(field);

            Assert.Equal(expectedDirection, player.CardinalDirection);
        }
    }

    public class ProgramTests
    {
        [Fact]
        public void Program_ExecutesSingleCommand()
        {
            var grid = new IGridObject[3, 3];
            grid[0, 0] = new Player(CardinalDirection.East);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i, j] == null)
                    {
                        grid[i, j] = new EmptySpace();
                    }
                }
            }
            var field = new Field(grid);

            var commands = new List<ICommand> { new MoveCommand(1) };
            var program = new Program(commands, new Field(grid));
            var result = program.Execute(out _);
            Assert.Equal("Move 1, ", result);
        }

        [Fact]
        public void Program_ExecutesMultipleCommands()
        {
            var grid = new IGridObject[3, 3];
            grid[0, 0] = new Player(CardinalDirection.East);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i, j] == null)
                    {
                        grid[i, j] = new EmptySpace();
                    }
                }
            }
            var field = new Field(grid);
            var commands = new List<ICommand> { new MoveCommand(1), new TurnCommand('R') };
            var program = new Program(commands, new Field(grid));
            var result = program.Execute(out _);
            Assert.Equal("Move 1, Turn R, ", result);
        }

        [Fact]

        public void testNumberOfCommands()
        {
            List<ICommand> commands;
        }
    }

    public class ShapeExerciseTests
    {
        [Fact]
        public void ShapeExercise_returnsTrue_ifShapeIsCorret()
        {
            bool[,] expectedShape = new bool[,]
            {
                { true, true, false },
                { false, true, true },
                { false, false, true }
            };


        }

        public class PathFindingExerciseTests
        {

        }
    }

    public class WinFormTests
    {

    }
}
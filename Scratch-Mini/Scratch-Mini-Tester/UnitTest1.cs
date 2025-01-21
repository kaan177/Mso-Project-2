using System.Reflection;
using Newtonsoft.Json.Bson;
using Scratch_Mini;
using ScratchMini;

namespace Scratch_Mini_Tester

{
    public class ProgramTests
    {

        [Fact]
        public void testNumberOfCommands()
        {

            var field = new Field();
            List<ICommand> testCommands = new List<ICommand>()
            {
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R')
            };

            var testProgram = new Program(testCommands, field);

            int totalCommands = testProgram.NumberOfCommands;

            Assert.Equal(6, totalCommands);
        }

        [Fact]

        public void testMaximumNesting()
        {
            var field = new Field();

            List<ICommand> helperList = new List<ICommand>()
            {
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R')
            };
            List<ICommand> repeatTheHelperList = new List<ICommand>()
            {
                new RepeatCommand(2, helperList)
            };

            List<ICommand> testCommands = new List<ICommand>()
            {
                new RepeatCommand(2, helperList), new RepeatCommand(1, repeatTheHelperList)
            };

            var testProgram = new Program(testCommands, field);
            int totalNesting = testProgram.MaximumNesting;

            Assert.Equal(3, totalNesting);

        }

        [Fact]
        public void testNumberOfRepeatCommands()
        {
            var field = new Field();

            List<ICommand> helperList = new List<ICommand>()
            {
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R')
            };
            List<ICommand> repeatTheHelperList = new List<ICommand>()
            {
                new RepeatCommand(2, helperList)
            };

            List<ICommand> testCommands = new List<ICommand>()
            {
                new RepeatCommand(2, helperList), new RepeatCommand(1, repeatTheHelperList)
            };

            var testProgram = new Program(testCommands, field);
            int totalRepeatCommands = testProgram.NumberOfRepeatCommands;

            Assert.Equal(3, totalRepeatCommands);
        }

        [Fact]

        public void testToStringMethod()
        {
            var command = new MoveCommand(1);
            var command2 = new TurnCommand('L');


            string finalString = command2.ToString();

            Assert.Equal("Turn left,", finalString);

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
    }

    public class WinFormTests
    {

    }

    public class PlayerTests
    {
        [Fact]
        public void Player_CardinalDirection_PositionInFront_ShouldReturnCorrectCoordinates()
        {

            Player player = new Player(CardinalDirection.North);
            var position = player.PositionInFront(2, 2);
            Assert.Equal((2, 1), position);

            player.CardinalDirection = CardinalDirection.East;
            position = player.PositionInFront(2, 2);
            Assert.Equal((3, 2), position);

            player.CardinalDirection = CardinalDirection.South;
            position = player.PositionInFront(2, 2);
            Assert.Equal((2, 3), position);

            player.CardinalDirection = CardinalDirection.West;
            position = player.PositionInFront(2, 2);
            Assert.Equal((1, 2), position);
        }
    }

    public class FieldTests
    {
        [Fact]
        public void Field_SetPlayerPosition_ShouldPlacePlayerCorrectly()
        {
            IGridObject[,] grid = new IGridObject[3, 3];
            Field field = new Field(grid);
            Player player = new Player(CardinalDirection.East);

            grid[0, 0] = player;
            grid[1, 1] = new EmptySpace();
            field.SetPlayerPosition(1, 1);

            var position = field.GetPlayerPosition();

            Assert.Equal((1, 1), position);
            Assert.True(grid[1, 1] is Player);
            Assert.True(grid[0, 0] is EmptySpace);
        }

        [Fact]
        public void Field_GetPlayer_ShouldReturnCorrectPlayer()
        {
            IGridObject[,] grid = new IGridObject[3, 3];
            Player player = new Player(CardinalDirection.South);
            grid[2, 2] = player;

            Field field = new Field(grid);

            var returnedPlayer = field.GetPlayer();

            Assert.Equal(player, returnedPlayer);
            Assert.Equal(CardinalDirection.South, returnedPlayer.CardinalDirection);
        }

        [Fact]
        public void Field_IsInsideBounds_ShouldValidateCorrectly()
        {
            
            IGridObject[,] grid = new IGridObject[3, 3];
            Field field = new Field(grid);

            //checking boundaries
            Assert.True(field.IsInsideBounds(0, 0));
            Assert.True(field.IsInsideBounds(2, 2));
            Assert.False(field.IsInsideBounds(-1, 0));
            Assert.False(field.IsInsideBounds(0, -1));
            Assert.False(field.IsInsideBounds(-1, 1));
            Assert.False(field.IsInsideBounds(3, 3));
            Assert.False(field.IsInsideBounds(2, 3));
            Assert.False(field.IsInsideBounds(3, 2));
        }
    }

    public class MoveCommandTests
    {
        [Fact]
        public void MoveCommand_ShouldMovePlayerIfEmptySpace()
        {
            IGridObject[,] grid = new IGridObject[3, 3];
            Player player = new Player(CardinalDirection.South);
            grid[1, 1] = player;
            grid[1, 2] = new EmptySpace();

            Field field = new Field(grid);
            MoveCommand moveCommand = new MoveCommand(1);

            Field updatedField = moveCommand.executeCommand(field);

            Assert.Equal((1, 2), updatedField.GetPlayerPosition());
        }

        [Fact]
        public void MoveCommand_ShouldStopAtWall()
        { 
            IGridObject[,] grid = new IGridObject[3, 3];
            Player player = new Player(CardinalDirection.East);
            grid[1, 1] = player;
            grid[1, 2] = new Wall();

            Field field = new Field(grid);
            MoveCommand moveCommand = new MoveCommand(1);

            Field updatedField = moveCommand.executeCommand(field);

            Assert.Equal((1, 1), updatedField.GetPlayerPosition());
        }
    }

    public class TurnCommandTests
    {
        [Fact]
        public void TurnCommand_ShouldChangePlayerDirectionCorrectly()
        {
            
            Player player = new Player(CardinalDirection.North);
            IGridObject[,] grid = new IGridObject[3, 3];
            grid[1, 1] = player;

            Field field = new Field(grid);
            TurnCommand turnCommandRight = new TurnCommand('R');
            TurnCommand turnCommandLeft = new TurnCommand('L');

            
            turnCommandRight.executeCommand(field);
            Assert.Equal(CardinalDirection.East, player.CardinalDirection);

            turnCommandRight.executeCommand(field);
            Assert.Equal(CardinalDirection.South, player.CardinalDirection);

            turnCommandLeft.executeCommand(field);
            Assert.Equal(CardinalDirection.East, player.CardinalDirection);
        }
    }

    public class ConditionalCommandTests
    {
        [Fact]
        public void ConditionalCommand_ShouldExecuteCommandsIfConditionIsTrue()
        {
            IGridObject[,] grid = new IGridObject[3, 3];
            Player player = new Player(CardinalDirection.East);
            grid[0, 0] = player;
            grid[0, 2] = new Wall();

            Field field = new Field(grid);

            IConditional condition = new WallAheadConditionel();
            ICommand moveCommand = new MoveCommand(1);
            ConditionalCommand conditionalCommand = new ConditionalCommand(new List<ICommand> { moveCommand }, condition);

            Field updatedField = conditionalCommand.executeCommand(field);

            Assert.Equal((0, 1), updatedField.GetPlayerPosition());
        }
    }


}


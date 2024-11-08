using Newtonsoft.Json.Bson;
using Scratch_Mini;
using ScratchMini;

namespace Scratch_Mini_Tester

{
    public class MoveCommandTests
    {
    }

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

            Assert.Equal("Turn L", finalString);



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
using Scratch_Mini;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

//Scratch mini is a program where a user can learn programming.
//This is done in a game format where there is a player that moves around in a grid.
//This player is controlled by commands provided by the user.
//To start learning more you should start reading from program.

namespace ScratchMini {
    class MainProgram
    {
        IScratchMini iCommandLine;
        public void Main()
        {
            iCommandLine = new IScratchMini();
        }
    }
    
    //This is the scratch mini interface.
    //This class provides three diffrent programs, 1 shape excercise and 1 Path exersice.
    //It also provides a function to load programs in
    public class IScratchMini
    {
        public Program basic;
        public Program advanced;
        public Program expert;
        public Program loaded;
        public ShapeExercise exerciseShape1;
        public PathFindingExercise exercisePath1;


        ProgramImporter importer;

        public IScratchMini()
        {
            importer = new ProgramImporter();

            bool[,] shapeGrid = new bool[,]
            {
                {false, true, true, true, false },
                {false, true, false, false, false },
                {false, true, true, true, false },
                {false, false, false, true, false },
                {false, true, true, true , false }
            };

            IGridObject[,] exerciseShapeGrid1 = new IGridObject[5, 5];
            exerciseShapeGrid1 [0, 4] = new Player(CardinalDirection.East);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (exerciseShapeGrid1[i, j] == null)
                    {
                        exerciseShapeGrid1[i, j] = new EmptySpace();
                    }
                }
            }

            exerciseShape1 = new ShapeExercise(shapeGrid, new Program (new List<ICommand> { }, new Field(exerciseShapeGrid1)));

            IGridObject[,] exercisePathGrid1 = new IGridObject[5, 5];
            exercisePathGrid1[3, 4] = new Player(CardinalDirection.North);
            exercisePathGrid1[3, 1] = new TargetSpace();
            List <(int, int)> coordinates = new List<(int, int)>
            {
                (1, 1), (1, 2), (2, 2), (3, 2), (4, 2), (0, 4)
            };
            foreach(var coord in coordinates)
            {
                int x = coord.Item1;
                int y = coord.Item2;

                if (exercisePathGrid1[x, y] == null)
                {
                    exercisePathGrid1[x, y] = new Wall(); //creating the walls
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (exercisePathGrid1[i, j] == null)
                    {
                        exercisePathGrid1[i, j] = new EmptySpace(); //creating the emptyspaces
                    }
                }
            }

            exercisePath1 = new PathFindingExercise((3, 1), new Program(new List<ICommand> { }, new Field(exercisePathGrid1)));
            
            IGridObject[,] basicGrid = new IGridObject[3, 3];
            basicGrid[0, 1] = new Player(CardinalDirection.West);
            basicGrid[1, 2] = new Wall();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (basicGrid[i, j] == null)
                    {
                        basicGrid[i, j] = new EmptySpace();
                    }
                }
            }


            ICommand[] basicCommands =
                { new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R') };
            
            basic = new Program(basicCommands.ToList(), new Field(basicGrid));

            IGridObject[,] advancedGrid = new IGridObject[4, 4];
            advancedGrid[3,0] = new Player(CardinalDirection.South);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (advancedGrid[i, j] == null)
                    {
                        advancedGrid[i, j] = new EmptySpace();
                    }
                }
            }

            ICommand[] advacedCommands = 
                { new RepeatCommand(4, new List<ICommand> {new MoveCommand(1), new TurnCommand('R')})};

            advanced = new Program(advacedCommands.ToList(), new Field(advancedGrid));

            IGridObject[,] expertGrid = new IGridObject[5, 5];
            expertGrid[3, 4] = new Player(CardinalDirection.North);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (expertGrid[i, j] == null)
                    {
                        expertGrid[i , j] = new EmptySpace(); 
                    }
                }

            }

            ICommand[] expertCommands = 
                { new RepeatCommand(2, new List<ICommand> {new MoveCommand(1), new TurnCommand('R')}),
                new RepeatCommand(2, new List<ICommand> {new MoveCommand(1), new TurnCommand('L')})};

            expert = new Program(expertCommands.ToList(), new Field(expertGrid));

            //Interact();
        }

        public void LoadProgram(string input)
        {
            Program importedProgram = importer.Import(input);
            loaded = importedProgram;
            
        }

        public List<ICommand> LoadCommands(string input)
        {
            return importer.StringToCommands(input);
        }


        public string GetMetricsProgram(Program program)
        {
            return $"The number of commands is {program.NumberOfCommands}, the maximum nesting is {program.MaximumNesting}, the amount of repeat commands is {program.NumberOfRepeatCommands}";
        }

    }
    //A program is a combination of a field and a list of commands.
    //Programs provide a execute method and metrics like number of commands.
    public class Program
    {
        public string name;
        public List <ICommand> Commands;
        public Field field;
        Field startingField;

        public int NumberOfCommands
        {
            get
            {
                int numberOfCommands = 0;
                foreach (ICommand command in Commands)
                {
                    numberOfCommands += command.NumberOfCommands;
                };
                return numberOfCommands;
            }
        }

        public int MaximumNesting
        {
            get
            {
                int nestingAmount = 0;
                foreach (ICommand command in Commands)
                {
                    nestingAmount = Math.Max(nestingAmount, command.MaximumNesting);
                }
                return nestingAmount;
            }
        }

        public int NumberOfRepeatCommands
        {
            get
            {
                int repeatAmount = 0;
                foreach (ICommand command in Commands)
                {
                    repeatAmount += command.NumberOfRepeatCommands;
                }
                return repeatAmount;
            }
        }

        public Program(List<ICommand> commands, Field field)
        {
            Commands = commands;
            this.field = field;
            startingField = field;
        }
        //Execute returns three variables.
        //The log of the commands, the touched spaces for the excersise and the final field.
        //It loops over all the commands executing them one by on
        public string Execute(out bool[,] touchedSpaces, out Field outputField)
        {
            Field currentField = field;
            touchedSpaces = new bool[currentField.Grid.GetLength(0), currentField.Grid.GetLength(1)];
            for (int i = 0; i < touchedSpaces.GetLength(0); i++) 
            {
                for (int j = 0; j < touchedSpaces.GetLength(1); j++)
                {
                    touchedSpaces[i,j] = false;
                }
            }
            touchedSpaces[currentField.GetPlayerPosition().Item1, currentField.GetPlayerPosition().Item2] = true;
            string commandLog = "";


            foreach (ICommand command in Commands)
            {
                currentField = command.executeCommand(currentField);
                touchedSpaces[currentField.GetPlayerPosition().Item1, currentField.GetPlayerPosition().Item2] = true;
            }


            outputField = currentField;
            field = startingField;
            return commandLog;
        }
        
    }


    #region Playing Field
    //A field represents the grid that the player move in.
    //It has a two dimensional array of gridobjects.
    //And provides helper functions like get player position and set player position.
    public class Field 
    {
        public IGridObject[,] Grid;
        /// <summary>
        /// For a Initial Empty field
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public Field()
        {
            Grid = new IGridObject[,]
                { {new Player(CardinalDirection.East), new EmptySpace(), new EmptySpace() },
                {new EmptySpace(), new EmptySpace(), new EmptySpace() },
                {new EmptySpace(), new EmptySpace(), new EmptySpace() }};
        }
        
        public Field(IGridObject[,] grid)
        {
            Grid = grid;
        }

        public (int, int) GetPlayerPosition()
        {

            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    
                    if (Grid[x, y] != null && Grid[x, y].Name == "Player")
                    {
                        return (x, y); 
                    }
                }
            }
            throw new Exception("Player not found in grid");
        }
        public void SetPlayerPosition(int playerX, int playerY)
        {
            (int oldx, int oldy) = GetPlayerPosition();
            Player player = GetPlayer();
            Grid[oldx, oldy] = new EmptySpace();
            if (Grid[playerX, playerY] is EmptySpace)
            {
                Grid[playerX, playerY] = player;
            }
            else
            {
                throw new Exception("Can't place player on a wall");
            }
        }

        public Player GetPlayer()
        {
            (int, int) playerPostion = GetPlayerPosition();
            return (Player)Grid[playerPostion.Item1, playerPostion.Item2];
        }
        public bool IsInsideBounds(int x, int y)
        {
            return x >= 0 && x < Grid.GetLength(0) && y >= 0 && y < Grid.GetLength(1);
        }
    }

    public abstract class IGridObject
    {
        public abstract string Name {  get; }
    }

    public class Player : IGridObject
    {
        //The cardinal direction represents where the player is facing in the grid.
        public CardinalDirection CardinalDirection;
        public override string Name { get { return "Player"; } }
        public Player(CardinalDirection direction)
        {
            CardinalDirection = direction;
        }
        public (int newX, int newY) PositionInFront(int x, int y)
        {
            switch (CardinalDirection)
            {
                case CardinalDirection.North:
                    return (x , y - 1);
                case CardinalDirection.East:
                    return (x + 1, y);
                case CardinalDirection.South:
                    return (x, y + 1);
                case CardinalDirection.West:
                    return (x - 1, y);
                default:
                    return (x, y);
            }
        }
    }
    
    public class EmptySpace : IGridObject
    {
        public override string Name { get { return "Empty Space"; } }
    }

    public class TargetSpace : IGridObject
    {
        public override string Name { get { return "Target"; } }
    }

    public class Wall : IGridObject
    {
        public override string Name { get { return "Wall"; } }
    }
    public enum CardinalDirection
    {
        North,
        East, 
        South, 
        West
    }
    #endregion


    #region Commands
    //Command are what the user provides to move the player.
    //The most simple commands are move and turn.
    //And then there are logic commands like repeat a number of times or a while loop with conditionals.
    //All the commands provide metrics like number of commands or level of nesting.
    public abstract class ICommand
    {
        public abstract int NumberOfCommands {  get; }
        public abstract int MaximumNesting { get; }
        public abstract int NumberOfRepeatCommands { get; }
        public abstract string Name { get; }
        public abstract Field executeCommand(Field field);
        public abstract string ToString();
    }

    public class RepeatCommand : ICommand {
        int times;
        List<ICommand> commands;
        public override string Name { get { return "Repeat command"; } }

        public override int NumberOfCommands
        {
            get
            {
                int numberOfCommands = 0;
                foreach (ICommand command in commands)
                {
                    numberOfCommands += command.NumberOfCommands;
                };
                return numberOfCommands + 1;
            }
        }

        public override int MaximumNesting { get 
            {   
                int nestingAmount = 0;
                foreach (ICommand command in commands)
                {
                    nestingAmount = Math.Max(nestingAmount, command.MaximumNesting);
                }
                return nestingAmount + 1;
            } }

        public override int NumberOfRepeatCommands
        {
            get
            {
                int repeatAmount = 0;
                foreach (ICommand command in commands)
                {
                    repeatAmount += command.NumberOfRepeatCommands;
                }
                return repeatAmount + 1;
            }
        }

        public RepeatCommand(int times , List<ICommand> commands)
        {
            this.times = times;
            this.commands = commands;
        }
        public override Field executeCommand(Field field)
        {
            for (int i = 0; i < times; i++)
            {
                foreach (ICommand command in commands)
                {
                    field = command.executeCommand(field);
                }
            }

            return field;
        }

        public override string ToString()
        {
            string childrenStrings = "";
            foreach (ICommand command in commands)
            {
                childrenStrings += command.ToString();
            }

            return "Repeat " + times.ToString() + "{," + childrenStrings + "},";
        }
    }

    public class TurnCommand : ICommand {
        char turn; // 'L' to turn left, 'R' to turn right. 
        public override string Name { get { return "Turn command"; } }

        public override int NumberOfCommands { get { return 1; } }

        public override int MaximumNesting { get { return 1; } }

        public override int NumberOfRepeatCommands { get { return 0; } }
        public TurnCommand(char turn)
        {
            this.turn = turn;
        }
        public override Field executeCommand(Field field)
        {
            Player player = field.GetPlayer();
            player.CardinalDirection = NewDirection(player.CardinalDirection, turn);
            return field; 
        }

        private CardinalDirection NewDirection(CardinalDirection direction, Char turnDirection)
        {
            int next;
            if (turnDirection == 'R')
            {
                next = ((int)direction + 1) % 4;
            }
            else if (turnDirection == 'L')
            {
                if ((int)direction == 0)
                {
                    next = ((int)direction + 3);
                }
                else
                {
                    next = ((int)direction - 1) % 4;
                }
                
            }
            else { throw new Exception("The direction was not left or right"); }

            //throw new Exception($"directoin: {(CardinalDirection)next}");
            return (CardinalDirection)next;

        }

        public override string ToString()
        {
            if (turn == 'L')
            { return "Turn " + "left,"; }
            else if (turn == 'R'){
                return "Turn " + "right,";
            }
            else { throw new Exception("The direction was not left or right"); }
        }
    }

    public class MoveCommand : ICommand {

        int steps;
        public override string Name { get { return "Move command"; } }

        public override int NumberOfCommands { get { return 1; } }

        public override int MaximumNesting { get { return 1; } }

        public override int NumberOfRepeatCommands { get { return 0; } }
        public MoveCommand(int steps)
        {
            this.steps = steps;
        }
        public override Field executeCommand(Field field)
        {
            (int playerX, int playerY) = field.GetPlayerPosition();


            Player player = field.GetPlayer();

            for (int i = 0; i < steps; i++)
            {
                (int newX, int newY) = player.PositionInFront(playerX, playerY);

                if (field.IsInsideBounds(newX, newY)) //check if the new position is inside of the boundaries
                {
                    if (field.Grid[newX, newY] is EmptySpace) //check if the new position is an emptyspace
                    {
                        field.Grid[newX, newY] = player; //place player on the new position
                        field.Grid[playerX, playerY] = new EmptySpace(); //turn the old playerposition into an empty space
                        playerX = newX;
                        playerY = newY;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return field;
        }
        

        public override string ToString()
        {
            return "Move " + steps.ToString() + ",";
        }
    }
    public class ConditionalCommand : ICommand
    {
        List<ICommand> commands;
        IConditional condition;

        
        public override string Name { get { return "Conditionel command"; } }
        public override int NumberOfCommands 
        { 
            get 
            {
                int numberOfCommands = 0;
                foreach(ICommand command in commands)
                {
                    numberOfCommands += command.NumberOfCommands;
                }; 
                return numberOfCommands + 1;
            } 
        }

        public override int MaximumNesting
        {
            get
            {
                int nestingAmount = 0;
                foreach (ICommand command in commands)
                {
                    nestingAmount = Math.Max(nestingAmount, command.MaximumNesting);
                }
                return nestingAmount + 1;
            }
        }

        public override int NumberOfRepeatCommands
        {
            get
            {
                int repeatAmount = 0;
                foreach (ICommand command in commands)
                {
                    repeatAmount += command.NumberOfRepeatCommands;
                }
                return repeatAmount;
            }
        }
        public ConditionalCommand(List<ICommand> commands, IConditional condition)
        {
            this.commands = commands;
            this.condition = condition;
        }
        public override Field executeCommand(Field field)
        {
            while (condition.CheckIfTrue(field))
            {
                foreach (ICommand command in commands) { field = command.executeCommand(field); }
            }
            return field;
        }

        public override string ToString()
        {
            string childrenStrings = "";
            foreach (ICommand command in commands)
            {
                childrenStrings += command.ToString();
            }

            return "Condition " + condition.ToString() + "{," + childrenStrings + "},";
        }
    }

    //Conditionals are boolean statements that apply to a field.
    public abstract class IConditional
    {
        public abstract bool CheckIfTrue(Field field);

        public abstract string ToString();
    }

    public class WallAheadConditionel : IConditional
    {
        public WallAheadConditionel() { }
        public override bool CheckIfTrue(Field field)
        {
            (int xPos, int yPos) = field.GetPlayerPosition();
            Player player = field.GetPlayer();
            (int newX, int newY) = player.PositionInFront(xPos, yPos);
            if(field.IsInsideBounds(newX, newY)) { return field.Grid[newX, newY] is Wall; }
            else { return false; }
        }
        
        public override string ToString()
        {
            return "Wall";
        }
    }
    public class GridAheadConditionel : IConditional
    {
        public GridAheadConditionel() { }

        public override bool CheckIfTrue(Field field)
        {
            (int xPos, int yPos) = field.GetPlayerPosition();
            Player player = field.GetPlayer();
            (int newX, int newY) = player.PositionInFront(xPos, yPos);
            return !field.IsInsideBounds(newX, newY);
        }

        public override string ToString()
        {
            return "Grid";
        }
    }
    #endregion


}


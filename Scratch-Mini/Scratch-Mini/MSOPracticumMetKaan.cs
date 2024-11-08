using Scratch_Mini;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

//TO DO: make move command stop when wall

namespace ScratchMini {
    class MainProgram
    {
        ICommandLine iCommandLine;
        public void Main()
        {
            iCommandLine = new ICommandLine();
        }
    }
    

    public class ICommandLine
    {
        public Program basic;
        public Program advanced;
        public Program expert;
        public Program loaded;
        public ShapeExercise exerciseShape1;
        public PathFindingExercise exercisePath1;


        ProgramImporter importer;

        public ICommandLine()
        {
            importer = new ProgramImporter();

            bool[,] exercise1Grid = new bool[,]
            {
                {false, true, true, true, false },
                {false, true, false, false, false },
                {false, true, true, true, false },
                {false, false, false, true, false },
                {false, true, true, true , false }
            };

           
            


            IGridObject[,] basicGrid = new IGridObject[3, 3];
            basicGrid[0, 1] = new Player(CardinalDirection.East);
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
            loaded = importer.Import(input);
        }


        public string GetMetricsProgram(Program program)
        {
            return $"The number of commands is {program.NumberOfCommands}, the maximum nesting is {program.MaximumNesting}, the amount of repeat commands is {program.NumberOfRepeatCommands}";
        }

    }
    public class Program
    {
        public string name;
        public List <ICommand> Commands;
        public Field field;

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
        }

        public string Execute(out bool[,] touchedSpaces)
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
                commandLog += command.ToString() + ", ";
                touchedSpaces[currentField.GetPlayerPosition().Item1, currentField.GetPlayerPosition().Item2] = true;
            }
            field = currentField;
            return commandLog;
        }
        
    }
    #region Playing Field

    public class Player : IGridObject
    {
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
                    return (x - 1, y);
                case CardinalDirection.East:
                    return (x, y + 1);
                case CardinalDirection.South:
                    return (x + 1, y);
                case CardinalDirection.West:
                    return (x, y - 1);
                default:
                    return (x, y);
            }
        }
    }
    
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

        public (int,int) GetPlayerPosition()
        {
            int playerX = -1;
            int playerY = -1;
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    if (Grid[x, y].Name == "Player")
                    {
                        playerX = x; //current x coordinate of the player object
                        playerY = y; //current y coordinate of the player object
                        break;
                    }
                }
                if (playerX != -1 && playerY != -1)
                {
                    throw new Exception("Player not found in grid"); // to stop the initial loop
                }
            }
            return (playerX, playerY);
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
    public class EmptySpace : IGridObject
    {
        public override string Name { get { return "Empty Space"; } }
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

            return "Repeat " + times.ToString() + "(" + childrenStrings + ")";
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
                next = ((int)direction - 1) % 4;
            }
            else { throw new Exception("The direction was not left or right"); }

            return (CardinalDirection)next;

        }

        public override string ToString()
        {
            return "Turn " + turn;
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
            return "Move " + steps.ToString();
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

            return "Repeat while " + condition.ToString() + "(" + childrenStrings + ")";
        }
    }

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
            return "is wall ahead";
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
            return "is grid edge ahead";
        }
    }
    #endregion


}


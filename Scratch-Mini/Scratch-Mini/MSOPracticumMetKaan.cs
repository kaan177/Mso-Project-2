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
        Program basicP;
        Program advanced;
        Program extreme;
        Program loaded;

        ProgramImporter importer;

        public ICommandLine()
        {
            importer = new ProgramImporter();
            ICommand[] basicCommands = 
                { new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R'),
                new MoveCommand(1), new TurnCommand('R') };
            basicP = new Program(basicCommands.ToList());

            ICommand[] advacedCommands = 
                { new RepeatCommand(4, new List<ICommand> {new MoveCommand(1), new TurnCommand('R')})};
            advanced = new Program(advacedCommands.ToList());

            ICommand[] extremeCommands = 
                { new RepeatCommand(2, new List<ICommand> {new MoveCommand(1), new TurnCommand('R')}),
                new RepeatCommand(2, new List<ICommand> {new MoveCommand(1), new TurnCommand('L')})};
            extreme = new Program(extremeCommands.ToList());
            Interact();
        }

        public void Interact()
        {
            Console.WriteLine("Do you want to play a random program (R) or do you want to import a program (I)");
            string answer = Console.ReadLine();

            if (answer.ToLower() == "r")
            {
                if (loaded != null)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 5);
                    switch (randomNumber)
                    {
                        case 1:
                            ExecuteProgram(basicP);
                            break;
                        case 2:
                            ExecuteProgram(advanced);
                            break;
                        case 3:
                            ExecuteProgram(extreme);
                            break;
                        case 4:
                            ExecuteProgram(loaded);
                            break;
                    }
                }
                else
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 4);
                    switch (randomNumber)
                    {
                        case 1:
                            ExecuteProgram(basicP);
                            break;
                        case 2:
                            ExecuteProgram(advanced);
                            break;
                        case 3:
                            ExecuteProgram(extreme);
                            break;
                    }
                }
                
            }
            else if (answer.ToLower() == "i")
            {
                answer = Console.ReadLine();
                loaded = importer.Import(answer);
                if (loaded == null)
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                Console.WriteLine(answer + " is not a accepted answer.");
                
            }
            Interact();
        }
        public void LoadProgram(string filePath)
        {
            throw new NotImplementedException();
        }
        public void ExecuteProgram(Program program)
        {
            Console.WriteLine(program.Execute()); //hier moet nog de keuzen tussen metrics en program output
        }

    }
    public class Program
    {
        public string name;
        List <ICommand> Commands;
        Field startingField;

        public Program(List<ICommand> commands, Field field)
        {
            Commands = commands;
            startingField = field;
        }

        public string Execute()
        {
            Field currentField = startingField;
            string commandLog = "";
            foreach (ICommand command in Commands)
            {
                currentField = command.executeCommand(currentField);
                commandLog += command.ToString() + ", ";
            }
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
        public abstract string Name { get; }
        public abstract Field executeCommand(Field field);
        public abstract string ToString();
    }

    public class RepeatCommand : ICommand {
        int times;
        List<ICommand> commands;
        public override string Name { get { return "Repeat command"; } }

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

        public MoveCommand(int steps)
        {
            this.steps = steps;
        }

        public override string Name { get { return "Move command"; } }

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

        public ConditionalCommand(List<ICommand> commands, IConditional condition)
        {
            this.commands = commands;
            this.condition = condition;
        }
        public override string Name { get { return "Conditionel command"; } }

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


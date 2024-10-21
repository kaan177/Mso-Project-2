using System;
using System.Diagnostics;

namespace ScratchMini {
    class MainProgram
    {
        ICommandLine iCommandLine;
        public void Main()
        {
            iCommandLine = new ICommandLine();
        }
    }
    public class Program
    {
        public string name;
        List <ICommand> Commands;
        Field currentField;

        public Program(List<ICommand> commands)
        {
            Commands = commands;

        }

        public string Execute()
        {
            throw new NotImplementedException();
        }
        
    }

    public class ICommandLine
    {
        Program basicP;
        Program advanced;
        Program extreme;
        Program loaded;

        public ICommandLine()
        {
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
            if (answer.ToLower() == "i")
            {
                loaded = Import(answer);
                if (loaded == null)
                {
                    Interact();
                }
            }
            else
            {
                Console.WriteLine(answer + " is not a accepted answer.");
                Interact();
            }
        }

        public Program Import(string input)
        {
            string[] inputSplit = input.Split(",");
            List<ICommand> commands = new List<ICommand>();
            foreach (string commandString in inputSplit)
            {
                string[] commandStringSplit = commandString.Split(" ");
                switch (commandStringSplit[0])
                {
                    case "Move":
                        try
                        {
                            commands.Add(new MoveCommand(int.Parse(commandStringSplit[1])));
                        }
                        catch { Console.WriteLine(commandString + "is not in a valid format "); return null; }
                        break;
                    case "Turn":
                        if(commandStringSplit[0] == "right") { commands.Add(new TurnCommand('R')); }
                        else if (commandStringSplit[0] == "left") { commands.Add(new TurnCommand('L')); }
                        else { Console.WriteLine(commandString + "is not in a valid format "); return null; }
                        break;
                        //Hier moet nog de repeat command. Maar die is helaas nog niet af.

                }
                

            }
            return new Program(commands);
        }
        public void ExecuteProgram(Program program)
        {
            Console.WriteLine(program.Execute()); //hier moet nog de keuzen tussen metrics en program output
        }

    }

    public class Player : IGridObject
    {
        public string Image ;
        public CardinalDirection CardinalDirection;
        public override string Name { get { return "Player"; } }
        public Player(CardinalDirection direction)
        {
            Image = "defaultImage";
            CardinalDirection = direction;
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
            throw new NotImplementedException();
        }
        
        public Field(IGridObject[,] grid)
        {
            Grid = grid;
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

    public enum CardinalDirection
    {
        North,
        East, 
        South, 
        West
    }



    public abstract class ICommand
    {
        public abstract string Name { get; }
        public abstract Field executeCommand(Field field);
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
            throw new NotImplementedException();
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
            foreach (IGridObject o in field.Grid)
            {
                if (o.Name == "Player")
                {
                    Player player = o as Player;
                    player.CardinalDirection = newDirection(player.CardinalDirection, turn);
                }
            }
            
            return field; 
        }

        private CardinalDirection newDirection(CardinalDirection direction, Char turnDirection)
        {
            int next;
            if (turnDirection == 'R')
            {
                next = (int)direction + 1;
                if (next > (int)CardinalDirection.West)
                {
                    next = (int)CardinalDirection.North;
                }
            }
            else
            {
                next = (int)direction - 1;
                if (next < (int)CardinalDirection.North)
                {
                    next = (int)CardinalDirection.West;
                }
            }

            return (CardinalDirection)next;

        }

    }

    class MoveCommand : ICommand {
        
        int steps;

        public MoveCommand(int steps)
        {
            this.steps = steps;
        }

        public override string Name { get { return "Move command"; } }
        public MoveCommand(int steps)
        {
            this.steps = steps;
        }

        public override Field executeCommand(Field field)
        {
            //not done yet
            /*
            int playerX = -1;
            int playerY = -1; 
            for (int x = 0; x < field.Grid.GetLength(0); x++)
            {
                for (int y = 0; y < field.Grid.GetLength(1); y++)
                {
                    if (field.Grid[x, y].Name == "Player")
                    {
                        playerX = x;
                        playerY = y;
                        break;
                    }
                }
                if (playerX != -1 && playerY != -1)
                {
                    break; // to stop the initial loop
                }

            }
            Player player = (Player)field.Grid[playerX, playerY];

            for (int i = 0; i < steps; i++)
            {

            }
            */



            throw new NotImplementedException();
        }

        private (int newX, int newY) CalculateNewPosition(int x, int y, CardinalDirection direction)
        {
            throw new NotImplementedException();
        }


        private bool isInsideBounds(int x, int y, IGridObject[,] grid)
        {
            return x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1);
        }
    }
}


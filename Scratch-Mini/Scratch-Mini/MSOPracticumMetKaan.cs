using System;
using System.Diagnostics;

namespace ScratchMini {
    class MainProgram
    {
        public void Main()
        {
            
        }
    }
    class Program
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

    class ICommandLine
    {
        Program basicP;
        Program advanced;
        Program extreme;
        Program loaded;
        public Program Import()
        {
            throw new NotImplementedException();
        }
        public void ExecuteProgram(Program program)
        {
            throw new NotImplementedException();
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

        public TurnCommand(char turnDirection)
        {
            turn = turnDirection;
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


using System;

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
        IGridObject[,] Grid;
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
        Nort,
        South, 
        East, 
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
        char turn;
        public override string Name { get { return "Turn command"; } }

        public override Field executeCommand(Field field)
        {
            throw new NotImplementedException();
        }
    }

    class MoveCommand : ICommand {
        int steps;

        public override string Name { get { return "Move command"; } }

        public override Field executeCommand(Field field)
        {
            throw new NotImplementedException();
        }
    }
}


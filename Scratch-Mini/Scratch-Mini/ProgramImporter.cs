using ScratchMini;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scratch_Mini
{
    public class ProgramImporter
    {
        //class that focusses on importing the command sequences and running the program
        public ProgramImporter() { }
        public ScratchMini.Program Import(string input)
        {
            CheckIfFileValid(input);
            string[] inputSplit = input.Split("COMMANDS");

            IGridObject[,] grid = GridImporter(inputSplit[0]);

            List<ICommand> commands = StringToCommands(inputSplit[1]);

            return new ScratchMini.Program(commands, new Field(grid));
        }
        private IGridObject[,] GridImporter(string input)
        {
            /*
             * O = empty space (capital o)
             * + = Wall
             */
            input = input.Replace("\r", "");
            string[] lines = input.Split('\n');
            IGridObject[,] grid = new IGridObject[lines.Length, lines.Length];
            //string loopOutput = "";
            for (int x = 0; x < lines.Length; x++)
            {

                for (int y = 0; y < lines[x].Length; y++)
                {
                    //loopOutput = loopOutput + "(" + x.ToString() + "," + y.ToString() + ")";

                    char symbol = lines[x][y];  

                    switch (symbol)
                    {
                        case 'O':
                            grid[x, y] = new EmptySpace();
                            
                            break;
                        case '+':
                            grid[x, y] = new Wall();
                            break;
                        case 'X':
                            grid[x, y] = new Player(CardinalDirection.North);               
                            break;
                        case '\r':
                            throw new Exception("There is a r");
                        default: 
                            throw new Exception($"There is a null space : {symbol + 1}");
                    }
                    
                }
                
            }
            //throw new Exception($"loopoutput ; {loopOutput}"); //check of de loop goed gaat
            //grid[0, 0] = new Player(CardinalDirection.East); 
            return grid;
        }
        
        public List<ICommand> StringToCommands(string input)
        {
            
            string[] commandInput = input.Split(",");
            Stack<string> commandStack = new Stack<string>(commandInput.Reverse());
            return ImportCommands(ref commandStack);
        }
        private List<ICommand> ImportCommands(ref Stack<string> strings)
        {
            
            List<ICommand> commands = new List<ICommand>();
            while (strings.Count > 0) 
            {
                string commandString = strings.Pop();
                commandString = commandString.Replace("\r", "").Trim();
                string[] commandStringSplit = commandString.Split(" ");
                switch (commandStringSplit[0])
                {
                    case "}":
                        return commands;

                    case "Move":
                        
                        try
                        {
                            commands.Add(new MoveCommand(int.Parse(commandStringSplit[1])));
                        }
                        catch { throw new Exception($"No valid number after the Move command: {commandStringSplit[1]}"); }
                        break;
                    case "Turn":
                        if (commandStringSplit[1] == "right") { commands.Add(new TurnCommand('R')); }
                        else if (commandStringSplit[1] == "left") { commands.Add(new TurnCommand('L')); }
                        else { throw new Exception("No valid direction after the Turn command."); }
                        break;
                    case "Repeat":
                        int times;
                        try
                        {
                            times = int.Parse(commandStringSplit[1]);
                        }
                        catch { throw new Exception("No valid number after repeat command."); }
                        commands.Add(new RepeatCommand(times, ImportCommands(ref strings)));
                        break;
                    case "Condition":
                        if (commandStringSplit[1] == "Wall") { commands.Add(new ConditionalCommand(ImportCommands(ref strings), new WallAheadConditionel())); }
                        if (commandStringSplit[1] == "Grid") { commands.Add(new ConditionalCommand(ImportCommands(ref strings), new GridAheadConditionel())); }
                        else { throw new Exception($"{commandStringSplit[1]} is not a recognized Condition"); }
                        break;
                }
            }
            return commands;
        }

        private void CheckIfFileValid(string input)
        {
            if (string.IsNullOrEmpty(input)) { throw new Exception("Import string is null or empty"); }
            if (input.Split("COMMANDS").Length != 2) { throw new Exception("Import string is not correctly split by COMMAND"); }

            string gridString = input.Split("COMMANDS")[0].TrimEnd();
            gridString = gridString.Replace("\r", "");
            string[] lines = gridString.Split('\n');

            int lineCount = lines.Length;

            foreach (string line in lines)
            {
                if (line.Length != lineCount)
                {
                    throw new Exception($"Import grid does not have the right dimensions LineCount: {lineCount}, lineLength: {line.Length}, line: {line}");
                }
            }
        }
    }
}

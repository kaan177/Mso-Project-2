using ScratchMini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch_Mini
{
    public abstract class IExercise
    {
        public abstract bool CheckIfProgramSuceeded(ScratchMini.Program program);
    }

    public class ShapeExercise : IExercise
    {
        public override bool CheckIfProgramSuceeded(ScratchMini.Program program)
        {
            throw new NotImplementedException();
        }
    }

    public class PathFindingExercise : IExercise
    {
        Field field;
        (int, int) StartingPosition;
        (int, int) EndingPosition;
        CardinalDirection StartingDirection;

        public PathFindingExercise(Field field, (int, int) startingPosition, (int, int) endingPosition, CardinalDirection startingDirection)
        {
            this.field = field;
            StartingPosition = startingPosition;
            EndingPosition = endingPosition;
            StartingDirection = startingDirection;
        }

        public override bool CheckIfProgramSuceeded(ScratchMini.Program program)
        {
            field.SetPlayerPosition(StartingPosition.Item1, StartingPosition.Item2);
            field.GetPlayer().CardinalDirection = StartingDirection;
            program.field = field;
            program.Execute();
            return program.field.GetPlayerPosition() == EndingPosition;
            
        }
    }
}

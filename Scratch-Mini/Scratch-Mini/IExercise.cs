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
        bool[,] shape;
        (int, int) StartingPosition;

        public ShapeExercise(bool[,] shape)
        {
            this.shape = shape;
        }

        public override bool CheckIfProgramSuceeded(ScratchMini.Program program)
        {
            program.field.SetPlayerPosition(StartingPosition.Item1, StartingPosition.Item2);
            bool[,] attemptedShape;
            program.Execute(out attemptedShape);
            bool returnValue = true;
            for (int i = 0;  i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j])
                    {
                        returnValue = returnValue && attemptedShape[i,j];
                    }
                }
            }return returnValue;
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
            program.Execute(out _);
            return program.field.GetPlayerPosition() == EndingPosition;
            
        }
    }
}

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
        public ScratchMini.Program exerciseProgram;
        public abstract bool CheckIfProgramSuceeded(ScratchMini.Program program);
    }

    public class ShapeExercise : IExercise
    {
        bool[,] shape;
        

        public ShapeExercise(bool[,] shape, ScratchMini.Program exerciseProgram)
        {
            this.shape = shape;
            this.exerciseProgram = exerciseProgram;
        }

        public override bool CheckIfProgramSuceeded(ScratchMini.Program program)
        {
          
            bool[,] attemptedShape;
            program.Execute(out attemptedShape ,out _);
            bool returnValue = true;
            for (int i = 0;  i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    
                    returnValue = returnValue && !(attemptedShape[i,j] ^ shape[i,j]);
                    
                }
            }return returnValue;
        }
    }

    public class PathFindingExercise : IExercise
    {
        
        (int, int) EndingPosition;
      

        public PathFindingExercise((int, int) endingPosition, ScratchMini.Program exerciseProgram)
        {
           
            
            EndingPosition = endingPosition;
            
            this.exerciseProgram = exerciseProgram;
        }

        public override bool CheckIfProgramSuceeded(ScratchMini.Program program)
        {
            
            program.Execute(out _, out _);
            return program.field.GetPlayerPosition() == EndingPosition;
            
        }
    }
}

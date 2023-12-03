using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class MovementAnalyzer
    {

        public bool IsAllowedMovement(Board board, Pawn pawn, int PositionX , int PositionY)
        {
            return true;
        }
        
        public bool IsRightField(Board board, int postionX, int positionY)
        {
            if (board.boardArray[positionY, postionX] == board.BlacKField)
            {
                return true;
            }
            return false;          
        }
        
        public bool IsFreeFromPawn(Board board, int positionX, int positionY)
        {
            if (board.boardArray[positionY, positionX] != (PlayerPawn.PLAYER_PAWN_SYMBOL | CpuPawn.CPU_PAWN_SYMBOL)) 
            {
                return true;
            }
            return false;
        }
    }
}

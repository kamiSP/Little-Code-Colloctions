using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typing_Game
{
    class main
    {
        public static void Main(String[] args)
        {
            if (!DxControl.DX_Init()) 
            {
                return;
            }
            if (!Game.GameInit())
            {
                return;
            }

            do
            {
                if (Game.DrawBackground())
                {

                }
                else
                {
                    Game.MainCircle();
                }
                Game.DrawScore();


                if (DxControl.Escape_KeyDown())
                {
                    break;
                }

                DxControl.DX_Flip();
                DxControl.DX_Wait(1);
            } while (DxControl.DX_ExitProcess());

            DxControl.DX_End();
        }
    }
}

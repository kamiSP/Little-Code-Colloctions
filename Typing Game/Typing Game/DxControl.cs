using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using System.Runtime.InteropServices;

namespace Typing_Game
{
    class DxControl
    {
        public static bool DX_Init()
        {
            DX.ChangeWindowMode(DX.TRUE);
            DX.SetWindowStyleMode(5);
            DX.SetWindowSize(500, 500);

            DX.SetGraphMode(500, 500, 32);
            DX.SetOutApplicationLogValidFlag(DX.FALSE);
            DX.SetWindowText("打字遊戲");
            DX.SetDisplayRefreshRate(60);

            if (DX.DxLib_Init() == -1)
            {
                return false;
            }

            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            return true;
        }

        public static void DX_End()
        {
            DX.DxLib_End();
        }

        public static void DX_Flip()
        {
            DX.ScreenFlip();
            DX.ClearDrawScreen();
        }

        public static void DX_Wait(int timer)
        {
            DX.WaitTimer(timer);
        }

        public static void DX_DrawString(int x, int y, String s)
        {
            DX.DrawString(x, y, s, DX.GetColor(255, 255, 255));
        }

        public static bool Escape_KeyDown()
        {
            if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 1)
            {
                return true;
            }
            return false;
        }

        public static bool DX_ExitProcess()
        {
            if (DX.ProcessMessage() == 0)
            {
                return true;
            }
            return false;
        }

        public static void DX_DrawGraph(int x, int y, int handle)
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            DX.DrawGraph(x, y, handle, DX.TRUE);
        }

        public static void DX_DrawGraph(int x, int y, int handle, int alpha)
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, alpha);
            DX.DrawGraph(x, y, handle, DX.TRUE);
        }

        public static int DX_LoadGraph(string filename)
        {
            return DX.LoadGraph(@"image\" + filename);
        }
        
        public static bool DX_CheckKeyIn(char c)
        {
            byte[] buffer = new byte[256];
            Type myType = typeof(DX);
            System.Reflection.FieldInfo myField = myType.GetField("KEY_INPUT_" + c);

            //if (DX.CheckHitKey((int)myField.GetValue(null)) == 1)
            //{
            //    return true;
            //}
            DX.GetHitKeyStateAll(out buffer[0]);
            if ((buffer[(int)myField.GetValue(null)]) == 1) 
            {
                return true;
            }
            return false;
        }

        public static void DX_DrawStringToHandle(int x, int y, string s,int font)
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            DX.DrawStringToHandle(x, y, s, DX.GetColor(0, 0, 0), font);
        }

        public static int DX_GetFont(string fontname,int size,int bold)
        {
            return DX.CreateFontToHandle(fontname, size, bold);
        }
    }
}

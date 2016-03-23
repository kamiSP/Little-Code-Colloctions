using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typing_Game
{
    class Image
    {
        public int handle;
        public int alpha;
        public Image(int gHandle) { handle = gHandle; }
        public Image(int gHandle,int gAlpha) { handle = gHandle; alpha = gAlpha; }
        public int getAlpha(int speed)
        {
            if (alpha <= 255 && 0 <= alpha) 
            {
                alpha += speed;
            }
            return alpha;
        }
    }

    class Letter
    {
        public enum STATUS { POPIN,POPOUT }
        public Image image;
        public int x, y;
        public char ch;
        private int alpha;
        public STATUS status;

        public Letter(Image image,char c)
        {
            this.image = image;
            alpha = image.alpha;
            ch = c;
            status = STATUS.POPIN;
            x = 125;y = 10;
        }

        public int getAlpha(int speed)
        {
            if (alpha <= 255 && 0 <= alpha)
            {
                alpha += speed;
            }
            return alpha;
        }

        public bool IsShowOff()
        {
            if (y < -150)
            {
                return true;
            }
            return false;
        }
    }

    class Game
    {
        private static Random rand;
        public static List<Letter> letterList = new List<Letter>();
        public static Letter current;
        private static int score = 0;
        private static int scoreFont;
        private static bool Locked = false;

        static class Resourse
        {
            public static List<Image> Alphabat = new List<Image>();
            public static Image background;
            //public static Image bar;
        }

        static class GameStatus
        {
            public static bool GameStart = false;
            
        }

        public static bool GameInit()
        {
            if (LoadImage() && LoadAlphabatImage())
            {
                rand = new Random();
                scoreFont = DxControl.DX_GetFont("Comic Sans MS", 48, 3);
                return true;
            }

            Console.Write(scoreFont);
            return false;
        }

        private static bool LoadImage()
        {
            Resourse.background = new Image(DxControl.DX_LoadGraph("background.png"), 0);
            if (Resourse.background.handle == -1)
            {
                return false;
            }
            return true;
            //Resourse.bar = new Image(DxControl.DX_LoadGraph("bar.png"));
        }

        private static bool LoadAlphabatImage()
        {
            for(char c = 'A'; c<= 'Z'; c++)
            {
                Resourse.Alphabat.Add(new Image(DxControl.DX_LoadGraph(c + ".png"), 0));
                if (Resourse.Alphabat[c - 'A'].handle == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool DrawBackground()
        {
            if (Resourse.background.alpha >= 255)
            {
                DxControl.DX_DrawGraph(0, 0, Resourse.background.handle);
                return false;
            }
            else
            {
                DxControl.DX_DrawGraph(0, 0, Resourse.background.handle, Resourse.background.getAlpha(3));
            }
            return true;
        }

        private static void createLetter()
        {
            int index = rand.Next(0, 25);
            current = new Letter(Resourse.Alphabat[index], (char)('A' + index));
            //current = new Letter(Resourse.Alphabat[1], (char)('A' + 1));
            letterList.Add(current);
        }

        public static void DrawScore()
        {
            DxControl.DX_DrawStringToHandle(450, 450, score.ToString(), scoreFont);
        }

        public static void MainCircle()
        {
            if (letterList.Count <= 0)
            {
                createLetter();
            }

            for(int i = 0; i < letterList.Count; i++)
            {
                Letter letter = letterList[i];
                if (letter.IsShowOff())
                {
                    continue;
                }
                if (letter.status == Letter.STATUS.POPIN)
                    DxControl.DX_DrawGraph(letter.x, letter.y, letter.image.handle, letter.getAlpha(5));
                else if(letter.status == Letter.STATUS.POPOUT)
                {
                    DxControl.DX_DrawGraph(letter.x, letter.y, letter.image.handle, letter.getAlpha(-20));
                    letter.y -= 10;
                }
            }

            if (DxControl.DX_CheckKeyIn(current.ch))
            {
                if (Locked)
                {

                }
                else
                {
                    score++;
                    current.status = Letter.STATUS.POPOUT;
                    createLetter();
                    Locked = true;
                }
            }
            else
            {
                Locked = false;
            }
        }
    }
}

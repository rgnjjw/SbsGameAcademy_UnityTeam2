using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud_game
{
    public static class StringExtentions
    {
        public static void MyPrint(this string str, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }

        public static void MyPrint(this string[] lines, int x, int y)
        {
            for (int i = 0; y < lines.Length; y++)
            {
                lines[y].MyPrint(x, y);
            }
        }
    }
}

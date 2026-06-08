// ═══════════════════════════════════════════════════════════════
//   FILE: UI.cs
//   Console UI helper — colors, typing effect, boxes, header
// ═══════════════════════════════════════════════════════════════

using System;
using System.Threading;

namespace HogwartsGame
{
    static class UI
    {
        // Colored text line
        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // Typing animation effect
        public static void TypeWrite(string text, ConsoleColor color, int delay = 16)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        // Horizontal line
        public static void DrawLine(char ch, ConsoleColor color, int length = 62)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(new string(ch, length));
            Console.ResetColor();
        }

        // Box with title
        public static void DrawBox(string title, ConsoleColor borderColor)
        {
            int w = 60;
            string top = "╔" + new string('═', w) + "╗";
            int pad = (w + title.Length) / 2;
            string middle = "║" + title.PadLeft(pad).PadRight(w) + "║";
            string bottom = "╚" + new string('═', w) + "╝";
            ColorWrite(top, borderColor);
            ColorWrite(middle, borderColor);
            ColorWrite(bottom, borderColor);
        }

        // Pause / sleep
        public static void Pause(int ms = 900) { Thread.Sleep(ms); }

        // Big HOGWARTS ASCII header
        public static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("   ██╗  ██╗ ██████╗  ██████╗ ██╗    ██╗ █████╗ ██████╗ ████████╗███████╗");
            Console.WriteLine("   ██║  ██║██╔═══██╗██╔════╝ ██║    ██║██╔══██╗██╔══██╗╚══██╔══╝██╔════╝");
            Console.WriteLine("   ███████║██║   ██║██║  ███╗██║ █╗ ██║███████║██████╔╝   ██║   ███████╗");
            Console.WriteLine("   ██╔══██║██║   ██║██║   ██║██║███╗██║██╔══██║██╔══██╗   ██║   ╚════██║");
            Console.WriteLine("   ██║  ██║╚██████╔╝╚██████╔╝╚███╔███╔╝██║  ██║██║  ██║   ██║   ███████║");
            Console.WriteLine("   ╚═╝  ╚═╝ ╚═════╝  ╚═════╝  ╚══╝╚══╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝");
            Console.ResetColor();
            DrawLine('═', ConsoleColor.DarkYellow);
        }

        // Clear screen + header
        public static void ClearWithHeader()
        {
            Console.Clear();
            PrintHeader();
        }
    }
}

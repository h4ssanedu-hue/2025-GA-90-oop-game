// ═══════════════════════════════════════════════════════════════
//   FILE: BattleAnimator.cs
//   Handles all battle visuals:
//   - Arena display with both characters side by side
//   - Live spell animation flying between characters
//   - HP bars, mana bar, hit flash
// ═══════════════════════════════════════════════════════════════

using System;
using System.Threading;

namespace HogwartsGame
{
    static class BattleAnimator
    {
        // Layout constants
        const int LEFT_CHAR_WIDTH = 18;
        const int ARENA_GAP = 14;   // Space between characters where spell travels

        // ══════════════════════════════════════════════
        // Draw the static arena (no spell flying)
        // Called before player chooses a spell
        // ══════════════════════════════════════════════
        public static void DrawArena(
            string playerName, string[] playerArt, int playerHP, int playerMana,
            string enemyName, string[] enemyArt, int enemyHP,
            ConsoleColor playerColor, ConsoleColor enemyColor,
            int round)
        {
            Console.Clear();

            // Title bar
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  " + new string('═', 70));
            Console.WriteLine("       *** HOGWARTS DUEL  —  ROUND " + round + " ***");
            Console.WriteLine("  " + new string('═', 70));
            Console.ResetColor();
            Console.WriteLine();

            // HP bars for both fighters
            DrawHPBar(playerName, playerHP, 100, playerColor);
            DrawHPBar(enemyName, enemyHP, 100, enemyColor);
            Console.WriteLine();

            // Mana bar for player
            Console.ForegroundColor = ConsoleColor.Cyan;
            int manaFilled = playerMana / 5;
            Console.WriteLine("  MANA  [" + new string(':', manaFilled)
                              + new string(' ', 20 - manaFilled) + "] " + playerMana + "/100");
            Console.ResetColor();
            Console.WriteLine();

            UI.DrawLine('-', ConsoleColor.DarkGray, 70);

            // Both characters side by side
            int artLines = Math.Max(playerArt.Length, enemyArt.Length);
            for (int i = 0; i < artLines; i++)
            {
                string pl = i < playerArt.Length ? playerArt[i] : "                  ";
                string en = i < enemyArt.Length ? enemyArt[i] : "                  ";

                Console.ForegroundColor = playerColor;
                Console.Write(pl.PadRight(LEFT_CHAR_WIDTH));
                Console.ResetColor();

                Console.Write(new string(' ', ARENA_GAP));   // gap between them

                Console.ForegroundColor = enemyColor;
                Console.Write(en);
                Console.ResetColor();
                Console.WriteLine();
            }

            // Names under the characters
            Console.ForegroundColor = playerColor;
            Console.Write(("  " + playerName).PadRight(LEFT_CHAR_WIDTH));
            Console.ResetColor();
            Console.Write(new string(' ', ARENA_GAP));
            Console.ForegroundColor = enemyColor;
            Console.Write("  " + enemyName);
            Console.ResetColor();
            Console.WriteLine();

            UI.DrawLine('-', ConsoleColor.DarkGray, 70);
        }

        // ══════════════════════════════════════════════
        // Animate spell flying between the two characters
        // playerCasting = true  → spell goes LEFT to RIGHT (player attacks)
        // playerCasting = false → spell goes RIGHT to LEFT (enemy attacks)
        // ══════════════════════════════════════════════
        public static void AnimateBattleSpell(
            string playerName, string[] playerArt, int playerHP,
            string enemyName, string[] enemyArt, int enemyHP,
            ConsoleColor playerColor, ConsoleColor enemyColor,
            string incantation, string symbol, ConsoleColor spellColor,
            bool playerCasting, int round)
        {
            int TRAVEL_STEPS = ARENA_GAP - symbol.Length;
            if (TRAVEL_STEPS < 1) TRAVEL_STEPS = 1;

            for (int step = 0; step <= TRAVEL_STEPS; step++)
            {
                Console.Clear();

                // Title
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("  " + new string('═', 70));
                Console.WriteLine("       *** HOGWARTS DUEL  —  ROUND " + round + " ***");
                Console.WriteLine("  " + new string('═', 70));
                Console.ResetColor();
                Console.WriteLine();

                // HP bars
                DrawHPBar(playerName, playerHP, 100, playerColor);
                DrawHPBar(enemyName, enemyHP, 100, enemyColor);
                Console.WriteLine();
                Console.WriteLine();
                UI.DrawLine('-', ConsoleColor.DarkGray, 70);

                // Characters with spell flying between them
                int artLines = Math.Max(playerArt.Length, enemyArt.Length);
                int spellRow = artLines / 2;  // spell appears at mid-height

                for (int i = 0; i < artLines; i++)
                {
                    string pl = i < playerArt.Length ? playerArt[i] : "                  ";
                    string en = i < enemyArt.Length ? enemyArt[i] : "                  ";

                    Console.ForegroundColor = playerColor;
                    Console.Write(pl.PadRight(LEFT_CHAR_WIDTH));
                    Console.ResetColor();

                    if (i == spellRow)
                    {
                        // Build the gap string with spell at correct position
                        string gapRow;

                        if (playerCasting)
                        {
                            // LEFT → RIGHT: step 0 is near player, last step is near enemy
                            string before = new string(' ', step);
                            string after = new string(' ', Math.Max(0, ARENA_GAP - step - symbol.Length));
                            gapRow = (before + symbol + after).PadRight(ARENA_GAP);
                            if (gapRow.Length > ARENA_GAP) gapRow = gapRow.Substring(0, ARENA_GAP);
                        }
                        else
                        {
                            // RIGHT → LEFT: reversed symbol, starts far right
                            string revSym = ReverseSymbol(symbol);
                            int spellPos = TRAVEL_STEPS - step;
                            string before = new string(' ', spellPos);
                            string after = new string(' ', Math.Max(0, ARENA_GAP - spellPos - revSym.Length));
                            gapRow = (before + revSym + after).PadRight(ARENA_GAP);
                            if (gapRow.Length > ARENA_GAP) gapRow = gapRow.Substring(0, ARENA_GAP);
                        }

                        Console.ForegroundColor = spellColor;
                        Console.Write(gapRow);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(new string(' ', ARENA_GAP));
                    }

                    Console.ForegroundColor = enemyColor;
                    Console.Write(en);
                    Console.ResetColor();
                    Console.WriteLine();
                }

                // Names
                Console.ForegroundColor = playerColor;
                Console.Write(("  " + playerName).PadRight(LEFT_CHAR_WIDTH));
                Console.ResetColor();
                Console.Write(new string(' ', ARENA_GAP));
                Console.ForegroundColor = enemyColor;
                Console.Write("  " + enemyName);
                Console.ResetColor();
                Console.WriteLine();

                UI.DrawLine('-', ConsoleColor.DarkGray, 70);
                Console.WriteLine();

                // Incantation label with progress arrow
                if (playerCasting)
                {
                    Console.ForegroundColor = spellColor;
                    Console.Write("  " + playerName + ": ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\"" + incantation + "!\"  " + new string('~', step + 1) + ">");
                }
                else
                {
                    Console.ForegroundColor = spellColor;
                    Console.Write("  " + enemyName + ": ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\"" + incantation + "!\"  <" + new string('~', TRAVEL_STEPS - step + 1));
                }
                Console.ResetColor();

                Thread.Sleep(60);
            }

            // Final HIT message
            Thread.Sleep(150);
            Console.ForegroundColor = ConsoleColor.White;
            if (playerCasting)
                Console.WriteLine("\n  *** DIRECT HIT ON " + enemyName.ToUpper() + "! ***");
            else
                Console.WriteLine("\n  *** " + playerName.ToUpper() + " IS STRUCK! ***");
            Console.ResetColor();
            Thread.Sleep(400);
        }

        // ══════════════════════════════════════════════
        // Reverse a symbol for right-to-left direction
        // e.g. "~*=>" becomes "<=*~"
        // ══════════════════════════════════════════════
        private static string ReverseSymbol(string sym)
        {
            string rev = "";
            for (int k = sym.Length - 1; k >= 0; k--)
            {
                char c = sym[k];
                if (c == '>') rev += '<';
                else if (c == '<') rev += '>';
                else rev += c;
            }
            return rev;
        }

        // ══════════════════════════════════════════════
        // HP bar display
        // Color: Green > 60hp, Yellow > 30hp, Red <= 30hp
        // ══════════════════════════════════════════════
        private static void DrawHPBar(string name, int hp, int maxHP, ConsoleColor color)
        {
            int filled = (int)((hp / (double)maxHP) * 20);
            filled = Math.Max(0, Math.Min(20, filled));
            int empty = 20 - filled;

            ConsoleColor barColor;
            if (hp > 60) barColor = ConsoleColor.Green;
            else if (hp > 30) barColor = ConsoleColor.Yellow;
            else barColor = ConsoleColor.Red;

            Console.ForegroundColor = color;
            Console.Write("  " + name.PadRight(22) + " HP: ");
            Console.ForegroundColor = barColor;
            Console.Write("[" + new string('#', filled) + new string('-', empty) + "]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  " + hp + "/100");
            Console.ResetColor();
        }

        // ══════════════════════════════════════════════
        // Flash a hit message 2 times (blink effect)
        // ══════════════════════════════════════════════
        public static void HitFlash(string message, ConsoleColor color)
        {
            Console.WriteLine();
            for (int f = 0; f < 2; f++)
            {
                Console.ForegroundColor = color;
                Console.Write("  *** " + message + " ***");
                Console.ResetColor();
                Thread.Sleep(130);
                Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                Thread.Sleep(80);
            }
            Console.ForegroundColor = color;
            Console.WriteLine("  *** " + message + " ***");
            Console.ResetColor();
        }
    }
}

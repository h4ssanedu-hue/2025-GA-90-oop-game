// ═══════════════════════════════════════════════════════════════
//   FILE: DarkWizard.cs
//   DarkWizard class — Inherits Person
//   The villain! Has ASCII art and dark power level
// ═══════════════════════════════════════════════════════════════

using System;

namespace HogwartsGame
{
    class DarkWizard : Person
    {
        // ── Private dark power field ─────────────────────────────
        private int _darkPower;

        // ── Properties ──────────────────────────────────────────
        public string Title { get; private set; }

        // ── Constructor ─────────────────────────────────────────
        public DarkWizard(string name, int age, string title, int darkPower)
            : base(name, age)
        {
            Title = title;
            _darkPower = darkPower;
        }

        // ── Polymorphism: override Introduce ────────────────────
        public override void Introduce()
        {
            UI.ColorWrite("\n  HAHAHAHA... I am " + Title + " — " + Name + "!", ConsoleColor.DarkRed);
            UI.ColorWrite("  Your pathetic resistance ends HERE. AVADA KEDAVRA!", ConsoleColor.DarkRed);
        }

        // ── Get dark power level ─────────────────────────────────
        public int GetDarkPower() { return _darkPower; }

        // ── ASCII Art for Voldemort ──────────────────────────────
        public string[] GetAsciiArt()
        {
            return new string[]
            {
                @"   _/\/\/\_  ",
                @"  (X_X_X_X) ",
                @"  _)=====(_ ",
                @" / VLDMRT  \",
                @"(//~~~~~\\) ",
                @" \\     //  ",
                @"  \\   //   ",
                @"  [|||||]   "
            };
        }
    }
}

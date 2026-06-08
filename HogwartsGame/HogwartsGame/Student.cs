// ═══════════════════════════════════════════════════════════════
//   FILE: Student.cs
//   Student class — Inherits Person
//   Features: spells, HP, mana, house points, year advancement
// ═══════════════════════════════════════════════════════════════

using System;
using System.Collections.Generic;

namespace HogwartsGame
{
    class Student : Person
    {
        // ── Private fields ──────────────────────────────────────
        private int _housePoints;
        private int _health;
        private int _mana;
        private List<Spell> _learnedSpells;

        // ── Properties ──────────────────────────────────────────
        public House StudentHouse { get; private set; }
        public int Year { get; private set; }
        public int SpellCount { get { return _learnedSpells.Count; } }

        // ── Year Advancement Thresholds ──────────────────────────
        // Year 1→2 needs 100 pts, 2→3 needs 250 pts, and so on
        private static readonly int[] YearThresholds = { 0, 100, 250, 450, 700, 1000, 1350 };

        // Check if student has enough points to move to next year
        // Returns true if year actually advanced
        public bool CheckYearAdvancement()
        {
            if (Year >= 7) return false;
            int nextYear = Year + 1;
            if (nextYear <= 7 && HousePoints >= YearThresholds[nextYear - 1])
            {
                Year++;
                Age++;   // Birthday too!
                return true;
            }
            return false;
        }

        // How many more points needed for next year (-1 if already Year 7)
        public int PointsForNextYear()
        {
            if (Year >= 7) return -1;
            return YearThresholds[Year] - HousePoints;
        }

        // ── Clamp helper ────────────────────────────────────────
        private static int Clamp(int v, int lo, int hi)
        {
            if (v < lo) return lo;
            if (v > hi) return hi;
            return v;
        }

        // ── HP Property (clamped 0–100) ──────────────────────────
        public int Health
        {
            get { return _health; }
            set { _health = Clamp(value, 0, 100); }
        }

        // ── Mana Property (clamped 0–100) ───────────────────────
        public int Mana
        {
            get { return _mana; }
            set { _mana = Clamp(value, 0, 100); }
        }

        // ── House Points (never goes below 0) ───────────────────
        public int HousePoints
        {
            get { return _housePoints; }
            private set { _housePoints = Math.Max(0, value); }
        }

        // ── Constructor ─────────────────────────────────────────
        public Student(string name, int age, House house, int year)
            : base(name, age)
        {
            StudentHouse = house;
            Year = year;
            Health = 100;
            Mana = 100;
            HousePoints = 0;
            _learnedSpells = new List<Spell>();
        }

        // ── Polymorphism: override Introduce ────────────────────
        public override void Introduce()
        {
            UI.ColorWrite("\n  My name is " + Name + ", a Year " + Year + " student!", HouseColor());
            UI.ColorWrite("  I proudly represent the house of " + StudentHouse + "!", HouseColor());
        }

        // ── Polymorphism: override DisplayInfo ──────────────────
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            UI.ColorWrite("  * House  : " + StudentHouse, HouseColor());
            UI.ColorWrite("  * Year   : " + Year + " of 7", ConsoleColor.White);
            UI.ColorWrite("  * HP     : " + Health + " / 100", ConsoleColor.Green);
            UI.ColorWrite("  * Mana   : " + Mana + " / 100", ConsoleColor.Cyan);
            UI.ColorWrite("  * Points : " + HousePoints + " pts", ConsoleColor.Yellow);

            int needed = PointsForNextYear();
            if (needed > 0)
                UI.ColorWrite("  * Next Yr: " + needed + " more pts needed for Year " + (Year + 1), ConsoleColor.DarkGray);
            else if (Year >= 7)
                UI.ColorWrite("  * Status : Final Year — Graduation imminent!", ConsoleColor.DarkYellow);
        }

        // ── Learn a new spell ───────────────────────────────────
        public void LearnSpell(Spell spell)
        {
            _learnedSpells.Add(spell);
            HousePoints += 10;
            UI.ColorWrite("\n  >> " + Name + " learned \"" + spell.SpellName + "\"! +10 House Points!", ConsoleColor.Yellow);
        }

        // ── Get spell by index ──────────────────────────────────
        public Spell GetSpell(int index)
        {
            if (index < 0 || index >= _learnedSpells.Count) return null;
            return _learnedSpells[index];
        }

        // ── Cast a spell (deducts mana, adds points) ────────────
        public bool CastSpell(int index, string targetName)
        {
            Spell s = GetSpell(index);
            if (s == null)
            {
                UI.ColorWrite("  [!] Invalid spell!", ConsoleColor.Red);
                return false;
            }

            int cost = s.Power / 2;
            if (Mana < cost)
            {
                UI.ColorWrite("  [!] Not enough mana! (Need: " + cost + ", Have: " + Mana + ")", ConsoleColor.Red);
                return false;
            }

            Mana -= cost;

            ConsoleColor c;
            if (s.Type == SpellType.Dark) c = ConsoleColor.DarkMagenta;
            else if (s.Type == SpellType.Attack) c = ConsoleColor.Red;
            else if (s.Type == SpellType.Defense) c = ConsoleColor.Cyan;
            else c = ConsoleColor.Yellow;

            UI.TypeWrite("  " + s.Cast(Name, targetName), c);
            HousePoints += s.Power;
            return true;
        }

        // ── Show all learned spells ─────────────────────────────
        public void ShowSpells()
        {
            if (_learnedSpells.Count == 0)
            {
                UI.ColorWrite("  [!] No spells learned yet!", ConsoleColor.DarkGray);
                return;
            }

            UI.ColorWrite("\n  Your Spells:", ConsoleColor.Yellow);
            UI.ColorWrite("      TYPE    SPELL NAME              | INCANTATION          | POWER", ConsoleColor.DarkGray);
            UI.DrawLine('-', ConsoleColor.DarkGray, 62);

            for (int i = 0; i < _learnedSpells.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  [" + (i + 1) + "] ");
                Console.ResetColor();
                _learnedSpells[i].DisplaySpell();
            }
        }

        // ── Heal HP ─────────────────────────────────────────────
        public void RestoreHealth(int amount)
        {
            Health += amount;
            UI.ColorWrite("  [+] Health restored by " + amount + "! (Now: " + Health + "/100)", ConsoleColor.Green);
        }

        // ── Restore Mana ─────────────────────────────────────────
        public void RestoreMana(int amount)
        {
            Mana += amount;
            UI.ColorWrite("  [+] Mana restored by " + amount + "! (Now: " + Mana + "/100)", ConsoleColor.Cyan);
        }

        // ── Add House Points ────────────────────────────────────
        public void AddPoints(int pts) { HousePoints += pts; }

        // ── House color for console output ──────────────────────
        public ConsoleColor HouseColor()
        {
            if (StudentHouse == House.Gryffindor) return ConsoleColor.Red;
            else if (StudentHouse == House.Slytherin) return ConsoleColor.Green;
            else if (StudentHouse == House.Hufflepuff) return ConsoleColor.Yellow;
            else if (StudentHouse == House.Ravenclaw) return ConsoleColor.Blue;
            else return ConsoleColor.White;
        }

        // ── ASCII Art (house-based wizard character) ─────────────
        public string[] GetAsciiArt()
        {
            if (StudentHouse == House.Gryffindor)
            {
                return new string[]
                {
                    @"    /\_/\  ",
                    @"   ( o.o ) ",
                    @"   _)==(__ ",
                    @"  /  GR  \ ",
                    @" (\\~~~~//)",
                    @"  \\    // ",
                    @"   \\  //  ",
                    @"   [||||]  "
                };
            }
            else if (StudentHouse == House.Slytherin)
            {
                return new string[]
                {
                    @"    /===\  ",
                    @"   (>.<)   ",
                    @"   _)~~(__ ",
                    @"  /  SL  \ ",
                    @" (//~~~~\\)",
                    @"  //    \\ ",
                    @"   //  \\  ",
                    @"   [||||]  "
                };
            }
            else if (StudentHouse == House.Hufflepuff)
            {
                return new string[]
                {
                    @"    /*_*\  ",
                    @"   ( ^.^ ) ",
                    @"   _)==(__ ",
                    @"  /  HF  \ ",
                    @" (\\~~~~//)",
                    @"  \\    // ",
                    @"   \\  //  ",
                    @"   [||||]  "
                };
            }
            else // Ravenclaw
            {
                return new string[]
                {
                    @"    /~*~\  ",
                    @"   ( 0.0 ) ",
                    @"   _)==(__ ",
                    @"  /  RC  \ ",
                    @" (\\~~~~//)",
                    @"  \\    // ",
                    @"   \\  //  ",
                    @"   [||||]  "
                };
            }
        }
    }
}

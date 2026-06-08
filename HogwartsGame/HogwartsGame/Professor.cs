// ═══════════════════════════════════════════════════════════════
//   FILE: Professor.cs
//   Professor class — Inherits Person
//   Can teach spells to students
// ═══════════════════════════════════════════════════════════════

using System;
using System.Collections.Generic;

namespace HogwartsGame
{
    class Professor : Person
    {
        // ── Properties ──────────────────────────────────────────
        public string Subject { get; private set; }

        // ── Private spell list ──────────────────────────────────
        private List<Spell> _teachableSpells;

        // ── Constructor ─────────────────────────────────────────
        public Professor(string name, int age, string subject)
            : base(name, age)
        {
            Subject = subject;
            _teachableSpells = new List<Spell>();
        }

        // ── Polymorphism: override Introduce ────────────────────
        public override void Introduce()
        {
            UI.ColorWrite("\n  I am Professor " + Name + ", and I teach " + Subject + ".", ConsoleColor.Magenta);
            UI.ColorWrite("  Pay close attention — this knowledge may save your life.", ConsoleColor.DarkMagenta);
        }

        // ── Add a spell to this professor's teachable list ──────
        public void AddTeachableSpell(Spell s) { _teachableSpells.Add(s); }

        // ── Teach a student (they choose which spell to learn) ───
        public void TeachStudent(Student student)
        {
            if (_teachableSpells.Count == 0)
            {
                UI.ColorWrite("  [!] No spells available to teach right now.", ConsoleColor.DarkGray);
                return;
            }

            UI.ColorWrite("\n  Class is now in session!", ConsoleColor.Magenta);
            UI.ColorWrite("  Subject: " + Subject, ConsoleColor.DarkMagenta);
            UI.DrawLine('-', ConsoleColor.DarkMagenta, 62);
            UI.ColorWrite("  Choose a spell to learn:\n", ConsoleColor.White);
            UI.ColorWrite("      TYPE    SPELL NAME              | INCANTATION          | POWER", ConsoleColor.DarkGray);
            UI.DrawLine('-', ConsoleColor.DarkGray, 62);

            for (int i = 0; i < _teachableSpells.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  [" + (i + 1) + "] ");
                Console.ResetColor();
                _teachableSpells[i].DisplaySpell();
            }

            Console.Write("\n  Enter spell number: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) &&
                choice >= 1 && choice <= _teachableSpells.Count)
            {
                student.LearnSpell(_teachableSpells[choice - 1]);
                student.AddPoints(5);
                UI.ColorWrite("  Professor " + Name + ": \"Excellent! +5 bonus points to "
                              + student.StudentHouse + "!\"", ConsoleColor.Magenta);
            }
            else
            {
                UI.ColorWrite("  [!] Invalid choice — class dismissed!", ConsoleColor.Red);
            }
        }
    }
}

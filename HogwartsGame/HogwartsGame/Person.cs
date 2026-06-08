// ═══════════════════════════════════════════════════════════════
//   FILE: Person.cs
//   Abstract base class — Encapsulation + Abstraction
// ═══════════════════════════════════════════════════════════════

using System;

namespace HogwartsGame
{
    abstract class Person
    {
        // ── Private fields (Encapsulation) ──────────────────────
        private string _name;
        private int _age;

        // ── Properties with validation ───────────────────────────
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty!");
                _name = value;
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 1 || value > 999)
                    throw new ArgumentException("Age must be between 1 and 999!");
                _age = value;
            }
        }

        // ── Constructor ─────────────────────────────────────────
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // ── Abstract methods (must be overridden) ────────────────
        public abstract void Introduce();

        // ── Virtual method (can be overridden) ──────────────────
        public virtual void DisplayInfo()
        {
            UI.ColorWrite("  * Name   : " + Name, ConsoleColor.White);
            UI.ColorWrite("  * Age    : " + Age + " years", ConsoleColor.White);
        }
    }
}

// ═══════════════════════════════════════════════════════════════
//   FILE: Spell.cs
//   Abstract Spell base class + 4 derived spell types
//   Demonstrates: Inheritance + Polymorphism
// ═══════════════════════════════════════════════════════════════

using System;

namespace HogwartsGame
{
    // ══════════════════════════════════════════════
    // BASE CLASS: Spell  (Abstract — Polymorphism)
    // ══════════════════════════════════════════════
    abstract class Spell
    {
        public string SpellName { get; protected set; }
        public int Power { get; protected set; }
        public SpellType Type { get; protected set; }
        public string Incantation { get; protected set; }

        // Symbol that flies across screen during battle animation
        public string ProjectileSymbol { get; protected set; }

        public Spell(string name, string incantation, int power, SpellType type, string symbol)
        {
            SpellName = name;
            Incantation = incantation;
            Power = power;
            Type = type;
            ProjectileSymbol = symbol;
        }

        // Each spell type describes its own effect differently
        public abstract string Cast(string casterName, string targetName);

        // Display spell in the spell list menu
        public virtual void DisplaySpell()
        {
            ConsoleColor color;
            string label;

            if (Type == SpellType.Attack) { color = ConsoleColor.Red; label = "[ATK]"; }
            else if (Type == SpellType.Defense) { color = ConsoleColor.Cyan; label = "[DEF]"; }
            else if (Type == SpellType.Charm) { color = ConsoleColor.Yellow; label = "[CHM]"; }
            else { color = ConsoleColor.DarkMagenta; label = "[DRK]"; }

            UI.ColorWrite("      " + label + "  " + SpellName.PadRight(22) +
                          "| " + Incantation.PadRight(20) + "| Power: " + Power, color);
        }
    }

    // ══════════════════════════════════════════════
    // DERIVED: AttackSpell
    // ══════════════════════════════════════════════
    class AttackSpell : Spell
    {
        public AttackSpell(string name, string incantation, int power)
            : base(name, incantation, power, SpellType.Attack, "~*=>") { }

        public override string Cast(string caster, string target)
        {
            return "[ATTACK]  " + caster + " casts \"" + Incantation + "!\"  —  "
                   + target + " takes " + Power + " damage!";
        }
    }

    // ══════════════════════════════════════════════
    // DERIVED: DefenseSpell
    // ══════════════════════════════════════════════
    class DefenseSpell : Spell
    {
        public DefenseSpell(string name, string incantation, int power)
            : base(name, incantation, power, SpellType.Defense, "=~~>") { }

        public override string Cast(string caster, string target)
        {
            return "[SHIELD]  " + caster + " casts \"" + Incantation + "!\"  —  "
                   + Power + "-point barrier raised!";
        }
    }

    // ══════════════════════════════════════════════
    // DERIVED: CharmSpell
    // ══════════════════════════════════════════════
    class CharmSpell : Spell
    {
        public CharmSpell(string name, string incantation, int power)
            : base(name, incantation, power, SpellType.Charm, "-**>") { }

        public override string Cast(string caster, string target)
        {
            return "[CHARM]   " + caster + " casts \"" + Incantation + "!\"  —  "
                   + target + " restored by " + Power + " mana!";
        }
    }

    // ══════════════════════════════════════════════
    // DERIVED: DarkSpell  (2x damage!)
    // ══════════════════════════════════════════════
    class DarkSpell : Spell
    {
        public DarkSpell(string name, string incantation, int power)
            : base(name, incantation, power, SpellType.Dark, "###>") { }

        public override string Cast(string caster, string target)
        {
            return "[DARK]    " + caster + " unleashes \"" + Incantation + "!\"  —  "
                   + target + " suffers " + (Power * 2) + " devastating damage!";
        }
    }
}

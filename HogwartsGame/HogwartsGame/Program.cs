// ═══════════════════════════════════════════════════════════════
//   FILE: Program.cs
//   Main game controller — HogwartsAdventure class
//   Entry point: Main()
//   All game screens and menus are here
// ═══════════════════════════════════════════════════════════════

using System;
using System.Threading;

namespace HogwartsGame
{
    class HogwartsAdventure
    {
        // ── Game world objects ───────────────────────────────────
        static Student player;
        static Professor profDumbledore;
        static Professor profSnape;
        static Professor profMcGonagall;
        static DarkWizard voldemort;

        // ════════════════════════════════════════════════════════
        // ENTRY POINT
        // ════════════════════════════════════════════════════════
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Hogwarts Adventure — C# OOP Battle Game";

            InitializeWorld();
            ShowIntro();
            CreateCharacter();
            MainMenu();
        }

        // ════════════════════════════════════════════════════════
        // WORLD SETUP — Create professors, spells, Voldemort
        // ════════════════════════════════════════════════════════
        static void InitializeWorld()
        {
            // Professors
            profDumbledore = new Professor("Albus Dumbledore", 150, "Defence Against the Dark Arts");
            profSnape = new Professor("Severus Snape", 50, "Potions & Dark Arts");
            profMcGonagall = new Professor("Minerva McGonagall", 75, "Transfiguration & Charms");

            // Dumbledore's spells
            profDumbledore.AddTeachableSpell(new AttackSpell("Expelliarmus", "Expelliarmus", 20));
            profDumbledore.AddTeachableSpell(new DefenseSpell("Protego", "Protego", 25));
            profDumbledore.AddTeachableSpell(new CharmSpell("Lumos Maxima", "Lumos Maxima", 15));

            // Snape's spells
            profSnape.AddTeachableSpell(new DarkSpell("Avada Kedavra", "Avada Kedavra", 50));
            profSnape.AddTeachableSpell(new AttackSpell("Sectumsempra", "Sectumsempra", 35));
            profSnape.AddTeachableSpell(new DefenseSpell("Occlumency Shield", "Occlumens", 30));

            // McGonagall's spells
            profMcGonagall.AddTeachableSpell(new CharmSpell("Episkey", "Episkey", 40));
            profMcGonagall.AddTeachableSpell(new AttackSpell("Confringo", "Confringo", 28));
            profMcGonagall.AddTeachableSpell(new CharmSpell("Wingardium Leviosa", "Wingardium", 12));

            // The Dark Lord
            voldemort = new DarkWizard("Lord Voldemort", 71, "He-Who-Must-Not-Be-Named", 35);
        }

        // ════════════════════════════════════════════════════════
        // INTRO SCREEN
        // ════════════════════════════════════════════════════════
        static void ShowIntro()
        {
            Console.Clear();
            UI.PrintHeader();
            UI.ColorWrite("         *** School of Witchcraft & Wizardry ***", ConsoleColor.Yellow);
            UI.ColorWrite("               ~~ A C# OOP Adventure Game ~~", ConsoleColor.Cyan);
            UI.DrawLine('═', ConsoleColor.DarkYellow);
            UI.Pause(400);
            UI.TypeWrite("\n  An ancient world exists beyond ordinary sight...", ConsoleColor.Gray);
            UI.TypeWrite("  Hogwarts — where destiny is forged and legends are born.", ConsoleColor.Gray);
            UI.TypeWrite("  And today... YOUR story begins.\n", ConsoleColor.White);
            UI.Pause(600);
        }

        // ════════════════════════════════════════════════════════
        // CHARACTER CREATION
        // House is randomly chosen by Sorting Hat (not by player)
        // ════════════════════════════════════════════════════════
        static void CreateCharacter()
        {
            UI.DrawBox("        WELCOME TO HOGWARTS        ", ConsoleColor.Yellow);

            // Name input
            Console.Write("\n  Enter your wizard name: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string name = Console.ReadLine();
            Console.ResetColor();
            if (string.IsNullOrWhiteSpace(name)) name = "Young Wizard";

            // Age input
            Console.Write("  Enter your age (11-17): ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string ageStr = Console.ReadLine();
            Console.ResetColor();
            int age;
            if (!int.TryParse(ageStr, out age) || age < 11 || age > 17) age = 14;

            // ── Sorting Hat Ceremony ─────────────────────────────
            Console.WriteLine();
            UI.ColorWrite("  The Great Hall falls silent...", ConsoleColor.DarkGray);
            UI.Pause(800);
            UI.ColorWrite("  Professor McGonagall places the ancient Sorting Hat upon your head.", ConsoleColor.White);
            UI.Pause(1000);

            // Animated thinking dots
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("  The Sorting Hat whispers: \"Hmm... let me see...");
            for (int i = 0; i < 6; i++) { Thread.Sleep(500); Console.Write("."); }
            Console.WriteLine("\"");
            Console.ResetColor();
            UI.Pause(600);

            // ── Random house selection ────────────────────────────
            Random rng = new Random();
            Array houses = Enum.GetValues(typeof(House));
            House selectedHouse = (House)houses.GetValue(rng.Next(houses.Length));

            // House-specific messages from the hat
            string[] sortingMessages;
            ConsoleColor houseCol;

            if (selectedHouse == House.Gryffindor)
            {
                houseCol = ConsoleColor.Red;
                sortingMessages = new string[]
                {
                    "  \"I see courage burning in your heart...\"",
                    "  \"Bravery beyond measure...\"",
                    "  \"The lion's spirit lives in you!\""
                };
            }
            else if (selectedHouse == House.Slytherin)
            {
                houseCol = ConsoleColor.Green;
                sortingMessages = new string[]
                {
                    "  \"Cunning... ambition... resourcefulness...\"",
                    "  \"You know exactly what you want...\"",
                    "  \"The serpent recognizes its own!\""
                };
            }
            else if (selectedHouse == House.Hufflepuff)
            {
                houseCol = ConsoleColor.Yellow;
                sortingMessages = new string[]
                {
                    "  \"Loyal... patient... hardworking...\"",
                    "  \"A kind and honest soul...\"",
                    "  \"The badger welcomes you!\""
                };
            }
            else // Ravenclaw
            {
                houseCol = ConsoleColor.Blue;
                sortingMessages = new string[]
                {
                    "  \"Wit and wisdom... a sharp mind...\"",
                    "  \"Knowledge is your greatest power...\"",
                    "  \"The eagle soars for you!\""
                };
            }

            foreach (string msg in sortingMessages)
            {
                UI.TypeWrite(msg, ConsoleColor.DarkYellow, 22);
                UI.Pause(500);
            }

            UI.Pause(600);

            // ── Big house reveal ──────────────────────────────────
            Console.ForegroundColor = houseCol;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════╗");
            string houseLabel = "        " + selectedHouse.ToString().ToUpper() + "!        ";
            Console.WriteLine("  ║" + houseLabel.PadLeft(20 + houseLabel.Length / 2).PadRight(38) + "║");
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.ResetColor();

            UI.Pause(400);
            player = new Student(name, age, selectedHouse, 1);

            Console.WriteLine();
            player.Introduce();
            UI.Pause(1000);
            Console.Write("\n  Press Enter to enter the castle...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // MAIN MENU LOOP
        // ════════════════════════════════════════════════════════
        static void MainMenu()
        {
            while (true)
            {
                UI.ClearWithHeader();
                UI.DrawBox("  " + player.Name + "  |  " + player.StudentHouse
                           + "  |  Year " + player.Year + "  |  " + player.HousePoints + " pts  ",
                           ConsoleColor.DarkYellow);

                UI.ColorWrite("\n  HOGWARTS CASTLE — GREAT HALL\n", ConsoleColor.Yellow);
                UI.ColorWrite("  [1]   View Your Profile", ConsoleColor.Cyan);
                UI.ColorWrite("  [2]   Prof. Dumbledore's Class   (Defence Against Dark Arts)", ConsoleColor.Green);
                UI.ColorWrite("  [3]   Prof. Snape's Class        (Potions & Dark Arts)", ConsoleColor.DarkGreen);
                UI.ColorWrite("  [4]   Prof. McGonagall's Class   (Transfiguration & Charms)", ConsoleColor.Blue);
                UI.ColorWrite("  [5]   Spell Practice             (Dueling Dummy)", ConsoleColor.Yellow);
                UI.ColorWrite("  [6]   FACE VOLDEMORT             (Animated Boss Battle!)", ConsoleColor.DarkRed);
                UI.ColorWrite("  [7]   House Points Leaderboard", ConsoleColor.Magenta);
                UI.ColorWrite("  [8]   Hospital Wing              (Restore HP & Mana)", ConsoleColor.Green);
                UI.ColorWrite("  [9]   Leave Hogwarts             (Exit)\n", ConsoleColor.DarkGray);
                UI.DrawLine('-', ConsoleColor.DarkYellow);
                Console.Write("  Enter your choice: ");

                string choice = Console.ReadLine();
                if (choice == null) choice = "";

                switch (choice.Trim())
                {
                    case "1": ShowProfile(); break;
                    case "2": AttendClass(profDumbledore); CheckAndAnnounceYear(); break;
                    case "3": AttendClass(profSnape); CheckAndAnnounceYear(); break;
                    case "4": AttendClass(profMcGonagall); CheckAndAnnounceYear(); break;
                    case "5": SpellPractice(); CheckAndAnnounceYear(); break;
                    case "6": FinalBoss(); CheckAndAnnounceYear(); break;
                    case "7": Leaderboard(); break;
                    case "8": HealStudent(); break;
                    case "9": ExitGame(); return;
                    default:
                        UI.ColorWrite("\n  [!] Invalid choice. Please try again.", ConsoleColor.Red);
                        UI.Pause(700);
                        break;
                }
            }
        }

        // ════════════════════════════════════════════════════════
        // PROFILE SCREEN
        // ════════════════════════════════════════════════════════
        static void ShowProfile()
        {
            UI.ClearWithHeader();
            UI.DrawBox("          YOUR STUDENT PROFILE          ", ConsoleColor.Cyan);
            Console.WriteLine();
            player.DisplayInfo();
            player.ShowSpells();
            UI.DrawLine('-', ConsoleColor.DarkCyan);
            Console.Write("\n  Press Enter to return...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // ATTEND CLASS
        // ════════════════════════════════════════════════════════
        static void AttendClass(Professor prof)
        {
            UI.ClearWithHeader();
            prof.Introduce();
            UI.Pause(500);
            prof.TeachStudent(player);
            UI.Pause(400);
            Console.Write("\n  Press Enter to return...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // SPELL PRACTICE (training dummy)
        // ════════════════════════════════════════════════════════
        static void SpellPractice()
        {
            UI.ClearWithHeader();
            UI.DrawBox("          DUELING DUMMY PRACTICE          ", ConsoleColor.Yellow);

            if (player.SpellCount == 0)
            {
                UI.ColorWrite("\n  [!] You have no spells! Attend a class first.", ConsoleColor.Red);
                UI.Pause(1400);
                return;
            }

            UI.ColorWrite("\n  A training dummy stands before you!\n", ConsoleColor.White);
            player.ShowSpells();
            Console.Write("\n  Enter spell number: ");

            int idx;
            if (int.TryParse(Console.ReadLine(), out idx) && idx >= 1 && idx <= player.SpellCount)
            {
                Console.WriteLine();
                player.CastSpell(idx - 1, "Training Dummy");
                player.AddPoints(15);
                UI.ColorWrite("\n  [+] Well done! +15 House Points!", ConsoleColor.Yellow);
            }
            else
            {
                UI.ColorWrite("  [!] Invalid spell number!", ConsoleColor.Red);
            }

            UI.Pause(400);
            Console.Write("\n  Press Enter to return...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // FINAL BOSS — Animated battle vs Voldemort
        // ════════════════════════════════════════════════════════
        static void FinalBoss()
        {
            UI.ClearWithHeader();

            if (player.SpellCount == 0)
            {
                UI.ColorWrite("\n  [!!] No spells learned! Face Voldemort after studying!", ConsoleColor.Red);
                UI.Pause(1500);
                return;
            }

            // Dramatic entrance
            UI.ColorWrite("\n  The sky turns black...", ConsoleColor.DarkGray);
            UI.Pause(700);
            UI.ColorWrite("  The torches flicker and die...", ConsoleColor.DarkGray);
            UI.Pause(700);
            UI.ColorWrite("  A cold wind sweeps through the corridor...", ConsoleColor.DarkGray);
            UI.Pause(600);
            voldemort.Introduce();
            UI.Pause(800);
            Console.Write("\n  Press Enter to begin the duel...");
            Console.ReadLine();

            // ── Battle variables ─────────────────────────────────
            int volHP = 100;
            int round = 1;
            bool playerWon = false;
            bool playerDead = false;

            // Voldemort's spell pool
            string[] darkIncantations = { "Avada Kedavra", "Crucio", "Sectumsempra", "Expulso", "Bombarda" };
            string[] darkSymbols = { "###>", "~X~>", "@@@>", "!!!>", "OOO>" };
            int[] darkDamages = { 30, 18, 22, 15, 25 };

            Random rng = new Random();

            string[] playerArt = player.GetAsciiArt();
            string[] enemyArt = voldemort.GetAsciiArt();
            ConsoleColor playerColor = player.HouseColor();
            ConsoleColor enemyColor = ConsoleColor.DarkRed;

            // ── Main battle loop (max 7 rounds) ──────────────────
            while (volHP > 0 && player.Health > 0 && round <= 7)
            {
                // Show arena + spell list
                BattleAnimator.DrawArena(
                    player.Name, playerArt, player.Health, player.Mana,
                    "Lord Voldemort", enemyArt, volHP,
                    playerColor, enemyColor, round);

                player.ShowSpells();
                Console.Write("\n  Choose your spell: ");

                int idx;
                if (!int.TryParse(Console.ReadLine(), out idx) || idx < 1 || idx > player.SpellCount)
                {
                    UI.ColorWrite("\n  [!] Invalid spell — you hesitate!", ConsoleColor.Red);
                    UI.Pause(800);
                    round++;
                    continue;
                }

                Spell chosenSpell = player.GetSpell(idx - 1);
                if (chosenSpell == null) { round++; continue; }

                // Mana check
                int manaCost = chosenSpell.Power / 2;
                if (player.Mana < manaCost)
                {
                    UI.ColorWrite("\n  [!] Not enough mana! (Need: " + manaCost + ", Have: " + player.Mana + ")", ConsoleColor.Red);
                    UI.Pause(1000);
                    round++;
                    continue;
                }

                player.Mana -= manaCost;

                // Spell color
                ConsoleColor spellColor;
                if (chosenSpell.Type == SpellType.Dark) spellColor = ConsoleColor.DarkMagenta;
                else if (chosenSpell.Type == SpellType.Attack) spellColor = ConsoleColor.Red;
                else if (chosenSpell.Type == SpellType.Defense) spellColor = ConsoleColor.Cyan;
                else spellColor = ConsoleColor.Yellow;

                // ── Player attacks → spell flies right ────────────
                BattleAnimator.AnimateBattleSpell(
                    player.Name, playerArt, player.Health,
                    "Lord Voldemort", enemyArt, volHP,
                    playerColor, enemyColor,
                    chosenSpell.Incantation, chosenSpell.ProjectileSymbol, spellColor,
                    true, round);

                // Damage calculation
                int playerDamage;
                if (chosenSpell.Type == SpellType.Dark) playerDamage = chosenSpell.Power * 2;
                else if (chosenSpell.Type == SpellType.Attack) playerDamage = chosenSpell.Power;
                else if (chosenSpell.Type == SpellType.Defense) playerDamage = 5;
                else playerDamage = chosenSpell.Power / 2;

                volHP -= playerDamage;
                if (volHP < 0) volHP = 0;
                player.AddPoints(chosenSpell.Power);

                BattleAnimator.HitFlash("Voldemort takes " + playerDamage + " damage!", ConsoleColor.Yellow);
                UI.Pause(500);

                if (volHP <= 0) { playerWon = true; break; }

                // ── Voldemort attacks → spell flies left ──────────
                int darkIdx = rng.Next(0, darkIncantations.Length);
                BattleAnimator.AnimateBattleSpell(
                    player.Name, playerArt, player.Health,
                    "Lord Voldemort", enemyArt, volHP,
                    playerColor, enemyColor,
                    darkIncantations[darkIdx], darkSymbols[darkIdx], ConsoleColor.DarkMagenta,
                    false, round);

                player.Health -= darkDamages[darkIdx];
                if (player.Health < 0) player.Health = 0;

                BattleAnimator.HitFlash(player.Name + " takes " + darkDamages[darkIdx] + " damage!", ConsoleColor.Red);
                UI.Pause(500);

                if (player.Health <= 0) { playerDead = true; break; }

                round++;
            }

            // ── Battle result screen ──────────────────────────────
            Console.Clear();
            UI.PrintHeader();
            UI.DrawLine('=', ConsoleColor.DarkYellow);

            if (playerWon)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
   __   __ ___  ___  _____ ___  ___  ___ ___  __
   \ \ / /|_ _|/ __||_   _/ _ \| _ \\ \ / / / /
    \ V /  | || (__   | || (_) |   / \ V / / /
     \_/  |___|\___|  |_| \___/|_|_\  \_/ /_/
");
                Console.ResetColor();
                UI.TypeWrite("  VOLDEMORT IS DEFEATED! You have saved Hogwarts!", ConsoleColor.Yellow, 18);
                UI.TypeWrite("  The wizarding world shall remember your name forever!", ConsoleColor.Cyan, 18);
                player.AddPoints(500);
                UI.ColorWrite("\n  [+] +500 House Points! Glory to " + player.StudentHouse + "!", ConsoleColor.Yellow);
            }
            else if (playerDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(@"
   ___  ___ ___ ___ ___ ___ _____ ___ ___
   |   \| __|| __|| __| /_\ |_   _|| __|   \
   | |) | _| | _| | _| / _ \  | |  | _|| |) |
   |___/|___||_|  |___/_/ \_\ |_|  |___|___/
");
                Console.ResetColor();
                UI.TypeWrite("  You have fallen... Voldemort claims victory this time.", ConsoleColor.DarkRed, 18);
                UI.TypeWrite("  Train harder. Rise again. Hogwarts needs you.", ConsoleColor.Gray, 18);
                UI.ColorWrite("\n  [!] Visit the Hospital Wing to recover.", ConsoleColor.Red);
                player.Health = 10;
            }
            else
            {
                UI.TypeWrite("\n  You survived the encounter! Voldemort retreats...", ConsoleColor.DarkYellow, 18);
                UI.TypeWrite("  Train harder and face him again!", ConsoleColor.Gray, 18);
                player.AddPoints(100);
                UI.ColorWrite("  [+] +100 House Points for your bravery!", ConsoleColor.Yellow);
            }

            UI.DrawLine('=', ConsoleColor.DarkYellow);
            UI.Pause(500);
            Console.Write("\n  Press Enter to return...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // LEADERBOARD
        // ════════════════════════════════════════════════════════
        static void Leaderboard()
        {
            UI.ClearWithHeader();
            UI.DrawBox("         HOUSE POINTS LEADERBOARD         ", ConsoleColor.Magenta);

            Random rng = new Random();
            int gryff = player.StudentHouse == House.Gryffindor ? player.HousePoints : rng.Next(80, 400);
            int slyt = player.StudentHouse == House.Slytherin ? player.HousePoints : rng.Next(80, 400);
            int huff = player.StudentHouse == House.Hufflepuff ? player.HousePoints : rng.Next(80, 400);
            int rave = player.StudentHouse == House.Ravenclaw ? player.HousePoints : rng.Next(80, 400);

            Console.WriteLine();
            UI.ColorWrite("  GRYFFINDOR   :  " + gryff + " points", ConsoleColor.Red);
            UI.ColorWrite("  SLYTHERIN    :  " + slyt + " points", ConsoleColor.Green);
            UI.ColorWrite("  HUFFLEPUFF   :  " + huff + " points", ConsoleColor.Yellow);
            UI.ColorWrite("  RAVENCLAW    :  " + rave + " points", ConsoleColor.Blue);
            UI.DrawLine('-', ConsoleColor.Magenta, 62);
            UI.ColorWrite("  Your House (" + player.StudentHouse + "): " + player.HousePoints + " points", player.HouseColor());

            Console.Write("\n  Press Enter to return...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // HOSPITAL WING
        // ════════════════════════════════════════════════════════
        static void HealStudent()
        {
            UI.ClearWithHeader();
            UI.DrawBox("      MADAME POMFREY — HOSPITAL WING      ", ConsoleColor.Green);
            UI.TypeWrite("\n  Madam Pomfrey: \"Rest now, dear. You will be right as rain.\"", ConsoleColor.Green, 18);
            UI.Pause(600);
            Console.WriteLine();
            player.RestoreHealth(40);
            player.RestoreMana(40);
            UI.Pause(800);
            Console.Write("\n  Press Enter to return...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // YEAR ADVANCEMENT CHECK & CEREMONY
        // Called after every activity that earns points
        // ════════════════════════════════════════════════════════
        static void CheckAndAnnounceYear()
        {
            if (!player.CheckYearAdvancement()) return;

            Console.Clear();
            UI.PrintHeader();
            Console.WriteLine();

            ConsoleColor hc = player.HouseColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  " + new string('★', 34));
            Console.ResetColor();

            UI.TypeWrite("\n  Professor Dumbledore steps forward...", ConsoleColor.Magenta, 20);
            UI.Pause(600);
            UI.TypeWrite("  \"Another year of growth, wisdom, and courage!\"", ConsoleColor.Magenta, 18);
            UI.Pause(400);

            string[] yearTitles = { "", "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh" };
            string title = (player.Year <= 7) ? yearTitles[player.Year] : "Seventh";

            Console.ForegroundColor = hc;
            Console.WriteLine(@"
   ╔══════════════════════════════════════════════╗");
            string line1 = "   ║   CONGRATULATIONS, " + player.Name.ToUpper() + "!";
            Console.WriteLine(line1.PadRight(47) + "║");
            string line2 = "   ║   You are now a " + title.ToUpper() + " YEAR student!";
            Console.WriteLine(line2.PadRight(47) + "║");
            string line3 = "   ║   Age: " + player.Age + "  |  Points: " + player.HousePoints;
            Console.WriteLine(line3.PadRight(47) + "║");
            Console.WriteLine(@"   ╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            // Year-specific flavour messages
            string[] msgs;
            if (player.Year == 2) msgs = new string[] { "  You've mastered the basics. Greater challenges await!", "  New subjects unlock in Year Two." };
            else if (player.Year == 3) msgs = new string[] { "  Care of Magical Creatures begins this year!", "  Your spells grow stronger with each lesson." };
            else if (player.Year == 4) msgs = new string[] { "  The Triwizard Tournament whispers your name...", "  Fourth Year students face real danger." };
            else if (player.Year == 5) msgs = new string[] { "  O.W.L. examinations approach. Study hard!", "  Umbridge cannot stop your progress." };
            else if (player.Year == 6) msgs = new string[] { "  N.E.W.T. level magic is within your grasp!", "  The Dark Lord stirs... be ready." };
            else if (player.Year == 7) msgs = new string[] { "  Your FINAL year at Hogwarts begins!", "  The fate of the wizarding world rests on YOU." };
            else msgs = new string[] { "  Keep collecting House Points to advance further!" };

            Console.WriteLine();
            foreach (string m in msgs) UI.TypeWrite(m, hc, 18);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n  " + new string('★', 34));
            Console.ResetColor();

            UI.Pause(500);
            Console.Write("\n  Press Enter to continue your journey...");
            Console.ReadLine();
        }

        // ════════════════════════════════════════════════════════
        // EXIT GAME
        // ════════════════════════════════════════════════════════
        static void ExitGame()
        {
            UI.ClearWithHeader();
            UI.TypeWrite("\n  Hogwarts will always be your home...", ConsoleColor.DarkYellow, 20);
            UI.TypeWrite("  Until we meet again — Mischief Managed!\n", ConsoleColor.Yellow, 20);
            UI.ColorWrite("  Final House Points: " + player.HousePoints + " pts", ConsoleColor.Cyan);
            UI.DrawLine('=', ConsoleColor.DarkYellow);
            UI.Pause(1500);
        }
    }
}

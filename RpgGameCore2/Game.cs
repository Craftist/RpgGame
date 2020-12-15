using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RpgGameCore2.Data;
using RpgGameCore2.Data.Creatures;
using RpgGameCore2.Screens;
using RpgGameCore2.Screens.Intro;
using RpgGameCore2.Util;

namespace RpgGameCore2
{
    internal class Game
    {
        public static Screen CurrentScreen;
        public static string ErrorString;
        public static Action ActionAfterGoto;

        /// <summary>
        /// If the array is not empty (or null), it shows the specified items on the top of the screen, prompting to use them.
        /// </summary>
        public static InventoryItem[] AcquiredItems;

        public static Human Player = new Human {
            Name = "Незнакомец"
        };

        public class ScreenCollection
        {
            public readonly Screen MainMenu = new MainMenuScreen();
            public readonly Screen GameOver = new GameOverScreen();
            public readonly Screen MainSquare = new MainSquareScreen();
            public readonly Screen Tavern = new TavernScreen();
            public readonly Screen Inventory = new InventoryScreen();

            public readonly Screen Intro1 = new Intro1Screen();
            public readonly Screen Intro2 = new Intro2Screen();
            public readonly Screen IntroTavern = new IntroTavernScreen();
            public readonly Screen IntroHealer = new IntroHealerScreen();
            public readonly Screen IntroShu = new IntroShuScreen();
            public readonly Screen Intro3 = new Intro3Screen();
            public readonly Screen Intro4 = new Intro4Screen();
            public readonly Screen Intro5 = new Intro5DScreen();
        }

        public static readonly ScreenCollection Screens = new ScreenCollection();

        public static readonly Dictionary<string, Creature> NamedCreatures = new Dictionary<string, Creature> {
            { "IntroDrunk", new Human { Name = "Местный пьяница", Strength = 4, Defense = 2, Agility = 2, Luck = 1, CurrentHealth = 100, MaxHealth = 100 } }
        };
        public static readonly List<Creature> Creatures = new List<Creature>();

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;

            GoTo(Screens.MainMenu);

            Console.Write("Игра окончена. Нажмите ENTER для выхода.");
            Console.ReadLine();
        }

        public static void AddItem(InventoryItem item)
        {
            Player.Inventory.Add(item);
            AcquiredItems = new[] { item };
        }

        public static void AddItems(params InventoryItem[] items)
        {
            Player.Inventory.AddRange(items);
            AcquiredItems = items;
        }

        public static void RenderStats()
        {
            Utils.PrintStats(Player);
            int inventoryLeft = Console.BufferWidth - $"💎 {Player.Diamonds}".Length - "[I] Open Inventory".Length + 4;
            Console.CursorLeft = inventoryLeft;
            Console.WriteLine("[I] Open Inventory");
            Console.CursorLeft = 0;

            Write($"{Player.Name} [LVL {Player.CurrentLevel}, EXP: {Player.CurrentExp}/...]\n");

            if (!string.IsNullOrWhiteSpace(CurrentScreen.LocationName)) {
                var textToCenter = $" {CurrentScreen.LocationName} ";
                var spacesLeft = Console.BufferWidth / 2 - textToCenter.Length / 2;
                var spacesRight = Console.BufferWidth - textToCenter.Length - spacesLeft;
                Write(new string('-', spacesLeft), ConsoleColor.DarkGray);
                Write(textToCenter, ConsoleColor.White);
                Write(new string('-', spacesRight), ConsoleColor.DarkGray);
            } else {
                WriteLine(new string('-', Console.BufferWidth), ConsoleColor.DarkGray);
            }
        }

        public static void RenderActions(List<ScreenAction> actions)
        {
            if (actions == null) return;

            int i = 1;
            foreach (var screenAction in actions) {
                Write($"{i}. ", ConsoleColor.Cyan);
                Write(screenAction.Dialogue != null ? $"\"{screenAction.Dialogue()}\"" : $"{screenAction.Description?.Invoke()}", ConsoleColor.Yellow);

                if (screenAction.AttackTarget?.Invoke() != null) {
                    Write($" [{Player.GetNameStats()} vs {screenAction.AttackTarget?.Invoke().GetNameStats()}]");
                }

                if (screenAction.PurchasedItem != null) {
                    if (screenAction.PurchasedItem is Consumable consumable) {
                        Write($" [{consumable.Price}, {string.Join(", ", (IEnumerable<Effect>) consumable.Effects)}]");
                    }
                }

                Console.WriteLine();

                i++;
            }
        }

        public static void GoTo(Screen screen)
        {
            CurrentScreen = screen;

            Console.Clear();

            WriteLine("DEBUG:", ConsoleColor.DarkGray);
            WriteLine($"Current screen name: {screen.GetType().Name}", ConsoleColor.DarkGray);
            WriteLine($"Screen's previous action ID: {screen.PreviousActionId}", ConsoleColor.DarkGray);
            WriteLine($"Player stats: STR {Player.Strength}, DEF {Player.Defense}, AGI {Player.Agility}, LUC {Player.Luck}", ConsoleColor.DarkGray);
            Console.WriteLine();

            screen.OnShow();

            if (!string.IsNullOrEmpty(ErrorString)) {
                WriteLine(ErrorString.Trim(), ConsoleColor.Red);
                WriteLine(new string('-', Console.BufferWidth), ConsoleColor.DarkGray);
                ErrorString = null;
            }

            if (ActionAfterGoto != null) {
                ActionAfterGoto();
                ActionAfterGoto = null;
            }

            Console.WriteLine($"AcquiredItems.Length: {AcquiredItems?.Length ?? -1}");
            if (AcquiredItems != null && AcquiredItems.Length > 0) {
                Write($"Приобретено {string.Join(", ", AcquiredItems.Select(x => x.Name))}.", ConsoleColor.Magenta);

                if (AcquiredItems.Length == 1) {
                    if (AcquiredItems[0] is Consumable) {
                        Write(" Использовать?", ConsoleColor.Magenta);
                        Write(" [E]", ConsoleColor.Cyan);
                    } else if (AcquiredItems[0] is Wearable) {
                        Write(" Надеть?", ConsoleColor.Magenta);
                        Write(" [E]", ConsoleColor.Cyan);
                    }
                }

                Console.WriteLine();

                AcquiredItems = null;
            }

            if (screen.ShouldRenderStats) RenderStats();

            if (screen is DialogueScreen dialogueScreen) {
                GoTo_DialogueScreen(dialogueScreen);
            } else {
                GoTo_RegularScreen(screen);
            }
        }

        private static void GoTo_RegularScreen(Screen screen)
        {
            if (!string.IsNullOrWhiteSpace(screen.Text)) Console.WriteLine(screen.Text.Trim());
            RenderActions(CurrentScreen?.Actions);

            Write("> ", ConsoleColor.Cyan);
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int result)) {
                int actionID = result - 1;

                if (actionID < 0 || actionID >= screen.Actions.Count) {
                    ErrorString = "This action does not exist!";
                    GoTo(CurrentScreen);
                }

                var action = screen.Actions[actionID];
                action.Screen = screen;
                var screenToGoTo = action.Execute(result - 1);
                GoTo(screenToGoTo);
            } else if (string.IsNullOrWhiteSpace(input)) {
                GoTo(CurrentScreen);
            } else {
                ErrorString = $"Invalid input: {input}\nTry again.";
                GoTo(CurrentScreen);
            }
        }

        private static void GoTo_DialogueScreen(DialogueScreen screen)
        {
            foreach (var (line, isPlayerLine, isTechnical) in screen.DialogueHistory) {
                WriteLine(line, isPlayerLine ? ConsoleColor.Gray : isTechnical ? ConsoleColor.DarkGray : ConsoleColor.White);
            }

            var currentPart = screen.Dialogues[screen.CurrentPartId];

            var currentLine = currentPart.Line?.Invoke();
            if (!string.IsNullOrWhiteSpace(currentLine)) {
                WriteLine(currentLine, currentPart.IsTechnical?.Invoke() ?? false ? ConsoleColor.DarkGray : ConsoleColor.White);
                screen.DialogueHistory.Add((currentLine, false, currentPart.IsTechnical?.Invoke() ?? false));
            }

            var currentPartType = currentPart.Type?.Invoke() ?? DialoguePart.PartType.Actions;
            if (currentPartType == DialoguePart.PartType.Actions) {
                RenderActions(currentPart.Actions);
                Write("> ", ConsoleColor.Cyan);
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int result)) {
                    int actionID = result - 1;

                    if (actionID < 0 || actionID >= currentPart.Actions.Count) {
                        ErrorString = "This action does not exist!";
                        GoTo(CurrentScreen);
                    }

                    var action = currentPart.Actions[actionID];
                    action.Screen = screen;

                    (string line, bool isPlayerLine, bool isTechnical) historyEntry = ("", true, false);
                    var dialogue = action.Dialogue?.Invoke();
                    if (string.IsNullOrWhiteSpace(dialogue)) {
                        historyEntry.line = $"\"{dialogue}\"";
                    } else {
                        historyEntry.line = action.Description?.Invoke() ?? "";
                    }

                    screen.DialogueHistory.Add(historyEntry);

                    action.Screen = screen;

                    var jumpTo2 = currentPart.JumpTo?.Invoke() ?? screen.CurrentPartId + 1;
                    if (jumpTo2 >= screen.Dialogues.Count) {
                        GoTo(screen.NextScreen?.Invoke() ?? CurrentScreen);
                    } else {
                        screen.CurrentPartId = jumpTo2;
                        GoTo(CurrentScreen);
                    }

                    var screenToGoTo = action.Execute(result - 1);
                    GoTo(screenToGoTo);
                    // TODO Line 242-248. action.Execute does not take that into account, it just GoTos to another point.
                } else if (string.IsNullOrWhiteSpace(input)) {
                    GoTo(CurrentScreen);
                } else {
                    ErrorString = $"Invalid input: {input}\nTry again.";
                    GoTo(CurrentScreen);
                }
            } else if (currentPartType == DialoguePart.PartType.InputText) {
                Write("> ", ConsoleColor.Cyan);
                var text = Console.ReadLine();

                (string line, bool isPlayerLine, bool isTechnical) historyEntry = ($"{Player.Name}: {text}", true, false);
                screen.DialogueHistory.Add(historyEntry);

                currentPart.ActionOnInput?.Invoke(text);
            }

            var jumpTo = currentPart.JumpTo?.Invoke() ?? screen.CurrentPartId + 1;
            if (jumpTo >= screen.Dialogues.Count) {
                GoTo(screen.NextScreen?.Invoke() ?? CurrentScreen);
            } else {
                screen.CurrentPartId = jumpTo;
                GoTo(CurrentScreen);
            }
        }

        private static void GoTo_AddDialogueHistory(DialogueScreen screen, ScreenAction action)
        {
            (string line, bool isPlayerLine, bool isTechnical) historyEntry = ("", true, false);
            var dialogue = action.Dialogue?.Invoke();
            if (string.IsNullOrWhiteSpace(dialogue)) {
                historyEntry.line = $"\"{dialogue}\"";
            } else {
                historyEntry.line = action.Description?.Invoke() ?? "";
            }

            screen.DialogueHistory.Add(historyEntry);
        }

        public static void Write(string s, ConsoleColor? fc = null, ConsoleColor? bc = null)
        {
            Console.ResetColor();
            if (fc != null) Console.ForegroundColor = (ConsoleColor) fc;
            if (bc != null) Console.BackgroundColor = (ConsoleColor) bc;
            Console.Write(s);
            Console.ResetColor();
        }

        public static void WriteLine(string s, ConsoleColor? fc = null, ConsoleColor? bc = null)
        {
            Write(s, fc, bc);
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;

namespace RpgGameCore2.Screens.Intro
{
    public class Intro5DScreen : DialogueScreen
    {
        public override List<ScreenAction> Actions { get; }
        public override string LocationName { get; set; }
        public override string Text { get; set; }
        public override bool ShouldRenderStats { get; } = true;
        
        public override List<DialoguePart> Dialogues { get; }
        public override Func<Screen> NextScreen { get; set; } = () => Game.Screens.MainSquare;

        public Intro5DScreen()
        {
            Dialogues = new List<DialoguePart> {
                new DialoguePart {
                    Line = () => "Аргард: Ну, пьяницу каждый побить может. Главное, чтобы он тебя не побил! Кстати, как тебя звать-то?",
                    Type = () => DialoguePart.PartType.InputText,
                    ActionOnInput = delegate(string name) {
                        Game.Player.Name = name ?? "Незнакомец";
                    }
                },
                new DialoguePart {
                    Line = () => "Аргард: Ладно, в общем, теперь ты сам по себе. Я тебе всё показал, что мог. Если что, обращайся.",
                    Type = () => DialoguePart.PartType.Actions,
                    Actions = new List<ScreenAction> {
                        new ScreenAction {
                            Dialogue = () => "Хорошо!"
                        }
                    }
                }
            };
        }
    }
}


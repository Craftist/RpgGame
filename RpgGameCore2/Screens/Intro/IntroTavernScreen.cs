using System.Collections.Generic;
using RpgGameCore2.Data.Consumables.Drinks;

namespace RpgGameCore2.Screens.Intro
{
    public class IntroTavernScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "Таверна";
        public override string Text { get; set; } = "Тавернщик: Привет! Всегда рад новым посетителям. Что желаешь?";

        public override List<ScreenAction> Actions { get; }

        public IntroTavernScreen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => "Эль",
                    PurchasedItem = new Ale()
                },
                new ScreenAction {
                    Description = () => "Виски",
                    PurchasedItem = new Whiskey()
                },
                new ScreenAction {
                    Dialogue = () => "Пожалуй откажусь, я пока просто осматриваюсь.",
                    BeforeAction = () => Game.Screens.Intro2.Actions.RemoveAt(PreviousActionId),
                    NextScreen = () => Game.Screens.Intro2
                }
            };
        }
    }
}


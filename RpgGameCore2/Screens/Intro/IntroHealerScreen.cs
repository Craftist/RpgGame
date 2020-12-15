using System.Collections.Generic;
using RpgGameCore2.Data.Consumables.Potions;

namespace RpgGameCore2.Screens.Intro
{
    public class IntroHealerScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "У Лекарни";
        public override string Text { get; set; } = "Лекарня: Приветствую тебя, странник. Желаешь подлечиться?";

        public override List<ScreenAction> Actions { get; }

        public IntroHealerScreen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => "Малое зелье здоровья",
                    PurchasedItem = new SmallHealingPotion()
                },
                new ScreenAction {
                    Description = () => "Среднее зелье здоровья",
                    PurchasedItem = new MediumHealingPotion()
                },
                new ScreenAction {
                    Description = () => "Большое зелье здоровья",
                    PurchasedItem = new BigHealingPotion()
                },
                new ScreenAction {
                    Dialogue = () => "Спасибо, я просто осматриваюсь.",
                    BeforeAction = () => Game.Screens.Intro2.Actions.RemoveAt(PreviousActionId),
                    NextScreen = () => Game.Screens.Intro2
                }
            };
        }
    }
}


using System.Collections.Generic;
using RpgGameCore2.Data;
using RpgGameCore2.Data.Consumables.Drinks;

namespace RpgGameCore2.Screens
{
    public class TavernScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "Таверна";
        public override string Text { get; set; } = "Тавернщик: Привет! Всегда рад новым посетителям. Что желаешь?";

        public override List<ScreenAction> Actions { get; }
        
        public readonly List<InventoryItem> AvailableProducts = new List<InventoryItem> {
            new Ale(),
            new Whiskey()
        };

        public override void OnShow()
        {
            Actions.Clear();
            foreach (var availableProduct in AvailableProducts) {
                Actions.Add(new ScreenAction {
                    Description = () => availableProduct.Name,
                    PurchasedItem = availableProduct
                });
            }
            Actions.Add(new ScreenAction {
                Description = () => "Вернуться на площадь",
                NextScreen = () => Game.Screens.MainSquare
            });
        }

        public TavernScreen()
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


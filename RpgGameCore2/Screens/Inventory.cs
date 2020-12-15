using System;
using System.Collections.Generic;
using System.Resources;
using RpgGameCore2.Util;

namespace RpgGameCore2.Screens
{
    public class InventoryScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string Text { get; set; } = "";
        public override string LocationName { get; set; }

        public override List<ScreenAction> Actions { get; }

        public override void OnShow()
        {
            Actions.Clear();
            foreach (var inventoryItem in Game.Player.Inventory) {
                Actions.Add(new ScreenAction {
                    Description = () => $"{inventoryItem.Name}\n{Utils.ConsoleColorStr.Reset}{inventoryItem}"
                });
            }

            Actions.Add(new ScreenAction {
                Description = () => "Назад",
                NextScreen = () => Game.Screens.MainSquare
            });
        }

        public InventoryScreen()
        {
            Actions = new List<ScreenAction>();
        }
    }
}

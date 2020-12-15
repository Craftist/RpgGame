using System;
using System.Collections.Generic;
using RpgGameCore2.Data;
using RpgGameCore2.Data.Creatures;

namespace RpgGameCore2.Screens
{
    public abstract class Screen
    {
        public abstract List<ScreenAction> Actions { get; }
        public abstract string LocationName { get; set; }
        public abstract string Text { get; set; }
        public abstract bool ShouldRenderStats { get; }
        public int PreviousActionId = -1;

        /// <summary>
        /// Gets called every time the screen is shown.
        /// </summary>
        public virtual void OnShow() { }
    }
    
    public class CheatScreenAction : ScreenAction { }

    public class ScreenAction
    {
        public Screen Screen;
        
        public Func<string> Description, Dialogue;
        public Func<int> GoldPrice;
        public Func<int> SilverPrice;
        public Func<int> DiamondsPrice;
        public Func<float> EnergyPrice;
        
        /// <summary>
        /// If true, does not go to the same screen in case NextScreen is not supplied.
        /// Defaults to true.
        /// </summary>
        public Func<bool> ShouldRedraw;

        /// <summary>
        /// If not, upon action the player will attack the specified creature.
        /// </summary>
        public Func<Creature> AttackTarget;
        public Action<FightResult> ActionAfterFight;

        /// <summary>
        /// Screen that will be next after performing the action. If null, no screen will be changed (the current screen will still be redrawn).
        /// </summary>
        public Func<Screen> NextScreen;

        /// <summary>
        /// Action that is performed BEFORE moving to the next screen. If null, it will not be executed.
        /// </summary>
        public Action BeforeAction;

        /// <summary>
        /// Action that is performed AFTER moving to the next screen. If null, it will not be executed.
        /// </summary>
        public Action AfterAction;

        /// <summary>
        /// If null, then represents the item that is to be purchased. If consumable/equippable, on the next screen the prompt to consume/equip the item will be shown.
        /// </summary>
        public InventoryItem PurchasedItem;

        public Screen Execute(int actionID, Screen nextScreenToGoTo = null)
        {
            bool success = true;

            if (EnergyPrice?.Invoke() > 0) {
                if (Game.Player.CurrentEnergy < EnergyPrice?.Invoke()) {
                    Game.ErrorString += "\n\nNot enough energy to do this ";
                    success = false;
                } else {
                    Game.Player.CurrentEnergy -= EnergyPrice?.Invoke() ?? 0;
                }
            }

            if (SilverPrice?.Invoke() > 0) {
                if (Game.Player.Silver < SilverPrice?.Invoke()) {
                    Game.ErrorString += "\n\nNot enough silver to do this ";
                    success = false;
                } else {
                    Game.Player.Silver -= SilverPrice?.Invoke() ?? 0;
                }
            }

            if (GoldPrice?.Invoke() > 0) {
                if (Game.Player.Gold < GoldPrice?.Invoke()) {
                    Game.ErrorString += "\n\nNot enough gold to do this ";
                    success = false;
                } else {
                    Game.Player.Gold -= GoldPrice?.Invoke() ?? 0;
                }
            }

            if (DiamondsPrice?.Invoke() > 0) {
                if (Game.Player.Diamonds < DiamondsPrice?.Invoke()) {
                    Game.ErrorString += "\n\nNot enough diamonds to do this ";
                    success = false;
                } else {
                    Game.Player.Diamonds -= DiamondsPrice?.Invoke() ?? 0;
                }
            }

            if (!success) {
                return Game.CurrentScreen;
            }

            var nextScreen = nextScreenToGoTo ?? NextScreen?.Invoke();

            if (nextScreen != null) {
                nextScreen.PreviousActionId = actionID;
            }

            BeforeAction?.Invoke();
            if (AfterAction != null) Game.ActionAfterGoto = AfterAction;

            if (AttackTarget != null) {
                // Engage in attack with this enemy
                var fightScreen = new FightScreen(AttackTarget.Invoke());
                fightScreen.ActionAfterFight = ActionAfterFight;
                return fightScreen;
            }

            if (PurchasedItem != null) {
                bool isBought = true;

                if (PurchasedItem.Price.Silver > Game.Player.Silver) {
                    Game.ErrorString += $"\n\nNot enough silver to purchase {PurchasedItem.Name}.";
                    isBought = false;
                } else {
                    Game.Player.Silver -= PurchasedItem.Price.Silver;
                }

                if (PurchasedItem.Price.Gold > Game.Player.Gold) {
                    Game.ErrorString += $"\n\nNot enough gold to purchase {PurchasedItem.Name}.";
                    isBought = false;
                } else {
                    Game.Player.Gold -= PurchasedItem.Price.Gold;
                }

                if (PurchasedItem.Price.Diamonds > Game.Player.Diamonds) {
                    Game.ErrorString += $"\n\nNot enough diamonds to purchase {PurchasedItem.Name}.";
                    isBought = false;
                } else {
                    Game.Player.Diamonds -= PurchasedItem.Price.Diamonds;
                }

                if (isBought) {
                    Game.AddItem(PurchasedItem);
                }
            }

            if (nextScreen != null) {
                return nextScreen;
            }

            if (ShouldRedraw?.Invoke() ?? true) {
                return Game.CurrentScreen;
            }

            return null;
        }
    }
}

using System.Collections.Generic;
using RpgGameCore2.Util;

namespace RpgGameCore2.Screens.Intro
{
    public class IntroShuScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "У Мастера Шу";
        public override string Text { get; set; } = "Мастер Шу: Приветствую тебя, заблудшая душа. Что будешь тренировать?";

        public override List<ScreenAction> Actions { get; }

        public IntroShuScreen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => $"Сила: {Game.Player.Strength} -> {Game.Player.Strength + 1} [{Calculations.StrengthUpgradePrice(Game.Player.Strength)}]",
                    SilverPrice = () => Calculations.StrengthUpgradePrice(Game.Player.Strength).Silver,
                    GoldPrice = () => Calculations.StrengthUpgradePrice(Game.Player.Strength).Gold,
                    DiamondsPrice = () => Calculations.StrengthUpgradePrice(Game.Player.Strength).Diamonds,
                    BeforeAction = () => Game.Player.Strength++
                },
                new ScreenAction {
                    Description = () => $"Защита: {Game.Player.Defense} -> {Game.Player.Defense + 1} [{Calculations.DefenseUpgradePrice(Game.Player.Defense)}]",
                    SilverPrice = () => Calculations.DefenseUpgradePrice(Game.Player.Defense).Silver,
                    GoldPrice = () => Calculations.DefenseUpgradePrice(Game.Player.Defense).Gold,
                    DiamondsPrice = () => Calculations.DefenseUpgradePrice(Game.Player.Defense).Diamonds,
                    BeforeAction = () => Game.Player.Defense++
                },
                new ScreenAction {
                    Description = () => $"Умение: {Game.Player.Agility} -> {Game.Player.Agility + 1} [{Calculations.AgilityUpgradePrice(Game.Player.Agility)}]",
                    SilverPrice = () => Calculations.AgilityUpgradePrice(Game.Player.Agility).Silver,
                    GoldPrice = () => Calculations.AgilityUpgradePrice(Game.Player.Agility).Gold,
                    DiamondsPrice = () => Calculations.AgilityUpgradePrice(Game.Player.Agility).Diamonds,
                    BeforeAction = () => Game.Player.Agility++
                },
                new ScreenAction {
                    Description = () => $"Удача: {Game.Player.Luck} -> {Game.Player.Luck + 1} [{Calculations.LuckUpgradePrice(Game.Player.Luck)}]",
                    SilverPrice = () => Calculations.LuckUpgradePrice(Game.Player.Luck).Silver,
                    GoldPrice = () => Calculations.LuckUpgradePrice(Game.Player.Luck).Gold,
                    DiamondsPrice = () => Calculations.LuckUpgradePrice(Game.Player.Luck).Diamonds,
                    BeforeAction = () => Game.Player.Luck++
                },
                new ScreenAction {
                    Dialogue = () => "Я подумаю над вашим предложением, а пока я, наверное, пойду.",
                    BeforeAction = () => Game.Screens.Intro2.Actions.RemoveAt(PreviousActionId),
                    NextScreen = () => Game.Screens.Intro2
                }
            };
        }
    }
}


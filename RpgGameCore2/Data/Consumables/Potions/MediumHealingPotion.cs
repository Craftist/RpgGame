using RpgGameCore2.Util;

namespace RpgGameCore2.Data.Consumables.Potions
{
    public class MediumHealingPotion : Consumable
    {
        public override Effect[] Effects { get; } = {
            new Effect {
                Type = EffectType.Add,
                Target = EffectTarget.CurrentHealth,
                Amount = 25,
                Duration = 0
            },
            new Effect {
                Type = EffectType.Add,
                Target = EffectTarget.CurrentHealth,
                Amount = 1,
                IsRepeated = true,
                Duration = 30,
                Interval = 3
            }
        };

        public override string Name { get; } = "Среднее зелье здоровья";
        public override string Description { get; } = "";
        public override SGD Price { get; } = new SGD(30);
        public override Rarity Rarity { get; } = Rarity.Common;
    }
}

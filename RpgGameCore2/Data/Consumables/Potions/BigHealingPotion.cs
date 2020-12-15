using RpgGameCore2.Util;

namespace RpgGameCore2.Data.Consumables.Potions
{
    public class BigHealingPotion : Consumable
    {
        public override Effect[] Effects { get; } = {
            new Effect {
                Type = EffectType.Add,
                Target = EffectTarget.CurrentHealth,
                Amount = 100,
                Duration = 0
            },
            new Effect {
                Type = EffectType.Add,
                Target = EffectTarget.CurrentHealth,
                Amount = 1,
                IsRepeated = true,
                Duration = 60,
                Interval = 1
            }
        };

        public override string Name { get; } = "Среднее зелье здоровья";
        public override string Description { get; } = "";
        public override SGD Price { get; } = new SGD(100);
        public override Rarity Rarity { get; } = Rarity.Common;
    }
}

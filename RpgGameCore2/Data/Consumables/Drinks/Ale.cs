using RpgGameCore2.Util;

namespace RpgGameCore2.Data.Consumables.Drinks
{
    public class Ale : Consumable
    {
        public override Effect[] Effects { get; } = {
            new Effect {
                Type = EffectType.Multiply,
                Target = EffectTarget.Strength,
                Amount = 1.1m,
                Duration = 3 * 60
            },
            new Effect {
                Type = EffectType.Multiply,
                Target = EffectTarget.Agility,
                Amount = -1.1m,
                Duration = 3 * 60
            }
        };

        public override string Name { get; } = "Эль";
        public override string Description { get; } = "Напиток богов. Увеличивает силу и уменьшает ловкость на 3 минуты.";
        public override SGD Price { get; } = new SGD(5);
        public override Rarity Rarity { get; } = Rarity.Common;
    }
}

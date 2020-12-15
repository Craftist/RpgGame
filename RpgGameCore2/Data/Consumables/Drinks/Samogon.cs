using RpgGameCore2.Util;

namespace RpgGameCore2.Data.Consumables.Drinks
{
    public class Samogon : Consumable
    {
        public override Effect[] Effects { get; } = {
            new Effect {
                Target = EffectTarget.Defense,
                Type = EffectType.Multiply,
                Amount = 2281488,
                Duration = Effect.GetDuration(hours: 1)
            }
        };

        public override string Name { get; } = "Самогон";
        public override string Description { get; } = "Самогон твоего бати. Делает тебя неубиваемым на целый час.";
        public override SGD Price { get; } = new SGD(diamonds: 100500);
        public override Rarity Rarity { get; } = Rarity.Godlike;
    }
}

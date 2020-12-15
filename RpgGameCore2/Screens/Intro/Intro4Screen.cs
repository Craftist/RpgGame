using System.Collections.Generic;

namespace RpgGameCore2.Screens.Intro
{
    public class Intro4Screen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "Таверна";
        public override string Text { get; set; } = "Заведующий таверной: Привет! Всегда рад новым посетителям. Что желаешь?";

        public override List<ScreenAction> Actions { get; }

        public Intro4Screen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Description = () => "Ударить пьяницу",
                    AttackTarget = () => Game.NamedCreatures["IntroDrunk"],
                    ActionAfterFight = delegate {
                        Game.GoTo(Game.Screens.Intro5);
                    }
                }
            };
        }
    }
}


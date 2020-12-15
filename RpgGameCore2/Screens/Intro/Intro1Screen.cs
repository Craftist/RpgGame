using System.Collections.Generic;

namespace RpgGameCore2.Screens.Intro
{
    public class Intro1Screen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "";
        public override string Text { get; set; } = "Аргард: Привет, я вижу, ты новенький. Давай, покажу тебе, что да как.";

        public override List<ScreenAction> Actions { get; } = new List<ScreenAction> {
            new ScreenAction {
                Dialogue = () => "Давай.",
                NextScreen = () => Game.Screens.Intro2
            }
        };
    }
}

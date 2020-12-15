using System.Collections.Generic;

namespace RpgGameCore2.Screens
{
    public class MainMenuScreen : Screen
    {
        public override bool ShouldRenderStats { get; } = false;
        public override string LocationName { get; set; } = "";
        public override string Text { get; set; } = "RpgGame v0.1";

        public override List<ScreenAction> Actions { get; } = new List<ScreenAction> {
            new ScreenAction {
                Description = () => "Start a new game",
                NextScreen = () => Game.Screens.Intro1
            },
            new ScreenAction {
                Description = () => "Start a new game and skip intro",
                NextScreen = () => Game.Screens.MainSquare
            },
            new ScreenAction {
                Description = () => "Exit 🚪",
                BeforeAction = () => System.Environment.Exit(0)
            }
        };
    }
}

using System.Collections.Generic;

namespace RpgGameCore2.Screens.Intro
{
    public class Intro3Screen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;
        public override string LocationName { get; set; } = "";
        public override string Text { get; set; } = "Аргард: Теперь я хочу посмотреть, каков ты в бою. Пойдём в таверну, ударишь какого-нибудь пьяницу.";

        public override List<ScreenAction> Actions { get; }

        public Intro3Screen()
        {
            Actions = new List<ScreenAction> {
                new ScreenAction {
                    Dialogue = () => "Пойдём!",
                    NextScreen = () => Game.Screens.Intro4
                }
            };
        }
    }
}


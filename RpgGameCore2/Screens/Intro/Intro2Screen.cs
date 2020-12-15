using System.Collections.Generic;

namespace RpgGameCore2.Screens.Intro
{
    public class Intro2Screen : Screen
    {
        public override bool ShouldRenderStats { get; } = true;

        public override string LocationName { get; set; } = "";
        public override string Text { get; set; } = @"Аргард: В Легионе есть множество мест для развлечения.
В Таверне можно подкрепиться или подраться.
У Лекарши можно подлечиться.
У Мастера Шу можно потренироваться.
Мы не можем позволить всем новеньким шикарные апартаменты, поэтому вот тот дом - твой. Как разживёшься, сможешь купить более достойное жилище.";
        
        public override void OnShow()
        {
            if (Actions.Count == 0) {
                Game.GoTo(Game.Screens.Intro3);
            }
        }

        public override List<ScreenAction> Actions { get; } = new List<ScreenAction> {
            new ScreenAction {
                Description = () => "\"Понятно. Давай начнём с таверны.\"",
                NextScreen = () => Game.Screens.IntroTavern
            },
            new ScreenAction {
                Description = () => "\"Понятно. Давай начнём с Лекарши.\"",
                NextScreen = () => Game.Screens.IntroHealer
            },
            new ScreenAction {
                Description = () => "\"Понятно. Давай начнём с Мастера Шу.\"",
                NextScreen = () => Game.Screens.IntroShu
            }
        };
    }
}

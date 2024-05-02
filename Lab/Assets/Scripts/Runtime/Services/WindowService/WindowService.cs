using Runtime.Infrastructure.Factories;
using Runtime.Logic.UI;
using UniRx;

namespace Runtime.Services.WindowService
{
    public enum WindowType
    {
        Menu,
        GameOver
    }

    public sealed class WindowService : IWindowService
    {
        public ReactiveProperty<BaseWindow> ActiveWindow { get; } = new();

        private readonly UIFactory _uiFactory;
        
        public WindowService(UIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowType windowType)
        {
            if (ActiveWindow.Value != null) 
                ActiveWindow.Value.Close();

            ActiveWindow.Value = _uiFactory.CreateWindow(windowType);
            ActiveWindow.Value.Open();
        }

        public void OpenGameOver(bool isWin)
        {
            Open(WindowType.GameOver);
            ((GameOverWindowDisplay)ActiveWindow.Value)?.SetStatus(isWin);
        }
    }
}
using UniRx;

namespace Runtime.Services.WindowService
{
    public interface IWindowService
    {
        public ReactiveProperty<BaseWindow> ActiveWindow { get; }
        
        public void Open(WindowType windowType);
        public void OpenGameOver(bool isWin);
    }
}
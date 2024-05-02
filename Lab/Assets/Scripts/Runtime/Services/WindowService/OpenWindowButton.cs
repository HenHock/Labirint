using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Services.WindowService
{
    [RequireComponent(typeof(Button))]
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private WindowType m_WindowType;

        private IWindowService _windowService;
        
        [Inject]
        private void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Start() =>
            GetComponent<Button>().OnClickAsObservable()
                .Subscribe(_ => _windowService.Open(m_WindowType))
                .AddTo(this);
    }
}
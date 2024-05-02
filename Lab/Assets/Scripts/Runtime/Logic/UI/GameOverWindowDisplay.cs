using Runtime.Infrastructure.Bootstrap.BootStateMachine;
using Runtime.Infrastructure.Bootstrap.BootStateMachine.StateFactory;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine;
using Runtime.Infrastructure.Bootstrap.ScenesStateMachine.States;
using Runtime.Services.PauseService;
using Runtime.Services.SaveSystem.ProgressService;
using Runtime.Services.SceneLoader;
using Runtime.Services.WindowService;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Logic.UI
{
    public class GameOverWindowDisplay : BaseWindow
    {
        private const string WinStatus = "You win!";
        private const string LoseStatus = "You lose!";
        
        [SerializeField] private TextMeshProUGUI m_StatusTextFiled;
        [SerializeField] private Button m_RestartButton;
        
        private IPauseService _pauseService;
        private IPersistentProgressService _progressService;
        private IStateMachine _sceneStateMachine;

        [Inject]
        private void Construct(SceneStateMachine sceneStateMachine, IPauseService pauseService, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _pauseService = pauseService;
            _sceneStateMachine = sceneStateMachine;
        }

        private void Start() =>
            m_RestartButton.OnClickAsObservable()
                .Subscribe(RestartLevel)
                .AddTo(this);

        public override void Open()
        {
            _pauseService.Pause();
            _progressService.Progress.m_StatsData.m_TotalAttemptCount.Value++;
        }

        public override void Close()
        {
            _pauseService.Play();
            base.Close();
        }

        public void SetStatus(bool isWin)
        {
            m_StatusTextFiled.text = isWin ? WinStatus : LoseStatus;
            
            if (isWin)
                _progressService.Progress.m_StatsData.m_WinCount.Value++;
            else _progressService.Progress.m_StatsData.m_LoseCount.Value++;
        }

        private void RestartLevel(Unit _) => _sceneStateMachine.Enter<LoadGameplayState>();
    }
}
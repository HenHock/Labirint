using System;
using Cysharp.Threading.Tasks;
using Runtime.Services.PauseService;
using Runtime.Services.SaveSystem.SaveLoadService;
using Runtime.Services.WindowService;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Logic.UI
{
    public class MenuWindowDisplay : BaseWindow
    {
        [SerializeField] private Button m_SaveButton;
        [SerializeField] private Button m_LoadButton;
        
        private ISaveLoadService _saveLoadService;
        private IPauseService _pauseService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            m_SaveButton.OnClickAsObservable()
                .Subscribe(_ => _saveLoadService.ProgressWriterUpdates())
                .AddTo(this);

            m_LoadButton.OnClickAsObservable()
                .Subscribe(_ => _saveLoadService.InformProgressReaders())
                .AddTo(this);
        }

        public override void Open() => _pauseService.Pause();

        public override void Close()
        {
            _pauseService.Play();
            base.Close();
        }
    }
}
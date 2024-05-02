using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Services.WindowService
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] protected Button m_CloseButton;

        protected virtual void Awake() =>
            m_CloseButton.OnClickAsObservable()
                .Subscribe(_ => Close());

        public abstract void Open();

        public virtual void Close() =>
            Destroy(gameObject);
    }
}
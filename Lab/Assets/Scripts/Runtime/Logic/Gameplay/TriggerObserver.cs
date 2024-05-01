using UniRx;
using UnityEngine;

namespace Runtime.Logic.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public ReactiveCommand<Collider> OnEnter { get; } = new();
        public ReactiveCommand<Collider> OnStay { get; } = new();
        public ReactiveCommand<Collider> OnExit { get; } = new();

        private void OnTriggerEnter(Collider other) => OnEnter?.Execute(other);

        private void OnTriggerStay(Collider other) => OnStay?.Execute(other);

        private void OnTriggerExit(Collider other) => OnExit?.Execute(other);
    }
}

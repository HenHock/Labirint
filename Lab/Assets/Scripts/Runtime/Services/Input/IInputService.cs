using UniRx;
using UnityEngine;

namespace Runtime.Services.Input
{
    public interface IInputService
    {
        public ReactiveCommand<Vector2> OnLeftClickDown { get; }
        public ReactiveCommand<Vector2> OnLeftClickUp { get; }
        public ReactiveCommand<Vector2> OnLeftClickDrag { get; }
    }
}

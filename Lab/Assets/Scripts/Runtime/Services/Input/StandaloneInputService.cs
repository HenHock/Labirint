using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Services.Input
{
    public sealed class StandaloneInputService : IInputService
    {
        public Vector2 Move => _defaultInputActions.Player.Move.ReadValue<Vector2>();
        
        public ReactiveCommand<Vector2> OnLeftClickDown { get; } = new();
        public ReactiveCommand<Vector2> OnLeftClickUp { get; } = new();
        public ReactiveCommand<Vector2> OnLeftClickDrag { get; } = new();

        private readonly DefaultInputActions _defaultInputActions;

        public StandaloneInputService()
        {
            _defaultInputActions = new DefaultInputActions();
            EnableInputs();
            
            RegisterDownHandler();
            RegisterDragHandler();
            RegisterUpHandler();
        }

        public void EnableInputs() => _defaultInputActions.Enable();
        public void DisableInputs() => _defaultInputActions.Disable();

        private void RegisterUpHandler() =>
            Observable.EveryUpdate()
                .Where(_ => Mouse.current.leftButton.wasPressedThisFrame)
                .Subscribe(OnLeftClickUpHandler);

        private void RegisterDragHandler() =>
            Observable.EveryUpdate()
                .Where(_ => Mouse.current.leftButton.isPressed)
                .Subscribe(OnLeftClickDragHandler);

        private void RegisterDownHandler() =>
            Observable.EveryUpdate()
                .Where(_ => Mouse.current.leftButton.wasPressedThisFrame)
                .Subscribe(OnLeftClickDownHandler);

        private void OnLeftClickDragHandler(long _) => 
            OnLeftClickDrag?.Execute(Mouse.current.position.ReadValue());

        private void OnLeftClickUpHandler(long _) => 
            OnLeftClickUp?.Execute(Mouse.current.position.ReadValue());

        private void OnLeftClickDownHandler(long _) => 
            OnLeftClickDown?.Execute(Mouse.current.position.ReadValue());
    }
}

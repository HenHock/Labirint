using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public ReactiveCommand<Vector2> OnLeftClickDown { get; private set; } = new();
        public ReactiveCommand<Vector2> OnLeftClickUp { get; private set; } = new();
        public ReactiveCommand<Vector2> OnLeftClickDrag { get; private set; } = new();
        
        public StandaloneInputService()
        {
            RegisterDownHandler();
            RegisterDragHandler();
            RegisterUpHandler();
        }

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

namespace Runtime.Infrastructure.Bootstrap.GameStateMachine.States.Interfaces
{
    public interface IPayloadState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
namespace Runtime.Infrastructure.Bootstrap.BootStateMachine.States.Interfaces
{
    public interface IPayloadState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload = default);
    }
}
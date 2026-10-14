public class PlayerStateMachine
{
    private IPlayerState currentState;

    public void Initialize(IPlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState == newState)
            return;

        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
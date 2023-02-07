using System;

public class GameState
{
    public enum State
    {
        Lobby,
        Elevator,
        GeneratingLevel,
        InGame,
        Trading,
        Selling
    }

    private State currentState;

    public State CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            OnStateChanged?.Invoke(currentState);
        }
    }

    public event Action<State> OnStateChanged;

    public GameState()
    {
        CurrentState = State.Lobby;
    }
}
using System;

namespace Infrastructure.GameStates
{
    public interface IGameStatesEvents
    {
        event Action OnLevelStart;
        event Action OnPlayGame;
        event Action OnLevelLost;
        event Action StopGame;

   
    }
}
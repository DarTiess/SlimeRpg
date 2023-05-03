using System;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public interface IGameStatesEvents
    {
        event Action OnLevelStart;
        event Action OnPlayGame;
        event Action OnLevelLost;
        event Action StopGame;
        event Action<Transform> CreatePlane;
        event Action<int> EnemyDeath;


    }
}
using System;

namespace Player
{
    public interface IPlayerEvents
    {
        event Action Moving;
        event Action StopMoving;
        event Action Damaging;
    }
}
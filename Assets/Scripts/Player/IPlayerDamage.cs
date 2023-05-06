using UnityEngine;

namespace Player
{
    public interface IPlayerDamage
    {
        void TakeDamage(int damage, Transform enemy);
    }
}
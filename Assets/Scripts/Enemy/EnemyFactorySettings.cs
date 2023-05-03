using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "Data/EnemyFactory", fileName = "EnemyFactorySetting")]
    public class EnemyFactorySettings : ScriptableObject
    {
        public List<string> enemyKeys;
        public int enemyCount;
        public float _timerPooling;
    }
}
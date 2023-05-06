using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.LevelHelpers
{
    [CreateAssetMenu(menuName = "Data/SceneSettings", fileName = "SceneData")]
    public class SceneSetting : ScriptableObject
    {
        public List<string> scenes;
    }
}
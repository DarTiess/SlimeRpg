using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeRpg
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private LoadScene loadScene;
        private void Awake()
        {
            loadScene.StartGame();
        }
    }
}


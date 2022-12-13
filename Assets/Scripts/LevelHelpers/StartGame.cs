using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class StartGame : MonoBehaviour
    {
        [SerializeField] private LoadScene loadScene;
        private void Awake()
        {
            loadScene.StartGame();
        }
    }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        private PlayerAnimator _playerAnimator;
   
        private GameManager _gameManager;

        [Inject]
        private void Init(GameManager manager)
        {
            _gameManager = manager;
            _gameManager.OnPlayGame+= StartMove;
        }

      

        // Start is called before the first frame update
        void Start()
        {
            _playerAnimator= GetComponent<PlayerAnimator>();   


        }
        private void StartMove()
        {
            _playerAnimator.MoveAnimation();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }



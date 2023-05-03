using System;
using Infrastructure.GameStates;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        private float _speed;
        private IGameStatesEvents _gameStatesEvents;

        private bool _canMove;
        private float zPosition;
        private float yPosition;
    
        public void Init(float speed)
        {
            _speed = speed;
            zPosition = transform.position.z;
            yPosition = transform.position.y;
        }

        private void Update()
        {
            if (_canMove)
            {
                Vector3 move = Vector3.zero;
                move.z = _speed * Time.deltaTime;
                transform.Translate(move);
            }
        }
     
        public void StopMove()
        {
            _canMove = false;
        }

        public void StartMove()
        {
            _canMove = true;
            transform.position = new Vector3(transform.position.x, yPosition, zPosition);
        }

    }
}
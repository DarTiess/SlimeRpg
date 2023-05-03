﻿using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerBulletStack
    {
        private Bullet _ballPref;
        private int _countBalls;
        private Transform _pushBallPoint;
       private float _jumpDuration;
        private Transform _parentTransform;
        private List<Bullet> _ballList;
        private int _indexBall = 0;
        private int _attackPower;

        public PlayerBulletStack(Bullet ballPref, 
                                 int countBalls, 
                                 Transform pushBallPoint, 
                                float jumpDuration, 
                                 Transform originParent,
                                 int attackPower)
        {
            _ballPref = ballPref;
            _countBalls = countBalls;
            _pushBallPoint = pushBallPoint;
            _jumpDuration = jumpDuration;
            _parentTransform = originParent;
            _attackPower = attackPower;
            _ballList = new List<Bullet>(_countBalls);
            CreateBulletsList();
        }

        private void CreateBulletsList()
        {
            for (int i = 0; i < _countBalls; i++)
            {
                Bullet bullet = Object.Instantiate(_ballPref, _pushBallPoint.transform.position, _pushBallPoint.transform.rotation);
                bullet.Init(_parentTransform, _attackPower);
                
                _ballList.Add(bullet);
            }
        }
        public void PushBullet(Transform target)
        {
            _ballList[_indexBall].Push(target, _pushBallPoint);
            _indexBall++;
                               if (_indexBall >= _ballList.Count)
                               {
                                   _indexBall = 0;
                               }
        }
    }
}
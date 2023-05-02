using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.GameStates;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
//[RequireComponent(typeof(PlayerUpgradeService))]
    public class Player : MonoBehaviour, IPlayerEvents
{
        public event Action Moving;
        public event Action StopMoving;
        public event Action Damaging;
        
        [Header("Move Settings")]
        [SerializeField] private float _speed;
        [Header("Pushing Balls Settings")]
        [SerializeField] private GameObject _ballPref;
        [SerializeField] private int _countBalls;
        [SerializeField] private Transform _pushBallPoint;
        [SerializeField] private float _jumpForce;
        [SerializeField] public float _jumpDuration;
        [Header("Attack Settings")]
        [SerializeField] private int _attackPower;
        [SerializeField] private float _radiusDetectEnemy;
        
        private List<GameObject> _ballList;
        private int _indexBall = 0;
        private bool _isOnAtack;

        private PlayerAnimator _playerAnimator;
        //  private PlayerUpgradeService _playerUpgradeService;

        private IGameStatesEvents _gameStatesEvents;

        private bool _canMove;
        private float zPosition;
        private float yPosition;

        void Update()
        {
            AttackEnemy(gameObject.transform.position, _radiusDetectEnemy);
            if (_canMove)
            {
                Vector3 move = Vector3.zero;
                move.z = _speed * Time.deltaTime;
                transform.Translate(move);
            }
        }

        private void OnDisable()
        {
            _gameStatesEvents.OnPlayGame -= StartMove;
            _gameStatesEvents.StopGame -= StopMove;
        }

        public void Init(IGameStatesEvents statesEvents)
        {
            _gameStatesEvents = statesEvents;
            _gameStatesEvents.OnPlayGame += StartMove;
            _gameStatesEvents.StopGame += StopMove;
           
            _ballList = new List<GameObject>(_countBalls);
            SetBallToStack();
       
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerAnimator.Init(this);
            // _playerUpgradeService = GetComponent<PlayerUpgradeService>();
            // _playerUpgradeService.InitPlayerService(_attackPower, _jumpDuration);
            zPosition = transform.position.z;
            yPosition = transform.position.y;
        }

        private void StopMove()
        {
            _canMove = false;
           StopMoving?.Invoke();
        }

        public void StartMove()
        {
            _canMove = true;
            Moving?.Invoke();
            transform.position = new Vector3(transform.position.x, yPosition, zPosition);
        }

        private void SetBallToStack()
        {
            for (int i = 0; i < _countBalls; i++)
            {
                GameObject ball = Instantiate(_ballPref, _pushBallPoint.transform.position, _pushBallPoint.transform.rotation);
                ball.transform.parent = transform;
                ball.gameObject.SetActive(false);
                _ballList.Add(ball);
            }
        }

        void AttackEnemy(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);

            int i = 0;

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.CompareTag("Enemy"))
                {
                    hitColliders[i].gameObject.tag = "Untagged";
                   StopMoving?.Invoke();
                    _canMove = false;
                    if (!_isOnAtack)
                    {
                        _isOnAtack = true;

                        PushBalls(hitColliders[i].gameObject.transform);
                    }
                }
                i++;
            }
        }

        void PushBalls(Transform target)
        {
            // StartCoroutine(Pushing(target,_countSteepMagnet, _steep, _changeY,_timeInSteep));

            _ballList[_indexBall].transform.position = _pushBallPoint.position;
            _ballList[_indexBall].transform.parent = gameObject.transform;
            _ballList[_indexBall].SetActive(true);
            _ballList[_indexBall].transform.DOJump(target.position, _jumpForce, 1, _jumpDuration).OnComplete(() =>
            {
                target.gameObject.GetComponent<Enemy>().TakeDamage(_attackPower);
                _ballList[_indexBall].transform.position = target.transform.position;
                _ballList[_indexBall].transform.parent = target.transform;
                _ballList[_indexBall].SetActive(false);
                _isOnAtack = false;
                _indexBall++;
                if (_indexBall >= _ballList.Count)
                {
                    _indexBall = 0;
                }
            });
        }
        public void TakeDamage(int damage, Transform enemy)
        {
            // _playerUpgradeService.TakeDamage(damage);
           
            Damaging?.Invoke();
            EnemyCollision(enemy);
        }


        public void EnemyCollision(Transform enemy)
        {
            Vector3 awayFly = transform.position - enemy.position;
            gameObject.GetComponent<Rigidbody>().AddForce(awayFly * 70, ForceMode.Impulse);
        }

        /*  public void MakeUpgrades( UpgradesType upgradeType, int value)
    {
        switch (upgradeType)
        {
            case UpgradesType.SpeedAttack:
                _playerUpgradeService.UpgradeSpeedAttack(value);
                break;
            case UpgradesType.HP:
                _playerUpgradeService.UpgradeHP(value);
                break;
            case UpgradesType.AttackPower:
                _playerUpgradeService.UpgradeAttackPower(value);
                break;

        }
    }*/
    }
}



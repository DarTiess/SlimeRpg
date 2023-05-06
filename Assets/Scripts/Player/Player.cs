using Data;
using HealthBar;
using Infrastructure.GameStates;
using UI;
using UnityEngine;


namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(Rigidbody))]
   [RequireComponent(typeof(PlayerHealthBar))]
    public class Player : MonoBehaviour, IPlayerDamage
    {
        [Header("Move Settings")]
        [SerializeField] private float _speed=18.14f;
        [Header("Pushing Balls Settings")]
        [SerializeField] private Bullet _ballPref;
        [SerializeField] private int _countBalls=10;
        [SerializeField] private Transform _pushBallPoint;
        [SerializeField] public float _attackDuration=0.64f;
        [Header("Attack Settings")]
        [SerializeField] private int _attackPower=5;
        [SerializeField] private float _radiusDetectEnemy=2.76f;
        [SerializeField] private float _kickForce = 70f;
        [Header("Health")]
        [SerializeField] private int _health;
        [SerializeField] private float _healthBarDuration= 0.4f;
        private Rigidbody _rigidbody;

        private PlayerAnimator _playerAnimator;
        private PlayerMove _playerMove;
        private PlayerBulletStack _bulletStack; 
        private PlayerUpgradeStates _playerUpgradeStates; 
        private PlayerHealthBar _playerHealthBar;

        private IGameStatesEvents _gameStatesEvents;
        private IGameStates _gameStates;
        private bool _isOnAtack;
        private IPlayerData _data;
        private bool _isDead;


        void Update()
        {
            if(_isDead) 
                return;
            
            TryDetectEnemy(gameObject.transform.position, _radiusDetectEnemy);
        }

        public void Init(IGameStatesEvents statesEvents, IGameStates gameStates, IPlayerData data)
        {
            _data = data;
            _data.MakeUpgrade += MakeUpgrades;
           _data.SetAttackPower(_attackPower);
           if (_data.HpNum <= 0)
           {
               _data.SetHp(_health);
           }
           else
           {
               _health = _data.HpNum;
           }
           
            _playerMove = GetComponent<PlayerMove>();
            _playerMove.Init(_speed);
           
            _playerAnimator = GetComponent<PlayerAnimator>();
           
            _bulletStack = new PlayerBulletStack(_ballPref, _countBalls, _pushBallPoint, transform, _attackPower);
            
            _gameStates = gameStates;
            _gameStatesEvents = statesEvents;
            _gameStatesEvents.OnPlayGame += StartMove;
            _gameStatesEvents.StopGame += StopMove;

            _playerHealthBar = GetComponent<PlayerHealthBar>();
          
            _playerUpgradeStates = new PlayerUpgradeStates(_attackPower, _health, _attackDuration,_healthBarDuration, _playerHealthBar);
            _playerUpgradeStates.DeadPlayer += DeadPayer;
            
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void TakeDamage(int damage, Transform enemy)
        {
            _playerUpgradeStates.TakeDamage(damage);
            _data.TakeDamageHp(damage);
            _playerAnimator.TakeDamage();
            EnemyCollision(enemy);
        }

        private void DeadPayer()
        {
            _isDead = true;
            StopMove();
            _gameStates.LevelLost();
        }

        private void StartMove()
        {
            _playerMove.StartMove();
            _playerAnimator.MoveAnimation();
        }

        private void StopMove()
        {
            _playerMove.StopMove();
            _playerAnimator.IdleAnimation();
        }

        private void TryDetectEnemy(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);

            int i = 0;

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.CompareTag("Enemy"))
                {
                    hitColliders[i].gameObject.tag = "Untagged";
                    StopMove();
                 
                    if (!_isOnAtack)
                    {
                        _isOnAtack = true;
                        _bulletStack.PushBullet(hitColliders[i].gameObject.transform);
                      _isOnAtack = false;
                    }
                }
                i++;
            }
        }

        private void EnemyCollision(Transform enemy)
        {
            Vector3 awayFly = transform.position - enemy.position;
            _rigidbody.AddForce(awayFly * _kickForce, ForceMode.Impulse);
        }

        private void MakeUpgrades( UpgradesType upgradeType, int value)
        {
            switch (upgradeType)
            {
                case UpgradesType.SpeedAttack:
                   _attackDuration= _playerUpgradeStates.UpgradeSpeedAttack(value);
                    break;
                case UpgradesType.HP:
                    _playerUpgradeStates.UpgradeHP(value);
                    break;
                case UpgradesType.AttackPower:
                   _attackPower = _playerUpgradeStates.UpgradeAttackPower(value);
                   // _data.OnUpgradeAttackPower(_attackPower);
                    break;
            }
        }

         
    }
}



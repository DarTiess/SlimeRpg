using Infrastructure.GameStates;
using UnityEngine;


namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(PlayerUpgradeService))]
    public class Player : MonoBehaviour
    {
        [Header("Move Settings")]
        [SerializeField] private float _speed=18.14f;
        [Header("Pushing Balls Settings")]
        [SerializeField] private Bullet _ballPref;
        [SerializeField] private int _countBalls=10;
        [SerializeField] private Transform _pushBallPoint;
        [SerializeField] public float _jumpDuration=0.64f;
        [Header("Attack Settings")]
        [SerializeField] private int _attackPower=5;
        [SerializeField] private float _radiusDetectEnemy=2.76f;
        [SerializeField] private float kickForce = 70f;
        
        private Rigidbody _rigidbody;

        private PlayerAnimator _playerAnimator;
        private PlayerMove _playerMove;
        private PlayerBulletStack _bulletStack;
        //  private PlayerUpgradeService _playerUpgradeService;

        private IGameStatesEvents _gameStatesEvents;
        private bool _isOnAtack;


        void Update()
        {
            TryDetectEnemy(gameObject.transform.position, _radiusDetectEnemy);
        }

        public void Init(IGameStatesEvents statesEvents)
        {
            _playerMove = GetComponent<PlayerMove>();
            _playerMove.Init(_speed);
            _playerAnimator = GetComponent<PlayerAnimator>();
            _bulletStack = new PlayerBulletStack(_ballPref, _countBalls, _pushBallPoint,_jumpDuration, transform, _attackPower);
            
            _gameStatesEvents = statesEvents;
            _gameStatesEvents.OnPlayGame += StartMove;
            _gameStatesEvents.StopGame += StopMove;
            
            // _playerUpgradeService = GetComponent<PlayerUpgradeService>();
            // _playerUpgradeService.InitPlayerService(_attackPower, _jumpDuration);
            _rigidbody = GetComponent<Rigidbody>();
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

        void TryDetectEnemy(Vector3 center, float radius)
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

     
   
        public void TakeDamage(int damage, Transform enemy)
        {
            // _playerUpgradeService.TakeDamage(damage);
           
            _playerAnimator.TakeDamage();
            EnemyCollision(enemy);
        }


        public void EnemyCollision(Transform enemy)
        {
            Vector3 awayFly = transform.position - enemy.position;
            _rigidbody.AddForce(awayFly * kickForce, ForceMode.Impulse);
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



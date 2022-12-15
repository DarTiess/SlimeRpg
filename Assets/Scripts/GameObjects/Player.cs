using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


[RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(HealthBar))]
    public class Player : MonoBehaviour
    {
        [Header("Move Settings")]
        [SerializeField]
        private float speed;

        private float xPos;
        [Header("Pushing Balls Settings")]
        [SerializeField] private GameObject ballPref;
        [SerializeField]
        private int countBalls;
        private List<GameObject> ballList=new List<GameObject>();
        private int indexBall = 0;
        [SerializeField] private Transform pushBallPoint;
        private bool isOnAtack;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float jumpDuration;
       // [SerializeField] private float _timeMagnet;
       // [SerializeField] private int _countSteepMagnet;
       // [SerializeField] private AnimationCurve _changeY;
      //  private float _steep;
      //  private float _timeInSteep;
      [Header("Attack Settings")]
      [SerializeField]
      private int attackPower;
      [SerializeField]
      private float radiusDetectEnemy;
      [Header("Health")]
      [SerializeField]
      private int health;
      private int HpNum {
          get { return PlayerPrefs.GetInt("Health"); }
          set { PlayerPrefs.SetInt("Health", value); }
      }
        private PlayerAnimator _playerAnimator;
        private HealthBar _healthBar;
       
        private GameManager _gameManager;
        private PlayerState _playerState;

        [Inject]
        private void InitiallizeComponent(GameManager manager, PlayerState state)
        {
            _gameManager = manager;
            _playerState = state;
            _gameManager.OnPlayGame+= StartMove;
        }

        private bool canMove;

        // Start is called before the first frame update
        void Start()
        {
            if (HpNum <= 0)
            {
                HpNum = health;
            }
            else
            {
                health = HpNum;
            }

            SetBallToStack();
            _playerState.SetHealthValue(HpNum);
            _playerState.SetAttackValue(attackPower);
            _healthBar=GetComponent<HealthBar>();
            _playerAnimator= GetComponent<PlayerAnimator>();   
         //   _steep = 1f / _countSteepMagnet;
         //   _timeInSteep = _timeMagnet / _countSteepMagnet;
            xPos = transform.position.z;
            _healthBar.SetMaxValus(health);
        }

        private void SetBallToStack()
        {
            for (int i = 0; i < countBalls; i++)
            {
               GameObject ball = Instantiate(ballPref,pushBallPoint.transform.position,pushBallPoint.transform.rotation);
               ball.transform.parent= transform;
                ball.gameObject.SetActive(false);
               ballList.Add(ball);
            }
        }
        public void StartMove()
        {
         canMove=true;
         transform.position = new Vector3(transform.position.x, transform.position.y,xPos);
        }

        // Update is called once per frame
        void Update()
        {
            AttackEnemy(gameObject.transform.position, radiusDetectEnemy);
            if (canMove)
            {
                _playerAnimator.MoveAnimation();
                Vector3 move = Vector3.zero;
                move.z =speed * Time.deltaTime;
                transform.Translate(move);
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
                    _playerAnimator.IdleAnimation();
                    canMove=false;
                    if (!isOnAtack)
                    {
                        isOnAtack= true;
                      
                        PushBalls(hitColliders[i].gameObject.transform);
                    }
                  
                }
          
                i++;
            }
        }

      
        void PushBalls(Transform target)
        {
           // StartCoroutine(Pushing(target,_countSteepMagnet, _steep, _changeY,_timeInSteep));
        
            ballList[indexBall].transform.position =pushBallPoint.position; 
            ballList[indexBall].transform.parent = gameObject.transform;
            ballList[indexBall].SetActive(true);
            ballList[indexBall].transform.DOJump(target.position, jumpForce, 1, jumpDuration).OnComplete(() =>
            {
                target.gameObject.GetComponent<Enemy>().TakeDamage(attackPower);
                ballList[indexBall].transform.position = target.transform.position;
                ballList[indexBall].transform.parent = target.transform;
                ballList[indexBall].SetActive(false);
                isOnAtack = false;
                indexBall++;
                if (indexBall >= ballList.Count)
                {
                    indexBall = 0;
                }
            });
        }

        public void TakeDamage(int damage, Transform enemy)
        {
            health -= damage;
            HpNum = health;
            _playerState.DamageHP(damage);
            int rndDamage=Random.Range(1, 4);
            _playerAnimator.TakeDamage(rndDamage);
            _healthBar.SetValues(damage, 0.4f);
             EnemyCollision(enemy);          
            if (health <= 0)
            {
                _gameManager.LevelLost();
            }
        }

        public void EnemyCollision(Transform enemy)
        {
            Vector3 awayFly = transform.position -enemy.position ;
            gameObject.GetComponent<Rigidbody>().AddForce(awayFly * 70, ForceMode.Impulse);
        }

        public void UpgradeHP(int hp)
        {
            health+=hp;
            HpNum = health;
            _playerState.AddHP(hp);
            _healthBar.UpgradeValue(hp, 0.3f);
        } 
        
        public void UpgradeAttackPower(int power)
        {
            attackPower+=power;
            
            _playerState.AddAttack(power);
        } 
        public void UpgradeSpeedAttack(int speed)
        {
           jumpDuration-=speed/100f;
        }
    }



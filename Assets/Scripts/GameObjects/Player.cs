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
        [SerializeField] private GameObject ball;
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
      [Header("Health")]
      [SerializeField]
      private int health;
        private PlayerAnimator _playerAnimator;
        private HealthBar _healthBar;
       
        private GameManager _gameManager;

        [Inject]
        private void Init(GameManager manager)
        {
            _gameManager = manager;
            _gameManager.OnPlayGame+= StartMove;
        }

        private bool canMove;

        // Start is called before the first frame update
        void Start()
        {
            _healthBar=GetComponent<HealthBar>();
            _playerAnimator= GetComponent<PlayerAnimator>();   
         //   _steep = 1f / _countSteepMagnet;
         //   _timeInSteep = _timeMagnet / _countSteepMagnet;
            xPos = transform.position.z;
            _healthBar.SetMaxValus(health);
        }
       
        public void StartMove()
        {
         canMove=true;
         transform.position = new Vector3(transform.position.x, transform.position.y,xPos);
        }

        // Update is called once per frame
        void Update()
        {
            AttackEnemy(gameObject.transform.position, 4f);
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
        
            ball.transform.position =pushBallPoint.position; 
            ball.transform.parent = gameObject.transform;
            ball.SetActive(true);
            ball.transform.DOJump(target.position, jumpForce, 1, jumpDuration).OnComplete(() =>
            {
                target.gameObject.GetComponent<Enemy>().TakeDamage(attackPower);
                ball.transform.position = target.transform.position;
                ball.transform.parent = target.transform;
                ball.SetActive(false);
                isOnAtack = false;
            });
        }


       

        private IEnumerator Pushing(Transform destin, int _countSteepMagnet, float _steep, AnimationCurve _changeY, float _timeInSteep)
        {
            ball.transform.position =pushBallPoint.position; 
            ball.transform.parent = gameObject.transform;
            ball.SetActive(true);
           
            for (int i = 0; i <= _countSteepMagnet; i++)
            {

                Vector3 pos = Vector3.Lerp(ball.transform.position, destin.transform.position, i * _steep);;
                pos.y += _changeY.Evaluate(i * _steep);
                ball.transform.position = pos;

                ball.transform.rotation = Quaternion.Lerp(ball.transform.rotation, destin.transform.rotation, i * _steep);


                yield return new WaitForSeconds(_timeInSteep);

            }

            destin.gameObject.GetComponent<Enemy>().TakeDamage(attackPower);
            ball.transform.position = destin.transform.position;
            ball.transform.parent = destin.transform;
          ball.SetActive(false);
          isOnAtack= false;
        }

        public void TakeDamage(int damage, Transform enemy)
        {
            health -= damage;
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

    }



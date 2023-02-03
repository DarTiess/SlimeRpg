using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerUpgradeService))]
public class Player : MonoBehaviour
{
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


    private float _xPos;
    private List<GameObject> _ballList = new List<GameObject>();
    private int _indexBall = 0;
    private bool _isOnAtack;

    private PlayerAnimator _playerAnimator;
    private PlayerUpgradeService _playerUpgradeService;

    private GameManager _gameManager;

    private bool _canMove;
    [Inject]
    private void Construct(GameManager manager, UIDisplay state)
    {
        _gameManager = manager;
        _gameManager.OnPlayGame += StartMove;
    }

    void Start()
    {
        SetBallToStack();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerUpgradeService = GetComponent<PlayerUpgradeService>();
        _playerUpgradeService.InitPlayerService(_attackPower, _jumpDuration);
        _xPos = transform.position.z;
    }

    void Update()
    {
        AttackEnemy(gameObject.transform.position, _radiusDetectEnemy);
        if (_canMove)
        {
            _playerAnimator.MoveAnimation();
            Vector3 move = Vector3.zero;
            move.z = _speed * Time.deltaTime;
            transform.Translate(move);
        }
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
    public void StartMove()
    {
        _canMove = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, _xPos);
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
        _playerUpgradeService.TakeDamage(damage);
        int rndDamage = Random.Range(1, 4);
        _playerAnimator.TakeDamage(rndDamage);
        EnemyCollision(enemy);
    }


    public void EnemyCollision(Transform enemy)
    {
        Vector3 awayFly = transform.position - enemy.position;
        gameObject.GetComponent<Rigidbody>().AddForce(awayFly * 70, ForceMode.Impulse);
    }

    public void MakeUpgrades( UpgradesType upgradeType, int value)
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
    }
}



using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int WALK = Animator.StringToHash("Walk");
        private static readonly int IDLE = Animator.StringToHash("Idle");
        private static readonly int DAMAGE = Animator.StringToHash("Damage");
        private static readonly int DAMAGE_TYPE = Animator.StringToHash("DamageType");
        
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        public void MoveAnimation()
        {
            _animator.SetBool(WALK, true);
            _animator.SetBool(IDLE, false);
        }

        public void IdleAnimation()
        {
            _animator.SetBool(WALK, false);
            _animator.SetBool(IDLE, true);
        }

        public void TakeDamage()
        {
            int rndDamage = Random.Range(1, 4);
            _animator.SetTrigger(DAMAGE);
            _animator.SetInteger(DAMAGE_TYPE, rndDamage);
        }
    }
}

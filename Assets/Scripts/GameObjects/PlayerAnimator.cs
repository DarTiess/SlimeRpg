using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation()
    {
        _animator.SetBool("Walk", true);
        _animator.SetBool("Idle", false);
    }

    public void IdleAnimation()
    {
        _animator.SetBool("Walk", false);
        _animator.SetBool("Idle", true);
    }

    public void TakeDamage(int numDamage)
    {
        _animator.SetTrigger("Damage");
        _animator.SetInteger("DamageType", numDamage);
    }
}

using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }


    public void MoveAnimation()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
    } 
    
    public void IdleAnimation()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
    }

    public void TakeDamage(int numDamage)
    {
        animator.SetTrigger("Damage");
        animator.SetInteger("DamageType", numDamage);
    }
}

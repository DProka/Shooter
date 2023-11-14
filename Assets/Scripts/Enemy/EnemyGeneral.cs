using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    [Header("Main Stats")]

    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] float maxHealth;

    private float currentHealth;
    private bool isAlive;

    private EnemyMovement moveScript;
    private EnemyAttack attackScript;
    private CapsuleCollider enemyCollider;

    private PlayerController player;

    [Header("Animator")]

    [SerializeField] Animator animator;

    public void Init(PlayerController _player)
    {
        player = _player;

        moveScript = GetComponent<EnemyMovement>();
        moveScript.Init(this);

        attackScript = GetComponent<EnemyAttack>();
        attackScript.Init(this);

        enemyCollider = GetComponent<CapsuleCollider>();

        healthBar.Init();

        animator.SetLayerWeight(1, 0);

        currentHealth = maxHealth;
        isAlive = true;
    }

    public void UpdateEnemy()
    {
        if (isAlive)
        {
            moveScript.UpdateMovement();
            attackScript.UpdateAttack();
            healthBar.UpdateBar();
        }
    }

    public PlayerController GetPlayer() { return player; }

    #region Health

    public void GetDamage(float damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;

            if (currentHealth > 0)
            {
                float dmg = (damage * 100) / maxHealth;
                healthBar.SetHealth(dmg);
            }
            else
            {
                SetDeath();
            }
        }
    }

    public bool GetAliveStatus() { return isAlive; }

    private void SetDeath()
    {
        animator.SetTrigger("Death");
        enemyCollider.enabled = false;
        isAlive = false;
        healthBar.gameObject.SetActive(false);
    }
    #endregion

    public void SetAnimation(string animation)
    {
        if(animation == "Idle")
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
     
        if(animation == "Walk")
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
        
        if(animation == "Run")
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }
        
        if(animation == "Attack")
        {
            animator.SetLayerWeight(1, 1);
            animator.SetTrigger("Attack");
        }

        if(animation == "ClearAttack")
        {
            animator.SetLayerWeight(1, 0);
            animator.ResetTrigger("Attack");
        }
    }

    public Animator GetAnimator() { return animator; }
}

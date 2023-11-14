using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRadius = 0.6f;
    [SerializeField] float timeBetweenAttacks = 1f;


    private EnemyGeneral general;
    private PlayerController player;

    private float attackTimer;

    public void Init(EnemyGeneral enemy)
    {
        general = enemy;
        player = general.GetPlayer();

        attackTimer = 0.3f;
    }

    public void UpdateAttack()
    {
        if (PlayerIsInRadius())
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            attackTimer = 0.3f;
        }
    }

    IEnumerator AttackPlayer()
    {
        attackTimer = timeBetweenAttacks;
        general.GetAnimator().SetLayerWeight(1, 1);
        general.GetAnimator().SetTrigger("Attack");

        yield return new WaitForSeconds(2.3f);

        general.GetAnimator().SetLayerWeight(1, 0);
        general.GetAnimator().ResetTrigger("Attack");
    }

    private bool PlayerIsInRadius()
    {
        return player != null && Vector3.Distance(transform.position, player.transform.position) <= attackRadius;
    }
}

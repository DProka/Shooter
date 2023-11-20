using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyGeneral general;
    private Transform player;

    [Header("Movement")]

    [SerializeField] float moveSpeedCalm = 3f;
    [SerializeField] float moveSpeedAgressive = 3f;
    [SerializeField] float changeDestinationTime = 5f;
    [SerializeField] float movementRadius = 10f;
    [SerializeField] float followRadius = 5f;
    
    public Vector3 destination;
    private float timer = 5f;

    [Header("Sight")]

    [SerializeField] LayerMask obstacleLayer;

    public void Init(EnemyGeneral enemy)
    {
        general = enemy;

        player = general.GetPlayer().transform;

        timer = changeDestinationTime;
        SetNewDestination();
    }

    public void UpdateMovement()
    {
        if (PlayerIsInRadius() && !PlayerIsVisible())
        {
            general.SwitchAgressive(true);
            //MoveToPlayer();
        }
        //else
        //    general.SwitchAgressive(false);
        //else
        //{
        //    MoveToDestination();

        //    timer -= Time.deltaTime;

        //    if (timer <= 0)
        //    {
        //        SetNewDestination();
        //        timer = Random.Range(changeDestinationTime - 2, changeDestinationTime + 2);
        //    }
        //}

        if (general.CheacAgressive())
        {
            MoveToPlayer();
        }
        else
        {
            MoveToDestination();

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SetNewDestination();
                timer = Random.Range(changeDestinationTime - 2, changeDestinationTime + 2);
            }
        }
    }

    private void SetNewDestination()
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * movementRadius;
        destination = new Vector3(randomPoint.x, transform.position.y, randomPoint.y) + transform.position;
    }

    private void MoveToDestination()
    {
        Vector3 direction = destination - transform.position;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            transform.Translate(Vector3.forward * moveSpeedCalm * Time.deltaTime);

            general.SetAnimation("Walk");
        }
        else
            general.SetAnimation("Idle");
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            
            if (directionToPlayer.magnitude > 0.5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
                transform.Translate(Vector3.forward * moveSpeedAgressive * Time.fixedDeltaTime);

                general.SetAnimation("Run");
            }
            else
            {
                general.SetAnimation("Idle");
            }
        }
    }

    private bool PlayerIsInRadius()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= followRadius;
    }

    private bool PlayerIsVisible()
    {
        Vector3 enemyPosition = transform.position;
        enemyPosition.y = 0.5f;
        Vector3 playerPosition = player.position;
        playerPosition.y = 0.5f;

        bool obstacleBetween = Physics.Raycast(enemyPosition, playerPosition - enemyPosition, followRadius + 2f, obstacleLayer);

        Debug.DrawRay(enemyPosition, playerPosition - enemyPosition, Color.red);
        return obstacleBetween;
    }
}

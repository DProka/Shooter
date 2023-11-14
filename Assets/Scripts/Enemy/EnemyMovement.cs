using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeedCalm = 3f;
    [SerializeField] float moveSpeedAgressive = 3f;
    [SerializeField] float changeDestinationTime = 5f;
    [SerializeField] float movementRadius = 10f;
    [SerializeField] float followRadius = 5f;

    private EnemyGeneral general;
    private Transform player;
    private Vector3 destination;
    private float timer;

    public void Init(EnemyGeneral enemy)
    {
        general = enemy;

        player = general.GetPlayer().transform;

        SetNewDestination();
    }

    public void UpdateMovement()
    {
        if (PlayerIsInRadius())
        {
            MoveToPlayer();
        }
        else
        {
            MoveToDestination();

            timer += Time.deltaTime;

            if (timer >= changeDestinationTime)
            {
                SetNewDestination();
                timer = 0f;
            }
        }
    }

    private void SetNewDestination()
    {
        Vector2 randomPoint = Random.insideUnitCircle * movementRadius;
        destination = new Vector3(randomPoint.x, transform.position.y, randomPoint.y);
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                transform.Translate(Vector3.forward * moveSpeedAgressive * Time.deltaTime);

                general.SetAnimation("Run");
            }
            else
                general.SetAnimation("Idle");
        }
    }

    private bool PlayerIsInRadius()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= followRadius;
    }
}

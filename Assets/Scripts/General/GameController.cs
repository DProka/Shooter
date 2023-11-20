using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Main")]

    private bool gameIsActive;

    [Header("Player")]

    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerBulletController playerBulletController;

    [Header("Enemy")]

    [SerializeField] EnemyController enemyController;

    [Header("Ground")]

    [SerializeField] GroundController groundController;

    void Start()
    {
        playerController.Init(playerBulletController);
        playerBulletController.Init(enemyController);

        enemyController.Init(playerController);

        //groundController.Init(playerBulletController.transform.position);
    }

    void FixedUpdate()
    {
        playerController.PlayerUpdate();
        playerBulletController.UpdateController();

        enemyController.UpdateEnemies();

        //groundController.UpdateController();
    }

}

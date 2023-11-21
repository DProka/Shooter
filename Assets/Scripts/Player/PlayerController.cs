using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    
    private PlayerFieldOfView fieldOfView;
 
    [Header("Movement")]

    [SerializeField] float speed = 0.5f;
    [SerializeField] LayerMask groundLayer;

    private PlayerMovement playerMovement;

    [Header("Shooting")]

    [SerializeField] LayerMask enemyLayer;
    private PlayerShooting playerShooting;
    private PlayerBulletController bulletController;

    public void Init(PlayerBulletController _bulletController)
    {
        bulletController = _bulletController;

        fieldOfView = GetComponent<PlayerFieldOfView>();
        fieldOfView.Init();

        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.Init(playerAnimator, speed, groundLayer, enemyLayer);

        playerShooting = GetComponent<PlayerShooting>();
        playerShooting.Init(playerAnimator);
    }

    public void PlayerUpdate()
    {
        playerMovement.UpdateMovement();

        playerShooting.UpdateShooting();
    }
    
    public void PlayerLateUpdate()
    {
        fieldOfView.UpdateView();
    }

    
}

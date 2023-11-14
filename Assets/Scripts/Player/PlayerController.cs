using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
 
    [Header("Movement")]

    [SerializeField] float speed = 0.5f;
    [SerializeField] LayerMask layerMask;

    private PlayerMovement playerMovement;

    [Header("Shooting")]

    private PlayerShooting playerShooting;
    private PlayerBulletController bulletController;

    public void Init(PlayerBulletController _bulletController)
    {
        bulletController = _bulletController;

        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.Init(playerAnimator, speed, layerMask);

        playerShooting = GetComponent<PlayerShooting>();
        playerShooting.Init(playerAnimator);
    }

    public void PlayerUpdate()
    {
        playerMovement.UpdateMovement();

        playerShooting.UpdateShooting();
    }

    
}

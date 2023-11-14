using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] PlayerBulletController bulletController;
    [SerializeField] Transform firepoint;
    [SerializeField] PlayerBulletScript bulletPrefab;
    [SerializeField] int maxAmmo;
    [SerializeField] float timeBetweenShoots;
    [SerializeField] float timeToReload;

    private int currentAmmo;
    private float shootTimer; 
    private float reloadTimer; 
    
    private Animator playerAnimator;

    public void Init(Animator animator)
    {
        playerAnimator = animator;

        currentAmmo = maxAmmo;
        shootTimer = timeBetweenShoots;
    }

    public void UpdateShooting()
    {
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            playerAnimator.SetBool("Shoot", true);
            Shoot();
        }
        else
            playerAnimator.SetBool("Shoot", false);
    }

    private void Shoot()
    {
        if (shootTimer <= 0)
        {
            currentAmmo -= 1;
            PlayerBulletScript bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation, bulletController.transform);
            bullet.Init(bulletController, firepoint);
            shootTimer = timeBetweenShoots;
        }
        
    }
}

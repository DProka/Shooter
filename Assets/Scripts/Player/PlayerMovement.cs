using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 0.5f;
    private LayerMask groundLayer;
    private LayerMask enemyLayer;
    private Animator playerAnimator;
    
    public void Init(Animator animator, float _speed, LayerMask _layerMask, LayerMask _enemyLayer)
    {
        playerAnimator = animator;
        speed = _speed;
        groundLayer = _layerMask;
        enemyLayer = _enemyLayer;
    }

    public void UpdateMovement()
    {
        AimTowardMouse();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        
        if(movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        playerAnimator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        playerAnimator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    private void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitTnfo;

        if (Physics.Raycast(ray, out hitTnfo, Mathf.Infinity, groundLayer))
        {
            Vector3 direction = hitTnfo.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
            Debug.DrawRay(transform.position, direction, Color.green);
        }

        if (Physics.Raycast(ray, out hitTnfo, Mathf.Infinity, enemyLayer))
        {
            Vector3 enemyPosition = hitTnfo.transform.position;

            Vector3 lookDirection = enemyPosition - transform.position;
            lookDirection.y = 0;

            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}


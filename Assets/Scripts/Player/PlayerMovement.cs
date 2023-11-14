using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 0.5f;
    private LayerMask layerMask;
    private Animator playerAnimator;
    
    public void Init(Animator animator, float _speed, LayerMask _layerMask)
    {
        playerAnimator = animator;
        speed = _speed;
        layerMask = _layerMask;
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

        if(Physics.Raycast(ray, out RaycastHit hitTnfo, Mathf.Infinity, layerMask))
        {
            Vector3 direction = hitTnfo.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
            Debug.DrawRay(transform.position, direction, Color.green);
        }
    }
}


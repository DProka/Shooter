using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned to the camera.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;

        transform.position = desiredPosition;
    }
}

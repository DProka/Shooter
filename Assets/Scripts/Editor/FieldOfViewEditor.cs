using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (PlayerFieldOfView))]

public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        PlayerFieldOfView fow = (PlayerFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        
        Vector3 vievAngleA = fow.DIrFromAngle(-fow.viewAngle / 2, false);
        Vector3 vievAngleB = fow.DIrFromAngle(fow.viewAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + vievAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + vievAngleB * fow.viewRadius);
    }
}

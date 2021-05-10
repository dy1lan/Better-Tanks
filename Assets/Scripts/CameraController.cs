using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0,10,-25);
    public float followSpeed = 10;
    public float lookSpeed = 75;
    public float ScaleFactorUP = 2f;

    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }

    /// <summary>
    /// LookAtTarget: Makes sure the camera stays looking at the target(Tank) and looks down at the target(Tank).
    /// </summary>
    public void LookAtTarget()
    {
        Vector3 LookDir = target.transform.position - transform.position  + (transform.up*ScaleFactorUP);
        Quaternion rotation = Quaternion.LookRotation(LookDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    /// <summary>
    /// MoveToTarget: Follows the Target(Tank) wherever it goes.
    /// </summary>
    public void MoveToTarget()
    {
        Vector3 targetPOS = target.transform.position + target.transform.forward * offset.z + target.transform.right * offset.x + target.transform.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPOS, followSpeed * Time.deltaTime);
    }
}

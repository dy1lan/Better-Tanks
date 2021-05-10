using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadController : MonoBehaviour
{
    public GameObject tank;

    // Update is called once per frame
    void Update()
    {
        Vector3 tankPOS = tank.transform.position;
        tankPOS.y += 100f;
        transform.position = Vector3.Lerp(transform.position, tankPOS, 8 * Time.deltaTime);


        Quaternion tankROT = tank.transform.rotation;
        Vector3 temp = tankROT.eulerAngles;
        temp.x = 90f;
        tankROT.eulerAngles = temp;
        transform.rotation = Quaternion.Lerp(transform.rotation, tankROT, 8 * Time.deltaTime);
    }
}

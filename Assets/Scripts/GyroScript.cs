using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScript : MonoBehaviour
{

    public int gyroText;
    
    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.supportsGyroscope)
            transform.rotation = GyroToUnity(Input.gyro.attitude);

        Debug.Log("GYRO DATA: " + Input.gyro.attitude);

    }

    private Quaternion GyroToUnity (Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, -q.w);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSScript : MonoBehaviour
{
    public Text GPSStatus;
    public Text latitudeValue;
    public Text longitudeValue;
    public Text altitudeValue;
    public Text horizontalAccuracyValue;
    public Text timestampValue;

    
    void Start()
    {
        StartCoroutine(GPSLoc());

    }
    IEnumerator GPSLoc()
    {
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initialize 
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;

        }

        // Service didn't init in 20 seconds
        if (maxWait < 1)
        {
            GPSStatus.text = "Time out";
            yield break;
        }

        // Connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }

    } // End if GPSLoc

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Access granted to GPS values and it has been initiated
            GPSStatus.text = "Running";
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location. lastData.longitude.ToString();
            altitudeValue.text = Input. location. lastData. altitude.ToString();
            horizontalAccuracyValue.text = Input. location. lastData. horizontalAccuracy.ToString();
            timestampValue.text = Input.location. lastData.timestamp.ToString();
        }
        else
        {
            // Service is stopped
            GPSStatus.text = "Stop";
        }
    } // End of UpdateGPSData

} // End of GPS Location
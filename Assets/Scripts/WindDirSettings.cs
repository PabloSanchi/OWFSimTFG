using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WindDirSettings : MonoBehaviour
{
    [SerializeField]
    WavesSettings wavesSettings;

    public float rotationSpeed = 70.0f;
    
    public void OnValueChanged(float value)
    {
        value = Mathf.Lerp(0f, 360f, value); // map the value to the range of the wind direction
        wavesSettings.UpdateWindDirection(value); // set the value to the waves settings asset
    }

    void Update()
    {
        float windAngle = wavesSettings.GetWindDirection();
        float windSpeed = wavesSettings.GetWindSpeed();
        Vector3 windDirection = Quaternion.Euler(0, windAngle, 0) * Vector3.forward;

        // Calculate the angle between wind direction and OWT forward direction
        Vector3 targetDirection = windDirection;
        float angle = Vector3.Angle(transform.forward, targetDirection);

        // Limit the maximum rotation speed to 10.0f 
        float maxRotationSpeed = 25.0f * windSpeed / 8.0f;
        float effectiveRotationSpeed = maxRotationSpeed * Mathf.Clamp01(Vector3.Dot(transform.forward, -windDirection));

        // Rotate the object
        if (angle > 0.1f) // avoid jittering
        {
            Vector3 rotationAxis = Vector3.Cross(transform.forward, targetDirection);
            float rotationAmount = Mathf.Min(angle, effectiveRotationSpeed * Time.deltaTime);
            
            transform.Rotate(Vector3.forward, rotationAmount);
            // transform.Rotate(Vector3.forward, Mathf.Min(angle, rotationSpeed * Time.deltaTime));
        }
    }


}

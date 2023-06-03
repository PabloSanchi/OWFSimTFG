using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public class WindDirSettings : MonoBehaviour
// {

//     [SerializeField]
//     WavesSettings wavesSettings;

//     private float maxRotationSpeed = 20.0f;
//     private float windSpeed;

//     private float windDirection;

//     private float rotationSpeed;

//     public void OnValueChanged(float value)
//     {
//         value = Mathf.Lerp(0f, 360f, value); // map the value to the range of the wind direction
//         wavesSettings.UpdateWindDirection(value); // set the value to the waves settings asset
//     }

//     void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(transform.position, 2.0f);
//     }

//     void Update()
//     {
//         windSpeed = wavesSettings.GetWindSpeed();
//         windDirection = wavesSettings.GetWindDirection();

//         Vector3 windDirectionVector = Quaternion.Euler(0, 0, windDirection) * Vector3.forward;

//         float dotProduct = Vector3.Dot(transform.up, windDirectionVector);

//         rotationSpeed = Mathf.Lerp(0.0f, maxRotationSpeed, Mathf.Abs(dotProduct));

//         Vector3 targetDirection = dotProduct > 0 ? windDirectionVector : -windDirectionVector;
//         float angle = Vector3.Angle(transform.forward, targetDirection);

//         if (angle > 0.1f)
//         {
//             Vector3 rotationAxis = Vector3.Cross(transform.forward, targetDirection);
//             transform.Rotate(rotationAxis, Mathf.Min(angle, rotationSpeed * Time.deltaTime));
//         }

//         transform.Translate(Vector3.forward * windSpeed * Time.deltaTime);
//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WindDirSettings : MonoBehaviour
// {
//     // [SerializeField]
//     // WavesSettings wavesSettings;

//     // public float maxRotationSpeed = 20.0f;

//     [SerializeField]
//     private float maxRotationSpeed = 20.0f;

//     [SerializeField]
//     private float minWindSpeed = 0.0f;

//     [SerializeField]
//     private float maxWindSpeed = 10.0f;

//     [SerializeField]
//     private WavesSettings wavesSettings;

//     private float rotationSpeed = 0.0f;

//     public void OnValueChanged(float value)
//     {
//         value = Mathf.Lerp(0f, 360f, value); // map the value to the range of the wind direction
//         wavesSettings.UpdateWindDirection(value); // set the value to the waves settings asset
//     }

  
//       void Update()
//     {
//         float windSpeed = wavesSettings.GetWindSpeed();
//         float windAngle = wavesSettings.GetWindDirection();

//         float normalizedWindSpeed = Mathf.InverseLerp(minWindSpeed, maxWindSpeed, windSpeed);
//         float normalizedRotationSpeed = Mathf.Lerp(0.0f, maxRotationSpeed, normalizedWindSpeed);
//         rotationSpeed = Mathf.Lerp(rotationSpeed, normalizedRotationSpeed, Time.deltaTime);

//         Vector3 windDirection = Quaternion.Euler(0, 0, windAngle) * Vector3.forward;
//         Vector3 targetDirection = windDirection;
//         float angle = Vector3.Angle(transform.forward, targetDirection);

//         if (angle > 0.1f)
//         {
//             float dotProduct = Vector3.Dot(transform.forward, targetDirection);
//             if (dotProduct < 0)
//             {
//                 rotationSpeed *= 0.5f;
//             }

//             Vector3 rotationAxis = Vector3.Cross(transform.forward, targetDirection);
//             transform.Rotate(rotationAxis, Mathf.Min(angle, rotationSpeed * Time.deltaTime), Space.World);
//         }
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WindDirSettings : MonoBehaviour
// {
//     [SerializeField]
//     WavesSettings wavesSettings;

//     public float rotationSpeed = 10.0f;
//     public float maxRotationSpeed = 10.0f;
//     public float maxRandomRotation = 5.0f;
    
//     public void OnValueChanged(float value)
//     {
//         value = Mathf.Lerp(0f, 360f, value); // map the value to the range of the wind direction
//         wavesSettings.UpdateWindDirection(value); // set the value to the waves settings asset
//     }

//     void Update()
//     {
//         float windAngle = wavesSettings.GetWindDirection();
//         float windSpeed = wavesSettings.GetWindSpeed();
//         // Convert wind angle to direction vector
//         Vector3 windDirection = Quaternion.Euler(0, windAngle, 0) * Vector3.forward;

//         // Calculate the angle between wind direction and object's forward direction
//         Vector3 targetDirection = windDirection;
//         float angle = Vector3.Angle(transform.forward, targetDirection);

//         // Calculate the rotation speed based on the wind speed
//         float rotationSpeedWithWind = Mathf.Min(rotationSpeed * windSpeed, maxRotationSpeed);

//         // Rotate the object towards the wind direction
//         if (angle > 0.1f || windSpeed > 0) // add a small threshold to avoid jittering and check if wind speed is greater than 0
//         {
//             Vector3 rotationAxis = Vector3.Cross(transform.forward, targetDirection);
//             transform.Rotate(rotationAxis, Mathf.Min(angle, rotationSpeedWithWind * Time.deltaTime));
//         }
//     }


// }


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
    
    // void Update()
    // {
    //     float windAngle = wavesSettings.GetWindDirection();
    //     float windSpeed = wavesSettings.GetWindSpeed();
    //     Vector3 windDirection = Quaternion.Euler(0, windAngle, 0) * Vector3.forward;

    //     // Calculate the angle between wind direction and object's forward direction
    //     Vector3 targetDirection = windDirection;
    //     float angle = Vector3.Angle(transform.forward, targetDirection);

    //     // Limit the maximum rotation speed to 10.0f
    //     float maxRotationSpeed = 25.0f * windSpeed / 8.0f;
    //     // float effectiveRotationSpeed = maxRotationSpeed * Mathf.Clamp01(Vector3.Dot(transform.forward, -windDirection));
    //     float effectiveRotationSpeed = maxRotationSpeed * Mathf.Clamp01(Vector3.Dot(transform.right, windDirection));
    //     if (Vector3.Dot(transform.forward, windDirection) < 0) 
    //     {
    //         effectiveRotationSpeed *= -1;
    //     }

    //     // Rotate the object towards the wind direction
    //     if (angle > 0.1f) // add a small threshold to avoid jittering
    //     {
    //         Vector3 rotationAxis = Vector3.Cross(transform.forward, targetDirection);
            
    //         float rotationAmount = Mathf.Min(angle, effectiveRotationSpeed * Time.deltaTime);
    //         transform.Rotate(Vector3.forward, rotationAmount);
    //     }
    // }

// void Update()
// {
//     float windAngle = wavesSettings.GetWindDirection();
//     float windSpeed = wavesSettings.GetWindSpeed();
//     Vector3 windDirection = Quaternion.Euler(0, windAngle, 0) * Vector3.forward;

//     // Calculate the angle between wind direction and object's forward direction
//     Vector3 targetDirection = windDirection;
//     float angle = Vector3.Angle(transform.forward, targetDirection);

//     // Limit the maximum rotation speed to 10.0f
//     float maxRotationSpeed = 25.0f * windSpeed / 8.0f;
//     // float effectiveRotationSpeed = maxRotationSpeed * Mathf.Clamp01(Vector3.Dot(transform.forward, -windDirection));
//     float effectiveRotationSpeed = maxRotationSpeed * Mathf.Clamp01(Vector3.Dot(transform.right, windDirection));
//     if (Vector3.Dot(transform.forward, windDirection) < 0) 
//     {
//         effectiveRotationSpeed *= -1;
//     }

//     // Rotate the object towards the wind direction
//     if (angle > 0.1f) // add a small threshold to avoid jittering
//     {
//         Vector3 rotationAxis = Vector3.up; // Rotate around the y-axis
//         float rotationAmount = Mathf.Min(angle, effectiveRotationSpeed * Time.deltaTime);
//                 transform.Rotate(Vector3.forward, rotationAmount);
//         // transform.Rotate(rotationAxis, rotationAmount);
//     }
// }


// void Update()
// {
//     float windAngle = wavesSettings.GetWindDirection();
//     float windSpeed = wavesSettings.GetWindSpeed();
//     Vector3 windDirection = Quaternion.Euler(0, windAngle, 0) * Vector3.forward;

//     // Calculate the angle between wind direction and object's forward direction
//     Vector3 targetDirection = windDirection;
//     float angle = Vector3.Angle(transform.forward, targetDirection);

//     // Limit the maximum rotation speed to 10.0f
//     float maxRotationSpeed = 25.0f * windSpeed / 8.0f;
//     float effectiveRotationSpeed = maxRotationSpeed * Mathf.Clamp01(Vector3.Dot(transform.right, windDirection));
//     if (Vector3.Dot(transform.forward, windDirection) < 0) 
//     {
//         effectiveRotationSpeed *= -1;
//     }

//     // Rotate the object towards the wind direction
//     if (angle > 0.1f) // add a small threshold to avoid jittering
//     {
//         Vector3 rotationAxis = Vector3.forward; // Rotate around the z-axis
//         float rotationAmount = Mathf.Min(angle, effectiveRotationSpeed * Time.deltaTime);
//         transform.Rotate(rotationAxis, rotationAmount);
//     }
// }



}
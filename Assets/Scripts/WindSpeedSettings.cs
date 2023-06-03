using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpeedSettings : MonoBehaviour 
{
    [SerializeField]
    WavesSettings wavesSettings;
    
    public void OnValueChanged(float value)
    {
        value = Mathf.Lerp(0.1f, 20f, value); // map the value to the range of the wind speed
        wavesSettings.UpdateWindSpeed(value); // set the value to the waves settings asset
    }
}
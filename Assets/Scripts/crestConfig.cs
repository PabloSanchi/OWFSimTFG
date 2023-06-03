using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crestConfig : MonoBehaviour
{
    [SerializeField]
    Crest.OceanRenderer renderer;
    
    public void OnValueChanged(float value)
    {
        value = Mathf.Lerp(0f, 150f, value);
        renderer._globalWindSpeed = value;
        // Debug.Log("value: " + renderer._globalWindSpeed);
    }
}

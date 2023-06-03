using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;


// default name : com.DefaultCompany.FFT-Ocean
public class FirebaseInit : MonoBehaviour
{
    [SerializeField]
    WavesSettings wavesSettings;

    [SerializeField]
    Crest.OceanRenderer renderer;

    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    public string databaseURL = "https://offshorewindfarm-c4f6b-default-rtdb.europe-west1.firebasedatabase.app";
    public string collectionId = "-NQVAkSX7VP9BOACVhM2";
    
    private float ParseRange(float value, float minFrom, float maxFrom, float minTo, float maxTo)
    {
        float proportion = (value - minFrom) / (maxFrom - minFrom);
        float result = proportion * (maxTo - minTo) + minTo;
        return result;
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread((task) => {
            if(task.Exception != null) {
                Debug.Log("Firebase initialization failed");
                return;
            }

            try 
            {
                OnFirebaseInitialized.Invoke();
                Debug.Log("Firebase initialization succeeded");

                try 
                {
                    FirebaseDatabase.GetInstance(databaseURL)
                    .GetReference("weather")
                    .ValueChanged += HandleValueChanged;
                } catch (Exception e) 
                {
                    Debug.Log("Firebase Read Error: " + e);
                }
            
            } catch (Exception e) 
            {
                Debug.Log("Init Error: " + e);
            }
        });
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // log the snapshot values
        Debug.Log(args.Snapshot.GetRawJsonValue());

        // extract the wind_speed, wind_direction, swell_height, swell_direction
        object ws = args.Snapshot.Child(collectionId).Child("wind_speed").Value;
        object wd = args.Snapshot.Child(collectionId).Child("wind_direction").Value;
        object sh = args.Snapshot.Child(collectionId).Child("swell_height").Value;
        object sd = args.Snapshot.Child(collectionId).Child("swell_direction").Value;

        // update the waves settings
        float windSpeed = float.Parse(ws.ToString());
        float windDirection = float.Parse(wd.ToString());
        float swellHeight = float.Parse(sh.ToString());
        float swellDirection = float.Parse(sd.ToString());
        wavesSettings.UpdateWindSpeed(windSpeed);
        wavesSettings.UpdateWindDirection(windDirection);
        wavesSettings.UpdateSwellDirection(swellDirection);
        

        // update wind speed in the background renderer
        float value = ParseRange(windSpeed, 0f, 38f, 0.1f, 150f);
        renderer._globalWindSpeed = value;
        // wavesSettings.UpdateSwell(swellHeight);
    }
}

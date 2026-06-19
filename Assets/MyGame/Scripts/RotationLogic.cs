using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class TurbineRotation : MonoBehaviour
{
    [SerializeField]
    private void Start()
    {
        StartCoroutine(GetWeatherData());
    }
    private int lastCityIndex = -1;
    void Update()
    {
        transform.Rotate(Vector3.left * (currentWindSpeed * 2) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha0)) selectedCityIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedCityIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedCityIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedCityIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedCityIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) selectedCityIndex = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6)) selectedCityIndex = 6;
        if (Input.GetKeyDown(KeyCode.Alpha7)) selectedCityIndex = 7;

        if (selectedCityIndex != lastCityIndex)
        {
            lastCityIndex = selectedCityIndex;
            StartCoroutine(GetWeatherData());
        }
    }
    [Header("Stadt auswählen")]
    public int selectedCityIndex = 0;
 
    [Header("Wind Speed Output")]
    public float currentWindSpeed;
 
    private List<City> cities = new List<City>()
    {
        new City("Salzburg", 47.8095f, 13.0550f),
        new City("London", 51.5072f, -0.1276f),
        new City("New York", 40.7128f, -74.0060f),
        new City("Paris", 48.8566f, 2.3522f),
        new City("Tokyo", 35.6762f, 139.6503f),
        new City("Berlin", 52.5200f, 13.4050f),
        new City("Sydney", -33.8688f, 151.2093f),
        new City("Dubai", 25.2048f, 55.2708f)
    };
 

 
    private IEnumerator GetWeatherData()
    {
        City selected = cities[selectedCityIndex];
 
        string url =
            $"https://api.open-meteo.com/v1/forecast" +
            $"?latitude={selected.Latitude.ToString(CultureInfo.InvariantCulture)}" +
            $"&longitude={selected.Longitude.ToString(CultureInfo.InvariantCulture)}" +
            $"&current=wind_speed_10m";
 
        using UnityWebRequest request = UnityWebRequest.Get(url);
 
        yield return request.SendWebRequest();
 
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }
 
        string json = request.downloadHandler.text;
 
        OpenMeteoResponse data =
            JsonUtility.FromJson<OpenMeteoResponse>(json);
 
        if (data != null && data.current != null)
        {
            currentWindSpeed = data.current.wind_speed_10m;
 
            Debug.Log(
                $"Wind Speed in {selected.Name}: {currentWindSpeed} km/h"
            );
        }
    }
 
    [Serializable]
    public class City
    {
        public string Name;
        public float Latitude;
        public float Longitude;
 
        public City(string name, float latitude, float longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
 
    [Serializable]
    public class OpenMeteoResponse
    {
        public Current current;
    }
 
    [Serializable]
    public class Current
    {
        public float wind_speed_10m;
    }
}



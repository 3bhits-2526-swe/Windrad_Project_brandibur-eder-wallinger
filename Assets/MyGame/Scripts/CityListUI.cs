using UnityEngine;
using TMPro;

public class CityListUI : MonoBehaviour
{
    [Header("TMP Reference")]
    public TMP_Text cityText;

    [Header("Selected City")]
    public int selectedCityIndex = 0;

    private string[] cities =
    {
        "Salzburg",
        "London",
        "New York",
        "Paris",
        "Tokyo",
        "Berlin",
        "Sydney",
        "Dubai"
    };

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                selectedCityIndex = i;
                UpdateUI();
            }
        }
    }

    void UpdateUI()
    {
        string output = "";

        for (int i = 0; i < cities.Length; i++)
        {
            if (i == selectedCityIndex)
            {
                output += $"<color=green><b>{i} - {cities[i]}</b></color>\n";
            }
            else
            {
                output += $"{i} - {cities[i]}\n";
            }
        }

        cityText.text = output;
    }
}
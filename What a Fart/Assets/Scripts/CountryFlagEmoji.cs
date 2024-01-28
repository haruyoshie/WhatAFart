using System;
using System.Collections.Generic;
using UnityEngine;

public class CountryFlagEmoji : MonoBehaviour
{
    public TextAsset jsonFile; // Asigna tu archivo JSON aquí en el Inspector de Unity

    void Start()
    {
        CountryList countryList = JsonUtility.FromJson<CountryList>("{\"countries\":" + jsonFile.text + "}");
        foreach (var country in countryList.countries)
        {
            string flagEmoji = GetFlagEmoji(country.code);
            Debug.Log(country.name + ": " + flagEmoji);
        }
    }

    string GetFlagEmoji(string countryCode)
    {
        int offset = 0x1F1E6;
        int asciiOffset = 0x41;

        char firstChar = (char)(offset + countryCode[0] - asciiOffset);
        char secondChar = (char)(offset + countryCode[1] - asciiOffset);

        return new string(new char[] { firstChar, secondChar });
    }
}

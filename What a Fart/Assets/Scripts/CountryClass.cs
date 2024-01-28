using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Country
{
    public string name;
    public string code;
}

[Serializable]
public class CountryList
{
    public List<Country> countries;
}

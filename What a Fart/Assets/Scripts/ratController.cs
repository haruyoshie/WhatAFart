using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratController : MonoBehaviour
{
    public GameObject RatMissionPanel, ratInstruccion;
    private void Start()
    {
        RatMissionPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        ratInstruccion.SetActive(true);
    }
}

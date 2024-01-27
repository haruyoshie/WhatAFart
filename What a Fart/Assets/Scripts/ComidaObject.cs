using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComidaObject : MonoBehaviour
{
    public float _potenciaPedo;
    public Controller _Controller;

    private void Start()
    {
        _Controller = FindObjectOfType<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _Controller.AddValuesToPedometer(_potenciaPedo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}

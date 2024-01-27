using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ComidaObject : MonoBehaviour
{
    public float potenciaPedo;
    public Controller controller;
    public bool goodFood;

    private void Start()
    {
        controller = FindObjectOfType<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (goodFood)
            {
                controller.DeleteValuesToPedometer(potenciaPedo);
            }
            else
            {
                controller.AddValuesToPedometer(potenciaPedo); 
            } 
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

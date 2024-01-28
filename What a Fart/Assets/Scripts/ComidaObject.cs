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
    public AudioSource foodEating;
    private void Start()
    {
        controller = FindObjectOfType<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
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
            StartCoroutine(EatingFood());
        }
    }

    IEnumerator EatingFood()
    {
        foodEating.Play();
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    
}

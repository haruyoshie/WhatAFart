using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTask : MonoBehaviour
{
    public string type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && FindObjectOfType<Controller>().quest.goal.goalType.ToString().Equals("Gathering"))
        {
            FindObjectOfType<Controller>().TaskByManager();
            Destroy(gameObject);
        }
        if (other.tag.Equals("Player") && FindObjectOfType<Controller>().quest.goal.goalType.ToString().Equals("Kitchen"))
        {
            FindObjectOfType<Controller>().GetObjectByClient();
            Destroy(gameObject);
        }
    }
}

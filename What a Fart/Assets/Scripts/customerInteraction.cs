using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class customerInteraction : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<ThirdPersonController>().customerClose = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<ThirdPersonController>().customerClose = false;
        }
    }
}

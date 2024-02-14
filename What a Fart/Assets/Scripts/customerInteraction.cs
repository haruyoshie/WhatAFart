using System;
using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class customerInteraction : MonoBehaviour
{
    public GameObject panelQuest, taskWindow;
    public GameObject objectToGather,objectToKitchen;
    public bool isOnMision, alreadyEntry, giveATask;
    public string typeMission;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && isOnMision && !alreadyEntry)
        {
            if (FindObjectOfType<Controller>().quest.isActive && FindObjectOfType<Controller>().quest.goal.goalType.ToString().Equals(typeMission))
            {
                ActivateGathering();
                alreadyEntry = true;
            }
        }

        if (other.tag.Equals("Player") && giveATask && !alreadyEntry && !FindObjectOfType<Controller>().quest.isActive)
        {
            FindObjectOfType<Controller>().taskWindow = taskWindow;
            panelQuest.SetActive(true);
            other.GetComponent<ThirdPersonController>()._canMove = false;
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
            alreadyEntry = true;
        }
    }

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
            InteractionManager.Instance.SetInteractState(InteractionState.Free);
            other.GetComponent<ThirdPersonController>().customerClose = false;
        }
    }

    public void ActivateGathering()
    {
        objectToGather.SetActive(true);
    }
    public void ActivateKitchen()
    {
        objectToKitchen.SetActive(true);
    }
}
public enum TypeMission
{
    Gathering,
    Kitchen
}
using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public GameObject panelQuest, taskWindow;
    private void OnTriggerEnter(Collider other)
    {
        Controller controller = FindObjectOfType<Controller>();
        if (other.tag.Equals("Player") && !controller.quest.isActive)
        {
            FindObjectOfType<Controller>().taskWindow = taskWindow;
            panelQuest.SetActive(true);
            other.GetComponent<ThirdPersonController>()._canMove = false;
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            InteractionManager.Instance.SetInteractState(InteractionState.Free);
        }
    }
}

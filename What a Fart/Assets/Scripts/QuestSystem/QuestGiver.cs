using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using StarterAssets;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public Controller player;
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI fartText;


    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        fartText.text = "The Reward are " + quest.fartReward.ToString() + " points of fartometter";
    }
    
    public void AcceptQuest()
    {
        quest.isActive = true;
        //give to player
        player.quest = quest;
        FindObjectOfType<ThirdPersonController>()._canMove = true;
    }
}

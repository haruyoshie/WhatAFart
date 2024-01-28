using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class GetScoreLevels : MonoBehaviour
{
    public GameObject poopScore, poopScore2, poopScore3;
    public Transform posRender;
    // Start is called before the first frame update
    void Start()
    {
        float levelScore = PlayerPrefs.GetFloat($"Level{this.gameObject.name}");
        Debug.Log(levelScore);
        
        if (levelScore == 1)
        {
            poopScore.gameObject.SetActive(true);
        }
        else if(levelScore == 2)
        {
            poopScore.gameObject.SetActive(true);
            poopScore2.gameObject.SetActive(true);
        }
        else if(levelScore == 3)
        {
            poopScore.gameObject.SetActive(true);
            poopScore2.gameObject.SetActive(true);
            poopScore3.gameObject.SetActive(true);
        }
    }
}

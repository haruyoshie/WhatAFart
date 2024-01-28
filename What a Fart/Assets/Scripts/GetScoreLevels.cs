using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

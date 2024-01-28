using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool _isFarted = false;
    public Animator customerAnimator;

    // Update is called once per frame
    void Update()
    {
        if(_isFarted == true)
        {
            Debug.Log("Cambio de animación");
            // customerAnimator.SetBool("isSmellingBad", _isFarted);
        }
    }
}

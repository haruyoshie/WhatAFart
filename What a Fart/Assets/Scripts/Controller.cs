using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public float _pedometer;
    public Slider _SliderPedometer;
    public GameObject Menu,uiMenu,player;
    private bool t;
    void Start()
    {
        player.GetComponent<StarterAssetsInputs>().openMenu.AddListener(OpenMenu);
    }
    
    public void OpenMenu()
    {
        t = !t;
        if (t)
        {
            uiMenu.SetActive(false);
            player.GetComponent<ThirdPersonController>()._canMove = false;
            Menu.SetActive(true);
            InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        }
        else
        {
            CloseMenu();
        }
    }
    public void CloseMenu()
    {
        t = false;
        Menu.SetActive(false);
        uiMenu.SetActive(true);
        player.GetComponent<ThirdPersonController>()._canMove = true;
        InteractionManager.Instance.SetInteractState(InteractionState.Free);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void AddValuesToPedometer(float valueFood)
    {
        _pedometer += valueFood;
        _SliderPedometer.value = _pedometer;
    }
    public void DeleteValuesToPedometer(float valueFood)
    {
        if(_SliderPedometer.value <10)return;
        _pedometer -= Mathf.Abs(valueFood);
        _SliderPedometer.value = _pedometer;
    }
    
}

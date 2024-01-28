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
    public GameObject Menu,uiMenu,player, gameOverPanel;
    public ParticleSystem fartParticles;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
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
        CalculateVelocityToFlart();

        if(_SliderPedometer.value >= 100)
        {
            EndGame();
            return;
        }
    }
    public void DeleteValuesToPedometer(float valueFood)
    {
        if(_SliderPedometer.value <10)return;
        _pedometer -= Mathf.Abs(valueFood);
        _SliderPedometer.value = _pedometer;
        CalculateVelocityToFlart();
        if(_SliderPedometer.value >= 100)
        {
            EndGame();
            return;
        }
    }

    public void Fart()
    {
        if(_SliderPedometer.value != 0)
        { 
            fartParticles.gameObject.SetActive(true);
            fartParticles.gameObject.transform.position = player.transform.position;
            fartParticles.gameObject.transform.localScale = new Vector3(_SliderPedometer.value/150, _SliderPedometer.value/150, _SliderPedometer.value/150);
            fartParticles.Play();

            int indexAudioSourcefart = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[indexAudioSourcefart];
            audioSource.Play();
            DeleteValuesToPedometer(_SliderPedometer.value);
        }
    }

    public void CalculateVelocityToFlart()
    {
        if (_SliderPedometer.value <= 100 && _SliderPedometer.value > 75)
        {
            player.GetComponent<ThirdPersonController>().MoveSpeed = 5;
        }
        else if (_SliderPedometer.value <= 75 && _SliderPedometer.value > 50)
        {
            player.GetComponent<ThirdPersonController>().MoveSpeed = 4;
        }
        else if (_SliderPedometer.value <= 50 && _SliderPedometer.value > 25)
        {
            player.GetComponent<ThirdPersonController>().MoveSpeed = 3;
        }
        else 
        {
            player.GetComponent<ThirdPersonController>().MoveSpeed = 2;
        }
    }

    public void EndGame()
    {
        player.GetComponent<ThirdPersonController>()._canMove = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        // player.GetComponent<ThirdPersonController>()._cui = false;
        gameOverPanel.SetActive(true);
    }
}

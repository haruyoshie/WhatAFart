using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Controller : MonoBehaviour
{
    public float _pedometer;
    public Slider _SliderPedometer;
    public GameObject Menu,uiMenu,player, gameOverPanel, passTheLevelPanel,gameOverForCustomer;
    public ParticleSystem fartParticles, poopParticles, fartParticlesVFX;
    public int numberOfClientsOut;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public GameObject CamaraAnim;
    private bool t;
    public GameObject[] food;
    public GameObject[] posFood;
    

    void Start()
    {
        player.GetComponent<StarterAssetsInputs>().openMenu.AddListener(OpenMenu);
        InvokeRepeating("RandomFood", 10,30);
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
            InvokeRepeating("loseParticle", 0.1f,0.1f);
        }
        else
        {
            CancelInvoke("loseParticle");
        }
    }
    public void loseParticle()
    {
        poopParticles.gameObject.SetActive(true);
        Vector3 posPlayer = player.transform.position;
        poopParticles.gameObject.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
    }
    public void DeleteValuesToPedometer(float valueFood)
    {
        if(_SliderPedometer.value <10)return;
        _pedometer -= Mathf.Abs(valueFood);
        _SliderPedometer.value = _pedometer;
        CalculateVelocityToFlart();
        if(_SliderPedometer.value < 75)
        {
            poopParticles.gameObject.SetActive(false);
            CancelInvoke("loseParticle");
        }
    }
    IEnumerator EndTheGame()
    {
        CamaraAnim.SetActive(true);
        poopParticles.gameObject.SetActive(true);
        Vector3 posPlayer = player.transform.position;
        poopParticles.gameObject.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
        yield return new WaitForSeconds(2.5f);
        EndGame();
    }
    public void Fart()
    {
        if(_SliderPedometer.value != 0)
        { 
            fartParticles.gameObject.SetActive(true);
            Vector3 posPlayer = player.transform.position;
            fartParticles.gameObject.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
            fartParticles.gameObject.transform.localScale = new Vector3(_SliderPedometer.value/150, _SliderPedometer.value/150, _SliderPedometer.value/150);
            fartParticles.Play();
            
            fartParticlesVFX.gameObject.SetActive(true);
            fartParticlesVFX.gameObject.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
            fartParticlesVFX.gameObject.transform.localScale = new Vector3(_SliderPedometer.value/50, _SliderPedometer.value/50, _SliderPedometer.value/50);
            fartParticlesVFX.Play();

            int indexAudioSourcefart = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[indexAudioSourcefart];
            audioSource.Play();
            if (player.GetComponent<ThirdPersonController>().customerClose)
            {
                LoseForCustomer();
            }
            if (_SliderPedometer.value >= 75)
            {
                StartCoroutine("EndTheGame");
                player.GetComponent<StarterAssetsInputs>().fart = false;
                DeleteValuesToPedometer(_SliderPedometer.value);
                return;
            }
            DeleteValuesToPedometer(_SliderPedometer.value);

            StartCoroutine(VerifyThePassLevel());
        }
        else
        {
            return;
        }
    }
    public void LoseForCustomer()
    {
        player.GetComponent<ThirdPersonController>()._canMove = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        gameOverForCustomer.SetActive(true);
    }
    IEnumerator VerifyThePassLevel()
    {
        Debug.Log("I'm verifying to pass the level");
        player.GetComponent<ThirdPersonController>().MoveSpeed = 0;
        CamaraAnim.SetActive(true);
        yield return new WaitForSeconds(2f); 
        player.GetComponent<ThirdPersonController>()._canMove = false;
        
        yield return new WaitForSeconds(5f);
        InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        CalculateScore();
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
    public void CalculateScore()
    {
        string nameScene = SceneManager.GetActiveScene().name;
        passTheLevelPanel.SetActive(true);
        if(numberOfClientsOut == 0) 
        {
            passTheLevelPanel.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            passTheLevelPanel.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            passTheLevelPanel.gameObject.transform.GetChild(6).gameObject.SetActive(true);
            
            PlayerPrefs.SetFloat(nameScene,0);
        } 
        else if(numberOfClientsOut <= 2 && numberOfClientsOut > 0) 
        {
            passTheLevelPanel.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(nameScene,1);
        } 
        else if(numberOfClientsOut <= 4 && numberOfClientsOut > 2) 
        {
            passTheLevelPanel.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(nameScene,2);
        }
        else
        {
            passTheLevelPanel.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(nameScene,3);
        }
    }
    public void EndGame()
    {
        player.GetComponent<ThirdPersonController>()._canMove = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        // player.GetComponent<ThirdPersonController>()._cui = false;
        gameOverPanel.SetActive(true);
    }

    public void RandomFood()
    {
        int randomFood = Random.Range(0, food.Length);
        int randomPosFood = Random.Range(0, posFood.Length);
        GameObject foodInstance = Instantiate(food[randomFood]);
        foodInstance.transform.position = posFood[randomPosFood].transform.position;
    }
}

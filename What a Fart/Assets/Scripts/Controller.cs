using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Controller : MonoBehaviour
{
    #region UI Components and others
    
    public float _pedometer;
    public Slider _SliderPedometer;
    [HideInInspector]
    public GameObject Menu,uiMenu,player, gameOverPanel, passTheLevelPanel,gameOverForCustomer;
    [HideInInspector]
    public ParticleSystem fartParticles, poopParticles, fartParticlesVFX;
    public int numberOfClientsOut;
    private int numberOfPoints;
    private GameObject[] clientsGameObject;
    private int numberOfClients;
    [HideInInspector]
    public AudioClip[] audioClips;
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public GameObject CamaraAnim;
    private bool t;
    [HideInInspector]
    public GameObject[] food;
    [HideInInspector]
    public GameObject[] posFood;
    public TextMeshProUGUI currentPoints;
    public GameObject taskWindow;
    public Quest quest;

    [SerializeField] private float timePerLevel;

    #endregion
   
    void Start()
    {
        player.GetComponent<StarterAssetsInputs>().openMenu.AddListener(OpenMenu);
        clientsGameObject = GameObject.FindGameObjectsWithTag("Customer");
        if (clientsGameObject != null)
        {
            foreach (GameObject client in clientsGameObject) {
                numberOfClients += 1;
            }
        }
       // InvokeRepeating("RandomFood", 2, 5);
    }

    #region Menu interactions

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

    #endregion
    #region Methods for slider pedometter

    public void AddValuesToPedometer(float valueFood)
    {
        _pedometer += valueFood;
        _SliderPedometer.value = _pedometer;
        CalculateVelocityToFlart();

        if(_SliderPedometer.value >= 75)
        {
            InvokeRepeating("LoseParticle", 0.1f,0.1f);
        }
        else
        {
            CancelInvoke("LoseParticle");
        }
    }
    public void DeleteValuesToPedometer(float valueFood)
    {
        if(_SliderPedometer.value < 10)return;
        _pedometer -= Mathf.Abs(valueFood);
        _SliderPedometer.value = _pedometer;
        CalculateVelocityToFlart();
        if(_SliderPedometer.value < 75)
        {
            poopParticles.gameObject.SetActive(false);
            CancelInvoke("LoseParticle");
        }
    }

    #endregion
    #region Methods for lose the game

    public void LoseParticle()
    {
        GameObject o;
        (o = poopParticles.gameObject).SetActive(true);
        Vector3 posPlayer = player.transform.position;
        o.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
    }
    public void LoseForCustomer()
    {
        player.GetComponent<ThirdPersonController>()._canMove = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        gameOverForCustomer.SetActive(true);
    }

    public void LoseForTime()
    {
        EndGame();
    }

    #endregion
    #region Methods to End the game and calculate the score

    IEnumerator EndTheGame()
    {
        CamaraAnim.SetActive(true);
        GameObject o;
        (o = poopParticles.gameObject).SetActive(true);
        Vector3 posPlayer = player.transform.position;
        o.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
        Debug.Log("Perdí por exceso de pedos");
        yield return new WaitForSeconds(2.5f);
        EndGame();
    }

    public void EndGame()
    {
        player.GetComponent<ThirdPersonController>()._canMove = false;
        InteractionManager.Instance.SetInteractState(InteractionState.StillMouseInteracting);
        // player.GetComponent<ThirdPersonController>()._cui = false;
        gameOverPanel.SetActive(true);
    }

    public void CalculateScore()
    {
        string nameScene = SceneManager.GetActiveScene().name;
        passTheLevelPanel.SetActive(true);
        if(numberOfPoints == 0) 
        {
            passTheLevelPanel.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            passTheLevelPanel.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            passTheLevelPanel.gameObject.transform.GetChild(6).gameObject.SetActive(true);
            
            PlayerPrefs.SetFloat(nameScene,0);
        } 
        else if(numberOfPoints <= Mathf.RoundToInt(numberOfClients/3) + 2 && numberOfPoints > 0) 
        {
            passTheLevelPanel.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(nameScene,1);
        } 
        else if(numberOfPoints <= Mathf.RoundToInt(numberOfClients/2) + 4 && numberOfPoints > Mathf.RoundToInt(numberOfClients/3) + 2) 
        {
            passTheLevelPanel.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(nameScene,2);
        }
        else if(numberOfPoints == numberOfClients + 8)
        {
            passTheLevelPanel.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            PlayerPrefs.SetFloat(nameScene,3);
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

    #endregion

    #region Methods to use in missions
    public void GetObjectByClient()
    {
        if (quest.isActive)
        {
            quest.goal.CustomerAttended();
            currentPoints.text = quest.goal.currentAmount.ToString();
            if (quest.goal.IsReached())
            {
                taskWindow.SetActive(false);
                currentPoints.text = "0";
                AddValuesToPedometer(quest.fartReward);
                FindObjectOfType<TimerLevel>().StopTimer(true);
                quest.Complete();
            } 
        }
    }

    public void TaskByManager()
    {
        if (quest.isActive)
        {
            quest.goal.ItemCollected();
            currentPoints.text = quest.goal.currentAmount.ToString();
            if (quest.goal.IsReached())
            {
                taskWindow.SetActive(false);
                currentPoints.text = "0";
                AddValuesToPedometer(quest.fartReward);
                FindObjectOfType<TimerLevel>().StopTimer(true);
                quest.Complete();
            } 
        }
    }
    #endregion
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Fart()
    {
        if(_SliderPedometer.value != 0)
        {
            GameObject o;
            (o = fartParticles.gameObject).SetActive(true);
            Vector3 posPlayer = player.transform.position;
            o.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
            o.transform.localScale = new Vector3(_SliderPedometer.value/150, _SliderPedometer.value/150, _SliderPedometer.value/150);
            fartParticles.Play();

            GameObject gameObject1;
            (gameObject1 = fartParticlesVFX.gameObject).SetActive(true);
            gameObject1.transform.position = new Vector3(posPlayer.x, posPlayer.y + 1, posPlayer.z);
            gameObject1.transform.localScale = new Vector3(_SliderPedometer.value/50, _SliderPedometer.value/50, _SliderPedometer.value/50);
            fartParticlesVFX.Play();

            int indexAudioSourcefart = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[indexAudioSourcefart];
            audioSource.Play();
            if (player.GetComponent<ThirdPersonController>().customerClose)
            {
                LoseForCustomer();
            }
            if (_SliderPedometer.value >= 85)
            {
                StartCoroutine(EndTheGame());
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

    public void RandomFood()
    {
        int randomFood = Random.Range(0, food.Length);
        int randomPosFood = Random.Range(0, posFood.Length);
        GameObject foodInstance = Instantiate(food[randomFood]);
        foodInstance.transform.position = posFood[randomPosFood].transform.position;
    }

    public void addPoints(int numberOfPointsToAdd)
    {
        numberOfPoints += numberOfPointsToAdd;
    }

    public void addClientOut()
    {
        numberOfClientsOut += 1;
        numberOfPoints += 2;
    }
}

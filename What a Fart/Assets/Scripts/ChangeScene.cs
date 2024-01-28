using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ChangeScene : MonoBehaviour
{
  public AudioClip[] audioClips;
  public AudioSource audioSource;
  public void changeScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  public void QuitApplication()
  {
    Application.Quit();
  }

  public void selectRandom()
  {
      int fart = Random.Range(0, audioClips.Length);
      audioSource.clip = audioClips[fart];
      audioSource.Play();
  }
}

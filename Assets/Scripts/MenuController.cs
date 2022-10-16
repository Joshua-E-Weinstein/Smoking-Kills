using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string gameLevel;
    [SerializeField] private string credits;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioClip clickClip;

   public void PlayButton()
   {
        SceneManager.LoadScene(gameLevel);
   }

   public void QuitButton()
   {
        Application.Quit();
   }

   public void CreditsButton()
    {
        SceneManager.LoadScene(credits);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void PlayClick()
    {
        SoundManager.Instance.PlayClip("click", clickClip, false, 0.5f);
    }
}

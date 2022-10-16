using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   [Header("Levels to Load")]
   [SerializeField] private string gameLevel;

   public void PlayButton()
   {
        SceneManager.LoadScene(gameLevel);
   }

   public void QuitButton()
   {
        Application.Quit();
   }
}

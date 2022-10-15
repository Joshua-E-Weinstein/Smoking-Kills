using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   [Header("Levels to Load")]
   [SerializeField] private string newGameLevel;
   private string _levelToLoad;

   public void NewGame()
   {
        SceneManager.LoadScene(newGameLevel);
   }

   public void LoadGame()
   {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            _levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(_levelToLoad);
        }
   }

   public void QuitButton()
   {
        Application.Quit();
   }
}

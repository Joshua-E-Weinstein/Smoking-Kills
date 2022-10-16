using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CreditsController : MonoBehaviour
{
    [SerializeField] private string menu;

    public void ExitToMenu()
    {
        SceneManager.LoadScene(menu);
    }
}

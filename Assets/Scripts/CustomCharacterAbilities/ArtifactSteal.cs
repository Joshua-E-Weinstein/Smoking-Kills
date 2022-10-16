using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtifactSteal : MonoBehaviour
{
    [SerializeField] private string winScreen;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Artifact")
        {
            //collision.gameObject.SetActive(false);
             SceneManager.LoadScene(winScreen);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TemporaryGameCompany;

public class SceptreGlow : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator Animator;

    [SerializeField] private ElementRuntimeSet PlayerSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerSet.Items[0].transform.position) < 5f)
        {
            Animator.SetBool("Glow", true);
        } else 
        {
            Animator.SetBool("Glow", false);
        }
    }
}

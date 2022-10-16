using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TemporaryGameCompany;

public class SmokeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private FloatReference current;
    [SerializeField] private FloatReference max;

    public void Start()
    {
        slider.value = slider.maxValue;
    }

    public void Update()
    {
        slider.value = slider.maxValue * (current/max);
    }
}

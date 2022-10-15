using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmokeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetSmoke(int smoke)
    {
        slider.value = smoke;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetHealth(int health)
    {
        if (health == 0)
            return;
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        if (health == 0)
            return;
        slider.maxValue = health;
        slider.value = health;
    }
}

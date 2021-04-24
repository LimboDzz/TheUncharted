using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;

    public void setMaxHP(int maxHealth){
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void setHP(int health){
        slider.value = health;
    }
}

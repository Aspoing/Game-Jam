using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void setMaxMana(float mana) {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void setMana(float mana) {
        slider.value = mana;
    } 
}

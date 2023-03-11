using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int max)
    {
        slider.maxValue = max;
        slider.value = max;
        //set health color to the end of the gradient value
        fill.color = gradient.Evaluate(1f);

    }

}

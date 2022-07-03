using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Image fillImage;
    public Stats stats;
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if(slider.value<=slider.minValue)
        {
            fillImage.enabled = false;
        }
        if(slider.value>slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = stats.currentHealth / stats.maxHealth;
        if(fillValue<=slider.maxValue/3f)
        {
            fillImage.color = Color.red;
        }
        else if (fillValue > slider.maxValue / 3f && fillValue < slider.maxValue / 1.4f)
        {
            fillImage.color = Color.yellow;

        }
        else if(fillValue>slider.maxValue/3f)
        {
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}

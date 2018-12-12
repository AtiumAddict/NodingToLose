using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{

    void OnEnable()
    {
        if (GetComponent<Slider>() != null)
        {
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("MovementSpeed");
        }
    }

    public void AdjustSpeed(float newSpeed)
    {
        PlayerPrefs.SetFloat("MovementSpeed", newSpeed);
    }
}

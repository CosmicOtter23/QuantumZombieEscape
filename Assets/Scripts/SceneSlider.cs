using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSlider : MonoBehaviour
{
    public Slider slider;

    public float timeLeft;

    public GameObject gameManager;

    private bool countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = false;
        //timeLeft = 10;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = timeLeft;

        if (countdown)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    public void StartSlider(float pickupDuration)
    {
        slider.maxValue = pickupDuration;
        timeLeft = pickupDuration;
        gameObject.SetActive(true);
        countdown = true;
    }
    public void StopSlider()
    {
        Debug.Log("Stopped");
        gameObject.SetActive(false);
        countdown = false;
    }
}

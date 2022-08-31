using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static float time;

    public Text text;

    // Update is called once per frame
    void Update()
    {
        time = Mathf.RoundToInt(gameObject.GetComponent<SpawnEnemy>().waveCountdown);

        text.text = "Time to next wave: " + time;
    }
}

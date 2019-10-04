using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    float theTime; // keeps a track of the current time
    public float speed = 1; //by seconds, mins, or hrs
    bool counting;
    bool reset;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
        StopTimer();
        ResetTimer();

        if (counting == true)
        {
            theTime += Time.deltaTime * speed;
            string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00"); // 3600 * 60
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00"); // 60 * 60; floor is to round the value
            string seconds = (theTime % 60).ToString("00");
            text.text = hours + ":" + minutes + ":" + seconds;
        }

        if (reset == true) {
            text.text = "00:00:00";
            theTime = 0;
        }
    }

    public void StartTimer() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            counting = true;
        }
    }

    public void StopTimer()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            counting = false;
        }
    }

    public void ResetTimer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reset = true;
        }
        else {
            reset = false;
        }
    }
}

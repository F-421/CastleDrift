using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    private float seconds;
    private int minutes;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        minutes = 0;
        timerText.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    //just in case I need to call it anywhere outside the text
    void UpdateTime(){
        seconds += Time.deltaTime; //convert to millis

        //convert to minutes
        if(seconds > 60){
            minutes++;
            seconds-= 60;
        }

        //display the time
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00.00");
    }
}

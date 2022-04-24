using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{

    public Ghost ghost;
    private float timer;
    private float timeValue;

    private void Awake()
    {

        //if the player choses the ghost cart option, record the players movements
        if(ghost.isRecord)
        {
            ghost.ResetData();
            timeValue = 0;
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;

        if(ghost.isRecord & timer >= 1/ghost.recordFrequency)
        {
            ghost.timestamp.Add(timeValue);
            ghost.positions.Add(this.transform.position);
            ghost.rotation.Add(this.transform.rotation);

            timer = 0;
        }
    }
}

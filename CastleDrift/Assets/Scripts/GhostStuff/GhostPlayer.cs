using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public Ghost ghost;
    private float timeValue;
    private int index1;
    private int index2;

    private void Awake()
    {
        timeValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.unscaledDeltaTime;

        //if it is time to replay the ghost
        if(ghost.isReplay)
        {
            getIndex();
            setTransform();
        }
    }

    private void getIndex()
    {
        for(int i = 0; i < ghost.timestamp.Count - 2; i++)
        {
            //the current time has been recorded and we can replay that
            if(ghost.timestamp[i] == timeValue)
            {
                index1 = i;
                index2 = i;
                return;
            }

            //we need to do linear interpolation between the last and next time stamp
            else if(ghost.timestamp[i] < timeValue & timeValue < ghost.timestamp[i+1])
            {
                index1 = i;
                index2 = i + 1;
                return;
            }
        }

        index1 = ghost.timestamp.Count - 1;
        index2 = ghost.timestamp.Count - 1;
    }

    private void setTransform()
    {
        //linear interpolation not needed
        if(index1 ==index2)
        {
            this.transform.position = ghost.positions[index1];
            this.transform.eulerAngles = ghost.positions[index1];
        }

        //need linear interpolation
        else
        {
            float interpolationFactor = (timeValue - ghost.timestamp[index1]) / (ghost.timestamp[index2] - ghost.timestamp[index1]);

            //Vector3.Lerp is a build in linear interpolation function
            this.transform.position = Vector3.Lerp(ghost.positions[index1], ghost.positions[index2], interpolationFactor);
            this.transform.eulerAngles = Vector3.Lerp(ghost.positions[index1], ghost.positions[index2], interpolationFactor);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popups;
    private int popupIndex;
    public float waiting = 2f;

    //order of popups is:
    //0: moving forward
    //1: turning
    //2: Drifting
    //3: Jumping



    private void Update()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            //waiting = 2f;
            //omly shows the current relevant popup instruction
            if (i == popupIndex)
            {
                    popups[i].SetActive(true);

            }

            else
            {

                    popups[i].SetActive(false);
            }
        }

            //tutorial has started

        //popup for moving forwards
        if (popupIndex == 0)
        {
           if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
              popupIndex++;
        }

        //popup for turning
        else if(popupIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                popupIndex++;
        }

        //popup for drifting
        else if (popupIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                popupIndex++;
        }

        //popup for Jumping
        else if (popupIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                popupIndex++;
        }
    }
}

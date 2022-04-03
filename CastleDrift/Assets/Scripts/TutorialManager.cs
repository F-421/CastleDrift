using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popups;
    private int popupIndex;

    //order of popups is:
    //0: moving forward
    //1: turning
    //2: Drifting
    //3: Jumping
    //4: Acceleration boosts
    //5: Avoiding Obstacles



    private void Update()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            //omly shows the current relevant popup instruction
            if( i == popupIndex)
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
            if(Input.GetKeyDown(KeyCode.UpArrow))
                popupIndex++;
        }

        //popup for turning
        else if(popupIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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

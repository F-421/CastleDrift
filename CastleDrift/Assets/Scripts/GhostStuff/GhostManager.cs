using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public Ghost ghost;

    private void Awake()
    {
        //if the player choses the ghost cart option, record the players movements
        //ghost.isRecord = true;
        ghost.isReplay = true;
    }
  

}

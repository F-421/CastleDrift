using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public Ghost ghost;

    public void RecordGhost()
    {
        //if the player choses the ghost cart option, record the players movements
        ghost.isRecord = true;
        ghost.isReplay = false;
    }
    public void PlayAgainstGhost()
    {
        //replay the ghost and make sure record is off
        ghost.isRecord = false;
        ghost.isReplay = true;
    }
  

}

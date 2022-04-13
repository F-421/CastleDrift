using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Ghost : ScriptableObject
{
    //tells the object if it is recording movement or replaying movemnt
    public bool isRecord;
    public bool isReplay;
    //sample frequency, the higher this is the smoother the replay will appear
    public float recordFrequency;

    //these will store the positions for the ghost to replay
    public List<float> timestamp;
    public List<Vector3> positions;
    public List<Quaternion> rotation;

    public void ResetData()
    {
        timestamp.Clear();
        positions.Clear();
        rotation.Clear();
    }


}

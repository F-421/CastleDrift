using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaternFireLauncher : MonoBehaviour
{
    const string TAG_COMPARE = "Player";  
    public float cooldown; // set time until launch another fire
    private float fire_cooldown_left; // cooldown when fire launched
    
    // Start is called before the first frame update
    void Start()
    {
        fire_cooldown_left = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*launch fire when player gets too close*/
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(TAG_COMPARE)){
            Debug.Log("Latern fire activate!!!");

        }
    }
}

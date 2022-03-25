using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* 
This file is to keep track of all the button functions in once place.
This is so I don't have to go on a wild goose chase for another quit button. 
*/

public class ButtonActions : MonoBehaviour
{
    // Loads the road
    public void StartGame()
    {
        // I'm just putting it to the next scene in the build queue 
        // so we don't have to change it if we change the title of our scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    // Update is called once per frame
    public void ResetTrack(){
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    //Exit the application (saving later)
    public void QuitGame(){
        Application.Quit();

    }
}

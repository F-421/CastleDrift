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
    public Ghost ghost;
    // Loads the road
    public void StartGame()
    {
        // I'm just putting it to the next scene in the build queue 
        // so we don't have to change it if we change the title of our scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetTrack(){
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    //Exit the application (saving later)
    public void QuitGame(){
        Application.Quit();

    }

    public void Tutorial()
    {
        //loads the tutorial scene
        //needs correct name for the scene
        SceneManager.LoadScene("TutorialScene");

    }

    public void Credits()
    {
        //loads the Credits scene
        //needs correct name for the scene
        SceneManager.LoadScene("CreditsScene");

    }

    public void Back()
    {
        //loads the Main Menu (StartMenu) scene
        //needs correct name for the scene
        SceneManager.LoadScene("StartMenu");
    }

    public void GhostCar()
    {
        //loads the Scene with the ghost car
        //needs correct name for the scene
        ghost.isRecord = true;
        ghost.isReplay = false;
        SceneManager.LoadScene("PracticeGhost");
      

    }

}

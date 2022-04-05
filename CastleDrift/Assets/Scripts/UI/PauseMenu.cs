using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool paused; //is pause active?
    public PlayerInput playerInput; // how we get the player input
    [SerializeField] private GameObject pausePanel; // the pause menu itself

    // Start is called before the first frame update


    /*pause activated*/
    public void OnPauseGame(InputAction.CallbackContext context){
        Debug.Log("Pause button hit");

        // resume game
        if (!paused){
            Time.timeScale = 0;
            paused = true;
            pausePanel.SetActive(true);
        }

        // pause game
        else{
            UnPause();
            
        }
        
    }

    public void UnPause(){
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
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


    //Go back to the main menu (From tutorial only)
    public void Back()
    {
        //loads the Main Menu (StartMenu) scene
        //needs correct name for the scene
        SceneManager.LoadScene("StartMenu");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{

    //restarts the track when pushed
    [SerializeField] UnityEngine.UI.Button reset_button;

    //button to quit when pushed
    [SerializeField] UnityEngine.UI.Button quit_button;

    // Start is called before the first frame update
    void Start()
    {
        reset_button.onClick.AddListener(ResetTrack);
        quit_button.onClick.AddListener(QuitGame);

        //hide this until we completed all laps
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void ResetTrack(){
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    //Exit the application (saving later)
    void QuitGame(){
        Application.Quit();

    }
}

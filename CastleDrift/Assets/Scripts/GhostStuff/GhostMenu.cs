using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GhostMenu : MonoBehaviour
{

    //restarts the track when pushed
    [SerializeField] UnityEngine.UI.Button start_button;

    //button to quit when pushed
    [SerializeField] UnityEngine.UI.Button quit_button;

    // Start is called before the first frame update
    void Start()
    {
        start_button.onClick.AddListener(StartGame);
        quit_button.onClick.AddListener(QuitGame);

        //hide this until we completed all laps
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    //Exit the application (saving later)
    void QuitGame()
    {
        Application.Quit();

    }
}

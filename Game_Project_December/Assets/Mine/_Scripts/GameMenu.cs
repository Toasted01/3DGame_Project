using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    PlayerManager playerManager;
    GameObject player;

    bool esc;
    public GameObject escape;
    public GameObject HUD;
    public GameObject Congrats;
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        esc = false;
        player = PlayerManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        //open
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) && !esc)
        {
            player.SendMessage("setEscape");
            HUD.SetActive(false);
            escape.SetActive(true);
            esc = true;
        }

        //close
        if (Input.GetKeyDown(KeyCode.Escape) && esc)
        {
            player.SendMessage("unsetEscape");
            HUD.SetActive(true);
            escape.SetActive(false);
            esc = false;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        player.SendMessage("unsetEscape");
        HUD.SetActive(true);
        escape.SetActive(false);
        gameOver.SetActive(false);
        Congrats.SetActive(false);
        esc = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

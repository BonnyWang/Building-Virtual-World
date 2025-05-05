using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerScreenController : MonoBehaviour
{
    public GameObject beeWinsImage;
    public GameObject beekeeperWinsImage;
    public Button restartButton;

    private void Start()
    {
        // Initially hide both images
        beeWinsImage.SetActive(false);
        beekeeperWinsImage.SetActive(false);

        // Set up the restart button click event
        restartButton.onClick.AddListener(RestartGame);
    }

    public void BeeWins()
    {
        beeWinsImage.SetActive(true);
        beekeeperWinsImage.SetActive(false);
    }

    public void BeekeeperWins()
    {
        beeWinsImage.SetActive(false);
        beekeeperWinsImage.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Start"); // Load the "start" scene
    }
}


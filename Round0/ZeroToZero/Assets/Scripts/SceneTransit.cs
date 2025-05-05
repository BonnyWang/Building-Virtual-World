using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransit : MonoBehaviour
{
    public void TransitToMain() =>
    SceneManager.LoadScene("Main");

    public void TransitToStart() =>
    SceneManager.LoadScene("Start");

    public void TransitToAsteroid() =>
    SceneManager.LoadScene("Asteroid");
}

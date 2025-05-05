using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Serializable Variables

    [Header("Hint Status")]
    // true if the letter has been picked up
    public bool letterPicked;

    // true if the letter has been read
    public bool letterRead;

    // true if CD picked up 
    public bool CDPicked;

    // true if the disk is placed on disk player
    public bool diskSocketed;

    // true if pulled handle
    public bool success;

    #endregion

    #region Unity Routines

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    #endregion

}
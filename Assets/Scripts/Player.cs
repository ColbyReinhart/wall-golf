using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayMode()
    {
        return playMode;
    }

    private bool playMode; // Are we in play mode? If not, edit mode
}

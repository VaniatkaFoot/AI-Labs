using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceScript : MonoBehaviour
{
    // public string gameMode;
    public int difficulty;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
    }
}

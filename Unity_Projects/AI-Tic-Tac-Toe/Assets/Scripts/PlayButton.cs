using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject persistantObj;

    public int diff;

    public void OnClicked()
    {
        
        persistantObj.GetComponent<PersistanceScript>().difficulty = diff;
        SceneManager.LoadScene("easy");
    }
}

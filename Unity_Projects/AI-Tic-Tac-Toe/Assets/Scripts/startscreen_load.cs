using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startscreen_load : MonoBehaviour
{
    // Start is called before the first frame update
   public void startscreen()
    {
        // Application.LoadLevel("Main");
        SceneManager.LoadScene("Main");
        GameObject persistantObj = GameObject.FindGameObjectWithTag("PersistantObj") as GameObject;
        Destroy(persistantObj);
    }

    
}

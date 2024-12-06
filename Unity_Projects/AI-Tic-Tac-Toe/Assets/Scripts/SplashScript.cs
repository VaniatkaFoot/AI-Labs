using UnityEngine;
using System.Collections;

public class SplashScript : MonoBehaviour {

    public float timer;
	// Update is called once per frame
	void Update () {
        if (timer < 0.0f)
        {
            Application.LoadLevel("main");
        }            
        timer -= Time.deltaTime;
	}
}

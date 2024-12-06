using UnityEngine;
using System.Collections;

public class OnHit : MonoBehaviour {
   // public int idx;
    
    public GameObject Camera;
    public GameScript Script;
    public int square_index;
    void Awake()
    {
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        Script = Camera.GetComponent<GameScript>();
    }

    void OnMouseDown()
    {
        Script.SpawnNew(this.gameObject,square_index);
        
    }
    
}

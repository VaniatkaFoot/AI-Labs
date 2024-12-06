using UnityEngine;
using UnityEngine.UI;

public class wins_and_loses : MonoBehaviour
{
    public Text wins,loses;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("wins")){
        wins.text = "Total wins: "+PlayerPrefs.GetInt("wins").ToString();
        loses.text = "Total loses: "+PlayerPrefs.GetInt("loses").ToString();
        }
        else{
        wins.text = "Total wins: 0";
        loses.text = "Total loses: 0";
        PlayerPrefs.SetInt("wins",0);
        PlayerPrefs.SetInt("loses",0);

        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentScore : MonoBehaviour
{
    public Text opponent_score;
    public GameObject multiplayer;

    // Start is called before the first frame update
    void Start()
    {
        //multiplayer = GameObject.Find("MultiplayerManager");
        opponent_score.text = "OPP:  0";
    }

    // Update is called once per frame
    void Update()
    {
        multiplayer = GameObject.FindGameObjectWithTag("Manager");
        //Debug.Log(multiplayer.GetComponent<MultiplayerScript>().opponent_score.ToString());
        opponent_score.text = "OPP:  " + multiplayer.GetComponent<MultiplayerScript>().opponent_score.ToString();
    }
}

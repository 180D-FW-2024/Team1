using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public MultiplayerScript multiplayerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onClickQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void onClickJoin()
    {
        Debug.Log("Join");
        //MyOtherScript multiplayerScript1 = multiplayerScript.GetComponent<MultiplayerScript>();
        multiplayerScript.ConnectToServer("172.30.48.181");

        //Application.LoadLevel(1);
    }
    public void onClickHost()
    {
        Application.LoadLevel(1);
    }
    public void onClickCalibration()
    {
        Application.LoadLevel(2);
    }

}

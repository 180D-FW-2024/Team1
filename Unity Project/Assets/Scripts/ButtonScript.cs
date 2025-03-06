using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public MultiplayerScript multiplayerScript;
    public string ip_addr_string = "";
    public Text iptext;
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
        multiplayerScript.ConnectToServer();

        //Application.LoadLevel(1);
    }
    public void onClickHost()
    {
        multiplayerScript.HostServer();
        //Application.LoadLevel(1);
    }
    public void onClickCalibration()
    {
        Application.LoadLevel(2);
    }

}

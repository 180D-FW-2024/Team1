using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
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
        Application.LoadLevel(1);
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

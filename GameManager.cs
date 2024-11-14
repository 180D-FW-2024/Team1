using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject toSpawn;
    public int start_z = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(toSpawn, new Vector3(0, 0, start_z+5), Quaternion.identity);
            start_z += 10;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DONTDIE : MonoBehaviour
{
    public static DONTDIE Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

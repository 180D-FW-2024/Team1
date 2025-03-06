using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class IP_ADDR : MonoBehaviour
{
    public Text ip_text;
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    // Start is called before the first frame update
    void Start()
    {
        ip_text.text = "My IP: " + GetLocalIPAddress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

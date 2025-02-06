using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Net;

using System.Text;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class MultiplayerScript : MonoBehaviour
{
    public static MultiplayerScript Instance;
    public int hosting = 0;
    public int connected = 0;
    public string ip_addr = "";
    public Text iptext;
    public int opponent_score = 0;
    public NetworkStream stream;
    public UdpClient udpSend;
    public UdpClient udpRecv;
    public System.Net.IPEndPoint RecvRemoteIpEndPoint;
    public System.Net.IPEndPoint SendRemoteIpEndPoint;
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
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("JUMP RECOGNIZED");
        }
    }

    public void ConnectToServer()
    {
        string ip = iptext.text;
        Debug.Log("Connecting to server"+ ip);
        //return;
        hosting = 0;
        //SEND A JOIN REQUEST TO SERVER
        TcpClient client = new TcpClient();
        client.Connect(ip, 5000);
        Debug.Log("Connected to server");
        stream = client.GetStream();
        byte[] data = Encoding.UTF8.GetBytes("JOIN");
        stream.Write(data, 0, data.Length);
        Debug.Log("Data sent");

        //RECEIVE JOIN ACK FROM SERVER
        byte[] response = new byte[4];
        int bytesRead = stream.Read(response, 0, response.Length);
        string responseString = Encoding.UTF8.GetString(response, 0, bytesRead);
        Debug.Log("Response received");
        Debug.Log(responseString);
        ip_addr = ip;
        if (responseString == "JACK")
        {
            //load scene 1
            SceneManager.LoadScene("GameScene");
            connected = 1;
            //invoke every 5 seconds
            
            udpRecv = new UdpClient(5005);
            RecvRemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 5005);
            udpSend = new UdpClient();
            SendRemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(ip_addr), 5006);
            InvokeRepeating("RecvScore", 0.0f, 1.0f);
            InvokeRepeating("SendScore", 0.0f, 1.0f);
        }
        else
        {
            Debug.Log("INCORRECT RESPONSE");
        }
        
       
    }

    private void RecvScore()
    {
        //recv a 4 byte int from the connection, and put into opponent_score
        //create a new stream which is udp and port 5005
        
        //get the ip address of the server
        
        //receive the data from the server
        byte[] receiveBytes = udpRecv.Receive(ref RecvRemoteIpEndPoint);
        //convert the data to an int
        opponent_score = BitConverter.ToInt32(receiveBytes, 0);
        Debug.Log("Received score: " + opponent_score);


    }



    public void HostServer()
    {
        hosting = 1;
        //create a server
        TcpListener server = new TcpListener(System.Net.IPAddress.Any, 5000);
        server.Start();
        Debug.Log("Server started");
        TcpClient client = server.AcceptTcpClient();
        Debug.Log("Client connected");
        
        stream = client.GetStream();
        ip_addr = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        Debug.Log(ip_addr);
        
        byte[] response = new byte[4];
        int bytesRead = stream.Read(response, 0, response.Length);
        string responseString = Encoding.UTF8.GetString(response, 0, bytesRead);
        Debug.Log("Data received");
        Debug.Log(responseString);

        if (responseString == "JOIN")
        {
            byte[] data = Encoding.UTF8.GetBytes("JACK");
            stream.Write(data, 0, data.Length);
            Debug.Log("Data sent");
            //load scene 1
       
            
            connected = 1;
            //invoke every 1 seconds
            udpRecv = new UdpClient(5006);
            RecvRemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 5006);
            udpSend = new UdpClient();
            SendRemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(ip_addr), 5005);
            InvokeRepeating("RecvScore", 0.0f, 1.0f);
            InvokeRepeating("SendScore", 0.0f, 1.0f);
            SceneManager.LoadScene("GameScene");
        }

    }
    private void SendScore()
    {
        //send our score as a 4 byte int over udp port 5006
        GameObject bear = GameObject.FindGameObjectWithTag("Bear");
        //Debug.Log(bear.GetComponent<NewCharacterController>().coins);
        //Debug.Log(multiplayer.GetComponent<MultiplayerScript>().opponent_score.ToString());
        int own_score = bear.GetComponent<NewCharacterController>().coins;
        Debug.Log("Sending score");
        //convert the score to a byte array
        byte[] score = BitConverter.GetBytes(own_score);
        Array.Reverse(score);
        //send the score to the server
        udpSend.Send(score, score.Length, ip_addr, 5006);
        Debug.Log("Sent score: " + own_score.ToString());
         
    }
}

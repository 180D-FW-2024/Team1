using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Sockets;
using System.Text;

using UnityEngine.SceneManagement;

public class MultiplayerScript : MonoBehaviour
{
    public static MultiplayerScript Instance;
    public int hosting = 0;
    public int connected = 0;
    public string ip_addr = "";
    public int opponent_score = 0;
    public NetworkStream stream;

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

    public void ConnectToServer(string ip)
    {
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
            InvokeRepeating("SendScore", 0.0f, 1.0f);
            InvokeRepeating("RecvScore", 0.0f, 1.0f);
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
        UdpClient udpClient = new UdpClient(5005);
        //get the ip address of the server
        System.Net.IPEndPoint RemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
        //receive the data from the server
        byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
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
       
            SceneManager.LoadScene("GameScene");
            connected = 1;
            //invoke every 1 seconds
            InvokeRepeating("SendScore", 0.0f, 1.0f);
            InvokeRepeating("RecvScore", 0.0f, 1.0f);
        }

    }
    private void SendScore()
    {
        //send our score as a 4 byte int
        byte[] myScore = BitConverter.GetBytes(10);
        stream.Write(myScore, 0, myScore.Length);
    }
}

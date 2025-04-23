using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public static Client instance;
    private TcpClient tcpClient;
    private NetworkStream stream;
    private bool isConnected;
    public Text chatText;
    public Text IPtext;
    public void Awake() => instance = this;

    void Start()
    {
        ConnectToServer("192.168.12.85", 8888);
    }

    void OnApplicationQuit()
    {
        tcpClient.Close();
    }
    public async void ConnectToServer(string ip, int port)
    {
        tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(ip, port);
        IPtext.text = "IP="+ ip;
        stream = tcpClient.GetStream();
        isConnected = true; //標記連線成功
        startReceiving();
    }

    private async void startReceiving()
    {
        byte[] buffer = new byte[4096];
        while (tcpClient.Connected)
        {
            int len = await stream.ReadAsync(buffer, 0, buffer.Length);
            var packet = Packet.Deserialize(buffer.Take(len).ToArray());
            Debug.Log($"[Server] {packet.userName}: {packet.message}");

            // 更新 UI
            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                ChatUI.instance.AddMessage(packet.userName, packet.message);
            });
        }
    }

    public async void SendMessageToServer(string userName, string msg)
    {

        if (!isConnected || stream == null)
        {
            Debug.LogWarning("尚未連線到 Server，請先執行 ConnectToServer");
            return;
        }

        var packet = new Packet()
        {
            packetId = 1,
            userName = userName,
            message = msg
        };
        byte[] data = packet.Serialize();
        await stream.WriteAsync(data, 0, data.Length);
    }

}

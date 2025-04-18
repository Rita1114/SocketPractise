﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class Server
{
    TcpListener listener = new TcpListener(IPAddress.Any, 8888);
    List<TcpClient> clients = new List<TcpClient>();

    private void StartSever()
    {
        listener.Start();
        Console.WriteLine("Start");
        Task.Run(() =>
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                clients.Add(client);
                HandleClient(client);
            }
        });
    }

    private async void HandleClient(TcpClient client)
    {
        var stream = client.GetStream();
        var buffer = new byte[4096];

        while (client.Connected)
        {
            int len =await stream.ReadAsync(buffer, 0, buffer.Length);
            var packet =Packet.Deserialize(buffer.Take(len).ToArray());

            Debug.Log($"{packet.userName}:{packet.message}");

            foreach( var c in clients)
            {
                if(c.Connected)
                {
                    await c.GetStream().WriteAsync(buffer, 0, len);
                }    
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
    
namespace ApiClient
{
    public static class ClientTCP
    {
        public static async Task<VacanciesInfo> GetVacanciesInfoAsync(string name, string host)
        {
            const int bytesize = 128 * 128;
            byte[] bytes = new byte[bytesize];

            return await Task.Run(() =>
            {
            
                // Establish the remote endpoint for the socket.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(host);
                IPAddress ipAddress = ipHostInfo.AddressList.Last();
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8888);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                sender.ReceiveTimeout = 30000;

                // Connect the socket to the remote endpoint. Catch any errors.  
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.UTF8.GetBytes(name);

                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);

                string results = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                VacanciesInfo vacanciesInfo = JsonConvert.DeserializeObject<VacanciesInfo>(results);

                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                return vacanciesInfo;

            });
        }
    }
}

using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Client");

var port = 11;     //2321;
IPAddress ip = IPAddress.Parse("127.0.0.1");         //("192.168.99.208");
IPEndPoint end = new(ip, port);

using TcpClient client = new();
client.Connect(end);
var stream = client.GetStream();
int readtot;
string msg;
byte[] buffer = new byte[1024];

while ((readtot = stream.Read(buffer, 0, buffer.Length)) > 0)
{
    msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    Console.WriteLine(msg);

    Array.Clear(buffer, 0, buffer.Length);
    int read;
    while (true)
    {

        Console.Write("Input:");
        string done = Console.ReadLine();
        /* Thread inp = new Thread(() => Input(done, stream,buffer));
         inp.Start();
         inp.Join();*/
        var stopbytes = Encoding.UTF8.GetBytes(done);
        stream.Write(stopbytes);
        //Thread.Sleep(20);

        if (done.Substring(0, 4) == "Done")
        {
            break;
        }
        else
        {
            stream.Read(buffer, 0, buffer.Length);
            msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Console.WriteLine(msg);
            Thread.Sleep(1000);

        }
    }


    static void Input(string done, NetworkStream stream, byte[] buffer)
    {
        var stopbytes = Encoding.UTF8.GetBytes(done);
        stream.Write(stopbytes);
        int readtot;
        while ((readtot = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            if (done.Substring(0, 4) == "Done")
            {
                break;
            }
            else
            {
                Array.Clear(buffer, 0, buffer.Length);
                stream.Read(buffer, 0, buffer.Length);
                string msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                Console.WriteLine(msg);

            }
        }
    }
}  
client.Close();

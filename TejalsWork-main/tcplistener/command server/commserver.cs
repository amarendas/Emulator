// See https://aka.ms/new-console-template for more information
using System.IO;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace Machine
{
    class Program
    {
 
        
        public static byte sensor1;
        public static byte sensor2;
        public static int encI = 1000;
        public static int encS;
        public static int encD;
        public static int dwellTime;
        public static byte totalTime;
        public static int errorCode;
        //public static readonly int[] indexerVals = { 730, 5730, 10730, 15730, 20730, 25730, 30730, 35730, 40730, 45730, 50730, 55730, 60730, 65730, 70730, 75730, 80730, 85730, 90730, 95730 };
        public static readonly int[] indexerVals = [ 880, 5879, 10873, 15869, 20822, 25904, 30860, 35859, 40729, 45740, 50755, 55728, 60751, 65744, 70725, 75705, 80794, 25904, 30860, 35859 ];
        public static int d, s = 0;



        public static void Main(string[] args)
        {


            //Connection
            Console.WriteLine("Server");
            var port = 2321;
            //var hostname = Dns.GetHostName();
            string local = "127.0.0.1";
            Console.WriteLine(local);
            IPAddress ip = IPAddress.Parse(local); 
            IPEndPoint end = new(ip, port);
            using TcpListener listener = new(end);

            while (true)
            {

                listener.Start();
                using TcpClient handler = listener.AcceptTcpClient();
                using NetworkStream stream = handler.GetStream();
                byte[] buffer = new byte[1024];
                
               
                if (encS == 0)
                {
                    sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                }
                if (encI == 0)
                {
                    sensor1 = Convert.ToByte(sensor1 | (1 << 4));

                }
                if (encD == 0)
                {
                    sensor1 = Convert.ToByte(sensor1 | (1 << 6));

                }
                sensor1 = Convert.ToByte(sensor1 | (1 << 1));
                sensor1 = Convert.ToByte(sensor1 | (1 << 5));
                sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                sensor2 = Convert.ToByte(sensor2 | (1 << 5));
                sensor2 = Convert.ToByte(sensor2 | (1 << 7));

                while (true)
                {

                    try {
                        stream.Read(buffer, 0, buffer.Length);
                        HB(stream);
                        string done = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        Thread c = new Thread(() => Emulator(stream, buffer));
                        string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        c.Start();
                        Thread.Sleep(20);
                        Thread inp = new Thread(() => Input(stream, buffer));
                        inp.Start();
                    }catch (Exception e)
                    {
                        Console.WriteLine("Connection Closed from Client Side. ");
                        break;
                    }
                    
                    
                }


            }
        }
        static Boolean InterlockOK(byte _Sensor)
        {
            //OK if   CmdInProgress==0,            Emg ==0,                      DoorSW==0                   TreatmentSw==1
            return (sensor2 & (1 << 1)) == 0 && (sensor2 & (1 << 6)) == 0 && (sensor2 & (1 << 4)) == 0 && (sensor2 & (1 << 7)) != 0;
        }
        static void Emulator(NetworkStream stream, byte[] buffer)
        {
            string input = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Array.Clear(buffer, 0, buffer.Length);
            int size = input.Length;
            int encCounts;
            int n;
            string str = @"#,+,?";
            Regex re = new Regex(str);
            if (re.IsMatch(input))
            {
                if(!input.Contains("HB"))
                    Console.WriteLine("Cmd Recieved:" + input);
                if (input.Contains("EI")) 
                {
                    if (InterlockOK(sensor2))
                    {
                        int w = 1;
                        int t = 0;
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                        encI = t * w;
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                        }
                        sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                        sensor1 = Convert.ToByte(sensor1 | (1 << 4)); ; //indexer home
                        encI = 0;
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));
                    }
                }
                else if (input.Contains("ES"))
                {
                     
                    if (InterlockOK(sensor2))
                    {
                        int t = 0;
                        int w = 1;
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                        encS = t * w;
                        if (Convert.ToByte(sensor1 & (1 << 1)) != 2)
                        {
                            sensor1 = Convert.ToByte(sensor1 | (1 << 1));
                        }
                        if (Convert.ToByte(sensor1 & (1 << 0)) == 1)
                        {
                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                        }
                        sensor2 = Convert.ToByte(sensor2 | (1 << 3));  //indexer calibrated
                        sensor1 = Convert.ToByte(sensor1 | (1 << 2)); //SOURCE home
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));

                        encS = 0;
                    }
                }
                else if (input.Contains("ED"))
                {
                    if (InterlockOK(sensor2))
                    {
                        int t = 0;
                        int w = 1;
                        sensor2 = (Byte)(sensor2 | (1 << 0)); // set moving =True
                        sensor2 = (Byte)(sensor2 | (1 << 1)); //Sent command_in_progress =True
                        encD = t * w;
                        if ((sensor1 & (1 << 5)) == 0) // if DummyOverShoot not True
                        {
                            sensor1 = (Byte)(sensor1 | (1 << 5)); // Set DummyOverShoot =TRUE
                        }
                        if ((sensor1 & (1 << 0)) == 1) //if Source_out == True 
                        {
                            sensor1 = (Byte)(sensor1 & ~(1 << 0)); //set Source_out==0
                        }

                        sensor2 = (Byte)(sensor2 | (1 << 3)); ; //indexer calibrated
                        sensor1 = (Byte)(sensor1 | (1 << 6)); ;//DUMMY home
                        sensor2 = (Byte)(sensor2 & ~(1 << 0)); //moving reset
                        sensor2 = (Byte)(sensor2 & ~(1 << 1)); // Command_In_Progress reset
                        encD = 0;
                    }
                }
                else if (input.Contains("MSF"))
                {
                    // Checks if cmd_in_progress=0,EMG=0,DOOR=0,Treatment_Switch=1
                    if (Convert.ToByte(sensor2 & (1 << 1)) ==0 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress

                            if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (Convert.ToByte(sensor1 & (1 << 1)) != 2))
                            {
                                errorCode = 209;
                            }
                            else if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (d > 0))
                            {
                                errorCode = 500;
                            }

                            else
                            {
                                encCounts = 5;
                                int end = input.IndexOf(",", 2);
                                int start = input.IndexOf("F");
                                n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                                int mul = encCounts * n;
                                int j = 0;
                                while (mul != 0)
                                {

                                    if (mul >= 500)
                                    {
                                        Thread.Sleep(10);
                                        encS += 100;
                                        mul -= 100;
                                    }
                                    else if (mul >= 100)
                                    {
                                        Thread.Sleep(10);
                                        encS += 50;
                                        mul -= 50;
                                    }
                                    else if (mul >= 50)
                                    {
                                        Thread.Sleep(10);
                                        encS += 10;
                                        mul -= 10;
                                    }
                                    else
                                    {
                                        Thread.Sleep(10);
                                        encS += 1;
                                        mul -= 1;
                                    }
                                    if (encS != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                                    }
                                    if (encS >= 1585)
                                    {
                                        j++;
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (j == 1)
                                        {
                                            s++;
                                        }
                                        if (encS == 51240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 1));
                                        }
                                        else if (encS > 51240)
                                        {
                                            encS = 51240;
                                            errorCode = 209;
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 1));
                                            break;
                                        }
                                    }
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                                if (encS == 0)
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                                }


                            }
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                        }
                        else
                        {
                            errorCode = 205;
                        }
                    }
                }
                else if (input.Contains("MSR"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 0 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress      
                            if (encS == 0)
                            {
                                errorCode = 208;
                            }
                            else if (encS > 0)
                            {
                                encCounts = 5;
                                int end = input.IndexOf(",", 2);
                                int start = input.IndexOf("R");
                                n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                                int mul = encCounts * n;
                                int j = 0;

                                while (mul != 0)
                                {
                                    if (mul >= 500)
                                    {
                                        Thread.Sleep(10);
                                        encS -= 100;
                                        mul -= 100;
                                    }
                                    else if (mul >= 100)
                                    {
                                        Thread.Sleep(10);
                                        encS -= 50;
                                        mul -= 50;
                                    }
                                    else if (mul >= 50)
                                    {
                                        Thread.Sleep(10);
                                        encS -= 10;
                                        mul -= 10;
                                    }

                                    else
                                    {
                                        Thread.Sleep(10);
                                        encS -= 1;
                                        mul -= 1;
                                    }

                                    if (encS < 0)
                                    {
                                        errorCode = 208;
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                                        encS = 0;
                                        break;
                                    }
                                    else if (encS < 21420)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 1));
                                        if (encS < 1585)
                                        {
                                            j++;
                                            if (j == 1)
                                            {
                                                sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                                                s = 0;
                                            }
                                        }
                                    }


                                    else
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));

                                    }
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (encS == 0)
                            {
                                sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                            }
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                        }
                        else
                        {
                            errorCode = 205;
                        }
                    }
                }
                else if (input.Contains("MDF"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (Convert.ToByte(sensor1 & (1 << 5)) != 32))
                            {
                                errorCode = 204;
                            }
                            else if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && s > 0)
                            {
                                errorCode = 500;
                            }

                            else
                            {
                                encCounts = 5;
                                int end = input.IndexOf(",", 2);
                                int start = input.IndexOf("F");
                                n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                                int mul = encCounts * n;
                                int j = 0;
                                while (mul != 0)
                                {

                                    if (mul >= 500)
                                    {
                                        Thread.Sleep(50);
                                        encD -= 100;
                                        mul -= 100;
                                    }
                                    else if (mul >= 100)
                                    {
                                        Thread.Sleep(50);
                                        encD -= 50;
                                        mul -= 50;
                                    }
                                    else if (mul >= 50)
                                    {
                                        Thread.Sleep(50);
                                        encD -= 10;
                                        mul -= 10;
                                    }

                                    else
                                    {
                                        Thread.Sleep(50);
                                        encD -= 1;
                                        mul -= 1;
                                    }

                                    if (encD != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                                    }
                                    if (encD <= -1585)
                                    {
                                        j++;
                                        if (j == 1)
                                        {
                                            d++;
                                        }

                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (encD == -51240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                        }
                                        else if (encD < -51240)
                                        {
                                            encD = -51240;
                                            errorCode = 204;
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                            break;
                                        }
                                    }
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                            }

                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                        }
                        else
                        {
                            errorCode = 205;
                        }
                    }
                }
                else if (input.Contains("MDS"))
                {
                    errorCode = 0;
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && (Convert.ToByte(sensor1 & (1 << 5)) != 32))
                            {
                                errorCode = 204;
                            }
                            else if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && s > 0)
                            {
                                errorCode = 500;
                            }

                            else
                            {
                                encCounts = 5;
                                int end = input.IndexOf(",", 2);
                                int start = input.IndexOf("S");
                                n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                                int mul = encCounts * n;
                                int j = 0;
                                while (mul != 0)
                                {

                                    if (mul >= 500)
                                    {
                                        Thread.Sleep(50);
                                        encD -= 100;
                                        mul -= 100;
                                    }
                                    else if (mul >= 100)
                                    {
                                        Thread.Sleep(50);
                                        encD -= 50;
                                        mul -= 50;
                                    }
                                    else if (mul >= 50)
                                    {
                                        Thread.Sleep(50);
                                        encD -= 10;
                                        mul -= 10;
                                    }

                                    else
                                    {
                                        Thread.Sleep(50);
                                        encD -= 1;
                                        mul -= 1;
                                    }

                                    if (encD != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                                    }
                                    if (encD <= -1585)
                                    {
                                        j++;
                                        if (j == 1)
                                        {
                                            d++;
                                        }

                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (encD == -21240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                        }
                                        else if (encD < -21240)
                                        {
                                            encD = -21240;
                                            errorCode = 204;
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                            break;
                                        }
                                    }
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                            }

                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                        

                    }
                }
                else if (input.Contains("MDR"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            if (encD == 0)
                            {
                                errorCode = 203;
                            }
                            if (encD < 0)
                            {
                                encCounts = 5;
                                int end = input.IndexOf(",", 2);
                                int start = input.IndexOf("R");
                                n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                                int mul = encCounts * n;
                                int j = 0;
                                while (mul != 0)
                                {
                                    if (mul >= 500)
                                    {
                                        Thread.Sleep(50);
                                        encD += 100;
                                        mul -= 100;
                                    }
                                    else if (mul >= 100)
                                    {
                                        Thread.Sleep(50);
                                        encD += 50;
                                        mul -= 50;
                                    }
                                    else if (mul >= 50)
                                    {
                                        Thread.Sleep(50);
                                        encD += 10;
                                        mul -= 10;
                                    }
                                    else
                                    {
                                        Thread.Sleep(50);
                                        encD += 1;
                                        mul -= 1;
                                    }

                                    if (encD > 0)
                                    {
                                        errorCode = 203;
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                                        encD = 0;
                                        break;
                                    }
                                    else if (encD > -21420)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 5));
                                        if (encD > -1585)
                                        {
                                            j++;
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                                            if (j == 1)
                                            {

                                                d = 0;
                                            }
                                        }
                                        if (encD == 0)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                                        }
                                        else
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                                        }
                                    }
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }

                                }

                            }
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                        }
                        else
                        {
                            errorCode = 205;
                        }
                    }
                }
                else if (input.Contains("MIF"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128 && Convert.ToByte(sensor1 & (1<<0))!=1))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress

                        encCounts = 1;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("F");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        while (mul != 0)
                        {

                            if (mul >= 500)
                            {
                                Thread.Sleep(10);
                                encI += 100;
                                mul -= 100;
                            }
                            else if (mul >= 100)
                            {
                                Thread.Sleep(10);
                                encI += 50;
                                mul -= 50;
                            }
                            else if (mul >= 50)
                            {
                                Thread.Sleep(10);
                                encI += 10;
                                mul -= 10;
                            }
                            else
                            {
                                Thread.Sleep(10);
                                encI += 1;
                                mul -= 1;
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (encI >= indexerVals[i] - 5 && encI <= indexerVals[i] + 5)
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                    errorCode = 0;
                                    break;
                                }
                                else
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                }
                            }

                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 4));
                            if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                            {
                                break;
                            }
                        }


                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                    }
                }
                else if (input.Contains("MIR"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128 && Convert.ToByte(sensor1 & (1 << 0)) != 1))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                        encCounts = 1;
                        int end = input.IndexOf(",", 2);
                        int start = input.IndexOf("R");
                        n = Convert.ToInt16(input.Substring(start + 1, end - start - 1));
                        int mul = encCounts * n;
                        while (mul != 0)
                        {
                            if (mul >= 500)
                            {
                                Thread.Sleep(10);
                                encI -= 100;
                                mul -= 100;
                            }
                            else if (mul >= 100)
                            {
                                Thread.Sleep(10);
                                encI -= 50;
                                mul -= 50;
                            }
                            else if (mul >= 50)
                            {
                                Thread.Sleep(10);
                                encI -= 10;
                                mul -= 10;
                            }
                            else
                            {
                                Thread.Sleep(10);
                                encI -= 1;
                                mul -= 1;
                            }
                            if (encI < 0)
                            {
                                encI = 100000 + encI;
                            }

                            for (int i = 0; i < 20; i++)
                            {
                                if (encI >= indexerVals[i] - 5 && encI <= indexerVals[i] + 5)
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                    errorCode = 0;
                                    break;

                                }
                                else
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                }

                            }
                            if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                            {
                                break;
                            }

                        }
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                    }
                }
                else if (input.Contains("OI"))
                {
                    if (InterlockOK(sensor2) && (sensor1 & (1 << 0)) != 1) // Dont move indexer is SOUT==1
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                        while (encI != 0)
                        {
                            if (encI >= 500)
                            {
                                encI -= 100;
                                Thread.Sleep(50);
                            }
                            else if (encI >= 100)
                            {
                                encI -= 50;
                                Thread.Sleep(50);
                            }
                            else if (encI >= 50)
                            {
                                encI -= 10;
                                Thread.Sleep(50);
                            }
                            else if (encI >= 10)
                            {
                                encI -= 5;
                                Thread.Sleep(50);
                            }
                            else
                            {
                                encI -= 1;
                                Thread.Sleep(50);
                            }
                            for (int i = 0; i < 20; i++)
                            {
                                if (!(indexerVals[i] - 5 <= encI && encI <= indexerVals[i] + 5))
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                    // errorCode = 205;
                                }
                            }
                            if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                            {
                                break;
                            }
                        }
                        sensor1 = Convert.ToByte(sensor1 | (1 << 4)); // Set Inderxer_Home ==TRUE
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                    }
                }
                else if (input.Contains("OS"))
                {
                    if (InterlockOK(sensor2))
                    {
                        
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            while (encS != 0)
                            {
                                if (encS >= 500)
                                {
                                    encS -= 100;
                                    Thread.Sleep(50);
                                }
                                else if (encS >= 100)
                                {
                                    encS -= 50;
                                    Thread.Sleep(50);
                                }
                                else if (encS >= 50)
                                {
                                    encS -= 10;
                                    Thread.Sleep(50);
                                }
                                else if (encS >= 10)
                                {
                                    encS -= 5;
                                    Thread.Sleep(50);
                                }
                                else
                                {
                                    encS -= 1;
                                    Thread.Sleep(50);
                                }
                                if (encS < 21240)
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 1));
                                }

                                if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && s > 0)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                                    s = 0;
                                }
                                if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                {
                                    break;
                                }
                            }

                            sensor1 = Convert.ToByte(sensor1 | (1 << 2));
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                        }
                      
                    }
                }
                else if (input.Contains("OD"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            while (encD != 0)
                            {
                                if (encD <= -500)
                                {
                                    encD += 100;
                                    Thread.Sleep(50);
                                }
                                else if (encD <= -100)
                                {
                                    encD += 50;
                                    Thread.Sleep(50);
                                }
                                else if (encD <= -50)
                                {
                                    encD += 10;
                                    Thread.Sleep(50);
                                }
                                else if (encD <= -10)
                                {
                                    encD += 5;
                                    Thread.Sleep(50);
                                }
                                else
                                {
                                    encD += 1;
                                    Thread.Sleep(50);
                                }
                                if (encS > -21240)
                                {
                                    sensor1 = Convert.ToByte(sensor1 | (1 << 5));
                                }

                                if ((Convert.ToByte(sensor1 & (1 << 0)) == 1) && d > 0)
                                {
                                    sensor1 = Convert.ToByte(sensor1 & ~(1 << 0));
                                    d = 0;
                                }
                                if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                {
                                    break;
                                }
                            }

                            sensor1 = Convert.ToByte(sensor1 | (1 << 6));
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                        }
                       
                    }
                }
                else if (input.Contains("R"))
                {

                   
                    errorCode = 0;
                   

                }
                else if (input.Contains("P"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128 && Convert.ToByte(sensor1 & (1 << 0)) != 1))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                        int start = input.IndexOf("P");
                        int end = input.IndexOf(",", 2);
                        input = input.Substring(start + 1, end - start - 1);
                        n = Convert.ToInt32(input);
                        int mul = encI - n;
                        int add = Math.Abs(mul);
                        if (mul < 0)
                        {
                            while (add != 0)
                            {

                                if (add >= 500)
                                {
                                    Thread.Sleep(50);
                                    encI += 100;
                                    add -= 100;
                                }
                                else if (add >= 100)
                                {
                                    Thread.Sleep(50);
                                    encI += 50;
                                    add -= 50;
                                }
                                else if (add >= 50)
                                {
                                    Thread.Sleep(50);
                                    encI += 10;
                                    add -= 10;
                                }
                                else
                                {
                                    Thread.Sleep(50);
                                    encI += 1;
                                    add -= 1;
                                }
                                for (int i = 0; i < 20; i++)
                                {
                                    if (encI >= indexerVals[i] - 5 && encI <= indexerVals[i] + 5)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                        errorCode = 0;
                                        break;

                                    }
                                    else
                                    {
                                        //errorCode = 205;
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                    }
                                }

                                if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                {
                                    break;
                                }
                            }
                        }

                        else
                        {
                            while (add != 0)
                            {
                                if (add >= 500)
                                {
                                    Thread.Sleep(50);
                                    encI -= 100;
                                    add -= 100;
                                }
                                else if (add >= 100)
                                {
                                    Thread.Sleep(50);
                                    encI -= 50;
                                    add -= 50;
                                }
                                else if (add >= 50)
                                {
                                    Thread.Sleep(50);
                                    encI -= 10;
                                    add -= 10;
                                }
                                else
                                {
                                    Thread.Sleep(50);
                                    encI -= 1;
                                    add -= 1;
                                }
                                for (int i = 0; i < 20; i++)
                                {
                                    if (encI >= indexerVals[i] - 5 && encI <= indexerVals[i] + 5)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 3));
                                        errorCode = 0;
                                        break;

                                    }
                                    else
                                    {
                                        //errorCode = 205;
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 3));
                                    }
                                }
                                if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                {
                                    break;
                                }
                            }
                        }

                        if (encI != 0)
                        {
                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 4));
                        }
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress
                    }
                }
                else if (input.Contains("S"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            int start = input.IndexOf("S");
                            int end = input.IndexOf(",", 2);
                            input = input.Substring(start + 1, end - start - 1);
                            n = Convert.ToInt32(input);
                            int mul = encS - n;
                            int add = Math.Abs(mul);

                            if (mul < 0)
                            {
                                while (add !=0)
                                {
                                    if (add >= 500)
                                    {
                                        Thread.Sleep(50);
                                        encS += 100;
                                        add -= 100;
                                    }
                                    else if (add >= 100)
                                    {
                                        Thread.Sleep(50);
                                        encS += 50;
                                        add -= 50;
                                    }
                                    else if (mul >= 50)
                                    {
                                        Thread.Sleep(50);
                                        encS += 10;
                                        add -= 10;
                                    }
                                    else
                                    {
                                        Thread.Sleep(50);
                                        encS += 1;
                                        add -= 1;
                                    }
                                    if (encS != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                                    }
                                    if (encS >= 1585)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (encS == 21240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                        }
                                        else if (encS > 21240)
                                        {
                                            encS = 21240;
                                            errorCode = 204;
                                            break;
                                        }
                                    }
                                    Thread.Sleep(50);
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                while (add !=0)
                                {
                                    if (add >= 500)
                                    {
                                        Thread.Sleep(50);
                                        encS -= 100;
                                        add -= 100;
                                    }
                                    else if (add >= 100)
                                    {
                                        Thread.Sleep(50);
                                        encS -= 50;
                                        add -= 50;
                                    }
                                    else if (add >= 50)
                                    {
                                        Thread.Sleep(50);
                                        encS -= 10;
                                        add -= 10;
                                    }
                                    else
                                    {
                                        Thread.Sleep(50);
                                        encS -= 1;
                                        add -= 1;
                                    }
                                    if (encS != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 2));
                                    }
                                    if (encS >= 1585)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (encS == 21240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                        }
                                        else if (encS > 21240)
                                        {
                                            encS = 21240;
                                            errorCode = 204;
                                            break;
                                        }
                                    }
                                    Thread.Sleep(50);
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                        }
                        else
                        {
                            errorCode = 205;
                        }
                    }
                }
                else if (input.Contains("D"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        if (Convert.ToByte(sensor1 & (1 << 3)) == 8)
                        {
                            sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                            int start = input.IndexOf("D");
                            int end = input.IndexOf(",", 2);
                            input = input.Substring(start + 1, end - start - 1);
                            
                            n = Convert.ToInt32(input);
                            int mul =Math.Abs(encD) - n;
                            int add = Math.Abs(mul);
                            if (mul < 0)
                            { 
                                    while (add != 0)
                                    {
                                        if (add >= 500)
                                        {
                                            Thread.Sleep(50);
                                            encD -= 100;
                                            add -= 100;
                                        }
                                        else if (add >= 100)
                                        {
                                            Thread.Sleep(50);
                                            encD -= 50;
                                            add -= 50;
                                        }
                                        else if (mul >= 50)
                                        {
                                            Thread.Sleep(50);
                                            encD -= 10;
                                            add -= 10;
                                        }
                                        else
                                        {
                                            Thread.Sleep(50);
                                            encD -= 1;
                                            add -= 1;
                                        }
                                        if (encD != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                                    }
                                    if (encD <= -1585)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (encD == -21240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                        }
                                        else if (encD < -21240)
                                        {
                                            encD = -21240;
                                            errorCode = 204;
                                            break;
                                        }
                                    }
                                    Thread.Sleep(50);
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }

                            }
                            else if(mul>0)
                            {
                                while (add !=0)
                                {
                                    if (add >= 500)
                                    {
                                        Thread.Sleep(50);
                                        encD += 100;
                                        add -= 100;
                                    }
                                    else if (add >= 100)
                                    {
                                        Thread.Sleep(50);
                                        encD += 50;
                                        add -= 50;
                                    }
                                    else if (add >= 50)
                                    {
                                        Thread.Sleep(50);
                                        encD += 10;
                                        add -= 10;
                                    }
                                    else
                                    {
                                        Thread.Sleep(50);
                                        encD += 1;
                                        add -= 1;
                                    }
                                    if (encD != 0)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 & ~(1 << 6));
                                    }
                                    if (encD <= -1585)
                                    {
                                        sensor1 = Convert.ToByte(sensor1 | (1 << 0));
                                        if (encD == -21240)
                                        {
                                            sensor1 = Convert.ToByte(sensor1 & ~(1 << 5));
                                        }
                                        else if (encD < -21240)
                                        {
                                            encD = -21240;
                                            errorCode = 204;
                                            break;
                                        }
                                    }
                                    Thread.Sleep(50);
                                    if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                            sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                        }
                        else
                        {
                            errorCode = 205;
                        }
                    }
                }
                else if (input.Contains("W"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                        int start = input.IndexOf("W");
                        int end = input.IndexOf(",", 2);
                        input = input.Substring(start + 1, end - start - 1);
                        int value = Convert.ToInt32(input)*1000;
                        int delay = 50; //ms
                        
                        for (int i = 0;  i < (value/delay); i++)
                        {
                            Thread.Sleep(delay);
                            dwellTime = i*delay;
                        }
                        
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1)); //command in progress

                    }
                }
                else if (input.Contains("I"))
                {
                    if (Convert.ToByte(sensor2 & (1 << 1)) != 2 && (Convert.ToByte(sensor2 & (1 << 6)) == 0 && Convert.ToByte(sensor2 & (1 << 4)) == 0 && Convert.ToByte(sensor2 & (1 << 7)) == 128))
                    {
                        Thread.Sleep(100);
                        int w = 1;
                        int t = 0;
                        sensor2 = Convert.ToByte(sensor2 | (1 << 0)); //moving
                        sensor2 = Convert.ToByte(sensor2 | (1 << 1)); //command in progress
                       

                        while (encI > 0)
                        {
                            encI = encI - t * w;
                            if (encI < 0)
                            {
                                encI = 0;
                            }
                            Thread.Sleep(500);

                            t += 100;
                            
                            if (Convert.ToByte(sensor2 & (1 << 6)) == 64 || Convert.ToByte(sensor2 & (1 << 4)) == 16 || Convert.ToByte(sensor2 & (1 << 7)) == 0)
                            {
                                break;
                            }

                        }

                        sensor2 = Convert.ToByte(sensor2 | (1 << 3)); ; //indexer calibrated
                        if (encI == 0)
                        {
                            sensor1 = Convert.ToByte(sensor1 | (1 << 4)); ; //indexer home
                        }
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 0));
                        sensor2 = Convert.ToByte(sensor2 & ~(1 << 1));

                    }

                }

                }
            }
        public static void HB(NetworkStream stream)
        {

            string Data2Send = "#," + Convert.ToString(sensor1) + "," + Convert.ToString(sensor2) + "," + Convert.ToString(encI) + "," + Convert.ToString(encS) + "," + Convert.ToString(encD) + "," + Convert.ToString(dwellTime) + "," + Convert.ToString(totalTime) + "," + Convert.ToString(errorCode)+",?";
            var Data2Sendbytes = Encoding.UTF8.GetBytes(Data2Send);
            //Console.WriteLine(Data2Send);
            stream.Write(Data2Sendbytes);

        }
        public static void Input(NetworkStream stream, byte[] buffer)
        {
            //Console.Write("Input:");
            string input = Console.ReadLine();
            if (input.Contains("EMGR"))
            {
                sensor2 = Convert.ToByte(sensor2 & ~(1 << 6));

            }
            else if (input.Contains("EMG"))
            {
                sensor2 = Convert.ToByte(sensor2 | (1 << 6));
            }
            else if (input.Contains("LMOR"))
            {
                sensor2 = Convert.ToByte(sensor2 & ~(1 << 5));
            }
            else if (input.Contains("LMO"))
            {
                sensor2 = Convert.ToByte(sensor2 | (1 << 5));
            }
            else if (input.Contains("DOORR"))
            {
                sensor2 = Convert.ToByte(sensor2 & ~(1 << 4));
            }
            else if (input.Contains("DOOR"))
            {
                sensor2 = Convert.ToByte(sensor2 | (1 << 4));
            }
            else if (input.Contains("TRTR"))
            {
                sensor2 = Convert.ToByte(sensor2 & ~(1 << 7));

            }
            else if (input.Contains("TRT"))
            {
                sensor2 = Convert.ToByte(sensor2 | (1 << 7));


            }
        }

    }

}
    



           


        

        
    











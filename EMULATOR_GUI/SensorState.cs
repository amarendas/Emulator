using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMULATOR_GUI
{
    internal class SensorState
    {
        private byte sensor1=0;
        private byte sensor2=0;
        public readonly int Tip = 0, sOver=1, sHome=2,Adapter=3,iHome=4, dOver=5,dHome=6,ilock=7;
        public readonly int moving = 10, CmdProg = 11, mainOn = 12, iCalib = 13, doorSw = 14, Lmo = 15, Emg = 16, TRT = 17;

        public byte Sensor1 { get => sensor1;  }
        public byte Sensor2 { get => sensor2;  }

        // Set a particular bit (0-7) in sensor1
        public void Set(int bit)
        {
            if(bit<10)
                sensor1 |= (byte)(1 <<bit);
            else 
                sensor2 |= (byte)(1 <<(bit-10));
        }


        // Clear a particular bit (0-7) in sensor1
        public void Clear(int bit)
        {
            if(bit < 10) 
                sensor1 &= (byte)~(1 << bit);
            else 
                sensor2 &= (byte)~(1<<(bit-10));
        }

        // Read a particular bit (0-7) in sensor1 (returns true if set, false otherwise)
        public bool isSet(int bit)
        {
            if (bit<10)
                return (sensor1 & (1 << bit)) != 0;
            else
                return (sensor2 & (1 << (bit - 10))) != 0;
        }

    }
}

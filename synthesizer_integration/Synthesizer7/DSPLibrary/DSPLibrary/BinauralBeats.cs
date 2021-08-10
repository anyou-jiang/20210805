using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace DSPLibrary
{
   public class BinauralBeats : WaveStream
    {

     public   BinauralBeats()
        {
            Amplitude = 1;
            LeftFrequencyOutPut = 180;
            tempSample = 0;
            RightFrequencyOutPut = 184;
            Frequency_switch = true;
        }

        bool onetimeflag;
        public bool stopflag { get; set; }
        public double LeftFrequencyOutPut { get; set; }
      
        public double RightFrequencyOutPut { get; set; }
        public double Amplitude { get; set; }
        public long BufferLength { get; set; }
        public override long Length { get { return BufferLength; } }
        public override WaveFormat WaveFormat { get { return WaveFormat.CreateIeeeFloatWaveFormat(44100, 2); } }
        private long position;
        static int n2;
        static float tempSample;

        
        public bool stopPlaying_flag { get; set; }

        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        bool Frequency_switch;

        public override int Read(byte[] buffer, int offset, int sampleCount)
        {

            if (position == 0 && onetimeflag == false)
            {
                n2 = 0;
                onetimeflag = true;
            }
            if (n2 >= BufferLength && stopflag == false)
            {
               Dispose();

                return -1;
            }
            float temp1;
            for (int i = 0; i < (sampleCount / 4); i++)
            {
                if (Frequency_switch)
                {
                    temp1 = (float)(Amplitude * Math.Sin(2D * Math.PI * LeftFrequencyOutPut * n2 / 44100D));
                    Frequency_switch = false;
                }
                else
                {
                    temp1 = (float)(Amplitude * Math.Sin(2D * Math.PI * RightFrequencyOutPut * n2 / 44100D));
                    Frequency_switch = true;
                    n2++;
                }

                byte[] bytes = BitConverter.GetBytes(temp1);
                buffer[i * 4 + 0] = bytes[0];
                buffer[i * 4 + 1] = bytes[1];
                buffer[i * 4 + 2] = bytes[2];
                buffer[i * 4 + 3] = bytes[3];
                tempSample++;
               
            }
            return sampleCount;
        }


    }
}

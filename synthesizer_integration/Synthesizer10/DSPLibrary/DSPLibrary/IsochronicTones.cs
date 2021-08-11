using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
namespace DSPLibrary
{
    public class IsochronicTones : WaveStream
    {
        public IsochronicTones()
        {
            Amplitude = 1;
            Frequency = 100;
            tempSample = 0;
            Beat = 0;
        }
        bool onetimeflag;
        public bool stopflag { get; set; }
        public double Frequency { get; set; }
        public double Beat { get; set; }
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
                // we'll just return the number of bytes read so far
                return position;
            }
            set
            {
                position = value;

            }
        }

       

      
        public override int Read(byte[] buffer, int offset, int sampleCount)
        {
            float temp1;

            if (position == 0 && onetimeflag == false)
            {
                n2 = 0;
                onetimeflag = true;
            }
            if (n2 >= BufferLength && stopflag == false)
            {
                Dispose();
                return 0;
            }

            float w = 0;
            for (int i = 0; i < (sampleCount / 4); i++)
            {
                if (Beat == 0)
                {
                    temp1 = (float)(Amplitude * Math.Sin(2D * Math.PI * Frequency * n2 / (2*44100D)
                    ));
                }
                else
                {
                    w = (float)(1D * Math.Sin(2D * n2 * Math.PI * Beat / (2* 44100D)));
                    if (w < 0)
                    {
                        w = 0;
                    }

                    temp1 = (float)(w * Amplitude * Math.Sin((2D * Math.PI * Frequency * n2) / (2* 44100D)));
                }
                byte[] bytes = BitConverter.GetBytes(temp1);
                buffer[i * 4 + 0] = bytes[0];
                buffer[i * 4 + 1] = bytes[1];
                buffer[i * 4 + 2] = bytes[2];
                buffer[i * 4 + 3] = bytes[3];
                tempSample++;
                n2++;
            }
            return sampleCount;
        }
    }
}

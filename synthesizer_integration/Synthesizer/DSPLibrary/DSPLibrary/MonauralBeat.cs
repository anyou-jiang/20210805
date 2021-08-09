using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Brain_Entrainment
{
    public class Monaural_beats : WaveStream
    {
        public Monaural_beats()
        {
            Amplitude = 1;
            Frequency = 100;
            tempSample = 0;
            Beat = 0;
        }

        public double Frequency { get; set; }
        public double Beat { get; set; }
        public double Amplitude { get; set; }
        public long Bufferlength { get; set; }
        bool onetimeflag;
        public bool stopflag { get; set; }

        public override long Length
        {
            get { return Bufferlength; }
        }

        public override WaveFormat WaveFormat
        {
            get { return WaveFormat.CreateIeeeFloatWaveFormat(44100, 2); }
        }

        static float tempSample;
        private long position;
        static int n2;

        public override long Position
        {
            get
            {
                // we'll just return the number of bytes read so far
                return position;
            }
            set { position = value; }
        }

        public override int Read(byte[] buffer, int offset, int sampleCount)
        {
            if (position == 0 && onetimeflag == false)
            {
                n2 = 0;
                onetimeflag = true;
            }

            if (n2 >= Bufferlength && stopflag == false)
            {
                Dispose();
                return 0;
            }

            float temp1;
            for (int i = 0; i < (sampleCount / 4); i++)
            {

                temp1 = (float) (Amplitude * Math.Sin(Math.PI * Frequency * n2 / 44100D) +
                                 Math.Sin(Math.PI * (Frequency - Beat) * n2 / 44100D) +
                                 5 * Math.Sin(Math.PI * 450 * n2 / 44100D));
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
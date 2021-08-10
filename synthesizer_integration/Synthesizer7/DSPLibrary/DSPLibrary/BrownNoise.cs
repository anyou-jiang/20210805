using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace DSPLibrary
{
    public class BrownNoise : WaveStream
    {
        double T;
        double dt;
        Random rand1;
        float lastValue;
        static int n2;

        public bool stopflag { get; set; }

        public BrownNoise()
        {
            T = 1;
            rand1 = new Random();
            n2 = 0;
            stopflag = false;
            position = 1;
            onetimeflag = false;
        }

        bool onetimeflag;
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public long Bufferlength { get; set; }
        private long position;
        public double LowPass;
        public double HighPass;
        public double Amplifier;


        double [] Filter(double[] list)
        {

            DspTools temp = new DspTools(list, 44100);
            temp.FFT1(); // calling the fft

            
            //  low pass  Filter
            if (LowPass != 0)
            {
                temp.LowPassFilter(LowPass);
            }

            if (HighPass != 0)
            {
                temp.HighPassFilter(HighPass);
            }

            //revering the order of the FFT array.
            temp.Reverse();
            //calling the IFFT
            List<double> buffer = temp.IFFT1();
            // Now Do the FFT again to see the new 
            // frequencies 

            return buffer.ToArray();
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
                return -1;
            }
            buffer[0] = 0;
            buffer[1] = 0;
            buffer[2] = 0;
            buffer[3] = 0;
            lastValue = 0;
            double[] temp3 = new double[sampleCount / 4];
            dt = T / (double)sampleCount;

            for (int n = 1; n < sampleCount / 4; n++)
            {
                float temp = (float)(Math.Sqrt(dt) * Amplitude * (2 * rand1.NextDouble() - 1));
                if (Frequency == 0)
                {
                    temp3[n] = lastValue + temp;
                    n2++;
                }
                else
                {
                    float w = (float)(0.5D * Math.Sin(n2 * Math.PI * Frequency / 44100D));
                    temp3[n] = (lastValue + temp) * w;
                    n2++;
                }

                lastValue = temp;
            }

            double[] temp4  = Filter(temp3);

            if (Amplifier == 0)
                Amplifier = 1;

            for (int n = 1; n < sampleCount / 4; n++)
            {
                byte[] bytes = BitConverter.GetBytes((float)(temp4[n] * Amplifier));
                buffer[n * 4 + 0] = bytes[0];
                buffer[n * 4 + 1] = bytes[1];
                buffer[n * 4 + 2] = bytes[2];
                buffer[n * 4 + 3] = bytes[3];

            }
            return sampleCount;
        }

        public override long Length { get { return Bufferlength; } }
        public override WaveFormat WaveFormat { get { return WaveFormat.CreateIeeeFloatWaveFormat(44100, 2); } }

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

    }

}

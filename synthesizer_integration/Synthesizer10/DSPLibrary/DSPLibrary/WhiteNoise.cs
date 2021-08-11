using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace DSPLibrary
{
   public class WhiteNoise : WaveStream
    {
        public  WhiteNoise()
        {
            Frequency = 0;
            Amplitude = 0.5f; // let's not hurt our ears  
            n2 = 0;
            stopflag = false;
            position = 1;
            onetimeflag = false;
        }
        bool onetimeflag;

        public double LowPass;
        public double HighPass;
        public double Amplifier;


        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        static int n2;
        public double filter { get; set; }
        public bool stopflag { get; set; }
        public long Bufferlength { get; set; }
        public double filter2 { get; set; }
        private long position;
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




        double[] Filter(double[] list)
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
                return 0;

            } 
            double[] temp3 = new double[sampleCount / 4];
            int sampleRate = WaveFormat.SampleRate;
            Random rnd1 = new System.Random();
            Double temp;
            for (int n = 0; n < (sampleCount / 4); n++)
            {
                temp = Amplitude * 2 * rnd1.NextDouble() - 1;
                if (Frequency == 0)
                {
                    temp3[n] = temp;
                    n2++;
                }
                else
                {
                    temp3[n] = (temp * Math.Sin(Math.PI * Frequency * n2 / 44100D));

                    n2++;
                }

            }

            double[] temp4 = Filter(temp3);

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

    }
}



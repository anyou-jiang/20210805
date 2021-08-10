using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace DSPLibrary
{
    public class PinkNoise : WaveStream
    {

        int max_key;
        int key;
        float[] whiteValues;
        UInt16 range;
        Random rnd1;
        static int n2;
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        public bool StopFlag { get; set; }
        bool _oneTimeFlag;

        public double LowPass;
        public double HighPass;
        public double Amplifier;


        public PinkNoise()
        {
            rnd1 = new System.Random();
            whiteValues = new float[5];
            max_key = 0x1f; // Five bits set 
            range = 128;
            key = 0;
            n2 = 0;
            Frequency = 0;
            for (int i = 0; i < 5; i++)
                whiteValues[i] = (float)((2 * rnd1.NextDouble() - 1) % (range / 5f));
            Amplitude = 1;
            StopFlag = false;
            position = 1;
            _oneTimeFlag = false;

        }

        public long Bufferlength { get; set; }
        public override long Length { get { return Bufferlength; } }
        public override WaveFormat WaveFormat { get { return WaveFormat.CreateIeeeFloatWaveFormat(44100, 2); } }
        private long position;

        public override int Read(byte[] buffer, int offset, int sampleCount)
        {
            if (position == 0 && _oneTimeFlag == false)
            {
                n2 = 0;
                _oneTimeFlag = true;
            }
            if (n2 >= Bufferlength && StopFlag == false)
            {
                Dispose();
                return 0;
            }
            double[] temp3 = new double[sampleCount / 4];
            for (int n = 0; n < sampleCount / 4; n++)
            {
                if (Frequency == 0)
                {
                    float temp = (float)(Amplitude * GetNextValue());
                    temp3[n] = temp;
                    n2++;
                }
                else
                {
                    float temp = (float)(Amplitude * GetNextValue() * Math.Sin(Math.PI * Frequency * n2 / 44100D));
                    temp3[n] = temp;
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


        public float GetNextValue()
        {
            int last_key = key;
            float sum;
            key++;
            if (key > max_key)
                key = 0;
            // Exclusive-Or previous value with current value. This gives 
            // a list of bits that have changed. 
            int diff = last_key ^ key;
            sum = 0;
            for (int i = 0; i < 5; i++)
            {
                // If bit changed, get new random number for corresponding 
                // white_value 
                if ((diff & (1 << i)) != 0)
                    whiteValues[i] = (float)(2 * rnd1.NextDouble() - 1) % (range / 5F);
                sum += whiteValues[i];
            }
            return sum;
        }

    }
}



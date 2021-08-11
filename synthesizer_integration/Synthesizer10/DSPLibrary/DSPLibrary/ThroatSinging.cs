using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using  System.Numerics;
using NAudio.Wave.Asio;
using System.Runtime.InteropServices;

namespace DSPLibrary
{
   public class ThroatSinging : WaveStream
    {
        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double get_throat_singing_tick();
        //public static extern double* get_throat_singing_frame(double* samples, int n_samples);


        public ThroatSinging()
        {
            Frequency = 1D;
            Amplitude = 1f; // let's not hurt our ears  
            n2 = 0;
            SimulationRadius = 2000;
            stopflag = false;
            position = 1;
            onetimeflag = false;
            Bufferlength = 44100;

        }



        bool onetimeflag;

        
        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        static int n2;
        public double SimulationRadius { get; set; }
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
            //double[] temp4 = new double[sampleCount / 4];
            // your code goes here



            //Random rand = new Random();
            for (int n1 = 0; n1 < sampleCount / 4; n1++)
            {
                double temp4 = get_throat_singing_tick();
                byte[] bytes = BitConverter.GetBytes((float)(temp4));
                //float randomFloat = (float)rand.NextDouble();
                //byte[] bytes = BitConverter.GetBytes(randomFloat);
                buffer[n1 * 4 + 0] = bytes[0];
                buffer[n1 * 4 + 1] = bytes[1];
                buffer[n1 * 4 + 2] = bytes[2];
                buffer[n1 * 4 + 3] = bytes[3];
                n2++;
                
            }
            return sampleCount;

        }
    }
    }

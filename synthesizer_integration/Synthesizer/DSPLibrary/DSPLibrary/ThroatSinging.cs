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

        public ThroatSinging()
        {
            Frequency = 1D;
            Amplitude = 1f; // let's not hurt our ears  
            n2 = 0;
            SimulationRadius = 2000;
            stopflag = false;
            position = 1;
            onetimeflag = false;
        

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
            double[] temp4 = new double[sampleCount / 4];
            // your code goes here

            for (int t_i = 0; t_i < sampleCount / 4; t_i++)
            {
                temp4[t_i] = get_throat_singing_tick();
                System.Console.WriteLine(temp4[t_i]);
            }


            for (int n1 = 0; n1 < sampleCount / 4; n1++)
            {
                byte[] bytes = BitConverter.GetBytes((float)(temp4[n1]));
                buffer[n1 * 4 + 0] = bytes[0];
                buffer[n1 * 4 + 1] = bytes[1];
                buffer[n1 * 4 + 2] = bytes[2];
                buffer[n1 * 4 + 3] = bytes[3];
                
            }
            return sampleCount;

        }
    }
    }

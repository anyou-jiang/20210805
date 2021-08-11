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

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int throatSinging_init_2();

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int throatSinging_push_sine(double amplitude, double frequency);

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


            //Initialize the dll
            throatSinging_init_2();

            //double[] frequencies = { 261.63, 293.66, 329.63, 349.23, 392.00, 440.00, 493.88 };
            //double[] amplitudes = { 1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4 };
            double[] frequencies = { 261.63, 293.66, 329.63, 349.23, 392.00, 440.00, 493.88 };
            double[] amplitudes = { 1, 0.1, 0.1, 0.1, 0.1, 0.1, 1 };
            double[] amplitudes_norm = { 0, 0, 0, 0, 0, 0, 0 };
            double sum = 0.0;
            double margin = 0.1;
            for (int tone_i = 0; tone_i < 7; tone_i++)
            {
                sum = sum + amplitudes[tone_i];
            }
            for (int tone_i = 0; tone_i < 7; tone_i++)
            {
                amplitudes_norm[tone_i] = (1 - margin) * amplitudes[tone_i] / sum;
                System.Console.WriteLine(amplitudes_norm[tone_i]);
            }

            for (int tone_i = 0; tone_i < 7; tone_i++)
            {
                throatSinging_push_sine(amplitudes_norm[tone_i], frequencies[tone_i]);
            }
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

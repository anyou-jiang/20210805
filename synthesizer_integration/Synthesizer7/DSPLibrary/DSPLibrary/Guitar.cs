using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace DSPLibrary
{
  public  class Guitar : WaveStream
    {
        public Guitar()
        {
            Frequency = 0;
            n2 = 0;
            n3 = 0;
            n4 = 0;
            rnd1 = new System.Random();
            t = 0;
            Oct1 =7;
        
        }

        public double Frequency { get; set; }
        public double Amplitude { get; set; }
        static int n2;
        static int n3;
        static int n4;
        bool onetimeflag;
        public long Bufferlength { get; set; }
        private long position;
        
        public override long Length { get { return Bufferlength; } }
        public override WaveFormat WaveFormat { get { return WaveFormat.CreateIeeeFloatWaveFormat(44100, 2); } }
        Random rnd1;
        static int t;
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

        private int i = 0;
        // buffer size is 52920 and so is sampleCount
        // oddset is 0

            bool flag;
            private int Oct1;
        public override int Read(byte[] buffer, int offset, int sampleCount)
        {
            int temp = 0;
            
            if (i < Song.Count)
            {
                Frequency = Song[i];
                n2 = 0;
                n4 = 0;
                t = 0;
                temp = PlayG(buffer, offset, sampleCount);
                i++;

            }
            else
            {
                i = 0;
                    Dispose(); // stop playing
                    return 0; // stop playing
                
            }

         

            return temp;

        }


        int PlayG(byte[] buffer, int offset, int sampleCount)
        {
            float[] X;
            float[] temp;

            // BufferLength set to the min I want the sound to play for

            int N = (int)(44100D / Frequency); // size of buffer for the karplus-strong algorithm
            float sum;// temp holding place
                      // below only happens once 
                      // it make the karplus-strong algorithm buffer

                      X = new float[N];
                      temp = new float[N + 1];


            if (n2 == 0) // flag
            {
                
                for (int i = 0; i < N; i++)
                {
                    X[i] = (float)(rnd1.Next(-5, 5)) + (float)(0.5 * Math.Sin(i * Frequency * Math.PI / 44100D));// random number from -0.5 - 0.5
                                                                                                                 // X[i] = (float)(0.5 * Math.Sin(i * LeftFrequencyOutPut * Math.PI / 44100D));
                }
                n2 = 1; // flag 
            }
            // karplus-strong algorithm low pass filer
            for (int r = 0; r < sampleCount / 4; r++)
            {
                if (t >= (N - 1))
                {
                    t = 0;
                    n4 = 1;
                }
                
                if (n4 == 0)
                {
                    sum = X[t];
                    temp[t] = sum;
                }
                else
                {
                    sum = (float)(((temp[t] + temp[t + 1]) * 0.5 * 0.998));// low pass SimulationRadius and decay factor of 0,996
                    temp[t] = sum;

                }
                t++; // counter this is a static counter

                // send sum to the butter that so it can be played later
                byte[] bytes = BitConverter.GetBytes(sum);
                buffer[r * 4 + 0] = bytes[0];
                buffer[r * 4 + 1] = bytes[1];
                buffer[r * 4 + 2] = bytes[2];
                buffer[r * 4 + 3] = bytes[3];
                n3++;
            }
            return sampleCount;// end of Read sound start playing no
        }

        private List<double> Song;
        void SetUpMario()
        {

             Song = new List<double>();
            Song.Add(findNote("E", Oct1));
            Song.Add(findNote("E", Oct1));
            Song.Add(findNote("E", Oct1));

            Song.Add(findNote("C", Oct1));
            Song.Add(findNote("E", Oct1));
            Song.Add(findNote("G", Oct1));
            Song.Add(findNote("G", Oct1));

            Song.Add(findNote("C", Oct1));
            Song.Add(findNote("G", Oct1));
            Song.Add(findNote("E", Oct1));

            Song.Add(findNote("A", Oct1));
            Song.Add(findNote("B", Oct1));
            Song.Add(findNote("C", Oct1));
            Song.Add(findNote("A", Oct1));
            Song.Add(findNote("G", Oct1));
            Song.Add(findNote("E", Oct1));
            Song.Add(findNote("G", Oct1));
            Song.Add(findNote("A", Oct1));

            Song.Add(findNote("F", Oct1));
            Song.Add(findNote("G", Oct1));
            Song.Add(findNote("E", Oct1));
            Song.Add(findNote("C", Oct1));

            Song.Add(findNote("D", Oct1));
            Song.Add(findNote("B", Oct1));


        }

        double findNote(string te, int oct)
        {
            double note = 0.0;
            string temp = te + oct;

            switch (temp)
            {
                case "A0":
                    note = 27.50;
                    break;
                case "A1":
                    note = 55.00;
                    break;
                case "A2":
                    note = 110.0;
                    break;
                case "A3":
                    note = 220.0;
                    break;
                case "A4":
                    note = 440.0;
                    break;
                case "A5":
                    note = 880.0;
                    break;
                case "A6":
                    note = 1760;
                    break;
                case "A7":
                    note = 3520;
                    break;
                case "A8":
                    note = 7040;
                    break;

                case "B0":
                    note = 30.87;
                    break;
                case "B1":
                    note = 61.74;
                    break;
                case "B2":
                    note = 123.5;
                    break;
                case "B3":
                    note = 246.9;
                    break;
                case "B4":
                    note = 493.9;
                    break;
                case "B5":
                    note = 987.8;
                    break;
                case "B6":
                    note = 1976;
                    break;
                case "B7":
                    note = 3951;
                    break;
                case "B8":
                    note = 7902;
                    break;

                case "C0":
                    note = 16.35;
                    break;
                case "C1":
                    note = 32.70;
                    break;
                case "C2":
                    note = 65.41;
                    break;
                case "C3":
                    note = 130.8;
                    break;
                case "C4":
                    note = 261.6;
                    break;
                case "C5":
                    note = 523.3;
                    break;
                case "C6":
                    note = 1047;
                    break;
                case "C7":
                    note = 2093;
                    break;
                case "C8":
                    note = 4186;
                    break;

                case "D0":
                    note = 18.35;
                    break;
                case "D1":
                    note = 36.71;
                    break;
                case "D2":
                    note = 73.42;
                    break;
                case "D3":
                    note = 146.8;
                    break;
                case "D4":
                    note = 293.7;
                    break;
                case "D5":
                    note = 587.3;
                    break;
                case "D6":
                    note = 1175;
                    break;
                case "D7":
                    note = 2349;
                    break;
                case "D8":
                    note = 4699;
                    break;

                case "E0":
                    note = 20.60;
                    break;
                case "E1":
                    note = 41.20;
                    break;
                case "E2":
                    note = 82.41;
                    break;
                case "E3":
                    note = 164.8;
                    break;
                case "E4":
                    note = 329.6;
                    break;
                case "E5":
                    note = 659.3;
                    break;
                case "E6":
                    note = 1319;
                    break;
                case "E7":
                    note = 2637;
                    break;
                case "E8":
                    note = 5274;
                    break;

                case "E20":
                    note = 20.60;
                    break;
                case "E21":
                    note = 41.20;
                    break;
                case "E22":
                    note = 82.41;
                    break;
                case "E23":
                    note = 164.8;
                    break;
                case "E24":
                    note = 329.6;
                    break;
                case "E25":
                    note = 659.3;
                    break;
                case "E26":
                    note = 1319;
                    break;
                case "E27":
                    note = 2637;
                    break;
                case "E28":
                    note = 5274;
                    break;

                case "F0":
                    note = 21.83;
                    break;
                case "F1":
                    note = 43.65;
                    break;
                case "F2":
                    note = 87.31;
                    break;
                case "F3":
                    note = 174.6;
                    break;
                case "F4":
                    note = 349.2;
                    break;
                case "F5":
                    note = 698.5;
                    break;
                case "F6":
                    note = 1397;
                    break;
                case "F7":
                    note = 2794;
                    break;
                case "F8":
                    note = 5588;
                    break;

                case "F20":
                    note = 21.83;
                    break;
                case "F21":
                    note = 43.65;
                    break;
                case "F22":
                    note = 87.31;
                    break;
                case "F23":
                    note = 174.6;
                    break;
                case "F24":
                    note = 349.2;
                    break;
                case "F25":
                    note = 698.5;
                    break;
                case "F26":
                    note = 1397;
                    break;
                case "F27":
                    note = 2794;
                    break;
                case "F28":
                    note = 5588;
                    break;

                case "G0":
                    note = 24.50;
                    break;
                case "G1":
                    note = 49.00;
                    break;
                case "G2":
                    note = 98.00;
                    break;
                case "G3":
                    note = 196.0;
                    break;
                case "G4":
                    note = 392.0;
                    break;
                case "G5":
                    note = 784.0;
                    break;
                case "G6":
                    note = 1568;
                    break;
                case "G7":
                    note = 3136;
                    break;
                case "G8":
                    note = 6272;
                    break;

                case "Eb0":
                    note = 19.45;
                    break;
                case "Eb1":
                    note = 38.89;
                    break;
                case "Eb2":
                    note = 77.78;
                    break;
                case "Eb3":
                    note = 155.6;
                    break;
                case "Eb4":
                    note = 311.1;
                    break;
                case "Eb5":
                    note = 622.3;
                    break;
                case "Eb6":
                    note = 1245;
                    break;
                case "Eb7":
                    note = 2489;
                    break;
                case "Eb8":
                    note = 4699;
                    break;

                case "Bb0":
                    note = 29.14;
                    break;
                case "Bb1":
                    note = 58.27;
                    break;
                case "Bb2":
                    note = 116.5;
                    break;
                case "Bb3":
                    note = 233.1;
                    break;
                case "Bb4":
                    note = 466.2;
                    break;
                case "Bb5":
                    note = 932.3;
                    break;
                case "Bb6":
                    note = 1865;
                    break;
                case "Bb7":
                    note = 3729;
                    break;
                case "Bb8":
                    note = 7459;
                    break;
                default:
                    note = 0;
                    break;
            }


            return note;
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace DSPLibrary
{
  public  class Isochronic_binaural_Beats : WaveStream
        {
            public Isochronic_binaural_Beats()
            {
                Amplitude = 1;
                Frequency = 100;
                tempSample = 0;
                Beat = 0;
                Frequency_switch = true;
            }
            bool onetimeflag;
            public bool stopflag { get; set; }
            public double Frequency { get; set; }
            public double Frequency2 { get; set; }
            public double Beat { get; set; }
            public double Beat2 { get; set; }
            public double Amplitude { get; set; }
            public double Amplitude2 { get; set; }

            public long Bufferlength { get; set; }
            public override long Length { get { return Bufferlength; } }
            public override WaveFormat WaveFormat { get { return WaveFormat.CreateIeeeFloatWaveFormat(44100, 2); } }
            private long position;
            static int n2;
            static float tempSample;
            public bool stopPlaying_flag { get; set; }
            public override long Position
            {
                get
                {
                    // so far, we'll just return the number of bytes read
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
                float temp1;

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

                float w = 0;
                for (int i = 0; i < (sampleCount / 4); i++)
                {

                    if (Frequency_switch == true)
                    {

                        w = (float)(1D * Math.Sin(n2 * Math.PI * Beat / 44100D));
                        if (w < 0)
                        {
                            w = 0;
                        }

                        temp1 = (float)(w * Amplitude * Math.Sin((Math.PI * Frequency * n2) / 44100D));
                        Frequency_switch = false;
                    }
                    else
                    {


                        w = (float)(1D * Math.Sin(n2 * Math.PI * Beat2 / 44100D));
                        if (w < 0)
                        {
                            w = 0;
                        }

                        temp1 = (float)(w * Amplitude2 * Math.Sin((Math.PI * Frequency2 * n2) / 44100D));
                        Frequency_switch = true;

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




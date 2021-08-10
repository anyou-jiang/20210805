using System;
using System.Collections.Generic;
using System.Numerics;

namespace DSPLibrary
{
    public class DspTools
    {
        private double N;
        private int R;
        private Complex[] F;
        private double Fs;
        private Complex[] x;


        public DspTools(double[] DSP1, int f1)
        {
            N = DSP1.Length;
            R = DSP1.Length;
            F = new Complex[DSP1.Length];
            Fs = (double)f1;
            x = new Complex[DSP1.Length];

            for (int v = 0; v < N; v++)
            {
                x[v] = DSP1[v];
            }
        }

        public Complex Complex1(int K, int N3)
        {
            Complex W = Complex.Pow((Complex.Exp(-1 * Complex.ImaginaryOne * (2.0 * Math.PI / N3))), K);
            return W;

        }


        public int Frequencies(double[] freq, double[] Ctemp)
        {

            int counter = 0;
            for (int i = 0; i < R; i++) // R is the length of the input signal 
            {
                if (((i / N) * Fs * 2) >= (Fs / 2))
                {
                    return counter;

                }
                if (Math.Abs(F[i].Magnitude) > 30000)// 300,000
                {
                    freq[counter] = (i / N) * Fs * 2;
                    Ctemp[counter] = F[i].Magnitude;
                    counter++;

                }
            }
            return counter;
        }



        public void FFT1()
        {
            F = FFT(x);
        }


        public Complex[] FFT(Complex[] x)
        {
            int N2 = x.Length;
            Complex[] X = new Complex[N2];
            if (N2 == 1)  // checking the length of the DFT, remember we want to break it 
            {          // down in to a many two point butterflies 
                return x;
            }
            Complex[] odd = new Complex[N2 / 2];  // breaking up the even and odd 
            Complex[] even = new Complex[N2 / 2]; // in two different butterflies 
            Complex[] Y_Odd = new Complex[N2 / 2];
            Complex[] Y_Even = new Complex[N2 / 2];


            for (int t = 0; t < N2 / 2; t++)
            {
                even[t] = x[t * 2];   // putting the even indexes into the even array
                odd[t] = x[(t * 2) + 1]; //putting the odd indexes into the odd array
            }
            Y_Even = FFT(even);// computer the even butterfly 1st  
            Y_Odd = FFT(odd);// computer the odd butterfly
            Complex temp4;

            for (int k = 0; k < (N2 / 2); k++)
            {
                temp4 = Complex1(k, N2); // this function does the Wkn math 
                X[k] = Y_Even[k] + (Y_Odd[k] * temp4); // the math for the top part of
                // the buffer fly
                X[k + (N2 / 2)] = Y_Even[k] - (Y_Odd[k] * temp4); // the math of the             
                //bottom of the butterfly 
            }

            return X;
        }


        public void LowPassFilter(double frequency)
        {
            int i = 0;
            int t = R / 2;
            double temp;
            for (; i < R / 2; i++)
            {

                temp = ((i / N) * Fs);
                if ((i / N) * Fs > frequency)
                {
                    F[i] = 0D;
                }
            }
            for (; i < R; i++)
            {
                temp = ((t / N) * Fs);
                if ((t / N) * Fs > frequency)
                {
                    F[i] = 0D;
                }
                t--;
            }
        }




        public void HighPassFilter(double frequency)
        {
            int i = 0;
            int t = R / 2;
            double temp;
            for (; i < R / 2; i++)
            {

                temp = ((i / N) * Fs);
                if ((i / N) * Fs < frequency)
                {
                    F[i] = 0D;
                }
            }
            for (; i < R; i++)
            {
                temp = ((t / N) * Fs);
                if ((t / N) * Fs < frequency)
                {
                    F[i] = 0D;
                }
                t--;
            }
        }



        public void Reverse()
        {
            int counter = R - 1;
            Complex[] temp = new Complex[R];
            temp[0] = F[0];
            // reversing the FFT array
            for (int u = 1; u < R; u++)
            {
                temp[u] = F[counter];
                counter--;
            }
            F = FFT(temp); // calling the fft again 
        }
        // function does the 1/N part of the IDFT. To enter value in the buffer.

        public List<double> IFFT1()
        {
            List<double> returntemp = new List<double>();
            for (int v = 0; v < R; v++)
            {
                returntemp.Add(F[v].Real / N);
                
            }

            return returntemp;
        }

    }
}

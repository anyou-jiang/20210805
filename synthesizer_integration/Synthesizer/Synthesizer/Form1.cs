using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSPLibrary;
using NAudio.Wave;
using System.Runtime.InteropServices;

namespace Synthesizer
{
    public partial class Form1 : Form
    {

        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int my_sythesize([MarshalAs(UnmanagedType.LPStr)] string path);
        public static extern int throatSinging_play(double amplitude, double frequency, long n_samples);

        public Form1()
        {
            InitializeComponent();
        }

        // Private class variables of Form1 
        WaveOut _waveOut;
        BrownNoise _brownNoise;
        PinkNoise _pinkNoise;
        WhiteNoise _whiteNoise;
        private BinauralBeats _ISBeats;

        private ThroatSinging _throatSinging;

        
        private void HearingTestPlayBtn_Click(object sender, EventArgs e)
        {

        }



        private void WhiteNoiseSaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog save2 = new SaveFileDialog();
            save2.Filter = "Wave file (*.wav)|*.wav;";
           _ISBeats.BufferLength  = (long)(Convert.ToDouble(timeTxt.Text) * 44100D * 60D * 2D);
           _ISBeats.stopflag = false;
           _ISBeats.Position = 0;

            if (save2.ShowDialog() != DialogResult.OK) return;
            try
            {

                WaveFileWriter.CreateWaveFile(save2.FileName, _ISBeats);

            }
            catch (Exception o)
            {
                MessageBox.Show(o.Message);
            }
            MessageBox.Show("File Saved");


        }

        private void BrownNoisePlayBtn_Click(object sender, EventArgs e)
        {
            _brownNoise = new BrownNoise();
            _brownNoise.Amplitude = Convert.ToDouble(AmplitudeTxt.Text);
            _brownNoise.Frequency = Convert.ToDouble(FrequencyForLeftSpeakerTxt.Text);
           // _brownNoise.LowPass = Convert.ToDouble(LowPassTxt.Text);
         //   _brownNoise.HighPass = Convert.ToDouble(HighPassTxt.Text);
            _brownNoise.Amplifier = Convert.ToDouble(FrequencyForRightSpeakerTxt.Text);
            _brownNoise.Bufferlength = Int32.MaxValue;
            _waveOut = new WaveOut();
            WaveChannel32 temp = new WaveChannel32(_brownNoise);
            temp.PadWithZeroes = false;
            _waveOut.Init(temp);
            _waveOut.Play();

        }

        private void BrownNoiseSaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog save2 = new SaveFileDialog();
            save2.Filter = "Wave file (*.wav)|*.wav;";
            _brownNoise.Bufferlength = (long)(Convert.ToDouble(timeTxt.Text) * 44100D * 60D * 2D);
            _brownNoise.stopflag = false;
            _brownNoise.Position = 0;

            if (save2.ShowDialog() != DialogResult.OK) return;
            try
            {
                WaveFileWriter.CreateWaveFile(save2.FileName, _brownNoise);
            }
            catch (Exception o)
            {
                MessageBox.Show(o.Message);
            }
            MessageBox.Show("File Saved");

        }

        private void PinkNoisePlayBtn_Click(object sender, EventArgs e)
        {
            _pinkNoise = new PinkNoise();
            _pinkNoise.Amplitude = Convert.ToDouble(AmplitudeTxt.Text);
            _pinkNoise.Frequency = Convert.ToDouble(FrequencyForLeftSpeakerTxt.Text);
        //    _pinkNoise.LowPass = Convert.ToDouble(LowPassTxt.Text);
         //   _pinkNoise.HighPass = Convert.ToDouble(HighPassTxt.Text);
        //    _pinkNoise.Amplifier = Convert.ToDouble(FrequencyForRightSpeakerTxt.Text);
            _pinkNoise.Bufferlength = Int32.MaxValue;
            _waveOut = new WaveOut();
            WaveChannel32 temp = new WaveChannel32(_pinkNoise);
            temp.PadWithZeroes = false;
            _waveOut.Init(temp);
            _waveOut.Play();

        }

        
        private void PinkNoiseSaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog save2 = new SaveFileDialog();
            save2.Filter = "Wave file (*.wav)|*.wav;";
            _pinkNoise.Bufferlength = (long)(Convert.ToDouble(timeTxt.Text) * 44100D * 60D * 2D);
            _pinkNoise.StopFlag = false;
            _pinkNoise.Position = 0;

            if (save2.ShowDialog() != DialogResult.OK) return;
            try
            {
                WaveFileWriter.CreateWaveFile(save2.FileName, _pinkNoise);
            }
            catch (Exception o)
            {
                MessageBox.Show(o.Message);
            }
            MessageBox.Show("File Saved");
        }

        // The Stop button, It stops all the sounds.
        // one button to stop them all

        private async void StopBtn_Click(object sender, EventArgs e)
        {

            while (_waveOut.Volume > 0)
            {

                _waveOut.Volume *= 0.1F;

                await Task.Delay(500);
            }

            if (_waveOut != null)
            {
                _waveOut.Stop();
                try
                {
                    _waveOut.Dispose();
                    _waveOut = null;

                }
                catch (System.InvalidOperationException)
                {
                    _waveOut = null;

                }
            }
        }

        private void RainFallPlayBtn_Click(object sender, EventArgs e)
        {
            _throatSinging = new ThroatSinging();
            _throatSinging.Bufferlength = Int32.MaxValue;
            _throatSinging.Amplitude = Convert.ToDouble(AmplitudeTxt.Text);
            _throatSinging.Frequency = Convert.ToDouble(FrequencyForLeftSpeakerTxt.Text);
            //  _throatSinging.SimulationRadius = Convert.ToDouble(LowPassTxt.Text);
            //WaveChannel32 temp = new WaveChannel32(_throatSinging);
            //temp.PadWithZeroes = false;
            //_waveOut = new WaveOut();
            //_waveOut.Init(temp);
            //_waveOut.Play();
            const int sampling_rate = 44100;
            double duration = 2.0;  //sec
            int status = throatSinging_play(_throatSinging.Amplitude, _throatSinging.Frequency, (long)(Math.Floor(sampling_rate * duration)));
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save2 = new SaveFileDialog();
            save2.Filter = "Wave file (*.wav)|*.wav;";

            _ISBeats.Amplitude = Convert.ToDouble(AmplitudeTxt.Text);
            _ISBeats.LeftFrequencyOutPut = Convert.ToDouble(FrequencyForLeftSpeakerTxt.Text);
            _ISBeats.BufferLength = (long)(Convert.ToDouble(timeTxt.Text) * 44100D * 60D * 2D);
            _ISBeats.stopflag = false;
            _ISBeats.Position = 0;


            if (save2.ShowDialog() != DialogResult.OK) return;
            try
            {
                WaveFileWriter.CreateWaveFile(save2.FileName, _ISBeats);
            }
            catch (Exception o)
            {
                MessageBox.Show(o.Message);
            }
            MessageBox.Show("File Saved");
        }

        private void FrequencyForLeftSpeakerTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}








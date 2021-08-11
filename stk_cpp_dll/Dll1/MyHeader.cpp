// crtsine.cpp STK tutorial program
#include "pch.h" 
#include <vector>
#include <iostream>
#include <fstream>
#include <sstream>
#include <memory>
#include "Scalable_SineWave.h"
#include "RtAudio.h"
#include "MyHeader.h"

using namespace::std;

using namespace stk;

struct TickData {
    std::vector<std::shared_ptr<Scalable_SineWave>> sinewaves;
    long counter;
    long total_samples;
    bool done;

    // Default constructor.
    TickData(std::vector<std::shared_ptr<Scalable_SineWave>> waves)
        :sinewaves(waves), counter(0), total_samples(0), done(false) {}
};

// This tick() function handles sample computation only.  It will be
// called automatically when the system needs a new buffer of audio
// samples.
int tick(void* outputBuffer, void* inputBuffer, unsigned int nBufferFrames,
    double streamTime, RtAudioStreamStatus status, void* dataPointer)
{
    TickData* tickData = (TickData*)dataPointer;
    std::vector<std::shared_ptr<Scalable_SineWave>>* sines = &(tickData->sinewaves);
    
    register StkFloat* samples = (StkFloat*)outputBuffer;

    const unsigned n_sine = sines->size();
    const double amp_margin = 0.1;
    for (unsigned int i = 0; i < nBufferFrames; i++)
    {
        *samples = 0.0;
        for (unsigned sine_i = 0; sine_i < n_sine; sine_i++)
        {
            *samples = *samples + (1.0 - amp_margin) * (*sines)[sine_i]->tick();
        }
        samples++;
        tickData->counter++;
    }

    if (tickData->counter >= tickData->total_samples) {
        tickData->done = true;
    }

    return 0;
}

unsigned n_sine_throat_singing = 0;
std::vector<std::shared_ptr<Scalable_SineWave>> sines_throat_singing;

int throatSinging_push_sine(double amplitude, double frequency)
{
    int status = 0;
    std::shared_ptr<Scalable_SineWave> sine = std::shared_ptr<Scalable_SineWave>(new Scalable_SineWave());
    sine->setFrequency(frequency);
    sine->setScale(StkFloat(amplitude));
    sines_throat_singing.push_back(sine);
    std::cout << sines_throat_singing.size() << ", " << frequency << ", " << amplitude << std::endl;
    n_sine_throat_singing++;
    return status;
}

int throatSinging_init_2()
{
    int status = 0;
    n_sine_throat_singing = 0;
    sines_throat_singing = std::vector<std::shared_ptr<Scalable_SineWave>>();

    return status;
}

int throatSinging_init(double amplitude, double frequency)
{
    int status = 0;
    n_sine_throat_singing = 1;
    sines_throat_singing = std::vector<std::shared_ptr<Scalable_SineWave>>();
    for (unsigned sine_i = 0; sine_i < n_sine_throat_singing; sine_i++)
    {
        std::shared_ptr<Scalable_SineWave> sine = std::shared_ptr<Scalable_SineWave>(new Scalable_SineWave());
        sine->setFrequency(frequency);
        sine->setScale(StkFloat(amplitude));
        sines_throat_singing.push_back(sine);
    }
    return status;
}


double* get_throat_singing_frame(double* samples, int n_samples) {
    for (int sample_i = 0; sample_i < n_samples; sample_i++)
    {
        samples[sample_i] = get_throat_singing_tick();
    }
    return samples;
}

double get_throat_singing_tick() {
    const double amp_margin = 0.1;
    double sample = 0.0;
    for (unsigned sine_i = 0; sine_i < n_sine_throat_singing; sine_i++)
    {
        sample = sample + (1.0 - amp_margin) * sines_throat_singing[sine_i]->tick();
    }
    return sample;
}



int throatSinging_play(double amplitude, double frequency, long n_samples)
{
    unsigned n_sine = 1;
    std::vector<std::shared_ptr<Scalable_SineWave>> sines;
    for (unsigned sine_i = 0; sine_i < n_sine; sine_i++)
    {
        std::shared_ptr<Scalable_SineWave> sine = std::shared_ptr<Scalable_SineWave>(new Scalable_SineWave());
        sine->setFrequency(frequency);
        sine->setScale(StkFloat(amplitude));
        sines.push_back(sine);
    }

    // Set the global sample rate
    Stk::setSampleRate(44100.0);

    RtAudio dac;
    // Figure out how many bytes in an StkFloat and setup the RtAudio stream.
    RtAudio::StreamParameters parameters;
    parameters.deviceId = dac.getDefaultOutputDevice();
    parameters.nChannels = 1;
    RtAudioFormat format = (sizeof(StkFloat) == 8) ? RTAUDIO_FLOAT64 : RTAUDIO_FLOAT32;
    unsigned int bufferFrames = RT_BUFFER_SIZE;
    TickData tickData(sines);
    tickData.total_samples = n_samples;


    int status = 0;
    try {
        //dac.openStream(&parameters, NULL, format, (unsigned int)Stk::sampleRate(), &bufferFrames, &tick, (void*)&sine);
        dac.openStream(&parameters, NULL, format, (unsigned int)Stk::sampleRate(), &bufferFrames, &tick, (void*)&tickData);
    }
    catch (RtAudioError& error) {
        error.printMessage();
        status = -1;
    }

    try {
        dac.startStream();
    }
    catch (RtAudioError& error) {
        error.printMessage();
        status = -2;
    }

    if (status == 0)
    {
        while (!tickData.done)
        {
            Stk::sleep(100);
        }

        // Shut down the output stream.
        try {
            dac.closeStream();
        }
        catch (RtAudioError& error) {
            error.printMessage();
            status = -3;
            std::cout << "something wrong when closing stream, return with status = " << status << std::endl;
        }
    }
    else
    {
        std::cout << "something wrong when open and start the stream, return with status = " << status << std::endl;
    }

    return status;
}


int my_sythesize(const char* path)
{
    //Read parameters from a file
    fstream input_file;
    vector<double> frequencies;
    vector<double> amplitudes;
    input_file.open(string(path), ios::in); // "C:\\Users\\Edwin\\source\\repos\\Synthesizer-20210804T081432Z-001\\Synthesizer\\Synthesizer\\input.txt", );
    unsigned input_status = 0;
    if (input_file.is_open()) {
        string frq;
        string amp;
        std::getline(input_file, frq);
        std::getline(input_file, amp);
        std::cout << "frequencies = " << frq << std::endl;
        std::cout << "amplitudes = " << amp << std::endl;


        stringstream frq_stream(frq);  // split into double number
        while (frq_stream.good()) {
            string substr;
            std::getline(frq_stream, substr, ',');
            frequencies.push_back(stod(substr));
        }


        stringstream amp_stream(amp);  // split into double number
        while (amp_stream.good()) {
            string substr;
            std::getline(amp_stream, substr, ',');
            amplitudes.push_back(stod(substr));
        }

        if (frequencies.size() != amplitudes.size()) {
            input_status = 1;
            std::cout << "the number of the frequencies should be equal to the number of amplitudes in the input file." << std::endl;
        }
    }
    else
    {
        input_status = 1;
        std::cout << "could not open input.txt in the application folder." << std::endl;
    }

    if (input_status != 0)
    {
        return -1;
    }
    else
    {
        input_file.close();
    }

    const unsigned n_sine = frequencies.size();

    //calculate a normalizer
    double max_sum_amplitude = 0.0;
    for (unsigned sine_i = 0; sine_i < n_sine; sine_i++)
    {
        max_sum_amplitude += amplitudes[sine_i];
    }
    std::cout << "maximum sum of amplitude = " << max_sum_amplitude << std::endl;

    for (unsigned sine_i = 0; sine_i < n_sine; sine_i++)
    {
        amplitudes[sine_i] = amplitudes[sine_i] / max_sum_amplitude;
    }

    std::cout << "After amplitude normalization, the amplitudes are " << std::endl;
    for (unsigned sine_i = 0; sine_i < n_sine; sine_i++)
    {
        std::cout << amplitudes[sine_i] << ", ";
    }
    std::cout << std::endl;

    std::vector<std::shared_ptr<Scalable_SineWave>> sines;
    for (unsigned sine_i = 0; sine_i < n_sine; sine_i++)
    {
        std::shared_ptr<Scalable_SineWave> sine = std::shared_ptr<Scalable_SineWave>(new Scalable_SineWave());
        sine->setFrequency(frequencies[sine_i]);
        sine->setScale(StkFloat(amplitudes[sine_i]));
        sines.push_back(sine);
    }

    // Set the global sample rate
    Stk::setSampleRate(44100.0);

    RtAudio dac;
    // Figure out how many bytes in an StkFloat and setup the RtAudio stream.
    RtAudio::StreamParameters parameters;
    parameters.deviceId = dac.getDefaultOutputDevice();
    parameters.nChannels = 1;
    RtAudioFormat format = (sizeof(StkFloat) == 8) ? RTAUDIO_FLOAT64 : RTAUDIO_FLOAT32;
    unsigned int bufferFrames = RT_BUFFER_SIZE;
    TickData tickData(sines);

    int status = 0;
    try {
        //dac.openStream(&parameters, NULL, format, (unsigned int)Stk::sampleRate(), &bufferFrames, &tick, (void*)&sine);
        dac.openStream(&parameters, NULL, format, (unsigned int)Stk::sampleRate(), &bufferFrames, &tick, (void*)&tickData);
    }
    catch (RtAudioError& error) {
        error.printMessage();
        status = -1;
    }

    try {
        dac.startStream();
    }
    catch (RtAudioError& error) {
        error.printMessage();
        status = -2;
    }
 
    if (status == 0)
    {
        while (!tickData.done)
        {
            Stk::sleep(100);
        }

        // Shut down the output stream.
        try {
            dac.closeStream();
        }
        catch (RtAudioError& error) {
            error.printMessage();
            status = -3;
            std::cout << "something wrong when closing stream, return with status = " << status << std::endl;
        }
    }
    else
    {
        std::cout << "something wrong when open and start the stream, return with status = " << status << std::endl;
    }

    return status;
}
// ConsoleApplication1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "MyHeader.h"

int main()
{
	std::cout << "Hello World!\n";
	//string solutioinDir = string(MY_SOLUTIONDIR);
	//string input_file = solutioinDir.append("input.txt");
	//const char* file_chars = input_file.c_str();
	//my_sythesize(file_chars);
	//double amplitude = 0.5;
	//double frequency = 300;
	//long n_samples = 44100 * 2;
	//int status = throatSinging_play(amplitude, frequency, n_samples);

	//double amplitude = 0.5;
	//double frequency = 300;
	//long n_samples = 44100 * 2;
	//int status = throatSinging_init(amplitude, frequency);
	//for (unsigned sample_i = 0; sample_i < n_samples; sample_i++)
	//{
	//	std::cout << get_throat_singing_tick() << std::endl;
	//}

	double frequences[] = { 261.63, 293.66, 329.63, 349.23, 392.00, 440.00, 493.88 };
	double amplitudes[] = { 1, 0.9, 0.8, 0.7, 0.6, 0.5, 0.4 };
	long n_samples = 44100 * 2;
	int status = throatSinging_init_2();

	double amplitudes_norm[7];
	double sum = 0.0;
	for (unsigned tone_i = 0; tone_i < 7; tone_i++)
	{
		sum = sum + amplitudes[tone_i];
	}
	for (unsigned tone_i = 0; tone_i < 7; tone_i++)
	{
		amplitudes_norm[tone_i] = amplitudes[tone_i] / sum;
		std::cout << amplitudes_norm[tone_i] << std::endl;
	}

	for (unsigned tone_i = 0; tone_i < 7; tone_i++)
	{
		throatSinging_push_sine(amplitudes_norm[tone_i], frequences[tone_i]);
	}

	for (unsigned sample_i = 0; sample_i < n_samples; sample_i++)
	{
		std::cout << get_throat_singing_tick() << std::endl;
	}

}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file

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
	double amplitude = 0.5;
	double frequency = 300;
	long n_samples = 44100 * 2;
	int status = throatSinging_play(amplitude, frequency, n_samples);


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

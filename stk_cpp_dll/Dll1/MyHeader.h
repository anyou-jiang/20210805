#pragma once
#include <string>
using std::string;
#ifdef DLL1_EXPORTS
#define DLL1_API __declspec(dllexport)
#else
#define DLL1_API __declspec(dllimport)
#endif

extern "C" DLL1_API int my_sythesize(const char* path);
extern "C" DLL1_API int throatSinging_play(double amplitude, double frequency, long n_samples);
extern "C" DLL1_API int throatSinging_init(double amplitude, double frequency);
extern "C" DLL1_API double get_throat_singing_tick();



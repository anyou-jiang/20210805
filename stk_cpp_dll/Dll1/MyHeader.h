#pragma once
#include <string>
using std::string;
#ifdef DLL1_EXPORTS
#define DLL1_API __declspec(dllexport)
#else
#define DLL1_API __declspec(dllimport)
#endif

extern "C" DLL1_API int my_sythesize(const char* path);



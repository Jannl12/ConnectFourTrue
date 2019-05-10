#pragma once

#ifdef EVAL_DLL_EXPORTS
	#define EVAL_DLL __declspec(dllexport)
#else
	#define EVAL_DLL __declspec(dllimport)
#endif

extern "C" EVAL_DLL int EvaluateBoard(
	unsigned long long playerOneBoard, unsigned long long playerTwoBoard, int inputValues[]);

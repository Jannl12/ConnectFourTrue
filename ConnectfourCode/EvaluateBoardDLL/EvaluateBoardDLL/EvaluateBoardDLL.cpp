#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <limits.h>
#include <windows.h>

#define outerFrameBuffer 18446464815071240256
/*111111111111111_1000000_1000000_1000000_1000000_1000000_1000000_1000000
 *outerframe is the long with the following bits flipped:
 *6, 13, 20, 27, 34, 41, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64--
 */

inline int countSetBitsInUlong(unsigned long long inputValue) {
	int returnCount = 0;
	while (inputValue > 0) {
		if ((inputValue & 1) == 1) {
			returnCount++;
		}
		inputValue >>= 1;
	}
	return  returnCount;
}

inline int evalDirection(unsigned long long firstBoard, unsigned long long secondBoard, unsigned long long thirdBoard, unsigned long long fourthBoard, int score) {
	int returnValue = 0;
	int directions[] = { 1, 7, 6, 8 }; //Vertikal, Horizontal, V.Diagonal, H.Diagonal
	for (int i = 0; i < sizeof(directions) / sizeof(int); i++) {
		unsigned long long boardShiftAndAdditionBuffer =
			(firstBoard)                         &
			(secondBoard >> (directions[i]))     &
			(thirdBoard  >> (2 * directions[i])) &
			(fourthBoard >> (3 * directions[i]));
		if (boardShiftAndAdditionBuffer != 0) {
			returnValue += score * countSetBitsInUlong(boardShiftAndAdditionBuffer);
		}
	}
	return returnValue;
}

extern "C" __declspec(dllexport) int EvaluateBoard(unsigned long long playerOneBoard, unsigned long long playerTwoBoard) {

	unsigned long long emptySlotsBitBoard = (ULLONG_MAX ^ (playerOneBoard | playerTwoBoard)) ^ outerFrameBuffer;

	unsigned long long gameBoards[2] = { playerOneBoard, playerTwoBoard };

	int testCase[] = { 0, 1, 4, 9, 1000 };

	int allCombinations[15][4] = { {1,1,1,1},
								{ 1, 1, 1, 0}, { 1, 1, 0, 1}, { 1, 0, 1, 1}, {0, 1, 1, 1},
								{1, 1, 0, 0}, {1, 0, 1, 0}, {1, 0, 0, 1}, {0, 1, 1, 0}, {0, 1, 0, 1}, {0, 0, 1, 1},
								{1, 0, 0, 0}, {0, 1, 0, 0}, {0, 0, 1, 0}, {0, 0, 0, 1} };

	int returnValue = 0;
	for (int playerIterator = 0; playerIterator < 2; playerIterator++) {
		for (int combination = 0; combination < 15; combination++) {
			int evaluationBuffer = evalDirection(
				allCombinations[combination][0] == 1 ? gameBoards[playerIterator] : emptySlotsBitBoard,
				allCombinations[combination][1] == 1 ? gameBoards[playerIterator] : emptySlotsBitBoard,
				allCombinations[combination][2] == 1 ? gameBoards[playerIterator] : emptySlotsBitBoard,
				allCombinations[combination][3] == 1 ? gameBoards[playerIterator] : emptySlotsBitBoard,
				testCase[allCombinations[combination][0] + allCombinations[combination][1] + allCombinations[combination][2] + allCombinations[combination][3]]);
			returnValue += playerIterator == 0 ? evaluationBuffer : -evaluationBuffer;
		}
	}
	return returnValue;
}


	//for (int playerIterator = 0; playerIterator < 2; playerIterator++) {
	//	//1111
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		inputValues[4]);

	//	//111x
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		emptySlotsBitBoard,
	//		inputValues[3]);

	//	//11x1
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		emptySlotsBitBoard,
	//		gameBoards[playerIterator],
	//		inputValues[3]);

	//	//1x11
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator],
	//		emptySlotsBitBoard,
	//		gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		inputValues[3]);

	//	//x111
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard,
	//		gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		inputValues[3]);

	//	//11xx
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator],
	//		gameBoards[playerIterator],
	//		emptySlotsBitBoard,
	//		emptySlotsBitBoard,
	//		inputValues[2]);

	//	//1x1x
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		inputValues[2]);

	//	//1xx1
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		inputValues[2]);

	//	//x11x
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		inputValues[2]);

	//	//x1x1
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		inputValues[2]);

	//	//xx11
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		gameBoards[playerIterator], 
	//		inputValues[2]);

	//	//1xxx
	//	returnValues[playerIterator] += evalDirection(gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		inputValues[2]);

	//	//x1xx
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		inputValues[1]);

	//	//xx1x
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		emptySlotsBitBoard, 
	//		inputValues[1]);

	//	//xxx1
	//	returnValues[playerIterator] += evalDirection(emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		emptySlotsBitBoard, 
	//		gameBoards[playerIterator], 
	//		inputValues[1]);
	//}



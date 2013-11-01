// Platform: Visual Studio 2012
// memory leak.cpp : Defines the entry point for the console application.


#include "stdafx.h"
#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>

#include <iostream>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	int *x=(int*)malloc(sizeof(int)); // uncomment this, then check the output window
	cout << "test";
	//free(x);
	//x=NULL;
	_CrtDumpMemoryLeaks();
	return 0;
}


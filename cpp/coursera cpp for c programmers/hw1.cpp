//==========================================================
//=#=====#=#=====#====#=====#===========================#===
//=#=====#=#==#==#====#==#==#=######=######=#====#=====##===
//=#=====#=#==#==#====#==#==#=#======#======#===#=====#=#===
//=#######=#==#==#====#==#==#=#####==#####==####========#===
//=#=====#=#==#==#====#==#==#=#======#======#==#========#===
//=#=====#=#==#==#====#==#==#=#======#======#===#=======#===
//=#=====#==##=##======##=##==######=######=#====#====#####=
//==========================================================

//Platform:
//    Eclipse CDT Kepler SR1 with Mingw
//
//Objective:
//    Convert this program to C++
//    change to C++ io
//    change to one line comments
//    change defines of constants to const
//    change array to vector<>
//    inline any short function



//=###==================================================
//==#==#====#==####==#======#====#=#####==######==####==
//==#==##===#=#====#=#======#====#=#====#=#======#======
//==#==#=#==#=#======#======#====#=#====#=#####===####==
//==#==#==#=#=#======#======#====#=#====#=#===========#=
//==#==#===##=#====#=#======#====#=#====#=#======#====#=
//=###=#====#==####==######==####==#####==######==####==
//======================================================
#include <iostream>
#include <vector>
using namespace std;






//==#####========================================================
//=#=====#==####==#====#==####==#####===##===#====#=#####==####==
//=#=======#====#=##===#=#========#====#==#==##===#===#===#======
//=#=======#====#=#=#==#==####====#===#====#=#=#==#===#====####==
//=#=======#====#=#==#=#======#===#===######=#==#=#===#========#=
//=#=====#=#====#=#===##=#====#===#===#====#=#===##===#===#====#=
//==#####===####==#====#==####====#===#====#=#====#===#====####==
//===============================================================
const int ARRAY_SZ = 40; // Renamed to a more descriptive name






//=#######===================================================
//=#=======#====#=#====#==####==#####=#==####==#====#==####==
//=#=======#====#=##===#=#====#===#===#=#====#=##===#=#======
//=#####===#====#=#=#==#=#========#===#=#====#=#=#==#==####==
//=#=======#====#=#==#=#=#========#===#=#====#=#==#=#======#=
//=#=======#====#=#===##=#====#===#===#=#====#=#===##=#====#=
//=#========####==#====#==####====#===#==####==#====#==####==
//===========================================================

// --------------- Function to get the sum of vectors of data ---------------
inline void getArraySum(int *ptr, int arraySz, vector<int> vectorData)
    {
    *ptr = 0;
    for (int ctr = 0; ctr < arraySz; ++ctr)
        *ptr += vectorData[ctr]; // enhanced
    }


// --------------- Main Function ---------------
int main(void)
    {
    int accumulatedData = 0;
    vector<int> vArrayData(ARRAY_SZ);

    for (int ctr = 0; ctr < ARRAY_SZ; ++ctr)
        {
        vArrayData[ctr] = ctr;
        }

    getArraySum(&accumulatedData, ARRAY_SZ, vArrayData);
    cout << "sum is " << accumulatedData << endl;
    return 0;
    }


// end of file





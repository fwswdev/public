// Started: 11/3/2013  13:35 PM
// Finished:

#include "stdafx.h"

#define USE_MSVC_COMPILER	1 // set to 0 if not using Microsoft Visual Studio
#define USE_TEST_CASES		0
#if USE_MSVC_COMPILER
#include "stdafx.h"

// This is for memory leak detection //
#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>
#endif

#include <iostream>
#include <list>
#include <vector>
#include <map>
#include <cstdlib>
#include <ctime>
#include <algorithm>
#include <cmath>
using namespace std;

typedef int NodeIdxT;
typedef int NodeValT;

static const NodeValT INVALID_NODE = -1;
static const int NUM_NODES = 50;

// No methods here to emulate struct
class NodeT
{
public:
    NodeValT val;
    vector<NodeIdxT> nodesConnected;
};

class Graph
{
private:

    NodeT mNode[NUM_NODES];
    const NodeT * mNodePtr;
    //NodeT *mNode;

public:
    bool mCheckIfXYConnected(Graph g, NodeIdxT x, NodeIdxT y)
    {
    for (int ctr = 0; ctr < mNode[x].nodesConnected.size(); ctr++)
        {
        if (mNode[x].nodesConnected[ctr] == y)
            return true;
        };

    return false;
    }

public:
    Graph(void)
    {
    mNodePtr = mNode;

    srand(time(NULL));

    for (int ctr = 0; ctr < NUM_NODES; ctr++)
        {
        //mNode[ctr]=0;
        mNode[ctr].val = rand() % 100;
        }
    }

    const NodeT * GetNodes(void)
    {
    return mNodePtr;
    }

    int GetNumVertices(Graph g)
    {
    return NUM_NODES;
    }

    int GetNumEdges(Graph g)
    {
    return 0;
    }

    // adjacent (G, x, y): tests whether there is an edge from node x to node y.
    bool IsAdjacent(Graph g, NodeIdxT x, NodeIdxT y)
    {
    return false;
    }

    // neighbors (G, x): lists all nodes y such that there is an edge from x to y.
    list<NodeIdxT> GetNeighbors(Graph g, NodeIdxT x)
    {
    list<NodeIdxT> locList;
    return locList;
    }

    // add (G, x, y): adds to G the edge from x to y, if it is not there.
    // this will return true if operation is successful, else, return false
    bool AddXY(Graph g, NodeIdxT x, NodeIdxT y)
    {
    if (x == y)
        return false;

    if (mCheckIfXYConnected(g, x, y) == false)
        {
        mNode[x].nodesConnected.push_back(y);
        return true;
        }
    return false;
    }

    // delete (G, x, y): removes the edge from x to y, if it is there.
    // this will return true if operation is successful, else, return false
    bool DeleteXY(Graph g, NodeIdxT x, NodeIdxT y)
    {
    for (int ctr = 0; ctr < mNode[x].nodesConnected.size(); ctr++)
        {
        if (mNode[x].nodesConnected[ctr] == y)
            {
            mNode[x].nodesConnected[ctr] = INVALID_NODE; // TODO: remove the element from the vector
            }
        }
    return false;
    }

    NodeValT GetDistanceDirect(Graph g, NodeIdxT x, NodeIdxT y)
    {
    for (int ctr = 0; ctr < mNode[x].nodesConnected.size(); ctr++)
        {
        if (mNode[x].nodesConnected[ctr] == y)
            return mNode[y].val;
        };
    return INVALID_NODE;
    }

};

//Class ShortestPath

//    vertices(List): list of vertices in G(V,E).
//    path(u, w): find shortest path between u-w and returns the sequence of vertices representing shorest path u-v1-v2-…-vn-w.
//    path_size(u, w): return the path cost associated with the shortest path.

class ShortestPath
{
private:
    Graph *mGraph;

public:
    ShortestPath(Graph *g)
    {
    mGraph = g;
    }

    list<NodeIdxT> GetPath(void)
    {
    list<NodeIdxT> path;

    return path;
    }

    NodeValT GetPathCost(void)
    {
    return 0;
    }

};

int TestCases(void)
{
Graph g;
int sz;
sz = g.GetNumVertices(g);
bool m = g.mCheckIfXYConnected(g, 1, 2);
g.AddXY(g, 1, 2);
m = g.mCheckIfXYConnected(g, 1, 2);

// Shortest Path Algo
const NodeT * nodePtr;
nodePtr = g.GetNodes();
ShortestPath sp(&g);
int distance = g.GetDistanceDirect(g, 0, 2);

return 0;
}

const int STARTING_POINT_IDX = 0;
const int ENDING_POINT_IDX = NUM_NODES - 1;

int AppMain(void)
{
Graph g;

// setup the graph

//for (int ctr = 0; ctr < g.GetNumVertices(g); ctr++)
//    {
//    g.AddXY(g, STARTING_POINT_IDX, ctr);
//    }

g.AddXY(g, STARTING_POINT_IDX, 3);
g.AddXY(g, 3, 49);

// Shortest Path Algo
const NodeT * nodePtr;
nodePtr = g.GetNodes();
ShortestPath sp(&g);
int distance = g.GetDistanceDirect(g, 0, 49);

return 0;
}

///////////////////////////////////////////////////////////////////////////////////////////
// Below are compiler specifics starting point. Don't modify anything below this comment //
///////////////////////////////////////////////////////////////////////////////////////////

#if USE_MSVC_COMPILER

int _tmain(int argc, _TCHAR* argv[])
{
#if USE_TEST_CASES
TestCases();
#else
AppMain();
#endif
_CrtDumpMemoryLeaks();
return 0;
}

#else

int main(void)
    {
#if USE_TEST_CASES
    return TestCases();
#else
    return AppMain();
#endif
    }

#endif

////////////// Samples below

//typedef enum  {MON,TUE,WED} DaysT;
//
//inline DaysT operator+ (DaysT t,DaysT x)
//{
//	return static_cast<DaysT>( (static_cast<int>(t) + 1) % 3  );
//}
//
//
//int AppMain(void)
//{
//	DaysT m=TUE,x=TUE;
//	x= m;
//
//	return 0;
//}

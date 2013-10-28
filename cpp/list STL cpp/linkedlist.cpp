// This was created as an exercise for STL List Implementation using C++
// Compiled and tested using Visual Studio C++ 2010 Express Edition


#include "stdafx.h"

#include <iostream>
#include <string>
#include <list>
using namespace std;



typedef struct
{
    string name;
    int age;
}StudentT;


typedef list <StudentT>  StudentListT; 


StudentT a={"je",10};
StudentT b={"my",30};
StudentT c={"ra",20};
StudentT d={"fa",50};




void AddLocalDataIntoList(StudentListT *lst)
{
	StudentT student={"hm",99};
	lst->push_back(student); // add to the end of the list
	
	student.age=0;
	student.name="None";
}




void TraverseToTheList(StudentListT *lst)
{
StudentListT::iterator it,end;
it=lst->begin();
end=lst->end();

while(it!=end)
	{
		cout <<  it->name << it->age << endl;// << it->age << endl;
		it++;
	}
}


int mymain(void)
{
	StudentListT mList; // create an empty list
	StudentListT::iterator it;

	//Add the two elements two our list
	mList.push_back(a);
	mList.push_back(b);

	// insert item into 2 position at the beginning of our list
	it=mList.begin();
	it++;
	mList.insert(it,d);

	// insert item C into end of our list
	mList.insert(mList.end(),c);


	// call a local data into our list. We don't need any dynamic allocation for this
	AddLocalDataIntoList(&mList);

	// Traverse to the list from the beginning up to end and display them to the console
	TraverseToTheList(&mList);

	cout << "How many data in the list? " << mList.size();

	// let the list's destructor take care of the clean-up
	return 0;
}


int _tmain(int argc, _TCHAR* argv[])
{
	mymain();
	return 0;
}


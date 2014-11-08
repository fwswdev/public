#include <iostream>
#include <boost/algorithm/string.hpp>
using namespace boost;
using namespace std;

int main(void)
{
	cout << " Hello ";
	
	string str1(" hello world! ");
    to_upper(str1);  // str1 == " HELLO WORLD! "
    cout << str1;
    trim(str1);      // str1 == "HELLO WORLD!"
	cout << str1;
    string str2=
       to_lower_copy(
          ireplace_first_copy(
             str1,"hello","goodbye")); // str2 == "goodbye world!"
	cout << str2;
	return 0;
	
}

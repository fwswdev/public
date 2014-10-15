import re

MYSTR="""%------------------test%---------har%---------"""


REGEX="%-+?[a-z]+?%-*"

m=re.search(REGEX,MYSTR)

##re.
m=re.split(REGEX,MYSTR)
print m


m=re.search(REGEX,MYSTR)
print m.group(0)
##print m.group(1)

m=re.findall(REGEX,MYSTR)
print m

##print m.group(1)

m=re.match(REGEX,MYSTR)
##print m
print m.group(0)
##print m.group(1)
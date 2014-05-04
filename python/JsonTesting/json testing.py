import json

FILENAMEDICT="testjsondict.txt"

with open(FILENAMEDICT,"w") as f:
    p=dict()
    p["name"]="Name"
    p["school"]="School"
    json.dump(p,f)


with open(FILENAMEDICT,"r") as f:
    p=json.load(f)
    print p
    print type(p)


FILENAMELIST="testjsonlist.txt"

with open(FILENAMELIST,"w") as f:
    p=[]
    p.append("test")
    p.append("1")
    p.append("2")
    p.append(3)
    json.dump(p,f)


with open(FILENAMELIST,"r") as f:
    p=json.load(f)
    print p
    print type(p)



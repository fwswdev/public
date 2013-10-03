#-------------------------------------------------------------------------------
# Name:        rsa practice.py
# Purpose:
#              Demonstation of RSA of pycrypto Library

# Author:      fwswdev @ github
#
# Created:     03/10/2013
# Copyright:   fwswdev @ github
#-------------------------------------------------------------------------------

from Crypto.PublicKey import RSA
from Crypto import Random

# The story begins.....

##################
##### Alice#######
##################

# Alice instantiates an RSA object with private key and public key

MYFILE = 'rsakey.txt'
ENCFILE = 'enc.txt'
random_generator = Random.new().read
alicekey = RSA.generate(1024, random_generator)

# now Alice will send the file which contains the public key to Bob
public_key = alicekey.publickey()

with open(MYFILE,'w') as f:
    rsapubkeytxt=public_key.exportKey()
    f.write(rsapubkeytxt)
    print "Alice will send this file:\n", rsapubkeytxt , '\n'


##################
##### Bob ########
##################

# now Bob has the public key and he opens the file
bobKey=None
with open(MYFILE,'r') as f:
    rsapubkeytxt=f.read()
    bobKey= RSA.importKey(rsapubkeytxt)
    print "Bob reads the file:\n", rsapubkeytxt , '\n'

# Bob encrypts a msg using public key
BOBS_MESSAGE_TO_ALICE='Alice, will you marry me?'
mynewencmsg=bobKey.encrypt(BOBS_MESSAGE_TO_ALICE,32)

# Bob generates a file with the encrypted msg and will send to alice
with open(ENCFILE,'wb') as f:
    f.write(mynewencmsg[0])


##################
##### Alice#######
##################

# The file with the encrypted msg was received by Alice and she decrypts it
aliceencmsg=None
with open(ENCFILE,'rb') as f:
    aliceencmsg= f.read()
    print "Alice reads the file with the encrypted message:\n", `aliceencmsg` , '\n'

# Now Alice will decrypt it!!!
dec_data = alicekey.decrypt(aliceencmsg)
print "Decrypted Message Received by Alice from Bob:\n" ,  `dec_data`


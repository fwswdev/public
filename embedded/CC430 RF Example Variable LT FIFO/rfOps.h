/* ===============================================================================================
Function Library for rfOps
File: rfOps.c

Sevenstring

Version 1.0
2013-12-5

V1.0  Initial Version

================================================================================================= */


#ifndef __RFOPSH
#define __RFOPSH

#define TRUE	1
#define FALSE	0

typedef struct
	{
		unsigned Transmitting :1;
		unsigned Receiving :1;
		unsigned PacketReceived :1;
	} RfBooleanFlagsT;


void RfOps_InitFlags(void);
void RfOps_InitRadio(RF_SETTINGS *pRfSettings,unsigned char *patablearray, unsigned char patablesize);
void RfOps_ReceiveOn(void);
void RfOps_ReceiveOff(void);
void RfOps_Transmit(unsigned char *buffer, unsigned char length);

unsigned char RfOps_HasPacketReceived(void);
void RfOps_ClearPacketReceivedFlag(void);
unsigned char RfOps_IsTransmitting(void);

#endif /* __PMM */

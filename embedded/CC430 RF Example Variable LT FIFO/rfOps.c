/* ===============================================================================================
Function Library for rfOps
File: rfOps.c

Sevenstring

Version 1.0
2013-12-5

V1.0  Initial Version

This file is to provide code modularity since the original sample code is difficult to
be used for reuse. Since most of this codes are used from original sample codes, I will
reuse TI's license.
================================================================================================= */



/* ***********************************************************
* THIS PROGRAM IS PROVIDED "AS IS". TI MAKES NO WARRANTIES OR
* REPRESENTATIONS, EITHER EXPRESS, IMPLIED OR STATUTORY,
* INCLUDING ANY IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS
* FOR A PARTICULAR PURPOSE, LACK OF VIRUSES, ACCURACY OR
* COMPLETENESS OF RESPONSES, RESULTS AND LACK OF NEGLIGENCE.
* TI DISCLAIMS ANY WARRANTY OF TITLE, QUIET ENJOYMENT, QUIET
* POSSESSION, AND NON-INFRINGEMENT OF ANY THIRD PARTY
* INTELLECTUAL PROPERTY RIGHTS WITH REGARD TO THE PROGRAM OR
* YOUR USE OF THE PROGRAM.
*
* IN NO EVENT SHALL TI BE LIABLE FOR ANY SPECIAL, INCIDENTAL,
* CONSEQUENTIAL OR INDIRECT DAMAGES, HOWEVER CAUSED, ON ANY
* THEORY OF LIABILITY AND WHETHER OR NOT TI HAS BEEN ADVISED
* OF THE POSSIBILITY OF SUCH DAMAGES, ARISING IN ANY WAY OUT
* OF THIS AGREEMENT, THE PROGRAM, OR YOUR USE OF THE PROGRAM.
* EXCLUDED DAMAGES INCLUDE, BUT ARE NOT LIMITED TO, COST OF
* REMOVAL OR REINSTALLATION, COMPUTER TIME, LABOR COSTS, LOSS
* OF GOODWILL, LOSS OF PROFITS, LOSS OF SAVINGS, OR LOSS OF
* USE OR INTERRUPTION OF BUSINESS. IN NO EVENT WILL TI'S
* AGGREGATE LIABILITY UNDER THIS AGREEMENT OR ARISING OUT OF
* YOUR USE OF THE PROGRAM EXCEED FIVE HUNDRED DOLLARS
* (U.S.$500).
*
* Unless otherwise stated, the Program written and copyrighted
* by Texas Instruments is distributed as "freeware".  You may,
* only under TI's copyright in the Program, use and modify the
* Program without any charge or restriction.  You may
* distribute to third parties, provided that you transfer a
* copy of this license to the third party and the third party
* agrees to these terms by its first use of the Program. You
* must reproduce the copyright notice and any other legend of
* ownership on each copy or partial copy, of the Program.
*
* You acknowledge and agree that the Program contains
* copyrighted material, trade secrets and other TI proprietary
* information and is protected by copyright laws,
* international copyright treaties, and trade secret laws, as
* well as other intellectual property laws.  To protect TI's
* rights in the Program, you agree not to decompile, reverse
* engineer, disassemble or otherwise translate any object code
* versions of the Program to a human-readable form.  You agree
* that in no event will you alter, remove or destroy any
* copyright notice included in the Program.  TI reserves all
* rights not specifically granted under this license. Except
* as specifically provided herein, nothing in this agreement
* shall be construed as conferring by implication, estoppel,
* or otherwise, upon you, any license or other right under any
* TI patents, copyrights or trade secrets.
*
* You may not use the Program in non-TI devices.
* ********************************************************* */

#include "cc430x513x.h"
#include "RF1A.h"
#include "hal_pmm.h"
#include "rfOps.h"


static volatile RfBooleanFlagsT mRfBoolFlags; // must be initialized before infinite while loop


void RfOps_InitFlags(void)
{
mRfBoolFlags.PacketReceived = FALSE;
mRfBoolFlags.Transmitting = FALSE;
mRfBoolFlags.Receiving = FALSE;
}


void RfOps_InitRadio(RF_SETTINGS *pRfSettings,unsigned char *patablearray, unsigned char patablesize)
{
// Set the High-Power Mode Request Enable bit so LPM3 can be entered
// with active radio enabled
PMMCTL0_H = 0xA5;
PMMCTL0_L |= PMMHPMRE_L;
PMMCTL0_H = 0x00;

WriteRfSettings(pRfSettings);

//WriteSinglePATable(PATABLE_VAL);
WriteBurstPATable(patablearray, patablesize);
}




void RfOps_ReceiveOn(void)
{
RF1AIES |= BIT9;                          // Falling edge of RFIFG9
RF1AIFG &= ~BIT9;                         // Clear a pending interrupt
RF1AIE |= BIT9;                          // Enable the interrupt

// Radio is in IDLE following a TX, so strobe SRX to enter Receive Mode
Strobe(RF_SRX);
mRfBoolFlags.Receiving = TRUE;
}




void RfOps_ReceiveOff(void)
{
RF1AIE &= ~BIT9;                          // Disable RX interrupts
RF1AIFG &= ~BIT9;                         // Clear pending IFG

// It is possible that ReceiveOff is called while radio is receiving a packet.
// Therefore, it is necessary to flush the RX FIFO after issuing IDLE strobe
// such that the RXFIFO is empty prior to receiving a packet.
Strobe(RF_SIDLE);
Strobe(RF_SFRX);
mRfBoolFlags.Receiving = FALSE;
}



void RfOps_Transmit(unsigned char *buffer, unsigned char length)
{
RF1AIES |= BIT9;
RF1AIFG &= ~BIT9;                         // Clear pending interrupts
RF1AIE |= BIT9;                           // Enable TX end-of-packet interrupt

WriteBurstReg(RF_TXFIFOWR, buffer, length);

Strobe(RF_STX);                         // Strobe STX
mRfBoolFlags.Transmitting = TRUE;
}



unsigned char RfOps_HasPacketReceived(void)
{
return mRfBoolFlags.PacketReceived;
}

void RfOps_ClearPacketReceivedFlag(void)
{
mRfBoolFlags.PacketReceived=FALSE;
}

unsigned char RfOps_IsTransmitting(void)
{
return mRfBoolFlags.Transmitting;
}


#pragma vector=CC1101_VECTOR
__interrupt void CC1101_ISR(void)
{
switch (__even_in_range(RF1AIV, 32))
	// Prioritizing Radio Core Interrupt
	{
	case 0:
		break; // No RF core interrupt pending
	case 2:
		break;                         // RFIFG0
	case 4:
		break;                         // RFIFG1
	case 6:
		break;                         // RFIFG2
	case 8:
		break;                         // RFIFG3
	case 10:
		break;                         // RFIFG4
	case 12:
		break;                         // RFIFG5
	case 14:
		break;                         // RFIFG6
	case 16:
		break;                         // RFIFG7
	case 18:
		break;                         // RFIFG8
	case 20:                                // RFIFG9
		if (mRfBoolFlags.Receiving)			    // RX end of packet
			{
			mRfBoolFlags.PacketReceived = TRUE;

#if 0
// NOTE: (sevenstring) This block must be moved outside out interrupt routine
// This cause problems to other developers including me.

			// Read the length byte from the FIFO
			RxBufferLength = ReadSingleReg( RXBYTES );
			ReadBurstReg(RF_RXFIFORD, RxBuffer, RxBufferLength);

			// Stop here to see contents of RxBuffer
			__no_operation();

			// Check the CRC results
			if(RxBuffer[CRC_LQI_IDX] & CRC_OK)
			P1OUT ^= BIT0;// Toggle LED1
#endif

			}
		else if (mRfBoolFlags.Transmitting)		    // TX end of packet
			{
			RF1AIE &= ~BIT9;               // Disable TX end-of-packet interrupt
//			P3OUT &= ~BIT6;        // Turn off LED after Transmit
			mRfBoolFlags.Transmitting = FALSE;
			}
		else
			while (1)
				; 			    // trap
		break;
	case 22:
		break;                         // RFIFG10
	case 24:
		break;                         // RFIFG11
	case 26:
		break;                         // RFIFG12
	case 28:
		break;                         // RFIFG13
	case 30:
		break;                         // RFIFG14
	case 32:
		break;                         // RFIFG15
	}
__bic_SR_register_on_exit(LPM3_bits);
}

//////////////////////////


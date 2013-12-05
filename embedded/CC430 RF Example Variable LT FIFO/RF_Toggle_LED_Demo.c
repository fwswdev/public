/******************************************************************************
 * CC430 RF Code Example - TX and RX (variable packet length =< FIFO size)
 *
 * Simple RF Link to Toggle Receiver's LED by pressing Transmitter's Button
 * Warning: This RF code example is setup to operate at either 868 or 915 MHz,
 * which might be out of allowable range of operation in certain countries.
 * The frequency of operation is selectable as an active build configuration
 * in the project menu.
 *
 * Please refer to the appropriate legal sources before performing tests with
 * this code example.
 *
 * This code example can be loaded to 2 CC430 devices. Each device will transmit
 * a small packet upon a button pressed. Each device will also toggle its LED
 * upon receiving the packet.
 *
 * The RF packet engine settings specify variable-length-mode with CRC check
 * enabled. The RX packet also appends 2 status bytes regarding CRC check, RSSI
 * and LQI info. For specific register settings please refer to the comments for
 * each register in RfRegSettings.c, the CC430x513x User's Guide, and/or
 * SmartRF Studio.
 *
 * G. Larmore
 * Texas Instruments Inc.
 * June 2012
 * Built with IAR v5.40.1 and CCS v5.2
 *
 *
 * Revision:
 * 	sevenstring
 * 		- minor enhancements (15/5/2013)
 * 		- This version is still hard for reuse. If will release another version
 * 		for easy code reuse.
 ******************************************************************************/

#include "RF_Toggle_LED_Demo.h"
#include "rfOps.h"



#define  PACKET_LEN         (0x05)	    // PACKET_LEN <= 61
#define  RSSI_IDX           (PACKET_LEN+1)  // Index of appended RSSI 
#define  CRC_LQI_IDX        (PACKET_LEN+2)  // Index of appended LQI, checksum
#define  CRC_OK             (BIT7)          // CRC_OK bit 
#define  PATABLE_VAL        (0x51)          // 0 dBm output 

#define  PATABLE_VAL        (0x51)          // 0 dBm output

#define PATABLE_ARRAY_SZ	8
static const unsigned char PATABLE_ARR[PATABLE_ARRAY_SZ]={0x51,0,0,0,0,0,0,0};

extern RF_SETTINGS rfSettings;

unsigned char packetReceived;
unsigned char packetTransmit;

unsigned char RxBuffer[64];
unsigned char RxBufferLength = 0;
const unsigned char TxBuffer[6] =
	{
	PACKET_LEN, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE
	};
unsigned char buttonPressed = FALSE;
unsigned int i = 0;

//unsigned char transmitting = 0;
//unsigned char receiving = 0;

//typedef struct
//	{
//		unsigned Transmitting :1;
//		unsigned Receiving :1;
//		unsigned PacketReceived :1;
//	} RfBooleanFlagsT;

//static volatile RfBooleanFlagsT mRfBoolFlags; // must be initialized before infinite while loop

void main(void)
{
// Stop watchdog timer to prevent time out reset
WDTCTL = WDTPW + WDTHOLD;

// Increase PMMCOREV level to 2 for proper radio operation
SetVCore(2);


ResetRadioCore();
RfOps_InitFlags();
RfOps_InitRadio(&rfSettings,(unsigned char*)PATABLE_ARR,PATABLE_ARRAY_SZ);
InitButtonLeds();

RfOps_ReceiveOn();

//mRfBoolFlags.PacketReceived = FALSE;
//mRfBoolFlags.Transmitting = FALSE;
//mRfBoolFlags.Receiving = TRUE;

while (1)
	{
	__bis_SR_register(LPM3_bits + GIE);
	__no_operation();

	if (buttonPressed)                      // Process a button press->transmit
		{
		P3OUT |= BIT6;    // Pulse LED during Transmit
		buttonPressed = FALSE;
		P1IFG = 0;

		RfOps_ReceiveOff();
//		mRfBoolFlags.Receiving = FALSE;
		RfOps_Transmit((unsigned char*) TxBuffer, sizeof TxBuffer);
//		mRfBoolFlags.Transmitting = TRUE;

		P1IE |= BIT7;                         // Re-enable button press
		}
	else if (!RfOps_IsTransmitting())
		{
		RfOps_ReceiveOn();
//		mRfBoolFlags.Receiving = TRUE;
		}


	// TODO: this needs to be improved
	if (RfOps_HasPacketReceived())
		{
		// Read the length byte from the FIFO
		RxBufferLength = ReadSingleReg(RXBYTES);
		ReadBurstReg(RF_RXFIFORD, RxBuffer, RxBufferLength);

		// Stop here to see contents of RxBuffer
		__no_operation();

		// Check the CRC results
		if (RxBuffer[CRC_LQI_IDX] & CRC_OK)
			P1OUT ^= BIT0;                    // Toggle LED1

//		mRfBoolFlags.PacketReceived = FALSE;
		RfOps_ClearPacketReceivedFlag();
		}

	}
}

void InitButtonLeds(void)
{
// Set up the button as interruptible
P1DIR &= ~BIT7;
P1REN |= BIT7;
P1IES &= BIT7;
P1IFG = 0;
P1OUT |= BIT7;
P1IE |= BIT7;

// Initialize Port J
PJOUT = 0x00;
PJDIR = 0xFF;

// Set up LEDs
P1OUT &= ~BIT0;
P1DIR |= BIT0;
P3OUT &= ~BIT6;
P3DIR |= BIT6;
}

//void InitRadio(void)
//{
//// Set the High-Power Mode Request Enable bit so LPM3 can be entered
//// with active radio enabled
//PMMCTL0_H = 0xA5;
//PMMCTL0_L |= PMMHPMRE_L;
//PMMCTL0_H = 0x00;
//
//WriteRfSettings(&rfSettings);
//
//WriteSinglePATable(PATABLE_VAL);
//}

//void Transmit(unsigned char *buffer, unsigned char length)
//{
//RF1AIES |= BIT9;
//RF1AIFG &= ~BIT9;                         // Clear pending interrupts
//RF1AIE |= BIT9;                           // Enable TX end-of-packet interrupt
//
//WriteBurstReg(RF_TXFIFOWR, buffer, length);
//
//Strobe(RF_STX);                         // Strobe STX
//}

//void ReceiveOn(void)
//{
//RF1AIES |= BIT9;                          // Falling edge of RFIFG9
//RF1AIFG &= ~BIT9;                         // Clear a pending interrupt
//RF1AIE |= BIT9;                          // Enable the interrupt
//
//// Radio is in IDLE following a TX, so strobe SRX to enter Receive Mode
//Strobe(RF_SRX);
//}

//void ReceiveOff(void)
//{
//RF1AIE &= ~BIT9;                          // Disable RX interrupts
//RF1AIFG &= ~BIT9;                         // Clear pending IFG
//
//// It is possible that ReceiveOff is called while radio is receiving a packet.
//// Therefore, it is necessary to flush the RX FIFO after issuing IDLE strobe
//// such that the RXFIFO is empty prior to receiving a packet.
//Strobe(RF_SIDLE);
//Strobe(RF_SFRX);
//}


#if 0
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
			P3OUT &= ~BIT6;        // Turn off LED after Transmit
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
#endif


#pragma vector=PORT1_VECTOR
__interrupt void PORT1_ISR(void)
{
switch (__even_in_range(P1IV, 16))
	{
	case 0:
		break;
	case 2:
		break;                         // P1.0 IFG
	case 4:
		break;                         // P1.1 IFG
	case 6:
		break;                         // P1.2 IFG
	case 8:
		break;                         // P1.3 IFG
	case 10:
		break;                         // P1.4 IFG
	case 12:
		break;                         // P1.5 IFG
	case 14:
		break;                         // P1.6 IFG
	case 16:                                // P1.7 IFG
		P1IE = 0;                             // Debounce by disabling buttons
		buttonPressed = TRUE;
		__bic_SR_register_on_exit(LPM3_bits); // Exit active
		break;
	}
}

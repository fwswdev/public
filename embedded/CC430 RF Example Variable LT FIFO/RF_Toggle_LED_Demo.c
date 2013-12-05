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
 * 		- This version was created for easy code reuse.
 * 		- This version is Not yet fully tested.
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




void main(void)
{
// Stop watchdog timer to prevent time out reset
WDTCTL = WDTPW + WDTHOLD;

// Increase PMMCOREV level to 2 for proper radio operation
SetVCore(2);
ResetRadioCore();
RfOps_InitRadio(&rfSettings,(unsigned char*)PATABLE_ARR,PATABLE_ARRAY_SZ);
InitButtonLeds();
RfOps_ReceiveOn();

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
		RfOps_Transmit((unsigned char*) TxBuffer, sizeof TxBuffer);

		P1IE |= BIT7;                         // Re-enable button press
		}
	else if (!RfOps_IsTransmitting())
		{
		RfOps_ReceiveOn();
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



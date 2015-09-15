#include "ADuC847.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include "jstp.h"

static char temp;
volatile bool tx_complit = 1;

//uart interrupt
void UART_isr (void) __interrupt 4
{
	if (RI)
	{
		temp = SBUF;
		jstp_rx_push_char(temp);
		RI = 0;
	}
	if (TI)
	{
		TI = 0;
		tx_complit = 1;
	}
}

void tic_isr (void) __interrupt 10
{
}
	
void ADC_isr (void) __interrupt 6
{
	RDY0 = 0;
}

void init()
{
	PLLCON = PLLCON & 0xF8;

	PSMCON = 0x0B;			//POWER SUPPLY MONITOR
	
	T3CON = 0x86;			//9600 Baud
	T3FD  = 0x12;
	SCON  = 0x50;
	
	//timer settings; 1/128s interval
	TIMECON = 0xC0;
	INTVAL = 0x1;
	TIMECON = 0xC3;
	IEIP2 = ETI;
	
	//enable interrupt
	ES = 1;
	EA = 1;
	IP |= 0b00010000;
	
	//set port 1_6 as digital input
	P1_6 = 0;
	jstp_init();
}

int main(void)
{
	char tmp_ch;
	CFG845|=0x1; 
	init();
	
	while(1)				
	{
		jstp_tick();
		if (tx_complit!=0)
		{
			if (jstp_tx_pop_char(&tmp_ch))
			{
				SBUF = tmp_ch;
				tx_complit = 0;
			}
		}
	}
}

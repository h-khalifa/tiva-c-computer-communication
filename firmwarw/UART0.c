#include "UART0.h"

void UART0_init(void){


    //-------uart0_init---------\\

    SYSCTL_RCGCUART_R |= 1;       //uart0
    SYSCTL_RCGCGPIO_R |= 1;       //port a

    UART0_CTL_R = 0;         //disable uart0
    //  @ (9600,16mhz)
    //UART0_IBRD_R = 104;
    //UART0_FBRD_R = 11;
    //  @ (115200,16mhz)
    UART0_IBRD_R = 8;
    UART0_FBRD_R = 44;

    UART0_LCRH_R = (UART_LCRH_WLEN_8|UART_LCRH_FEN);
    UART0_CTL_R = 0x301;     //enable uart0 + enable(tx+rx)

    ///UART0((PA0+PA1)=(U0Rx+U0Tx))
    GPIO_PORTA_DEN_R |= 0x03;
    GPIO_PORTA_AFSEL_R |= 0x03;
    GPIO_PORTA_PCTL_R = 0x00000011;  //4bits foe each pin
    GPIO_PORTA_AMSEL_R &= ~0x30;

}

 char UART0_InChar(void){          //Rx
  while((UART0_FR_R&0x10) != 0);
  return(( char)(UART0_DR_R&0xFF));
}

void UART0_OutChar( char data){   //Tx
  while((UART0_FR_R&0x20) != 0);
  UART0_DR_R = data;
}

void UART0_Outstring(char s[]){
    int i=0;
    while(s[i] != 0){
        UART0_OutChar(s[i++]);
        //UART0_OutChar('\0');       //sending null especially for my ui becuase char in c# stored in 2 bytes
    }
}

#include "inc/tm4c123gh6pm.h"
#include "UART0.h"



int main(void) {

    char c;
    char m[6] = "hello\n";

    UART0_init();

    SYSCTL_RCGCGPIO_R |= 0x20;
    GPIO_PORTF_DIR_R |= 0x0E;
    GPIO_PORTF_DEN_R |= 0x0E;
    GPIO_PORTF_DATA_R &= ~0x0E;

    UART0_Outstring(m);


       while (1){
           c = UART0_InChar();
           UART0_OutChar(c);

           switch(c){

           case 'r':
                          GPIO_PORTF_DATA_R = 0x2;
                          break;
           case 'b':
                          GPIO_PORTF_DATA_R = 0x4;
                          break;
           case 'g':
                          GPIO_PORTF_DATA_R = 0x8;
                          break;
           default:
                          GPIO_PORTF_DATA_R = ~0x0E;
           }

       }

    return 0;

}

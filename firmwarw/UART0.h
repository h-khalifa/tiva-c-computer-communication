#ifndef UART0_H_
#define UART0_H_

#include <stdint.h>
#include "inc/tm4c123gh6pm.h"

void UART0_init(void);
char UART0_InChar(void);
void UART0_OutChar(char data);
void UART0_Outstring(char s[]);




#endif  //end of file

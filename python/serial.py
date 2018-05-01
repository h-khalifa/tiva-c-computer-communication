from time import sleep
import serial

def snd_id (x):
	s = serial.Serial('COM3' , 115200)
	##print s.read()
	s.write(x)
	s.close()


for i in range (8):
	snd_id('g')
	sleep(1)
	snd_id('r')
	sleep(1)
	snd_id('b')
	sleep(1)

snd_id('o')
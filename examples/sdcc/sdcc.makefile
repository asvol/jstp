CC=sdcc
CFLAGS= -c -mmcs51
LDFLAGS= --model-large 
SOURCES=./main.c ./jstp.c ./jsmn.c ./jstp_gen.c
EXECUTABLE=./bin/m3.hex

OBJECTS=$(SOURCES:.c=.rel)

all: $(SOURCES) $(EXECUTABLE)

$(EXECUTABLE): $(OBJECTS)
	$(CC) $(OBJECTS) -o $@ $(LDFLAGS) 

%.rel:
	$(CC) $(LDFLAGS) $(CFLAGS) $(@:.rel=.c)

clean:
	rm -rf *.o *.asm *.rel *.map *.mem *.lst *.rst *.ihx *.sym *.lk

CC=gcc
CFLAGS=-I.
LDFLAGS=
SOURCES=./jstp.c ./test.c ./jsmn.c ./jstp_gen.c
OBJECTS=$(SOURCES:.c=.o)
EXECUTABLE=./bin/test

all: $(SOURCES) $(EXECUTABLE)

$(EXECUTABLE): $(OBJECTS)
	$(CC) $(LDFLAGS) $(OBJECTS) -o $@

.c.o:
	$(CC) $(CFLAGS) $< -o $@

clean:
	rm -rf *.o test


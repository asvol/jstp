CC=gcc
CFLAGS=-I.
LDFLAGS=
SOURCES=./jstp.c ./jstp_test.c ./jsmn.c ./jstp_gen.c
OBJECTS=$(SOURCES:.c=.o)
EXECUTABLE=./jstp_test

all: $(SOURCES) $(EXECUTABLE)

$(EXECUTABLE): $(OBJECTS)
	$(CC) $(LDFLAGS) $(OBJECTS) -o $@

.cpp.o:
	$(CC) $(CFLAGS) $< -o $@

clean:
	rm -rf *.o test


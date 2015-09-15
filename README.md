# JSTP
Simple JSON text protocol with code\help\test generation tool

Features:
 - simple interface description
 - generate api mock function and help file with api description
 - no dependencies from any standard libraries
 - no dynamic memory allocation
 - use [jsmn](https://bitbucket.org/zserge/jsmn/wiki/Home) as JSON parser
 
Example interface description with one function and one type:
``` yaml
JSTP    : 1.0.0
Name    : DeviceInfo
Version : 1.0.0
BasePath: info
Paths   :
  date:
    Title   : Production date
    Desc    : Return device production date.
    Result  : DateTime
Types:
  DateTime:
    Type : String
    Title: ISO 8601 date
    Desc : |
        Date format. See[ISO 8601](https://goo.gl/fkoOxq)
```

Example generated mock function:
```c
/**
 * info.date
 * Production date
 * Arg      : DateTime
 * Result   : DateTime
 */
static int jstp_info_date(char *path_str, char* arg_str, jsmntok_t *arg_tokens, size_t args_token_count)
{
    // ======= BEGIN ARGS =======
    char * str;
    size_t str_len;
    if (!jstp_rx_str(arg_str,arg_tokens,&str,&str_len)) return JSTP_ARGS_ERROR;
    // ======= END ARGS =======

    // BY DEFAULT RETURN 'NOT IMPLEMENTED'
    return JSTP_ITEM_NOT_IMPLEMENTED;    // <- REMOVE THIS

    // ======= BEGIN RESULT =======
    jstp_tx_begin();                     // <- {"OK":
    // jstp_tx_str(<str>);               // <- DateTime
    jstp_tx_end();                       // <- }\n
    return JSTP_NO_ERROR;                // <- SUCCESS
    // ======= END RESULT =======
}

```
API contain 4 function:
```c
// init jstp args (call one time)
void jstp_init();

// put char to rx buffer. Light function, can call from interrupt.
void jstp_rx_push_char(char c);

// check rx-buffer, execute tasks, write to tx buffer (call as many as possible)
void jstp_tick();

// get one char from tx buffer. return bool - is tx buffer contain data to read
int jstp_tx_pop_char(char* c);

```
You can insert generated C-code to you device, implement read\write char from UART and can control it by serial port from any terminal program

Command example:
```
ifc.on
ifc.set 1
ifc.name "test"
ifc.switch {"freq":154}
```
Same commands in different style
```
/ifc/on
/ifc/set 1
/ifc/name "test"
/ifc/switch {"freq":154}
```
Result exmaple (always JSON)
```json
{"OK":true}
{"OK":{"Freq":15.5154}}
{"OK":[123,13,1,231,3]}
{"ERR":51,"MSG":"Not implemented"}
{"ERR":41,"MSG":"Bad request: args error"}
```

Tested on **gcc** and [sdcc](http://sdcc.sourceforge.net/) compiler.


## Versioning

Project is maintained under [the Semantic Versioning guidelines](http://semver.org/).

## Changes
All notable changes to this project will be documented here

### [0.1.0] - 2015/09/15

 - Add C-code generation
 - Add Html help generation
 - Add gcc test application
 - Add sdcc example code for ADuC847

## Copyright and license

Project use JSON parser library [JSMN](http://zserge.com/jsmn.html)

Code released under the MIT license. Docs is licensed under a [Creative Commons — CC BY 3.0](http://creativecommons.org/licenses/by/3.0/).

# JSTP
JSon Text Protocol and C code jenerator for small devices

## How it work
You create interface description: define paths, arguments types, result types.
For example:
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

Run generation tool and you have:
- Small, simple C-code without link to any standard libraries.
- Single html help file with full interface description
- Test application for c-generated code

Example C-generated mock function:
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

You can insert generated C-code to you device, implement read\write char from UART and can control it by serial port from any terminal program

Command example:
```json
ifc.on
ifc.set 1
ifc.name "test"
ifc.switch {"freq":154}
```
Same commands in different style
```json
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
{"ERR":-5}
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

## Copyright and license

Project include JSON parser library [JSMN](http://zserge.com/jsmn.html)

Code released under the MIT license. Docs is licensed under a [Creative Commons — CC BY 3.0](http://creativecommons.org/licenses/by/3.0/).

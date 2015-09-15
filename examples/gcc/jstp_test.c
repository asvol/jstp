#include <stdio.h>
#include <string.h>
#include "jstp.h"


// static int dump(const char *js, jsmntok_t *t, size_t count, int indent)
// {
//     int i, j, k;
//     if (count == 0) {
//         return 0;
//     }
//     if (t->type == JSMN_PRIMITIVE) {
//         printf("%.*s", t->end - t->start, js+t->start);
//         return 1;
//     } else if (t->type == JSMN_STRING) {
//         printf("'%.*s'", t->end - t->start, js+t->start);
//         return 1;
//     } else if (t->type == JSMN_OBJECT) {
//         printf("\n");
//         j = 0;
//         for (i = 0; i < t->size; i++) {
//             for (k = 0; k < indent; k++) printf("  ");
//             j += dump(js, t+1+j, count-j, indent+1);
//             printf(": ");
//             j += dump(js, t+1+j, count-j, indent+1);
//             printf("\n");
//         }
//         return j+1;
//     } else if (t->type == JSMN_ARRAY) {
//         j = 0;
//         printf("\n");
//         for (i = 0; i < t->size; i++) {
//             for (k = 0; k < indent-1; k++) printf("  ");
//             printf("   - ");
//             j += dump(js, t+1+j, count-j, indent+1);
//             printf("\n");
//         }
//         return j+1;
//     }
//     return 0;
// }

int main() {

    jstp_init();
    char ch;
    while(read(0, &ch, 1) > 0)
    {

        if (ch == '\n')
        {
            jstp_rx_push_char(0x0d);
            printf("->");
            fflush(stdout);
            jstp_tick();
            while(jstp_tx_pop_char(&ch))
            {
                write(1,&ch,1);
                jstp_tick();
            }
            printf("\n");
            continue;
        }
        printf("%c",ch );
        jstp_rx_push_char(ch);
        jstp_tick();

    }
    return 0;
}


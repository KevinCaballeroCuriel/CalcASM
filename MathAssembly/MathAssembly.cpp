
#include "pch.h" /
#include <utility>
#include <limits.h>
#include "MathAssembly.h"


int Add(
    int a,
    int b)
{
    _asm {  mov eax, a
    add eax, b
    }

}

int Power(
    int a,
    int b)
{
    _asm {
        mov ecx, b;
        mov eax, 1;
    ciclo:
        imul a;
        loop ciclo;
    }
}

int Absolute(int a) {
    _asm {
        mov eax, a;
        cdq;
        xor eax, edx;
        sub eax, edx;
    }
}

int Substract(int a, int b) {
    _asm {
        mov eax, a;
        sub eax, b;
    }
}

int Multiply(int a, int b) {
    _asm {
        mov eax, a;
        imul b;
    }
}

int Divide(int a, int b) {
    _asm {
        mov eax, a;
        cdq
            idiv b;
    }
}

int Sign(int a) {
    _asm {
        mov eax, a;
        cmp ax, 0;
        jl negative;
        mov eax, 1;
        jmp end;
    negative:
        mov eax, -1;
    end:
    }
}

int Factorial(int a) {
    _asm {
        mov ecx, a;
        mov eax, 1;
    ciclo:
        imul ecx;
        loop ciclo;
    }
}
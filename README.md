# Modern C# port of https://github.com/jeremyong/klein

This is work in progress. 

### An ASM dump of all methods using Xoofx's [JitBuddy](https://github.com/xoofx/JitBuddy) 

```x86asm
# [struct Branch] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef)
00007FFDF7284CA0 vzeroupper
00007FFDF7284CA3 xchg      ax,ax
00007FFDF7284CA5 vmovupd   xmm0,[rcx]
00007FFDF7284CA9 vextractps eax,xmm0,1
00007FFDF7284CAF vmovd     xmm0,eax
00007FFDF7284CB3 vmovss    [rdx],xmm0
00007FFDF7284CB7 vmovupd   xmm0,[rcx]
00007FFDF7284CBB vextractps eax,xmm0,2
00007FFDF7284CC1 vmovd     xmm0,eax
00007FFDF7284CC5 vmovss    [r8],xmm0
00007FFDF7284CCA vmovupd   xmm0,[rcx]
00007FFDF7284CCE vextractps eax,xmm0,3
00007FFDF7284CD4 vmovd     xmm0,eax
00007FFDF7284CD8 vmovss    [r9],xmm0
00007FFDF7284CDD ret

# -----------------------------------------------------------------------------------

# [struct Branch] Single SquaredNorm()
00007FFDF74DDD20 push      rax
00007FFDF74DDD21 vzeroupper
00007FFDF74DDD24 nop
00007FFDF74DDD25 vmovupd   xmm0,[rcx]
00007FFDF74DDD29 vmovaps   xmm1,xmm0
00007FFDF74DDD2D vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74DDD33 vxorps    xmm1,xmm1,xmm1
00007FFDF74DDD37 vmovss    [rsp+4],xmm1
00007FFDF74DDD3D vmovss    [rsp+4],xmm0
00007FFDF74DDD43 vmovss    xmm0,[rsp+4]
00007FFDF74DDD49 add       rsp,8
00007FFDF74DDD4D ret

# -----------------------------------------------------------------------------------

# [struct Branch] Single Norm()
00007FFDF74DE970 push      rax
00007FFDF74DE971 vzeroupper
00007FFDF74DE974 nop
00007FFDF74DE975 vmovupd   xmm0,[rcx]
00007FFDF74DE979 vmovaps   xmm1,xmm0
00007FFDF74DE97D vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74DE983 vxorps    xmm1,xmm1,xmm1
00007FFDF74DE987 vmovss    [rsp+4],xmm1
00007FFDF74DE98D vmovss    [rsp+4],xmm0
00007FFDF74DE993 vsqrtss   xmm0,xmm0,[rsp+4]
00007FFDF74DE999 add       rsp,8
00007FFDF74DE99D ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch Normalized()
00007FFDF74DE9C0 vzeroupper
00007FFDF74DE9C3 xchg      ax,ax
00007FFDF74DE9C5 vmovupd   xmm0,[rcx]
00007FFDF74DE9C9 vmovaps   xmm1,xmm0
00007FFDF74DE9CD vmovaps   xmm2,xmm0
00007FFDF74DE9D1 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74DE9D7 vrsqrtps  xmm2,xmm1
00007FFDF74DE9DB vmulps    xmm3,xmm2,xmm2
00007FFDF74DE9DF vmulps    xmm3,xmm1,xmm3
00007FFDF74DE9E3 vmovss    xmm1,[rel 7FFD`F74D`EA28h]
00007FFDF74DE9EB vbroadcastss xmm1,xmm1
00007FFDF74DE9F0 vsubps    xmm1,xmm1,xmm3
00007FFDF74DE9F4 vmovss    xmm3,[rel 7FFD`F74D`EA2Ch]
00007FFDF74DE9FC vbroadcastss xmm3,xmm3
00007FFDF74DEA01 vmulps    xmm2,xmm3,xmm2
00007FFDF74DEA05 vmulps    xmm1,xmm2,xmm1
00007FFDF74DEA09 vmulps    xmm0,xmm0,xmm1
00007FFDF74DEA0D vmovupd   [rdx],xmm0
00007FFDF74DEA11 mov       rax,rdx
00007FFDF74DEA14 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch Inverse()
00007FFDF74DED00 vzeroupper
00007FFDF74DED03 xchg      ax,ax
00007FFDF74DED05 vmovupd   xmm0,[rcx]
00007FFDF74DED09 vmovaps   xmm1,xmm0
00007FFDF74DED0D vmovaps   xmm2,xmm0
00007FFDF74DED11 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74DED17 vrsqrtps  xmm2,xmm1
00007FFDF74DED1B vmulps    xmm3,xmm2,xmm2
00007FFDF74DED1F vmulps    xmm3,xmm1,xmm3
00007FFDF74DED23 vmovss    xmm1,[rel 7FFD`F74D`EDA0h]
00007FFDF74DED2B vbroadcastss xmm1,xmm1
00007FFDF74DED30 vsubps    xmm1,xmm1,xmm3
00007FFDF74DED34 vmovss    xmm3,[rel 7FFD`F74D`EDA4h]
00007FFDF74DED3C vbroadcastss xmm3,xmm3
00007FFDF74DED41 vmulps    xmm2,xmm3,xmm2
00007FFDF74DED45 vmulps    xmm1,xmm2,xmm1
00007FFDF74DED49 vmulps    xmm0,xmm0,xmm1
00007FFDF74DED4D vmulps    xmm0,xmm0,xmm1
00007FFDF74DED51 vxorps    xmm1,xmm1,xmm1
00007FFDF74DED55 vmovss    xmm2,[rel 7FFD`F74D`EDA8h]
00007FFDF74DED5D vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74DED63 vmovss    xmm2,[rel 7FFD`F74D`EDACh]
00007FFDF74DED6B vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74DED71 vmovss    xmm2,[rel 7FFD`F74D`EDB0h]
00007FFDF74DED79 vinsertps xmm1,xmm1,xmm2,30h
00007FFDF74DED7F vxorps    xmm0,xmm1,xmm0
00007FFDF74DED83 vmovupd   [rdx],xmm0
00007FFDF74DED87 mov       rax,rdx
00007FFDF74DED8A ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_Addition(Branch, Branch)
00007FFDF74DEDD0 vzeroupper
00007FFDF74DEDD3 xchg      ax,ax
00007FFDF74DEDD5 vmovupd   xmm0,[rdx]
00007FFDF74DEDD9 vmovupd   xmm1,[r8]
00007FFDF74DEDDE vaddps    xmm0,xmm0,xmm1
00007FFDF74DEDE2 vmovupd   [rcx],xmm0
00007FFDF74DEDE6 mov       rax,rcx
00007FFDF74DEDE9 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Addition(Branch, Line)
00007FFDF74DEE00 vzeroupper
00007FFDF74DEE03 xchg      ax,ax
00007FFDF74DEE05 vmovupd   xmm0,[rdx]
00007FFDF74DEE09 vmovupd   xmm1,[r8]
00007FFDF74DEE0E vaddps    xmm0,xmm0,xmm1
00007FFDF74DEE12 vmovupd   xmm1,[r8+10h]
00007FFDF74DEE18 vmovupd   [rcx],xmm0
00007FFDF74DEE1C vmovupd   [rcx+10h],xmm1
00007FFDF74DEE21 mov       rax,rcx
00007FFDF74DEE24 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Addition(Line, Branch)
00007FFDF74DEE40 vzeroupper
00007FFDF74DEE43 xchg      ax,ax
00007FFDF74DEE45 vmovupd   xmm0,[rdx]
00007FFDF74DEE49 vmovupd   xmm1,[r8]
00007FFDF74DEE4E vaddps    xmm0,xmm0,xmm1
00007FFDF74DEE52 vmovupd   xmm1,[rdx+10h]
00007FFDF74DEE57 vmovupd   [rcx],xmm0
00007FFDF74DEE5B vmovupd   [rcx+10h],xmm1
00007FFDF74DEE60 mov       rax,rcx
00007FFDF74DEE63 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Addition(Branch, IdealLine)
00007FFDF74DEE80 vzeroupper
00007FFDF74DEE83 xchg      ax,ax
00007FFDF74DEE85 vmovupd   xmm0,[rdx]
00007FFDF74DEE89 vmovupd   xmm1,[r8]
00007FFDF74DEE8E vmovupd   [rcx],xmm0
00007FFDF74DEE92 vmovupd   [rcx+10h],xmm1
00007FFDF74DEE97 mov       rax,rcx
00007FFDF74DEE9A ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Addition(IdealLine, Branch)
00007FFDF74DEEB0 vzeroupper
00007FFDF74DEEB3 xchg      ax,ax
00007FFDF74DEEB5 vmovupd   xmm0,[r8]
00007FFDF74DEEBA vmovupd   xmm1,[rdx]
00007FFDF74DEEBE vmovupd   [rcx],xmm0
00007FFDF74DEEC2 vmovupd   [rcx+10h],xmm1
00007FFDF74DEEC7 mov       rax,rcx
00007FFDF74DEECA ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_Subtraction(Branch, Branch)
00007FFDF74DEEE0 vzeroupper
00007FFDF74DEEE3 xchg      ax,ax
00007FFDF74DEEE5 vmovupd   xmm0,[rdx]
00007FFDF74DEEE9 vmovupd   xmm1,[r8]
00007FFDF74DEEEE vsubps    xmm0,xmm0,xmm1
00007FFDF74DEEF2 vmovupd   [rcx],xmm0
00007FFDF74DEEF6 mov       rax,rcx
00007FFDF74DEEF9 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Subtraction(Branch, Line)
00007FFDF74DEF10 vzeroupper
00007FFDF74DEF13 xchg      ax,ax
00007FFDF74DEF15 vmovupd   xmm0,[rdx]
00007FFDF74DEF19 vmovupd   xmm1,[r8]
00007FFDF74DEF1E vsubps    xmm0,xmm0,xmm1
00007FFDF74DEF22 vmovupd   xmm1,[r8+10h]
00007FFDF74DEF28 vmovupd   [rcx],xmm0
00007FFDF74DEF2C vmovupd   [rcx+10h],xmm1
00007FFDF74DEF31 mov       rax,rcx
00007FFDF74DEF34 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Subtraction(Line, Branch)
00007FFDF74DF8D0 vzeroupper
00007FFDF74DF8D3 xchg      ax,ax
00007FFDF74DF8D5 vmovupd   xmm0,[rdx]
00007FFDF74DF8D9 vmovupd   xmm1,[r8]
00007FFDF74DF8DE vsubps    xmm0,xmm0,xmm1
00007FFDF74DF8E2 vmovupd   xmm1,[rdx+10h]
00007FFDF74DF8E7 vmovupd   [rcx],xmm0
00007FFDF74DF8EB vmovupd   [rcx+10h],xmm1
00007FFDF74DF8F0 mov       rax,rcx
00007FFDF74DF8F3 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Subtraction(Branch, IdealLine)
00007FFDF74DF910 vzeroupper
00007FFDF74DF913 xchg      ax,ax
00007FFDF74DF915 vmovupd   xmm0,[rdx]
00007FFDF74DF919 vmovupd   xmm1,[r8]
00007FFDF74DF91E vmovupd   [rcx],xmm0
00007FFDF74DF922 vmovupd   [rcx+10h],xmm1
00007FFDF74DF927 mov       rax,rcx
00007FFDF74DF92A ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Subtraction(IdealLine, Branch)
00007FFDF74DF940 vzeroupper
00007FFDF74DF943 xchg      ax,ax
00007FFDF74DF945 vmovupd   xmm0,[r8]
00007FFDF74DF94A vmovupd   xmm1,[rdx]
00007FFDF74DF94E vmovupd   [rcx],xmm0
00007FFDF74DF952 vmovupd   [rcx+10h],xmm1
00007FFDF74DF957 mov       rax,rcx
00007FFDF74DF95A ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_Multiply(Branch, Single)
00007FFDF74DF970 vzeroupper
00007FFDF74DF973 xchg      ax,ax
00007FFDF74DF975 vmovupd   xmm0,[rdx]
00007FFDF74DF979 vbroadcastss xmm1,xmm2
00007FFDF74DF97E vmulps    xmm0,xmm0,xmm1
00007FFDF74DF982 vmovupd   [rcx],xmm0
00007FFDF74DF986 mov       rax,rcx
00007FFDF74DF989 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_Multiply(Single, Branch)
00007FFDF74DF9A0 vzeroupper
00007FFDF74DF9A3 xchg      ax,ax
00007FFDF74DF9A5 vmovupd   xmm0,[r8]
00007FFDF74DF9AA vbroadcastss xmm1,xmm1
00007FFDF74DF9AF vmulps    xmm0,xmm0,xmm1
00007FFDF74DF9B3 vmovupd   [rcx],xmm0
00007FFDF74DF9B7 mov       rax,rcx
00007FFDF74DF9BA ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_Division(Branch, Single)
00007FFDF74DF9D0 vzeroupper
00007FFDF74DF9D3 xchg      ax,ax
00007FFDF74DF9D5 vmovupd   xmm0,[rdx]
00007FFDF74DF9D9 vbroadcastss xmm1,xmm2
00007FFDF74DF9DE vrcpps    xmm2,xmm1
00007FFDF74DF9E2 vmulps    xmm1,xmm1,xmm2
00007FFDF74DF9E6 vmovss    xmm3,[rel 7FFD`F74D`FA18h]
00007FFDF74DF9EE vbroadcastss xmm3,xmm3
00007FFDF74DF9F3 vsubps    xmm1,xmm3,xmm1
00007FFDF74DF9F7 vmulps    xmm1,xmm2,xmm1
00007FFDF74DF9FB vmulps    xmm0,xmm0,xmm1
00007FFDF74DF9FF vmovupd   [rcx],xmm0
00007FFDF74DFA03 mov       rax,rcx
00007FFDF74DFA06 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_UnaryNegation(Branch)
00007FFDF74DFA30 vzeroupper
00007FFDF74DFA33 xchg      ax,ax
00007FFDF74DFA35 vmovupd   xmm0,[rdx]
00007FFDF74DFA39 vmovss    xmm1,[rel 7FFD`F74D`FA58h]
00007FFDF74DFA41 vbroadcastss xmm1,xmm1
00007FFDF74DFA46 vxorps    xmm0,xmm0,xmm1
00007FFDF74DFA4A vmovupd   [rcx],xmm0
00007FFDF74DFA4E mov       rax,rcx
00007FFDF74DFA51 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Branch op_OnesComplement(Branch)
00007FFDF74DFA70 vzeroupper
00007FFDF74DFA73 xchg      ax,ax
00007FFDF74DFA75 vxorps    xmm0,xmm0,xmm0
00007FFDF74DFA79 vmovss    xmm1,[rel 7FFD`F74D`FAC0h]
00007FFDF74DFA81 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74DFA87 vmovss    xmm1,[rel 7FFD`F74D`FAC4h]
00007FFDF74DFA8F vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74DFA95 vmovss    xmm1,[rel 7FFD`F74D`FAC8h]
00007FFDF74DFA9D vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74DFAA3 vmovupd   xmm1,[rdx]
00007FFDF74DFAA7 vxorps    xmm0,xmm1,xmm0
00007FFDF74DFAAB vmovupd   [rcx],xmm0
00007FFDF74DFAAF mov       rax,rcx
00007FFDF74DFAB2 ret

# -----------------------------------------------------------------------------------

# [struct Branch] IdealLine op_LogicalNot(Branch)
00007FFDF74DFAE0 vzeroupper
00007FFDF74DFAE3 xchg      ax,ax
00007FFDF74DFAE5 vmovupd   xmm0,[rdx]
00007FFDF74DFAE9 vmovupd   [rcx],xmm0
00007FFDF74DFAED mov       rax,rcx
00007FFDF74DFAF0 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Dual op_ExclusiveOr(Branch, IdealLine)
00007FFDF74DFB10 sub       rsp,18h
00007FFDF74DFB14 vzeroupper
00007FFDF74DFB17 vmovupd   xmm0,[rcx]
00007FFDF74DFB1B vmovupd   xmm1,[rdx]
00007FFDF74DFB1F vmulps    xmm0,xmm0,xmm1
00007FFDF74DFB23 vmovshdup xmm1,xmm0
00007FFDF74DFB27 vaddps    xmm1,xmm1,xmm0
00007FFDF74DFB2B vunpcklps xmm0,xmm0,xmm0
00007FFDF74DFB2F vaddps    xmm0,xmm1,xmm0
00007FFDF74DFB33 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74DFB37 vxorps    xmm1,xmm1,xmm1
00007FFDF74DFB3B vmovss    [rsp+0Ch],xmm1
00007FFDF74DFB41 vmovss    [rsp+0Ch],xmm0
00007FFDF74DFB47 vmovss    [rsp+10h],xmm1
00007FFDF74DFB4D vmovss    [rsp+14h],xmm1
00007FFDF74DFB53 vmovss    xmm0,[rsp+0Ch]
00007FFDF74DFB59 vmovss    [rsp+10h],xmm1
00007FFDF74DFB5F vmovss    [rsp+14h],xmm0
00007FFDF74DFB65 mov       rax,[rsp+10h]
00007FFDF74DFB6A add       rsp,18h
00007FFDF74DFB6E ret

# -----------------------------------------------------------------------------------

# [struct Branch] Point op_ExclusiveOr(Branch, Plane)
00007FFDF74DFBA0 vzeroupper
00007FFDF74DFBA3 xchg      ax,ax
00007FFDF74DFBA5 vmovupd   xmm0,[r8]
00007FFDF74DFBAA vmovupd   xmm1,[rdx]
00007FFDF74DFBAE vmovaps   xmm2,xmm0
00007FFDF74DFBB2 vmovaps   xmm3,xmm1
00007FFDF74DFBB6 vshufps   xmm0,xmm0,xmm0,1
00007FFDF74DFBBB vmulps    xmm0,xmm0,xmm1
00007FFDF74DFBBF vxorps    xmm1,xmm1,xmm1
00007FFDF74DFBC3 vmovss    xmm4,[rel 7FFD`F74D`FC10h]
00007FFDF74DFBCB vinsertps xmm1,xmm1,xmm4,10h
00007FFDF74DFBD1 vmovss    xmm4,[rel 7FFD`F74D`FC14h]
00007FFDF74DFBD9 vinsertps xmm1,xmm1,xmm4,20h
00007FFDF74DFBDF vmovss    xmm4,[rel 7FFD`F74D`FC18h]
00007FFDF74DFBE7 vinsertps xmm1,xmm1,xmm4,30h
00007FFDF74DFBED vmulps    xmm0,xmm0,xmm1
00007FFDF74DFBF1 vdpps     xmm1,xmm2,xmm3,0E1h
00007FFDF74DFBF7 vaddss    xmm0,xmm0,xmm1
00007FFDF74DFBFB vmovupd   [rcx],xmm0
00007FFDF74DFBFF mov       rax,rcx
00007FFDF74DFC02 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Dual op_ExclusiveOr(Branch, Line)
00007FFDF74DFC30 sub       rsp,18h
00007FFDF74DFC34 vzeroupper
00007FFDF74DFC37 vmovupd   xmm0,[rdx+10h]
00007FFDF74DFC3C vmovupd   xmm1,[rcx]
00007FFDF74DFC40 vmulps    xmm0,xmm1,xmm0
00007FFDF74DFC44 vmovshdup xmm1,xmm0
00007FFDF74DFC48 vaddps    xmm1,xmm1,xmm0
00007FFDF74DFC4C vunpcklps xmm0,xmm0,xmm0
00007FFDF74DFC50 vaddps    xmm0,xmm1,xmm0
00007FFDF74DFC54 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74DFC58 vxorps    xmm1,xmm1,xmm1
00007FFDF74DFC5C vmovss    [rsp+0Ch],xmm1
00007FFDF74DFC62 vmovss    [rsp+0Ch],xmm0
00007FFDF74DFC68 vmovss    [rsp+10h],xmm1
00007FFDF74DFC6E vmovss    [rsp+14h],xmm1
00007FFDF74DFC74 vmovss    xmm0,[rsp+0Ch]
00007FFDF74DFC7A vmovss    [rsp+10h],xmm1
00007FFDF74DFC80 vmovss    [rsp+14h],xmm0
00007FFDF74DFC86 mov       rax,[rsp+10h]
00007FFDF74DFC8B add       rsp,18h
00007FFDF74DFC8F ret

# -----------------------------------------------------------------------------------

# [struct Branch] Rotor op_Multiply(Branch, Branch)
00007FFDF74DFCC0 vzeroupper
00007FFDF74DFCC3 xchg      ax,ax
00007FFDF74DFCC5 vmovupd   xmm0,[rdx]
00007FFDF74DFCC9 vmovupd   xmm1,[r8]
00007FFDF74DFCCE vshufps   xmm2,xmm0,xmm0,0
00007FFDF74DFCD3 vmulps    xmm2,xmm2,xmm1
00007FFDF74DFCD7 vshufps   xmm3,xmm0,xmm0,79h
00007FFDF74DFCDC vshufps   xmm4,xmm1,xmm1,9Dh
00007FFDF74DFCE1 vmulps    xmm3,xmm3,xmm4
00007FFDF74DFCE5 vsubps    xmm2,xmm2,xmm3
00007FFDF74DFCE9 vshufps   xmm3,xmm0,xmm0,0E6h
00007FFDF74DFCEE vshufps   xmm4,xmm1,xmm1,2
00007FFDF74DFCF3 vmulps    xmm3,xmm3,xmm4
00007FFDF74DFCF7 vshufps   xmm0,xmm0,xmm0,9Fh
00007FFDF74DFCFC vshufps   xmm1,xmm1,xmm1,7Bh
00007FFDF74DFD01 vmulps    xmm0,xmm0,xmm1
00007FFDF74DFD05 vaddps    xmm0,xmm3,xmm0
00007FFDF74DFD09 vxorps    xmm1,xmm1,xmm1
00007FFDF74DFD0D vmovss    xmm3,[rel 7FFD`F74D`FD40h]
00007FFDF74DFD15 vmovss    xmm1,xmm1,xmm3
00007FFDF74DFD19 vxorps    xmm0,xmm0,xmm1
00007FFDF74DFD1D vaddps    xmm0,xmm2,xmm0
00007FFDF74DFD21 vmovupd   [rcx],xmm0
00007FFDF74DFD25 mov       rax,rcx
00007FFDF74DFD28 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Rotor op_Division(Branch, Branch)
00007FFDF74DFD60 vzeroupper
00007FFDF74DFD63 xchg      ax,ax
00007FFDF74DFD65 vmovupd   xmm0,[r8]
00007FFDF74DFD6A vmovaps   xmm1,xmm0
00007FFDF74DFD6E vmovaps   xmm2,xmm0
00007FFDF74DFD72 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74DFD78 vrsqrtps  xmm2,xmm1
00007FFDF74DFD7C vmulps    xmm3,xmm2,xmm2
00007FFDF74DFD80 vmulps    xmm3,xmm1,xmm3
00007FFDF74DFD84 vmovss    xmm1,[rel 7FFD`F74D`FE70h]
00007FFDF74DFD8C vbroadcastss xmm1,xmm1
00007FFDF74DFD91 vsubps    xmm1,xmm1,xmm3
00007FFDF74DFD95 vmovss    xmm3,[rel 7FFD`F74D`FE74h]
00007FFDF74DFD9D vbroadcastss xmm3,xmm3
00007FFDF74DFDA2 vmulps    xmm2,xmm3,xmm2
00007FFDF74DFDA6 vmulps    xmm1,xmm2,xmm1
00007FFDF74DFDAA vmulps    xmm0,xmm0,xmm1
00007FFDF74DFDAE vmulps    xmm0,xmm0,xmm1
00007FFDF74DFDB2 vxorps    xmm1,xmm1,xmm1
00007FFDF74DFDB6 vmovss    xmm2,[rel 7FFD`F74D`FE78h]
00007FFDF74DFDBE vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74DFDC4 vmovss    xmm2,[rel 7FFD`F74D`FE7Ch]
00007FFDF74DFDCC vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74DFDD2 vmovss    xmm2,[rel 7FFD`F74D`FE80h]
00007FFDF74DFDDA vinsertps xmm1,xmm1,xmm2,30h
00007FFDF74DFDE0 vxorps    xmm0,xmm1,xmm0
00007FFDF74DFDE4 vmovupd   xmm1,[rdx]
00007FFDF74DFDE8 vshufps   xmm2,xmm1,xmm1,0
00007FFDF74DFDED vmulps    xmm2,xmm2,xmm0
00007FFDF74DFDF1 vshufps   xmm3,xmm1,xmm1,79h
00007FFDF74DFDF6 vshufps   xmm4,xmm0,xmm0,9Dh
00007FFDF74DFDFB vmulps    xmm3,xmm3,xmm4
00007FFDF74DFDFF vsubps    xmm2,xmm2,xmm3
00007FFDF74DFE03 vshufps   xmm3,xmm1,xmm1,0E6h
00007FFDF74DFE08 vshufps   xmm4,xmm0,xmm0,2
00007FFDF74DFE0D vmulps    xmm3,xmm3,xmm4
00007FFDF74DFE11 vshufps   xmm1,xmm1,xmm1,9Fh
00007FFDF74DFE16 vshufps   xmm0,xmm0,xmm0,7Bh
00007FFDF74DFE1B vmulps    xmm0,xmm1,xmm0
00007FFDF74DFE1F vaddps    xmm0,xmm3,xmm0
00007FFDF74DFE23 vxorps    xmm1,xmm1,xmm1
00007FFDF74DFE27 vmovss    xmm3,[rel 7FFD`F74D`FE84h]
00007FFDF74DFE2F vmovss    xmm1,xmm1,xmm3
00007FFDF74DFE33 vxorps    xmm0,xmm0,xmm1
00007FFDF74DFE37 vaddps    xmm0,xmm2,xmm0
00007FFDF74DFE3B vmovupd   [rcx],xmm0
00007FFDF74DFE3F mov       rax,rcx
00007FFDF74DFE42 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Plane op_BitwiseAnd(Branch, Point)
00007FFDF74DFEA0 vzeroupper
00007FFDF74DFEA3 xchg      ax,ax
00007FFDF74DFEA5 vmovupd   xmm0,[r8]
00007FFDF74DFEAA vmovupd   xmm1,[rdx]
00007FFDF74DFEAE vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74DFEB3 vmulps    xmm2,xmm0,xmm2
00007FFDF74DFEB7 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74DFEBC vmulps    xmm0,xmm0,xmm1
00007FFDF74DFEC0 vsubps    xmm0,xmm2,xmm0
00007FFDF74DFEC4 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74DFEC9 vmovupd   [rcx],xmm0
00007FFDF74DFECD mov       rax,rcx
00007FFDF74DFED0 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Boolean op_Equality(Branch, Branch)
00007FFDF74DFFE0 vzeroupper
00007FFDF74DFFE3 xchg      ax,ax
00007FFDF74DFFE5 vmovupd   xmm0,[rdx]
00007FFDF74DFFE9 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74DFFEE vmovmskps eax,xmm0
00007FFDF74DFFF2 cmp       eax,0Fh
00007FFDF74DFFF5 sete      al
00007FFDF74DFFF8 movzx     eax,al
00007FFDF74DFFFB ret

# -----------------------------------------------------------------------------------

# [struct Branch] Boolean op_Inequality(Branch, Branch)
00007FFDF74E04D0 vzeroupper
00007FFDF74E04D3 xchg      ax,ax
00007FFDF74E04D5 vmovupd   xmm0,[rdx]
00007FFDF74E04D9 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E04DE vmovmskps eax,xmm0
00007FFDF74E04E2 cmp       eax,0Fh
00007FFDF74E04E5 sete      al
00007FFDF74E04E8 movzx     eax,al
00007FFDF74E04EB test      eax,eax
00007FFDF74E04ED sete      al
00007FFDF74E04F0 movzx     eax,al
00007FFDF74E04F3 ret

# -----------------------------------------------------------------------------------

# [struct Branch] Line op_Implicit(Branch)
00007FFDF74E0690 vzeroupper
00007FFDF74E0693 xchg      ax,ax
00007FFDF74E0695 vmovupd   xmm0,[rdx]
00007FFDF74E0699 vxorps    xmm1,xmm1,xmm1
00007FFDF74E069D vmovupd   [rcx],xmm0
00007FFDF74E06A1 vmovupd   [rcx+10h],xmm1
00007FFDF74E06A6 mov       rax,rcx
00007FFDF74E06A9 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E06C0 vzeroupper
00007FFDF74E06C3 xchg      ax,ax
00007FFDF74E06C5 vmovupd   xmm0,[rcx]
00007FFDF74E06C9 vextractps eax,xmm0,1
00007FFDF74E06CF vmovd     xmm0,eax
00007FFDF74E06D3 vmovss    [rdx],xmm0
00007FFDF74E06D7 vmovupd   xmm0,[rcx]
00007FFDF74E06DB vextractps eax,xmm0,2
00007FFDF74E06E1 vmovd     xmm0,eax
00007FFDF74E06E5 vmovss    [r8],xmm0
00007FFDF74E06EA vmovupd   xmm0,[rcx]
00007FFDF74E06EE vextractps eax,xmm0,3
00007FFDF74E06F4 vmovd     xmm0,eax
00007FFDF74E06F8 vmovss    [r9],xmm0
00007FFDF74E06FD ret

# -----------------------------------------------------------------------------------

# [struct Direction] Direction Normalized()
00007FFDF74E0720 vzeroupper
00007FFDF74E0723 xchg      ax,ax
00007FFDF74E0725 vmovupd   xmm0,[rcx]
00007FFDF74E0729 vmovaps   xmm1,xmm0
00007FFDF74E072D vmovaps   xmm2,xmm0
00007FFDF74E0731 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74E0737 vrsqrtps  xmm2,xmm1
00007FFDF74E073B vmulps    xmm3,xmm2,xmm2
00007FFDF74E073F vmulps    xmm3,xmm1,xmm3
00007FFDF74E0743 vmovss    xmm1,[rel 7FFD`F74E`0788h]
00007FFDF74E074B vbroadcastss xmm1,xmm1
00007FFDF74E0750 vsubps    xmm1,xmm1,xmm3
00007FFDF74E0754 vmovss    xmm3,[rel 7FFD`F74E`078Ch]
00007FFDF74E075C vbroadcastss xmm3,xmm3
00007FFDF74E0761 vmulps    xmm2,xmm3,xmm2
00007FFDF74E0765 vmulps    xmm1,xmm2,xmm1
00007FFDF74E0769 vmulps    xmm0,xmm0,xmm1
00007FFDF74E076D vmovupd   [rdx],xmm0
00007FFDF74E0771 mov       rax,rdx
00007FFDF74E0774 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Direction op_Addition(Direction, Direction)
00007FFDF74E07A0 vzeroupper
00007FFDF74E07A3 xchg      ax,ax
00007FFDF74E07A5 vmovupd   xmm0,[rdx]
00007FFDF74E07A9 vmovupd   xmm1,[r8]
00007FFDF74E07AE vaddps    xmm0,xmm0,xmm1
00007FFDF74E07B2 vmovupd   [rcx],xmm0
00007FFDF74E07B6 mov       rax,rcx
00007FFDF74E07B9 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Point op_Addition(Point, Direction)
00007FFDF74E07D0 vzeroupper
00007FFDF74E07D3 xchg      ax,ax
00007FFDF74E07D5 vmovupd   xmm0,[rdx]
00007FFDF74E07D9 vmovupd   xmm1,[r8]
00007FFDF74E07DE vaddps    xmm0,xmm0,xmm1
00007FFDF74E07E2 vmovupd   [rcx],xmm0
00007FFDF74E07E6 mov       rax,rcx
00007FFDF74E07E9 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Point op_Addition(Direction, Point)
00007FFDF74E0800 vzeroupper
00007FFDF74E0803 xchg      ax,ax
00007FFDF74E0805 vmovupd   xmm0,[rdx]
00007FFDF74E0809 vmovupd   xmm1,[r8]
00007FFDF74E080E vaddps    xmm0,xmm0,xmm1
00007FFDF74E0812 vmovupd   [rcx],xmm0
00007FFDF74E0816 mov       rax,rcx
00007FFDF74E0819 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Direction op_Subtraction(Direction, Direction)
00007FFDF74E0830 vzeroupper
00007FFDF74E0833 xchg      ax,ax
00007FFDF74E0835 vmovupd   xmm0,[rdx]
00007FFDF74E0839 vmovupd   xmm1,[r8]
00007FFDF74E083E vsubps    xmm0,xmm0,xmm1
00007FFDF74E0842 vmovupd   [rcx],xmm0
00007FFDF74E0846 mov       rax,rcx
00007FFDF74E0849 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Point op_Subtraction(Point, Direction)
00007FFDF74E0860 vzeroupper
00007FFDF74E0863 xchg      ax,ax
00007FFDF74E0865 vmovupd   xmm0,[rdx]
00007FFDF74E0869 vmovupd   xmm1,[r8]
00007FFDF74E086E vsubps    xmm0,xmm0,xmm1
00007FFDF74E0872 vmovupd   [rcx],xmm0
00007FFDF74E0876 mov       rax,rcx
00007FFDF74E0879 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Point op_Subtraction(Direction, Point)
00007FFDF74E0890 vzeroupper
00007FFDF74E0893 xchg      ax,ax
00007FFDF74E0895 vmovupd   xmm0,[rdx]
00007FFDF74E0899 vmovupd   xmm1,[r8]
00007FFDF74E089E vsubps    xmm0,xmm0,xmm1
00007FFDF74E08A2 vmovupd   [rcx],xmm0
00007FFDF74E08A6 mov       rax,rcx
00007FFDF74E08A9 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Direction op_Multiply(Direction, Single)
00007FFDF74E08C0 vzeroupper
00007FFDF74E08C3 xchg      ax,ax
00007FFDF74E08C5 vmovupd   xmm0,[rdx]
00007FFDF74E08C9 vbroadcastss xmm1,xmm2
00007FFDF74E08CE vmulps    xmm0,xmm0,xmm1
00007FFDF74E08D2 vmovupd   [rcx],xmm0
00007FFDF74E08D6 mov       rax,rcx
00007FFDF74E08D9 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Direction op_Multiply(Single, Direction)
00007FFDF74E08F0 vzeroupper
00007FFDF74E08F3 xchg      ax,ax
00007FFDF74E08F5 vmovupd   xmm0,[r8]
00007FFDF74E08FA vbroadcastss xmm1,xmm1
00007FFDF74E08FF vmulps    xmm0,xmm0,xmm1
00007FFDF74E0903 vmovupd   [rcx],xmm0
00007FFDF74E0907 mov       rax,rcx
00007FFDF74E090A ret

# -----------------------------------------------------------------------------------

# [struct Direction] Direction op_Division(Direction, Single)
00007FFDF74E0920 vzeroupper
00007FFDF74E0923 xchg      ax,ax
00007FFDF74E0925 vmovupd   xmm0,[rdx]
00007FFDF74E0929 vbroadcastss xmm1,xmm2
00007FFDF74E092E vrcpps    xmm2,xmm1
00007FFDF74E0932 vmulps    xmm1,xmm1,xmm2
00007FFDF74E0936 vmovss    xmm3,[rel 7FFD`F74E`0968h]
00007FFDF74E093E vbroadcastss xmm3,xmm3
00007FFDF74E0943 vsubps    xmm1,xmm3,xmm1
00007FFDF74E0947 vmulps    xmm1,xmm2,xmm1
00007FFDF74E094B vmulps    xmm0,xmm0,xmm1
00007FFDF74E094F vmovupd   [rcx],xmm0
00007FFDF74E0953 mov       rax,rcx
00007FFDF74E0956 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Point op_Implicit(Direction)
00007FFDF74E0980 vzeroupper
00007FFDF74E0983 xchg      ax,ax
00007FFDF74E0985 vmovupd   xmm0,[rdx]
00007FFDF74E0989 vmovupd   [rcx],xmm0
00007FFDF74E098D mov       rax,rcx
00007FFDF74E0990 ret

# -----------------------------------------------------------------------------------

# [struct Direction] Boolean op_Equality(Direction, Direction)
00007FFDF74E0AA0 vzeroupper
00007FFDF74E0AA3 xchg      ax,ax
00007FFDF74E0AA5 vmovupd   xmm0,[rdx]
00007FFDF74E0AA9 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E0AAE vmovmskps eax,xmm0
00007FFDF74E0AB2 cmp       eax,0Fh
00007FFDF74E0AB5 sete      al
00007FFDF74E0AB8 movzx     eax,al
00007FFDF74E0ABB ret

# -----------------------------------------------------------------------------------

# [struct Direction] Boolean op_Inequality(Direction, Direction)
00007FFDF74E0AD0 vzeroupper
00007FFDF74E0AD3 xchg      ax,ax
00007FFDF74E0AD5 vmovupd   xmm0,[rdx]
00007FFDF74E0AD9 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E0ADE vmovmskps eax,xmm0
00007FFDF74E0AE2 cmp       eax,0Fh
00007FFDF74E0AE5 sete      al
00007FFDF74E0AE8 movzx     eax,al
00007FFDF74E0AEB test      eax,eax
00007FFDF74E0AED sete      al
00007FFDF74E0AF0 movzx     eax,al
00007FFDF74E0AF3 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Void Deconstruct(Single ByRef, Single ByRef)
00007FFDF74E0B10 vzeroupper
00007FFDF74E0B13 xchg      ax,ax
00007FFDF74E0B15 vmovss    xmm0,[rcx]
00007FFDF74E0B19 vmovss    [rdx],xmm0
00007FFDF74E0B1D vmovss    xmm0,[rcx+4]
00007FFDF74E0B22 vmovss    [r8],xmm0
00007FFDF74E0B27 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Dual op_Addition(Dual, Dual)
00007FFDF74E0B40 push      rax
00007FFDF74E0B41 vzeroupper
00007FFDF74E0B44 nop
00007FFDF74E0B45 mov       [rsp+10h],rcx
00007FFDF74E0B4A mov       [rsp+18h],rdx
00007FFDF74E0B4F vxorps    xmm0,xmm0,xmm0
00007FFDF74E0B53 vmovss    [rsp],xmm0
00007FFDF74E0B58 vmovss    [rsp+4],xmm0
00007FFDF74E0B5E vmovss    xmm0,[rsp+10h]
00007FFDF74E0B64 vaddss    xmm0,xmm0,[rsp+18h]
00007FFDF74E0B6A vmovss    xmm1,[rsp+14h]
00007FFDF74E0B70 vaddss    xmm1,xmm1,[rsp+1Ch]
00007FFDF74E0B76 vmovss    [rsp],xmm0
00007FFDF74E0B7B vmovss    [rsp+4],xmm1
00007FFDF74E0B81 mov       rax,[rsp]
00007FFDF74E0B85 add       rsp,8
00007FFDF74E0B89 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Dual op_Subtraction(Dual, Dual)
00007FFDF74E0C00 push      rax
00007FFDF74E0C01 vzeroupper
00007FFDF74E0C04 nop
00007FFDF74E0C05 mov       [rsp+10h],rcx
00007FFDF74E0C0A mov       [rsp+18h],rdx
00007FFDF74E0C0F vxorps    xmm0,xmm0,xmm0
00007FFDF74E0C13 vmovss    [rsp],xmm0
00007FFDF74E0C18 vmovss    [rsp+4],xmm0
00007FFDF74E0C1E vmovss    xmm0,[rsp+10h]
00007FFDF74E0C24 vsubss    xmm0,xmm0,[rsp+18h]
00007FFDF74E0C2A vmovss    xmm1,[rsp+14h]
00007FFDF74E0C30 vsubss    xmm1,xmm1,[rsp+1Ch]
00007FFDF74E0C36 vmovss    [rsp],xmm0
00007FFDF74E0C3B vmovss    [rsp+4],xmm1
00007FFDF74E0C41 mov       rax,[rsp]
00007FFDF74E0C45 add       rsp,8
00007FFDF74E0C49 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Dual op_Multiply(Dual, Single)
00007FFDF74E0C70 push      rax
00007FFDF74E0C71 vzeroupper
00007FFDF74E0C74 nop
00007FFDF74E0C75 mov       [rsp+10h],rcx
00007FFDF74E0C7A vxorps    xmm0,xmm0,xmm0
00007FFDF74E0C7E vmovss    [rsp],xmm0
00007FFDF74E0C83 vmovss    [rsp+4],xmm0
00007FFDF74E0C89 vmovaps   xmm0,xmm1
00007FFDF74E0C8D vmulss    xmm0,xmm0,[rsp+10h]
00007FFDF74E0C93 vmulss    xmm1,xmm1,[rsp+14h]
00007FFDF74E0C99 vmovss    [rsp],xmm0
00007FFDF74E0C9E vmovss    [rsp+4],xmm1
00007FFDF74E0CA4 mov       rax,[rsp]
00007FFDF74E0CA8 add       rsp,8
00007FFDF74E0CAC ret

# -----------------------------------------------------------------------------------

# [struct Dual] Dual op_Multiply(Single, Dual)
00007FFDF74E0CD0 push      rax
00007FFDF74E0CD1 vzeroupper
00007FFDF74E0CD4 nop
00007FFDF74E0CD5 mov       [rsp+18h],rdx
00007FFDF74E0CDA vxorps    xmm1,xmm1,xmm1
00007FFDF74E0CDE vmovss    [rsp],xmm1
00007FFDF74E0CE3 vmovss    [rsp+4],xmm1
00007FFDF74E0CE9 vmovaps   xmm1,xmm0
00007FFDF74E0CED vmulss    xmm1,xmm1,[rsp+18h]
00007FFDF74E0CF3 vmulss    xmm0,xmm0,[rsp+1Ch]
00007FFDF74E0CF9 vmovss    [rsp],xmm1
00007FFDF74E0CFE vmovss    [rsp+4],xmm0
00007FFDF74E0D04 mov       rax,[rsp]
00007FFDF74E0D08 add       rsp,8
00007FFDF74E0D0C ret

# -----------------------------------------------------------------------------------

# [struct Dual] Dual op_Division(Dual, Single)
00007FFDF74E0D30 push      rax
00007FFDF74E0D31 vzeroupper
00007FFDF74E0D34 nop
00007FFDF74E0D35 mov       [rsp+10h],rcx
00007FFDF74E0D3A vxorps    xmm0,xmm0,xmm0
00007FFDF74E0D3E vmovss    [rsp],xmm0
00007FFDF74E0D43 vmovss    [rsp+4],xmm0
00007FFDF74E0D49 vmovss    xmm0,[rsp+10h]
00007FFDF74E0D4F vdivss    xmm0,xmm0,xmm1
00007FFDF74E0D53 vmovss    xmm2,[rsp+14h]
00007FFDF74E0D59 vdivss    xmm2,xmm2,xmm1
00007FFDF74E0D5D vmovss    [rsp],xmm0
00007FFDF74E0D62 vmovss    [rsp+4],xmm2
00007FFDF74E0D68 mov       rax,[rsp]
00007FFDF74E0D6C add       rsp,8
00007FFDF74E0D70 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Dual op_LogicalNot(Dual)
00007FFDF74E0D90 push      rax
00007FFDF74E0D91 vzeroupper
00007FFDF74E0D94 nop
00007FFDF74E0D95 mov       [rsp+10h],rcx
00007FFDF74E0D9A vxorps    xmm0,xmm0,xmm0
00007FFDF74E0D9E vmovss    [rsp],xmm0
00007FFDF74E0DA3 vmovss    [rsp+4],xmm0
00007FFDF74E0DA9 vmovss    xmm0,[rsp+14h]
00007FFDF74E0DAF vmovss    xmm1,[rsp+10h]
00007FFDF74E0DB5 vmovss    [rsp],xmm0
00007FFDF74E0DBA vmovss    [rsp+4],xmm1
00007FFDF74E0DC0 mov       rax,[rsp]
00007FFDF74E0DC4 add       rsp,8
00007FFDF74E0DC8 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Boolean op_Equality(Dual, Dual)
00007FFDF74E1020 sub       rsp,18h
00007FFDF74E1024 vzeroupper
00007FFDF74E1027 mov       [rsp+20h],rcx
00007FFDF74E102C mov       [rsp+28h],rdx
00007FFDF74E1031 lea       rax,[rsp+28h]
00007FFDF74E1036 vmovss    xmm0,[rax]
00007FFDF74E103A vmovss    xmm1,[rax+4]
00007FFDF74E103F vucomiss  xmm0,[rsp+20h]
00007FFDF74E1045 jp        short 0000`7FFD`F74E`1049h
00007FFDF74E1047 je        short 0000`7FFD`F74E`1083h
00007FFDF74E1049 vmovss    [rsp+14h],xmm0
00007FFDF74E104F mov       eax,[rsp+14h]
00007FFDF74E1053 and       eax,7FFF`FFFFh
00007FFDF74E1058 cmp       eax,7F80`0000h
00007FFDF74E105D jle       short 0000`7FFD`F74E`10CEh
00007FFDF74E105F vmovss    xmm0,[rsp+20h]
00007FFDF74E1065 vmovss    [rsp+10h],xmm0
00007FFDF74E106B mov       eax,[rsp+10h]
00007FFDF74E106F and       eax,7FFF`FFFFh
00007FFDF74E1074 cmp       eax,7F80`0000h
00007FFDF74E1079 setg      al
00007FFDF74E107C movzx     eax,al
00007FFDF74E107F test      eax,eax
00007FFDF74E1081 je        short 0000`7FFD`F74E`10CEh
00007FFDF74E1083 vmovss    xmm0,[rsp+24h]
00007FFDF74E1089 vucomiss  xmm1,xmm0
00007FFDF74E108D jp        short 0000`7FFD`F74E`1098h
00007FFDF74E108F jne       short 0000`7FFD`F74E`1098h
00007FFDF74E1091 mov       eax,1
00007FFDF74E1096 jmp       short 0000`7FFD`F74E`10CCh
00007FFDF74E1098 vmovss    [rsp+0Ch],xmm1
00007FFDF74E109E mov       eax,[rsp+0Ch]
00007FFDF74E10A2 and       eax,7FFF`FFFFh
00007FFDF74E10A7 cmp       eax,7F80`0000h
00007FFDF74E10AC jle       short 0000`7FFD`F74E`10CAh
00007FFDF74E10AE vmovss    [rsp+8],xmm0
00007FFDF74E10B4 mov       eax,[rsp+8]
00007FFDF74E10B8 and       eax,7FFF`FFFFh
00007FFDF74E10BD cmp       eax,7F80`0000h
00007FFDF74E10C2 setg      al
00007FFDF74E10C5 movzx     eax,al
00007FFDF74E10C8 jmp       short 0000`7FFD`F74E`10CCh
00007FFDF74E10CA xor       eax,eax
00007FFDF74E10CC jmp       short 0000`7FFD`F74E`10D0h
00007FFDF74E10CE xor       eax,eax
00007FFDF74E10D0 add       rsp,18h
00007FFDF74E10D4 ret

# -----------------------------------------------------------------------------------

# [struct Dual] Boolean op_Inequality(Dual, Dual)
00007FFDF74E19E0 sub       rsp,18h
00007FFDF74E19E4 vzeroupper
00007FFDF74E19E7 mov       [rsp+20h],rcx
00007FFDF74E19EC mov       [rsp+28h],rdx
00007FFDF74E19F1 lea       rax,[rsp+28h]
00007FFDF74E19F6 vmovss    xmm0,[rax]
00007FFDF74E19FA vmovss    xmm1,[rax+4]
00007FFDF74E19FF vucomiss  xmm0,[rsp+20h]
00007FFDF74E1A05 jp        short 0000`7FFD`F74E`1A09h
00007FFDF74E1A07 je        short 0000`7FFD`F74E`1A43h
00007FFDF74E1A09 vmovss    [rsp+14h],xmm0
00007FFDF74E1A0F mov       eax,[rsp+14h]
00007FFDF74E1A13 and       eax,7FFF`FFFFh
00007FFDF74E1A18 cmp       eax,7F80`0000h
00007FFDF74E1A1D jle       short 0000`7FFD`F74E`1A8Eh
00007FFDF74E1A1F vmovss    xmm0,[rsp+20h]
00007FFDF74E1A25 vmovss    [rsp+10h],xmm0
00007FFDF74E1A2B mov       eax,[rsp+10h]
00007FFDF74E1A2F and       eax,7FFF`FFFFh
00007FFDF74E1A34 cmp       eax,7F80`0000h
00007FFDF74E1A39 setg      al
00007FFDF74E1A3C movzx     eax,al
00007FFDF74E1A3F test      eax,eax
00007FFDF74E1A41 je        short 0000`7FFD`F74E`1A8Eh
00007FFDF74E1A43 vmovss    xmm0,[rsp+24h]
00007FFDF74E1A49 vucomiss  xmm1,xmm0
00007FFDF74E1A4D jp        short 0000`7FFD`F74E`1A58h
00007FFDF74E1A4F jne       short 0000`7FFD`F74E`1A58h
00007FFDF74E1A51 mov       eax,1
00007FFDF74E1A56 jmp       short 0000`7FFD`F74E`1A8Ch
00007FFDF74E1A58 vmovss    [rsp+0Ch],xmm1
00007FFDF74E1A5E mov       eax,[rsp+0Ch]
00007FFDF74E1A62 and       eax,7FFF`FFFFh
00007FFDF74E1A67 cmp       eax,7F80`0000h
00007FFDF74E1A6C jle       short 0000`7FFD`F74E`1A8Ah
00007FFDF74E1A6E vmovss    [rsp+8],xmm0
00007FFDF74E1A74 mov       eax,[rsp+8]
00007FFDF74E1A78 and       eax,7FFF`FFFFh
00007FFDF74E1A7D cmp       eax,7F80`0000h
00007FFDF74E1A82 setg      al
00007FFDF74E1A85 movzx     eax,al
00007FFDF74E1A88 jmp       short 0000`7FFD`F74E`1A8Ch
00007FFDF74E1A8A xor       eax,eax
00007FFDF74E1A8C jmp       short 0000`7FFD`F74E`1A90h
00007FFDF74E1A8E xor       eax,eax
00007FFDF74E1A90 test      eax,eax
00007FFDF74E1A92 sete      al
00007FFDF74E1A95 movzx     eax,al
00007FFDF74E1A98 add       rsp,18h
00007FFDF74E1A9C ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E1AC0 vzeroupper
00007FFDF74E1AC3 xchg      ax,ax
00007FFDF74E1AC5 vmovupd   xmm0,[rcx]
00007FFDF74E1AC9 vextractps eax,xmm0,1
00007FFDF74E1ACF vmovd     xmm0,eax
00007FFDF74E1AD3 vmovss    [rdx],xmm0
00007FFDF74E1AD7 vmovupd   xmm0,[rcx]
00007FFDF74E1ADB vextractps eax,xmm0,2
00007FFDF74E1AE1 vmovd     xmm0,eax
00007FFDF74E1AE5 vmovss    [r8],xmm0
00007FFDF74E1AEA vmovupd   xmm0,[rcx]
00007FFDF74E1AEE vextractps eax,xmm0,3
00007FFDF74E1AF4 vmovd     xmm0,eax
00007FFDF74E1AF8 vmovss    [r9],xmm0
00007FFDF74E1AFD ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Single SquaredIdealNorm()
00007FFDF74E1B20 push      rax
00007FFDF74E1B21 vzeroupper
00007FFDF74E1B24 nop
00007FFDF74E1B25 vmovupd   xmm0,[rcx]
00007FFDF74E1B29 vmovaps   xmm1,xmm0
00007FFDF74E1B2D vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74E1B33 vxorps    xmm1,xmm1,xmm1
00007FFDF74E1B37 vmovss    [rsp+4],xmm1
00007FFDF74E1B3D vmovss    [rsp+4],xmm0
00007FFDF74E1B43 vmovss    xmm0,[rsp+4]
00007FFDF74E1B49 add       rsp,8
00007FFDF74E1B4D ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Single IdealNorm()
00007FFDF74E1B70 push      rax
00007FFDF74E1B71 vzeroupper
00007FFDF74E1B74 nop
00007FFDF74E1B75 vmovupd   xmm0,[rcx]
00007FFDF74E1B79 vmovaps   xmm1,xmm0
00007FFDF74E1B7D vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74E1B83 vxorps    xmm1,xmm1,xmm1
00007FFDF74E1B87 vmovss    [rsp+4],xmm1
00007FFDF74E1B8D vmovss    [rsp+4],xmm0
00007FFDF74E1B93 vsqrtss   xmm0,xmm0,[rsp+4]
00007FFDF74E1B99 add       rsp,8
00007FFDF74E1B9D ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_Addition(IdealLine, IdealLine)
00007FFDF74E1BC0 vzeroupper
00007FFDF74E1BC3 xchg      ax,ax
00007FFDF74E1BC5 vmovupd   xmm0,[rdx]
00007FFDF74E1BC9 vmovupd   xmm1,[r8]
00007FFDF74E1BCE vaddps    xmm0,xmm0,xmm1
00007FFDF74E1BD2 vmovupd   [rcx],xmm0
00007FFDF74E1BD6 mov       rax,rcx
00007FFDF74E1BD9 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Line op_Addition(IdealLine, Line)
00007FFDF74E1BF0 vzeroupper
00007FFDF74E1BF3 xchg      ax,ax
00007FFDF74E1BF5 vmovupd   xmm0,[r8]
00007FFDF74E1BFA vmovupd   xmm1,[rdx]
00007FFDF74E1BFE vmovupd   xmm2,[r8+10h]
00007FFDF74E1C04 vaddps    xmm1,xmm1,xmm2
00007FFDF74E1C08 vmovupd   [rcx],xmm0
00007FFDF74E1C0C vmovupd   [rcx+10h],xmm1
00007FFDF74E1C11 mov       rax,rcx
00007FFDF74E1C14 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Line op_Addition(Line, IdealLine)
00007FFDF74E1C30 vzeroupper
00007FFDF74E1C33 xchg      ax,ax
00007FFDF74E1C35 vmovupd   xmm0,[rdx]
00007FFDF74E1C39 vmovupd   xmm1,[rdx+10h]
00007FFDF74E1C3E vmovupd   xmm2,[r8]
00007FFDF74E1C43 vaddps    xmm1,xmm1,xmm2
00007FFDF74E1C47 vmovupd   [rcx],xmm0
00007FFDF74E1C4B vmovupd   [rcx+10h],xmm1
00007FFDF74E1C50 mov       rax,rcx
00007FFDF74E1C53 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_Subtraction(IdealLine, IdealLine)
00007FFDF74E1C70 vzeroupper
00007FFDF74E1C73 xchg      ax,ax
00007FFDF74E1C75 vmovupd   xmm0,[rdx]
00007FFDF74E1C79 vmovupd   xmm1,[r8]
00007FFDF74E1C7E vsubps    xmm0,xmm0,xmm1
00007FFDF74E1C82 vmovupd   [rcx],xmm0
00007FFDF74E1C86 mov       rax,rcx
00007FFDF74E1C89 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Line op_Subtraction(IdealLine, Line)
00007FFDF74E1CA0 vzeroupper
00007FFDF74E1CA3 xchg      ax,ax
00007FFDF74E1CA5 vmovupd   xmm0,[r8]
00007FFDF74E1CAA vmovupd   xmm1,[rdx]
00007FFDF74E1CAE vmovupd   xmm2,[r8+10h]
00007FFDF74E1CB4 vsubps    xmm1,xmm1,xmm2
00007FFDF74E1CB8 vmovupd   [rcx],xmm0
00007FFDF74E1CBC vmovupd   [rcx+10h],xmm1
00007FFDF74E1CC1 mov       rax,rcx
00007FFDF74E1CC4 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Line op_Subtraction(Line, IdealLine)
00007FFDF74E1CE0 vzeroupper
00007FFDF74E1CE3 xchg      ax,ax
00007FFDF74E1CE5 vmovupd   xmm0,[rdx]
00007FFDF74E1CE9 vmovupd   xmm1,[rdx+10h]
00007FFDF74E1CEE vmovupd   xmm2,[r8]
00007FFDF74E1CF3 vsubps    xmm1,xmm1,xmm2
00007FFDF74E1CF7 vmovupd   [rcx],xmm0
00007FFDF74E1CFB vmovupd   [rcx+10h],xmm1
00007FFDF74E1D00 mov       rax,rcx
00007FFDF74E1D03 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_Multiply(IdealLine, Single)
00007FFDF74E1D20 vzeroupper
00007FFDF74E1D23 xchg      ax,ax
00007FFDF74E1D25 vmovupd   xmm0,[rdx]
00007FFDF74E1D29 vbroadcastss xmm1,xmm2
00007FFDF74E1D2E vmulps    xmm0,xmm0,xmm1
00007FFDF74E1D32 vmovupd   [rcx],xmm0
00007FFDF74E1D36 mov       rax,rcx
00007FFDF74E1D39 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_Multiply(Single, IdealLine)
00007FFDF74E1D50 vzeroupper
00007FFDF74E1D53 xchg      ax,ax
00007FFDF74E1D55 vmovupd   xmm0,[r8]
00007FFDF74E1D5A vbroadcastss xmm1,xmm1
00007FFDF74E1D5F vmulps    xmm0,xmm0,xmm1
00007FFDF74E1D63 vmovupd   [rcx],xmm0
00007FFDF74E1D67 mov       rax,rcx
00007FFDF74E1D6A ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_Division(IdealLine, Single)
00007FFDF74E1D80 vzeroupper
00007FFDF74E1D83 xchg      ax,ax
00007FFDF74E1D85 vmovupd   xmm0,[rdx]
00007FFDF74E1D89 vbroadcastss xmm1,xmm2
00007FFDF74E1D8E vrcpps    xmm2,xmm1
00007FFDF74E1D92 vmulps    xmm1,xmm1,xmm2
00007FFDF74E1D96 vmovss    xmm3,[rel 7FFD`F74E`1DC8h]
00007FFDF74E1D9E vbroadcastss xmm3,xmm3
00007FFDF74E1DA3 vsubps    xmm1,xmm3,xmm1
00007FFDF74E1DA7 vmulps    xmm1,xmm2,xmm1
00007FFDF74E1DAB vmulps    xmm0,xmm0,xmm1
00007FFDF74E1DAF vmovupd   [rcx],xmm0
00007FFDF74E1DB3 mov       rax,rcx
00007FFDF74E1DB6 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_UnaryNegation(IdealLine)
00007FFDF74E1DE0 vzeroupper
00007FFDF74E1DE3 xchg      ax,ax
00007FFDF74E1DE5 vmovupd   xmm0,[rdx]
00007FFDF74E1DE9 vmovss    xmm1,[rel 7FFD`F74E`1E08h]
00007FFDF74E1DF1 vbroadcastss xmm1,xmm1
00007FFDF74E1DF6 vxorps    xmm0,xmm0,xmm1
00007FFDF74E1DFA vmovupd   [rcx],xmm0
00007FFDF74E1DFE mov       rax,rcx
00007FFDF74E1E01 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] IdealLine op_OnesComplement(IdealLine)
00007FFDF74E1E20 vzeroupper
00007FFDF74E1E23 xchg      ax,ax
00007FFDF74E1E25 vxorps    xmm0,xmm0,xmm0
00007FFDF74E1E29 vmovss    xmm1,[rel 7FFD`F74E`1E70h]
00007FFDF74E1E31 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74E1E37 vmovss    xmm1,[rel 7FFD`F74E`1E74h]
00007FFDF74E1E3F vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74E1E45 vmovss    xmm1,[rel 7FFD`F74E`1E78h]
00007FFDF74E1E4D vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74E1E53 vmovupd   xmm1,[rdx]
00007FFDF74E1E57 vxorps    xmm0,xmm1,xmm0
00007FFDF74E1E5B vmovupd   [rcx],xmm0
00007FFDF74E1E5F mov       rax,rcx
00007FFDF74E1E62 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Branch op_LogicalNot(IdealLine)
00007FFDF74E1E90 vzeroupper
00007FFDF74E1E93 xchg      ax,ax
00007FFDF74E1E95 vmovupd   xmm0,[rdx]
00007FFDF74E1E99 vmovupd   [rcx],xmm0
00007FFDF74E1E9D mov       rax,rcx
00007FFDF74E1EA0 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Plane op_BitwiseOr(IdealLine, Plane)
00007FFDF74E1EC0 vzeroupper
00007FFDF74E1EC3 xchg      ax,ax
00007FFDF74E1EC5 vmovupd   xmm0,[r8]
00007FFDF74E1ECA vmovupd   xmm1,[rdx]
00007FFDF74E1ECE vdpps     xmm0,xmm0,xmm1,0E1h
00007FFDF74E1ED4 vmovupd   [rcx],xmm0
00007FFDF74E1ED8 mov       rax,rcx
00007FFDF74E1EDB ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Point op_ExclusiveOr(IdealLine, Plane)
00007FFDF74E1EF0 vzeroupper
00007FFDF74E1EF3 xchg      ax,ax
00007FFDF74E1EF5 vmovupd   xmm0,[r8]
00007FFDF74E1EFA vmovupd   xmm1,[rdx]
00007FFDF74E1EFE vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74E1F03 vmulps    xmm2,xmm0,xmm2
00007FFDF74E1F07 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E1F0C vmulps    xmm0,xmm0,xmm1
00007FFDF74E1F10 vsubps    xmm0,xmm2,xmm0
00007FFDF74E1F14 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E1F19 vmovupd   [rcx],xmm0
00007FFDF74E1F1D mov       rax,rcx
00007FFDF74E1F20 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Dual op_ExclusiveOr(IdealLine, Branch)
00007FFDF74E1F40 sub       rsp,18h
00007FFDF74E1F44 vzeroupper
00007FFDF74E1F47 vmovupd   xmm0,[rdx]
00007FFDF74E1F4B vmovupd   xmm1,[rcx]
00007FFDF74E1F4F vmulps    xmm0,xmm0,xmm1
00007FFDF74E1F53 vmovshdup xmm1,xmm0
00007FFDF74E1F57 vaddps    xmm1,xmm1,xmm0
00007FFDF74E1F5B vunpcklps xmm0,xmm0,xmm0
00007FFDF74E1F5F vaddps    xmm0,xmm1,xmm0
00007FFDF74E1F63 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E1F67 vxorps    xmm1,xmm1,xmm1
00007FFDF74E1F6B vmovss    [rsp+0Ch],xmm1
00007FFDF74E1F71 vmovss    [rsp+0Ch],xmm0
00007FFDF74E1F77 vmovss    [rsp+10h],xmm1
00007FFDF74E1F7D vmovss    [rsp+14h],xmm1
00007FFDF74E1F83 vmovss    xmm0,[rsp+0Ch]
00007FFDF74E1F89 vmovss    [rsp+10h],xmm1
00007FFDF74E1F8F vmovss    [rsp+14h],xmm0
00007FFDF74E1F95 mov       rax,[rsp+10h]
00007FFDF74E1F9A add       rsp,18h
00007FFDF74E1F9E ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Dual op_ExclusiveOr(IdealLine, Line)
00007FFDF74E1FD0 sub       rsp,18h
00007FFDF74E1FD4 vzeroupper
00007FFDF74E1FD7 vmovupd   xmm0,[rdx]
00007FFDF74E1FDB vmovupd   xmm1,[rcx]
00007FFDF74E1FDF vmulps    xmm0,xmm0,xmm1
00007FFDF74E1FE3 vmovshdup xmm1,xmm0
00007FFDF74E1FE7 vaddps    xmm1,xmm1,xmm0
00007FFDF74E1FEB vunpcklps xmm0,xmm0,xmm0
00007FFDF74E1FEF vaddps    xmm0,xmm1,xmm0
00007FFDF74E1FF3 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E1FF7 vxorps    xmm1,xmm1,xmm1
00007FFDF74E1FFB vmovss    [rsp+0Ch],xmm1
00007FFDF74E2001 vmovss    [rsp+0Ch],xmm0
00007FFDF74E2007 vmovss    [rsp+10h],xmm1
00007FFDF74E200D vmovss    [rsp+14h],xmm1
00007FFDF74E2013 vmovss    xmm0,[rsp+0Ch]
00007FFDF74E2019 vmovss    [rsp+10h],xmm1
00007FFDF74E201F vmovss    [rsp+14h],xmm0
00007FFDF74E2025 mov       rax,[rsp+10h]
00007FFDF74E202A add       rsp,18h
00007FFDF74E202E ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Plane op_BitwiseAnd(IdealLine, Point)
00007FFDF74E2060 vzeroupper
00007FFDF74E2063 xchg      ax,ax
00007FFDF74E2065 vmovupd   xmm0,[r8]
00007FFDF74E206A vmovupd   xmm1,[rdx]
00007FFDF74E206E vmovaps   xmm2,xmm0
00007FFDF74E2072 vmovaps   xmm3,xmm1
00007FFDF74E2076 vshufps   xmm0,xmm0,xmm0,1
00007FFDF74E207B vmulps    xmm0,xmm0,xmm1
00007FFDF74E207F vxorps    xmm1,xmm1,xmm1
00007FFDF74E2083 vmovss    xmm4,[rel 7FFD`F74E`20D0h]
00007FFDF74E208B vinsertps xmm1,xmm1,xmm4,10h
00007FFDF74E2091 vmovss    xmm4,[rel 7FFD`F74E`20D4h]
00007FFDF74E2099 vinsertps xmm1,xmm1,xmm4,20h
00007FFDF74E209F vmovss    xmm4,[rel 7FFD`F74E`20D8h]
00007FFDF74E20A7 vinsertps xmm1,xmm1,xmm4,30h
00007FFDF74E20AD vmulps    xmm0,xmm0,xmm1
00007FFDF74E20B1 vdpps     xmm1,xmm2,xmm3,0E1h
00007FFDF74E20B7 vaddss    xmm0,xmm0,xmm1
00007FFDF74E20BB vmovupd   [rcx],xmm0
00007FFDF74E20BF mov       rax,rcx
00007FFDF74E20C2 ret

# -----------------------------------------------------------------------------------

# [struct IdealLine] Line op_Implicit(IdealLine)
00007FFDF74E20F0 vzeroupper
00007FFDF74E20F3 xchg      ax,ax
00007FFDF74E20F5 vmovupd   xmm0,[rdx]
00007FFDF74E20F9 vxorps    xmm1,xmm1,xmm1
00007FFDF74E20FD vmovupd   [rcx],xmm1
00007FFDF74E2101 vmovupd   [rcx+10h],xmm0
00007FFDF74E2106 mov       rax,rcx
00007FFDF74E2109 ret

# -----------------------------------------------------------------------------------

# [struct Line] Void Store(Single*)
00007FFDF74E2120 vzeroupper
00007FFDF74E2123 xchg      ax,ax
00007FFDF74E2125 vmovupd   xmm0,[rcx]
00007FFDF74E2129 vmovups   [rdx],xmm0
00007FFDF74E212D vmovupd   xmm0,[rcx+10h]
00007FFDF74E2132 vmovups   [rdx+10h],xmm0
00007FFDF74E2137 ret

# -----------------------------------------------------------------------------------

# [struct Line] Void Store(System.Span`1[System.Single])
00007FFDF74E2150 push      rsi
00007FFDF74E2151 sub       rsp,30h
00007FFDF74E2155 vzeroupper
00007FFDF74E2158 xor       eax,eax
00007FFDF74E215A mov       [rsp+28h],rax
00007FFDF74E215F mov       rax,[rdx]
00007FFDF74E2162 mov       edx,[rdx+8]
00007FFDF74E2165 vmovupd   xmm0,[rcx]
00007FFDF74E2169 vmovupd   xmm1,[rcx+10h]
00007FFDF74E216E xor       ecx,ecx
00007FFDF74E2170 mov       [rsp+28h],rcx
00007FFDF74E2175 cmp       edx,8
00007FFDF74E2178 jl        short 0000`7FFD`F74E`219Eh
00007FFDF74E217A xor       ecx,ecx
00007FFDF74E217C test      edx,edx
00007FFDF74E217E je        short 0000`7FFD`F74E`2183h
00007FFDF74E2180 mov       rcx,rax
00007FFDF74E2183 mov       [rsp+28h],rcx
00007FFDF74E2188 vmovups   [rcx],xmm0
00007FFDF74E218C vmovups   [rcx+10h],xmm1
00007FFDF74E2191 xor       ecx,ecx
00007FFDF74E2193 mov       [rsp+28h],rcx
00007FFDF74E2198 add       rsp,30h
00007FFDF74E219C pop       rsi
00007FFDF74E219D ret
00007FFDF74E219E mov       rcx,7FFD`F72F`D4C0h
00007FFDF74E21A8 call      0000`7FFE`56D7`7710h
00007FFDF74E21AD mov       rsi,rax
00007FFDF74E21B0 mov       ecx,25h
00007FFDF74E21B5 mov       rdx,7FFD`F731`9EA0h
00007FFDF74E21BF call      0000`7FFE`56EA`03E0h
00007FFDF74E21C4 mov       rdx,rax
00007FFDF74E21C7 mov       rcx,rsi
00007FFDF74E21CA call      0000`7FFD`F725`D238h
00007FFDF74E21CF mov       rcx,rsi
00007FFDF74E21D2 call      0000`7FFE`56D3`B3A0h
00007FFDF74E21D7 int3

# -----------------------------------------------------------------------------------

# [struct Line] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef, Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E2440 vzeroupper
00007FFDF74E2443 xchg      ax,ax
00007FFDF74E2445 vmovupd   xmm0,[rcx+10h]
00007FFDF74E244A vextractps eax,xmm0,1
00007FFDF74E2450 vmovd     xmm0,eax
00007FFDF74E2454 vmovss    [rdx],xmm0
00007FFDF74E2458 vmovupd   xmm0,[rcx+10h]
00007FFDF74E245D vextractps eax,xmm0,2
00007FFDF74E2463 vmovd     xmm0,eax
00007FFDF74E2467 vmovss    [r8],xmm0
00007FFDF74E246C vmovupd   xmm0,[rcx+10h]
00007FFDF74E2471 vextractps eax,xmm0,3
00007FFDF74E2477 vmovd     xmm0,eax
00007FFDF74E247B vmovss    [r9],xmm0
00007FFDF74E2480 vmovupd   xmm0,[rcx]
00007FFDF74E2484 vextractps eax,xmm0,1
00007FFDF74E248A vmovd     xmm0,eax
00007FFDF74E248E mov       rax,[rsp+28h]
00007FFDF74E2493 vmovss    [rax],xmm0
00007FFDF74E2497 vmovupd   xmm0,[rcx]
00007FFDF74E249B vextractps eax,xmm0,2
00007FFDF74E24A1 vmovd     xmm0,eax
00007FFDF74E24A5 mov       rax,[rsp+30h]
00007FFDF74E24AA vmovss    [rax],xmm0
00007FFDF74E24AE vmovupd   xmm0,[rcx]
00007FFDF74E24B2 vextractps eax,xmm0,3
00007FFDF74E24B8 vmovd     xmm0,eax
00007FFDF74E24BC mov       rax,[rsp+38h]
00007FFDF74E24C1 vmovss    [rax],xmm0
00007FFDF74E24C5 ret

# -----------------------------------------------------------------------------------

# [struct Line] Void Deconstruct(m128 ByRef, m128 ByRef)
00007FFDF74E24F0 vzeroupper
00007FFDF74E24F3 xchg      ax,ax
00007FFDF74E24F5 vmovupd   xmm0,[rcx]
00007FFDF74E24F9 vmovupd   [rdx],xmm0
00007FFDF74E24FD add       rcx,10h
00007FFDF74E2501 vmovupd   xmm0,[rcx]
00007FFDF74E2505 vmovupd   [r8],xmm0
00007FFDF74E250A ret

# -----------------------------------------------------------------------------------

# [struct Line] Single Norm()
00007FFDF74E2520 push      rax
00007FFDF74E2521 vzeroupper
00007FFDF74E2524 nop
00007FFDF74E2525 vmovupd   xmm0,[rcx]
00007FFDF74E2529 vmovaps   xmm1,xmm0
00007FFDF74E252D vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74E2533 vxorps    xmm1,xmm1,xmm1
00007FFDF74E2537 vmovss    [rsp+4],xmm1
00007FFDF74E253D vmovss    [rsp+4],xmm0
00007FFDF74E2543 vsqrtss   xmm0,xmm0,[rsp+4]
00007FFDF74E2549 add       rsp,8
00007FFDF74E254D ret

# -----------------------------------------------------------------------------------

# [struct Line] Single SquaredNorm()
00007FFDF74E2570 push      rax
00007FFDF74E2571 vzeroupper
00007FFDF74E2574 nop
00007FFDF74E2575 vmovupd   xmm0,[rcx]
00007FFDF74E2579 vmovaps   xmm1,xmm0
00007FFDF74E257D vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74E2583 vxorps    xmm1,xmm1,xmm1
00007FFDF74E2587 vmovss    [rsp+4],xmm1
00007FFDF74E258D vmovss    [rsp+4],xmm0
00007FFDF74E2593 vmovss    xmm0,[rsp+4]
00007FFDF74E2599 add       rsp,8
00007FFDF74E259D ret

# -----------------------------------------------------------------------------------

# [struct Line] Line Normalized()
00007FFDF74E29C0 sub       rsp,18h
00007FFDF74E29C4 vzeroupper
00007FFDF74E29C7 vmovaps   [rsp],xmm6
00007FFDF74E29CC vmovupd   xmm0,[rcx]
00007FFDF74E29D0 vmovupd   xmm1,[rcx+10h]
00007FFDF74E29D5 vmovaps   xmm2,xmm0
00007FFDF74E29D9 vmovaps   xmm3,xmm0
00007FFDF74E29DD vdpps     xmm2,xmm2,xmm3,0EFh
00007FFDF74E29E3 vmovaps   xmm3,xmm2
00007FFDF74E29E7 vrsqrtps  xmm4,xmm2
00007FFDF74E29EB vmulps    xmm5,xmm4,xmm4
00007FFDF74E29EF vmulps    xmm5,xmm2,xmm5
00007FFDF74E29F3 vmovss    xmm2,[rel 7FFD`F74E`2A98h]
00007FFDF74E29FB vbroadcastss xmm2,xmm2
00007FFDF74E2A00 vsubps    xmm2,xmm2,xmm5
00007FFDF74E2A04 vmovss    xmm5,[rel 7FFD`F74E`2A9Ch]
00007FFDF74E2A0C vbroadcastss xmm5,xmm5
00007FFDF74E2A11 vmulps    xmm4,xmm5,xmm4
00007FFDF74E2A15 vmulps    xmm2,xmm4,xmm2
00007FFDF74E2A19 vmovaps   xmm4,xmm0
00007FFDF74E2A1D vmovaps   xmm5,xmm1
00007FFDF74E2A21 vdpps     xmm4,xmm4,xmm5,0EFh
00007FFDF74E2A27 vrcpps    xmm5,xmm3
00007FFDF74E2A2B vmulps    xmm3,xmm3,xmm5
00007FFDF74E2A2F vmovss    xmm6,[rel 7FFD`F74E`2AA0h]
00007FFDF74E2A37 vbroadcastss xmm6,xmm6
00007FFDF74E2A3C vsubps    xmm3,xmm6,xmm3
00007FFDF74E2A40 vmulps    xmm3,xmm5,xmm3
00007FFDF74E2A44 vmulps    xmm3,xmm4,xmm3
00007FFDF74E2A48 vmulps    xmm3,xmm3,xmm2
00007FFDF74E2A4C vmulps    xmm1,xmm1,xmm2
00007FFDF74E2A50 vmulps    xmm3,xmm0,xmm3
00007FFDF74E2A54 vsubps    xmm1,xmm1,xmm3
00007FFDF74E2A58 vmulps    xmm0,xmm0,xmm2
00007FFDF74E2A5C vmovupd   [rdx],xmm0
00007FFDF74E2A60 vmovupd   [rdx+10h],xmm1
00007FFDF74E2A65 mov       rax,rdx
00007FFDF74E2A68 vmovaps   xmm6,[rsp]
00007FFDF74E2A6D add       rsp,18h
00007FFDF74E2A71 ret

# -----------------------------------------------------------------------------------

# [struct Line] Line Inverse()
00007FFDF74E2AC0 sub       rsp,18h
00007FFDF74E2AC4 vzeroupper
00007FFDF74E2AC7 vmovaps   [rsp],xmm6
00007FFDF74E2ACC vmovupd   xmm0,[rcx]
00007FFDF74E2AD0 vmovupd   xmm1,[rcx+10h]
00007FFDF74E2AD5 vmovaps   xmm2,xmm0
00007FFDF74E2AD9 vmovaps   xmm3,xmm0
00007FFDF74E2ADD vdpps     xmm2,xmm2,xmm3,0EFh
00007FFDF74E2AE3 vmovaps   xmm3,xmm2
00007FFDF74E2AE7 vrsqrtps  xmm4,xmm2
00007FFDF74E2AEB vmulps    xmm5,xmm4,xmm4
00007FFDF74E2AEF vmulps    xmm5,xmm2,xmm5
00007FFDF74E2AF3 vmovss    xmm2,[rel 7FFD`F74E`2BE0h]
00007FFDF74E2AFB vbroadcastss xmm2,xmm2
00007FFDF74E2B00 vsubps    xmm2,xmm2,xmm5
00007FFDF74E2B04 vmovss    xmm5,[rel 7FFD`F74E`2BE4h]
00007FFDF74E2B0C vbroadcastss xmm5,xmm5
00007FFDF74E2B11 vmulps    xmm4,xmm5,xmm4
00007FFDF74E2B15 vmulps    xmm2,xmm4,xmm2
00007FFDF74E2B19 vmovaps   xmm4,xmm0
00007FFDF74E2B1D vmovaps   xmm5,xmm1
00007FFDF74E2B21 vdpps     xmm4,xmm4,xmm5,0EFh
00007FFDF74E2B27 vrcpps    xmm5,xmm3
00007FFDF74E2B2B vmulps    xmm3,xmm3,xmm5
00007FFDF74E2B2F vmovss    xmm6,[rel 7FFD`F74E`2BE8h]
00007FFDF74E2B37 vbroadcastss xmm6,xmm6
00007FFDF74E2B3C vsubps    xmm3,xmm6,xmm3
00007FFDF74E2B40 vmulps    xmm3,xmm5,xmm3
00007FFDF74E2B44 vmulps    xmm4,xmm4,xmm3
00007FFDF74E2B48 vmulps    xmm4,xmm4,xmm2
00007FFDF74E2B4C vxorps    xmm5,xmm5,xmm5
00007FFDF74E2B50 vmovss    xmm6,[rel 7FFD`F74E`2BECh]
00007FFDF74E2B58 vinsertps xmm5,xmm5,xmm6,10h
00007FFDF74E2B5E vmovss    xmm6,[rel 7FFD`F74E`2BF0h]
00007FFDF74E2B66 vinsertps xmm5,xmm5,xmm6,20h
00007FFDF74E2B6C vmovss    xmm6,[rel 7FFD`F74E`2BF4h]
00007FFDF74E2B74 vinsertps xmm5,xmm5,xmm6,30h
00007FFDF74E2B7A vmulps    xmm2,xmm2,xmm4
00007FFDF74E2B7E vmulps    xmm2,xmm0,xmm2
00007FFDF74E2B82 vmulps    xmm1,xmm1,xmm3
00007FFDF74E2B86 vaddps    xmm2,xmm2,xmm2
00007FFDF74E2B8A vsubps    xmm1,xmm1,xmm2
00007FFDF74E2B8E vxorps    xmm1,xmm1,xmm5
00007FFDF74E2B92 vmulps    xmm0,xmm0,xmm3
00007FFDF74E2B96 vxorps    xmm0,xmm0,xmm5
00007FFDF74E2B9A vmovupd   [rdx],xmm0
00007FFDF74E2B9E vmovupd   [rdx+10h],xmm1
00007FFDF74E2BA3 mov       rax,rdx
00007FFDF74E2BA6 vmovaps   xmm6,[rsp]
00007FFDF74E2BAB add       rsp,18h
00007FFDF74E2BAF ret

# -----------------------------------------------------------------------------------

# [struct Line] Boolean Equals(Line ByRef, Single)
00007FFDF74E2C10 vzeroupper
00007FFDF74E2C13 xchg      ax,ax
00007FFDF74E2C15 vbroadcastss xmm0,xmm2
00007FFDF74E2C1A vmovss    xmm1,[rel 7FFD`F74E`2C78h]
00007FFDF74E2C22 vbroadcastss xmm1,xmm1
00007FFDF74E2C27 vmovupd   xmm2,[rcx]
00007FFDF74E2C2B vmovupd   xmm3,[rdx]
00007FFDF74E2C2F vsubps    xmm2,xmm2,xmm3
00007FFDF74E2C33 vandnps   xmm2,xmm1,xmm2
00007FFDF74E2C37 vcmpltps  xmm2,xmm2,xmm0
00007FFDF74E2C3C vmovupd   xmm3,[rcx+10h]
00007FFDF74E2C41 vmovupd   xmm4,[rdx+10h]
00007FFDF74E2C46 vsubps    xmm3,xmm3,xmm4
00007FFDF74E2C4A vandnps   xmm1,xmm1,xmm3
00007FFDF74E2C4E vcmpltps  xmm0,xmm1,xmm0
00007FFDF74E2C53 vandps    xmm0,xmm2,xmm0
00007FFDF74E2C57 vmovmskps eax,xmm0
00007FFDF74E2C5B cmp       eax,0Fh
00007FFDF74E2C5E sete      al
00007FFDF74E2C61 movzx     eax,al
00007FFDF74E2C64 ret

# -----------------------------------------------------------------------------------

# [struct Line] Boolean op_Equality(Line ByRef, Line ByRef)
00007FFDF74E2E00 vzeroupper
00007FFDF74E2E03 xchg      ax,ax
00007FFDF74E2E05 vmovupd   xmm0,[rdx]
00007FFDF74E2E09 vmovupd   xmm1,[rdx+10h]
00007FFDF74E2E0E vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E2E13 vmovmskps eax,xmm0
00007FFDF74E2E17 cmp       eax,0Fh
00007FFDF74E2E1A sete      al
00007FFDF74E2E1D movzx     eax,al
00007FFDF74E2E20 test      eax,eax
00007FFDF74E2E22 je        short 0000`7FFD`F74E`2E3Ch
00007FFDF74E2E24 add       rcx,10h
00007FFDF74E2E28 vcmpeqps  xmm0,xmm1,[rcx]
00007FFDF74E2E2D vmovmskps eax,xmm0
00007FFDF74E2E31 cmp       eax,0Fh
00007FFDF74E2E34 sete      al
00007FFDF74E2E37 movzx     eax,al
00007FFDF74E2E3A jmp       short 0000`7FFD`F74E`2E3Eh
00007FFDF74E2E3C xor       eax,eax
00007FFDF74E2E3E ret

# -----------------------------------------------------------------------------------

# [struct Line] Boolean op_Inequality(Line ByRef, Line ByRef)
00007FFDF74E2E60 vzeroupper
00007FFDF74E2E63 xchg      ax,ax
00007FFDF74E2E65 vmovupd   xmm0,[rdx]
00007FFDF74E2E69 vmovupd   xmm1,[rdx+10h]
00007FFDF74E2E6E vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E2E73 vmovmskps eax,xmm0
00007FFDF74E2E77 cmp       eax,0Fh
00007FFDF74E2E7A sete      al
00007FFDF74E2E7D movzx     eax,al
00007FFDF74E2E80 test      eax,eax
00007FFDF74E2E82 je        short 0000`7FFD`F74E`2E9Ch
00007FFDF74E2E84 add       rcx,10h
00007FFDF74E2E88 vcmpeqps  xmm0,xmm1,[rcx]
00007FFDF74E2E8D vmovmskps eax,xmm0
00007FFDF74E2E91 cmp       eax,0Fh
00007FFDF74E2E94 sete      al
00007FFDF74E2E97 movzx     eax,al
00007FFDF74E2E9A jmp       short 0000`7FFD`F74E`2E9Eh
00007FFDF74E2E9C xor       eax,eax
00007FFDF74E2E9E test      eax,eax
00007FFDF74E2EA0 sete      al
00007FFDF74E2EA3 movzx     eax,al
00007FFDF74E2EA6 ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_Addition(Line ByRef, Line ByRef)
00007FFDF74E2EC0 vzeroupper
00007FFDF74E2EC3 xchg      ax,ax
00007FFDF74E2EC5 vmovupd   xmm0,[rdx]
00007FFDF74E2EC9 vmovupd   xmm1,[r8]
00007FFDF74E2ECE vaddps    xmm0,xmm0,xmm1
00007FFDF74E2ED2 vmovupd   xmm1,[rdx+10h]
00007FFDF74E2ED7 vmovupd   xmm2,[r8+10h]
00007FFDF74E2EDD vaddps    xmm1,xmm1,xmm2
00007FFDF74E2EE1 vmovupd   [rcx],xmm0
00007FFDF74E2EE5 vmovupd   [rcx+10h],xmm1
00007FFDF74E2EEA mov       rax,rcx
00007FFDF74E2EED ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_Subtraction(Line ByRef, Line ByRef)
00007FFDF74E2F10 vzeroupper
00007FFDF74E2F13 xchg      ax,ax
00007FFDF74E2F15 vmovupd   xmm0,[rdx]
00007FFDF74E2F19 vmovupd   xmm1,[r8]
00007FFDF74E2F1E vsubps    xmm0,xmm0,xmm1
00007FFDF74E2F22 vmovupd   xmm1,[rdx+10h]
00007FFDF74E2F27 vmovupd   xmm2,[r8+10h]
00007FFDF74E2F2D vsubps    xmm1,xmm1,xmm2
00007FFDF74E2F31 vmovupd   [rcx],xmm0
00007FFDF74E2F35 vmovupd   [rcx+10h],xmm1
00007FFDF74E2F3A mov       rax,rcx
00007FFDF74E2F3D ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_Multiply(Line ByRef, Single)
00007FFDF74E2F60 vzeroupper
00007FFDF74E2F63 xchg      ax,ax
00007FFDF74E2F65 vbroadcastss xmm0,xmm2
00007FFDF74E2F6A vmovupd   xmm1,[rdx]
00007FFDF74E2F6E vmulps    xmm1,xmm1,xmm0
00007FFDF74E2F72 vmovupd   xmm2,[rdx+10h]
00007FFDF74E2F77 vmulps    xmm0,xmm2,xmm0
00007FFDF74E2F7B vmovupd   [rcx],xmm1
00007FFDF74E2F7F vmovupd   [rcx+10h],xmm0
00007FFDF74E2F84 mov       rax,rcx
00007FFDF74E2F87 ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_Multiply(Single, Line ByRef)
00007FFDF74E2FA0 vzeroupper
00007FFDF74E2FA3 xchg      ax,ax
00007FFDF74E2FA5 vbroadcastss xmm0,xmm1
00007FFDF74E2FAA vmovupd   xmm1,[r8]
00007FFDF74E2FAF vmulps    xmm1,xmm1,xmm0
00007FFDF74E2FB3 vmovupd   xmm2,[r8+10h]
00007FFDF74E2FB9 vmulps    xmm0,xmm2,xmm0
00007FFDF74E2FBD vmovupd   [rcx],xmm1
00007FFDF74E2FC1 vmovupd   [rcx+10h],xmm0
00007FFDF74E2FC6 mov       rax,rcx
00007FFDF74E2FC9 ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_Division(Line ByRef, Single)
00007FFDF74E2FE0 vzeroupper
00007FFDF74E2FE3 xchg      ax,ax
00007FFDF74E2FE5 vbroadcastss xmm0,xmm2
00007FFDF74E2FEA vrcpps    xmm1,xmm0
00007FFDF74E2FEE vmulps    xmm0,xmm0,xmm1
00007FFDF74E2FF2 vmovss    xmm2,[rel 7FFD`F74E`3038h]
00007FFDF74E2FFA vbroadcastss xmm2,xmm2
00007FFDF74E2FFF vsubps    xmm0,xmm2,xmm0
00007FFDF74E3003 vmulps    xmm0,xmm1,xmm0
00007FFDF74E3007 vmovupd   xmm1,[rdx]
00007FFDF74E300B vmulps    xmm1,xmm1,xmm0
00007FFDF74E300F vmovupd   xmm2,[rdx+10h]
00007FFDF74E3014 vmulps    xmm0,xmm2,xmm0
00007FFDF74E3018 vmovupd   [rcx],xmm1
00007FFDF74E301C vmovupd   [rcx+10h],xmm0
00007FFDF74E3021 mov       rax,rcx
00007FFDF74E3024 ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_UnaryNegation(Line ByRef)
00007FFDF74E3050 vzeroupper
00007FFDF74E3053 xchg      ax,ax
00007FFDF74E3055 vmovss    xmm0,[rel 7FFD`F74E`3088h]
00007FFDF74E305D vbroadcastss xmm0,xmm0
00007FFDF74E3062 vmovupd   xmm1,[rdx]
00007FFDF74E3066 vxorps    xmm1,xmm1,xmm0
00007FFDF74E306A vmovupd   xmm2,[rdx+10h]
00007FFDF74E306F vxorps    xmm0,xmm2,xmm0
00007FFDF74E3073 vmovupd   [rcx],xmm1
00007FFDF74E3077 vmovupd   [rcx+10h],xmm0
00007FFDF74E307C mov       rax,rcx
00007FFDF74E307F ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_OnesComplement(Line ByRef)
00007FFDF74E30A0 vzeroupper
00007FFDF74E30A3 xchg      ax,ax
00007FFDF74E30A5 vxorps    xmm0,xmm0,xmm0
00007FFDF74E30A9 vmovss    xmm1,[rel 7FFD`F74E`3100h]
00007FFDF74E30B1 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74E30B7 vmovss    xmm1,[rel 7FFD`F74E`3104h]
00007FFDF74E30BF vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74E30C5 vmovss    xmm1,[rel 7FFD`F74E`3108h]
00007FFDF74E30CD vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74E30D3 vmovupd   xmm1,[rdx]
00007FFDF74E30D7 vxorps    xmm1,xmm1,xmm0
00007FFDF74E30DB vmovupd   xmm2,[rdx+10h]
00007FFDF74E30E0 vxorps    xmm0,xmm2,xmm0
00007FFDF74E30E4 vmovupd   [rcx],xmm1
00007FFDF74E30E8 vmovupd   [rcx+10h],xmm0
00007FFDF74E30ED mov       rax,rcx
00007FFDF74E30F0 ret

# -----------------------------------------------------------------------------------

# [struct Line] Line op_LogicalNot(Line ByRef)
00007FFDF74E3120 vzeroupper
00007FFDF74E3123 xchg      ax,ax
00007FFDF74E3125 vmovupd   xmm0,[rdx+10h]
00007FFDF74E312A vmovupd   xmm1,[rdx]
00007FFDF74E312E vmovupd   [rcx],xmm0
00007FFDF74E3132 vmovupd   [rcx+10h],xmm1
00007FFDF74E3137 mov       rax,rcx
00007FFDF74E313A ret

# -----------------------------------------------------------------------------------

# [struct Line] Dual op_ExclusiveOr(Line, IdealLine)
00007FFDF74E3150 sub       rsp,18h
00007FFDF74E3154 vzeroupper
00007FFDF74E3157 vmovupd   xmm0,[rcx]
00007FFDF74E315B vmovupd   xmm1,[rdx]
00007FFDF74E315F vmulps    xmm0,xmm0,xmm1
00007FFDF74E3163 vmovshdup xmm1,xmm0
00007FFDF74E3167 vaddps    xmm1,xmm1,xmm0
00007FFDF74E316B vunpcklps xmm0,xmm0,xmm0
00007FFDF74E316F vaddps    xmm0,xmm1,xmm0
00007FFDF74E3173 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E3177 vxorps    xmm1,xmm1,xmm1
00007FFDF74E317B vmovss    [rsp+0Ch],xmm1
00007FFDF74E3181 vmovss    [rsp+0Ch],xmm0
00007FFDF74E3187 vmovss    [rsp+10h],xmm1
00007FFDF74E318D vmovss    [rsp+14h],xmm1
00007FFDF74E3193 vmovss    xmm0,[rsp+0Ch]
00007FFDF74E3199 vmovss    [rsp+10h],xmm1
00007FFDF74E319F vmovss    [rsp+14h],xmm0
00007FFDF74E31A5 mov       rax,[rsp+10h]
00007FFDF74E31AA add       rsp,18h
00007FFDF74E31AE ret

# -----------------------------------------------------------------------------------

# [struct Line] Dual op_ExclusiveOr(Line, Line)
00007FFDF74E31E0 sub       rsp,18h
00007FFDF74E31E4 vzeroupper
00007FFDF74E31E7 xor       eax,eax
00007FFDF74E31E9 mov       [rsp+0Ch],eax
00007FFDF74E31ED mov       [rsp+8],eax
00007FFDF74E31F1 vmovupd   xmm0,[rcx]
00007FFDF74E31F5 vmovupd   xmm1,[rdx+10h]
00007FFDF74E31FA vmulps    xmm0,xmm0,xmm1
00007FFDF74E31FE vmovshdup xmm1,xmm0
00007FFDF74E3202 vaddps    xmm1,xmm1,xmm0
00007FFDF74E3206 vunpcklps xmm0,xmm0,xmm0
00007FFDF74E320A vaddps    xmm0,xmm1,xmm0
00007FFDF74E320E vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E3212 vmovupd   xmm1,[rdx]
00007FFDF74E3216 vmovupd   xmm2,[rcx+10h]
00007FFDF74E321B vmulps    xmm1,xmm1,xmm2
00007FFDF74E321F vmovshdup xmm2,xmm1
00007FFDF74E3223 vaddps    xmm2,xmm2,xmm1
00007FFDF74E3227 vunpcklps xmm1,xmm1,xmm1
00007FFDF74E322B vaddps    xmm1,xmm2,xmm1
00007FFDF74E322F vmovhlps  xmm1,xmm1,xmm1
00007FFDF74E3233 vxorps    xmm2,xmm2,xmm2
00007FFDF74E3237 vmovss    [rsp+0Ch],xmm2
00007FFDF74E323D vmovss    [rsp+0Ch],xmm0
00007FFDF74E3243 vmovss    xmm0,[rsp+0Ch]
00007FFDF74E3249 vmovss    [rsp+8],xmm2
00007FFDF74E324F vmovss    [rsp+8],xmm1
00007FFDF74E3255 vmovss    [rsp+10h],xmm2
00007FFDF74E325B vmovss    [rsp+14h],xmm2
00007FFDF74E3261 vaddss    xmm0,xmm0,[rsp+8]
00007FFDF74E3267 vmovss    [rsp+10h],xmm2
00007FFDF74E326D vmovss    [rsp+14h],xmm0
00007FFDF74E3273 mov       rax,[rsp+10h]
00007FFDF74E3278 add       rsp,18h
00007FFDF74E327C ret

# -----------------------------------------------------------------------------------

# [struct Line] Point op_ExclusiveOr(Line, Plane)
00007FFDF74E32B0 sub       rsp,18h
00007FFDF74E32B4 vzeroupper
00007FFDF74E32B7 vmovaps   [rsp],xmm6
00007FFDF74E32BC vmovupd   xmm0,[r8]
00007FFDF74E32C1 vmovupd   xmm1,[rdx]
00007FFDF74E32C5 vmovupd   xmm2,[rdx+10h]
00007FFDF74E32CA vmovaps   xmm3,xmm0
00007FFDF74E32CE vmovaps   xmm4,xmm1
00007FFDF74E32D2 vshufps   xmm5,xmm0,xmm0,1
00007FFDF74E32D7 vmulps    xmm1,xmm5,xmm1
00007FFDF74E32DB vxorps    xmm5,xmm5,xmm5
00007FFDF74E32DF vmovss    xmm6,[rel 7FFD`F74E`3360h]
00007FFDF74E32E7 vinsertps xmm5,xmm5,xmm6,10h
00007FFDF74E32ED vmovss    xmm6,[rel 7FFD`F74E`3364h]
00007FFDF74E32F5 vinsertps xmm5,xmm5,xmm6,20h
00007FFDF74E32FB vmovss    xmm6,[rel 7FFD`F74E`3368h]
00007FFDF74E3303 vinsertps xmm5,xmm5,xmm6,30h
00007FFDF74E3309 vmulps    xmm1,xmm1,xmm5
00007FFDF74E330D vdpps     xmm3,xmm3,xmm4,0E1h
00007FFDF74E3313 vaddss    xmm1,xmm1,xmm3
00007FFDF74E3317 vshufps   xmm3,xmm2,xmm2,78h
00007FFDF74E331C vmulps    xmm3,xmm0,xmm3
00007FFDF74E3320 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E3325 vmulps    xmm0,xmm0,xmm2
00007FFDF74E3329 vsubps    xmm0,xmm3,xmm0
00007FFDF74E332D vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E3332 vaddps    xmm1,xmm0,xmm1
00007FFDF74E3336 vmovupd   [rcx],xmm1
00007FFDF74E333A mov       rax,rcx
00007FFDF74E333D vmovaps   xmm6,[rsp]
00007FFDF74E3342 add       rsp,18h
00007FFDF74E3346 ret

# -----------------------------------------------------------------------------------

# [struct Line] Dual op_ExclusiveOr(Line, Branch)
00007FFDF74E3390 sub       rsp,18h
00007FFDF74E3394 vzeroupper
00007FFDF74E3397 vmovupd   xmm0,[rcx+10h]
00007FFDF74E339C vmovupd   xmm1,[rdx]
00007FFDF74E33A0 vmulps    xmm0,xmm1,xmm0
00007FFDF74E33A4 vmovshdup xmm1,xmm0
00007FFDF74E33A8 vaddps    xmm1,xmm1,xmm0
00007FFDF74E33AC vunpcklps xmm0,xmm0,xmm0
00007FFDF74E33B0 vaddps    xmm0,xmm1,xmm0
00007FFDF74E33B4 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E33B8 vxorps    xmm1,xmm1,xmm1
00007FFDF74E33BC vmovss    [rsp+0Ch],xmm1
00007FFDF74E33C2 vmovss    [rsp+0Ch],xmm0
00007FFDF74E33C8 vmovss    [rsp+10h],xmm1
00007FFDF74E33CE vmovss    [rsp+14h],xmm1
00007FFDF74E33D4 vmovss    xmm0,[rsp+0Ch]
00007FFDF74E33DA vmovss    [rsp+10h],xmm1
00007FFDF74E33E0 vmovss    [rsp+14h],xmm0
00007FFDF74E33E6 mov       rax,[rsp+10h]
00007FFDF74E33EB add       rsp,18h
00007FFDF74E33EF ret

# -----------------------------------------------------------------------------------

# [struct Line] Plane op_BitwiseOr(Line, Plane)
00007FFDF74E3420 vzeroupper
00007FFDF74E3423 xchg      ax,ax
00007FFDF74E3425 vmovupd   xmm0,[r8]
00007FFDF74E342A vmovupd   xmm1,[rdx]
00007FFDF74E342E vmovupd   xmm2,[rdx+10h]
00007FFDF74E3433 vshufps   xmm3,xmm1,xmm1,78h
00007FFDF74E3438 vmulps    xmm3,xmm0,xmm3
00007FFDF74E343C vshufps   xmm4,xmm0,xmm0,78h
00007FFDF74E3441 vmulps    xmm1,xmm4,xmm1
00007FFDF74E3445 vsubps    xmm3,xmm3,xmm1
00007FFDF74E3449 vshufps   xmm3,xmm3,xmm3,78h
00007FFDF74E344E vmulps    xmm0,xmm0,xmm2
00007FFDF74E3452 vmovshdup xmm1,xmm0
00007FFDF74E3456 vaddps    xmm1,xmm1,xmm0
00007FFDF74E345A vunpcklps xmm0,xmm0,xmm0
00007FFDF74E345E vaddps    xmm0,xmm1,xmm0
00007FFDF74E3462 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E3466 vaddss    xmm3,xmm3,xmm0
00007FFDF74E346A vmovupd   [rcx],xmm3
00007FFDF74E346E mov       rax,rcx
00007FFDF74E3471 ret

# -----------------------------------------------------------------------------------

# [struct Line] Single op_BitwiseOr(Line, Line)
00007FFDF74E34A0 push      rax
00007FFDF74E34A1 vzeroupper
00007FFDF74E34A4 nop
00007FFDF74E34A5 vmovupd   xmm0,[rcx]
00007FFDF74E34A9 vmovupd   xmm1,[rdx]
00007FFDF74E34AD vxorps    xmm2,xmm2,xmm2
00007FFDF74E34B1 vmovss    xmm3,[rel 7FFD`F74E`3508h]
00007FFDF74E34B9 vmovss    xmm2,xmm2,xmm3
00007FFDF74E34BD vmulps    xmm0,xmm0,xmm1
00007FFDF74E34C1 vmovshdup xmm1,xmm0
00007FFDF74E34C5 vaddps    xmm1,xmm1,xmm0
00007FFDF74E34C9 vunpcklps xmm0,xmm0,xmm0
00007FFDF74E34CD vaddps    xmm0,xmm1,xmm0
00007FFDF74E34D1 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E34D5 vxorps    xmm0,xmm2,xmm0
00007FFDF74E34D9 vxorps    xmm1,xmm1,xmm1
00007FFDF74E34DD vmovss    [rsp+4],xmm1
00007FFDF74E34E3 vmovss    [rsp+4],xmm0
00007FFDF74E34E9 vmovss    xmm0,[rsp+4]
00007FFDF74E34EF add       rsp,8
00007FFDF74E34F3 ret

# -----------------------------------------------------------------------------------

# [struct Line] Plane op_BitwiseOr(Line, Point)
00007FFDF74E3520 vzeroupper
00007FFDF74E3523 xchg      ax,ax
00007FFDF74E3525 vmovupd   xmm0,[r8]
00007FFDF74E352A vmovupd   xmm1,[rdx]
00007FFDF74E352E vmulps    xmm2,xmm0,xmm1
00007FFDF74E3532 vmovshdup xmm3,xmm2
00007FFDF74E3536 vaddps    xmm3,xmm3,xmm2
00007FFDF74E353A vunpcklps xmm2,xmm2,xmm2
00007FFDF74E353E vaddps    xmm2,xmm3,xmm2
00007FFDF74E3542 vmovhlps  xmm2,xmm2,xmm2
00007FFDF74E3546 vshufps   xmm0,xmm0,xmm0,0
00007FFDF74E354B vmulps    xmm0,xmm0,xmm1
00007FFDF74E354F vxorps    xmm1,xmm1,xmm1
00007FFDF74E3553 vmovss    xmm3,[rel 7FFD`F74E`35A0h]
00007FFDF74E355B vinsertps xmm1,xmm1,xmm3,10h
00007FFDF74E3561 vmovss    xmm3,[rel 7FFD`F74E`35A4h]
00007FFDF74E3569 vinsertps xmm1,xmm1,xmm3,20h
00007FFDF74E356F vmovss    xmm3,[rel 7FFD`F74E`35A8h]
00007FFDF74E3577 vinsertps xmm1,xmm1,xmm3,30h
00007FFDF74E357D vxorps    xmm0,xmm0,xmm1
00007FFDF74E3581 vblendps  xmm0,xmm0,xmm2,1
00007FFDF74E3587 vmovupd   [rcx],xmm0
00007FFDF74E358B mov       rax,rcx
00007FFDF74E358E ret

# -----------------------------------------------------------------------------------

# [struct Line] Motor op_Multiply(Line, Line)
00007FFDF74E35C0 sub       rsp,48h
00007FFDF74E35C4 vzeroupper
00007FFDF74E35C7 vmovaps   [rsp+30h],xmm6
00007FFDF74E35CD vmovaps   [rsp+20h],xmm7
00007FFDF74E35D3 vmovaps   [rsp+10h],xmm8
00007FFDF74E35D9 vmovaps   [rsp],xmm9
00007FFDF74E35DE vmovupd   xmm0,[rdx]
00007FFDF74E35E2 vmovupd   xmm1,[rdx+10h]
00007FFDF74E35E7 vmovupd   xmm2,[r8]
00007FFDF74E35EC vmovupd   xmm3,[r8+10h]
00007FFDF74E35F2 vxorps    xmm4,xmm4,xmm4
00007FFDF74E35F6 vmovss    xmm5,[rel 7FFD`F74E`3700h]
00007FFDF74E35FE vmovss    xmm4,xmm4,xmm5
00007FFDF74E3602 vshufps   xmm5,xmm0,xmm0,0D9h
00007FFDF74E3607 vshufps   xmm6,xmm2,xmm2,0B5h
00007FFDF74E360C vmulps    xmm5,xmm5,xmm6
00007FFDF74E3610 vxorps    xmm5,xmm5,xmm4
00007FFDF74E3614 vshufps   xmm6,xmm0,xmm0,0B7h
00007FFDF74E3619 vshufps   xmm7,xmm2,xmm2,0DBh
00007FFDF74E361E vmulps    xmm6,xmm6,xmm7
00007FFDF74E3622 vsubps    xmm5,xmm5,xmm6
00007FFDF74E3626 vunpckhps xmm6,xmm0,xmm0
00007FFDF74E362A vunpckhps xmm7,xmm2,xmm2
00007FFDF74E362E vmulss    xmm8,xmm6,xmm7
00007FFDF74E3632 vsubss    xmm5,xmm5,xmm8
00007FFDF74E3637 vshufps   xmm8,xmm0,xmm0,9Dh
00007FFDF74E363C vshufps   xmm9,xmm3,xmm3,79h
00007FFDF74E3641 vmulps    xmm8,xmm8,xmm9
00007FFDF74E3646 vshufps   xmm0,xmm0,xmm0,7Bh
00007FFDF74E364B vshufps   xmm9,xmm3,xmm3,9Fh
00007FFDF74E3650 vmulps    xmm0,xmm0,xmm9
00007FFDF74E3655 vxorps    xmm0,xmm4,xmm0
00007FFDF74E3659 vsubps    xmm8,xmm8,xmm0
00007FFDF74E365D vshufps   xmm0,xmm2,xmm2,79h
00007FFDF74E3662 vshufps   xmm9,xmm1,xmm1,9Dh
00007FFDF74E3667 vmulps    xmm0,xmm0,xmm9
00007FFDF74E366C vaddps    xmm8,xmm8,xmm0
00007FFDF74E3670 vshufps   xmm0,xmm2,xmm2,9Fh
00007FFDF74E3675 vshufps   xmm2,xmm1,xmm1,7Bh
00007FFDF74E367A vmulps    xmm0,xmm0,xmm2
00007FFDF74E367E vxorps    xmm0,xmm4,xmm0
00007FFDF74E3682 vsubps    xmm8,xmm8,xmm0
00007FFDF74E3686 vunpckhps xmm0,xmm3,xmm3
00007FFDF74E368A vunpckhps xmm1,xmm1,xmm1
00007FFDF74E368E vmulss    xmm0,xmm6,xmm0
00007FFDF74E3692 vaddss    xmm8,xmm8,xmm0
00007FFDF74E3696 vmovaps   xmm0,xmm8
00007FFDF74E369B vmulss    xmm1,xmm7,xmm1
00007FFDF74E369F vaddss    xmm8,xmm0,xmm1
00007FFDF74E36A3 vmovaps   xmm0,xmm8
00007FFDF74E36A8 vmovupd   [rcx],xmm5
00007FFDF74E36AC vmovupd   [rcx+10h],xmm0
00007FFDF74E36B1 mov       rax,rcx
00007FFDF74E36B4 vmovaps   xmm6,[rsp+30h]
00007FFDF74E36BA vmovaps   xmm7,[rsp+20h]
00007FFDF74E36C0 vmovaps   xmm8,[rsp+10h]
00007FFDF74E36C6 vmovaps   xmm9,[rsp]
00007FFDF74E36CB add       rsp,48h
00007FFDF74E36CF ret

# -----------------------------------------------------------------------------------

# [struct Line] Motor op_Division(Line, Line)
00007FFDF74E3730 push      rdi
00007FFDF74E3731 push      rsi
00007FFDF74E3732 sub       rsp,88h
00007FFDF74E3739 vzeroupper
00007FFDF74E373C vmovaps   [rsp+70h],xmm6
00007FFDF74E3742 vmovaps   [rsp+60h],xmm7
00007FFDF74E3748 vmovaps   [rsp+50h],xmm8
00007FFDF74E374E vmovaps   [rsp+40h],xmm9
00007FFDF74E3754 mov       rdi,rcx
00007FFDF74E3757 mov       rsi,rdx
00007FFDF74E375A lea       rdx,[rsp+20h]
00007FFDF74E375F mov       rcx,r8
00007FFDF74E3762 call      0000`7FFD`F74E`2AC0h
00007FFDF74E3767 vmovupd   xmm0,[rsi]
00007FFDF74E376B vmovupd   xmm1,[rsi+10h]
00007FFDF74E3770 vmovapd   xmm2,[rsp+20h]
00007FFDF74E3776 vmovapd   xmm3,[rsp+30h]
00007FFDF74E377C vxorps    xmm4,xmm4,xmm4
00007FFDF74E3780 vmovss    xmm5,[rel 7FFD`F74E`3890h]
00007FFDF74E3788 vmovss    xmm4,xmm4,xmm5
00007FFDF74E378C vshufps   xmm5,xmm0,xmm0,0D9h
00007FFDF74E3791 vshufps   xmm6,xmm2,xmm2,0B5h
00007FFDF74E3796 vmulps    xmm5,xmm5,xmm6
00007FFDF74E379A vxorps    xmm5,xmm5,xmm4
00007FFDF74E379E vshufps   xmm6,xmm0,xmm0,0B7h
00007FFDF74E37A3 vshufps   xmm7,xmm2,xmm2,0DBh
00007FFDF74E37A8 vmulps    xmm6,xmm6,xmm7
00007FFDF74E37AC vsubps    xmm5,xmm5,xmm6
00007FFDF74E37B0 vunpckhps xmm6,xmm0,xmm0
00007FFDF74E37B4 vunpckhps xmm7,xmm2,xmm2
00007FFDF74E37B8 vmulss    xmm8,xmm6,xmm7
00007FFDF74E37BC vsubss    xmm5,xmm5,xmm8
00007FFDF74E37C1 vshufps   xmm8,xmm0,xmm0,9Dh
00007FFDF74E37C6 vshufps   xmm9,xmm3,xmm3,79h
00007FFDF74E37CB vmulps    xmm8,xmm8,xmm9
00007FFDF74E37D0 vshufps   xmm0,xmm0,xmm0,7Bh
00007FFDF74E37D5 vshufps   xmm9,xmm3,xmm3,9Fh
00007FFDF74E37DA vmulps    xmm0,xmm0,xmm9
00007FFDF74E37DF vxorps    xmm0,xmm4,xmm0
00007FFDF74E37E3 vsubps    xmm8,xmm8,xmm0
00007FFDF74E37E7 vshufps   xmm0,xmm2,xmm2,79h
00007FFDF74E37EC vshufps   xmm9,xmm1,xmm1,9Dh
00007FFDF74E37F1 vmulps    xmm0,xmm0,xmm9
00007FFDF74E37F6 vaddps    xmm8,xmm8,xmm0
00007FFDF74E37FA vshufps   xmm0,xmm2,xmm2,9Fh
00007FFDF74E37FF vshufps   xmm2,xmm1,xmm1,7Bh
00007FFDF74E3804 vmulps    xmm0,xmm0,xmm2
00007FFDF74E3808 vxorps    xmm0,xmm4,xmm0
00007FFDF74E380C vsubps    xmm8,xmm8,xmm0
00007FFDF74E3810 vunpckhps xmm0,xmm3,xmm3
00007FFDF74E3814 vunpckhps xmm1,xmm1,xmm1
00007FFDF74E3818 vmulss    xmm0,xmm6,xmm0
00007FFDF74E381C vaddss    xmm8,xmm8,xmm0
00007FFDF74E3820 vmovaps   xmm0,xmm8
00007FFDF74E3825 vmulss    xmm1,xmm7,xmm1
00007FFDF74E3829 vaddss    xmm8,xmm0,xmm1
00007FFDF74E382D vmovaps   xmm0,xmm8
00007FFDF74E3832 vmovupd   [rdi],xmm5
00007FFDF74E3836 vmovupd   [rdi+10h],xmm0
00007FFDF74E383B mov       rax,rdi
00007FFDF74E383E vmovaps   xmm6,[rsp+70h]
00007FFDF74E3844 vmovaps   xmm7,[rsp+60h]
00007FFDF74E384A vmovaps   xmm8,[rsp+50h]
00007FFDF74E3850 vmovaps   xmm9,[rsp+40h]
00007FFDF74E3856 add       rsp,88h
00007FFDF74E385D pop       rsi
00007FFDF74E385E pop       rdi
00007FFDF74E385F ret

# -----------------------------------------------------------------------------------

# [struct Line] Plane op_BitwiseAnd(Line, Point)
00007FFDF74E3AB0 sub       rsp,18h
00007FFDF74E3AB4 vzeroupper
00007FFDF74E3AB7 vmovaps   [rsp],xmm6
00007FFDF74E3ABC vmovupd   xmm0,[r8]
00007FFDF74E3AC1 vmovupd   xmm1,[rdx]
00007FFDF74E3AC5 vmovupd   xmm2,[rdx+10h]
00007FFDF74E3ACA vmovaps   xmm3,xmm0
00007FFDF74E3ACE vmovaps   xmm4,xmm2
00007FFDF74E3AD2 vmovaps   xmm5,xmm3
00007FFDF74E3AD6 vshufps   xmm0,xmm0,xmm0,1
00007FFDF74E3ADB vmulps    xmm0,xmm0,xmm2
00007FFDF74E3ADF vxorps    xmm2,xmm2,xmm2
00007FFDF74E3AE3 vmovss    xmm6,[rel 7FFD`F74E`3B68h]
00007FFDF74E3AEB vinsertps xmm2,xmm2,xmm6,10h
00007FFDF74E3AF1 vmovss    xmm6,[rel 7FFD`F74E`3B6Ch]
00007FFDF74E3AF9 vinsertps xmm2,xmm2,xmm6,20h
00007FFDF74E3AFF vmovss    xmm6,[rel 7FFD`F74E`3B70h]
00007FFDF74E3B07 vinsertps xmm2,xmm2,xmm6,30h
00007FFDF74E3B0D vmulps    xmm0,xmm0,xmm2
00007FFDF74E3B11 vdpps     xmm2,xmm5,xmm4,0E1h
00007FFDF74E3B17 vaddss    xmm0,xmm0,xmm2
00007FFDF74E3B1B vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74E3B20 vmulps    xmm2,xmm3,xmm2
00007FFDF74E3B24 vshufps   xmm3,xmm3,xmm3,78h
00007FFDF74E3B29 vmulps    xmm1,xmm3,xmm1
00007FFDF74E3B2D vsubps    xmm1,xmm2,xmm1
00007FFDF74E3B31 vshufps   xmm1,xmm1,xmm1,78h
00007FFDF74E3B36 vaddps    xmm0,xmm1,xmm0
00007FFDF74E3B3A vmovupd   [rcx],xmm0
00007FFDF74E3B3E mov       rax,rcx
00007FFDF74E3B41 vmovaps   xmm6,[rsp]
00007FFDF74E3B46 add       rsp,18h
00007FFDF74E3B4A ret

# -----------------------------------------------------------------------------------

# [struct Line] Branch op_Explicit(Line ByRef)
00007FFDF74E3B90 vzeroupper
00007FFDF74E3B93 xchg      ax,ax
00007FFDF74E3B95 vmovupd   xmm0,[rdx]
00007FFDF74E3B99 vmovupd   [rcx],xmm0
00007FFDF74E3B9D mov       rax,rcx
00007FFDF74E3BA0 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor Screw(Single, Single, Line)
00007FFDF74E3BC0 push      rdi
00007FFDF74E3BC1 push      rsi
00007FFDF74E3BC2 sub       rsp,188h
00007FFDF74E3BC9 vzeroupper
00007FFDF74E3BCC vmovaps   [rsp+170h],xmm6
00007FFDF74E3BD5 vmovaps   [rsp+160h],xmm7
00007FFDF74E3BDE vmovaps   [rsp+150h],xmm8
00007FFDF74E3BE7 vmovaps   [rsp+140h],xmm9
00007FFDF74E3BF0 vmovaps   [rsp+130h],xmm10
00007FFDF74E3BF9 vmovaps   [rsp+120h],xmm11
00007FFDF74E3C02 vmovaps   [rsp+110h],xmm12
00007FFDF74E3C0B mov       rsi,rcx
00007FFDF74E3C0E vxorps    xmm0,xmm0,xmm0
00007FFDF74E3C12 vmovapd   [rsp+0F0h],xmm0
00007FFDF74E3C1B vxorps    xmm0,xmm0,xmm0
00007FFDF74E3C1F vmovapd   [rsp+100h],xmm0
00007FFDF74E3C28 vmovupd   xmm0,[r9]
00007FFDF74E3C2D vmovupd   xmm3,[r9+10h]
00007FFDF74E3C33 vmovss    xmm4,[rel 7FFD`F74E`3FE0h]
00007FFDF74E3C3B vxorps    xmm1,xmm1,xmm4
00007FFDF74E3C3F vmulss    xmm1,xmm1,[rel 7FFD`F74E`3FE4h]
00007FFDF74E3C47 vbroadcastss xmm1,xmm1
00007FFDF74E3C4C vmulss    xmm2,xmm2,[rel 7FFD`F74E`3FE8h]
00007FFDF74E3C54 vbroadcastss xmm2,xmm2
00007FFDF74E3C59 vmulps    xmm6,xmm1,xmm0
00007FFDF74E3C5D vmulps    xmm7,xmm3,xmm1
00007FFDF74E3C61 vmulps    xmm0,xmm0,xmm2
00007FFDF74E3C65 vsubps    xmm7,xmm7,xmm0
00007FFDF74E3C69 vmovaps   xmm8,xmm6
00007FFDF74E3C6D vmovaps   xmm9,xmm7
00007FFDF74E3C71 vxorps    xmm0,xmm0,xmm0
00007FFDF74E3C75 vmovapd   [rsp+0E0h],xmm0
00007FFDF74E3C7E vxorps    xmm0,xmm0,xmm0
00007FFDF74E3C82 vmovapd   [rsp+0D0h],xmm0
00007FFDF74E3C8B vxorps    xmm0,xmm0,xmm0
00007FFDF74E3C8F vmovapd   [rsp+0C0h],xmm0
00007FFDF74E3C98 vxorps    xmm0,xmm0,xmm0
00007FFDF74E3C9C vmovapd   [rsp+0A0h],xmm0
00007FFDF74E3CA5 vxorps    xmm0,xmm0,xmm0
00007FFDF74E3CA9 vmovapd   [rsp+80h],xmm0
00007FFDF74E3CB2 lea       rcx,[rsp+0E0h]
00007FFDF74E3CBA vmovapd   [rsp+40h],xmm8
00007FFDF74E3CC0 vmovapd   [rsp+30h],xmm8
00007FFDF74E3CC6 lea       rdx,[rsp+40h]
00007FFDF74E3CCB lea       r8,[rsp+30h]
00007FFDF74E3CD0 call      0000`7FFD`F726`3C50h
00007FFDF74E3CD5 lea       rcx,[rsp+0D0h]
00007FFDF74E3CDD vmovapd   [rsp+40h],xmm8
00007FFDF74E3CE3 vmovapd   [rsp+30h],xmm9
00007FFDF74E3CE9 lea       rdx,[rsp+40h]
00007FFDF74E3CEE lea       r8,[rsp+30h]
00007FFDF74E3CF3 call      0000`7FFD`F726`3C50h
00007FFDF74E3CF8 lea       rcx,[rsp+0C0h]
00007FFDF74E3D00 vmovapd   xmm0,[rsp+0E0h]
00007FFDF74E3D09 vmovapd   [rsp+40h],xmm0
00007FFDF74E3D0F lea       rdx,[rsp+40h]
00007FFDF74E3D14 call      0000`7FFD`F726`3C38h
00007FFDF74E3D19 vmovapd   xmm1,[rsp+0E0h]
00007FFDF74E3D22 vmovapd   xmm0,[rsp+0C0h]
00007FFDF74E3D2B vmulps    xmm8,xmm1,xmm0
00007FFDF74E3D2F vmovapd   xmm1,[rsp+0D0h]
00007FFDF74E3D38 vmovapd   xmm0,[rsp+0C0h]
00007FFDF74E3D41 vmulps    xmm9,xmm1,xmm0
00007FFDF74E3D45 vmovapd   xmm1,[rsp+0C0h]
00007FFDF74E3D4E vmulps    xmm10,xmm6,xmm1
00007FFDF74E3D52 vmovapd   xmm1,[rsp+0C0h]
00007FFDF74E3D5B vmulps    xmm7,xmm7,xmm1
00007FFDF74E3D5F vmovapd   xmm1,[rsp+0E0h]
00007FFDF74E3D68 vrcpps    xmm11,xmm1
00007FFDF74E3D6C vmulps    xmm12,xmm1,xmm11
00007FFDF74E3D71 lea       rcx,[rsp+60h]
00007FFDF74E3D76 vmovss    xmm1,[rel 7FFD`F74E`3FECh]
00007FFDF74E3D7E call      0000`7FFD`F725`AD20h
00007FFDF74E3D83 vmovapd   xmm0,[rsp+60h]
00007FFDF74E3D89 vsubps    xmm0,xmm0,xmm12
00007FFDF74E3D8E vmulps    xmm0,xmm11,xmm0
00007FFDF74E3D92 vmovapd   xmm1,[rsp+0C0h]
00007FFDF74E3D9B vmulps    xmm0,xmm1,xmm0
00007FFDF74E3D9F vmovapd   xmm1,[rsp+0D0h]
00007FFDF74E3DA8 vmulps    xmm0,xmm1,xmm0
00007FFDF74E3DAC vmulps    xmm0,xmm6,xmm0
00007FFDF74E3DB0 vsubps    xmm7,xmm7,xmm0
00007FFDF74E3DB4 vxorps    xmm0,xmm0,xmm0
00007FFDF74E3DB8 vmovss    [rsp+5Ch],xmm0
00007FFDF74E3DBE vmovss    [rsp+5Ch],xmm8
00007FFDF74E3DC4 vmovss    xmm6,[rsp+5Ch]
00007FFDF74E3DCA vmovss    [rsp+58h],xmm0
00007FFDF74E3DD0 vmovss    [rsp+58h],xmm9
00007FFDF74E3DD6 vmovss    xmm1,[rsp+58h]
00007FFDF74E3DDC vmovss    [rsp+0BCh],xmm1
00007FFDF74E3DE5 vmovaps   xmm0,xmm6
00007FFDF74E3DE9 call      0000`7FFE`56FC`8BF0h
00007FFDF74E3DEE vmovss    [rsp+0B8h],xmm0
00007FFDF74E3DF7 vmovaps   xmm0,xmm6
00007FFDF74E3DFB call      0000`7FFE`56FC`8B00h
00007FFDF74E3E00 vmovaps   xmm6,xmm0
00007FFDF74E3E04 lea       rcx,[rsp+0A0h]
00007FFDF74E3E0C vmovss    xmm1,[rsp+0B8h]
00007FFDF74E3E15 call      0000`7FFD`F725`AD20h
00007FFDF74E3E1A lea       rdi,[rsp+0F0h]
00007FFDF74E3E22 vxorps    xmm2,xmm2,xmm2
00007FFDF74E3E26 vmovss    [rsp+20h],xmm2
00007FFDF74E3E2C lea       rcx,[rsp+90h]
00007FFDF74E3E34 vxorps    xmm3,xmm3,xmm3
00007FFDF74E3E38 vmovaps   xmm1,xmm6
00007FFDF74E3E3C call      0000`7FFD`F725`AD70h
00007FFDF74E3E41 vmovapd   xmm1,[rsp+0A0h]
00007FFDF74E3E4A vmulps    xmm1,xmm1,xmm10
00007FFDF74E3E4F vmovapd   xmm2,[rsp+90h]
00007FFDF74E3E58 vaddps    xmm1,xmm2,xmm1
00007FFDF74E3E5C vmovupd   [rdi],xmm1
00007FFDF74E3E60 vmovss    [rsp+20h],xmm6
00007FFDF74E3E66 lea       rcx,[rsp+80h]
00007FFDF74E3E6E vxorps    xmm1,xmm1,xmm1
00007FFDF74E3E72 vmovaps   xmm2,xmm6
00007FFDF74E3E76 vmovaps   xmm3,xmm6
00007FFDF74E3E7A call      0000`7FFD`F725`AD70h
00007FFDF74E3E7F vmovapd   xmm2,[rsp+80h]
00007FFDF74E3E88 vmulps    xmm2,xmm9,xmm2
00007FFDF74E3E8C lea       rcx,[rsp+100h]
00007FFDF74E3E94 vmovapd   xmm3,[rsp+0A0h]
00007FFDF74E3E9D vmulps    xmm3,xmm3,xmm7
00007FFDF74E3EA1 vmovupd   [rcx],xmm3
00007FFDF74E3EA5 lea       rcx,[rsp+100h]
00007FFDF74E3EAD vmovapd   xmm3,[rsp+100h]
00007FFDF74E3EB6 vmulps    xmm2,xmm2,xmm10
00007FFDF74E3EBB vaddps    xmm2,xmm3,xmm2
00007FFDF74E3EBF vmovupd   [rcx],xmm2
00007FFDF74E3EC3 vmovss    xmm1,[rsp+0B8h]
00007FFDF74E3ECC vmulss    xmm1,xmm1,[rsp+0BCh]
00007FFDF74E3ED5 lea       rdi,[rsp+100h]
00007FFDF74E3EDD vxorps    xmm2,xmm2,xmm2
00007FFDF74E3EE1 vmovss    [rsp+20h],xmm2
00007FFDF74E3EE7 lea       rcx,[rsp+70h]
00007FFDF74E3EEC vxorps    xmm3,xmm3,xmm3
00007FFDF74E3EF0 call      0000`7FFD`F725`AD70h
00007FFDF74E3EF5 vmovapd   xmm0,[rsp+100h]
00007FFDF74E3EFE vaddps    xmm0,xmm0,[rsp+70h]
00007FFDF74E3F04 vmovupd   [rdi],xmm0
00007FFDF74E3F08 vmovapd   xmm0,[rsp+0F0h]
00007FFDF74E3F11 vmovupd   [rsi],xmm0
00007FFDF74E3F15 vmovapd   xmm0,[rsp+100h]
00007FFDF74E3F1E vmovupd   [rsi+10h],xmm0
00007FFDF74E3F23 mov       rax,rsi
00007FFDF74E3F26 vmovaps   xmm6,[rsp+170h]
00007FFDF74E3F2F vmovaps   xmm7,[rsp+160h]
00007FFDF74E3F38 vmovaps   xmm8,[rsp+150h]
00007FFDF74E3F41 vmovaps   xmm9,[rsp+140h]
00007FFDF74E3F4A vmovaps   xmm10,[rsp+130h]
00007FFDF74E3F53 vmovaps   xmm11,[rsp+120h]
00007FFDF74E3F5C vmovaps   xmm12,[rsp+110h]
00007FFDF74E3F65 add       rsp,188h
00007FFDF74E3F6C pop       rsi
00007FFDF74E3F6D pop       rdi
00007FFDF74E3F6E ret

# -----------------------------------------------------------------------------------

# [struct Motor] Void Store(Single*)
00007FFDF74E4030 vzeroupper
00007FFDF74E4033 xchg      ax,ax
00007FFDF74E4035 vmovupd   xmm0,[rcx]
00007FFDF74E4039 vmovups   [rdx],xmm0
00007FFDF74E403D vmovupd   xmm0,[rcx+10h]
00007FFDF74E4042 vmovups   [rdx+10h],xmm0
00007FFDF74E4047 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Void Store(System.Span`1[System.Single])
00007FFDF74E4060 push      rsi
00007FFDF74E4061 sub       rsp,30h
00007FFDF74E4065 vzeroupper
00007FFDF74E4068 xor       eax,eax
00007FFDF74E406A mov       [rsp+28h],rax
00007FFDF74E406F mov       rax,[rdx]
00007FFDF74E4072 mov       edx,[rdx+8]
00007FFDF74E4075 vmovupd   xmm0,[rcx]
00007FFDF74E4079 vmovupd   xmm1,[rcx+10h]
00007FFDF74E407E xor       ecx,ecx
00007FFDF74E4080 mov       [rsp+28h],rcx
00007FFDF74E4085 cmp       edx,8
00007FFDF74E4088 jl        short 0000`7FFD`F74E`40AEh
00007FFDF74E408A xor       ecx,ecx
00007FFDF74E408C test      edx,edx
00007FFDF74E408E je        short 0000`7FFD`F74E`4093h
00007FFDF74E4090 mov       rcx,rax
00007FFDF74E4093 mov       [rsp+28h],rcx
00007FFDF74E4098 vmovups   [rcx],xmm0
00007FFDF74E409C vmovups   [rcx+10h],xmm1
00007FFDF74E40A1 xor       ecx,ecx
00007FFDF74E40A3 mov       [rsp+28h],rcx
00007FFDF74E40A8 add       rsp,30h
00007FFDF74E40AC pop       rsi
00007FFDF74E40AD ret
00007FFDF74E40AE mov       rcx,7FFD`F72F`D4C0h
00007FFDF74E40B8 call      0000`7FFE`56D7`7710h
00007FFDF74E40BD mov       rsi,rax
00007FFDF74E40C0 mov       ecx,25h
00007FFDF74E40C5 mov       rdx,7FFD`F731`9EA0h
00007FFDF74E40CF call      0000`7FFE`56EA`03E0h
00007FFDF74E40D4 mov       rdx,rax
00007FFDF74E40D7 mov       rcx,rsi
00007FFDF74E40DA call      0000`7FFD`F725`D238h
00007FFDF74E40DF mov       rcx,rsi
00007FFDF74E40E2 call      0000`7FFE`56D3`B3A0h
00007FFDF74E40E7 int3

# -----------------------------------------------------------------------------------

# [struct Motor] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef, Single ByRef, Single ByRef, Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E4100 vzeroupper
00007FFDF74E4103 xchg      ax,ax
00007FFDF74E4105 vmovss    xmm0,[rcx]
00007FFDF74E4109 vmovss    [rdx],xmm0
00007FFDF74E410D vmovupd   xmm0,[rcx]
00007FFDF74E4111 vextractps eax,xmm0,1
00007FFDF74E4117 vmovd     xmm0,eax
00007FFDF74E411B vmovss    [r8],xmm0
00007FFDF74E4120 vmovupd   xmm0,[rcx]
00007FFDF74E4124 vextractps eax,xmm0,2
00007FFDF74E412A vmovd     xmm0,eax
00007FFDF74E412E vmovss    [r9],xmm0
00007FFDF74E4133 vmovupd   xmm0,[rcx]
00007FFDF74E4137 vextractps eax,xmm0,3
00007FFDF74E413D vmovd     xmm0,eax
00007FFDF74E4141 mov       rax,[rsp+28h]
00007FFDF74E4146 vmovss    [rax],xmm0
00007FFDF74E414A vmovupd   xmm0,[rcx+10h]
00007FFDF74E414F vextractps eax,xmm0,1
00007FFDF74E4155 vmovd     xmm0,eax
00007FFDF74E4159 mov       rax,[rsp+30h]
00007FFDF74E415E vmovss    [rax],xmm0
00007FFDF74E4162 vmovupd   xmm0,[rcx+10h]
00007FFDF74E4167 vextractps eax,xmm0,2
00007FFDF74E416D vmovd     xmm0,eax
00007FFDF74E4171 mov       rax,[rsp+38h]
00007FFDF74E4176 vmovss    [rax],xmm0
00007FFDF74E417A vmovupd   xmm0,[rcx+10h]
00007FFDF74E417F vextractps eax,xmm0,3
00007FFDF74E4185 vmovd     xmm0,eax
00007FFDF74E4189 mov       rax,[rsp+40h]
00007FFDF74E418E vmovss    [rax],xmm0
00007FFDF74E4192 vmovss    xmm0,[rcx+10h]
00007FFDF74E4197 mov       rax,[rsp+48h]
00007FFDF74E419C vmovss    [rax],xmm0
00007FFDF74E41A0 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor Normalized()
00007FFDF74E41D0 sub       rsp,18h
00007FFDF74E41D4 vzeroupper
00007FFDF74E41D7 vmovaps   [rsp],xmm6
00007FFDF74E41DC vmovupd   xmm0,[rcx]
00007FFDF74E41E0 vmovupd   xmm1,[rcx+10h]
00007FFDF74E41E5 vmovaps   xmm2,xmm0
00007FFDF74E41E9 vmovaps   xmm3,xmm0
00007FFDF74E41ED vdpps     xmm2,xmm2,xmm3,0FFh
00007FFDF74E41F3 vmovaps   xmm3,xmm2
00007FFDF74E41F7 vrsqrtps  xmm4,xmm2
00007FFDF74E41FB vmulps    xmm5,xmm4,xmm4
00007FFDF74E41FF vmulps    xmm5,xmm2,xmm5
00007FFDF74E4203 vmovss    xmm2,[rel 7FFD`F74E`42D0h]
00007FFDF74E420B vbroadcastss xmm2,xmm2
00007FFDF74E4210 vsubps    xmm2,xmm2,xmm5
00007FFDF74E4214 vmovss    xmm5,[rel 7FFD`F74E`42D4h]
00007FFDF74E421C vbroadcastss xmm5,xmm5
00007FFDF74E4221 vmulps    xmm4,xmm5,xmm4
00007FFDF74E4225 vmulps    xmm2,xmm4,xmm2
00007FFDF74E4229 vxorps    xmm4,xmm4,xmm4
00007FFDF74E422D vmovss    xmm5,[rel 7FFD`F74E`42D8h]
00007FFDF74E4235 vmovss    xmm4,xmm4,xmm5
00007FFDF74E4239 vxorps    xmm4,xmm0,xmm4
00007FFDF74E423D vmovaps   xmm5,xmm1
00007FFDF74E4241 vdpps     xmm4,xmm4,xmm5,0FFh
00007FFDF74E4247 vrcpps    xmm5,xmm3
00007FFDF74E424B vmulps    xmm3,xmm3,xmm5
00007FFDF74E424F vmovss    xmm6,[rel 7FFD`F74E`42DCh]
00007FFDF74E4257 vbroadcastss xmm6,xmm6
00007FFDF74E425C vsubps    xmm3,xmm6,xmm3
00007FFDF74E4260 vmulps    xmm3,xmm5,xmm3
00007FFDF74E4264 vmulps    xmm3,xmm4,xmm3
00007FFDF74E4268 vmulps    xmm3,xmm3,xmm2
00007FFDF74E426C vmulps    xmm1,xmm1,xmm2
00007FFDF74E4270 vmulps    xmm3,xmm0,xmm3
00007FFDF74E4274 vxorps    xmm4,xmm4,xmm4
00007FFDF74E4278 vmovss    xmm5,[rel 7FFD`F74E`42E0h]
00007FFDF74E4280 vmovss    xmm4,xmm4,xmm5
00007FFDF74E4284 vxorps    xmm3,xmm3,xmm4
00007FFDF74E4288 vsubps    xmm1,xmm1,xmm3
00007FFDF74E428C vmulps    xmm0,xmm0,xmm2
00007FFDF74E4290 vmovupd   [rdx],xmm0
00007FFDF74E4294 vmovupd   [rdx+10h],xmm1
00007FFDF74E4299 mov       rax,rdx
00007FFDF74E429C vmovaps   xmm6,[rsp]
00007FFDF74E42A1 add       rsp,18h
00007FFDF74E42A5 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor Inverse()
00007FFDF74E4300 sub       rsp,18h
00007FFDF74E4304 vzeroupper
00007FFDF74E4307 vmovaps   [rsp],xmm6
00007FFDF74E430C vmovupd   xmm0,[rcx]
00007FFDF74E4310 vmovupd   xmm1,[rcx+10h]
00007FFDF74E4315 vmovaps   xmm2,xmm0
00007FFDF74E4319 vmovaps   xmm3,xmm0
00007FFDF74E431D vdpps     xmm2,xmm2,xmm3,0FFh
00007FFDF74E4323 vmovaps   xmm3,xmm2
00007FFDF74E4327 vrsqrtps  xmm4,xmm2
00007FFDF74E432B vmulps    xmm5,xmm4,xmm4
00007FFDF74E432F vmulps    xmm5,xmm2,xmm5
00007FFDF74E4333 vmovss    xmm2,[rel 7FFD`F74E`4448h]
00007FFDF74E433B vbroadcastss xmm2,xmm2
00007FFDF74E4340 vsubps    xmm2,xmm2,xmm5
00007FFDF74E4344 vmovss    xmm5,[rel 7FFD`F74E`444Ch]
00007FFDF74E434C vbroadcastss xmm5,xmm5
00007FFDF74E4351 vmulps    xmm4,xmm5,xmm4
00007FFDF74E4355 vmulps    xmm2,xmm4,xmm2
00007FFDF74E4359 vxorps    xmm4,xmm4,xmm4
00007FFDF74E435D vmovss    xmm5,[rel 7FFD`F74E`4450h]
00007FFDF74E4365 vmovss    xmm4,xmm4,xmm5
00007FFDF74E4369 vxorps    xmm4,xmm0,xmm4
00007FFDF74E436D vmovaps   xmm5,xmm1
00007FFDF74E4371 vdpps     xmm4,xmm4,xmm5,0FFh
00007FFDF74E4377 vrcpps    xmm5,xmm3
00007FFDF74E437B vmulps    xmm3,xmm3,xmm5
00007FFDF74E437F vmovss    xmm6,[rel 7FFD`F74E`4454h]
00007FFDF74E4387 vbroadcastss xmm6,xmm6
00007FFDF74E438C vsubps    xmm3,xmm6,xmm3
00007FFDF74E4390 vmulps    xmm3,xmm5,xmm3
00007FFDF74E4394 vmulps    xmm4,xmm4,xmm3
00007FFDF74E4398 vmulps    xmm4,xmm4,xmm2
00007FFDF74E439C vxorps    xmm5,xmm5,xmm5
00007FFDF74E43A0 vmovss    xmm6,[rel 7FFD`F74E`4458h]
00007FFDF74E43A8 vinsertps xmm5,xmm5,xmm6,10h
00007FFDF74E43AE vmovss    xmm6,[rel 7FFD`F74E`445Ch]
00007FFDF74E43B6 vinsertps xmm5,xmm5,xmm6,20h
00007FFDF74E43BC vmovss    xmm6,[rel 7FFD`F74E`4460h]
00007FFDF74E43C4 vinsertps xmm5,xmm5,xmm6,30h
00007FFDF74E43CA vmulps    xmm2,xmm2,xmm4
00007FFDF74E43CE vmulps    xmm2,xmm0,xmm2
00007FFDF74E43D2 vmulps    xmm1,xmm1,xmm3
00007FFDF74E43D6 vaddps    xmm2,xmm2,xmm2
00007FFDF74E43DA vxorps    xmm4,xmm4,xmm4
00007FFDF74E43DE vmovss    xmm6,[rel 7FFD`F74E`4464h]
00007FFDF74E43E6 vmovss    xmm4,xmm4,xmm6
00007FFDF74E43EA vxorps    xmm2,xmm2,xmm4
00007FFDF74E43EE vsubps    xmm1,xmm1,xmm2
00007FFDF74E43F2 vxorps    xmm1,xmm1,xmm5
00007FFDF74E43F6 vmulps    xmm0,xmm0,xmm3
00007FFDF74E43FA vxorps    xmm0,xmm0,xmm5
00007FFDF74E43FE vmovupd   [rdx],xmm0
00007FFDF74E4402 vmovupd   [rdx+10h],xmm1
00007FFDF74E4407 mov       rax,rdx
00007FFDF74E440A vmovaps   xmm6,[rsp]
00007FFDF74E440F add       rsp,18h
00007FFDF74E4413 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor Constrained()
00007FFDF74E4480 vzeroupper
00007FFDF74E4483 xchg      ax,ax
00007FFDF74E4485 vmovupd   xmm0,[rcx]
00007FFDF74E4489 vmovupd   xmm1,[rcx+10h]
00007FFDF74E448E vxorps    xmm2,xmm2,xmm2
00007FFDF74E4492 vmovss    xmm3,[rel 7FFD`F74E`44C8h]
00007FFDF74E449A vmovss    xmm2,xmm2,xmm3
00007FFDF74E449E vandps    xmm2,xmm0,xmm2
00007FFDF74E44A2 vshufps   xmm2,xmm2,xmm2,0
00007FFDF74E44A7 vxorps    xmm0,xmm2,xmm0
00007FFDF74E44AB vxorps    xmm1,xmm2,xmm1
00007FFDF74E44AF vmovupd   [rdx],xmm0
00007FFDF74E44B3 vmovupd   [rdx+10h],xmm1
00007FFDF74E44B8 mov       rax,rdx
00007FFDF74E44BB ret

# -----------------------------------------------------------------------------------

# [struct Motor] Boolean Equals(Motor)
00007FFDF74E44E0 vzeroupper
00007FFDF74E44E3 xchg      ax,ax
00007FFDF74E44E5 vmovupd   xmm0,[rcx]
00007FFDF74E44E9 vmovupd   xmm1,[rdx]
00007FFDF74E44ED vcmpeqps  xmm0,xmm0,xmm1
00007FFDF74E44F2 vmovupd   xmm1,[rcx+10h]
00007FFDF74E44F7 vmovupd   xmm2,[rdx+10h]
00007FFDF74E44FC vcmpeqps  xmm1,xmm1,xmm2
00007FFDF74E4501 vandps    xmm0,xmm0,xmm1
00007FFDF74E4505 vmovmskps eax,xmm0
00007FFDF74E4509 cmp       eax,0Fh
00007FFDF74E450C sete      al
00007FFDF74E450F movzx     eax,al
00007FFDF74E4512 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Boolean Equals(Motor, Single)
00007FFDF74E4530 vzeroupper
00007FFDF74E4533 xchg      ax,ax
00007FFDF74E4535 vbroadcastss xmm0,xmm2
00007FFDF74E453A vmovss    xmm1,[rel 7FFD`F74E`4598h]
00007FFDF74E4542 vbroadcastss xmm1,xmm1
00007FFDF74E4547 vmovupd   xmm2,[rcx]
00007FFDF74E454B vmovupd   xmm3,[rdx]
00007FFDF74E454F vsubps    xmm2,xmm2,xmm3
00007FFDF74E4553 vandnps   xmm2,xmm1,xmm2
00007FFDF74E4557 vcmpltps  xmm2,xmm2,xmm0
00007FFDF74E455C vmovupd   xmm3,[rcx+10h]
00007FFDF74E4561 vmovupd   xmm4,[rdx+10h]
00007FFDF74E4566 vsubps    xmm3,xmm3,xmm4
00007FFDF74E456A vandnps   xmm1,xmm1,xmm3
00007FFDF74E456E vcmpltps  xmm0,xmm1,xmm0
00007FFDF74E4573 vandps    xmm0,xmm2,xmm0
00007FFDF74E4577 vmovmskps eax,xmm0
00007FFDF74E457B cmp       eax,0Fh
00007FFDF74E457E sete      al
00007FFDF74E4581 movzx     eax,al
00007FFDF74E4584 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Implicit(Rotor)
00007FFDF74E45B0 vzeroupper
00007FFDF74E45B3 xchg      ax,ax
00007FFDF74E45B5 vmovupd   xmm0,[rdx]
00007FFDF74E45B9 vxorps    xmm1,xmm1,xmm1
00007FFDF74E45BD vmovupd   [rcx],xmm0
00007FFDF74E45C1 vmovupd   [rcx+10h],xmm1
00007FFDF74E45C6 mov       rax,rcx
00007FFDF74E45C9 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Implicit(Translator)
00007FFDF74E45E0 vzeroupper
00007FFDF74E45E3 xchg      ax,ax
00007FFDF74E45E5 vxorps    xmm0,xmm0,xmm0
00007FFDF74E45E9 vmovss    xmm1,[rel 7FFD`F74E`4610h]
00007FFDF74E45F1 vmovss    xmm0,xmm0,xmm1
00007FFDF74E45F5 vmovupd   xmm1,[rdx]
00007FFDF74E45F9 vmovupd   [rcx],xmm0
00007FFDF74E45FD vmovupd   [rcx+10h],xmm1
00007FFDF74E4602 mov       rax,rcx
00007FFDF74E4605 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Addition(Motor, Motor)
00007FFDF74E4630 vzeroupper
00007FFDF74E4633 xchg      ax,ax
00007FFDF74E4635 vmovupd   xmm0,[rdx]
00007FFDF74E4639 vmovupd   xmm1,[r8]
00007FFDF74E463E vaddps    xmm0,xmm0,xmm1
00007FFDF74E4642 vmovupd   xmm1,[rdx+10h]
00007FFDF74E4647 vmovupd   xmm2,[r8+10h]
00007FFDF74E464D vaddps    xmm1,xmm1,xmm2
00007FFDF74E4651 vmovupd   [rcx],xmm0
00007FFDF74E4655 vmovupd   [rcx+10h],xmm1
00007FFDF74E465A mov       rax,rcx
00007FFDF74E465D ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Subtraction(Motor, Motor)
00007FFDF74E4680 vzeroupper
00007FFDF74E4683 xchg      ax,ax
00007FFDF74E4685 vmovupd   xmm0,[rdx]
00007FFDF74E4689 vmovupd   xmm1,[r8]
00007FFDF74E468E vsubps    xmm0,xmm0,xmm1
00007FFDF74E4692 vmovupd   xmm1,[rdx+10h]
00007FFDF74E4697 vmovupd   xmm2,[r8+10h]
00007FFDF74E469D vsubps    xmm1,xmm1,xmm2
00007FFDF74E46A1 vmovupd   [rcx],xmm0
00007FFDF74E46A5 vmovupd   [rcx+10h],xmm1
00007FFDF74E46AA mov       rax,rcx
00007FFDF74E46AD ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Multiply(Motor, Single)
00007FFDF74E46D0 vzeroupper
00007FFDF74E46D3 xchg      ax,ax
00007FFDF74E46D5 vbroadcastss xmm0,xmm2
00007FFDF74E46DA vmovupd   xmm1,[rdx]
00007FFDF74E46DE vmulps    xmm1,xmm1,xmm0
00007FFDF74E46E2 vmovupd   xmm2,[rdx+10h]
00007FFDF74E46E7 vmulps    xmm0,xmm2,xmm0
00007FFDF74E46EB vmovupd   [rcx],xmm1
00007FFDF74E46EF vmovupd   [rcx+10h],xmm0
00007FFDF74E46F4 mov       rax,rcx
00007FFDF74E46F7 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Multiply(Single, Motor)
00007FFDF74E4710 vzeroupper
00007FFDF74E4713 xchg      ax,ax
00007FFDF74E4715 vmovupd   xmm0,[r8]
00007FFDF74E471A vmovupd   xmm2,[r8+10h]
00007FFDF74E4720 vbroadcastss xmm1,xmm1
00007FFDF74E4725 vmulps    xmm0,xmm0,xmm1
00007FFDF74E4729 vmulps    xmm1,xmm2,xmm1
00007FFDF74E472D vmovupd   [rcx],xmm0
00007FFDF74E4731 vmovupd   [rcx+10h],xmm1
00007FFDF74E4736 mov       rax,rcx
00007FFDF74E4739 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Division(Motor, Single)
00007FFDF74E4750 vzeroupper
00007FFDF74E4753 xchg      ax,ax
00007FFDF74E4755 vbroadcastss xmm0,xmm2
00007FFDF74E475A vrcpps    xmm1,xmm0
00007FFDF74E475E vmulps    xmm0,xmm0,xmm1
00007FFDF74E4762 vmovss    xmm2,[rel 7FFD`F74E`47A8h]
00007FFDF74E476A vbroadcastss xmm2,xmm2
00007FFDF74E476F vsubps    xmm0,xmm2,xmm0
00007FFDF74E4773 vmulps    xmm0,xmm1,xmm0
00007FFDF74E4777 vmovupd   xmm1,[rdx]
00007FFDF74E477B vmulps    xmm1,xmm1,xmm0
00007FFDF74E477F vmovupd   xmm2,[rdx+10h]
00007FFDF74E4784 vmulps    xmm0,xmm2,xmm0
00007FFDF74E4788 vmovupd   [rcx],xmm1
00007FFDF74E478C vmovupd   [rcx+10h],xmm0
00007FFDF74E4791 mov       rax,rcx
00007FFDF74E4794 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_UnaryNegation(Motor)
00007FFDF74E47C0 vzeroupper
00007FFDF74E47C3 xchg      ax,ax
00007FFDF74E47C5 vmovss    xmm0,[rel 7FFD`F74E`47F8h]
00007FFDF74E47CD vbroadcastss xmm0,xmm0
00007FFDF74E47D2 vmovupd   xmm1,[rdx]
00007FFDF74E47D6 vxorps    xmm1,xmm1,xmm0
00007FFDF74E47DA vmovupd   xmm2,[rdx+10h]
00007FFDF74E47DF vxorps    xmm0,xmm2,xmm0
00007FFDF74E47E3 vmovupd   [rcx],xmm1
00007FFDF74E47E7 vmovupd   [rcx+10h],xmm0
00007FFDF74E47EC mov       rax,rcx
00007FFDF74E47EF ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_OnesComplement(Motor)
00007FFDF74E4810 vzeroupper
00007FFDF74E4813 xchg      ax,ax
00007FFDF74E4815 vxorps    xmm0,xmm0,xmm0
00007FFDF74E4819 vmovss    xmm1,[rel 7FFD`F74E`4870h]
00007FFDF74E4821 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74E4827 vmovss    xmm1,[rel 7FFD`F74E`4874h]
00007FFDF74E482F vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74E4835 vmovss    xmm1,[rel 7FFD`F74E`4878h]
00007FFDF74E483D vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74E4843 vmovupd   xmm1,[rdx]
00007FFDF74E4847 vxorps    xmm1,xmm1,xmm0
00007FFDF74E484B vmovupd   xmm2,[rdx+10h]
00007FFDF74E4850 vxorps    xmm0,xmm2,xmm0
00007FFDF74E4854 vmovupd   [rcx],xmm1
00007FFDF74E4858 vmovupd   [rcx+10h],xmm0
00007FFDF74E485D mov       rax,rcx
00007FFDF74E4860 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Multiply(Motor, Rotor)
00007FFDF74E4890 sub       rsp,28h
00007FFDF74E4894 vzeroupper
00007FFDF74E4897 vmovaps   [rsp+10h],xmm6
00007FFDF74E489D vmovaps   [rsp],xmm7
00007FFDF74E48A2 vmovupd   xmm0,[r8]
00007FFDF74E48A7 vmovupd   xmm1,[rdx]
00007FFDF74E48AB vshufps   xmm2,xmm1,xmm1,0
00007FFDF74E48B0 vmulps    xmm2,xmm2,xmm0
00007FFDF74E48B4 vshufps   xmm3,xmm1,xmm1,79h
00007FFDF74E48B9 vshufps   xmm4,xmm0,xmm0,9Dh
00007FFDF74E48BE vmulps    xmm3,xmm3,xmm4
00007FFDF74E48C2 vsubps    xmm2,xmm2,xmm3
00007FFDF74E48C6 vshufps   xmm3,xmm1,xmm1,0E6h
00007FFDF74E48CB vshufps   xmm4,xmm0,xmm0,2
00007FFDF74E48D0 vmulps    xmm3,xmm3,xmm4
00007FFDF74E48D4 vshufps   xmm1,xmm1,xmm1,9Fh
00007FFDF74E48D9 vshufps   xmm4,xmm0,xmm0,7Bh
00007FFDF74E48DE vmulps    xmm1,xmm1,xmm4
00007FFDF74E48E2 vaddps    xmm1,xmm3,xmm1
00007FFDF74E48E6 vxorps    xmm3,xmm3,xmm3
00007FFDF74E48EA vmovss    xmm4,[rel 7FFD`F74E`49C0h]
00007FFDF74E48F2 vmovss    xmm3,xmm3,xmm4
00007FFDF74E48F6 vxorps    xmm1,xmm1,xmm3
00007FFDF74E48FA vaddps    xmm1,xmm2,xmm1
00007FFDF74E48FE vmovupd   xmm2,[rdx+10h]
00007FFDF74E4903 vmovaps   xmm3,xmm0
00007FFDF74E4907 vmovaps   xmm4,xmm2
00007FFDF74E490B vshufps   xmm5,xmm3,xmm3,1
00007FFDF74E4910 vshufps   xmm6,xmm4,xmm4,0E5h
00007FFDF74E4915 vmulps    xmm5,xmm5,xmm6
00007FFDF74E4919 vshufps   xmm6,xmm3,xmm3,7Ah
00007FFDF74E491E vshufps   xmm7,xmm4,xmm4,9Eh
00007FFDF74E4923 vmulps    xmm6,xmm6,xmm7
00007FFDF74E4927 vaddps    xmm5,xmm5,xmm6
00007FFDF74E492B vxorps    xmm6,xmm6,xmm6
00007FFDF74E492F vmovss    xmm7,[rel 7FFD`F74E`49C4h]
00007FFDF74E4937 vmovss    xmm6,xmm6,xmm7
00007FFDF74E493B vshufps   xmm3,xmm3,xmm3,9Fh
00007FFDF74E4940 vshufps   xmm4,xmm4,xmm4,7Bh
00007FFDF74E4945 vmulps    xmm3,xmm3,xmm4
00007FFDF74E4949 vxorps    xmm3,xmm6,xmm3
00007FFDF74E494D vsubps    xmm5,xmm5,xmm3
00007FFDF74E4951 vxorps    xmm3,xmm3,xmm3
00007FFDF74E4955 vmovss    xmm4,[rel 7FFD`F74E`49C8h]
00007FFDF74E495D vmovss    xmm3,xmm3,xmm4
00007FFDF74E4961 vshufps   xmm2,xmm2,xmm2,0
00007FFDF74E4966 vmulps    xmm0,xmm0,xmm2
00007FFDF74E496A vxorps    xmm0,xmm3,xmm0
00007FFDF74E496E vsubps    xmm0,xmm5,xmm0
00007FFDF74E4972 vmovupd   [rcx],xmm1
00007FFDF74E4976 vmovupd   [rcx+10h],xmm0
00007FFDF74E497B mov       rax,rcx
00007FFDF74E497E vmovaps   xmm6,[rsp+10h]
00007FFDF74E4984 vmovaps   xmm7,[rsp]
00007FFDF74E4989 add       rsp,28h
00007FFDF74E498D ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Multiply(Motor, Translator)
00007FFDF74E49F0 sub       rsp,18h
00007FFDF74E49F4 vzeroupper
00007FFDF74E49F7 vmovaps   [rsp],xmm6
00007FFDF74E49FC vmovupd   xmm0,[rdx]
00007FFDF74E4A00 vmovupd   xmm1,[rdx+10h]
00007FFDF74E4A05 vmovaps   xmm2,xmm0
00007FFDF74E4A09 vmovupd   xmm3,[r8]
00007FFDF74E4A0E vshufps   xmm4,xmm2,xmm2,1
00007FFDF74E4A13 vshufps   xmm5,xmm3,xmm3,0E5h
00007FFDF74E4A18 vmulps    xmm4,xmm4,xmm5
00007FFDF74E4A1C vshufps   xmm5,xmm2,xmm2,9Eh
00007FFDF74E4A21 vshufps   xmm6,xmm3,xmm3,7Ah
00007FFDF74E4A26 vmulps    xmm5,xmm5,xmm6
00007FFDF74E4A2A vaddps    xmm4,xmm4,xmm5
00007FFDF74E4A2E vxorps    xmm5,xmm5,xmm5
00007FFDF74E4A32 vmovss    xmm6,[rel 7FFD`F74E`4A88h]
00007FFDF74E4A3A vmovss    xmm5,xmm5,xmm6
00007FFDF74E4A3E vshufps   xmm2,xmm2,xmm2,7Bh
00007FFDF74E4A43 vshufps   xmm3,xmm3,xmm3,9Fh
00007FFDF74E4A48 vmulps    xmm2,xmm2,xmm3
00007FFDF74E4A4C vxorps    xmm2,xmm5,xmm2
00007FFDF74E4A50 vsubps    xmm4,xmm4,xmm2
00007FFDF74E4A54 vaddps    xmm1,xmm4,xmm1
00007FFDF74E4A58 vmovupd   [rcx],xmm0
00007FFDF74E4A5C vmovupd   [rcx+10h],xmm1
00007FFDF74E4A61 mov       rax,rcx
00007FFDF74E4A64 vmovaps   xmm6,[rsp]
00007FFDF74E4A69 add       rsp,18h
00007FFDF74E4A6D ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Multiply(Motor, Motor)
00007FFDF74E4AB0 sub       rsp,78h
00007FFDF74E4AB4 vzeroupper
00007FFDF74E4AB7 vmovaps   [rsp+60h],xmm6
00007FFDF74E4ABD vmovaps   [rsp+50h],xmm7
00007FFDF74E4AC3 vmovaps   [rsp+40h],xmm8
00007FFDF74E4AC9 vmovaps   [rsp+30h],xmm9
00007FFDF74E4ACF vmovaps   [rsp+20h],xmm10
00007FFDF74E4AD5 vmovaps   [rsp+10h],xmm11
00007FFDF74E4ADB vmovaps   [rsp],xmm12
00007FFDF74E4AE0 vmovupd   xmm0,[rdx]
00007FFDF74E4AE4 vmovupd   xmm1,[rdx+10h]
00007FFDF74E4AE9 vmovupd   xmm2,[r8]
00007FFDF74E4AEE vmovupd   xmm3,[r8+10h]
00007FFDF74E4AF4 vshufps   xmm4,xmm0,xmm0,0
00007FFDF74E4AF9 vshufps   xmm5,xmm0,xmm0,0E6h
00007FFDF74E4AFE vshufps   xmm6,xmm0,xmm0,9Dh
00007FFDF74E4B03 vshufps   xmm0,xmm0,xmm0,7Bh
00007FFDF74E4B08 vshufps   xmm7,xmm2,xmm2,9Fh
00007FFDF74E4B0D vshufps   xmm8,xmm2,xmm2,79h
00007FFDF74E4B12 vxorps    xmm9,xmm9,xmm9
00007FFDF74E4B17 vmovss    xmm10,[rel 7FFD`F74E`4C28h]
00007FFDF74E4B1F vmovss    xmm9,xmm9,xmm10
00007FFDF74E4B24 vmulps    xmm10,xmm4,xmm2
00007FFDF74E4B28 vmulps    xmm11,xmm6,xmm8
00007FFDF74E4B2D vshufps   xmm12,xmm2,xmm2,2
00007FFDF74E4B32 vmulps    xmm12,xmm5,xmm12
00007FFDF74E4B37 vaddps    xmm11,xmm11,xmm12
00007FFDF74E4B3C vxorps    xmm11,xmm11,xmm9
00007FFDF74E4B41 vaddps    xmm10,xmm10,xmm11
00007FFDF74E4B46 vmulps    xmm11,xmm0,xmm7
00007FFDF74E4B4A vsubps    xmm10,xmm10,xmm11
00007FFDF74E4B4F vmulps    xmm4,xmm4,xmm3
00007FFDF74E4B53 vshufps   xmm11,xmm2,xmm2,0
00007FFDF74E4B58 vmulps    xmm11,xmm1,xmm11
00007FFDF74E4B5D vaddps    xmm4,xmm4,xmm11
00007FFDF74E4B62 vshufps   xmm11,xmm3,xmm3,79h
00007FFDF74E4B67 vmulps    xmm6,xmm6,xmm11
00007FFDF74E4B6C vaddps    xmm4,xmm4,xmm6
00007FFDF74E4B70 vshufps   xmm6,xmm1,xmm1,9Dh
00007FFDF74E4B75 vmulps    xmm6,xmm6,xmm8
00007FFDF74E4B7A vaddps    xmm4,xmm4,xmm6
00007FFDF74E4B7E vshufps   xmm6,xmm3,xmm3,2
00007FFDF74E4B83 vmulps    xmm11,xmm5,xmm6
00007FFDF74E4B87 vshufps   xmm3,xmm3,xmm3,9Fh
00007FFDF74E4B8C vmulps    xmm0,xmm0,xmm3
00007FFDF74E4B90 vaddps    xmm11,xmm11,xmm0
00007FFDF74E4B94 vshufps   xmm0,xmm1,xmm1,2
00007FFDF74E4B99 vshufps   xmm2,xmm2,xmm2,0E6h
00007FFDF74E4B9E vmulps    xmm0,xmm0,xmm2
00007FFDF74E4BA2 vaddps    xmm11,xmm11,xmm0
00007FFDF74E4BA6 vshufps   xmm0,xmm1,xmm1,7Bh
00007FFDF74E4BAB vmulps    xmm0,xmm0,xmm7
00007FFDF74E4BAF vaddps    xmm11,xmm11,xmm0
00007FFDF74E4BB3 vxorps    xmm11,xmm11,xmm9
00007FFDF74E4BB8 vsubps    xmm4,xmm4,xmm11
00007FFDF74E4BBD vmovupd   [rcx],xmm10
00007FFDF74E4BC1 vmovupd   [rcx+10h],xmm4
00007FFDF74E4BC6 mov       rax,rcx
00007FFDF74E4BC9 vmovaps   xmm6,[rsp+60h]
00007FFDF74E4BCF vmovaps   xmm7,[rsp+50h]
00007FFDF74E4BD5 vmovaps   xmm8,[rsp+40h]
00007FFDF74E4BDB vmovaps   xmm9,[rsp+30h]
00007FFDF74E4BE1 vmovaps   xmm10,[rsp+20h]
00007FFDF74E4BE7 vmovaps   xmm11,[rsp+10h]
00007FFDF74E4BED vmovaps   xmm12,[rsp]
00007FFDF74E4BF2 add       rsp,78h
00007FFDF74E4BF6 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Motor op_Division(Motor, Motor)
00007FFDF74E4C60 push      rdi
00007FFDF74E4C61 push      rsi
00007FFDF74E4C62 sub       rsp,0B8h
00007FFDF74E4C69 vzeroupper
00007FFDF74E4C6C vmovaps   [rsp+0A0h],xmm6
00007FFDF74E4C75 vmovaps   [rsp+90h],xmm7
00007FFDF74E4C7E vmovaps   [rsp+80h],xmm8
00007FFDF74E4C87 vmovaps   [rsp+70h],xmm9
00007FFDF74E4C8D vmovaps   [rsp+60h],xmm10
00007FFDF74E4C93 vmovaps   [rsp+50h],xmm11
00007FFDF74E4C99 vmovaps   [rsp+40h],xmm12
00007FFDF74E4C9F mov       rdi,rcx
00007FFDF74E4CA2 mov       rsi,rdx
00007FFDF74E4CA5 lea       rdx,[rsp+20h]
00007FFDF74E4CAA mov       rcx,r8
00007FFDF74E4CAD call      0000`7FFD`F74E`4300h
00007FFDF74E4CB2 vmovupd   xmm0,[rsi]
00007FFDF74E4CB6 vmovupd   xmm1,[rsi+10h]
00007FFDF74E4CBB vmovapd   xmm2,[rsp+20h]
00007FFDF74E4CC1 vmovapd   xmm3,[rsp+30h]
00007FFDF74E4CC7 vmovaps   xmm4,xmm2
00007FFDF74E4CCB vshufps   xmm5,xmm0,xmm0,0
00007FFDF74E4CD0 vshufps   xmm6,xmm0,xmm0,0E6h
00007FFDF74E4CD5 vshufps   xmm7,xmm0,xmm0,9Dh
00007FFDF74E4CDA vshufps   xmm0,xmm0,xmm0,7Bh
00007FFDF74E4CDF vshufps   xmm8,xmm2,xmm2,9Fh
00007FFDF74E4CE4 vshufps   xmm2,xmm2,xmm2,79h
00007FFDF74E4CE9 vxorps    xmm9,xmm9,xmm9
00007FFDF74E4CEE vmovss    xmm10,[rel 7FFD`F74E`4E10h]
00007FFDF74E4CF6 vmovss    xmm9,xmm9,xmm10
00007FFDF74E4CFB vmulps    xmm10,xmm5,xmm4
00007FFDF74E4CFF vmulps    xmm11,xmm7,xmm2
00007FFDF74E4D03 vshufps   xmm12,xmm4,xmm4,2
00007FFDF74E4D08 vmulps    xmm12,xmm6,xmm12
00007FFDF74E4D0D vaddps    xmm11,xmm11,xmm12
00007FFDF74E4D12 vxorps    xmm11,xmm11,xmm9
00007FFDF74E4D17 vaddps    xmm10,xmm10,xmm11
00007FFDF74E4D1C vmulps    xmm11,xmm0,xmm8
00007FFDF74E4D21 vsubps    xmm10,xmm10,xmm11
00007FFDF74E4D26 vmulps    xmm5,xmm5,xmm3
00007FFDF74E4D2A vshufps   xmm11,xmm4,xmm4,0
00007FFDF74E4D2F vmulps    xmm11,xmm1,xmm11
00007FFDF74E4D34 vaddps    xmm5,xmm5,xmm11
00007FFDF74E4D39 vshufps   xmm11,xmm3,xmm3,79h
00007FFDF74E4D3E vmulps    xmm7,xmm7,xmm11
00007FFDF74E4D43 vaddps    xmm5,xmm5,xmm7
00007FFDF74E4D47 vshufps   xmm7,xmm1,xmm1,9Dh
00007FFDF74E4D4C vmulps    xmm2,xmm7,xmm2
00007FFDF74E4D50 vaddps    xmm5,xmm5,xmm2
00007FFDF74E4D54 vshufps   xmm2,xmm3,xmm3,2
00007FFDF74E4D59 vmulps    xmm11,xmm6,xmm2
00007FFDF74E4D5D vshufps   xmm2,xmm3,xmm3,9Fh
00007FFDF74E4D62 vmulps    xmm0,xmm0,xmm2
00007FFDF74E4D66 vaddps    xmm11,xmm11,xmm0
00007FFDF74E4D6A vshufps   xmm0,xmm1,xmm1,2
00007FFDF74E4D6F vshufps   xmm2,xmm4,xmm4,0E6h
00007FFDF74E4D74 vmulps    xmm0,xmm0,xmm2
00007FFDF74E4D78 vaddps    xmm11,xmm11,xmm0
00007FFDF74E4D7C vshufps   xmm0,xmm1,xmm1,7Bh
00007FFDF74E4D81 vmulps    xmm0,xmm0,xmm8
00007FFDF74E4D86 vaddps    xmm11,xmm11,xmm0
00007FFDF74E4D8A vxorps    xmm11,xmm11,xmm9
00007FFDF74E4D8F vsubps    xmm5,xmm5,xmm11
00007FFDF74E4D94 vmovupd   [rdi],xmm10
00007FFDF74E4D98 vmovupd   [rdi+10h],xmm5
00007FFDF74E4D9D mov       rax,rdi
00007FFDF74E4DA0 vmovaps   xmm6,[rsp+0A0h]
00007FFDF74E4DA9 vmovaps   xmm7,[rsp+90h]
00007FFDF74E4DB2 vmovaps   xmm8,[rsp+80h]
00007FFDF74E4DBB vmovaps   xmm9,[rsp+70h]
00007FFDF74E4DC1 vmovaps   xmm10,[rsp+60h]
00007FFDF74E4DC7 vmovaps   xmm11,[rsp+50h]
00007FFDF74E4DCD vmovaps   xmm12,[rsp+40h]
00007FFDF74E4DD3 add       rsp,0B8h
00007FFDF74E4DDA pop       rsi
00007FFDF74E4DDB pop       rdi
00007FFDF74E4DDC ret

# -----------------------------------------------------------------------------------

# [struct Motor] Plane Conjugate(Plane)
00007FFDF74E5090 sub       rsp,68h
00007FFDF74E5094 vzeroupper
00007FFDF74E5097 vmovaps   [rsp+50h],xmm6
00007FFDF74E509D vmovaps   [rsp+40h],xmm7
00007FFDF74E50A3 vmovaps   [rsp+30h],xmm8
00007FFDF74E50A9 vmovaps   [rsp+20h],xmm9
00007FFDF74E50AF vmovaps   [rsp+10h],xmm10
00007FFDF74E50B5 vmovaps   [rsp],xmm11
00007FFDF74E50BA vmovupd   xmm0,[r8]
00007FFDF74E50BF vmovupd   xmm1,[rcx]
00007FFDF74E50C3 vmovupd   xmm2,[rcx+10h]
00007FFDF74E50C8 vmovss    xmm3,[rel 7FFD`F74E`5248h]
00007FFDF74E50D0 vmovss    xmm4,[rel 7FFD`F74E`524Ch]
00007FFDF74E50D8 vinsertps xmm3,xmm3,xmm4,10h
00007FFDF74E50DE vmovss    xmm4,[rel 7FFD`F74E`5250h]
00007FFDF74E50E6 vinsertps xmm3,xmm3,xmm4,20h
00007FFDF74E50EC vmovss    xmm4,[rel 7FFD`F74E`5254h]
00007FFDF74E50F4 vinsertps xmm3,xmm3,xmm4,30h
00007FFDF74E50FA vmovaps   xmm4,xmm3
00007FFDF74E50FE vshufps   xmm5,xmm1,xmm1,9Ch
00007FFDF74E5103 vshufps   xmm6,xmm1,xmm1,78h
00007FFDF74E5108 vshufps   xmm7,xmm1,xmm1,0
00007FFDF74E510D vshufps   xmm8,xmm1,xmm1,2
00007FFDF74E5112 vshufps   xmm9,xmm1,xmm1,9Eh
00007FFDF74E5117 vmulps    xmm8,xmm8,xmm9
00007FFDF74E511C vshufps   xmm9,xmm1,xmm1,79h
00007FFDF74E5121 vshufps   xmm10,xmm1,xmm1,0E5h
00007FFDF74E5126 vmulps    xmm9,xmm9,xmm10
00007FFDF74E512B vaddps    xmm8,xmm8,xmm9
00007FFDF74E5130 vmulps    xmm3,xmm8,xmm3
00007FFDF74E5134 vmulps    xmm8,xmm1,xmm5
00007FFDF74E5138 vxorps    xmm9,xmm9,xmm9
00007FFDF74E513D vmovss    xmm10,[rel 7FFD`F74E`5258h]
00007FFDF74E5145 vmovss    xmm9,xmm9,xmm10
00007FFDF74E514A vshufps   xmm10,xmm1,xmm1,3
00007FFDF74E514F vshufps   xmm11,xmm1,xmm1,7Bh
00007FFDF74E5154 vmulps    xmm10,xmm10,xmm11
00007FFDF74E5159 vxorps    xmm9,xmm9,xmm10
00007FFDF74E515E vsubps    xmm8,xmm8,xmm9
00007FFDF74E5163 vmulps    xmm8,xmm8,xmm4
00007FFDF74E5167 vmulps    xmm9,xmm1,xmm1
00007FFDF74E516B vmulps    xmm10,xmm5,xmm5
00007FFDF74E516F vsubps    xmm9,xmm9,xmm10
00007FFDF74E5174 vmulps    xmm10,xmm7,xmm7
00007FFDF74E5178 vaddps    xmm9,xmm9,xmm10
00007FFDF74E517D vmulps    xmm10,xmm6,xmm6
00007FFDF74E5181 vsubps    xmm9,xmm9,xmm10
00007FFDF74E5186 vmulps    xmm7,xmm7,xmm2
00007FFDF74E518A vshufps   xmm10,xmm2,xmm2,9Ch
00007FFDF74E518F vmulps    xmm6,xmm6,xmm10
00007FFDF74E5194 vaddps    xmm7,xmm7,xmm6
00007FFDF74E5198 vshufps   xmm6,xmm2,xmm2,0
00007FFDF74E519D vmulps    xmm1,xmm1,xmm6
00007FFDF74E51A1 vaddps    xmm7,xmm7,xmm1
00007FFDF74E51A5 vshufps   xmm1,xmm2,xmm2,78h
00007FFDF74E51AA vmulps    xmm1,xmm5,xmm1
00007FFDF74E51AE vsubps    xmm7,xmm7,xmm1
00007FFDF74E51B2 vmulps    xmm7,xmm7,xmm4
00007FFDF74E51B6 vshufps   xmm1,xmm0,xmm0,78h
00007FFDF74E51BB vmulps    xmm1,xmm3,xmm1
00007FFDF74E51BF vshufps   xmm2,xmm0,xmm0,9Ch
00007FFDF74E51C4 vmulps    xmm2,xmm8,xmm2
00007FFDF74E51C8 vaddps    xmm1,xmm1,xmm2
00007FFDF74E51CC vmulps    xmm2,xmm9,xmm0
00007FFDF74E51D0 vaddps    xmm1,xmm1,xmm2
00007FFDF74E51D4 vdpps     xmm0,xmm7,xmm0,0E1h
00007FFDF74E51DA vaddps    xmm1,xmm1,xmm0
00007FFDF74E51DE vmovupd   [rdx],xmm1
00007FFDF74E51E2 mov       rax,rdx
00007FFDF74E51E5 vmovaps   xmm6,[rsp+50h]
00007FFDF74E51EB vmovaps   xmm7,[rsp+40h]
00007FFDF74E51F1 vmovaps   xmm8,[rsp+30h]
00007FFDF74E51F7 vmovaps   xmm9,[rsp+20h]
00007FFDF74E51FD vmovaps   xmm10,[rsp+10h]
00007FFDF74E5203 vmovaps   xmm11,[rsp]
00007FFDF74E5208 add       rsp,68h
00007FFDF74E520C ret

# -----------------------------------------------------------------------------------

# [struct Motor] Point Conjugate(Point)
00007FFDF74E54D0 sub       rsp,78h
00007FFDF74E54D4 vzeroupper
00007FFDF74E54D7 vmovaps   [rsp+60h],xmm6
00007FFDF74E54DD vmovaps   [rsp+50h],xmm7
00007FFDF74E54E3 vmovaps   [rsp+40h],xmm8
00007FFDF74E54E9 vmovaps   [rsp+30h],xmm9
00007FFDF74E54EF vmovaps   [rsp+20h],xmm10
00007FFDF74E54F5 vmovaps   [rsp+10h],xmm11
00007FFDF74E54FB vmovaps   [rsp],xmm12
00007FFDF74E5500 vmovupd   xmm0,[r8]
00007FFDF74E5505 vmovupd   xmm1,[rcx]
00007FFDF74E5509 vmovupd   xmm2,[rcx+10h]
00007FFDF74E550E vxorps    xmm3,xmm3,xmm3
00007FFDF74E5512 vmovss    xmm4,[rel 7FFD`F74E`5688h]
00007FFDF74E551A vinsertps xmm3,xmm3,xmm4,10h
00007FFDF74E5520 vmovss    xmm4,[rel 7FFD`F74E`568Ch]
00007FFDF74E5528 vinsertps xmm3,xmm3,xmm4,20h
00007FFDF74E552E vmovss    xmm4,[rel 7FFD`F74E`5690h]
00007FFDF74E5536 vinsertps xmm3,xmm3,xmm4,30h
00007FFDF74E553C vmovaps   xmm4,xmm3
00007FFDF74E5540 vshufps   xmm5,xmm1,xmm1,0
00007FFDF74E5545 vshufps   xmm6,xmm1,xmm1,9Ch
00007FFDF74E554A vshufps   xmm7,xmm1,xmm1,78h
00007FFDF74E554F vmulps    xmm8,xmm1,xmm6
00007FFDF74E5553 vmulps    xmm9,xmm5,xmm7
00007FFDF74E5557 vsubps    xmm8,xmm8,xmm9
00007FFDF74E555C vmulps    xmm8,xmm8,xmm3
00007FFDF74E5560 vmulps    xmm9,xmm5,xmm6
00007FFDF74E5564 vmulps    xmm10,xmm7,xmm1
00007FFDF74E5568 vaddps    xmm9,xmm9,xmm10
00007FFDF74E556D vmulps    xmm9,xmm9,xmm3
00007FFDF74E5571 vmulps    xmm3,xmm1,xmm1
00007FFDF74E5575 vshufps   xmm10,xmm1,xmm1,1
00007FFDF74E557A vmulps    xmm10,xmm10,xmm10
00007FFDF74E557F vaddps    xmm3,xmm3,xmm10
00007FFDF74E5584 vshufps   xmm10,xmm1,xmm1,9Eh
00007FFDF74E5589 vmulps    xmm11,xmm10,xmm10
00007FFDF74E558E vshufps   xmm10,xmm1,xmm1,7Bh
00007FFDF74E5593 vmulps    xmm10,xmm10,xmm10
00007FFDF74E5598 vaddps    xmm11,xmm11,xmm10
00007FFDF74E559D vxorps    xmm10,xmm10,xmm10
00007FFDF74E55A2 vmovss    xmm12,[rel 7FFD`F74E`5694h]
00007FFDF74E55AA vmovss    xmm10,xmm10,xmm12
00007FFDF74E55AF vxorps    xmm11,xmm11,xmm10
00007FFDF74E55B4 vsubps    xmm3,xmm3,xmm11
00007FFDF74E55B9 vshufps   xmm10,xmm2,xmm2,9Ch
00007FFDF74E55BE vmulps    xmm11,xmm7,xmm10
00007FFDF74E55C3 vmulps    xmm5,xmm5,xmm2
00007FFDF74E55C7 vsubps    xmm11,xmm11,xmm5
00007FFDF74E55CB vshufps   xmm5,xmm2,xmm2,78h
00007FFDF74E55D0 vmulps    xmm5,xmm6,xmm5
00007FFDF74E55D4 vsubps    xmm11,xmm11,xmm5
00007FFDF74E55D8 vshufps   xmm2,xmm2,xmm2,0
00007FFDF74E55DD vmulps    xmm1,xmm1,xmm2
00007FFDF74E55E1 vsubps    xmm11,xmm11,xmm1
00007FFDF74E55E5 vmulps    xmm11,xmm11,xmm4
00007FFDF74E55E9 vshufps   xmm1,xmm0,xmm0,9Ch
00007FFDF74E55EE vmulps    xmm1,xmm8,xmm1
00007FFDF74E55F2 vshufps   xmm2,xmm0,xmm0,78h
00007FFDF74E55F7 vmulps    xmm2,xmm9,xmm2
00007FFDF74E55FB vaddps    xmm1,xmm1,xmm2
00007FFDF74E55FF vmulps    xmm2,xmm3,xmm0
00007FFDF74E5603 vaddps    xmm1,xmm1,xmm2
00007FFDF74E5607 vshufps   xmm0,xmm0,xmm0,0
00007FFDF74E560C vmulps    xmm0,xmm11,xmm0
00007FFDF74E5610 vaddps    xmm1,xmm1,xmm0
00007FFDF74E5614 vmovupd   [rdx],xmm1
00007FFDF74E5618 mov       rax,rdx
00007FFDF74E561B vmovaps   xmm6,[rsp+60h]
00007FFDF74E5621 vmovaps   xmm7,[rsp+50h]
00007FFDF74E5627 vmovaps   xmm8,[rsp+40h]
00007FFDF74E562D vmovaps   xmm9,[rsp+30h]
00007FFDF74E5633 vmovaps   xmm10,[rsp+20h]
00007FFDF74E5639 vmovaps   xmm11,[rsp+10h]
00007FFDF74E563F vmovaps   xmm12,[rsp]
00007FFDF74E5644 add       rsp,78h
00007FFDF74E5648 ret

# -----------------------------------------------------------------------------------

# [struct Motor] Line Conjugate(Line)
00007FFDF74E5EA0 sub       rsp,0C8h
00007FFDF74E5EA7 vzeroupper
00007FFDF74E5EAA vmovaps   [rsp+0B0h],xmm6
00007FFDF74E5EB3 vmovaps   [rsp+0A0h],xmm7
00007FFDF74E5EBC vmovaps   [rsp+90h],xmm8
00007FFDF74E5EC5 vmovaps   [rsp+80h],xmm9
00007FFDF74E5ECE vmovaps   [rsp+70h],xmm10
00007FFDF74E5ED4 vmovaps   [rsp+60h],xmm11
00007FFDF74E5EDA vmovaps   [rsp+50h],xmm12
00007FFDF74E5EE0 vmovaps   [rsp+40h],xmm13
00007FFDF74E5EE6 vmovaps   [rsp+30h],xmm14
00007FFDF74E5EEC vmovaps   [rsp+20h],xmm15
00007FFDF74E5EF2 vmovupd   xmm0,[r8]
00007FFDF74E5EF7 vmovupd   xmm1,[r8+10h]
00007FFDF74E5EFD vmovupd   xmm2,[rcx]
00007FFDF74E5F01 vmovupd   xmm3,[rcx+10h]
00007FFDF74E5F06 vmovaps   xmm4,xmm2
00007FFDF74E5F0A vshufps   xmm5,xmm2,xmm2,9Ch
00007FFDF74E5F0F vshufps   xmm6,xmm2,xmm2,78h
00007FFDF74E5F14 vshufps   xmm7,xmm2,xmm2,1
00007FFDF74E5F19 vmulps    xmm7,xmm7,xmm7
00007FFDF74E5F1D vmulps    xmm8,xmm2,xmm2
00007FFDF74E5F21 vaddps    xmm7,xmm8,xmm7
00007FFDF74E5F25 vshufps   xmm8,xmm2,xmm2,9Eh
00007FFDF74E5F2A vmulps    xmm9,xmm8,xmm8
00007FFDF74E5F2F vshufps   xmm8,xmm2,xmm2,7Bh
00007FFDF74E5F34 vmulps    xmm8,xmm8,xmm8
00007FFDF74E5F39 vaddps    xmm9,xmm9,xmm8
00007FFDF74E5F3E vxorps    xmm8,xmm8,xmm8
00007FFDF74E5F43 vmovss    xmm10,[rel 7FFD`F74E`6210h]
00007FFDF74E5F4B vmovss    xmm8,xmm8,xmm10
00007FFDF74E5F50 vxorps    xmm9,xmm9,xmm8
00007FFDF74E5F55 vsubps    xmm7,xmm7,xmm9
00007FFDF74E5F5A vshufps   xmm8,xmm4,xmm4,0
00007FFDF74E5F5F vxorps    xmm9,xmm9,xmm9
00007FFDF74E5F64 vmovss    xmm10,[rel 7FFD`F74E`6214h]
00007FFDF74E5F6C vinsertps xmm9,xmm9,xmm10,10h
00007FFDF74E5F72 vmovss    xmm10,[rel 7FFD`F74E`6218h]
00007FFDF74E5F7A vinsertps xmm9,xmm9,xmm10,20h
00007FFDF74E5F80 vmovss    xmm10,[rel 7FFD`F74E`621Ch]
00007FFDF74E5F88 vinsertps xmm9,xmm9,xmm10,30h
00007FFDF74E5F8E vmulps    xmm10,xmm8,xmm5
00007FFDF74E5F92 vmulps    xmm11,xmm4,xmm6
00007FFDF74E5F96 vaddps    xmm10,xmm10,xmm11
00007FFDF74E5F9B vmulps    xmm10,xmm10,xmm9
00007FFDF74E5FA0 vmulps    xmm4,xmm4,xmm5
00007FFDF74E5FA4 vmulps    xmm5,xmm8,xmm6
00007FFDF74E5FA8 vsubps    xmm4,xmm4,xmm5
00007FFDF74E5FAC vmulps    xmm4,xmm4,xmm9
00007FFDF74E5FB1 vmovapd   [rsp+10h],xmm4
00007FFDF74E5FB7 vmovaps   xmm5,xmm2
00007FFDF74E5FBB vshufps   xmm6,xmm2,xmm2,9Ch
00007FFDF74E5FC0 vshufps   xmm8,xmm2,xmm2,78h
00007FFDF74E5FC5 vshufps   xmm9,xmm2,xmm2,1
00007FFDF74E5FCA vmovapd   [rsp],xmm9
00007FFDF74E5FCF vshufps   xmm2,xmm2,xmm2,0
00007FFDF74E5FD4 vxorps    xmm11,xmm11,xmm11
00007FFDF74E5FD9 vmovss    xmm12,[rel 7FFD`F74E`6220h]
00007FFDF74E5FE1 vinsertps xmm11,xmm11,xmm12,10h
00007FFDF74E5FE7 vmovss    xmm12,[rel 7FFD`F74E`6224h]
00007FFDF74E5FEF vinsertps xmm11,xmm11,xmm12,20h
00007FFDF74E5FF5 vmovss    xmm12,[rel 7FFD`F74E`6228h]
00007FFDF74E5FFD vinsertps xmm11,xmm11,xmm12,30h
00007FFDF74E6003 vshufps   xmm12,xmm3,xmm3,0
00007FFDF74E6008 vshufps   xmm13,xmm3,xmm3,78h
00007FFDF74E600D vshufps   xmm14,xmm3,xmm3,9Ch
00007FFDF74E6012 vmulps    xmm15,xmm5,xmm3
00007FFDF74E6016 vshufps   xmm9,xmm3,xmm3,1
00007FFDF74E601B vmovapd   xmm4,[rsp]
00007FFDF74E6020 vmulps    xmm4,xmm4,xmm9
00007FFDF74E6025 vsubps    xmm4,xmm15,xmm4
00007FFDF74E6029 vshufps   xmm9,xmm5,xmm5,7Eh
00007FFDF74E602E vshufps   xmm15,xmm3,xmm3,7Eh
00007FFDF74E6033 vmulps    xmm9,xmm9,xmm15
00007FFDF74E6038 vsubps    xmm4,xmm4,xmm9
00007FFDF74E603D vshufps   xmm9,xmm5,xmm5,9Bh
00007FFDF74E6042 vshufps   xmm15,xmm3,xmm3,9Bh
00007FFDF74E6047 vmulps    xmm9,xmm9,xmm15
00007FFDF74E604C vsubps    xmm4,xmm4,xmm9
00007FFDF74E6051 vaddps    xmm4,xmm4,xmm4
00007FFDF74E6055 vmulps    xmm9,xmm5,xmm14
00007FFDF74E605A vmulps    xmm15,xmm8,xmm12
00007FFDF74E605F vaddps    xmm9,xmm9,xmm15
00007FFDF74E6064 vmulps    xmm15,xmm6,xmm3
00007FFDF74E6068 vaddps    xmm9,xmm9,xmm15
00007FFDF74E606D vmulps    xmm15,xmm2,xmm13
00007FFDF74E6072 vsubps    xmm9,xmm9,xmm15
00007FFDF74E6077 vmulps    xmm9,xmm9,xmm11
00007FFDF74E607C vmulps    xmm5,xmm5,xmm13
00007FFDF74E6081 vmulps    xmm2,xmm2,xmm14
00007FFDF74E6086 vaddps    xmm5,xmm5,xmm2
00007FFDF74E608A vmulps    xmm2,xmm8,xmm3
00007FFDF74E608E vaddps    xmm5,xmm5,xmm2
00007FFDF74E6092 vmulps    xmm2,xmm6,xmm12
00007FFDF74E6097 vsubps    xmm5,xmm5,xmm2
00007FFDF74E609B vmulps    xmm5,xmm5,xmm11
00007FFDF74E60A0 vmovaps   xmm2,xmm0
00007FFDF74E60A4 vmovaps   xmm3,xmm2
00007FFDF74E60A8 vshufps   xmm2,xmm3,xmm2,78h
00007FFDF74E60AD vmovaps   xmm3,xmm0
00007FFDF74E60B1 vmovaps   xmm6,xmm3
00007FFDF74E60B5 vshufps   xmm3,xmm6,xmm3,9Ch
00007FFDF74E60BA vmovaps   xmm6,xmm7
00007FFDF74E60BE vmovaps   xmm8,xmm0
00007FFDF74E60C2 vmulps    xmm6,xmm6,xmm8
00007FFDF74E60C7 vmovaps   xmm8,xmm10
00007FFDF74E60CC vmovaps   xmm11,xmm2
00007FFDF74E60D0 vmulps    xmm8,xmm8,xmm11
00007FFDF74E60D5 vaddps    xmm6,xmm6,xmm8
00007FFDF74E60DA vmovapd   xmm8,[rsp+10h]
00007FFDF74E60E0 vmovaps   xmm11,xmm8
00007FFDF74E60E5 vmovaps   xmm12,xmm3
00007FFDF74E60E9 vmulps    xmm11,xmm11,xmm12
00007FFDF74E60EE vaddps    xmm6,xmm6,xmm11
00007FFDF74E60F3 vmovaps   xmm11,xmm1
00007FFDF74E60F7 vmulps    xmm7,xmm7,xmm11
00007FFDF74E60FC vmovaps   xmm11,xmm1
00007FFDF74E6100 vmovaps   xmm12,xmm11
00007FFDF74E6105 vshufps   xmm11,xmm12,xmm11,78h
00007FFDF74E610B vmulps    xmm10,xmm10,xmm11
00007FFDF74E6110 vaddps    xmm7,xmm7,xmm10
00007FFDF74E6115 vmovaps   xmm10,xmm1
00007FFDF74E6119 vshufps   xmm1,xmm10,xmm1,9Ch
00007FFDF74E611E vmulps    xmm1,xmm8,xmm1
00007FFDF74E6122 vaddps    xmm7,xmm7,xmm1
00007FFDF74E6126 vmulps    xmm0,xmm4,xmm0
00007FFDF74E612A vmovaps   xmm1,xmm7
00007FFDF74E612E vaddps    xmm7,xmm1,xmm0
00007FFDF74E6132 vmulps    xmm0,xmm9,xmm3
00007FFDF74E6136 vmovaps   xmm1,xmm7
00007FFDF74E613A vaddps    xmm7,xmm1,xmm0
00007FFDF74E613E vmulps    xmm0,xmm5,xmm2
00007FFDF74E6142 vmovaps   xmm1,xmm7
00007FFDF74E6146 vaddps    xmm7,xmm1,xmm0
00007FFDF74E614A vmovaps   xmm0,xmm6
00007FFDF74E614E vmovaps   xmm1,xmm7
00007FFDF74E6152 vmovupd   [rdx],xmm0
00007FFDF74E6156 vmovupd   [rdx+10h],xmm1
00007FFDF74E615B mov       rax,rdx
00007FFDF74E615E vmovaps   xmm6,[rsp+0B0h]
00007FFDF74E6167 vmovaps   xmm7,[rsp+0A0h]
00007FFDF74E6170 vmovaps   xmm8,[rsp+90h]
00007FFDF74E6179 vmovaps   xmm9,[rsp+80h]
00007FFDF74E6182 vmovaps   xmm10,[rsp+70h]
00007FFDF74E6188 vmovaps   xmm11,[rsp+60h]
00007FFDF74E618E vmovaps   xmm12,[rsp+50h]
00007FFDF74E6194 vmovaps   xmm13,[rsp+40h]
00007FFDF74E619A vmovaps   xmm14,[rsp+30h]
00007FFDF74E61A0 vmovaps   xmm15,[rsp+20h]
00007FFDF74E61A6 add       rsp,0C8h
00007FFDF74E61AD ret

# -----------------------------------------------------------------------------------

# [struct Motor] Direction Conjugate(Direction)
00007FFDF74E63E0 sub       rsp,28h
00007FFDF74E63E4 vzeroupper
00007FFDF74E63E7 vmovaps   [rsp+10h],xmm6
00007FFDF74E63ED xor       eax,eax
00007FFDF74E63EF mov       [rsp],rax
00007FFDF74E63F3 mov       [rsp+8],rax
00007FFDF74E63F8 vmovupd   xmm0,[rcx]
00007FFDF74E63FC vxorps    xmm1,xmm1,xmm1
00007FFDF74E6400 vmovss    xmm2,[rel 7FFD`F74E`6538h]
00007FFDF74E6408 vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74E640E vmovss    xmm2,[rel 7FFD`F74E`653Ch]
00007FFDF74E6416 vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74E641C vmovss    xmm2,[rel 7FFD`F74E`6540h]
00007FFDF74E6424 vinsertps xmm1,xmm1,xmm2,30h
00007FFDF74E642A vshufps   xmm2,xmm0,xmm0,0
00007FFDF74E642F vshufps   xmm3,xmm0,xmm0,9Ch
00007FFDF74E6434 vshufps   xmm4,xmm0,xmm0,78h
00007FFDF74E6439 vmulps    xmm5,xmm0,xmm3
00007FFDF74E643D vmulps    xmm6,xmm2,xmm4
00007FFDF74E6441 vsubps    xmm5,xmm5,xmm6
00007FFDF74E6445 vmulps    xmm5,xmm5,xmm1
00007FFDF74E6449 vmulps    xmm2,xmm2,xmm3
00007FFDF74E644D vmulps    xmm3,xmm4,xmm0
00007FFDF74E6451 vaddps    xmm2,xmm2,xmm3
00007FFDF74E6455 vmulps    xmm2,xmm2,xmm1
00007FFDF74E6459 vmulps    xmm1,xmm0,xmm0
00007FFDF74E645D vshufps   xmm3,xmm0,xmm0,1
00007FFDF74E6462 vmulps    xmm3,xmm3,xmm3
00007FFDF74E6466 vaddps    xmm1,xmm1,xmm3
00007FFDF74E646A vshufps   xmm3,xmm0,xmm0,9Eh
00007FFDF74E646F vmulps    xmm4,xmm3,xmm3
00007FFDF74E6473 vshufps   xmm3,xmm0,xmm0,7Bh
00007FFDF74E6478 vmulps    xmm0,xmm3,xmm3
00007FFDF74E647C vaddps    xmm4,xmm4,xmm0
00007FFDF74E6480 vxorps    xmm0,xmm0,xmm0
00007FFDF74E6484 vmovss    xmm3,[rel 7FFD`F74E`6544h]
00007FFDF74E648C vmovss    xmm0,xmm0,xmm3
00007FFDF74E6490 vxorps    xmm0,xmm4,xmm0
00007FFDF74E6494 vsubps    xmm1,xmm1,xmm0
00007FFDF74E6498 xor       eax,eax
00007FFDF74E649A lea       rcx,[rsp]
00007FFDF74E649E movsxd    r9,eax
00007FFDF74E64A1 shl       r9,4
00007FFDF74E64A5 add       rcx,r9
00007FFDF74E64A8 add       r9,r8
00007FFDF74E64AB vmovupd   xmm0,[r9]
00007FFDF74E64B0 vshufps   xmm0,xmm0,xmm0,9Ch
00007FFDF74E64B5 vmulps    xmm0,xmm5,xmm0
00007FFDF74E64B9 vmovupd   [rcx],xmm0
00007FFDF74E64BD vmovupd   xmm0,[rcx]
00007FFDF74E64C1 vmovupd   xmm3,[r9]
00007FFDF74E64C6 vshufps   xmm3,xmm3,xmm3,78h
00007FFDF74E64CB vmulps    xmm3,xmm2,xmm3
00007FFDF74E64CF vaddps    xmm0,xmm0,xmm3
00007FFDF74E64D3 vmovupd   [rcx],xmm0
00007FFDF74E64D7 vmovupd   xmm0,[rcx]
00007FFDF74E64DB vmovupd   xmm3,[r9]
00007FFDF74E64E0 vmulps    xmm3,xmm1,xmm3
00007FFDF74E64E4 vaddps    xmm0,xmm0,xmm3
00007FFDF74E64E8 vmovupd   [rcx],xmm0
00007FFDF74E64EC inc       eax
00007FFDF74E64EE test      eax,eax
00007FFDF74E64F0 jle       short 0000`7FFD`F74E`649Ah
00007FFDF74E64F2 vmovapd   xmm0,[rsp]
00007FFDF74E64F7 vmovupd   [rdx],xmm0
00007FFDF74E64FB mov       rax,rdx
00007FFDF74E64FE vmovaps   xmm6,[rsp+10h]
00007FFDF74E6504 add       rsp,28h
00007FFDF74E6508 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Void Store(Single*)
00007FFDF74E69D0 vzeroupper
00007FFDF74E69D3 xchg      ax,ax
00007FFDF74E69D5 vmovupd   xmm0,[rcx]
00007FFDF74E69D9 vmovups   [rdx],xmm0
00007FFDF74E69DD ret

# -----------------------------------------------------------------------------------

# [struct Plane] Void Store(System.Span`1[System.Single])
00007FFDF74E69F0 push      rsi
00007FFDF74E69F1 sub       rsp,30h
00007FFDF74E69F5 vzeroupper
00007FFDF74E69F8 xor       eax,eax
00007FFDF74E69FA mov       [rsp+28h],rax
00007FFDF74E69FF mov       rax,[rdx]
00007FFDF74E6A02 mov       edx,[rdx+8]
00007FFDF74E6A05 vmovupd   xmm0,[rcx]
00007FFDF74E6A09 xor       ecx,ecx
00007FFDF74E6A0B mov       [rsp+28h],rcx
00007FFDF74E6A10 cmp       edx,4
00007FFDF74E6A13 jl        short 0000`7FFD`F74E`6A34h
00007FFDF74E6A15 xor       ecx,ecx
00007FFDF74E6A17 test      edx,edx
00007FFDF74E6A19 je        short 0000`7FFD`F74E`6A1Eh
00007FFDF74E6A1B mov       rcx,rax
00007FFDF74E6A1E mov       [rsp+28h],rcx
00007FFDF74E6A23 vmovups   [rcx],xmm0
00007FFDF74E6A27 xor       ecx,ecx
00007FFDF74E6A29 mov       [rsp+28h],rcx
00007FFDF74E6A2E add       rsp,30h
00007FFDF74E6A32 pop       rsi
00007FFDF74E6A33 ret
00007FFDF74E6A34 mov       rcx,7FFD`F72F`D4C0h
00007FFDF74E6A3E call      0000`7FFE`56D7`7710h
00007FFDF74E6A43 mov       rsi,rax
00007FFDF74E6A46 mov       ecx,25h
00007FFDF74E6A4B mov       rdx,7FFD`F731`9EA0h
00007FFDF74E6A55 call      0000`7FFE`56EA`03E0h
00007FFDF74E6A5A mov       rdx,rax
00007FFDF74E6A5D mov       rcx,rsi
00007FFDF74E6A60 call      0000`7FFD`F725`D238h
00007FFDF74E6A65 mov       rcx,rsi
00007FFDF74E6A68 call      0000`7FFE`56D3`B3A0h
00007FFDF74E6A6D int3

# -----------------------------------------------------------------------------------

# [struct Plane] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E6A90 vzeroupper
00007FFDF74E6A93 xchg      ax,ax
00007FFDF74E6A95 vmovupd   xmm0,[rcx]
00007FFDF74E6A99 vextractps eax,xmm0,1
00007FFDF74E6A9F vmovd     xmm0,eax
00007FFDF74E6AA3 vmovss    [rdx],xmm0
00007FFDF74E6AA7 vmovupd   xmm0,[rcx]
00007FFDF74E6AAB vextractps eax,xmm0,2
00007FFDF74E6AB1 vmovd     xmm0,eax
00007FFDF74E6AB5 vmovss    [r8],xmm0
00007FFDF74E6ABA vmovupd   xmm0,[rcx]
00007FFDF74E6ABE vextractps eax,xmm0,3
00007FFDF74E6AC4 vmovd     xmm0,eax
00007FFDF74E6AC8 vmovss    [r9],xmm0
00007FFDF74E6ACD vmovss    xmm0,[rcx]
00007FFDF74E6AD1 mov       rax,[rsp+28h]
00007FFDF74E6AD6 vmovss    [rax],xmm0
00007FFDF74E6ADA ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane Normalized()
00007FFDF74E6B00 vzeroupper
00007FFDF74E6B03 xchg      ax,ax
00007FFDF74E6B05 vmovupd   xmm0,[rcx]
00007FFDF74E6B09 vmovaps   xmm1,xmm0
00007FFDF74E6B0D vmovaps   xmm2,xmm0
00007FFDF74E6B11 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74E6B17 vrsqrtps  xmm2,xmm1
00007FFDF74E6B1B vmulps    xmm3,xmm2,xmm2
00007FFDF74E6B1F vmulps    xmm3,xmm1,xmm3
00007FFDF74E6B23 vmovss    xmm1,[rel 7FFD`F74E`6B80h]
00007FFDF74E6B2B vbroadcastss xmm1,xmm1
00007FFDF74E6B30 vsubps    xmm1,xmm1,xmm3
00007FFDF74E6B34 vmovss    xmm3,[rel 7FFD`F74E`6B84h]
00007FFDF74E6B3C vbroadcastss xmm3,xmm3
00007FFDF74E6B41 vmulps    xmm2,xmm3,xmm2
00007FFDF74E6B45 vmulps    xmm1,xmm2,xmm1
00007FFDF74E6B49 vxorps    xmm2,xmm2,xmm2
00007FFDF74E6B4D vmovss    xmm3,[rel 7FFD`F74E`6B88h]
00007FFDF74E6B55 vmovss    xmm2,xmm2,xmm3
00007FFDF74E6B59 vblendps  xmm1,xmm1,xmm2,1
00007FFDF74E6B5F vmulps    xmm0,xmm1,xmm0
00007FFDF74E6B63 vmovupd   [rdx],xmm0
00007FFDF74E6B67 mov       rax,rdx
00007FFDF74E6B6A ret

# -----------------------------------------------------------------------------------

# [struct Plane] Single Norm()
00007FFDF74E6BA0 push      rax
00007FFDF74E6BA1 vzeroupper
00007FFDF74E6BA4 nop
00007FFDF74E6BA5 vmovupd   xmm0,[rcx]
00007FFDF74E6BA9 vmovaps   xmm1,xmm0
00007FFDF74E6BAD vdpps     xmm0,xmm1,xmm0,0E1h
00007FFDF74E6BB3 vmovaps   xmm1,xmm0
00007FFDF74E6BB7 vrsqrtps  xmm2,xmm0
00007FFDF74E6BBB vmulps    xmm3,xmm2,xmm2
00007FFDF74E6BBF vmulps    xmm3,xmm0,xmm3
00007FFDF74E6BC3 vmovss    xmm0,[rel 7FFD`F74E`6C20h]
00007FFDF74E6BCB vbroadcastss xmm0,xmm0
00007FFDF74E6BD0 vsubps    xmm0,xmm0,xmm3
00007FFDF74E6BD4 vmovss    xmm3,[rel 7FFD`F74E`6C24h]
00007FFDF74E6BDC vbroadcastss xmm3,xmm3
00007FFDF74E6BE1 vmulps    xmm2,xmm3,xmm2
00007FFDF74E6BE5 vmulps    xmm0,xmm2,xmm0
00007FFDF74E6BE9 vmulps    xmm0,xmm1,xmm0
00007FFDF74E6BED vxorps    xmm1,xmm1,xmm1
00007FFDF74E6BF1 vmovss    [rsp+4],xmm1
00007FFDF74E6BF7 vmovss    [rsp+4],xmm0
00007FFDF74E6BFD vmovss    xmm0,[rsp+4]
00007FFDF74E6C03 add       rsp,8
00007FFDF74E6C07 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane Inverse()
00007FFDF74E6C40 vzeroupper
00007FFDF74E6C43 xchg      ax,ax
00007FFDF74E6C45 vmovupd   xmm0,[rcx]
00007FFDF74E6C49 vmovaps   xmm1,xmm0
00007FFDF74E6C4D vmovaps   xmm2,xmm0
00007FFDF74E6C51 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74E6C57 vrsqrtps  xmm2,xmm1
00007FFDF74E6C5B vmulps    xmm3,xmm2,xmm2
00007FFDF74E6C5F vmulps    xmm3,xmm1,xmm3
00007FFDF74E6C63 vmovss    xmm1,[rel 7FFD`F74E`6CB0h]
00007FFDF74E6C6B vbroadcastss xmm1,xmm1
00007FFDF74E6C70 vsubps    xmm1,xmm1,xmm3
00007FFDF74E6C74 vmovss    xmm3,[rel 7FFD`F74E`6CB4h]
00007FFDF74E6C7C vbroadcastss xmm3,xmm3
00007FFDF74E6C81 vmulps    xmm2,xmm3,xmm2
00007FFDF74E6C85 vmulps    xmm1,xmm2,xmm1
00007FFDF74E6C89 vmulps    xmm0,xmm1,xmm0
00007FFDF74E6C8D vmulps    xmm0,xmm1,xmm0
00007FFDF74E6C91 vmovupd   [rdx],xmm0
00007FFDF74E6C95 mov       rax,rdx
00007FFDF74E6C98 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Boolean Equals(Plane, Single)
00007FFDF74E6D10 vzeroupper
00007FFDF74E6D13 xchg      ax,ax
00007FFDF74E6D15 vbroadcastss xmm0,xmm2
00007FFDF74E6D1A vmovss    xmm1,[rel 7FFD`F74E`6D58h]
00007FFDF74E6D22 vbroadcastss xmm1,xmm1
00007FFDF74E6D27 vmovupd   xmm2,[rcx]
00007FFDF74E6D2B vmovupd   xmm3,[rdx]
00007FFDF74E6D2F vsubps    xmm2,xmm2,xmm3
00007FFDF74E6D33 vandnps   xmm1,xmm1,xmm2
00007FFDF74E6D37 vcmpltps  xmm0,xmm1,xmm0
00007FFDF74E6D3C vmovmskps eax,xmm0
00007FFDF74E6D40 cmp       eax,0Fh
00007FFDF74E6D43 setne     al
00007FFDF74E6D46 movzx     eax,al
00007FFDF74E6D49 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane Reflect(Plane)
00007FFDF74E6D70 sub       rsp,18h
00007FFDF74E6D74 vzeroupper
00007FFDF74E6D77 vmovaps   [rsp],xmm6
00007FFDF74E6D7C vmovupd   xmm0,[rcx]
00007FFDF74E6D80 vmovupd   xmm1,[r8]
00007FFDF74E6D85 vshufps   xmm2,xmm0,xmm0,7Ah
00007FFDF74E6D8A vshufps   xmm3,xmm0,xmm0,9Fh
00007FFDF74E6D8F vshufps   xmm4,xmm1,xmm1,7Ah
00007FFDF74E6D94 vmulps    xmm4,xmm2,xmm4
00007FFDF74E6D98 vshufps   xmm5,xmm1,xmm1,9Fh
00007FFDF74E6D9D vmulps    xmm5,xmm3,xmm5
00007FFDF74E6DA1 vaddps    xmm4,xmm4,xmm5
00007FFDF74E6DA5 vmovshdup xmm5,xmm0
00007FFDF74E6DA9 vmovshdup xmm6,xmm1
00007FFDF74E6DAD vmulss    xmm5,xmm5,xmm6
00007FFDF74E6DB1 vaddss    xmm4,xmm4,xmm5
00007FFDF74E6DB5 vaddps    xmm5,xmm0,xmm0
00007FFDF74E6DB9 vmulps    xmm4,xmm4,xmm5
00007FFDF74E6DBD vshufps   xmm0,xmm0,xmm0,0E5h
00007FFDF74E6DC2 vmulps    xmm0,xmm0,xmm0
00007FFDF74E6DC6 vxorps    xmm5,xmm5,xmm5
00007FFDF74E6DCA vmovss    xmm6,[rel 7FFD`F74E`6E20h]
00007FFDF74E6DD2 vmovss    xmm5,xmm5,xmm6
00007FFDF74E6DD6 vxorps    xmm0,xmm0,xmm5
00007FFDF74E6DDA vmulps    xmm2,xmm2,xmm2
00007FFDF74E6DDE vsubps    xmm0,xmm0,xmm2
00007FFDF74E6DE2 vmulps    xmm2,xmm3,xmm3
00007FFDF74E6DE6 vsubps    xmm0,xmm0,xmm2
00007FFDF74E6DEA vmulps    xmm0,xmm0,xmm1
00007FFDF74E6DEE vaddps    xmm0,xmm4,xmm0
00007FFDF74E6DF2 vmovupd   [rdx],xmm0
00007FFDF74E6DF6 mov       rax,rdx
00007FFDF74E6DF9 vmovaps   xmm6,[rsp]
00007FFDF74E6DFE add       rsp,18h
00007FFDF74E6E02 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Line Reflect(Line)
00007FFDF74E6E40 sub       rsp,58h
00007FFDF74E6E44 vzeroupper
00007FFDF74E6E47 vmovaps   [rsp+40h],xmm6
00007FFDF74E6E4D vmovaps   [rsp+30h],xmm7
00007FFDF74E6E53 vmovaps   [rsp+20h],xmm8
00007FFDF74E6E59 vmovaps   [rsp+10h],xmm9
00007FFDF74E6E5F vmovaps   [rsp],xmm10
00007FFDF74E6E64 vmovupd   xmm0,[rcx]
00007FFDF74E6E68 vmovupd   xmm1,[r8]
00007FFDF74E6E6D vshufps   xmm2,xmm0,xmm0,0E6h
00007FFDF74E6E72 vshufps   xmm3,xmm0,xmm0,9Dh
00007FFDF74E6E77 vshufps   xmm4,xmm0,xmm0,7Bh
00007FFDF74E6E7C vshufps   xmm5,xmm1,xmm1,78h
00007FFDF74E6E81 vxorps    xmm6,xmm6,xmm6
00007FFDF74E6E85 vmovss    xmm7,[rel 7FFD`F74E`7040h]
00007FFDF74E6E8D vinsertps xmm6,xmm6,xmm7,10h
00007FFDF74E6E93 vmovss    xmm7,[rel 7FFD`F74E`7044h]
00007FFDF74E6E9B vinsertps xmm6,xmm6,xmm7,20h
00007FFDF74E6EA1 vmovss    xmm7,[rel 7FFD`F74E`7048h]
00007FFDF74E6EA9 vinsertps xmm6,xmm6,xmm7,30h
00007FFDF74E6EAF vmovaps   xmm7,xmm6
00007FFDF74E6EB3 vmulps    xmm8,xmm0,xmm1
00007FFDF74E6EB7 vmulps    xmm9,xmm4,xmm5
00007FFDF74E6EBB vaddps    xmm8,xmm8,xmm9
00007FFDF74E6EC0 vmulps    xmm6,xmm3,xmm6
00007FFDF74E6EC4 vmulps    xmm8,xmm8,xmm6
00007FFDF74E6EC8 vmulps    xmm6,xmm2,xmm2
00007FFDF74E6ECC vmulps    xmm9,xmm4,xmm4
00007FFDF74E6ED0 vaddps    xmm6,xmm6,xmm9
00007FFDF74E6ED5 vxorps    xmm9,xmm9,xmm9
00007FFDF74E6EDA vmovss    xmm10,[rel 7FFD`F74E`704Ch]
00007FFDF74E6EE2 vmovss    xmm9,xmm9,xmm10
00007FFDF74E6EE7 vxorps    xmm6,xmm6,xmm9
00007FFDF74E6EEC vmulps    xmm3,xmm3,xmm3
00007FFDF74E6EF0 vsubps    xmm6,xmm3,xmm6
00007FFDF74E6EF4 vshufps   xmm3,xmm1,xmm1,9Ch
00007FFDF74E6EF9 vmulps    xmm6,xmm3,xmm6
00007FFDF74E6EFD vaddps    xmm3,xmm8,xmm6
00007FFDF74E6F01 vshufps   xmm8,xmm3,xmm3,78h
00007FFDF74E6F06 vmulps    xmm2,xmm2,xmm5
00007FFDF74E6F0A vmulps    xmm1,xmm4,xmm1
00007FFDF74E6F0E vsubps    xmm2,xmm2,xmm1
00007FFDF74E6F12 vshufps   xmm0,xmm0,xmm0,0
00007FFDF74E6F17 vmulps    xmm0,xmm0,xmm7
00007FFDF74E6F1B vmulps    xmm2,xmm2,xmm0
00007FFDF74E6F1F vshufps   xmm2,xmm2,xmm2,78h
00007FFDF74E6F24 vmovupd   xmm0,[rcx]
00007FFDF74E6F28 vmovupd   xmm1,[r8+10h]
00007FFDF74E6F2E vshufps   xmm3,xmm0,xmm0,7Ah
00007FFDF74E6F33 vshufps   xmm4,xmm0,xmm0,9Fh
00007FFDF74E6F38 vmulps    xmm5,xmm0,xmm1
00007FFDF74E6F3C vshufps   xmm6,xmm1,xmm1,78h
00007FFDF74E6F41 vmulps    xmm6,xmm3,xmm6
00007FFDF74E6F45 vaddps    xmm5,xmm5,xmm6
00007FFDF74E6F49 vxorps    xmm6,xmm6,xmm6
00007FFDF74E6F4D vmovss    xmm7,[rel 7FFD`F74E`7050h]
00007FFDF74E6F55 vinsertps xmm6,xmm6,xmm7,10h
00007FFDF74E6F5B vmovss    xmm7,[rel 7FFD`F74E`7054h]
00007FFDF74E6F63 vinsertps xmm6,xmm6,xmm7,20h
00007FFDF74E6F69 vmovss    xmm7,[rel 7FFD`F74E`7058h]
00007FFDF74E6F71 vinsertps xmm6,xmm6,xmm7,30h
00007FFDF74E6F77 vmulps    xmm6,xmm4,xmm6
00007FFDF74E6F7B vmulps    xmm5,xmm5,xmm6
00007FFDF74E6F7F vshufps   xmm0,xmm0,xmm0,0E5h
00007FFDF74E6F84 vmulps    xmm0,xmm0,xmm0
00007FFDF74E6F88 vxorps    xmm6,xmm6,xmm6
00007FFDF74E6F8C vmovss    xmm7,[rel 7FFD`F74E`705Ch]
00007FFDF74E6F94 vmovss    xmm6,xmm6,xmm7
00007FFDF74E6F98 vmulps    xmm3,xmm3,xmm3
00007FFDF74E6F9C vaddps    xmm0,xmm0,xmm3
00007FFDF74E6FA0 vxorps    xmm0,xmm6,xmm0
00007FFDF74E6FA4 vmulps    xmm3,xmm4,xmm4
00007FFDF74E6FA8 vsubps    xmm0,xmm0,xmm3
00007FFDF74E6FAC vshufps   xmm1,xmm1,xmm1,9Ch
00007FFDF74E6FB1 vmulps    xmm0,xmm0,xmm1
00007FFDF74E6FB5 vaddps    xmm0,xmm5,xmm0
00007FFDF74E6FB9 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E6FBE vaddps    xmm2,xmm2,xmm0
00007FFDF74E6FC2 vmovaps   xmm0,xmm2
00007FFDF74E6FC6 vmovupd   [rdx],xmm8
00007FFDF74E6FCA vmovupd   [rdx+10h],xmm0
00007FFDF74E6FCF mov       rax,rdx
00007FFDF74E6FD2 vmovaps   xmm6,[rsp+40h]
00007FFDF74E6FD8 vmovaps   xmm7,[rsp+30h]
00007FFDF74E6FDE vmovaps   xmm8,[rsp+20h]
00007FFDF74E6FE4 vmovaps   xmm9,[rsp+10h]
00007FFDF74E6FEA vmovaps   xmm10,[rsp]
00007FFDF74E6FEF add       rsp,58h
00007FFDF74E6FF3 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Point Reflect(Point)
00007FFDF74E7090 sub       rsp,18h
00007FFDF74E7094 vzeroupper
00007FFDF74E7097 vmovaps   [rsp],xmm6
00007FFDF74E709C vmovupd   xmm0,[rcx]
00007FFDF74E70A0 vmovupd   xmm1,[r8]
00007FFDF74E70A5 vshufps   xmm2,xmm0,xmm0,9Eh
00007FFDF74E70AA vshufps   xmm3,xmm0,xmm0,79h
00007FFDF74E70AF vshufps   xmm4,xmm0,xmm0,0
00007FFDF74E70B4 vshufps   xmm5,xmm1,xmm1,0
00007FFDF74E70B9 vmulps    xmm4,xmm4,xmm5
00007FFDF74E70BD vshufps   xmm5,xmm1,xmm1,9Ch
00007FFDF74E70C2 vmulps    xmm5,xmm2,xmm5
00007FFDF74E70C6 vaddps    xmm4,xmm4,xmm5
00007FFDF74E70CA vshufps   xmm5,xmm1,xmm1,78h
00007FFDF74E70CF vmulps    xmm5,xmm3,xmm5
00007FFDF74E70D3 vaddps    xmm4,xmm4,xmm5
00007FFDF74E70D7 vxorps    xmm5,xmm5,xmm5
00007FFDF74E70DB vmovss    xmm6,[rel 7FFD`F74E`7178h]
00007FFDF74E70E3 vinsertps xmm5,xmm5,xmm6,10h
00007FFDF74E70E9 vmovss    xmm6,[rel 7FFD`F74E`717Ch]
00007FFDF74E70F1 vinsertps xmm5,xmm5,xmm6,20h
00007FFDF74E70F7 vmovss    xmm6,[rel 7FFD`F74E`7180h]
00007FFDF74E70FF vinsertps xmm5,xmm5,xmm6,30h
00007FFDF74E7105 vmulps    xmm5,xmm0,xmm5
00007FFDF74E7109 vmulps    xmm4,xmm4,xmm5
00007FFDF74E710D vmulps    xmm3,xmm3,xmm3
00007FFDF74E7111 vmulps    xmm2,xmm2,xmm2
00007FFDF74E7115 vaddps    xmm3,xmm3,xmm2
00007FFDF74E7119 vshufps   xmm0,xmm0,xmm0,0E7h
00007FFDF74E711E vmulps    xmm0,xmm0,xmm0
00007FFDF74E7122 vxorps    xmm2,xmm2,xmm2
00007FFDF74E7126 vmovss    xmm5,[rel 7FFD`F74E`7184h]
00007FFDF74E712E vmovss    xmm2,xmm2,xmm5
00007FFDF74E7132 vxorps    xmm0,xmm0,xmm2
00007FFDF74E7136 vsubps    xmm3,xmm3,xmm0
00007FFDF74E713A vmulps    xmm0,xmm1,xmm3
00007FFDF74E713E vaddps    xmm0,xmm4,xmm0
00007FFDF74E7142 vmovupd   [rdx],xmm0
00007FFDF74E7146 mov       rax,rdx
00007FFDF74E7149 vmovaps   xmm6,[rsp]
00007FFDF74E714E add       rsp,18h
00007FFDF74E7152 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_Addition(Plane, Plane)
00007FFDF74E71A0 vzeroupper
00007FFDF74E71A3 xchg      ax,ax
00007FFDF74E71A5 vmovupd   xmm0,[rdx]
00007FFDF74E71A9 vmovupd   xmm1,[r8]
00007FFDF74E71AE vaddps    xmm0,xmm0,xmm1
00007FFDF74E71B2 vmovupd   [rcx],xmm0
00007FFDF74E71B6 mov       rax,rcx
00007FFDF74E71B9 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_Subtraction(Plane, Plane)
00007FFDF74E71D0 vzeroupper
00007FFDF74E71D3 xchg      ax,ax
00007FFDF74E71D5 vmovupd   xmm0,[rdx]
00007FFDF74E71D9 vmovupd   xmm1,[r8]
00007FFDF74E71DE vsubps    xmm0,xmm0,xmm1
00007FFDF74E71E2 vmovupd   [rcx],xmm0
00007FFDF74E71E6 mov       rax,rcx
00007FFDF74E71E9 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_Multiply(Plane, Single)
00007FFDF74E7200 vzeroupper
00007FFDF74E7203 xchg      ax,ax
00007FFDF74E7205 vmovupd   xmm0,[rdx]
00007FFDF74E7209 vbroadcastss xmm1,xmm2
00007FFDF74E720E vmulps    xmm0,xmm0,xmm1
00007FFDF74E7212 vmovupd   [rcx],xmm0
00007FFDF74E7216 mov       rax,rcx
00007FFDF74E7219 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_Multiply(Single, Plane)
00007FFDF74E7230 vzeroupper
00007FFDF74E7233 xchg      ax,ax
00007FFDF74E7235 vmovupd   xmm0,[r8]
00007FFDF74E723A vbroadcastss xmm1,xmm1
00007FFDF74E723F vmulps    xmm0,xmm0,xmm1
00007FFDF74E7243 vmovupd   [rcx],xmm0
00007FFDF74E7247 mov       rax,rcx
00007FFDF74E724A ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_Division(Plane, Single)
00007FFDF74E7260 vzeroupper
00007FFDF74E7263 xchg      ax,ax
00007FFDF74E7265 vmovupd   xmm0,[rdx]
00007FFDF74E7269 vbroadcastss xmm1,xmm2
00007FFDF74E726E vrcpps    xmm2,xmm1
00007FFDF74E7272 vmulps    xmm1,xmm1,xmm2
00007FFDF74E7276 vmovss    xmm3,[rel 7FFD`F74E`72A8h]
00007FFDF74E727E vbroadcastss xmm3,xmm3
00007FFDF74E7283 vsubps    xmm1,xmm3,xmm1
00007FFDF74E7287 vmulps    xmm1,xmm2,xmm1
00007FFDF74E728B vmulps    xmm0,xmm0,xmm1
00007FFDF74E728F vmovupd   [rcx],xmm0
00007FFDF74E7293 mov       rax,rcx
00007FFDF74E7296 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_UnaryNegation(Plane)
00007FFDF74E72C0 vzeroupper
00007FFDF74E72C3 xchg      ax,ax
00007FFDF74E72C5 vmovupd   xmm0,[rdx]
00007FFDF74E72C9 vxorps    xmm1,xmm1,xmm1
00007FFDF74E72CD vmovss    xmm2,[rel 7FFD`F74E`7310h]
00007FFDF74E72D5 vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74E72DB vmovss    xmm2,[rel 7FFD`F74E`7314h]
00007FFDF74E72E3 vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74E72E9 vmovss    xmm2,[rel 7FFD`F74E`7318h]
00007FFDF74E72F1 vinsertps xmm1,xmm1,xmm2,30h
00007FFDF74E72F7 vxorps    xmm0,xmm0,xmm1
00007FFDF74E72FB vmovupd   [rcx],xmm0
00007FFDF74E72FF mov       rax,rcx
00007FFDF74E7302 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Line op_ExclusiveOr(Plane, Plane)
00007FFDF74E7330 vzeroupper
00007FFDF74E7333 xchg      ax,ax
00007FFDF74E7335 vmovupd   xmm0,[rdx]
00007FFDF74E7339 vmovupd   xmm1,[r8]
00007FFDF74E733E vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74E7343 vmulps    xmm2,xmm0,xmm2
00007FFDF74E7347 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E734C vmulps    xmm3,xmm3,xmm1
00007FFDF74E7350 vsubps    xmm2,xmm2,xmm3
00007FFDF74E7354 vshufps   xmm2,xmm2,xmm2,78h
00007FFDF74E7359 vshufps   xmm3,xmm0,xmm0,0
00007FFDF74E735E vmulps    xmm3,xmm3,xmm1
00007FFDF74E7362 vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E7367 vmulps    xmm0,xmm0,xmm1
00007FFDF74E736B vsubps    xmm3,xmm3,xmm0
00007FFDF74E736F vmovupd   [rcx],xmm2
00007FFDF74E7373 vmovupd   [rcx+10h],xmm3
00007FFDF74E7378 mov       rax,rcx
00007FFDF74E737B ret

# -----------------------------------------------------------------------------------

# [struct Plane] Point op_ExclusiveOr(Plane, Branch)
00007FFDF74E73A0 vzeroupper
00007FFDF74E73A3 xchg      ax,ax
00007FFDF74E73A5 vmovupd   xmm0,[rdx]
00007FFDF74E73A9 vmovupd   xmm1,[r8]
00007FFDF74E73AE vshufps   xmm2,xmm0,xmm0,1
00007FFDF74E73B3 vmulps    xmm2,xmm2,xmm1
00007FFDF74E73B7 vxorps    xmm3,xmm3,xmm3
00007FFDF74E73BB vmovss    xmm4,[rel 7FFD`F74E`7408h]
00007FFDF74E73C3 vinsertps xmm3,xmm3,xmm4,10h
00007FFDF74E73C9 vmovss    xmm4,[rel 7FFD`F74E`740Ch]
00007FFDF74E73D1 vinsertps xmm3,xmm3,xmm4,20h
00007FFDF74E73D7 vmovss    xmm4,[rel 7FFD`F74E`7410h]
00007FFDF74E73DF vinsertps xmm3,xmm3,xmm4,30h
00007FFDF74E73E5 vmulps    xmm2,xmm2,xmm3
00007FFDF74E73E9 vdpps     xmm0,xmm0,xmm1,0E1h
00007FFDF74E73EF vaddss    xmm0,xmm2,xmm0
00007FFDF74E73F3 vmovupd   [rcx],xmm0
00007FFDF74E73F7 mov       rax,rcx
00007FFDF74E73FA ret

# -----------------------------------------------------------------------------------

# [struct Plane] Point op_ExclusiveOr(Plane, Line)
00007FFDF74E7430 vzeroupper
00007FFDF74E7433 xchg      ax,ax
00007FFDF74E7435 vmovupd   xmm0,[rdx]
00007FFDF74E7439 vmovaps   xmm1,xmm0
00007FFDF74E743D vmovupd   xmm2,[r8]
00007FFDF74E7442 vshufps   xmm3,xmm0,xmm0,1
00007FFDF74E7447 vmulps    xmm3,xmm3,xmm2
00007FFDF74E744B vxorps    xmm4,xmm4,xmm4
00007FFDF74E744F vmovss    xmm5,[rel 7FFD`F74E`74C8h]
00007FFDF74E7457 vinsertps xmm4,xmm4,xmm5,10h
00007FFDF74E745D vmovss    xmm5,[rel 7FFD`F74E`74CCh]
00007FFDF74E7465 vinsertps xmm4,xmm4,xmm5,20h
00007FFDF74E746B vmovss    xmm5,[rel 7FFD`F74E`74D0h]
00007FFDF74E7473 vinsertps xmm4,xmm4,xmm5,30h
00007FFDF74E7479 vmulps    xmm3,xmm3,xmm4
00007FFDF74E747D vdpps     xmm1,xmm1,xmm2,0E1h
00007FFDF74E7483 vaddss    xmm1,xmm3,xmm1
00007FFDF74E7487 vmovupd   xmm2,[r8+10h]
00007FFDF74E748D vshufps   xmm3,xmm2,xmm2,78h
00007FFDF74E7492 vmulps    xmm3,xmm0,xmm3
00007FFDF74E7496 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E749B vmulps    xmm0,xmm0,xmm2
00007FFDF74E749F vsubps    xmm0,xmm3,xmm0
00007FFDF74E74A3 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E74A8 vaddps    xmm1,xmm0,xmm1
00007FFDF74E74AC vmovupd   [rcx],xmm1
00007FFDF74E74B0 mov       rax,rcx
00007FFDF74E74B3 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Dual op_ExclusiveOr(Plane, Point)
00007FFDF74E74F0 sub       rsp,18h
00007FFDF74E74F4 vzeroupper
00007FFDF74E74F7 vmovupd   xmm0,[rcx]
00007FFDF74E74FB vmovupd   xmm1,[rdx]
00007FFDF74E74FF vdpps     xmm0,xmm0,xmm1,0F1h
00007FFDF74E7505 vxorps    xmm1,xmm1,xmm1
00007FFDF74E7509 vmovss    [rsp+0Ch],xmm1
00007FFDF74E750F vmovss    [rsp+0Ch],xmm0
00007FFDF74E7515 vmovss    [rsp+10h],xmm1
00007FFDF74E751B vmovss    [rsp+14h],xmm1
00007FFDF74E7521 vmovss    xmm0,[rsp+0Ch]
00007FFDF74E7527 vmovss    [rsp+10h],xmm1
00007FFDF74E752D vmovss    [rsp+14h],xmm0
00007FFDF74E7533 mov       rax,[rsp+10h]
00007FFDF74E7538 add       rsp,18h
00007FFDF74E753C ret

# -----------------------------------------------------------------------------------

# [struct Plane] Point op_ExclusiveOr(Plane, IdealLine)
00007FFDF74E7560 vzeroupper
00007FFDF74E7563 xchg      ax,ax
00007FFDF74E7565 vmovupd   xmm0,[rdx]
00007FFDF74E7569 vmovupd   xmm1,[r8]
00007FFDF74E756E vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74E7573 vmulps    xmm2,xmm0,xmm2
00007FFDF74E7577 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E757C vmulps    xmm0,xmm0,xmm1
00007FFDF74E7580 vsubps    xmm0,xmm2,xmm0
00007FFDF74E7584 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E7589 vmovupd   [rcx],xmm0
00007FFDF74E758D mov       rax,rcx
00007FFDF74E7590 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_BitwiseOr(Plane, IdealLine)
00007FFDF74E75B0 vzeroupper
00007FFDF74E75B3 xchg      ax,ax
00007FFDF74E75B5 vmovupd   xmm0,[rdx]
00007FFDF74E75B9 vmovupd   xmm1,[r8]
00007FFDF74E75BE vdpps     xmm0,xmm0,xmm1,0E1h
00007FFDF74E75C4 vxorps    xmm1,xmm1,xmm1
00007FFDF74E75C8 vmovss    xmm2,[rel 7FFD`F74E`75E8h]
00007FFDF74E75D0 vmovss    xmm1,xmm1,xmm2
00007FFDF74E75D4 vxorps    xmm0,xmm0,xmm1
00007FFDF74E75D8 vmovupd   [rcx],xmm0
00007FFDF74E75DC mov       rax,rcx
00007FFDF74E75DF ret

# -----------------------------------------------------------------------------------

# [struct Plane] Single op_BitwiseOr(Plane, Plane)
00007FFDF74E7600 push      rax
00007FFDF74E7601 vzeroupper
00007FFDF74E7604 nop
00007FFDF74E7605 vmovupd   xmm0,[rcx]
00007FFDF74E7609 vmovupd   xmm1,[rdx]
00007FFDF74E760D vdpps     xmm0,xmm0,xmm1,0E1h
00007FFDF74E7613 vxorps    xmm1,xmm1,xmm1
00007FFDF74E7617 vmovss    [rsp+4],xmm1
00007FFDF74E761D vmovss    [rsp+4],xmm0
00007FFDF74E7623 vmovss    xmm0,[rsp+4]
00007FFDF74E7629 add       rsp,8
00007FFDF74E762D ret

# -----------------------------------------------------------------------------------

# [struct Plane] Plane op_BitwiseOr(Plane, Line)
00007FFDF74E7650 vzeroupper
00007FFDF74E7653 xchg      ax,ax
00007FFDF74E7655 vmovupd   xmm0,[rdx]
00007FFDF74E7659 vmovupd   xmm1,[r8]
00007FFDF74E765E vmovupd   xmm2,[r8+10h]
00007FFDF74E7664 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E7669 vmulps    xmm3,xmm3,xmm1
00007FFDF74E766D vshufps   xmm1,xmm1,xmm1,78h
00007FFDF74E7672 vmulps    xmm1,xmm0,xmm1
00007FFDF74E7676 vsubps    xmm3,xmm3,xmm1
00007FFDF74E767A vshufps   xmm3,xmm3,xmm3,78h
00007FFDF74E767F vmulps    xmm0,xmm0,xmm2
00007FFDF74E7683 vmovshdup xmm1,xmm0
00007FFDF74E7687 vaddps    xmm1,xmm1,xmm0
00007FFDF74E768B vunpcklps xmm0,xmm0,xmm0
00007FFDF74E768F vaddps    xmm0,xmm1,xmm0
00007FFDF74E7693 vmovhlps  xmm0,xmm0,xmm0
00007FFDF74E7697 vsubss    xmm3,xmm3,xmm0
00007FFDF74E769B vmovupd   [rcx],xmm3
00007FFDF74E769F mov       rax,rcx
00007FFDF74E76A2 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Point op_LogicalNot(Plane)
00007FFDF74E76D0 vzeroupper
00007FFDF74E76D3 xchg      ax,ax
00007FFDF74E76D5 vmovupd   xmm0,[rdx]
00007FFDF74E76D9 vmovupd   [rcx],xmm0
00007FFDF74E76DD mov       rax,rcx
00007FFDF74E76E0 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Dual op_BitwiseAnd(Plane, Point)
00007FFDF74E7700 sub       rsp,28h
00007FFDF74E7704 vzeroupper
00007FFDF74E7707 vmovupd   xmm0,[rcx]
00007FFDF74E770B vmovupd   xmm1,[rdx]
00007FFDF74E770F vdpps     xmm0,xmm1,xmm0,0F1h
00007FFDF74E7715 vxorps    xmm1,xmm1,xmm1
00007FFDF74E7719 vmovss    xmm2,[rel 7FFD`F74E`77A8h]
00007FFDF74E7721 vmovss    xmm1,xmm1,xmm2
00007FFDF74E7725 vxorps    xmm0,xmm0,xmm1
00007FFDF74E7729 vxorps    xmm1,xmm1,xmm1
00007FFDF74E772D vmovss    [rsp+14h],xmm1
00007FFDF74E7733 vmovss    [rsp+14h],xmm0
00007FFDF74E7739 vmovss    [rsp+18h],xmm1
00007FFDF74E773F vmovss    [rsp+1Ch],xmm1
00007FFDF74E7745 vmovss    xmm0,[rsp+14h]
00007FFDF74E774B vmovss    [rsp+18h],xmm1
00007FFDF74E7751 vmovss    [rsp+1Ch],xmm0
00007FFDF74E7757 mov       rax,[rsp+18h]
00007FFDF74E775C mov       [rsp+20h],rax
00007FFDF74E7761 vmovss    [rsp+8],xmm1
00007FFDF74E7767 vmovss    [rsp+0Ch],xmm1
00007FFDF74E776D vmovss    xmm0,[rsp+24h]
00007FFDF74E7773 vmovss    [rsp+8],xmm0
00007FFDF74E7779 vmovss    xmm0,[rsp+20h]
00007FFDF74E777F vmovss    [rsp+0Ch],xmm0
00007FFDF74E7785 mov       rax,[rsp+8]
00007FFDF74E778A add       rsp,28h
00007FFDF74E778E ret

# -----------------------------------------------------------------------------------

# [struct Plane] Boolean op_Equality(Plane, Plane)
00007FFDF74E7890 vzeroupper
00007FFDF74E7893 xchg      ax,ax
00007FFDF74E7895 vmovupd   xmm0,[rdx]
00007FFDF74E7899 vmovupd   xmm1,[rcx]
00007FFDF74E789D vcmpeqps  xmm0,xmm1,xmm0
00007FFDF74E78A2 vmovmskps eax,xmm0
00007FFDF74E78A6 cmp       eax,0Fh
00007FFDF74E78A9 sete      al
00007FFDF74E78AC movzx     eax,al
00007FFDF74E78AF ret

# -----------------------------------------------------------------------------------

# [struct Plane] Boolean op_Inequality(Plane, Plane)
00007FFDF74E78D0 vzeroupper
00007FFDF74E78D3 xchg      ax,ax
00007FFDF74E78D5 vmovupd   xmm0,[rdx]
00007FFDF74E78D9 vmovupd   xmm1,[rcx]
00007FFDF74E78DD vcmpeqps  xmm0,xmm1,xmm0
00007FFDF74E78E2 vmovmskps eax,xmm0
00007FFDF74E78E6 cmp       eax,0Fh
00007FFDF74E78E9 setne     al
00007FFDF74E78EC movzx     eax,al
00007FFDF74E78EF ret

# -----------------------------------------------------------------------------------

# [struct Plane] Line op_BitwiseOr(Plane, Point)
00007FFDF74E7910 sub       rsp,18h
00007FFDF74E7914 vzeroupper
00007FFDF74E7917 xor       eax,eax
00007FFDF74E7919 mov       [rsp],rax
00007FFDF74E791D mov       [rsp+8],rax
00007FFDF74E7922 vmovupd   xmm0,[rdx]
00007FFDF74E7926 vmovupd   xmm1,[r8]
00007FFDF74E792B vshufps   xmm2,xmm1,xmm1,0
00007FFDF74E7930 vmulps    xmm2,xmm0,xmm2
00007FFDF74E7934 vmovapd   [rsp],xmm2
00007FFDF74E7939 lea       rax,[rsp]
00007FFDF74E793D vmovapd   xmm2,[rsp]
00007FFDF74E7942 vxorps    xmm3,xmm3,xmm3
00007FFDF74E7946 vblendps  xmm2,xmm2,xmm3,1
00007FFDF74E794C vmovupd   [rax],xmm2
00007FFDF74E7950 vshufps   xmm2,xmm0,xmm0,78h
00007FFDF74E7955 vmulps    xmm2,xmm2,xmm1
00007FFDF74E7959 vshufps   xmm1,xmm1,xmm1,78h
00007FFDF74E795E vmulps    xmm0,xmm0,xmm1
00007FFDF74E7962 vsubps    xmm0,xmm2,xmm0
00007FFDF74E7966 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E796B vmovapd   xmm1,[rsp]
00007FFDF74E7970 vmovupd   [rcx],xmm1
00007FFDF74E7974 vmovupd   [rcx+10h],xmm0
00007FFDF74E7979 mov       rax,rcx
00007FFDF74E797C add       rsp,18h
00007FFDF74E7980 ret

# -----------------------------------------------------------------------------------

# [struct Plane] Motor op_Multiply(Plane, Plane)
00007FFDF74E79B0 vzeroupper
00007FFDF74E79B3 xchg      ax,ax
00007FFDF74E79B5 vmovupd   xmm0,[rdx]
00007FFDF74E79B9 vmovupd   xmm1,[r8]
00007FFDF74E79BE vshufps   xmm2,xmm0,xmm0,79h
00007FFDF74E79C3 vshufps   xmm3,xmm1,xmm1,9Dh
00007FFDF74E79C8 vmulps    xmm2,xmm2,xmm3
00007FFDF74E79CC vxorps    xmm3,xmm3,xmm3
00007FFDF74E79D0 vmovss    xmm4,[rel 7FFD`F74E`7A48h]
00007FFDF74E79D8 vmovss    xmm3,xmm3,xmm4
00007FFDF74E79DC vshufps   xmm4,xmm0,xmm0,9Eh
00007FFDF74E79E1 vshufps   xmm5,xmm1,xmm1,7Ah
00007FFDF74E79E6 vmulps    xmm4,xmm4,xmm5
00007FFDF74E79EA vxorps    xmm3,xmm3,xmm4
00007FFDF74E79EE vsubps    xmm2,xmm2,xmm3
00007FFDF74E79F2 vshufps   xmm3,xmm0,xmm0,3
00007FFDF74E79F7 vshufps   xmm4,xmm1,xmm1,3
00007FFDF74E79FC vmulps    xmm3,xmm3,xmm4
00007FFDF74E7A00 vaddss    xmm2,xmm2,xmm3
00007FFDF74E7A04 vshufps   xmm3,xmm0,xmm0,0
00007FFDF74E7A09 vmulps    xmm3,xmm3,xmm1
00007FFDF74E7A0D vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E7A12 vmulps    xmm0,xmm0,xmm1
00007FFDF74E7A16 vsubps    xmm3,xmm3,xmm0
00007FFDF74E7A1A vmovaps   xmm0,xmm2
00007FFDF74E7A1E vmovupd   [rcx],xmm0
00007FFDF74E7A22 vmovupd   [rcx+10h],xmm3
00007FFDF74E7A27 mov       rax,rcx
00007FFDF74E7A2A ret

# -----------------------------------------------------------------------------------

# [struct Plane] Motor op_Multiply(Plane, Point)
00007FFDF74E7A60 sub       rsp,18h
00007FFDF74E7A64 vzeroupper
00007FFDF74E7A67 xor       eax,eax
00007FFDF74E7A69 mov       [rsp],rax
00007FFDF74E7A6D mov       [rsp+8],rax
00007FFDF74E7A72 vmovupd   xmm0,[rdx]
00007FFDF74E7A76 vmovupd   xmm1,[r8]
00007FFDF74E7A7B vshufps   xmm2,xmm1,xmm1,0
00007FFDF74E7A80 vmulps    xmm2,xmm0,xmm2
00007FFDF74E7A84 vmovapd   [rsp],xmm2
00007FFDF74E7A89 lea       rax,[rsp]
00007FFDF74E7A8D vmovapd   xmm2,[rsp]
00007FFDF74E7A92 vxorps    xmm3,xmm3,xmm3
00007FFDF74E7A96 vblendps  xmm2,xmm2,xmm3,1
00007FFDF74E7A9C vmovupd   [rax],xmm2
00007FFDF74E7AA0 vshufps   xmm2,xmm0,xmm0,9Ch
00007FFDF74E7AA5 vshufps   xmm3,xmm1,xmm1,78h
00007FFDF74E7AAA vmulps    xmm2,xmm2,xmm3
00007FFDF74E7AAE vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E7AB3 vshufps   xmm4,xmm1,xmm1,9Ch
00007FFDF74E7AB8 vmulps    xmm3,xmm3,xmm4
00007FFDF74E7ABC vsubps    xmm2,xmm2,xmm3
00007FFDF74E7AC0 vdpps     xmm0,xmm0,xmm1,0F1h
00007FFDF74E7AC6 vaddps    xmm2,xmm2,xmm0
00007FFDF74E7ACA vmovapd   xmm0,[rsp]
00007FFDF74E7ACF vmovupd   [rcx],xmm0
00007FFDF74E7AD3 vmovupd   [rcx+10h],xmm2
00007FFDF74E7AD8 mov       rax,rcx
00007FFDF74E7ADB add       rsp,18h
00007FFDF74E7ADF ret

# -----------------------------------------------------------------------------------

# [struct Plane] Motor op_Division(Plane, Plane)
00007FFDF74E7B10 vzeroupper
00007FFDF74E7B13 xchg      ax,ax
00007FFDF74E7B15 vmovupd   xmm0,[r8]
00007FFDF74E7B1A vmovaps   xmm1,xmm0
00007FFDF74E7B1E vmovaps   xmm2,xmm0
00007FFDF74E7B22 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74E7B28 vrsqrtps  xmm2,xmm1
00007FFDF74E7B2C vmulps    xmm3,xmm2,xmm2
00007FFDF74E7B30 vmulps    xmm3,xmm1,xmm3
00007FFDF74E7B34 vmovss    xmm1,[rel 7FFD`F74E`7C00h]
00007FFDF74E7B3C vbroadcastss xmm1,xmm1
00007FFDF74E7B41 vsubps    xmm1,xmm1,xmm3
00007FFDF74E7B45 vmovss    xmm3,[rel 7FFD`F74E`7C04h]
00007FFDF74E7B4D vbroadcastss xmm3,xmm3
00007FFDF74E7B52 vmulps    xmm2,xmm3,xmm2
00007FFDF74E7B56 vmulps    xmm1,xmm2,xmm1
00007FFDF74E7B5A vmulps    xmm0,xmm1,xmm0
00007FFDF74E7B5E vmulps    xmm0,xmm1,xmm0
00007FFDF74E7B62 vmovaps   xmm1,xmm0
00007FFDF74E7B66 vmovupd   xmm2,[rdx]
00007FFDF74E7B6A vmovaps   xmm3,xmm2
00007FFDF74E7B6E vshufps   xmm2,xmm2,xmm2,79h
00007FFDF74E7B73 vshufps   xmm0,xmm0,xmm0,9Dh
00007FFDF74E7B78 vmulps    xmm0,xmm2,xmm0
00007FFDF74E7B7C vxorps    xmm2,xmm2,xmm2
00007FFDF74E7B80 vmovss    xmm4,[rel 7FFD`F74E`7C08h]
00007FFDF74E7B88 vmovss    xmm2,xmm2,xmm4
00007FFDF74E7B8C vshufps   xmm4,xmm3,xmm3,9Eh
00007FFDF74E7B91 vshufps   xmm5,xmm1,xmm1,7Ah
00007FFDF74E7B96 vmulps    xmm4,xmm4,xmm5
00007FFDF74E7B9A vxorps    xmm2,xmm2,xmm4
00007FFDF74E7B9E vsubps    xmm0,xmm0,xmm2
00007FFDF74E7BA2 vshufps   xmm2,xmm3,xmm3,3
00007FFDF74E7BA7 vshufps   xmm4,xmm1,xmm1,3
00007FFDF74E7BAC vmulps    xmm2,xmm2,xmm4
00007FFDF74E7BB0 vaddss    xmm0,xmm0,xmm2
00007FFDF74E7BB4 vshufps   xmm2,xmm3,xmm3,0
00007FFDF74E7BB9 vmulps    xmm2,xmm2,xmm1
00007FFDF74E7BBD vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E7BC2 vmulps    xmm1,xmm3,xmm1
00007FFDF74E7BC6 vsubps    xmm2,xmm2,xmm1
00007FFDF74E7BCA vmovupd   [rcx],xmm0
00007FFDF74E7BCE vmovupd   [rcx+10h],xmm2
00007FFDF74E7BD3 mov       rax,rcx
00007FFDF74E7BD6 ret

# -----------------------------------------------------------------------------------

# [struct Point] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E7C20 vzeroupper
00007FFDF74E7C23 xchg      ax,ax
00007FFDF74E7C25 vmovupd   xmm0,[rcx]
00007FFDF74E7C29 vextractps eax,xmm0,1
00007FFDF74E7C2F vmovd     xmm0,eax
00007FFDF74E7C33 vmovss    [rdx],xmm0
00007FFDF74E7C37 vmovupd   xmm0,[rcx]
00007FFDF74E7C3B vextractps eax,xmm0,2
00007FFDF74E7C41 vmovd     xmm0,eax
00007FFDF74E7C45 vmovss    [r8],xmm0
00007FFDF74E7C4A vmovupd   xmm0,[rcx]
00007FFDF74E7C4E vextractps eax,xmm0,3
00007FFDF74E7C54 vmovd     xmm0,eax
00007FFDF74E7C58 vmovss    [r9],xmm0
00007FFDF74E7C5D ret

# -----------------------------------------------------------------------------------

# [struct Point] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E7C80 vzeroupper
00007FFDF74E7C83 xchg      ax,ax
00007FFDF74E7C85 vmovupd   xmm0,[rcx]
00007FFDF74E7C89 vextractps eax,xmm0,1
00007FFDF74E7C8F vmovd     xmm0,eax
00007FFDF74E7C93 vmovss    [rdx],xmm0
00007FFDF74E7C97 vmovupd   xmm0,[rcx]
00007FFDF74E7C9B vextractps eax,xmm0,2
00007FFDF74E7CA1 vmovd     xmm0,eax
00007FFDF74E7CA5 vmovss    [r8],xmm0
00007FFDF74E7CAA vmovupd   xmm0,[rcx]
00007FFDF74E7CAE vextractps eax,xmm0,3
00007FFDF74E7CB4 vmovd     xmm0,eax
00007FFDF74E7CB8 vmovss    [r9],xmm0
00007FFDF74E7CBD vmovss    xmm0,[rcx]
00007FFDF74E7CC1 mov       rax,[rsp+28h]
00007FFDF74E7CC6 vmovss    [rax],xmm0
00007FFDF74E7CCA ret

# -----------------------------------------------------------------------------------

# [struct Point] Void Store(Single*)
00007FFDF74E7CF0 vzeroupper
00007FFDF74E7CF3 xchg      ax,ax
00007FFDF74E7CF5 vmovupd   xmm0,[rcx]
00007FFDF74E7CF9 vmovups   [rdx],xmm0
00007FFDF74E7CFD ret

# -----------------------------------------------------------------------------------

# [struct Point] Void Store(System.Span`1[System.Single])
00007FFDF74E7D10 push      rsi
00007FFDF74E7D11 sub       rsp,30h
00007FFDF74E7D15 vzeroupper
00007FFDF74E7D18 xor       eax,eax
00007FFDF74E7D1A mov       [rsp+28h],rax
00007FFDF74E7D1F mov       rax,[rdx]
00007FFDF74E7D22 mov       edx,[rdx+8]
00007FFDF74E7D25 vmovupd   xmm0,[rcx]
00007FFDF74E7D29 xor       ecx,ecx
00007FFDF74E7D2B mov       [rsp+28h],rcx
00007FFDF74E7D30 cmp       edx,4
00007FFDF74E7D33 jl        short 0000`7FFD`F74E`7D54h
00007FFDF74E7D35 xor       ecx,ecx
00007FFDF74E7D37 test      edx,edx
00007FFDF74E7D39 je        short 0000`7FFD`F74E`7D3Eh
00007FFDF74E7D3B mov       rcx,rax
00007FFDF74E7D3E mov       [rsp+28h],rcx
00007FFDF74E7D43 vmovups   [rcx],xmm0
00007FFDF74E7D47 xor       ecx,ecx
00007FFDF74E7D49 mov       [rsp+28h],rcx
00007FFDF74E7D4E add       rsp,30h
00007FFDF74E7D52 pop       rsi
00007FFDF74E7D53 ret
00007FFDF74E7D54 mov       rcx,7FFD`F72F`D4C0h
00007FFDF74E7D5E call      0000`7FFE`56D7`7710h
00007FFDF74E7D63 mov       rsi,rax
00007FFDF74E7D66 mov       ecx,25h
00007FFDF74E7D6B mov       rdx,7FFD`F731`9EA0h
00007FFDF74E7D75 call      0000`7FFE`56EA`03E0h
00007FFDF74E7D7A mov       rdx,rax
00007FFDF74E7D7D mov       rcx,rsi
00007FFDF74E7D80 call      0000`7FFD`F725`D238h
00007FFDF74E7D85 mov       rcx,rsi
00007FFDF74E7D88 call      0000`7FFE`56D3`B3A0h
00007FFDF74E7D8D int3

# -----------------------------------------------------------------------------------

# [struct Point] Point Normalized()
00007FFDF74E7DB0 vzeroupper
00007FFDF74E7DB3 xchg      ax,ax
00007FFDF74E7DB5 vmovupd   xmm0,[rcx]
00007FFDF74E7DB9 vmovaps   xmm1,xmm0
00007FFDF74E7DBD vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E7DC2 vrcpps    xmm2,xmm1
00007FFDF74E7DC6 vmulps    xmm1,xmm1,xmm2
00007FFDF74E7DCA vmovss    xmm3,[rel 7FFD`F74E`7DF8h]
00007FFDF74E7DD2 vbroadcastss xmm3,xmm3
00007FFDF74E7DD7 vsubps    xmm1,xmm3,xmm1
00007FFDF74E7DDB vmulps    xmm1,xmm2,xmm1
00007FFDF74E7DDF vmulps    xmm0,xmm0,xmm1
00007FFDF74E7DE3 vmovupd   [rdx],xmm0
00007FFDF74E7DE7 mov       rax,rdx
00007FFDF74E7DEA ret

# -----------------------------------------------------------------------------------

# [struct Point] Point Inverse()
00007FFDF74E7E10 vzeroupper
00007FFDF74E7E13 xchg      ax,ax
00007FFDF74E7E15 vmovupd   xmm0,[rcx]
00007FFDF74E7E19 vshufps   xmm1,xmm0,xmm0,0
00007FFDF74E7E1E vrcpps    xmm2,xmm1
00007FFDF74E7E22 vmulps    xmm1,xmm1,xmm2
00007FFDF74E7E26 vmovss    xmm3,[rel 7FFD`F74E`7E58h]
00007FFDF74E7E2E vbroadcastss xmm3,xmm3
00007FFDF74E7E33 vsubps    xmm1,xmm3,xmm1
00007FFDF74E7E37 vmulps    xmm1,xmm2,xmm1
00007FFDF74E7E3B vmulps    xmm0,xmm1,xmm0
00007FFDF74E7E3F vmulps    xmm0,xmm1,xmm0
00007FFDF74E7E43 vmovupd   [rdx],xmm0
00007FFDF74E7E47 mov       rax,rdx
00007FFDF74E7E4A ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_Addition(Point, Point)
00007FFDF74E7E70 vzeroupper
00007FFDF74E7E73 xchg      ax,ax
00007FFDF74E7E75 vmovupd   xmm0,[rdx]
00007FFDF74E7E79 vmovupd   xmm1,[r8]
00007FFDF74E7E7E vaddps    xmm0,xmm0,xmm1
00007FFDF74E7E82 vmovupd   [rcx],xmm0
00007FFDF74E7E86 mov       rax,rcx
00007FFDF74E7E89 ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_Subtraction(Point, Point)
00007FFDF74E7EA0 vzeroupper
00007FFDF74E7EA3 xchg      ax,ax
00007FFDF74E7EA5 vmovupd   xmm0,[rdx]
00007FFDF74E7EA9 vmovupd   xmm1,[r8]
00007FFDF74E7EAE vsubps    xmm0,xmm0,xmm1
00007FFDF74E7EB2 vmovupd   [rcx],xmm0
00007FFDF74E7EB6 mov       rax,rcx
00007FFDF74E7EB9 ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_Multiply(Point, Single)
00007FFDF74E7ED0 vzeroupper
00007FFDF74E7ED3 xchg      ax,ax
00007FFDF74E7ED5 vmovupd   xmm0,[rdx]
00007FFDF74E7ED9 vbroadcastss xmm1,xmm2
00007FFDF74E7EDE vmulps    xmm0,xmm0,xmm1
00007FFDF74E7EE2 vmovupd   [rcx],xmm0
00007FFDF74E7EE6 mov       rax,rcx
00007FFDF74E7EE9 ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_Multiply(Single, Point)
00007FFDF74E7F00 vzeroupper
00007FFDF74E7F03 xchg      ax,ax
00007FFDF74E7F05 vmovupd   xmm0,[r8]
00007FFDF74E7F0A vbroadcastss xmm1,xmm1
00007FFDF74E7F0F vmulps    xmm0,xmm0,xmm1
00007FFDF74E7F13 vmovupd   [rcx],xmm0
00007FFDF74E7F17 mov       rax,rcx
00007FFDF74E7F1A ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_Division(Point, Single)
00007FFDF74E7F30 vzeroupper
00007FFDF74E7F33 xchg      ax,ax
00007FFDF74E7F35 vmovupd   xmm0,[rdx]
00007FFDF74E7F39 vbroadcastss xmm1,xmm2
00007FFDF74E7F3E vrcpps    xmm2,xmm1
00007FFDF74E7F42 vmulps    xmm1,xmm1,xmm2
00007FFDF74E7F46 vmovss    xmm3,[rel 7FFD`F74E`7F78h]
00007FFDF74E7F4E vbroadcastss xmm3,xmm3
00007FFDF74E7F53 vsubps    xmm1,xmm3,xmm1
00007FFDF74E7F57 vmulps    xmm1,xmm2,xmm1
00007FFDF74E7F5B vmulps    xmm0,xmm0,xmm1
00007FFDF74E7F5F vmovupd   [rcx],xmm0
00007FFDF74E7F63 mov       rax,rcx
00007FFDF74E7F66 ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_UnaryNegation(Point)
00007FFDF74E7F90 vzeroupper
00007FFDF74E7F93 xchg      ax,ax
00007FFDF74E7F95 vmovupd   xmm0,[rdx]
00007FFDF74E7F99 vxorps    xmm1,xmm1,xmm1
00007FFDF74E7F9D vmovss    xmm2,[rel 7FFD`F74E`7FE0h]
00007FFDF74E7FA5 vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74E7FAB vmovss    xmm2,[rel 7FFD`F74E`7FE4h]
00007FFDF74E7FB3 vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74E7FB9 vmovss    xmm2,[rel 7FFD`F74E`7FE8h]
00007FFDF74E7FC1 vinsertps xmm1,xmm1,xmm2,30h
00007FFDF74E7FC7 vxorps    xmm0,xmm0,xmm1
00007FFDF74E7FCB vmovupd   [rcx],xmm0
00007FFDF74E7FCF mov       rax,rcx
00007FFDF74E7FD2 ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_OnesComplement(Point)
00007FFDF74E8000 vzeroupper
00007FFDF74E8003 xchg      ax,ax
00007FFDF74E8005 vmovss    xmm0,[rel 7FFD`F74E`8028h]
00007FFDF74E800D vbroadcastss xmm0,xmm0
00007FFDF74E8012 vmovupd   xmm1,[rdx]
00007FFDF74E8016 vxorps    xmm0,xmm1,xmm0
00007FFDF74E801A vmovupd   [rcx],xmm0
00007FFDF74E801E mov       rax,rcx
00007FFDF74E8021 ret

# -----------------------------------------------------------------------------------

# [struct Point] Translator op_Multiply(Point, Point)
00007FFDF74E8040 vzeroupper
00007FFDF74E8043 xchg      ax,ax
00007FFDF74E8045 vmovupd   xmm0,[rdx]
00007FFDF74E8049 vmovupd   xmm1,[r8]
00007FFDF74E804E vshufps   xmm2,xmm0,xmm0,0
00007FFDF74E8053 vmulps    xmm2,xmm2,xmm1
00007FFDF74E8057 vmovss    xmm3,[rel 7FFD`F74E`80F0h]
00007FFDF74E805F vmovss    xmm4,[rel 7FFD`F74E`80F4h]
00007FFDF74E8067 vinsertps xmm3,xmm3,xmm4,10h
00007FFDF74E806D vmovss    xmm4,[rel 7FFD`F74E`80F8h]
00007FFDF74E8075 vinsertps xmm3,xmm3,xmm4,20h
00007FFDF74E807B vmovss    xmm4,[rel 7FFD`F74E`80FCh]
00007FFDF74E8083 vinsertps xmm3,xmm3,xmm4,30h
00007FFDF74E8089 vmulps    xmm2,xmm2,xmm3
00007FFDF74E808D vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E8092 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8096 vaddps    xmm2,xmm2,xmm0
00007FFDF74E809A vmovsldup xmm0,xmm2
00007FFDF74E809E vmovlhps  xmm0,xmm0,xmm0
00007FFDF74E80A2 vrcpps    xmm1,xmm0
00007FFDF74E80A6 vmulps    xmm0,xmm0,xmm1
00007FFDF74E80AA vmovss    xmm3,[rel 7FFD`F74E`8100h]
00007FFDF74E80B2 vbroadcastss xmm3,xmm3
00007FFDF74E80B7 vsubps    xmm0,xmm3,xmm0
00007FFDF74E80BB vmulps    xmm0,xmm1,xmm0
00007FFDF74E80BF vmulps    xmm2,xmm2,xmm0
00007FFDF74E80C3 vxorps    xmm0,xmm0,xmm0
00007FFDF74E80C7 vblendps  xmm0,xmm2,xmm0,1
00007FFDF74E80CD vmovupd   [rcx],xmm0
00007FFDF74E80D1 mov       rax,rcx
00007FFDF74E80D4 ret

# -----------------------------------------------------------------------------------

# [struct Point] Motor op_Multiply(Point, Plane)
00007FFDF74E8120 sub       rsp,18h
00007FFDF74E8124 vzeroupper
00007FFDF74E8127 xor       eax,eax
00007FFDF74E8129 mov       [rsp],rax
00007FFDF74E812D mov       [rsp+8],rax
00007FFDF74E8132 vmovupd   xmm0,[r8]
00007FFDF74E8137 vmovupd   xmm1,[rdx]
00007FFDF74E813B vshufps   xmm2,xmm1,xmm1,0
00007FFDF74E8140 vmulps    xmm2,xmm0,xmm2
00007FFDF74E8144 vmovapd   [rsp],xmm2
00007FFDF74E8149 lea       rax,[rsp]
00007FFDF74E814D vmovapd   xmm2,[rsp]
00007FFDF74E8152 vxorps    xmm3,xmm3,xmm3
00007FFDF74E8156 vblendps  xmm2,xmm2,xmm3,1
00007FFDF74E815C vmovupd   [rax],xmm2
00007FFDF74E8160 vshufps   xmm2,xmm0,xmm0,9Ch
00007FFDF74E8165 vshufps   xmm3,xmm1,xmm1,78h
00007FFDF74E816A vmulps    xmm2,xmm2,xmm3
00007FFDF74E816E vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E8173 vshufps   xmm4,xmm1,xmm1,9Ch
00007FFDF74E8178 vmulps    xmm3,xmm3,xmm4
00007FFDF74E817C vsubps    xmm2,xmm2,xmm3
00007FFDF74E8180 vdpps     xmm0,xmm0,xmm1,0F1h
00007FFDF74E8186 vxorps    xmm1,xmm1,xmm1
00007FFDF74E818A vmovss    xmm3,[rel 7FFD`F74E`81D0h]
00007FFDF74E8192 vmovss    xmm1,xmm1,xmm3
00007FFDF74E8196 vxorps    xmm0,xmm0,xmm1
00007FFDF74E819A vaddps    xmm2,xmm2,xmm0
00007FFDF74E819E vmovapd   xmm0,[rsp]
00007FFDF74E81A3 vmovupd   [rcx],xmm0
00007FFDF74E81A7 vmovupd   [rcx+10h],xmm2
00007FFDF74E81AC mov       rax,rcx
00007FFDF74E81AF add       rsp,18h
00007FFDF74E81B3 ret

# -----------------------------------------------------------------------------------

# [struct Point] Translator op_Division(Point, Point)
00007FFDF74E81F0 vzeroupper
00007FFDF74E81F3 xchg      ax,ax
00007FFDF74E81F5 vmovupd   xmm0,[r8]
00007FFDF74E81FA vshufps   xmm1,xmm0,xmm0,0
00007FFDF74E81FF vrcpps    xmm2,xmm1
00007FFDF74E8203 vmulps    xmm1,xmm1,xmm2
00007FFDF74E8207 vmovss    xmm3,[rel 7FFD`F74E`82D0h]
00007FFDF74E820F vbroadcastss xmm3,xmm3
00007FFDF74E8214 vsubps    xmm1,xmm3,xmm1
00007FFDF74E8218 vmulps    xmm1,xmm2,xmm1
00007FFDF74E821C vmulps    xmm0,xmm1,xmm0
00007FFDF74E8220 vmulps    xmm0,xmm1,xmm0
00007FFDF74E8224 vmovupd   xmm1,[rdx]
00007FFDF74E8228 vshufps   xmm2,xmm1,xmm1,0
00007FFDF74E822D vmulps    xmm2,xmm2,xmm0
00007FFDF74E8231 vmovss    xmm3,[rel 7FFD`F74E`82D4h]
00007FFDF74E8239 vmovss    xmm4,[rel 7FFD`F74E`82D8h]
00007FFDF74E8241 vinsertps xmm3,xmm3,xmm4,10h
00007FFDF74E8247 vmovss    xmm4,[rel 7FFD`F74E`82DCh]
00007FFDF74E824F vinsertps xmm3,xmm3,xmm4,20h
00007FFDF74E8255 vmovss    xmm4,[rel 7FFD`F74E`82E0h]
00007FFDF74E825D vinsertps xmm3,xmm3,xmm4,30h
00007FFDF74E8263 vmulps    xmm2,xmm2,xmm3
00007FFDF74E8267 vshufps   xmm0,xmm0,xmm0,0
00007FFDF74E826C vmulps    xmm0,xmm1,xmm0
00007FFDF74E8270 vaddps    xmm2,xmm2,xmm0
00007FFDF74E8274 vmovsldup xmm0,xmm2
00007FFDF74E8278 vmovlhps  xmm0,xmm0,xmm0
00007FFDF74E827C vrcpps    xmm1,xmm0
00007FFDF74E8280 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8284 vmovss    xmm3,[rel 7FFD`F74E`82E4h]
00007FFDF74E828C vbroadcastss xmm3,xmm3
00007FFDF74E8291 vsubps    xmm0,xmm3,xmm0
00007FFDF74E8295 vmulps    xmm0,xmm1,xmm0
00007FFDF74E8299 vmulps    xmm2,xmm2,xmm0
00007FFDF74E829D vxorps    xmm0,xmm0,xmm0
00007FFDF74E82A1 vblendps  xmm0,xmm2,xmm0,1
00007FFDF74E82A7 vmovupd   [rcx],xmm0
00007FFDF74E82AB mov       rax,rcx
00007FFDF74E82AE ret

# -----------------------------------------------------------------------------------

# [struct Point] Line op_BitwiseOr(Point, Plane)
00007FFDF74E8300 sub       rsp,18h
00007FFDF74E8304 vzeroupper
00007FFDF74E8307 vmovupd   xmm0,[r8]
00007FFDF74E830C vmovupd   xmm1,[rdx]
00007FFDF74E8310 vxorps    xmm2,xmm2,xmm2
00007FFDF74E8314 vmovapd   [rsp],xmm2
00007FFDF74E8319 vshufps   xmm2,xmm1,xmm1,0
00007FFDF74E831E vmulps    xmm2,xmm0,xmm2
00007FFDF74E8322 vmovapd   [rsp],xmm2
00007FFDF74E8327 lea       rax,[rsp]
00007FFDF74E832B vmovapd   xmm2,[rsp]
00007FFDF74E8330 vxorps    xmm3,xmm3,xmm3
00007FFDF74E8334 vblendps  xmm2,xmm2,xmm3,1
00007FFDF74E833A vmovupd   [rax],xmm2
00007FFDF74E833E vshufps   xmm2,xmm0,xmm0,78h
00007FFDF74E8343 vmulps    xmm2,xmm2,xmm1
00007FFDF74E8347 vshufps   xmm1,xmm1,xmm1,78h
00007FFDF74E834C vmulps    xmm0,xmm0,xmm1
00007FFDF74E8350 vsubps    xmm0,xmm2,xmm0
00007FFDF74E8354 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E8359 vmovapd   xmm1,[rsp]
00007FFDF74E835E vmovupd   [rcx],xmm1
00007FFDF74E8362 vmovupd   [rcx+10h],xmm0
00007FFDF74E8367 mov       rax,rcx
00007FFDF74E836A add       rsp,18h
00007FFDF74E836E ret

# -----------------------------------------------------------------------------------

# [struct Point] Plane op_BitwiseOr(Point, Line)
00007FFDF74E83A0 vzeroupper
00007FFDF74E83A3 xchg      ax,ax
00007FFDF74E83A5 vmovupd   xmm0,[rdx]
00007FFDF74E83A9 vmovupd   xmm1,[r8]
00007FFDF74E83AE vmulps    xmm2,xmm0,xmm1
00007FFDF74E83B2 vmovshdup xmm3,xmm2
00007FFDF74E83B6 vaddps    xmm3,xmm3,xmm2
00007FFDF74E83BA vunpcklps xmm2,xmm2,xmm2
00007FFDF74E83BE vaddps    xmm2,xmm3,xmm2
00007FFDF74E83C2 vmovhlps  xmm2,xmm2,xmm2
00007FFDF74E83C6 vshufps   xmm0,xmm0,xmm0,0
00007FFDF74E83CB vmulps    xmm0,xmm0,xmm1
00007FFDF74E83CF vxorps    xmm1,xmm1,xmm1
00007FFDF74E83D3 vmovss    xmm3,[rel 7FFD`F74E`8420h]
00007FFDF74E83DB vinsertps xmm1,xmm1,xmm3,10h
00007FFDF74E83E1 vmovss    xmm3,[rel 7FFD`F74E`8424h]
00007FFDF74E83E9 vinsertps xmm1,xmm1,xmm3,20h
00007FFDF74E83EF vmovss    xmm3,[rel 7FFD`F74E`8428h]
00007FFDF74E83F7 vinsertps xmm1,xmm1,xmm3,30h
00007FFDF74E83FD vxorps    xmm0,xmm0,xmm1
00007FFDF74E8401 vblendps  xmm0,xmm0,xmm2,1
00007FFDF74E8407 vmovupd   [rcx],xmm0
00007FFDF74E840B mov       rax,rcx
00007FFDF74E840E ret

# -----------------------------------------------------------------------------------

# [struct Point] Single op_BitwiseOr(Point, Point)
00007FFDF74E8440 vzeroupper
00007FFDF74E8443 xchg      ax,ax
00007FFDF74E8445 vmovupd   xmm0,[rcx]
00007FFDF74E8449 vmovupd   xmm1,[rdx]
00007FFDF74E844D vxorps    xmm2,xmm2,xmm2
00007FFDF74E8451 vmovss    xmm3,[rel 7FFD`F74E`8470h]
00007FFDF74E8459 vmovss    xmm2,xmm2,xmm3
00007FFDF74E845D vmulss    xmm0,xmm0,xmm1
00007FFDF74E8461 vmulps    xmm0,xmm2,xmm0
00007FFDF74E8465 ret

# -----------------------------------------------------------------------------------

# [struct Point] Plane op_LogicalNot(Point)
00007FFDF74E8490 vzeroupper
00007FFDF74E8493 xchg      ax,ax
00007FFDF74E8495 vmovupd   xmm0,[rdx]
00007FFDF74E8499 vmovupd   [rcx],xmm0
00007FFDF74E849D mov       rax,rcx
00007FFDF74E84A0 ret

# -----------------------------------------------------------------------------------

# [struct Point] Line op_BitwiseAnd(Point, Point)
00007FFDF74E84C0 vzeroupper
00007FFDF74E84C3 xchg      ax,ax
00007FFDF74E84C5 vmovupd   xmm0,[rdx]
00007FFDF74E84C9 vmovupd   xmm1,[r8]
00007FFDF74E84CE vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74E84D3 vmulps    xmm2,xmm0,xmm2
00007FFDF74E84D7 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E84DC vmulps    xmm3,xmm3,xmm1
00007FFDF74E84E0 vsubps    xmm2,xmm2,xmm3
00007FFDF74E84E4 vshufps   xmm2,xmm2,xmm2,78h
00007FFDF74E84E9 vshufps   xmm3,xmm0,xmm0,0
00007FFDF74E84EE vmulps    xmm3,xmm3,xmm1
00007FFDF74E84F2 vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E84F7 vmulps    xmm0,xmm0,xmm1
00007FFDF74E84FB vsubps    xmm3,xmm3,xmm0
00007FFDF74E84FF vmovupd   [rcx],xmm3
00007FFDF74E8503 vmovupd   [rcx+10h],xmm2
00007FFDF74E8508 mov       rax,rcx
00007FFDF74E850B ret

# -----------------------------------------------------------------------------------

# [struct Point] Plane op_BitwiseAnd(Point, Line)
00007FFDF74E8530 sub       rsp,18h
00007FFDF74E8534 vzeroupper
00007FFDF74E8537 vmovaps   [rsp],xmm6
00007FFDF74E853C vmovupd   xmm0,[rdx]
00007FFDF74E8540 vmovaps   xmm1,xmm0
00007FFDF74E8544 vmovupd   xmm2,[r8+10h]
00007FFDF74E854A vmovupd   xmm3,[r8]
00007FFDF74E854F vmovaps   xmm4,xmm2
00007FFDF74E8553 vmovaps   xmm5,xmm1
00007FFDF74E8557 vshufps   xmm0,xmm0,xmm0,1
00007FFDF74E855C vmulps    xmm0,xmm0,xmm2
00007FFDF74E8560 vxorps    xmm2,xmm2,xmm2
00007FFDF74E8564 vmovss    xmm6,[rel 7FFD`F74E`85E8h]
00007FFDF74E856C vinsertps xmm2,xmm2,xmm6,10h
00007FFDF74E8572 vmovss    xmm6,[rel 7FFD`F74E`85ECh]
00007FFDF74E857A vinsertps xmm2,xmm2,xmm6,20h
00007FFDF74E8580 vmovss    xmm6,[rel 7FFD`F74E`85F0h]
00007FFDF74E8588 vinsertps xmm2,xmm2,xmm6,30h
00007FFDF74E858E vmulps    xmm0,xmm0,xmm2
00007FFDF74E8592 vdpps     xmm2,xmm5,xmm4,0E1h
00007FFDF74E8598 vaddss    xmm0,xmm0,xmm2
00007FFDF74E859C vshufps   xmm2,xmm3,xmm3,78h
00007FFDF74E85A1 vmulps    xmm2,xmm1,xmm2
00007FFDF74E85A5 vshufps   xmm1,xmm1,xmm1,78h
00007FFDF74E85AA vmulps    xmm1,xmm1,xmm3
00007FFDF74E85AE vsubps    xmm1,xmm2,xmm1
00007FFDF74E85B2 vshufps   xmm1,xmm1,xmm1,78h
00007FFDF74E85B7 vaddps    xmm0,xmm1,xmm0
00007FFDF74E85BB vmovupd   [rcx],xmm0
00007FFDF74E85BF mov       rax,rcx
00007FFDF74E85C2 vmovaps   xmm6,[rsp]
00007FFDF74E85C7 add       rsp,18h
00007FFDF74E85CB ret

# -----------------------------------------------------------------------------------

# [struct Point] Plane op_BitwiseAnd(Point, Branch)
00007FFDF74E8610 vzeroupper
00007FFDF74E8613 xchg      ax,ax
00007FFDF74E8615 vmovupd   xmm0,[rdx]
00007FFDF74E8619 vmovupd   xmm1,[r8]
00007FFDF74E861E vshufps   xmm2,xmm1,xmm1,78h
00007FFDF74E8623 vmulps    xmm2,xmm0,xmm2
00007FFDF74E8627 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E862C vmulps    xmm0,xmm0,xmm1
00007FFDF74E8630 vsubps    xmm0,xmm2,xmm0
00007FFDF74E8634 vshufps   xmm0,xmm0,xmm0,78h
00007FFDF74E8639 vmovupd   [rcx],xmm0
00007FFDF74E863D mov       rax,rcx
00007FFDF74E8640 ret

# -----------------------------------------------------------------------------------

# [struct Point] Plane op_BitwiseAnd(Point, IdealLine)
00007FFDF74E8660 vzeroupper
00007FFDF74E8663 xchg      ax,ax
00007FFDF74E8665 vmovupd   xmm0,[rdx]
00007FFDF74E8669 vmovaps   xmm1,xmm0
00007FFDF74E866D vmovupd   xmm2,[r8]
00007FFDF74E8672 vmovaps   xmm3,xmm2
00007FFDF74E8676 vshufps   xmm0,xmm0,xmm0,1
00007FFDF74E867B vmulps    xmm0,xmm0,xmm2
00007FFDF74E867F vxorps    xmm2,xmm2,xmm2
00007FFDF74E8683 vmovss    xmm4,[rel 7FFD`F74E`86D0h]
00007FFDF74E868B vinsertps xmm2,xmm2,xmm4,10h
00007FFDF74E8691 vmovss    xmm4,[rel 7FFD`F74E`86D4h]
00007FFDF74E8699 vinsertps xmm2,xmm2,xmm4,20h
00007FFDF74E869F vmovss    xmm4,[rel 7FFD`F74E`86D8h]
00007FFDF74E86A7 vinsertps xmm2,xmm2,xmm4,30h
00007FFDF74E86AD vmulps    xmm0,xmm0,xmm2
00007FFDF74E86B1 vdpps     xmm1,xmm1,xmm3,0E1h
00007FFDF74E86B7 vaddss    xmm0,xmm0,xmm1
00007FFDF74E86BB vmovupd   [rcx],xmm0
00007FFDF74E86BF mov       rax,rcx
00007FFDF74E86C2 ret

# -----------------------------------------------------------------------------------

# [struct Point] Dual op_BitwiseAnd(Point, Plane)
00007FFDF74E86F0 sub       rsp,28h
00007FFDF74E86F4 vzeroupper
00007FFDF74E86F7 vmovupd   xmm0,[rcx]
00007FFDF74E86FB vmovupd   xmm1,[rdx]
00007FFDF74E86FF vdpps     xmm0,xmm0,xmm1,0F1h
00007FFDF74E8705 vxorps    xmm1,xmm1,xmm1
00007FFDF74E8709 vmovss    [rsp+14h],xmm1
00007FFDF74E870F vmovss    [rsp+14h],xmm0
00007FFDF74E8715 vmovss    [rsp+18h],xmm1
00007FFDF74E871B vmovss    [rsp+1Ch],xmm1
00007FFDF74E8721 vmovss    xmm0,[rsp+14h]
00007FFDF74E8727 vmovss    [rsp+18h],xmm1
00007FFDF74E872D vmovss    [rsp+1Ch],xmm0
00007FFDF74E8733 mov       rax,[rsp+18h]
00007FFDF74E8738 mov       [rsp+20h],rax
00007FFDF74E873D vmovss    [rsp+8],xmm1
00007FFDF74E8743 vmovss    [rsp+0Ch],xmm1
00007FFDF74E8749 vmovss    xmm0,[rsp+24h]
00007FFDF74E874F vmovss    [rsp+8],xmm0
00007FFDF74E8755 vmovss    xmm0,[rsp+20h]
00007FFDF74E875B vmovss    [rsp+0Ch],xmm0
00007FFDF74E8761 mov       rax,[rsp+8]
00007FFDF74E8766 add       rsp,28h
00007FFDF74E876A ret

# -----------------------------------------------------------------------------------

# [struct Point] Dual op_ExclusiveOr(Point, Plane)
00007FFDF74E8790 sub       rsp,18h
00007FFDF74E8794 vzeroupper
00007FFDF74E8797 xor       eax,eax
00007FFDF74E8799 mov       [rsp+0Ch],eax
00007FFDF74E879D vmovupd   xmm0,[rdx]
00007FFDF74E87A1 vmovupd   xmm1,[rcx]
00007FFDF74E87A5 vdpps     xmm0,xmm0,xmm1,0F1h
00007FFDF74E87AB vxorps    xmm1,xmm1,xmm1
00007FFDF74E87AF vmovss    xmm2,[rel 7FFD`F74E`8808h]
00007FFDF74E87B7 vmovss    xmm1,xmm1,xmm2
00007FFDF74E87BB vxorps    xmm0,xmm0,xmm1
00007FFDF74E87BF vxorps    xmm1,xmm1,xmm1
00007FFDF74E87C3 vmovss    [rsp+0Ch],xmm1
00007FFDF74E87C9 vmovss    [rsp+0Ch],xmm0
00007FFDF74E87CF vmovss    [rsp+10h],xmm1
00007FFDF74E87D5 vmovss    [rsp+14h],xmm1
00007FFDF74E87DB vmovss    xmm0,[rsp+0Ch]
00007FFDF74E87E1 vmovss    [rsp+10h],xmm1
00007FFDF74E87E7 vmovss    [rsp+14h],xmm0
00007FFDF74E87ED mov       rax,[rsp+10h]
00007FFDF74E87F2 add       rsp,18h
00007FFDF74E87F6 ret

# -----------------------------------------------------------------------------------

# [struct Point] Point op_Implicit(Origin)
00007FFDF74E8820 vzeroupper
00007FFDF74E8823 xchg      ax,ax
00007FFDF74E8825 vxorps    xmm0,xmm0,xmm0
00007FFDF74E8829 vmovss    xmm1,[rel 7FFD`F74E`8848h]
00007FFDF74E8831 vmovss    xmm0,xmm0,xmm1
00007FFDF74E8835 vmovupd   [rcx],xmm0
00007FFDF74E8839 mov       rax,rcx
00007FFDF74E883C ret

# -----------------------------------------------------------------------------------

# [struct Point] Boolean op_Equality(Point, Point)
00007FFDF74E8950 vzeroupper
00007FFDF74E8953 xchg      ax,ax
00007FFDF74E8955 vmovupd   xmm0,[rdx]
00007FFDF74E8959 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E895E vmovmskps eax,xmm0
00007FFDF74E8962 cmp       eax,0Fh
00007FFDF74E8965 sete      al
00007FFDF74E8968 movzx     eax,al
00007FFDF74E896B ret

# -----------------------------------------------------------------------------------

# [struct Point] Boolean op_Inequality(Point, Point)
00007FFDF74E8980 vzeroupper
00007FFDF74E8983 xchg      ax,ax
00007FFDF74E8985 vmovupd   xmm0,[rdx]
00007FFDF74E8989 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E898E vmovmskps eax,xmm0
00007FFDF74E8992 cmp       eax,0Fh
00007FFDF74E8995 sete      al
00007FFDF74E8998 movzx     eax,al
00007FFDF74E899B test      eax,eax
00007FFDF74E899D sete      al
00007FFDF74E89A0 movzx     eax,al
00007FFDF74E89A3 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor FromEulerAngles(Single, Single, Single)
00007FFDF74E89C0 push      rsi
00007FFDF74E89C1 sub       rsp,30h
00007FFDF74E89C5 vzeroupper
00007FFDF74E89C8 mov       rsi,rcx
00007FFDF74E89CB lea       rcx,[rsp+20h]
00007FFDF74E89D0 vxorps    xmm0,xmm0,xmm0
00007FFDF74E89D4 vmovdqu   [rcx],xmm0
00007FFDF74E89D8 lea       rcx,[rsp+20h]
00007FFDF74E89DD call      0000`7FFD`F726`4458h
00007FFDF74E89E2 vmovdqu   xmm0,[rsp+20h]
00007FFDF74E89E8 vmovdqu   [rsi],xmm0
00007FFDF74E89EC mov       rax,rsi
00007FFDF74E89EF add       rsp,30h
00007FFDF74E89F3 pop       rsi
00007FFDF74E89F4 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor FromAngleAxis(Single, Single, Single, Single)
00007FFDF74E8A10 push      rsi
00007FFDF74E8A11 sub       rsp,70h
00007FFDF74E8A15 vzeroupper
00007FFDF74E8A18 vmovaps   [rsp+60h],xmm6
00007FFDF74E8A1E vmovaps   [rsp+50h],xmm7
00007FFDF74E8A24 vmovaps   [rsp+40h],xmm8
00007FFDF74E8A2A vmovaps   [rsp+30h],xmm9
00007FFDF74E8A30 mov       rsi,rcx
00007FFDF74E8A33 vmovaps   xmm6,xmm2
00007FFDF74E8A37 vmovaps   xmm7,xmm3
00007FFDF74E8A3B vmovss    xmm8,[rsp+0A0h]
00007FFDF74E8A44 vmovaps   xmm0,xmm6
00007FFDF74E8A48 vmulss    xmm0,xmm0,xmm6
00007FFDF74E8A4C vmovaps   xmm2,xmm7
00007FFDF74E8A50 vmulss    xmm2,xmm2,xmm7
00007FFDF74E8A54 vaddss    xmm0,xmm0,xmm2
00007FFDF74E8A58 vmovaps   xmm2,xmm8
00007FFDF74E8A5D vmulss    xmm2,xmm2,xmm8
00007FFDF74E8A62 vaddss    xmm0,xmm0,xmm2
00007FFDF74E8A66 vsqrtss   xmm0,xmm0,xmm0
00007FFDF74E8A6A vmovss    xmm9,[rel 7FFD`F74E`8B18h]
00007FFDF74E8A72 vdivss    xmm9,xmm9,xmm0
00007FFDF74E8A76 vmovaps   xmm0,xmm1
00007FFDF74E8A7A vmulss    xmm0,xmm0,[rel 7FFD`F74E`8B1Ch]
00007FFDF74E8A82 vmovss    [rsp+2Ch],xmm0
00007FFDF74E8A88 call      0000`7FFE`56FC`8BF0h
00007FFDF74E8A8D vmulss    xmm9,xmm9,xmm0
00007FFDF74E8A91 vmovss    xmm0,[rsp+2Ch]
00007FFDF74E8A97 call      0000`7FFE`56FC`8B00h
00007FFDF74E8A9C vinsertps xmm0,xmm0,xmm6,10h
00007FFDF74E8AA2 vinsertps xmm0,xmm0,xmm7,20h
00007FFDF74E8AA8 vinsertps xmm0,xmm0,xmm8,30h
00007FFDF74E8AAE vmovss    xmm1,[rel 7FFD`F74E`8B20h]
00007FFDF74E8AB6 vmovaps   xmm2,xmm9
00007FFDF74E8ABB vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74E8AC1 vmovaps   xmm2,xmm9
00007FFDF74E8AC6 vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74E8ACC vinsertps xmm1,xmm1,xmm9,30h
00007FFDF74E8AD2 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8AD6 vmovupd   [rsi],xmm0
00007FFDF74E8ADA mov       rax,rsi
00007FFDF74E8ADD vmovaps   xmm6,[rsp+60h]
00007FFDF74E8AE3 vmovaps   xmm7,[rsp+50h]
00007FFDF74E8AE9 vmovaps   xmm8,[rsp+40h]
00007FFDF74E8AEF vmovaps   xmm9,[rsp+30h]
00007FFDF74E8AF5 add       rsp,70h
00007FFDF74E8AF9 pop       rsi
00007FFDF74E8AFA ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor LoadNormalized(Single*)
00007FFDF74E8B50 vzeroupper
00007FFDF74E8B53 xchg      ax,ax
00007FFDF74E8B55 vmovups   xmm0,[rdx]
00007FFDF74E8B59 vmovupd   [rcx],xmm0
00007FFDF74E8B5D mov       rax,rcx
00007FFDF74E8B60 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor LoadNormalized(System.ReadOnlySpan`1[System.Single])
00007FFDF74E8B80 push      rsi
00007FFDF74E8B81 sub       rsp,30h
00007FFDF74E8B85 vzeroupper
00007FFDF74E8B88 xor       eax,eax
00007FFDF74E8B8A mov       [rsp+28h],rax
00007FFDF74E8B8F mov       rax,[rdx]
00007FFDF74E8B92 mov       edx,[rdx+8]
00007FFDF74E8B95 xor       r8d,r8d
00007FFDF74E8B98 mov       [rsp+28h],r8
00007FFDF74E8B9D cmp       edx,4
00007FFDF74E8BA0 jl        short 0000`7FFD`F74E`8BCAh
00007FFDF74E8BA2 xor       r8d,r8d
00007FFDF74E8BA5 test      edx,edx
00007FFDF74E8BA7 je        short 0000`7FFD`F74E`8BACh
00007FFDF74E8BA9 mov       r8,rax
00007FFDF74E8BAC mov       [rsp+28h],r8
00007FFDF74E8BB1 vmovups   xmm0,[r8]
00007FFDF74E8BB6 xor       eax,eax
00007FFDF74E8BB8 mov       [rsp+28h],rax
00007FFDF74E8BBD vmovupd   [rcx],xmm0
00007FFDF74E8BC1 mov       rax,rcx
00007FFDF74E8BC4 add       rsp,30h
00007FFDF74E8BC8 pop       rsi
00007FFDF74E8BC9 ret
00007FFDF74E8BCA mov       rcx,7FFD`F72F`D4C0h
00007FFDF74E8BD4 call      0000`7FFE`56D7`7710h
00007FFDF74E8BD9 mov       rsi,rax
00007FFDF74E8BDC mov       ecx,33h
00007FFDF74E8BE1 mov       rdx,7FFD`F731`9EA0h
00007FFDF74E8BEB call      0000`7FFE`56EA`03E0h
00007FFDF74E8BF0 mov       rdx,rax
00007FFDF74E8BF3 mov       rcx,rsi
00007FFDF74E8BF6 call      0000`7FFD`F725`D238h
00007FFDF74E8BFB mov       rcx,rsi
00007FFDF74E8BFE call      0000`7FFE`56D3`B3A0h
00007FFDF74E8C03 int3

# -----------------------------------------------------------------------------------

# [struct Rotor] Void Store(Single*)
00007FFDF74E8C20 vzeroupper
00007FFDF74E8C23 xchg      ax,ax
00007FFDF74E8C25 vmovupd   xmm0,[rcx]
00007FFDF74E8C29 vmovups   [rdx],xmm0
00007FFDF74E8C2D ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Void Store(System.Span`1[System.Single])
00007FFDF74E8C40 push      rsi
00007FFDF74E8C41 sub       rsp,30h
00007FFDF74E8C45 vzeroupper
00007FFDF74E8C48 xor       eax,eax
00007FFDF74E8C4A mov       [rsp+28h],rax
00007FFDF74E8C4F mov       rax,[rdx]
00007FFDF74E8C52 mov       edx,[rdx+8]
00007FFDF74E8C55 vmovupd   xmm0,[rcx]
00007FFDF74E8C59 xor       ecx,ecx
00007FFDF74E8C5B mov       [rsp+28h],rcx
00007FFDF74E8C60 cmp       edx,4
00007FFDF74E8C63 jl        short 0000`7FFD`F74E`8C84h
00007FFDF74E8C65 xor       ecx,ecx
00007FFDF74E8C67 test      edx,edx
00007FFDF74E8C69 je        short 0000`7FFD`F74E`8C6Eh
00007FFDF74E8C6B mov       rcx,rax
00007FFDF74E8C6E mov       [rsp+28h],rcx
00007FFDF74E8C73 vmovups   [rcx],xmm0
00007FFDF74E8C77 xor       ecx,ecx
00007FFDF74E8C79 mov       [rsp+28h],rcx
00007FFDF74E8C7E add       rsp,30h
00007FFDF74E8C82 pop       rsi
00007FFDF74E8C83 ret
00007FFDF74E8C84 mov       rcx,7FFD`F72F`D4C0h
00007FFDF74E8C8E call      0000`7FFE`56D7`7710h
00007FFDF74E8C93 mov       rsi,rax
00007FFDF74E8C96 mov       ecx,25h
00007FFDF74E8C9B mov       rdx,7FFD`F731`9EA0h
00007FFDF74E8CA5 call      0000`7FFE`56EA`03E0h
00007FFDF74E8CAA mov       rdx,rax
00007FFDF74E8CAD mov       rcx,rsi
00007FFDF74E8CB0 call      0000`7FFD`F725`D238h
00007FFDF74E8CB5 mov       rcx,rsi
00007FFDF74E8CB8 call      0000`7FFE`56D3`B3A0h
00007FFDF74E8CBD int3

# -----------------------------------------------------------------------------------

# [struct Rotor] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef, Single ByRef)
00007FFDF74E8CE0 vzeroupper
00007FFDF74E8CE3 xchg      ax,ax
00007FFDF74E8CE5 vmovss    xmm0,[rcx]
00007FFDF74E8CE9 vmovss    [rdx],xmm0
00007FFDF74E8CED vmovupd   xmm0,[rcx]
00007FFDF74E8CF1 vextractps eax,xmm0,1
00007FFDF74E8CF7 vmovd     xmm0,eax
00007FFDF74E8CFB vmovss    [r8],xmm0
00007FFDF74E8D00 vmovupd   xmm0,[rcx]
00007FFDF74E8D04 vextractps eax,xmm0,2
00007FFDF74E8D0A vmovd     xmm0,eax
00007FFDF74E8D0E vmovss    [r9],xmm0
00007FFDF74E8D13 vmovupd   xmm0,[rcx]
00007FFDF74E8D17 vextractps eax,xmm0,3
00007FFDF74E8D1D vmovd     xmm0,eax
00007FFDF74E8D21 mov       rax,[rsp+28h]
00007FFDF74E8D26 vmovss    [rax],xmm0
00007FFDF74E8D2A ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor Normalized()
00007FFDF74E8D50 vzeroupper
00007FFDF74E8D53 xchg      ax,ax
00007FFDF74E8D55 vmovupd   xmm0,[rcx]
00007FFDF74E8D59 vmovaps   xmm1,xmm0
00007FFDF74E8D5D vmovaps   xmm2,xmm0
00007FFDF74E8D61 vdpps     xmm1,xmm1,xmm2,0FFh
00007FFDF74E8D67 vrsqrtps  xmm2,xmm1
00007FFDF74E8D6B vmulps    xmm3,xmm2,xmm2
00007FFDF74E8D6F vmulps    xmm3,xmm1,xmm3
00007FFDF74E8D73 vmovss    xmm1,[rel 7FFD`F74E`8DB8h]
00007FFDF74E8D7B vbroadcastss xmm1,xmm1
00007FFDF74E8D80 vsubps    xmm1,xmm1,xmm3
00007FFDF74E8D84 vmovss    xmm3,[rel 7FFD`F74E`8DBCh]
00007FFDF74E8D8C vbroadcastss xmm3,xmm3
00007FFDF74E8D91 vmulps    xmm2,xmm3,xmm2
00007FFDF74E8D95 vmulps    xmm1,xmm2,xmm1
00007FFDF74E8D99 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8D9D vmovupd   [rdx],xmm0
00007FFDF74E8DA1 mov       rax,rdx
00007FFDF74E8DA4 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] m128 Inverse(m128)
00007FFDF74E8DD0 vzeroupper
00007FFDF74E8DD3 xchg      ax,ax
00007FFDF74E8DD5 vmovupd   xmm0,[rdx]
00007FFDF74E8DD9 vmovaps   xmm1,xmm0
00007FFDF74E8DDD vmovaps   xmm2,xmm0
00007FFDF74E8DE1 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74E8DE7 vrsqrtps  xmm2,xmm1
00007FFDF74E8DEB vmulps    xmm3,xmm2,xmm2
00007FFDF74E8DEF vmulps    xmm3,xmm1,xmm3
00007FFDF74E8DF3 vmovss    xmm1,[rel 7FFD`F74E`8E88h]
00007FFDF74E8DFB vbroadcastss xmm1,xmm1
00007FFDF74E8E00 vsubps    xmm1,xmm1,xmm3
00007FFDF74E8E04 vmovss    xmm3,[rel 7FFD`F74E`8E8Ch]
00007FFDF74E8E0C vbroadcastss xmm3,xmm3
00007FFDF74E8E11 vmulps    xmm2,xmm3,xmm2
00007FFDF74E8E15 vmulps    xmm1,xmm2,xmm1
00007FFDF74E8E19 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8E1D vmovupd   [rdx],xmm0
00007FFDF74E8E21 vmovupd   xmm0,[rdx]
00007FFDF74E8E25 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8E29 vmovupd   [rdx],xmm0
00007FFDF74E8E2D vxorps    xmm0,xmm0,xmm0
00007FFDF74E8E31 vmovss    xmm1,[rel 7FFD`F74E`8E90h]
00007FFDF74E8E39 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74E8E3F vmovss    xmm1,[rel 7FFD`F74E`8E94h]
00007FFDF74E8E47 vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74E8E4D vmovss    xmm1,[rel 7FFD`F74E`8E98h]
00007FFDF74E8E55 vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74E8E5B vmovupd   xmm1,[rdx]
00007FFDF74E8E5F vxorps    xmm0,xmm0,xmm1
00007FFDF74E8E63 vmovupd   [rcx],xmm0
00007FFDF74E8E67 mov       rax,rcx
00007FFDF74E8E6A ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor Inverse()
00007FFDF74E8EB0 vzeroupper
00007FFDF74E8EB3 xchg      ax,ax
00007FFDF74E8EB5 vmovupd   xmm0,[rcx]
00007FFDF74E8EB9 vmovaps   xmm1,xmm0
00007FFDF74E8EBD vmovaps   xmm2,xmm0
00007FFDF74E8EC1 vdpps     xmm1,xmm1,xmm2,0EFh
00007FFDF74E8EC7 vrsqrtps  xmm2,xmm1
00007FFDF74E8ECB vmulps    xmm3,xmm2,xmm2
00007FFDF74E8ECF vmulps    xmm3,xmm1,xmm3
00007FFDF74E8ED3 vmovss    xmm1,[rel 7FFD`F74E`8F50h]
00007FFDF74E8EDB vbroadcastss xmm1,xmm1
00007FFDF74E8EE0 vsubps    xmm1,xmm1,xmm3
00007FFDF74E8EE4 vmovss    xmm3,[rel 7FFD`F74E`8F54h]
00007FFDF74E8EEC vbroadcastss xmm3,xmm3
00007FFDF74E8EF1 vmulps    xmm2,xmm3,xmm2
00007FFDF74E8EF5 vmulps    xmm1,xmm2,xmm1
00007FFDF74E8EF9 vmulps    xmm0,xmm0,xmm1
00007FFDF74E8EFD vmulps    xmm0,xmm0,xmm1
00007FFDF74E8F01 vxorps    xmm1,xmm1,xmm1
00007FFDF74E8F05 vmovss    xmm2,[rel 7FFD`F74E`8F58h]
00007FFDF74E8F0D vinsertps xmm1,xmm1,xmm2,10h
00007FFDF74E8F13 vmovss    xmm2,[rel 7FFD`F74E`8F5Ch]
00007FFDF74E8F1B vinsertps xmm1,xmm1,xmm2,20h
00007FFDF74E8F21 vmovss    xmm2,[rel 7FFD`F74E`8F60h]
00007FFDF74E8F29 vinsertps xmm1,xmm1,xmm2,30h
00007FFDF74E8F2F vxorps    xmm0,xmm1,xmm0
00007FFDF74E8F33 vmovupd   [rdx],xmm0
00007FFDF74E8F37 mov       rax,rdx
00007FFDF74E8F3A ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor Constrained()
00007FFDF74E8F80 vzeroupper
00007FFDF74E8F83 xchg      ax,ax
00007FFDF74E8F85 vmovupd   xmm0,[rcx]
00007FFDF74E8F89 vmovaps   xmm1,xmm0
00007FFDF74E8F8D vxorps    xmm2,xmm2,xmm2
00007FFDF74E8F91 vmovss    xmm3,[rel 7FFD`F74E`8FC0h]
00007FFDF74E8F99 vmovss    xmm2,xmm2,xmm3
00007FFDF74E8F9D vandps    xmm1,xmm1,xmm2
00007FFDF74E8FA1 vshufps   xmm1,xmm1,xmm1,0
00007FFDF74E8FA6 vxorps    xmm0,xmm1,xmm0
00007FFDF74E8FAA vmovupd   [rdx],xmm0
00007FFDF74E8FAE mov       rax,rdx
00007FFDF74E8FB1 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Boolean Equals(Rotor)
00007FFDF74E8FE0 vzeroupper
00007FFDF74E8FE3 xchg      ax,ax
00007FFDF74E8FE5 vmovupd   xmm0,[rdx]
00007FFDF74E8FE9 vcmpeqps  xmm0,xmm0,[rcx]
00007FFDF74E8FEE vmovmskps eax,xmm0
00007FFDF74E8FF2 cmp       eax,0Fh
00007FFDF74E8FF5 sete      al
00007FFDF74E8FF8 movzx     eax,al
00007FFDF74E8FFB ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Boolean Equals(Rotor, Single)
00007FFDF74E9010 vzeroupper
00007FFDF74E9013 xchg      ax,ax
00007FFDF74E9015 vbroadcastss xmm0,xmm2
00007FFDF74E901A vmovss    xmm1,[rel 7FFD`F74E`9058h]
00007FFDF74E9022 vbroadcastss xmm1,xmm1
00007FFDF74E9027 vmovupd   xmm2,[rcx]
00007FFDF74E902B vmovupd   xmm3,[rdx]
00007FFDF74E902F vsubps    xmm2,xmm2,xmm3
00007FFDF74E9033 vandnps   xmm1,xmm1,xmm2
00007FFDF74E9037 vcmpltps  xmm0,xmm1,xmm0
00007FFDF74E903C vmovmskps eax,xmm0
00007FFDF74E9040 cmp       eax,0Fh
00007FFDF74E9043 setne     al
00007FFDF74E9046 movzx     eax,al
00007FFDF74E9049 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] System.ValueTuple`3[System.Single,System.Single,System.Single] AsEulerAngles()
00007FFDF74E9070 push      rsi
00007FFDF74E9071 sub       rsp,0A0h
00007FFDF74E9078 vzeroupper
00007FFDF74E907B vmovaps   [rsp+90h],xmm6
00007FFDF74E9084 vmovaps   [rsp+80h],xmm7
00007FFDF74E908D vmovaps   [rsp+70h],xmm8
00007FFDF74E9093 vmovaps   [rsp+60h],xmm9
00007FFDF74E9099 vmovaps   [rsp+50h],xmm10
00007FFDF74E909F vmovaps   [rsp+40h],xmm11
00007FFDF74E90A5 mov       rsi,rdx
00007FFDF74E90A8 vmovupd   xmm0,[rcx]
00007FFDF74E90AC vmovaps   xmm6,xmm0
00007FFDF74E90B0 vextractps eax,xmm0,1
00007FFDF74E90B6 vmovd     xmm7,eax
00007FFDF74E90BA vextractps eax,xmm0,2
00007FFDF74E90C0 vmovd     xmm8,eax
00007FFDF74E90C4 vextractps eax,xmm0,3
00007FFDF74E90CA vmovd     xmm9,eax
00007FFDF74E90CE vmovaps   xmm1,xmm7
00007FFDF74E90D2 vmulss    xmm1,xmm1,xmm7
00007FFDF74E90D6 vmovaps   xmm10,xmm8
00007FFDF74E90DB vmulss    xmm10,xmm10,xmm8
00007FFDF74E90E0 vmovaps   xmm0,xmm6
00007FFDF74E90E4 vmulss    xmm0,xmm0,xmm7
00007FFDF74E90E8 vmovaps   xmm2,xmm8
00007FFDF74E90ED vmulss    xmm2,xmm2,xmm9
00007FFDF74E90F2 vaddss    xmm0,xmm0,xmm2
00007FFDF74E90F6 vmulss    xmm0,xmm0,[rel 7FFD`F74E`9220h]
00007FFDF74E90FE vaddss    xmm1,xmm1,xmm10
00007FFDF74E9103 vmulss    xmm1,xmm1,[rel 7FFD`F74E`9224h]
00007FFDF74E910B vmovss    xmm2,[rel 7FFD`F74E`9228h]
00007FFDF74E9113 vsubss    xmm2,xmm2,xmm1
00007FFDF74E9117 vmovaps   xmm1,xmm2
00007FFDF74E911B call      0000`7FFE`56FC`8AB0h
00007FFDF74E9120 vmovaps   xmm11,xmm0
00007FFDF74E9124 vmovaps   xmm0,xmm6
00007FFDF74E9128 vmulss    xmm0,xmm0,xmm8
00007FFDF74E912D vmovaps   xmm1,xmm7
00007FFDF74E9131 vmulss    xmm1,xmm1,xmm9
00007FFDF74E9136 vsubss    xmm0,xmm0,xmm1
00007FFDF74E913A vmulss    xmm0,xmm0,[rel 7FFD`F74E`922Ch]
00007FFDF74E9142 call      0000`7FFE`56FC`8A90h
00007FFDF74E9147 vmovss    [rsp+3Ch],xmm0
00007FFDF74E914D vmulss    xmm6,xmm6,xmm9
00007FFDF74E9152 vmulss    xmm7,xmm7,xmm8
00007FFDF74E9157 vaddss    xmm7,xmm7,xmm6
00007FFDF74E915B vmovaps   xmm0,xmm7
00007FFDF74E915F vmulss    xmm0,xmm0,[rel 7FFD`F74E`9230h]
00007FFDF74E9167 vmulss    xmm9,xmm9,xmm9
00007FFDF74E916C vaddss    xmm9,xmm9,xmm10
00007FFDF74E9171 vmulss    xmm9,xmm9,[rel 7FFD`F74E`9234h]
00007FFDF74E9179 vmovss    xmm1,[rel 7FFD`F74E`9238h]
00007FFDF74E9181 vsubss    xmm1,xmm1,xmm9
00007FFDF74E9186 call      0000`7FFE`56FC`8AB0h
00007FFDF74E918B lea       rax,[rsp+28h]
00007FFDF74E9190 vxorps    xmm1,xmm1,xmm1
00007FFDF74E9194 vmovdqu   [rax],xmm1
00007FFDF74E9198 vmovss    [rsp+28h],xmm11
00007FFDF74E919E vmovss    xmm1,[rsp+3Ch]
00007FFDF74E91A4 vmovss    [rsp+2Ch],xmm1
00007FFDF74E91AA vmovss    [rsp+30h],xmm0
00007FFDF74E91B0 vmovdqu   xmm0,[rsp+28h]
00007FFDF74E91B6 vmovdqu   [rsi],xmm0
00007FFDF74E91BA mov       rax,rsi
00007FFDF74E91BD vmovaps   xmm6,[rsp+90h]
00007FFDF74E91C6 vmovaps   xmm7,[rsp+80h]
00007FFDF74E91CF vmovaps   xmm8,[rsp+70h]
00007FFDF74E91D5 vmovaps   xmm9,[rsp+60h]
00007FFDF74E91DB vmovaps   xmm10,[rsp+50h]
00007FFDF74E91E1 vmovaps   xmm11,[rsp+40h]
00007FFDF74E91E7 add       rsp,0A0h
00007FFDF74E91EE pop       rsi
00007FFDF74E91EF ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Plane Conjugate(Plane)
00007FFDF74E9430 sub       rsp,58h
00007FFDF74E9434 vzeroupper
00007FFDF74E9437 vmovaps   [rsp+40h],xmm6
00007FFDF74E943D vmovaps   [rsp+30h],xmm7
00007FFDF74E9443 vmovaps   [rsp+20h],xmm8
00007FFDF74E9449 vmovaps   [rsp+10h],xmm9
00007FFDF74E944F vmovaps   [rsp],xmm10
00007FFDF74E9454 vmovupd   xmm0,[r8]
00007FFDF74E9459 vmovupd   xmm1,[rcx]
00007FFDF74E945D vmovss    xmm2,[rel 7FFD`F74E`9590h]
00007FFDF74E9465 vmovss    xmm3,[rel 7FFD`F74E`9594h]
00007FFDF74E946D vinsertps xmm2,xmm2,xmm3,10h
00007FFDF74E9473 vmovss    xmm3,[rel 7FFD`F74E`9598h]
00007FFDF74E947B vinsertps xmm2,xmm2,xmm3,20h
00007FFDF74E9481 vmovss    xmm3,[rel 7FFD`F74E`959Ch]
00007FFDF74E9489 vinsertps xmm2,xmm2,xmm3,30h
00007FFDF74E948F vmovaps   xmm3,xmm2
00007FFDF74E9493 vshufps   xmm4,xmm1,xmm1,9Ch
00007FFDF74E9498 vshufps   xmm5,xmm1,xmm1,78h
00007FFDF74E949D vshufps   xmm6,xmm1,xmm1,0
00007FFDF74E94A2 vshufps   xmm7,xmm1,xmm1,2
00007FFDF74E94A7 vshufps   xmm8,xmm1,xmm1,9Eh
00007FFDF74E94AC vmulps    xmm7,xmm7,xmm8
00007FFDF74E94B1 vshufps   xmm8,xmm1,xmm1,79h
00007FFDF74E94B6 vshufps   xmm9,xmm1,xmm1,0E5h
00007FFDF74E94BB vmulps    xmm8,xmm8,xmm9
00007FFDF74E94C0 vaddps    xmm7,xmm7,xmm8
00007FFDF74E94C5 vmulps    xmm2,xmm7,xmm2
00007FFDF74E94C9 vmulps    xmm7,xmm1,xmm4
00007FFDF74E94CD vxorps    xmm8,xmm8,xmm8
00007FFDF74E94D2 vmovss    xmm9,[rel 7FFD`F74E`95A0h]
00007FFDF74E94DA vmovss    xmm8,xmm8,xmm9
00007FFDF74E94DF vshufps   xmm9,xmm1,xmm1,3
00007FFDF74E94E4 vshufps   xmm10,xmm1,xmm1,7Bh
00007FFDF74E94E9 vmulps    xmm9,xmm9,xmm10
00007FFDF74E94EE vxorps    xmm8,xmm8,xmm9
00007FFDF74E94F3 vsubps    xmm7,xmm7,xmm8
00007FFDF74E94F8 vmulps    xmm7,xmm7,xmm3
00007FFDF74E94FC vmulps    xmm1,xmm1,xmm1
00007FFDF74E9500 vmulps    xmm3,xmm4,xmm4
00007FFDF74E9504 vsubps    xmm1,xmm1,xmm3
00007FFDF74E9508 vmulps    xmm3,xmm6,xmm6
00007FFDF74E950C vaddps    xmm1,xmm1,xmm3
00007FFDF74E9510 vmulps    xmm3,xmm5,xmm5
00007FFDF74E9514 vsubps    xmm1,xmm1,xmm3
00007FFDF74E9518 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E951D vmulps    xmm2,xmm2,xmm3
00007FFDF74E9521 vshufps   xmm3,xmm0,xmm0,9Ch
00007FFDF74E9526 vmulps    xmm3,xmm7,xmm3
00007FFDF74E952A vaddps    xmm2,xmm2,xmm3
00007FFDF74E952E vmulps    xmm0,xmm1,xmm0
00007FFDF74E9532 vaddps    xmm2,xmm2,xmm0
00007FFDF74E9536 vmovupd   [rdx],xmm2
00007FFDF74E953A mov       rax,rdx
00007FFDF74E953D vmovaps   xmm6,[rsp+40h]
00007FFDF74E9543 vmovaps   xmm7,[rsp+30h]
00007FFDF74E9549 vmovaps   xmm8,[rsp+20h]
00007FFDF74E954F vmovaps   xmm9,[rsp+10h]
00007FFDF74E9555 vmovaps   xmm10,[rsp]
00007FFDF74E955A add       rsp,58h
00007FFDF74E955E ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Branch Conjugate(Branch)
00007FFDF74E9760 sub       rsp,38h
00007FFDF74E9764 vzeroupper
00007FFDF74E9767 vmovaps   [rsp+20h],xmm6
00007FFDF74E976D vmovaps   [rsp+10h],xmm7
00007FFDF74E9773 vmovaps   [rsp],xmm8
00007FFDF74E9778 vmovupd   xmm0,[r8]
00007FFDF74E977D vmovupd   xmm1,[rcx]
00007FFDF74E9781 vmovaps   xmm2,xmm1
00007FFDF74E9785 vshufps   xmm3,xmm1,xmm1,9Ch
00007FFDF74E978A vshufps   xmm4,xmm1,xmm1,78h
00007FFDF74E978F vshufps   xmm5,xmm1,xmm1,1
00007FFDF74E9794 vmulps    xmm5,xmm5,xmm5
00007FFDF74E9798 vmulps    xmm6,xmm1,xmm1
00007FFDF74E979C vaddps    xmm5,xmm6,xmm5
00007FFDF74E97A0 vshufps   xmm6,xmm1,xmm1,9Eh
00007FFDF74E97A5 vmulps    xmm7,xmm6,xmm6
00007FFDF74E97A9 vshufps   xmm6,xmm1,xmm1,7Bh
00007FFDF74E97AE vmulps    xmm1,xmm6,xmm6
00007FFDF74E97B2 vaddps    xmm7,xmm7,xmm1
00007FFDF74E97B6 vxorps    xmm1,xmm1,xmm1
00007FFDF74E97BA vmovss    xmm6,[rel 7FFD`F74E`9890h]
00007FFDF74E97C2 vmovss    xmm1,xmm1,xmm6
00007FFDF74E97C6 vxorps    xmm1,xmm7,xmm1
00007FFDF74E97CA vsubps    xmm1,xmm5,xmm1
00007FFDF74E97CE vshufps   xmm5,xmm2,xmm2,0
00007FFDF74E97D3 vxorps    xmm7,xmm7,xmm7
00007FFDF74E97D7 vmovss    xmm6,[rel 7FFD`F74E`9894h]
00007FFDF74E97DF vinsertps xmm7,xmm7,xmm6,10h
00007FFDF74E97E5 vmovss    xmm6,[rel 7FFD`F74E`9898h]
00007FFDF74E97ED vinsertps xmm7,xmm7,xmm6,20h
00007FFDF74E97F3 vmovss    xmm6,[rel 7FFD`F74E`989Ch]
00007FFDF74E97FB vinsertps xmm6,xmm7,xmm6,30h
00007FFDF74E9801 vmulps    xmm7,xmm5,xmm3
00007FFDF74E9805 vmulps    xmm8,xmm2,xmm4
00007FFDF74E9809 vaddps    xmm7,xmm7,xmm8
00007FFDF74E980E vmulps    xmm7,xmm7,xmm6
00007FFDF74E9812 vmulps    xmm2,xmm2,xmm3
00007FFDF74E9816 vmulps    xmm3,xmm5,xmm4
00007FFDF74E981A vsubps    xmm2,xmm2,xmm3
00007FFDF74E981E vmulps    xmm2,xmm2,xmm6
00007FFDF74E9822 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E9827 vshufps   xmm4,xmm0,xmm0,9Ch
00007FFDF74E982C vmulps    xmm0,xmm1,xmm0
00007FFDF74E9830 vmulps    xmm1,xmm7,xmm3
00007FFDF74E9834 vaddps    xmm0,xmm0,xmm1
00007FFDF74E9838 vmulps    xmm1,xmm2,xmm4
00007FFDF74E983C vaddps    xmm0,xmm0,xmm1
00007FFDF74E9840 vmovupd   [rdx],xmm0
00007FFDF74E9844 mov       rax,rdx
00007FFDF74E9847 vmovaps   xmm6,[rsp+20h]
00007FFDF74E984D vmovaps   xmm7,[rsp+10h]
00007FFDF74E9853 vmovaps   xmm8,[rsp]
00007FFDF74E9858 add       rsp,38h
00007FFDF74E985C ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Line Conjugate(Line)
00007FFDF74E9AB0 sub       rsp,48h
00007FFDF74E9AB4 vzeroupper
00007FFDF74E9AB7 vmovaps   [rsp+30h],xmm6
00007FFDF74E9ABD vmovaps   [rsp+20h],xmm7
00007FFDF74E9AC3 vmovaps   [rsp+10h],xmm8
00007FFDF74E9AC9 vmovaps   [rsp],xmm9
00007FFDF74E9ACE vmovupd   xmm0,[r8]
00007FFDF74E9AD3 vmovupd   xmm1,[r8+10h]
00007FFDF74E9AD9 vmovupd   xmm2,[rcx]
00007FFDF74E9ADD vmovaps   xmm3,xmm2
00007FFDF74E9AE1 vshufps   xmm4,xmm2,xmm2,9Ch
00007FFDF74E9AE6 vshufps   xmm5,xmm2,xmm2,78h
00007FFDF74E9AEB vshufps   xmm6,xmm2,xmm2,1
00007FFDF74E9AF0 vmulps    xmm6,xmm6,xmm6
00007FFDF74E9AF4 vmulps    xmm7,xmm2,xmm2
00007FFDF74E9AF8 vaddps    xmm6,xmm7,xmm6
00007FFDF74E9AFC vshufps   xmm7,xmm2,xmm2,9Eh
00007FFDF74E9B01 vmulps    xmm8,xmm7,xmm7
00007FFDF74E9B05 vshufps   xmm7,xmm2,xmm2,7Bh
00007FFDF74E9B0A vmulps    xmm2,xmm7,xmm7
00007FFDF74E9B0E vaddps    xmm8,xmm8,xmm2
00007FFDF74E9B12 vxorps    xmm2,xmm2,xmm2
00007FFDF74E9B16 vmovss    xmm7,[rel 7FFD`F74E`9C18h]
00007FFDF74E9B1E vmovss    xmm2,xmm2,xmm7
00007FFDF74E9B22 vxorps    xmm2,xmm8,xmm2
00007FFDF74E9B26 vsubps    xmm2,xmm6,xmm2
00007FFDF74E9B2A vshufps   xmm6,xmm3,xmm3,0
00007FFDF74E9B2F vxorps    xmm8,xmm8,xmm8
00007FFDF74E9B34 vmovss    xmm7,[rel 7FFD`F74E`9C1Ch]
00007FFDF74E9B3C vinsertps xmm8,xmm8,xmm7,10h
00007FFDF74E9B42 vmovss    xmm7,[rel 7FFD`F74E`9C20h]
00007FFDF74E9B4A vinsertps xmm8,xmm8,xmm7,20h
00007FFDF74E9B50 vmovss    xmm7,[rel 7FFD`F74E`9C24h]
00007FFDF74E9B58 vinsertps xmm7,xmm8,xmm7,30h
00007FFDF74E9B5E vmulps    xmm8,xmm6,xmm4
00007FFDF74E9B62 vmulps    xmm9,xmm3,xmm5
00007FFDF74E9B66 vaddps    xmm8,xmm8,xmm9
00007FFDF74E9B6B vmulps    xmm8,xmm8,xmm7
00007FFDF74E9B6F vmulps    xmm3,xmm3,xmm4
00007FFDF74E9B73 vmulps    xmm4,xmm6,xmm5
00007FFDF74E9B77 vsubps    xmm3,xmm3,xmm4
00007FFDF74E9B7B vmulps    xmm3,xmm3,xmm7
00007FFDF74E9B7F vshufps   xmm4,xmm0,xmm0,78h
00007FFDF74E9B84 vshufps   xmm5,xmm0,xmm0,9Ch
00007FFDF74E9B89 vmulps    xmm0,xmm2,xmm0
00007FFDF74E9B8D vmulps    xmm4,xmm8,xmm4
00007FFDF74E9B91 vaddps    xmm0,xmm0,xmm4
00007FFDF74E9B95 vmulps    xmm4,xmm3,xmm5
00007FFDF74E9B99 vaddps    xmm0,xmm0,xmm4
00007FFDF74E9B9D vmulps    xmm2,xmm2,xmm1
00007FFDF74E9BA1 vshufps   xmm4,xmm1,xmm1,78h
00007FFDF74E9BA6 vmulps    xmm4,xmm8,xmm4
00007FFDF74E9BAA vaddps    xmm2,xmm2,xmm4
00007FFDF74E9BAE vshufps   xmm1,xmm1,xmm1,9Ch
00007FFDF74E9BB3 vmulps    xmm1,xmm3,xmm1
00007FFDF74E9BB7 vaddps    xmm2,xmm2,xmm1
00007FFDF74E9BBB vmovupd   [rdx],xmm0
00007FFDF74E9BBF vmovupd   [rdx+10h],xmm2
00007FFDF74E9BC4 mov       rax,rdx
00007FFDF74E9BC7 vmovaps   xmm6,[rsp+30h]
00007FFDF74E9BCD vmovaps   xmm7,[rsp+20h]
00007FFDF74E9BD3 vmovaps   xmm8,[rsp+10h]
00007FFDF74E9BD9 vmovaps   xmm9,[rsp]
00007FFDF74E9BDE add       rsp,48h
00007FFDF74E9BE2 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Point Conjugate(Point)
00007FFDF74E9E10 sub       rsp,58h
00007FFDF74E9E14 vzeroupper
00007FFDF74E9E17 vmovaps   [rsp+40h],xmm6
00007FFDF74E9E1D vmovaps   [rsp+30h],xmm7
00007FFDF74E9E23 vmovaps   [rsp+20h],xmm8
00007FFDF74E9E29 vmovaps   [rsp+10h],xmm9
00007FFDF74E9E2F vmovaps   [rsp],xmm10
00007FFDF74E9E34 vmovupd   xmm0,[r8]
00007FFDF74E9E39 vmovupd   xmm1,[rcx]
00007FFDF74E9E3D vmovss    xmm2,[rel 7FFD`F74E`9F70h]
00007FFDF74E9E45 vmovss    xmm3,[rel 7FFD`F74E`9F74h]
00007FFDF74E9E4D vinsertps xmm2,xmm2,xmm3,10h
00007FFDF74E9E53 vmovss    xmm3,[rel 7FFD`F74E`9F78h]
00007FFDF74E9E5B vinsertps xmm2,xmm2,xmm3,20h
00007FFDF74E9E61 vmovss    xmm3,[rel 7FFD`F74E`9F7Ch]
00007FFDF74E9E69 vinsertps xmm2,xmm2,xmm3,30h
00007FFDF74E9E6F vmovaps   xmm3,xmm2
00007FFDF74E9E73 vshufps   xmm4,xmm1,xmm1,9Ch
00007FFDF74E9E78 vshufps   xmm5,xmm1,xmm1,78h
00007FFDF74E9E7D vshufps   xmm6,xmm1,xmm1,0
00007FFDF74E9E82 vshufps   xmm7,xmm1,xmm1,2
00007FFDF74E9E87 vshufps   xmm8,xmm1,xmm1,9Eh
00007FFDF74E9E8C vmulps    xmm7,xmm7,xmm8
00007FFDF74E9E91 vshufps   xmm8,xmm1,xmm1,79h
00007FFDF74E9E96 vshufps   xmm9,xmm1,xmm1,0E5h
00007FFDF74E9E9B vmulps    xmm8,xmm8,xmm9
00007FFDF74E9EA0 vaddps    xmm7,xmm7,xmm8
00007FFDF74E9EA5 vmulps    xmm2,xmm7,xmm2
00007FFDF74E9EA9 vmulps    xmm7,xmm1,xmm4
00007FFDF74E9EAD vxorps    xmm8,xmm8,xmm8
00007FFDF74E9EB2 vmovss    xmm9,[rel 7FFD`F74E`9F80h]
00007FFDF74E9EBA vmovss    xmm8,xmm8,xmm9
00007FFDF74E9EBF vshufps   xmm9,xmm1,xmm1,3
00007FFDF74E9EC4 vshufps   xmm10,xmm1,xmm1,7Bh
00007FFDF74E9EC9 vmulps    xmm9,xmm9,xmm10
00007FFDF74E9ECE vxorps    xmm8,xmm8,xmm9
00007FFDF74E9ED3 vsubps    xmm7,xmm7,xmm8
00007FFDF74E9ED8 vmulps    xmm7,xmm7,xmm3
00007FFDF74E9EDC vmulps    xmm1,xmm1,xmm1
00007FFDF74E9EE0 vmulps    xmm3,xmm4,xmm4
00007FFDF74E9EE4 vsubps    xmm1,xmm1,xmm3
00007FFDF74E9EE8 vmulps    xmm3,xmm6,xmm6
00007FFDF74E9EEC vaddps    xmm1,xmm1,xmm3
00007FFDF74E9EF0 vmulps    xmm3,xmm5,xmm5
00007FFDF74E9EF4 vsubps    xmm1,xmm1,xmm3
00007FFDF74E9EF8 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74E9EFD vmulps    xmm2,xmm2,xmm3
00007FFDF74E9F01 vshufps   xmm3,xmm0,xmm0,9Ch
00007FFDF74E9F06 vmulps    xmm3,xmm7,xmm3
00007FFDF74E9F0A vaddps    xmm2,xmm2,xmm3
00007FFDF74E9F0E vmulps    xmm0,xmm1,xmm0
00007FFDF74E9F12 vaddps    xmm2,xmm2,xmm0
00007FFDF74E9F16 vmovupd   [rdx],xmm2
00007FFDF74E9F1A mov       rax,rdx
00007FFDF74E9F1D vmovaps   xmm6,[rsp+40h]
00007FFDF74E9F23 vmovaps   xmm7,[rsp+30h]
00007FFDF74E9F29 vmovaps   xmm8,[rsp+20h]
00007FFDF74E9F2F vmovaps   xmm9,[rsp+10h]
00007FFDF74E9F35 vmovaps   xmm10,[rsp]
00007FFDF74E9F3A add       rsp,58h
00007FFDF74E9F3E ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Direction Conjugate(Direction)
00007FFDF74EA170 sub       rsp,58h
00007FFDF74EA174 vzeroupper
00007FFDF74EA177 vmovaps   [rsp+40h],xmm6
00007FFDF74EA17D vmovaps   [rsp+30h],xmm7
00007FFDF74EA183 vmovaps   [rsp+20h],xmm8
00007FFDF74EA189 vmovaps   [rsp+10h],xmm9
00007FFDF74EA18F vmovaps   [rsp],xmm10
00007FFDF74EA194 vmovupd   xmm0,[r8]
00007FFDF74EA199 vmovupd   xmm1,[rcx]
00007FFDF74EA19D vmovss    xmm2,[rel 7FFD`F74E`A2D0h]
00007FFDF74EA1A5 vmovss    xmm3,[rel 7FFD`F74E`A2D4h]
00007FFDF74EA1AD vinsertps xmm2,xmm2,xmm3,10h
00007FFDF74EA1B3 vmovss    xmm3,[rel 7FFD`F74E`A2D8h]
00007FFDF74EA1BB vinsertps xmm2,xmm2,xmm3,20h
00007FFDF74EA1C1 vmovss    xmm3,[rel 7FFD`F74E`A2DCh]
00007FFDF74EA1C9 vinsertps xmm2,xmm2,xmm3,30h
00007FFDF74EA1CF vmovaps   xmm3,xmm2
00007FFDF74EA1D3 vshufps   xmm4,xmm1,xmm1,9Ch
00007FFDF74EA1D8 vshufps   xmm5,xmm1,xmm1,78h
00007FFDF74EA1DD vshufps   xmm6,xmm1,xmm1,0
00007FFDF74EA1E2 vshufps   xmm7,xmm1,xmm1,2
00007FFDF74EA1E7 vshufps   xmm8,xmm1,xmm1,9Eh
00007FFDF74EA1EC vmulps    xmm7,xmm7,xmm8
00007FFDF74EA1F1 vshufps   xmm8,xmm1,xmm1,79h
00007FFDF74EA1F6 vshufps   xmm9,xmm1,xmm1,0E5h
00007FFDF74EA1FB vmulps    xmm8,xmm8,xmm9
00007FFDF74EA200 vaddps    xmm7,xmm7,xmm8
00007FFDF74EA205 vmulps    xmm2,xmm7,xmm2
00007FFDF74EA209 vmulps    xmm7,xmm1,xmm4
00007FFDF74EA20D vxorps    xmm8,xmm8,xmm8
00007FFDF74EA212 vmovss    xmm9,[rel 7FFD`F74E`A2E0h]
00007FFDF74EA21A vmovss    xmm8,xmm8,xmm9
00007FFDF74EA21F vshufps   xmm9,xmm1,xmm1,3
00007FFDF74EA224 vshufps   xmm10,xmm1,xmm1,7Bh
00007FFDF74EA229 vmulps    xmm9,xmm9,xmm10
00007FFDF74EA22E vxorps    xmm8,xmm8,xmm9
00007FFDF74EA233 vsubps    xmm7,xmm7,xmm8
00007FFDF74EA238 vmulps    xmm7,xmm7,xmm3
00007FFDF74EA23C vmulps    xmm1,xmm1,xmm1
00007FFDF74EA240 vmulps    xmm3,xmm4,xmm4
00007FFDF74EA244 vsubps    xmm1,xmm1,xmm3
00007FFDF74EA248 vmulps    xmm3,xmm6,xmm6
00007FFDF74EA24C vaddps    xmm1,xmm1,xmm3
00007FFDF74EA250 vmulps    xmm3,xmm5,xmm5
00007FFDF74EA254 vsubps    xmm1,xmm1,xmm3
00007FFDF74EA258 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74EA25D vmulps    xmm2,xmm2,xmm3
00007FFDF74EA261 vshufps   xmm3,xmm0,xmm0,9Ch
00007FFDF74EA266 vmulps    xmm3,xmm7,xmm3
00007FFDF74EA26A vaddps    xmm2,xmm2,xmm3
00007FFDF74EA26E vmulps    xmm0,xmm1,xmm0
00007FFDF74EA272 vaddps    xmm2,xmm2,xmm0
00007FFDF74EA276 vmovupd   [rdx],xmm2
00007FFDF74EA27A mov       rax,rdx
00007FFDF74EA27D vmovaps   xmm6,[rsp+40h]
00007FFDF74EA283 vmovaps   xmm7,[rsp+30h]
00007FFDF74EA289 vmovaps   xmm8,[rsp+20h]
00007FFDF74EA28F vmovaps   xmm9,[rsp+10h]
00007FFDF74EA295 vmovaps   xmm10,[rsp]
00007FFDF74EA29A add       rsp,58h
00007FFDF74EA29E ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_Addition(Rotor, Rotor)
00007FFDF74EA310 vzeroupper
00007FFDF74EA313 xchg      ax,ax
00007FFDF74EA315 vmovupd   xmm0,[rdx]
00007FFDF74EA319 vmovupd   xmm1,[r8]
00007FFDF74EA31E vaddps    xmm0,xmm0,xmm1
00007FFDF74EA322 vmovupd   [rcx],xmm0
00007FFDF74EA326 mov       rax,rcx
00007FFDF74EA329 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_Subtraction(Rotor, Rotor)
00007FFDF74EA340 vzeroupper
00007FFDF74EA343 xchg      ax,ax
00007FFDF74EA345 vmovupd   xmm0,[rdx]
00007FFDF74EA349 vmovupd   xmm1,[r8]
00007FFDF74EA34E vsubps    xmm0,xmm0,xmm1
00007FFDF74EA352 vmovupd   [rcx],xmm0
00007FFDF74EA356 mov       rax,rcx
00007FFDF74EA359 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_Multiply(Rotor, Single)
00007FFDF74EA370 vzeroupper
00007FFDF74EA373 xchg      ax,ax
00007FFDF74EA375 vmovupd   xmm0,[rdx]
00007FFDF74EA379 vbroadcastss xmm1,xmm2
00007FFDF74EA37E vmulps    xmm0,xmm0,xmm1
00007FFDF74EA382 vmovupd   [rcx],xmm0
00007FFDF74EA386 mov       rax,rcx
00007FFDF74EA389 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_Multiply(Single, Rotor)
00007FFDF74EA3A0 vzeroupper
00007FFDF74EA3A3 xchg      ax,ax
00007FFDF74EA3A5 vmovupd   xmm0,[r8]
00007FFDF74EA3AA vbroadcastss xmm1,xmm1
00007FFDF74EA3AF vmulps    xmm0,xmm0,xmm1
00007FFDF74EA3B3 vmovupd   [rcx],xmm0
00007FFDF74EA3B7 mov       rax,rcx
00007FFDF74EA3BA ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_Multiply(Rotor, Rotor)
00007FFDF74EA3D0 vzeroupper
00007FFDF74EA3D3 xchg      ax,ax
00007FFDF74EA3D5 vmovupd   xmm0,[rdx]
00007FFDF74EA3D9 vmovupd   xmm1,[r8]
00007FFDF74EA3DE vshufps   xmm2,xmm0,xmm0,0
00007FFDF74EA3E3 vmulps    xmm2,xmm2,xmm1
00007FFDF74EA3E7 vshufps   xmm3,xmm0,xmm0,79h
00007FFDF74EA3EC vshufps   xmm4,xmm1,xmm1,9Dh
00007FFDF74EA3F1 vmulps    xmm3,xmm3,xmm4
00007FFDF74EA3F5 vsubps    xmm2,xmm2,xmm3
00007FFDF74EA3F9 vshufps   xmm3,xmm0,xmm0,0E6h
00007FFDF74EA3FE vshufps   xmm4,xmm1,xmm1,2
00007FFDF74EA403 vmulps    xmm3,xmm3,xmm4
00007FFDF74EA407 vshufps   xmm0,xmm0,xmm0,9Fh
00007FFDF74EA40C vshufps   xmm1,xmm1,xmm1,7Bh
00007FFDF74EA411 vmulps    xmm0,xmm0,xmm1
00007FFDF74EA415 vaddps    xmm0,xmm3,xmm0
00007FFDF74EA419 vxorps    xmm1,xmm1,xmm1
00007FFDF74EA41D vmovss    xmm3,[rel 7FFD`F74E`A450h]
00007FFDF74EA425 vmovss    xmm1,xmm1,xmm3
00007FFDF74EA429 vxorps    xmm0,xmm0,xmm1
00007FFDF74EA42D vaddps    xmm0,xmm2,xmm0
00007FFDF74EA431 vmovupd   [rcx],xmm0
00007FFDF74EA435 mov       rax,rcx
00007FFDF74EA438 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Motor op_Multiply(Rotor, Translator)
00007FFDF74EA470 vzeroupper
00007FFDF74EA473 xchg      ax,ax
00007FFDF74EA475 vmovupd   xmm0,[rdx]
00007FFDF74EA479 vmovaps   xmm1,xmm0
00007FFDF74EA47D vmovupd   xmm2,[r8]
00007FFDF74EA482 vshufps   xmm3,xmm1,xmm1,1
00007FFDF74EA487 vshufps   xmm4,xmm2,xmm2,0E5h
00007FFDF74EA48C vmulps    xmm3,xmm3,xmm4
00007FFDF74EA490 vshufps   xmm4,xmm1,xmm1,9Eh
00007FFDF74EA495 vshufps   xmm5,xmm2,xmm2,7Ah
00007FFDF74EA49A vmulps    xmm4,xmm4,xmm5
00007FFDF74EA49E vaddps    xmm3,xmm3,xmm4
00007FFDF74EA4A2 vxorps    xmm4,xmm4,xmm4
00007FFDF74EA4A6 vmovss    xmm5,[rel 7FFD`F74E`A4E8h]
00007FFDF74EA4AE vmovss    xmm4,xmm4,xmm5
00007FFDF74EA4B2 vshufps   xmm1,xmm1,xmm1,7Bh
00007FFDF74EA4B7 vshufps   xmm2,xmm2,xmm2,9Fh
00007FFDF74EA4BC vmulps    xmm1,xmm1,xmm2
00007FFDF74EA4C0 vxorps    xmm1,xmm4,xmm1
00007FFDF74EA4C4 vsubps    xmm3,xmm3,xmm1
00007FFDF74EA4C8 vmovupd   [rcx],xmm0
00007FFDF74EA4CC vmovupd   [rcx+10h],xmm3
00007FFDF74EA4D1 mov       rax,rcx
00007FFDF74EA4D4 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Motor op_Multiply(Rotor, Motor)
00007FFDF74EA500 sub       rsp,28h
00007FFDF74EA504 vzeroupper
00007FFDF74EA507 vmovaps   [rsp+10h],xmm6
00007FFDF74EA50D vmovaps   [rsp],xmm7
00007FFDF74EA512 vmovupd   xmm0,[rdx]
00007FFDF74EA516 vmovupd   xmm1,[r8]
00007FFDF74EA51B vshufps   xmm2,xmm0,xmm0,0
00007FFDF74EA520 vmulps    xmm2,xmm2,xmm1
00007FFDF74EA524 vshufps   xmm3,xmm0,xmm0,79h
00007FFDF74EA529 vshufps   xmm4,xmm1,xmm1,9Dh
00007FFDF74EA52E vmulps    xmm3,xmm3,xmm4
00007FFDF74EA532 vsubps    xmm2,xmm2,xmm3
00007FFDF74EA536 vshufps   xmm3,xmm0,xmm0,0E6h
00007FFDF74EA53B vshufps   xmm4,xmm1,xmm1,2
00007FFDF74EA540 vmulps    xmm3,xmm3,xmm4
00007FFDF74EA544 vshufps   xmm4,xmm0,xmm0,9Fh
00007FFDF74EA549 vshufps   xmm1,xmm1,xmm1,7Bh
00007FFDF74EA54E vmulps    xmm1,xmm4,xmm1
00007FFDF74EA552 vaddps    xmm1,xmm3,xmm1
00007FFDF74EA556 vxorps    xmm3,xmm3,xmm3
00007FFDF74EA55A vmovss    xmm4,[rel 7FFD`F74E`A630h]
00007FFDF74EA562 vmovss    xmm3,xmm3,xmm4
00007FFDF74EA566 vxorps    xmm1,xmm1,xmm3
00007FFDF74EA56A vaddps    xmm1,xmm2,xmm1
00007FFDF74EA56E vmovupd   xmm2,[r8+10h]
00007FFDF74EA574 vmovaps   xmm3,xmm0
00007FFDF74EA578 vmovaps   xmm4,xmm2
00007FFDF74EA57C vshufps   xmm5,xmm3,xmm3,1
00007FFDF74EA581 vshufps   xmm6,xmm4,xmm4,0E5h
00007FFDF74EA586 vmulps    xmm5,xmm5,xmm6
00007FFDF74EA58A vshufps   xmm6,xmm3,xmm3,9Eh
00007FFDF74EA58F vshufps   xmm7,xmm4,xmm4,7Ah
00007FFDF74EA594 vmulps    xmm6,xmm6,xmm7
00007FFDF74EA598 vaddps    xmm5,xmm5,xmm6
00007FFDF74EA59C vxorps    xmm6,xmm6,xmm6
00007FFDF74EA5A0 vmovss    xmm7,[rel 7FFD`F74E`A634h]
00007FFDF74EA5A8 vmovss    xmm6,xmm6,xmm7
00007FFDF74EA5AC vshufps   xmm3,xmm3,xmm3,7Bh
00007FFDF74EA5B1 vshufps   xmm4,xmm4,xmm4,9Fh
00007FFDF74EA5B6 vmulps    xmm3,xmm3,xmm4
00007FFDF74EA5BA vxorps    xmm3,xmm6,xmm3
00007FFDF74EA5BE vsubps    xmm5,xmm5,xmm3
00007FFDF74EA5C2 vxorps    xmm3,xmm3,xmm3
00007FFDF74EA5C6 vmovss    xmm4,[rel 7FFD`F74E`A638h]
00007FFDF74EA5CE vmovss    xmm3,xmm3,xmm4
00007FFDF74EA5D2 vshufps   xmm2,xmm2,xmm2,0
00007FFDF74EA5D7 vmulps    xmm0,xmm0,xmm2
00007FFDF74EA5DB vxorps    xmm0,xmm3,xmm0
00007FFDF74EA5DF vsubps    xmm0,xmm5,xmm0
00007FFDF74EA5E3 vmovupd   [rcx],xmm1
00007FFDF74EA5E7 vmovupd   [rcx+10h],xmm0
00007FFDF74EA5EC mov       rax,rcx
00007FFDF74EA5EF vmovaps   xmm6,[rsp+10h]
00007FFDF74EA5F5 vmovaps   xmm7,[rsp]
00007FFDF74EA5FA add       rsp,28h
00007FFDF74EA5FE ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_Division(Rotor, Single)
00007FFDF74EA660 vzeroupper
00007FFDF74EA663 xchg      ax,ax
00007FFDF74EA665 vmovupd   xmm0,[rdx]
00007FFDF74EA669 vbroadcastss xmm1,xmm2
00007FFDF74EA66E vrcpps    xmm2,xmm1
00007FFDF74EA672 vmulps    xmm1,xmm1,xmm2
00007FFDF74EA676 vmovss    xmm3,[rel 7FFD`F74E`A6A8h]
00007FFDF74EA67E vbroadcastss xmm3,xmm3
00007FFDF74EA683 vsubps    xmm1,xmm3,xmm1
00007FFDF74EA687 vmulps    xmm1,xmm2,xmm1
00007FFDF74EA68B vmulps    xmm0,xmm0,xmm1
00007FFDF74EA68F vmovupd   [rcx],xmm0
00007FFDF74EA693 mov       rax,rcx
00007FFDF74EA696 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_OnesComplement(Rotor)
00007FFDF74EA6C0 vzeroupper
00007FFDF74EA6C3 xchg      ax,ax
00007FFDF74EA6C5 vxorps    xmm0,xmm0,xmm0
00007FFDF74EA6C9 vmovss    xmm1,[rel 7FFD`F74E`A710h]
00007FFDF74EA6D1 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74EA6D7 vmovss    xmm1,[rel 7FFD`F74E`A714h]
00007FFDF74EA6DF vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74EA6E5 vmovss    xmm1,[rel 7FFD`F74E`A718h]
00007FFDF74EA6ED vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74EA6F3 vmovupd   xmm1,[rdx]
00007FFDF74EA6F7 vxorps    xmm0,xmm1,xmm0
00007FFDF74EA6FB vmovupd   [rcx],xmm0
00007FFDF74EA6FF mov       rax,rcx
00007FFDF74EA702 ret

# -----------------------------------------------------------------------------------

# [struct Rotor] Rotor op_UnaryNegation(Rotor)
00007FFDF74EA730 vzeroupper
00007FFDF74EA733 xchg      ax,ax
00007FFDF74EA735 vmovupd   xmm0,[rdx]
00007FFDF74EA739 vmovss    xmm1,[rel 7FFD`F74E`A758h]
00007FFDF74EA741 vbroadcastss xmm1,xmm1
00007FFDF74EA746 vxorps    xmm0,xmm0,xmm1
00007FFDF74EA74A vmovupd   [rcx],xmm0
00007FFDF74EA74E mov       rax,rcx
00007FFDF74EA751 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator LoadNormalized(Single*)
00007FFDF74EA770 vzeroupper
00007FFDF74EA773 xchg      ax,ax
00007FFDF74EA775 vmovups   xmm0,[rdx]
00007FFDF74EA779 vmovupd   [rcx],xmm0
00007FFDF74EA77D mov       rax,rcx
00007FFDF74EA780 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator LoadNormalized(System.ReadOnlySpan`1[System.Single])
00007FFDF74EA7A0 push      rsi
00007FFDF74EA7A1 sub       rsp,30h
00007FFDF74EA7A5 vzeroupper
00007FFDF74EA7A8 xor       eax,eax
00007FFDF74EA7AA mov       [rsp+28h],rax
00007FFDF74EA7AF mov       rax,[rdx]
00007FFDF74EA7B2 mov       edx,[rdx+8]
00007FFDF74EA7B5 xor       r8d,r8d
00007FFDF74EA7B8 mov       [rsp+28h],r8
00007FFDF74EA7BD cmp       edx,4
00007FFDF74EA7C0 jl        short 0000`7FFD`F74E`A7EAh
00007FFDF74EA7C2 xor       r8d,r8d
00007FFDF74EA7C5 test      edx,edx
00007FFDF74EA7C7 je        short 0000`7FFD`F74E`A7CCh
00007FFDF74EA7C9 mov       r8,rax
00007FFDF74EA7CC mov       [rsp+28h],r8
00007FFDF74EA7D1 vmovups   xmm0,[r8]
00007FFDF74EA7D6 xor       eax,eax
00007FFDF74EA7D8 mov       [rsp+28h],rax
00007FFDF74EA7DD vmovupd   [rcx],xmm0
00007FFDF74EA7E1 mov       rax,rcx
00007FFDF74EA7E4 add       rsp,30h
00007FFDF74EA7E8 pop       rsi
00007FFDF74EA7E9 ret
00007FFDF74EA7EA mov       rcx,7FFD`F72F`D4C0h
00007FFDF74EA7F4 call      0000`7FFE`56D7`7710h
00007FFDF74EA7F9 mov       rsi,rax
00007FFDF74EA7FC mov       ecx,33h
00007FFDF74EA801 mov       rdx,7FFD`F731`9EA0h
00007FFDF74EA80B call      0000`7FFE`56EA`03E0h
00007FFDF74EA810 mov       rdx,rax
00007FFDF74EA813 mov       rcx,rsi
00007FFDF74EA816 call      0000`7FFD`F725`D238h
00007FFDF74EA81B mov       rcx,rsi
00007FFDF74EA81E call      0000`7FFE`56D3`B3A0h
00007FFDF74EA823 int3

# -----------------------------------------------------------------------------------

# [struct Translator] Void Deconstruct(Single ByRef, Single ByRef, Single ByRef)
00007FFDF74EA840 vzeroupper
00007FFDF74EA843 xchg      ax,ax
00007FFDF74EA845 vmovupd   xmm0,[rcx]
00007FFDF74EA849 vextractps eax,xmm0,1
00007FFDF74EA84F vmovd     xmm0,eax
00007FFDF74EA853 vmovss    [rdx],xmm0
00007FFDF74EA857 vmovupd   xmm0,[rcx]
00007FFDF74EA85B vextractps eax,xmm0,2
00007FFDF74EA861 vmovd     xmm0,eax
00007FFDF74EA865 vmovss    [r8],xmm0
00007FFDF74EA86A vmovupd   xmm0,[rcx]
00007FFDF74EA86E vextractps eax,xmm0,3
00007FFDF74EA874 vmovd     xmm0,eax
00007FFDF74EA878 vmovss    [r9],xmm0
00007FFDF74EA87D ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator Inverse()
00007FFDF74EA8A0 vzeroupper
00007FFDF74EA8A3 xchg      ax,ax
00007FFDF74EA8A5 vxorps    xmm0,xmm0,xmm0
00007FFDF74EA8A9 vmovss    xmm1,[rel 7FFD`F74E`A8F0h]
00007FFDF74EA8B1 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74EA8B7 vmovss    xmm1,[rel 7FFD`F74E`A8F4h]
00007FFDF74EA8BF vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74EA8C5 vmovss    xmm1,[rel 7FFD`F74E`A8F8h]
00007FFDF74EA8CD vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74EA8D3 vmovupd   xmm1,[rcx]
00007FFDF74EA8D7 vxorps    xmm0,xmm0,xmm1
00007FFDF74EA8DB vmovupd   [rdx],xmm0
00007FFDF74EA8DF mov       rax,rdx
00007FFDF74EA8E2 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Plane Conjugate(Plane)
00007FFDF74EAD20 vzeroupper
00007FFDF74EAD23 xchg      ax,ax
00007FFDF74EAD25 vmovupd   xmm0,[rcx]
00007FFDF74EAD29 vxorps    xmm1,xmm1,xmm1
00007FFDF74EAD2D vmovss    xmm2,[rel 7FFD`F74E`ADB8h]
00007FFDF74EAD35 vmovss    xmm1,xmm1,xmm2
00007FFDF74EAD39 vblendps  xmm0,xmm0,xmm1,1
00007FFDF74EAD3F vmovupd   xmm1,[r8]
00007FFDF74EAD44 vmovaps   xmm2,xmm1
00007FFDF74EAD48 vmovaps   xmm3,xmm0
00007FFDF74EAD4C vdpps     xmm2,xmm2,xmm3,0E1h
00007FFDF74EAD52 vrcpps    xmm3,xmm0
00007FFDF74EAD56 vmulps    xmm0,xmm0,xmm3
00007FFDF74EAD5A vmovss    xmm4,[rel 7FFD`F74E`ADBCh]
00007FFDF74EAD62 vbroadcastss xmm4,xmm4
00007FFDF74EAD67 vsubps    xmm0,xmm4,xmm0
00007FFDF74EAD6B vmulps    xmm0,xmm3,xmm0
00007FFDF74EAD6F vaddss    xmm0,xmm0,xmm0
00007FFDF74EAD73 mov       eax,0`FFFF`FFFFh
00007FFDF74EAD78 vmovd     xmm3,eax
00007FFDF74EAD7C xor       eax,eax
00007FFDF74EAD7E vpinsrd   xmm3,xmm3,eax,1
00007FFDF74EAD84 vpinsrd   xmm3,xmm3,eax,2
00007FFDF74EAD8A vpinsrd   xmm3,xmm3,eax,3
00007FFDF74EAD90 vandps    xmm0,xmm0,xmm3
00007FFDF74EAD94 vmulss    xmm2,xmm2,xmm0
00007FFDF74EAD98 vaddps    xmm0,xmm1,xmm2
00007FFDF74EAD9C vmovupd   [rdx],xmm0
00007FFDF74EADA0 mov       rax,rdx
00007FFDF74EADA3 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Line Conjugate(Line ByRef)
00007FFDF74EAEE0 vzeroupper
00007FFDF74EAEE3 xchg      ax,ax
00007FFDF74EAEE5 vmovupd   xmm0,[r8]
00007FFDF74EAEEA vmovupd   xmm1,[r8+10h]
00007FFDF74EAEF0 vmovupd   xmm2,[rcx]
00007FFDF74EAEF4 vshufps   xmm3,xmm0,xmm0,78h
00007FFDF74EAEF9 vshufps   xmm4,xmm2,xmm2,9Ch
00007FFDF74EAEFE vmulps    xmm3,xmm3,xmm4
00007FFDF74EAF02 vshufps   xmm4,xmm0,xmm0,9Ch
00007FFDF74EAF07 vshufps   xmm5,xmm2,xmm2,78h
00007FFDF74EAF0C vmulps    xmm4,xmm4,xmm5
00007FFDF74EAF10 vsubps    xmm3,xmm3,xmm4
00007FFDF74EAF14 vshufps   xmm2,xmm2,xmm2,0
00007FFDF74EAF19 vmulps    xmm2,xmm0,xmm2
00007FFDF74EAF1D vxorps    xmm4,xmm4,xmm4
00007FFDF74EAF21 vmovss    xmm5,[rel 7FFD`F74E`AF60h]
00007FFDF74EAF29 vmovss    xmm4,xmm4,xmm5
00007FFDF74EAF2D vxorps    xmm2,xmm2,xmm4
00007FFDF74EAF31 vsubps    xmm3,xmm3,xmm2
00007FFDF74EAF35 vaddps    xmm3,xmm3,xmm3
00007FFDF74EAF39 vaddps    xmm3,xmm3,xmm1
00007FFDF74EAF3D vmovupd   [rdx],xmm0
00007FFDF74EAF41 vmovupd   [rdx+10h],xmm3
00007FFDF74EAF46 mov       rax,rdx
00007FFDF74EAF49 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Point Conjugate(Point)
00007FFDF74EAF80 vzeroupper
00007FFDF74EAF83 xchg      ax,ax
00007FFDF74EAF85 vmovupd   xmm0,[r8]
00007FFDF74EAF8A vmovupd   xmm1,[rcx]
00007FFDF74EAF8E vshufps   xmm2,xmm0,xmm0,0
00007FFDF74EAF93 vmulps    xmm1,xmm2,xmm1
00007FFDF74EAF97 vxorps    xmm2,xmm2,xmm2
00007FFDF74EAF9B vmovss    xmm3,[rel 7FFD`F74E`AFE0h]
00007FFDF74EAFA3 vinsertps xmm2,xmm2,xmm3,10h
00007FFDF74EAFA9 vmovss    xmm3,[rel 7FFD`F74E`AFE4h]
00007FFDF74EAFB1 vinsertps xmm2,xmm2,xmm3,20h
00007FFDF74EAFB7 vmovss    xmm3,[rel 7FFD`F74E`AFE8h]
00007FFDF74EAFBF vinsertps xmm2,xmm2,xmm3,30h
00007FFDF74EAFC5 vmulps    xmm1,xmm2,xmm1
00007FFDF74EAFC9 vaddps    xmm1,xmm0,xmm1
00007FFDF74EAFCD vmovupd   [rdx],xmm1
00007FFDF74EAFD1 mov       rax,rdx
00007FFDF74EAFD4 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Addition(Translator, Translator)
00007FFDF74EB000 vzeroupper
00007FFDF74EB003 xchg      ax,ax
00007FFDF74EB005 vmovupd   xmm0,[rdx]
00007FFDF74EB009 vmovupd   xmm1,[r8]
00007FFDF74EB00E vaddps    xmm0,xmm0,xmm1
00007FFDF74EB012 vmovupd   [rcx],xmm0
00007FFDF74EB016 mov       rax,rcx
00007FFDF74EB019 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Subtraction(Translator, Translator)
00007FFDF74EB030 vzeroupper
00007FFDF74EB033 xchg      ax,ax
00007FFDF74EB035 vmovupd   xmm0,[rdx]
00007FFDF74EB039 vmovupd   xmm1,[r8]
00007FFDF74EB03E vsubps    xmm0,xmm0,xmm1
00007FFDF74EB042 vmovupd   [rcx],xmm0
00007FFDF74EB046 mov       rax,rcx
00007FFDF74EB049 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Multiply(Translator, Single)
00007FFDF74EB060 vzeroupper
00007FFDF74EB063 xchg      ax,ax
00007FFDF74EB065 vmovupd   xmm0,[rdx]
00007FFDF74EB069 vbroadcastss xmm1,xmm2
00007FFDF74EB06E vmulps    xmm0,xmm0,xmm1
00007FFDF74EB072 vmovupd   [rcx],xmm0
00007FFDF74EB076 mov       rax,rcx
00007FFDF74EB079 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Multiply(Single, Translator)
00007FFDF74EB090 vzeroupper
00007FFDF74EB093 xchg      ax,ax
00007FFDF74EB095 vmovupd   xmm0,[r8]
00007FFDF74EB09A vbroadcastss xmm1,xmm1
00007FFDF74EB09F vmulps    xmm0,xmm0,xmm1
00007FFDF74EB0A3 vmovupd   [rcx],xmm0
00007FFDF74EB0A7 mov       rax,rcx
00007FFDF74EB0AA ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Division(Translator, Single)
00007FFDF74EB0C0 vzeroupper
00007FFDF74EB0C3 xchg      ax,ax
00007FFDF74EB0C5 vmovupd   xmm0,[rdx]
00007FFDF74EB0C9 vbroadcastss xmm1,xmm2
00007FFDF74EB0CE vrcpps    xmm2,xmm1
00007FFDF74EB0D2 vmulps    xmm1,xmm1,xmm2
00007FFDF74EB0D6 vmovss    xmm3,[rel 7FFD`F74E`B108h]
00007FFDF74EB0DE vbroadcastss xmm3,xmm3
00007FFDF74EB0E3 vsubps    xmm1,xmm3,xmm1
00007FFDF74EB0E7 vmulps    xmm1,xmm2,xmm1
00007FFDF74EB0EB vmulps    xmm0,xmm0,xmm1
00007FFDF74EB0EF vmovupd   [rcx],xmm0
00007FFDF74EB0F3 mov       rax,rcx
00007FFDF74EB0F6 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Division(Translator, Translator)
00007FFDF74EB120 vzeroupper
00007FFDF74EB123 xchg      ax,ax
00007FFDF74EB125 vxorps    xmm0,xmm0,xmm0
00007FFDF74EB129 vmovss    xmm1,[rel 7FFD`F74E`B178h]
00007FFDF74EB131 vinsertps xmm0,xmm0,xmm1,10h
00007FFDF74EB137 vmovss    xmm1,[rel 7FFD`F74E`B17Ch]
00007FFDF74EB13F vinsertps xmm0,xmm0,xmm1,20h
00007FFDF74EB145 vmovss    xmm1,[rel 7FFD`F74E`B180h]
00007FFDF74EB14D vinsertps xmm0,xmm0,xmm1,30h
00007FFDF74EB153 vmovupd   xmm1,[r8]
00007FFDF74EB158 vxorps    xmm0,xmm0,xmm1
00007FFDF74EB15C vmovupd   xmm1,[rdx]
00007FFDF74EB160 vaddps    xmm0,xmm1,xmm0
00007FFDF74EB164 vmovupd   [rcx],xmm0
00007FFDF74EB168 mov       rax,rcx
00007FFDF74EB16B ret

# -----------------------------------------------------------------------------------

# [struct Translator] Motor op_Multiply(Translator, Rotor)
00007FFDF74EB1A0 vzeroupper
00007FFDF74EB1A3 xchg      ax,ax
00007FFDF74EB1A5 vmovupd   xmm0,[r8]
00007FFDF74EB1AA vmovaps   xmm1,xmm0
00007FFDF74EB1AE vmovupd   xmm2,[rdx]
00007FFDF74EB1B2 vshufps   xmm3,xmm0,xmm0,1
00007FFDF74EB1B7 vshufps   xmm4,xmm2,xmm2,0E5h
00007FFDF74EB1BC vmulps    xmm3,xmm3,xmm4
00007FFDF74EB1C0 vshufps   xmm4,xmm0,xmm0,7Ah
00007FFDF74EB1C5 vshufps   xmm5,xmm2,xmm2,9Eh
00007FFDF74EB1CA vmulps    xmm4,xmm4,xmm5
00007FFDF74EB1CE vaddps    xmm3,xmm3,xmm4
00007FFDF74EB1D2 vxorps    xmm4,xmm4,xmm4
00007FFDF74EB1D6 vmovss    xmm5,[rel 7FFD`F74E`B218h]
00007FFDF74EB1DE vmovss    xmm4,xmm4,xmm5
00007FFDF74EB1E2 vshufps   xmm0,xmm0,xmm0,9Fh
00007FFDF74EB1E7 vshufps   xmm2,xmm2,xmm2,7Bh
00007FFDF74EB1EC vmulps    xmm0,xmm0,xmm2
00007FFDF74EB1F0 vxorps    xmm0,xmm4,xmm0
00007FFDF74EB1F4 vsubps    xmm3,xmm3,xmm0
00007FFDF74EB1F8 vmovupd   [rcx],xmm1
00007FFDF74EB1FC vmovupd   [rcx+10h],xmm3
00007FFDF74EB201 mov       rax,rcx
00007FFDF74EB204 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Translator op_Multiply(Translator, Translator)
00007FFDF74EB230 vzeroupper
00007FFDF74EB233 xchg      ax,ax
00007FFDF74EB235 vmovupd   xmm0,[rdx]
00007FFDF74EB239 vmovupd   xmm1,[r8]
00007FFDF74EB23E vaddps    xmm0,xmm0,xmm1
00007FFDF74EB242 vmovupd   [rcx],xmm0
00007FFDF74EB246 mov       rax,rcx
00007FFDF74EB249 ret

# -----------------------------------------------------------------------------------

# [struct Translator] Motor op_Multiply(Translator, Motor)
00007FFDF74EB260 sub       rsp,18h
00007FFDF74EB264 vzeroupper
00007FFDF74EB267 vmovaps   [rsp],xmm6
00007FFDF74EB26C vmovupd   xmm0,[r8]
00007FFDF74EB271 vmovupd   xmm1,[r8+10h]
00007FFDF74EB277 vmovaps   xmm2,xmm0
00007FFDF74EB27B vmovupd   xmm3,[rdx]
00007FFDF74EB27F vshufps   xmm4,xmm2,xmm2,1
00007FFDF74EB284 vshufps   xmm5,xmm3,xmm3,0E5h
00007FFDF74EB289 vmulps    xmm4,xmm4,xmm5
00007FFDF74EB28D vshufps   xmm5,xmm2,xmm2,7Ah
00007FFDF74EB292 vshufps   xmm6,xmm3,xmm3,9Eh
00007FFDF74EB297 vmulps    xmm5,xmm5,xmm6
00007FFDF74EB29B vaddps    xmm4,xmm4,xmm5
00007FFDF74EB29F vxorps    xmm5,xmm5,xmm5
00007FFDF74EB2A3 vmovss    xmm6,[rel 7FFD`F74E`B2F8h]
00007FFDF74EB2AB vmovss    xmm5,xmm5,xmm6
00007FFDF74EB2AF vshufps   xmm2,xmm2,xmm2,9Fh
00007FFDF74EB2B4 vshufps   xmm3,xmm3,xmm3,7Bh
00007FFDF74EB2B9 vmulps    xmm2,xmm2,xmm3
00007FFDF74EB2BD vxorps    xmm2,xmm5,xmm2
00007FFDF74EB2C1 vsubps    xmm4,xmm4,xmm2
00007FFDF74EB2C5 vaddps    xmm1,xmm4,xmm1
00007FFDF74EB2C9 vmovupd   [rcx],xmm0
00007FFDF74EB2CD vmovupd   [rcx+10h],xmm1
00007FFDF74EB2D2 mov       rax,rcx
00007FFDF74EB2D5 vmovaps   xmm6,[rsp]
00007FFDF74EB2DA add       rsp,18h
00007FFDF74EB2DE ret
```


grammar Fountain;

//
// Parser Rules
//

// ======
// Dialog
// ======
dialog
	:	character EOL1 dialogBlock EOL2
	;

character
	:	USPAN
	|	'@' span
	;

dialogBlock
	:	dialogLine ( EOL1 dialogLine )*
	;

dialogLine
	:	( parenthetical | span )
	;

parenthetical
	:	PARENTHETICAL
	;

// ===========
// Other Rules
// ===========

action
	:	'!' span
	|	span
	;

centered
	:	'>' span '<'
	;

heading
	:	'.' span
	|	HEADING
	;

lyric
	:	'~' span
	;

pageBreak
	:	'===' '='*
	;

section
	:	'#'+ span
	;

synopsis
	:	'=' span
	;

transition
	:	'>' span
	|	TO
	;

titlePage
	:	TitlePageValue
	;

span // HACK: I hate '...' and how it breaks the forced heading rule.
	: ( '..' '.'* )? ( USPAN | SPAN )
	;


compileUnit
	:
	(	centered
	|	lyric
	|	pageBreak
	|	section
	|	synopsis
	|	transition
	|	heading
	|	dialog
	|	action
	|	titlePage
	|	EOL1
	|	EOL2
	)*	EOF
	;

//
// Lexer Rules
//

EOL2
	:	'\r'? '\n' '\r'? '\n'
	;

EOL1
	:	'\r'? '\n'
	;

BONEYARD
	:	'/*' .*? '*/' -> skip
	;

NOTE
	:	'[[' .*? ']]' -> skip
	;

HEADING
	:	[iI][nN][tT]         ( '.' | ' ' ) ( USPAN | SPAN ) // INT. NIGHT      || INT NIGHT
	|	[eE][xX][tT]         ( '.' | ' ' ) ( USPAN | SPAN ) // EXT. NIGHT      || EXT NIGHT
	|	[eE][sS][tT]         ( '.' | ' ' ) ( USPAN | SPAN ) // EST. NIGHT      || EST NIGHT
	|	[iI][nN][tT] '.'? '/' [eE][xX][tT] ( '.' | ' ' ) ( USPAN | SPAN ) // INT./EXT. NIGHT || INT./EXT NIGHT || INT/EXT NIGHT   || INT/EXT. NIGHT
	|	[iI]        '/' [eE] ( '.' | ' ' ) ( USPAN | SPAN ) // I/E. NIGHT      || I/E NIGHT
	;

TO
	:	( USPAN | SPAN ) [tT][oO] ':'
	|	[fF][aA][dD][eE]' '[oO][uU][tT]'.'
	|	[fF][aA][dD][eE]' '[tT][oO]' '[bB][lL][aA][cC][kK]'.'
	|	[cC][uU][tT]' '[tT][oO]' '[bB][lL][aA][cC][kK]'.'
	;

PARENTHETICAL
	:	'(' ( USPAN | SPAN ) ')'
	;

fragment UFIRST
	:	~[\r\n\t#=~!@><\.a-z]
	;

USPAN
	:	UFIRST ~( [\r\n\t\<] | [a-z] )+
	;

TitlePageValue
	: TitlePageKey .*? ('\r'?'\n' '\r'?'\n')
	;
TitlePageKey
	:	[tT][iI][tT][lL][eE]':'
	|	[cC][rR][eE][dD][iI][tT]':'
	|	[aA][uU][tT][hH][oO][rR]':'
	|	[sS][oO][uU][rR][cC][eE]':'
	;

fragment FIRST
	:	~[\r\n\t#=~!@><\.]
	;

SPAN
	:	FIRST ~( [\r\n\t\<] )+
	;

WS
	:	[ \t] -> skip
	;

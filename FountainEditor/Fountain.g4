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
	:	'(' span ')'
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
	|	span TO
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
	:	'INT'                ( '.' | ' ' ) ( USPAN | SPAN ) // INT. NIGHT      || INT NIGHT
	|	'EXT'                ( '.' | ' ' ) ( USPAN | SPAN ) // EXT. NIGHT      || EXT NIGHT
	|	'EST'                ( '.' | ' ' ) ( USPAN | SPAN ) // EST. NIGHT      || EST NIGHT
	|	'INT' '.'? '/' 'EXT' ( '.' | ' ' ) ( USPAN | SPAN ) // INT./EXT. NIGHT || INT./EXT NIGHT || INT/EXT NIGHT   || INT/EXT. NIGHT
	|	'I'        '/' 'E'   ( '.' | ' ' ) ( USPAN | SPAN ) // I/E. NIGHT      || I/E NIGHT
	;

TO
	:	[tT][oO] ':'
	;

fragment UFIRST
	:	~[\r\n\t#=~!@><\.a-z]
	;

USPAN
	:	UFIRST ~( [\r\n\t\<] | [a-z] )+
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

grammar Fountain;

//
// Parser Rules
//

boneyard	:	Boneyard	;
centered	:	Centered	;
heading		:	Heading		;
lyric		:	Lyric		;
note		:	Note		;
pageBreak	:	PageBreak	;
section		:	Section		;
span		:	Span		;
synopsis	:	Synopsis	;
transition	:	Transition	;

character	:	Character EOL (Parenthetical | Span | EOL)* ~(BlankLine)	;
upperCaseLine:	Character EOL BlankLine	;

compileUnit
	:	( boneyard | centered | heading | lyric | note | pageBreak | section | span | synopsis | transition | character | upperCaseLine )* EOF
	;

//
// Lexer Rules
//

fragment Ext
	: [eE] [xX] [tT]
	;

fragment Int
	: [iI] [nN] [tT]
	;

Heading
	:	( Ext  |  Int ) '.'      Span
	|	  Ext '/' Int   '.'      Span
	|	  Int '/' Ext   '.'      Span
	|	 [iI] '/' [eE]  '.'      Span
	|	                '.' ~'.' Span
	;

PageBreak
	:	'===' '='*
	;

Boneyard
	:	'/*' .*? '*/'
	;

// TODO: Check for double return
Note
	:	'[[' .*? ']]'
	;

Centered
	:	'>' ~[\t\r\n]*? '<'
	;
	
Lyric
	:	'~' Span
	;

Section
	:	'#'+ Span
	;

Synopsis
	:	'=' Span
	;

Transition
	:	Span [tT] [oO] ':'
	|	'>' Span
	;

Character
	:	~[a-z\t\r\n]+ '^'?
	|	'@' Span
	;

Parenthetical
	: '(' ~[\r\n] ')'
	;

BlankLine
	: {Column == 0}? EOL
	;

Span
	:	~[\t\r\n]+
	;

EOL
	:	'\r'? '\n'
	;

WS
	:	[ \t] -> skip
	;

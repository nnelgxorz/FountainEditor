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
blankLine	:	BlankLine	;
parenthetical:	Parenthetical	;
character	:	Character EOL (Parenthetical | Span | EOL)* ~(BlankLine)	;
upperCaseLine:	Character EOL BlankLine	;
titlePage	:	TitlePageValue+ ~(BlankLine)	;

compileUnit
	:	( boneyard | centered | heading | lyric | note | pageBreak | section | span | synopsis | 
		titlePage | transition | character | upperCaseLine | blankLine | parenthetical)* EOF
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
	|	[fF][aA][dD][eE]' '[oO][uU][tT]'.'
	|	[fF][aA][dD][eE]' '[tT][oO]' '[bB][lL][aA][cC][kK]'.'
	|	[cC][uU][tT]' '[tT][oO]' '[bB][lL][aA][cC][kK]'.'
	;

Character
	:	~[a-z\t\r\n]+ '^'?
	|	'@' Span
	;

Parenthetical
	: '(' ~[\r\n] ')'
	;
TitlePageKey
	:	[tT][iI][tT][lL][eE]':'
	|	[aA][uU][tT][hH][oO][rR]':'
	|	[cC][rR][eE][dD][iI][tT]':'
	|	[sS][oO][uU][rR][cC][eE]':'
	|	[dD][rR][aA][fF][tT]' '[dD][aA][tT][eE]':'
	|	[cC][oO][nN][tT][aA][cC][tT]':'
	|	[nN][oO][tT][eE][sS]':'
	;

TitlePageValue
	:	{Column == 0}? TitlePageKey .*? EOL
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

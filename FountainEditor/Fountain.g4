grammar Fountain;

//
// Parser Rules
//

eol
	:	EOL
	;

boneyard
	:	Boneyard
	;

centered
	:	Centered
	;

character
	:	Character ( Span | Parenthetical | EOL)*? EOL EOL
	;

heading
	:	Heading
	;

line
	:	Span
	;

lyric
	:	Lyric
	;

note
	:	Note
	;

pageBreak
	:	PageBreak
	;

section
	:	Section
	;

synopsis
	:	Synopsis
	;

transition
	:	Transition
	;

compileUnit
	:	( eol | boneyard | centered | character | heading | line | lyric | note | pageBreak | section | synopsis | transition )* EOF
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
	:	( Ext	|	Int )	Period      ~( '\r' | '\n' )*
	|	( Ext  '/'	Int )	Period      ~( '\r' | '\n' )*
	|	( Int  '/'	Ext )	Period      ~( '\r' | '\n' )*
	|	[iI]   '/'	[eE]	Period		~( '\r'	| '\n' )*
	|	{Column == 0}?		Period		~( '\r'	| '\n' )*
	;

EOL
	:	'\r\n'
	|	'\r'
	|	'\n'
	;

fragment Uppercase
	:	'A'..'Z'
	|	['().]
	;

fragment UppercaseWord
	:	Uppercase+
	;

Character
	:	UppercaseWord ( Space+ | UppercaseWord )* Caret?
	|	At Span
	;

fragment Symbol
	:	'A'..'Z'
	|	'a'..'z'
	|	'0'..'9'
	|	[\-_*.,!?'":()/]
	;

fragment Word
	:	Symbol+
	;

Span
	:	Word ( Space+ Word )*
	|	UppercaseWord ( Space+ UppercaseWord )* EOL EOL
	;

fragment Period
	:	'.'
	;

fragment At
	:	'@'
	;

fragment Caret
	:	'^'
	;

fragment Colon
	:	':'
	;

fragment Equal
	:	'='
	;

fragment Hyphen
	:	'-'
	;

fragment Pound
	:	{Column == 0}? '#'
	;

fragment Question
	:	'?'
	;

fragment Space
	:	' '
	;

fragment Tilde
	:	'~'
	;

fragment OAngle
	:	'>'
	;

fragment CAngle
	:	'<'
	;

fragment OParen
	:	'('
	;

fragment CParen
	:	')'
	;

fragment OComment
	:	'/*'
	;

fragment CComment
	:	'*/'
	;

fragment ONote
	:	'[['
	;

fragment CNote
	:	']]'
	;

fragment To
	:	[tT] [oO] Colon
	;

PageBreak
	:	'===' '='+?
	;

Boneyard
	:	OComment .*? CComment
	;

Centered
	:	OAngle ~( '\r' | '\n' )* CAngle
	;
	
// TODO: Check for double return
Note
	:	ONote .*? CNote
	;

Parenthetical
	:	OParen .*? CParen
	;

Lyric
	:	Tilde  ~( '\r' | '\n' )*
	;

Section
	:	Pound+ ~( '\r' | '\n' )*
	;

Synopsis
	:	Equal  ~( '\r' | '\n' )*
	;

Transition
	:	Span Space To
	|	OAngle ~( '\r' | '\n' )*
	;

WS
	: [ \t] -> skip
	;
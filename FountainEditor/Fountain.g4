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
	:	Character ( EOL+ Parenthetical )? EOL+ Span*
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

EOL
	:	( '\r'? '\n' )
	;

fragment Uppercase
	:	'A'..'Z'
	;

fragment UppercaseWord
	:	Uppercase+
	;

Character
	:	UppercaseWord ( Space UppercaseWord )* Caret?
	|	At Span
	;

fragment Symbol
	:	'A'..'Z'
	|	'a'..'z'
	|	'0'..'9'
	|	[_*.]
	;

fragment Word
	:	Symbol+
	;

Span
	:	Word ( Space Word )* Space?
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
	:	'#'
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

PageBreak
	:	'==='
	;

Heading
	:	[Ee] [Xx] [Tt] Period      ~( '\r' | '\n' )*
	|	[Ii] [Nn] [Tt] Period      ~( '\r' | '\n' )*
	|	{Column == 0}? Period ~'.' ~( '\r' | '\n' )*
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
	:	Span Space [Tt] [Oo] Colon
	|	OAngle ~( '\r' | '\n' )*
	;

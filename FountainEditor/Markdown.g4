grammar Markdown;

/*
 * Parser Rules
 */
boldItalics
	:	BoldItalics
	;

bold
	:	Bold
	;

italics
	:	Italics
	;

underline
	:	Underline
	;

notes
	:	Notes
	;

boneyard
	:	Boneyard
	;

words
	:	Words
	;

blank
	:	BlankLine
	;

compileUnit
	:	
	(	bold 
	|	boldItalics 
	|	italics 
	|	underline 
	|	notes 
	|	boneyard 
	|	words
	)*	EOF
	;

/*
 * Lexer Rules
 */

Boneyard
	:	'/*' (Words | EOL | BlankLine )* '*/'
	;

BoldItalics
	:	'***' ~(' ') Words '***'
	;

Bold
	:	'**' ~(' ') Words '**'
	;

Italics
	:	'*' ~(' ') Words '*'
	;

Underline
	:	'_' ~(' ') Words '_'
	;

Notes
	:	'[[' ( Words | EOL )* ']]'
	;

Words
	:	~( '/' | '*' | '_' | '[' | ']' | '\r' | '\n' )+
	;

BlankLine
	:	{Column == 0}? EOL
	;

EOL
	:	'\r'? '\n'
	;

WS
	:	' ' -> channel(HIDDEN)
	;

grammar Markdown;

/*
 * Parser Rules
 */
boldItalics
	:	BoldItalics ~(EOL)* BoldItalics
	;

bold
	:	Bold ~(EOL)* Bold
	;

italics
	:	Italics ~(EOL)* Italics
	;

underline
	:	Underline ~(EOL)* Underline
	;

notes
	:	Notes ~(EOL) Notes
	;

boneyard
	:	Boneyard .*? Boneyard
	;

compileUnit
	:	EOF
	;

/*
 * Lexer Rules
 */
BoldItalics
	:	'***'
	;

Bold
	:	'**'
	;

Italics
	:	'*'
	;

Underline
	:	'_'
	;

Notes
	:	'[[' 
	|	']]'
	;

Boneyard
	:	'/*'
	|	'*/'
	;

EOL
	:	'\r'? '\n'
	;

BlankLine
	:	{Column == 0}? EOL
	;

WS
	:	' ' -> channel(HIDDEN)
	;

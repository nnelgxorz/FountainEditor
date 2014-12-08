grammar Markdown;

/*
 * Parser Rules
 */

md
	:	'***' md* '***'	# boldItalics
	|	 '**' md* '**'	# bold
	|	  '*' md* '*'	# italics
	|	  '_' md* '_'	# underline
	|	String			# string
	;

notes
	:	Notes
	;

boneyard
	:	Boneyard
	;

compileUnit
	:	
	(	md
	|	notes
	|	boneyard
	)*
	;

/*
 * Lexer Rules
 */

Boneyard
	:	'/*' .*? '*/'
	;

Notes
	:	'[[' .*? ']]'
	;

String
	:	~[*_\r\n]+
	;

EOL
	:	'\r'? '\n'
	;

WS
	:	' ' -> channel(HIDDEN)
	;

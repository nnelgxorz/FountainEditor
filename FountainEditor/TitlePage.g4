grammar TitlePage;

/*
 * Parser Rules
 */

pair
	:	key value
	;

key
	:	Word ':'
	;

value
	:	( Word | Phrase )
	;

compileUnit
	:	( pair* ) | EOF
	;

/*
 * Lexer Rules
 */
Phrase
	: (Word | ' ')+
	;

Word
	:	~[: \r\n]+
	;

WS
	:	[ \t\r\n] -> channel(HIDDEN)
	;

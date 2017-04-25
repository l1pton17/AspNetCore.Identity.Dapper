CREATE TABLE [UserClaims] (
	[Id]			SERIAL			NOT NULL,
	[UserId]		VARCHAR(256)	REFERNCES Users(Id)	NOT NULL,
	[ClaimType]		TEXT			NULL,
	[ClaimValue]	TEXT			NULL
);
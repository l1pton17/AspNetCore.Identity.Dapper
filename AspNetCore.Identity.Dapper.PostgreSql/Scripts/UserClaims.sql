CREATE TABLE [UserClaims] (
	[Id]			SERIAL			NOT NULL,
	[UserId]		VARCHAR(256)	REFERNCES Users(Id)	NOT NULL,
	[ClaimType]		VARCHAR(65)		NULL,
	[ClaimValue]	VARCHAR(65)		NULL
);
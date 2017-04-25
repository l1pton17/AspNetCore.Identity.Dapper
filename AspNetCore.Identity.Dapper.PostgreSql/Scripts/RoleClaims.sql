CREATE TABLE [RoleClaims] (
	[Id]			SERIAL			NOT NULL,
	[RoleId]		VARCHAR(256)	REFERENCES Roles(Id)	NOT NULL,
	[ClaimType]		TEXT			NULL,
	[ClaimValue]	TEXT			NULL
);
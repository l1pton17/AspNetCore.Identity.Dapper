CREATE TABLE [RoleClaims] (
	[Id]			SERIAL			NOT NULL,
	[RoleId]		VARCHAR(128)	REFERENCES Roles(Id)	NOT NULL,
	[ClaimType]		TEXT			NULL,
	[ClaimValue]	TEXT			NULL
);
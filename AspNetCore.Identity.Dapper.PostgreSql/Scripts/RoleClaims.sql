﻿CREATE TABLE RoleClaims (
	Id  UUID PRIMARY KEY,
	RoleId		UUID	REFERENCES Roles(Id)	NOT NULL,
	ClaimType		TEXT			NULL,
	ClaimValue	TEXT			NULL
);
﻿CREATE TABLE Users (
	Id UUID PRIMARY KEY,
	UserName				VARCHAR(256)	NOT NULL,
	NormalizedUserName	VARCHAR(256)	NOT NULL,
	Email					VARCHAR(256)	NOT NULL,
	NormalizedEmail		VARCHAR(256)	NOT NULL,
	EmailConfirmed		BOOLEAN			NOT NULL,
	PasswordHash			TEXT,
	SecurityStamp			TEXT,
	ConcurrencyStamp		VARCHAR(128)	NOT NULL,
	PhoneNumber			TEXT,
	PhoneNumberConfirmed	BOOLEAN			NOT NULL,
	TwoFactorEnabled		BOOLEAN			NOT NULL,
	LockoutEndUtc			timestamp,
	LockoutEnabled		BOOLEAN			NOT NULL,
	AccessFailedCount		INT4			NOT NULL
);
CREATE TABLE [UserLogins](
	[UserId]				TEXT	REFERENCES Users(Id)	NOT NULL,
	[LoginProvider]			TEXT	NOT NULL,
	[ProviderKey]			TEXT	NOT NULL,
	[ProviderDisplayName]	TEXT	NOT NULL
);
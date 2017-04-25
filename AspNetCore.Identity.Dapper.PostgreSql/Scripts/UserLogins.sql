CREATE TABLE [UserLogins](
	[UserId] VARCHAR(128) REFERENCES Users(Id)	NOT NULL,
	[LoginProvider] VARCHAR(32)	NOT NULL,
	[ProviderKey] VARCHAR(64) NOT NULL,
	[ProviderDisplayName] VARCHAR(32) NOT NULL
);
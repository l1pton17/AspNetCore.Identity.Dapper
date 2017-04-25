CREATE TABLE [UserTokens] (
	[UserId]				VARCHAR(128)	REFERENCES Users(Id)	NOT NULL,
	[LoginProvider]			VARCHAR(32)	NOT NULL,
	[Name]					VARCHAR(256)	NOT NULL,
	[Value]					VARCHAR(256)	NOT NULL
);
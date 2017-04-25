CREATE TABLE [UserTokens] (
	[UserId]				VARCHAR(256)	REFERENCES Users(Id)	NOT NULL,
	[LoginProvider]			VARCHAR(256)	NOT NULL,
	[Name]					VARCHAR(256)	NOT NULL,
	[Value]					VARCHAR(256)	NOT NULL
);
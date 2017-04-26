CREATE TABLE UserLogins (
	UserId UUID REFERENCES Users(Id)	NOT NULL,
	LoginProvider VARCHAR(32)	NOT NULL,
	ProviderKey VARCHAR(64) NOT NULL,
	ProviderDisplayName VARCHAR(32) NOT NULL
);
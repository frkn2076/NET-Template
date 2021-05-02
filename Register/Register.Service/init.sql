CREATE TABLE Registration (
	Id serial PRIMARY KEY,
	Name VARCHAR ( 50 ) UNIQUE NOT NULL,
	Password VARCHAR ( 50 ) NOT NULL,
	CreatedAt TIMESTAMP,
	CreatedBy VARCHAR ( 50 ),
	ModifiedAt TIMESTAMP,
	ModifiedBy VARCHAR ( 50 )
);
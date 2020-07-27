CREATE TABLE NurseEntity(
	NurseID INT IDENTITY(1, 1) NOT NULL,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_Nurse PRIMARY KEY (NurseID)
);

CREATE TABLE DoctorEntity(
	DoctorID INT IDENTITY(1, 1) NOT NULL,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_Doctor PRIMARY KEY (DoctorID)
);

CREATE TABLE PatientRoomEntity(
	PatientRoomID INT IDENTITY(1, 1) NOT NULL,
	Available BIT NOT NULL,
	CONSTRAINT PK_PatientRoom PRIMARY KEY (PatientRoomID)
);

CREATE TABLE OpsRoomEntity(
	OpsRoomID INT IDENTITY(1, 1) NOT NULL,
	Available BIT NOT NULL,
	CONSTRAINT PK_OpsRoom PRIMARY KEY (OpsRoomID)
);

CREATE TABLE WorkingDetailsEntity(
	NurseID INT NOT NULL,
	DoctorID INT NOT NULL,
	ActiveAssociation BIT NOT NULL,
	CONSTRAINT PK_WorkingDetails PRIMARY KEY (NurseID, DoctorID),
	CONSTRAINT FK_WorkingDetails_NurseEntity_NurseID FOREIGN KEY (NurseID)
		REFERENCES NurseEntity (NurseID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_WorkingDetails_DoctorEntity_DoctorID FOREIGN KEY (DoctorID)
		REFERENCES DoctorEntity (DoctorID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IllnessEntity(
	IllnessID INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_IllnessEntity PRIMARY KEY (IllnessID)
);


CREATE TABLE TreatmentEntity(
	TreatmentID INT IDENTITY(1,1) NOT NULL,
	IllnessID INT NOT NULL,
	DoctorID INT NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	TimeToTreat INT NOT NULL,
	CONSTRAINT PK_TreatmentEntity PRIMARY KEY (TreatmentID), 
	CONSTRAINT FK_TreatmentEntity_DoctorEntity_DoctorID FOREIGN KEY (DoctorID)
		REFERENCES DoctorEntity (DoctorID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_TreatmentEntity_IllnessEntity_IllnessID FOREIGN KEY (IllnessID)
		REFERENCES IllnessEntity (IllnessID) ON DELETE CASCADE ON UPDATE CASCADE

);



CREATE TABLE PatientEntity(
	PatientID INT IDENTITY(1, 1) NOT NULL,
	PatientRoomID INT,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_PatientEntity PRIMARY KEY (PatientID), 
	CONSTRAINT FK_PatientEntity_PatientRoomEntity_PatientRoomID FOREIGN KEY (PatientRoomID)
		REFERENCES PatientRoomEntity (PatientRoomID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE TreatmentDetailsEntity (
	OpsRoomID INT,
	PatientID INT NOT NULL,
	TreatmentID INT NOT NULL,
	StartTime NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_TreatmentDetailsEntity PRIMARY KEY (TreatmentID, PatientID),
	CONSTRAINT FK_TreatmentDetailsEntity_PatientEntity_PatientID FOREIGN KEY (PatientID)
		REFERENCES PatientEntity (PatientID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_TreatmentDetailsEntity_TreatmentEntity_TreatmentID FOREIGN KEY (TreatmentID)
		REFERENCES TreatmentEntity (TreatmentID) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO NurseEntity(FirstName, LastName) VALUES
		('Jolene', 'Bean'),
		('Cairo', 'Brett'),
		('Dollie', 'Morales'),
		('Rhea',  'Novak'),
		('Brenna', 'Wallis')
		
INSERT INTO DoctorEntity(FirstName, LastName) VALUES
		('Neo', 'Coulson'),
		('Theodore', 'Lord'),
		('Karan', 'Sadler'),
		('Melina', 'Irvine'),
		('Ayda', 'Yates')

INSERT INTO WorkingDetailsEntity(DoctorID, NurseID, ActiveAssociation) VALUES
(1, 1, 0),(1, 2, 1),(1, 3, 0),(1, 4, 0),(1, 5, 0),
(2, 1, 0),(2, 2, 0),(2, 3, 0),(2, 4, 1),(2, 5, 0),
(3, 1, 1),(3, 2, 0),(3, 3, 0),(3, 4, 0),(3, 5, 0),
(4, 1, 0),(4, 2, 0),(4, 3, 0),(4, 4, 0),(4, 5, 1),
(5, 1, 0),(5, 2, 0),(5, 3, 1),(5, 4, 0),(5, 5, 0)

INSERT INTO PatientRoomEntity(Available) VALUES
		(1),(1),(1),(1),(1),(1),(1),(1),
		(1),(1),(1),(1),(1),(1),(1),(1)

INSERT INTO OpsRoomEntity(Available) VALUES
		(1),(1),(1),(1),(1),(1),(1)
	
INSERT INTO IllnessEntity(Name) VALUES
		('Severe Pain'),('Headache'),('Fever'),
		('Gingivitis'),('Amebiasis'),('Giardiasis')

INSERT INTO TreatmentEntity(Name, DoctorID, IllnessID, TimeToTreat) VALUES
		('Pain-relieving Drugs', 1, 1, 2),('Give Analgesics', 2, 2, 8),('Give Metronidazole', 4, 5, 5),('Dental Cleaning', 5, 4, 12),
		('Give Antipyretic', 1, 3, 4),('Dental Cleaning', 3, 4, 6),('Pain-relieving Drugs', 5, 1, 3),('Give Analgesics', 5, 2, 10),
		('Give Metronidazole', 1, 5, 3),('Give Metronidazole', 3, 6, 8),('Give Antipyretic', 4, 3, 4),('Dental Cleaning', 1, 4, 5)

INSERT INTO PatientEntity(PatientRoomID, FirstName, LastName) VALUES
		(null, 'Angus', 'Hogg'),(null, 'Brittney', 'Senior'),(null, 'Anabelle', 'Keeling'),
		(null,'Francisco', 'Olson'),(null, 'Janine', 'Medrano')

drop table DoctorEntity
drop table IllnessEntity
drop table NurseEntity
drop table OpsRoomEntity
drop table PatientEntity
drop table PatientRoomEntity
drop table TreatmentDetailsEntity
drop table WorkingDetailsEntity
drop table TreatmentEntity

--select *
--from PatientEntity
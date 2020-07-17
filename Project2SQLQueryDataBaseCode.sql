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
	CONSTRAINT PK_WorkingDetails PRIMARY KEY (NurseID, DoctorID),
	CONSTRAINT FK_WorkingDetails_NurseEntity_NurseID FOREIGN KEY (NurseID)
		REFERENCES NurseEntity (NurseID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_WorkingDetails_DoctorEntity_DoctorID FOREIGN KEY (DoctorID)
		REFERENCES DoctorEntity (DoctorID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE TreatmentEntity(
	TreatmentID INT IDENTITY(1, 1) NOT NULL,
	DoctorID INT NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_TreatmentEntity PRIMARY KEY (TreatmentID), 
	CONSTRAINT FK_TreatmentEntity_DoctorEntity_DoctorID FOREIGN KEY (DoctorID)
		REFERENCES DoctorEntity (DoctorID) ON DELETE CASCADE ON UPDATE CASCADE
);



CREATE TABLE IllnessEntity(
	IllnessID INT IDENTITY(1, 1) NOT NULL,
	TreatmentID INT NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_IllnessEntity PRIMARY KEY (IllnessID), 
	CONSTRAINT FK_IllnessEntity_TreatmentEntity_TreatmentID FOREIGN KEY (TreatmentID)
		REFERENCES TreatmentEntity (TreatmentID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE PatientEntity(
	PatientID INT IDENTITY(1, 1) NOT NULL,
	PatientRoomID INT NOT NULL,
	IllnessID INT NOT NULL,
	DoctorID INT NOT NULL,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_PatientEntity PRIMARY KEY (PatientID), 
	CONSTRAINT FK_PatientEntity_PatientRoomEntity_PatientRoomID FOREIGN KEY (PatientRoomID)
		REFERENCES PatientRoomEntity (PatientRoomID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_PatientEntity_IllnessEntity_IllnessID FOREIGN KEY (IllnessID)
		REFERENCES IllnessEntity (IllnessID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_PatientEntity_DoctorEntity_DoctorID FOREIGN KEY (DoctorID)
		REFERENCES DoctorEntity (DoctorID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE TreatmentDetailsEntity (
	OpsRoomID INT NOT NULL,
	TreatmentID INT NOT NULL,
	StartTime NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_TreatmentDetailsEntity PRIMARY KEY (OpsRoomID, TreatmentID),
	CONSTRAINT FK_TreatmentDetailsEntity_OpsRoomEntity_OpsRoomID FOREIGN KEY (OpsRoomID)
		REFERENCES OpsRoomEntity (OpsRoomID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_TreatmentDetailsEntity_TreatmentEntity_TreatmentID FOREIGN KEY (TreatmentID)
		REFERENCES TreatmentEntity (TreatmentID) ON DELETE CASCADE ON UPDATE CASCADE
);
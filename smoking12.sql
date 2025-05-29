CREATE database smoking12

CREATE TABLE Account (
    Account_ID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(250),
    Password NVARCHAR(100),
    Status BIT
);
GO

CREATE TABLE [User] (
    Account_ID INT PRIMARY KEY,
    FullName NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
    birthday DATE,
    Sex BIT,
    Role NVARCHAR(100),
    FOREIGN KEY (Account_ID) REFERENCES Account(Account_ID)
);
GO

CREATE TABLE Member (
    Member_ID INT IDENTITY(1,1) PRIMARY KEY,
    Account_ID INT UNIQUE,
    CigarettesPerDay INT,
    Smoking_time NVARCHAR(50),
    Goal_Time NVARCHAR(10),
    Reason NVARCHAR(250),
    CostPerCigarette DECIMAL(14,2),
    MedicalHistory NVARCHAR(250),
    MostSmokingTime NVARCHAR(250),
    Feedback_content NVARCHAR(250),
    Feedback_date DATE,
    Feedback_rating INT,
	Status_Process NVARCHAR(15),
    FOREIGN KEY (Account_ID) REFERENCES [User](Account_ID)
);
GO

CREATE TABLE Package_membership (
    Package_membership_ID INT IDENTITY(1,1) PRIMARY KEY,
    Category NVARCHAR(250),
    Price DECIMAL(14,2),
    Description NVARCHAR(250),
    Duration INT
);
GO

CREATE TABLE Purchase (
    Purchase_ID INT IDENTITY(1,1) PRIMARY KEY,
    Member_ID INT,
    Package_membership_ID INT,
    Time_BUY DATETIME,
    Total_price DECIMAL(14,2),
    Start_date DATE,
    End_date DATE,
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
    FOREIGN KEY (Package_membership_ID) REFERENCES Package_membership(Package_membership_ID)
);
GO

CREATE TABLE [Plan] (
    Plan_ID INT IDENTITY(1,1) PRIMARY KEY,
    Member_ID INT UNIQUE, 
    QuitSmokingDate DATE,
    TotalCigarettes INT,
    SaveMoney DECIMAL(14,2),
    Clock DATETIME,
    CigarettesQuit INT,
    MaxCigarettes INT,
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID)
);
GO

CREATE TABLE Plan_detail (
    Plan_detail_ID INT IDENTITY(1,1) PRIMARY KEY,
    Plan_ID INT,
    TodayCigarettes INT,
    MaxCigarettes INT,
    Date DATE,
    IsSuccess BIT,
    FOREIGN KEY (Plan_ID) REFERENCES [Plan](Plan_ID)
);
GO

CREATE TABLE Community_post (
    Post_ID INT IDENTITY(1,1) PRIMARY KEY,
    Account_ID INT,
    Create_Time DATE,
    Content NVARCHAR(250),
    FOREIGN KEY (Account_ID) REFERENCES Account(Account_ID)
);
GO

CREATE TABLE Comment (
    Cmt_ID INT IDENTITY(1,1) PRIMARY KEY,
    Account_ID INT,
    Post_ID INT,
    Comment NVARCHAR(250),
    Create_Time DATE,
    FOREIGN KEY (Account_ID) REFERENCES Account(Account_ID),
    FOREIGN KEY (Post_ID) REFERENCES Community_post(Post_ID)
);
GO

CREATE TABLE Ranking (
    Ranking_ID INT IDENTITY(1,1) PRIMARY KEY,
    Member_ID INT,
    Badge NVARCHAR(100),
    Total_score INT,
    Date DATE,
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID)
);
GO
CREATE TABLE Phase (
    PhaseID INT IDENTITY(1,1) PRIMARY KEY,
    Plan_ID INT,
    PhaseNumber INT,
    StartDatePhase DATE,
    EndDatePhase DATE,
    StatusPhase NVARCHAR(15),
    FOREIGN KEY (Plan_ID) REFERENCES [Plan](Plan_ID)
);
GO
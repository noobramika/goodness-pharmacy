CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [First_Name] VARCHAR(50) NOT NULL, 
    [Last_Name] VARCHAR(50) NULL, 
    [NIC] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [Gender] VARCHAR(50) NOT NULL, 
    [Phone_No] INT NOT NULL, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Confirm_Password] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NULL, 
    [Hire_Detail] VARCHAR(50) NOT NULL, 
    [DOB] DATETIME NOT NULL, 
    [Job_Title] VARCHAR(50) NOT NULL, 
    [Other_Details] VARCHAR(50) NULL
)

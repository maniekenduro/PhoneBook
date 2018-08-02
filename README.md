# PhoneBook
This little console program can be used as a simply web phone book (I know it is useless but it's only for my education :) ):
Functions:

-Adding new records

-Showing all records with pagination

-Editing records

-Deleting records.


This program require connection with database:

1.Create sql serwer with database Phonebook,

2.Execute this commands

  CREATE TABLE People(
  ID Int Identity(1,1) NOT NULL PRIMARY KEY ,
  FirstName nvarchar(32) NOT NULL,
  LastName nvarchar(64) NOT NULL,
  Phone nvarchar(25) NOT NULL,
  Email nvarchar(256) NOT NULL,
  Created DateTime,
  Updated DateTime
  ); 

3.Add few record by command (example):
   insert into People (FirstName, LastName, Phone, Email, Created, Updated)
	  values ('Jan', 'Sławko','7123413','janslawko@gmail.com','2015/1/1','2015/1/2');
	
4.Enjoy:)

alter table Trip alter column TripDate datetime2 not null
go

alter table Trip alter column TripDate datetime2 null
go

Select * from sys.check_Constraints
where Parent_object_id = 1394104007

Alter table Trip
Drop constraint ck_Trip_Times

ALTER TABLE TRIP
Add constraint ck_TripTimes Check ([TimeDeparting]<=[TimeReturning] or (TimeDeparting is null and TimeReturning is null))


alter table Trip alter column TripDate datetime null
go



-- TOP 10 RIDERS
select Name from 
	(select CONCAT(m.FirstName, ' ', m.LastName) Name, count(*) Runs
	from RawRunLog rr 
	LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName))
	where m.FirstName is not null
	group by Name 
	order by Runs desc 
	LIMIT 10) sub 
order by Name;

-- TOP 10 RIDERS
select Name , Runs from 
	(select CONCAT(m.FirstName, ' ', m.LastName) Name, count(*) Runs
	from RunLog rl 
	LEFT join Member m on m.MemberID = rl.RiderMemberID
	group by Name 
	order by Runs desc 
	LIMIT 20) top 
order by Runs desc;

-- TOP Controllers
select Name, Runs from 
	(select Controller Name, count(*) Runs
	from RawRunLog rr 
	LEFT join Member m on rr.Controller = (CONCAT(m.LastName, ' ', m.FirstName))
	where m.FirstName is not null
	group by Name 
	order by Runs desc 
	LIMIT 100) sub 
order by Runs desc;

-- BAD RIDER NAMES IN CALL LOG / MISSING RIDERS
select Rider, count(*), m.FirstName 
from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
where m.FirstName is null 
group by Rider, m.FirstName
order by count(*) desc;

-- Select riders who have not logged in, but have done a run since may
select CONCAT(m.FirstName, ' ', m.LastName), date(m.JoinDate) as Joined, m.EmailAddress, date(max(rr.CallDate)) as LastRun, count(*) as Runs
from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
where m.MemberID not in
(
	select m.MemberID from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null 
)
and rr.CallDate > '2013-05-01'
and m.LeaveDate is null
group by m.MemberID
order by max(rr.CallDate) desc;

-- Name & shame riders who have not logged in, but have done a run since may
select CONCAT(m.LastName, ' ', m.FirstName), 
concat('who joined on ', date(m.JoinDate)) as Joined/*, m.EmailAddress*/, 
concat(' and last did a run on ', date(max(rr.CallDate))) as LastRun, 
concat(' and has done ', cast(count(*) as char), ' run(s) since May') as Runs
from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
where m.MemberID not in
(
	select m.MemberID from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null 
)
and rr.CallDate > '2013-05-01'
and m.LeaveDate is null
group by m.MemberID
order by max(rr.CallDate) desc;

-- USERS WHO HAVE LOGGED IN AND SET A PASSWORD
select * from User u join Member m on m.MemberID = u.MemberID where u.PasswordHash is not null and u.PasswordHash != '';

-- USERS WHO HAVE LOGGED IN
select * from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null order by lastLoginDate desc; 

-- USERS WHO HAVE LOGGED IN TODAY
select CONCAT(m.FirstName, ' ', m.LastName) as Member from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate > CURRENT_DATE() order by lastLoginDate desc; 


-- USERS WHO HAVE NOT LOGGED IN
select * from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is null;

-- USERS WHO HAVE LOGGED IN
select m.LastName, m.FirstName from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null order by m.LastName;

-- Select all Mobile numbers for a set of tags
select distinct m.MobileNumber, CONCAT(m.FirstName, ' ', m.LastName) Name from Member m 
join Member_Tag mt on mt.MemberID = m.MemberID 
join Tag t on t.TagID = mt.TagID
where t.Tag in ('Fundraiser');

-- Sent messages
select * from Message;

-- Controllers from raw logs
select distinct Controller from RawRunLog;

-- Select an insert to generate missing member tags for a subset of filtered control logs
select distinct m.MemberID, 8 -- AA
from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
where m.MemberID not in 
(select MemberID from Member_Tag where TagID = 8)
and rr.CollectFrom like 'East Surrey' or rr.CollectFrom like '%Redhill%';

-- Recent run log activity
select date(CallDate), CallTime, TIME(rpad(replace(CallTime, '.',':'), 5, '0')) as FixedCallTime, 
	CONCAT(m.FirstName, ' ', m.LastName) as ResolvedMemberName, Consignment, CollectFrom, Destination, CONCAT(con.FirstName, ' ', con.LastName) as ResolvedController  
from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
LEFT join Member con on rr.Controller = (CONCAT(con.LastName, ' ', con.FirstName)) 
order by CallDate desc, RawRunLogID desc LIMIT 100;

-- Collection locations that cannot be resolved
select distinct CollectFrom, l.* from RawRunLog rrl left join Location l on rrl.CollectFrom = l.Location
where l.LocationID is null;

-- Destination locations that cannot be resolved
select distinct Destination, l.* from RawRunLog rrl left join Location l on rrl.Destination = l.Location
where l.LocationID is null;

select * from Tag;

select * from Product;

select * from CallLog;

select * from Member where MobileNumber = '07734819559';

select RunLogID as ID, date(DutyDate) as 'Duty Date', CallDateTime as 'Call Date Time', cf.Location as 'Call From', cl.Location as 'Collected From', 
	dl.Location as 'Taken To', time(rl.CollectDateTime) as Collected, time(rl.DeliverDateTime) as Delivered, 
	timediff(rl.DeliverDateTime, rl.CollectDateTime) as 'Journey Time',
	fl.Location as 'Final Destination', concat(m.FirstName, ' ', m.LastName) as Rider, v.VehicleType, rl.Description, 
	concat(c.FirstName, ' ', c.LastName) as Controller from RunLog rl 
join Member m on m.MemberID = rl.RiderMemberID
join Member c on c.MemberID = rl.ControllerMemberID
join Location cf on cf.LocationID = rl.CallFromLocationID
join Location cl on cl.LocationID = rl.CollectionLocationID
join Location dl on dl.LocationID = rl.DeliverToLocationID
join Location fl on fl.LocationID = rl.FinalDestinationLocationID
join VehicleType v on v.VehicleTypeID = rl.VehicleTypeID
where DutyDate > '2013-12-31' or CallDateTime > '2013-12-31'
order by rl.CallDateTime desc;

select * from RunLog;

/* 
delete from RunLog_Product where RunLogID < 42;
delete from RunLog where RunLogID < 42; */

/*
de--lete from RunLog_Product;
de--lete from RunLog;
*/

select * from Member m 
join User u on u.MemberID = m.MemberID 
where m.LastName = 'Holmes';
/*update User set PasswordHash = '' where UserID = 9*/
/*update User set UserLevelID = 3 where UserId = 7;*/

select * from Member where LastName = 'Stanton';
select * from User where MemberID = 200;
/* update User set UserlevelID = 3 where UserID = 9; */
/* update User set LastLoginDate = null where UserID = 77; */

select * from RawRunLog where CallDate = '2013-07-15';

select count( distinct (Calldate)) from RawRunLog where dayofweek(CallDate) =2 and CallDate > AddDate(CURRENT_DATE,@daysback);

/*
Select the average number of calls by day of week and make a riders required prediction based on average call numbers
The riders Required is Calls * @riderfactor (rounded UP)
The Call averages are based on the last @daysback days
The select has to work out the ShiftStart date or calls after midnight would be counted against the next days numbers
*/
SET @daysback = -240;
SET @daysinweek = 7;
SET @bloodrunafterhour = 17;
SET @riderfactor = 0.5;
SET @weeks := (@daysback / @daysinweek) * -1;
SELECT dayname(case when Hour(CallTime) > @bloodrunafterhour then CallDate else AddDate(CallDate, -1) end) as ShiftDay
	, round(count(*) / @weeks) as AverageCalls
	, ceil((count(*) / @weeks) * @riderfactor) as RidersRequired
FROM RawRunLog
WHERE CallDate > AddDate(CURRENT_DATE, @daysback)
AND(Consignment like "%blood%" 
	or Consignment like "%plate%" 
	or Consignment like "%plas%" 
	or Consignment like "%ffp%" 
	or Consignment like "%sample%"
	or Consignment like "%drugs%"
	or Consignment like "%cd%"
	or Consignment like "%package%")
GROUP BY dayname(case when Hour(CallTime) > @bloodrunafterhour then CallDate else AddDate(CallDate, -1) end)
ORDER BY dayofweek(case when Hour(CallTime) > @bloodrunafterhour then CallDate else AddDate(CallDate, -1) end);

-- Old records, member award stats based on nights worked for 2013
SET @bloodrunafterhour = 17;
select Member, sum(Nights) as Nights
from(
	-- Controllers
	select Controller as Member, 
	count(distinct 
			(case when Hour(CallTime) > @bloodrunafterhour then 
				CallDate else 
			AddDate(CallDate, -1) 
		end)) as Nights
	from RawRunLog
	where CallDate > '2013-01-01'
		and CallTime != '00:00'
	and Consignment not like '%human%'
	and Consignment not like '%milk%'
	and Consignment not like '%supply%'
	and Consignment not like '%return%'
	Group by Member
	UNION ALL
	-- Riders
	select Rider as Member, 
	count(distinct 
			(case when Hour(CallTime) > @bloodrunafterhour then 
				CallDate else 
			AddDate(CallDate, -1) 
		end)) as Nights
	from RawRunLog
	where CallDate > '2013-01-01'
	group by Member
) l
where Member != '' and Member != '-'
group by Member
having Nights >9
order by Nights desc;

-- New records, MUCH EASIER!!
select concat(m.LastName, ' ', m.firstName) as Member
	, count(distinct DutyDate) as Nights
from RunLog l
join Member m on 
	m.MemberID = l.RiderMemberID 
	or 
	(m.MemberID = l.ControllerMemberID and CallDateTime is not null)
group by Member
having Nights > 9
order by Nights desc


/*
Day vs Call Hour punchcard data
Select the number of calls by day of week and hour
The select has to work out the ShiftStart date or calls after midnight would be counted against the next days numbers
*/
SET @daysback = -240;
select dayname(case when Hour(CallTime) > @bloodrunafterhour then CallDate else AddDate(CallDate, -1) end) as Day, Hour(CallTime) as Hour, count(*) as Calls from RawRunLog 
WHERE CallDate > AddDate(CURRENT_DATE, @daysback)
AND Hour(CallTime) >= 0 AND Hour(CallTime) <= 23
AND(Consignment like "%blood%" 
	or Consignment like "%plate%" 
	or Consignment like "%plas%" 
	or Consignment like "%ffp%" 
	or Consignment like "%sample%"
	or Consignment like "%drugs%"
	or Consignment like "%cd%"
	or Consignment like "%package%")
group by dayname(case when Hour(CallTime) > @bloodrunafterhour then CallDate else AddDate(CallDate, -1) end), Hour(CallTime)
ORDER BY dayofweek(case when Hour(CallTime) > @bloodrunafterhour then CallDate else AddDate(CallDate, -1) end), Hour(CallTime);

select * from UserLevel;

/*
update User set UserLevelID = 4 where
UserID in(7);

update RunLog set RiderMemberID = 234 where RunLogID = 483;
select * from RunLog where RunLogID = 483;


delete from RunLog where RunLogID = 147;
delete from RunLog_Product where RunLogID = 147;

*/

select * from Member where LastName = 'Bowers';
select * from User where MemberID = 112;

select * from VehicleType;

select * from Location;

-- Riders who have done a run since may and their run counts
select m.MemberID, concat(m.Firstname, ' ', m.LastName), 
	count(*) from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
and rr.CallDate > '2013-05-01'
and m.LeaveDate is null
where LastName is not null
group by m.MemberID
order by count(*) desc;


-- Select riders who have left
select FirstName, LastName, LeaveDate from Member where LeaveDate is not null;

-- Select riders who have left
select FirstName, LastName, EmailAddress, LastGDPGMPDate from Member;


select * from Message  order by MessageID desc limit 10;

SET @TODATE= date_format((date_add(CURRENT_DATE, INTERVAL 1 MONTH)), '%Y-%m-01 00:00:00');
SET @FROMDATE= date_format(CURRENT_DATE, '%Y-%m-01 00:00:00');

select sum(rlp.Quantity) into @BLOOD from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 1;

select sum(rlp.Quantity) into @PLATELETS from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 2;

select sum(rlp.Quantity) into @PLASMA from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 3;

select sum(rlp.Quantity) into @SAMPLE from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 4;

select sum(rlp.Quantity) into @MILK from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 5;

select sum(rlp.Quantity) into @AA from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID in (7,8,9,10,11,12,13);

select sum(rlp.Quantity) into @PACKAGE from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 16;

select count(*) into @RUNS from RunLog where DutyDate > @FROMDATE and DutyDate < @TODATE;

select concat(monthname(@FROMDATE), ' ', year(@FROMDATE)) as Month, 
@RUNS as 'Total Runs', @BLOOD as 'Blood Boxes', 
@PLATELETS as 'Platelet Boxes', @PLASMA as 'Plasma Boxes', @SAMPLE as 'Samples', @MILK as 'Milk', @AA as 'AA Boxes', 
@PACKAGE as 'Packages';

select p.productID, p.Product, sum(rlp.Quantity) as BoxesCarried from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
where rl.DutyDate > @FROMDATE and rl.DutyDate < @TODATE
group by p.productID,Product
order by p.productID,Product;


select * from Product;
select * from RunLog;

select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
	, p.Product, sum(rlp.Quantity) as BoxesCarried from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
group by Month, Product
order by month(rl.DutyDate), Product;

select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as BloodProductRuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (1,2,3) -- Blood platelets & plasma
group by Month
order by month(rl.DutyDate);

select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as BloodPlasmaPlateletSamplePackageDrugRuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (1,2,3, 4, 15,16) -- Blood platelets,plasma, sample, package, drugs
group by Month
order by month(rl.DutyDate);

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as SampleRuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (4) -- Samples
group by Month
order by month(rl.DutyDate);

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as MilkRuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (5) -- Milk
group by Month
order by month(rl.DutyDate);

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as DrugRuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (15) -- Drugs
group by Month
order by month(rl.DutyDate);

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as PackageRuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (16) -- Package
group by Month
order by month(rl.DutyDate);

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, count(distinct rl.RunLogID) as AARuns from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID
AND rlp.ProductID in (7,8,9,10,11,12,13,14) -- AA
group by Month
order by month(rl.DutyDate);

select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, concat(m.FirstName, ' ', m.LastName) as RiderDriver, count(*) as Runs from RunLog rl 
join Member m on m.MemberID = rl.RiderMemberID
group by Month, RiderDriver
order by month(rl.DutyDate), count(*) desc;

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, l.Location as FinalDestination, count(*) Runs from RunLog rl 
join Location l on l.LocationID = rl.FinalDestinationLocationID
group by Month, l.Location
order by month(rl.DutyDate), Runs desc;

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, l.Location as PickupLocation, count(*) as Runs from RunLog rl 
join Location l on l.LocationID = rl.CollectionLocationID
group by Month, l.Location
order by month(rl.DutyDate), Runs desc;

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, l.Location as DropOffLocation, count(*) as Runs from RunLog rl 
join Location l on l.LocationID = rl.DeliverToLocationID
group by Month, l.Location
order by month(rl.DutyDate), Runs desc;

select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month
, l.Location as Caller, count(*) as Runs from RunLog rl 
join Location l on l.LocationID = rl.CallFromLocationID
group by Month, l.Location
order by month(rl.DutyDate), Runs desc;

call LastMonthRunStats;

select concat(FirstName, ' ', LastName) as 'Member', date(JoinDate) as 'Join Date', 
			            concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link' 
			            from Member  
			            where AdQualPassDate is null and LeaveDate is null order by JoinDate



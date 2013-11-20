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

-- BAD RIDER NAMES IN CALL LOG / MISSING RIDERS
select Rider, count(*), m.FirstName 
from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
where m.FirstName is null 
group by Rider, m.FirstName
order by count(*) desc;

-- USERS WHO HAVE LOGGED IN AND SET A PASSWORD
select * from User u join Member m on m.MemberID = u.MemberID where u.PasswordHash is not null and u.PasswordHash != '';

-- USERS WHO HAVE LOGGED IN
select * from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null order by lastLoginDate desc; 

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
select CallDate, CallTime, TIME(replace(CallTime, '.',':')), CONCAT(m.FirstName, ' ', m.LastName), Consignment, CollectFrom, Destination  from RawRunLog rr 
LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) 
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

select * from RunLog rl 
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
join Product p on p.ProductID = rlp.ProductID;

select * from Member m 
join User u on u.MemberID = m.MemberID 
where m.EmailAddress = 'servrunner@gmail.com';
--update User set PasswordHash = '' where UserId = 7;




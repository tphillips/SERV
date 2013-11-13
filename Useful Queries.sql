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
select * from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null;

-- Select all Mobile numbers for a set of tags
select distinct m.MobileNumber, CONCAT(m.FirstName, ' ', m.LastName) Name from Member m 
join Member_Tag mt on mt.MemberID = m.MemberID 
join Tag t on t.TagID = mt.TagID
where t.Tag in ('Fundraiser');

select * from Tag;

select * from Message

select distinct Controller from RawRunLog
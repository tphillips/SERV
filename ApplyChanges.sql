
/*
ALTER TABLE `SERV`.`Member` 
ADD COLUMN `OnDuty` TINYINT(1) NULL AFTER `LastGDPGMPDate`,
ADD COLUMN `LastLat` VARCHAR(45) NULL AFTER `OnDuty`,
ADD COLUMN `LastLng` VARCHAR(45) NULL AFTER `LastLat`;


/*   1.5.3

-- -----------------------------------------------------
-- Table `SERV`.`Calendar`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Calendar` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Calendar` (
  `CalendarID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `SimpleCalendar` TINYINT(1) NOT NULL DEFAULT 1,
  `SimpleDaysIncrement` INT NULL DEFAULT 14 COMMENT 'For simple calendars this is the number of days before the member gets rotad again.' /* comment truncated */ /*
14 means you get a shift every other week.*/,
  `SequentialDayCount` INT NULL COMMENT 'If a simple calendar and Simple days increment is null, then the auto scheduler will assign the member x days in a row on the calendar.  Allows for controllers doing 7 days in a row as AA controller.',
  `VolunteerRemainsFree` TINYINT(1) NOT NULL DEFAULT 1 COMMENT 'If set to 1 it means the member can be scheduled for another calendar where its set to one as well.' /* comment truncated */ /*
1+1 == OK.  1+0 || 0+1 == not ok*/,
  `RequiredTagID` INT NOT NULL COMMENT 'In order to be scheduled on this calendar, or volunteer for a shift, the ember must have this tag.',
  `DefaultRequirement` INT NOT NULL DEFAULT 4 COMMENT 'If not overridden in CalendarRequirements the system will try to achieve this number of volunteers per night',
  `LastGenerated` DATETIME NULL,
  `GeneratedUpTo` DATE NULL,
  PRIMARY KEY (`CalendarID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`CalendarEntry`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`CalendarEntry` ;

CREATE TABLE IF NOT EXISTS `SERV`.`CalendarEntry` (
  `CalendarEntryID` INT NOT NULL AUTO_INCREMENT,
  `CreateDateTime` TIMESTAMP NOT NULL,
  `CalendarID` INT NOT NULL,
  `EntryDate` DATE NOT NULL,
  `MemberID` INT NOT NULL,
  `CoverNeeded` TINYINT(1) NOT NULL DEFAULT 0,
  `CoverCalendarEntryID` INT NULL,
  `AdHoc` TINYINT(1) NOT NULL DEFAULT 0,
  `ManuallyAdded` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`CalendarEntryID`),
  INDEX `fk_CalendarEntry_Calendar1_idx` (`CalendarID` ASC),
  INDEX `fk_CalendarEntry_Member1_idx` (`MemberID` ASC),
  CONSTRAINT `fk_CalendarEntry_Calendar1`
    FOREIGN KEY (`CalendarID`)
    REFERENCES `SERV`.`Calendar` (`CalendarID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CalendarEntry_Member1`
    FOREIGN KEY (`MemberID`)
    REFERENCES `SERV`.`Member` (`MemberID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`CalendarRequirements`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`CalendarRequirements` ;

CREATE TABLE IF NOT EXISTS `SERV`.`CalendarRequirements` (
  `CalendarRequirementsID` INT NOT NULL AUTO_INCREMENT,
  `CalendarID` INT NOT NULL,
  `DayOfWeek` INT NOT NULL,
  `NumberNeeded` INT NOT NULL,
  PRIMARY KEY (`CalendarRequirementsID`),
  INDEX `fk_CalendarRequirements_Calendar1_idx` (`CalendarID` ASC),
  CONSTRAINT `fk_CalendarRequirements_Calendar1`
    FOREIGN KEY (`CalendarID`)
    REFERENCES `SERV`.`Calendar` (`CalendarID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Member_Calendar`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Member_Calendar` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Member_Calendar` (
  `Member_CalendarID` INT NOT NULL AUTO_INCREMENT,
  `MemberID` INT NOT NULL,
  `CalendarID` INT NOT NULL,
  `SetDayNo` INT NULL COMMENT 'The day of the week number the volunteer is rotad for ' /* comment truncated */ /*
Monday == 0*/,
  `Week` CHAR(1) NULL COMMENT 'Week 0 or week 1.  Allows every week of the year to be either week 1 or week 0' /* comment truncated */ /*
30th December 2013 = Start of week 0*/,
  `RecurrenceInterval` INT NULL,
  INDEX `fk_Member_Calendar_Member1_idx` (`MemberID` ASC),
  INDEX `fk_Member_Calendar_Calendar1_idx` (`CalendarID` ASC),
  PRIMARY KEY (`Member_CalendarID`),
  UNIQUE INDEX `UniqueMemberCalDayWeek` (`MemberID` ASC, `CalendarID` ASC, `SetDayNo` ASC, `Week` ASC),
  CONSTRAINT `fk_Member_Calendar_Member1`
    FOREIGN KEY (`MemberID`)
    REFERENCES `SERV`.`Member` (`MemberID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Member_Calendar_Calendar1`
    FOREIGN KEY (`CalendarID`)
    REFERENCES `SERV`.`Calendar` (`CalendarID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Blood', 1, 14, NULL, 0, 7, 4, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'AA Night', 1, 14, NULL, 1, 8, 1, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Day Controller', 1, 14, NULL, 1, 3, 1, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Night Controller', 1, 14, NULL, 0, 3, 1, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'AA Night Standby', 1, 14, NULL, 1, 8, 1, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'AA Daytime', 1, 14, NULL, 1, 8, 2, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Hooleygan', 1, 14, NULL, 0, 7, 1, NULL, NULL);



insert into Tag(Tag) values('OnRota'); 
insert into Tag(Tag) values('Committee'); 

select CONCAT('insert into Member_Tag values (',MemberID, ',' , 4, ');') from Member where memberID not in 
(
	select MemberID from Member_Tag where TagID = 4
);


insert into Member_Tag values (108,4);
insert into Member_Tag values (110,4);
insert into Member_Tag values (111,4);
insert into Member_Tag values (112,4);
insert into Member_Tag values (113,4);
insert into Member_Tag values (114,4);
insert into Member_Tag values (116,4);
insert into Member_Tag values (117,4);
insert into Member_Tag values (118,4);
insert into Member_Tag values (119,4);
insert into Member_Tag values (121,4);
insert into Member_Tag values (122,4);
insert into Member_Tag values (123,4);
insert into Member_Tag values (124,4);
insert into Member_Tag values (125,4);
insert into Member_Tag values (126,4);
insert into Member_Tag values (127,4);
insert into Member_Tag values (129,4);
insert into Member_Tag values (130,4);
insert into Member_Tag values (131,4);
insert into Member_Tag values (133,4);
insert into Member_Tag values (134,4);
insert into Member_Tag values (135,4);
insert into Member_Tag values (137,4);
insert into Member_Tag values (138,4);
insert into Member_Tag values (140,4);
insert into Member_Tag values (141,4);
insert into Member_Tag values (143,4);
insert into Member_Tag values (144,4);
insert into Member_Tag values (145,4);
insert into Member_Tag values (146,4);
insert into Member_Tag values (147,4);
insert into Member_Tag values (148,4);
insert into Member_Tag values (149,4);
insert into Member_Tag values (150,4);
insert into Member_Tag values (151,4);
insert into Member_Tag values (152,4);
insert into Member_Tag values (153,4);
insert into Member_Tag values (154,4);
insert into Member_Tag values (155,4);
insert into Member_Tag values (156,4);
insert into Member_Tag values (159,4);
insert into Member_Tag values (160,4);
insert into Member_Tag values (162,4);
insert into Member_Tag values (163,4);
insert into Member_Tag values (164,4);
insert into Member_Tag values (165,4);
insert into Member_Tag values (166,4);
insert into Member_Tag values (168,4);
insert into Member_Tag values (169,4);
insert into Member_Tag values (170,4);
insert into Member_Tag values (173,4);
insert into Member_Tag values (177,4);
insert into Member_Tag values (178,4);
insert into Member_Tag values (179,4);
insert into Member_Tag values (181,4);
insert into Member_Tag values (183,4);
insert into Member_Tag values (185,4);
insert into Member_Tag values (186,4);
insert into Member_Tag values (187,4);
insert into Member_Tag values (189,4);
insert into Member_Tag values (190,4);
insert into Member_Tag values (191,4);
insert into Member_Tag values (193,4);
insert into Member_Tag values (194,4);
insert into Member_Tag values (195,4);
insert into Member_Tag values (196,4);
insert into Member_Tag values (203,4);
insert into Member_Tag values (204,4);
insert into Member_Tag values (205,4);
insert into Member_Tag values (206,4);
insert into Member_Tag values (207,4);
insert into Member_Tag values (208,4);
insert into Member_Tag values (209,4);
insert into Member_Tag values (210,4);
insert into Member_Tag values (212,4);
insert into Member_Tag values (216,4);
insert into Member_Tag values (219,4);
insert into Member_Tag values (220,4);
insert into Member_Tag values (222,4);
insert into Member_Tag values (223,4);
insert into Member_Tag values (224,4);
insert into Member_Tag values (225,4);
insert into Member_Tag values (227,4);
insert into Member_Tag values (228,4);
insert into Member_Tag values (229,4);
insert into Member_Tag values (230,4);
insert into Member_Tag values (231,4);
insert into Member_Tag values (232,4);
insert into Member_Tag values (233,4);
insert into Member_Tag values (234,4);
insert into Member_Tag values (235,4);
insert into Member_Tag values (236,4);
insert into Member_Tag values (237,4);
insert into Member_Tag values (238,4);
insert into Member_Tag values (239,4);
insert into Member_Tag values (240,4);
insert into Member_Tag values (241,4);
insert into Member_Tag values (242,4);


/* V1.41 Applied

-- For canceled runs, memeberID needs to be !null
ALTER TABLE `SERV`.`RunLog` DROP FOREIGN KEY `fk_RunLog_Member1` ;
ALTER TABLE `SERV`.`RunLog` CHANGE COLUMN `RiderMemberID` `RiderMemberID` INT(11) NULL  , 
  ADD CONSTRAINT `fk_RunLog_Member1`
  FOREIGN KEY (`RiderMemberID` )
  REFERENCES `SERV`.`Member` (`MemberID` )
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `SERV`.`RunLog` DROP FOREIGN KEY `fk_RunLog_VehicleType1` ;
ALTER TABLE `SERV`.`RunLog` CHANGE COLUMN `VehicleTypeID` `VehicleTypeID` INT(11) NULL  , 
  ADD CONSTRAINT `fk_RunLog_VehicleType1`
  FOREIGN KEY (`VehicleTypeID` )
  REFERENCES `SERV`.`VehicleType` (`VehicleTypeID` )
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

USE `SERV`;
DROP procedure IF EXISTS `LastMonthRunStats`;

DELIMITER $$
USE `SERV`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `LastMonthRunStats`()
BEGIN
SET @FROMDATE= date_format((date_sub(CURRENT_DATE, INTERVAL 1 MONTH)), '%Y-%m-01 00:00:00');
SET @TODATE= date_format(CURRENT_DATE, '%Y-%m-01 00:00:00');

select sum(rlp.Quantity) into @BLOOD from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 1;

select sum(rlp.Quantity) into @PLATELETS from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 2;

select sum(rlp.Quantity) into @PLASMA from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 3;

select sum(rlp.Quantity) into @SAMPLE from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 4;

select sum(rlp.Quantity) into @MILK from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 5;

select sum(rlp.Quantity) into @AA from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID in (7,8,9,10,11,12,13);

select sum(rlp.Quantity) into @PACKAGE from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 16;

select count(*) into @RUNS from RunLog where RiderMemberID is not null and DutyDate > @FROMDATE and DutyDate < @TODATE;

select concat(monthname(@FROMDATE), ' ', year(@FROMDATE)) as Month, 
@RUNS as 'Total Runs', @BLOOD as 'Blood Boxes', 
@PLATELETS as 'Platelet Boxes', @PLASMA as 'Plasma Boxes', @SAMPLE as 'Samples', @MILK as 'Milk', @AA as 'AA Boxes', 
@PACKAGE as 'Packages';
END$$

DELIMITER ;

USE `SERV`;
DROP procedure IF EXISTS `ThisMonthRunStats`;

DELIMITER $$
USE `SERV`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ThisMonthRunStats`()
BEGIN
SET @TODATE= date_format((date_add(CURRENT_DATE, INTERVAL 1 MONTH)), '%Y-%m-01 00:00:00');
SET @FROMDATE= date_format(CURRENT_DATE, '%Y-%m-01 00:00:00');

select sum(rlp.Quantity) into @BLOOD from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 1;

select sum(rlp.Quantity) into @PLATELETS from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 2;

select sum(rlp.Quantity) into @PLASMA from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 3;

select sum(rlp.Quantity) into @SAMPLE from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 4;

select sum(rlp.Quantity) into @MILK from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 5;

select sum(rlp.Quantity) into @AA from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID in (7,8,9,10,11,12,13);

select sum(rlp.Quantity) into @PACKAGE from RunLog rl
join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID
where rl.RiderMemberID is not null and rl.DutyDate >= @FROMDATE and rl.DutyDate < @TODATE and rlp.productID = 16;

select count(*) into @RUNS from RunLog where RiderMemberID is not null and DutyDate > @FROMDATE and DutyDate < @TODATE;

select concat(monthname(@FROMDATE), ' ', year(@FROMDATE)) as Month, 
@RUNS as 'Total Runs', @BLOOD as 'Blood Boxes', 
@PLATELETS as 'Platelet Boxes', @PLASMA as 'Plasma Boxes', @SAMPLE as 'Samples', @MILK as 'Milk', @AA as 'AA Boxes', 
@PACKAGE as 'Packages';
END$$

DELIMITER ;

*/

/*

ALTER TABLE `SERV`.`RunLog` ADD COLUMN `Boxes` INT NOT NULL DEFAULT 0;
ALTER TABLE `SERV`.`RunLog` ADD COLUMN `Description` VARCHAR(300) NULL;


----- V1.2.1 Applied
------------------
ALTER TABLE`SERV`.`RunLog` ADD COLUMN
  `HomeSafeDateTime` DATETIME NULL
  

/*
----- V1.2 Applied
------------------

DROP TABLE IF EXISTS `SERV`.`RunLog_Product`;
CREATE TABLE IF NOT EXISTS `SERV`.`RunLog_Product` (
  `RunLog_ProductID` INT NOT NULL AUTO_INCREMENT ,
  `RunLogID` INT NOT NULL ,
  `ProductID` INT NOT NULL ,
  `Quantity` INT NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`RunLog_ProductID`) ,
  INDEX `fk_RunLog_Product_RunLog1_idx` (`RunLogID` ASC) ,
  INDEX `fk_RunLog_Product_Product1_idx` (`ProductID` ASC) ,
  CONSTRAINT `fk_RunLog_Product_RunLog1`
    FOREIGN KEY (`RunLogID` )
    REFERENCES `SERV`.`RunLog` (`RunLogID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Product_Product1`
    FOREIGN KEY (`ProductID` )
    REFERENCES `SERV`.`Product` (`ProductID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


----- V1.1 Applied
------------------

DROP TABLE IF EXISTS `SERV`.`RunLog_Product`;
DROP TABLE IF EXISTS `SERV`.`Product`;
DROP TABLE IF EXISTS `SERV`.`RunLog`;
DROP TABLE IF EXISTS `SERV`.`Location`;
CREATE  TABLE IF NOT EXISTS `SERV`.`Location` (
  `LocationID` INT NOT NULL AUTO_INCREMENT ,
  `Location` VARCHAR(45) NULL ,
  `Lat` VARCHAR(45) NULL ,
  `Lng` VARCHAR(45) NULL ,
  `Hospital` TINYINT(1) NOT NULL DEFAULT false ,
  `Changeover` TINYINT(1) NOT NULL DEFAULT false ,
  `BloodBank` TINYINT(1) NOT NULL DEFAULT false ,
  `Enabled` TINYINT(1) NOT NULL DEFAULT true ,
  PRIMARY KEY (`LocationID`) )
ENGINE = InnoDB;
TRUNCATE TABLE `SERV`.`Location`;
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'NBS Tooting', NULL, NULL, 0, 0, 1, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Hooley', NULL, NULL, 0, 1, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Royal Surrey', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'St Peter\'s', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'St Thomas\'', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Frimley Park', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'East Surrey', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Redhill Aerodrome', NULL, NULL, 0, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'King\'s College', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Guy\'s', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Queen Charlotte\'s', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Darent Valley', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'NBS Colindale', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'St Helier', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Great Ormond Street', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Royal Brompton', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Farnham Hospital', NULL, NULL, 1, 0, 0, 1);
INSERT INTO `SERV`.`Location` (`LocationID`, `Location`, `Lat`, `Lng`, `Hospital`, `Changeover`, `BloodBank`, `Enabled`) VALUES (NULL, 'Epsom General', NULL, NULL, 1, 0, 0, 1);

DROP TABLE IF EXISTS `SERV`.`VehicleType`;
CREATE  TABLE IF NOT EXISTS `SERV`.`VehicleType` (
  `VehicleTypeID` INT NOT NULL AUTO_INCREMENT ,
  `VehicleType` VARCHAR(45) NULL ,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`VehicleTypeID`) )
ENGINE = InnoDB;
TRUNCATE TABLE `SERV`.`VehicleType`;
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'Bike', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'Car', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'TB1', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'TB2', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'TB3', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'The Pig', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'Florence', 1);

DROP TABLE IF EXISTS `SERV`.`Product`;
CREATE  TABLE IF NOT EXISTS `SERV`.`Product` (
  `ProductID` INT NOT NULL AUTO_INCREMENT ,
  `Product` VARCHAR(100) NOT NULL ,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`ProductID`) )
ENGINE = InnoDB;
TRUNCATE TABLE `SERV`.`Product`;
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Blood', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Platelets', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Plasma', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Sample', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Human Milk', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Water Sample', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH1', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH2', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH3', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH4', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH5', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH6', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH7', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'RH8', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Drugs', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Package', 1);
INSERT INTO `SERV`.`Product` (`ProductID`, `Product`, `Enabled`) VALUES (NULL, 'Other', 1);

DROP TABLE IF EXISTS `SERV`.`RunLog`;
CREATE  TABLE IF NOT EXISTS `SERV`.`RunLog` (
  `RunLogID` INT NOT NULL AUTO_INCREMENT ,
  `CreatedByUserID` INT NOT NULL ,
  `CreateDate` TIMESTAMP NOT NULL ,
  `DutyDate` DATETIME NULL ,
  `CallDateTime` DATETIME NULL ,
  `CollectionLocationID` INT NOT NULL ,
  `CollectDateTime` DATETIME NULL ,
  `DeliverDateTime` DATETIME NULL ,
  `FinalDestinationLocationID` INT NOT NULL ,
  `ControllerMemberID` INT NOT NULL ,
  `Urgency` INT NOT NULL ,
  `IsTransfer` TINYINT(1) NOT NULL DEFAULT 0 ,
  `VehicleTypeID` INT NOT NULL ,
  `Notes` VARCHAR(600) NULL ,
  `OriginLocationID` INT NOT NULL ,
  `CallFromLocationID` INT NOT NULL ,
  `RiderMemberID` INT NOT NULL ,
  `DeliverToLocationID` INT NOT NULL ,
  PRIMARY KEY (`RunLogID`) ,
  INDEX `fk_RunLog_VehicleType1_idx` (`VehicleTypeID` ASC) ,
  INDEX `fk_RunLog_User1_idx` (`CreatedByUserID` ASC) ,
  INDEX `fk_RunLog_Member1_idx` (`RiderMemberID` ASC) ,
  INDEX `fk_RunLog_Location1_idx` (`DeliverToLocationID` ASC) ,
  CONSTRAINT `fk_RunLog_VehicleType1`
    FOREIGN KEY (`VehicleTypeID` )
    REFERENCES `SERV`.`VehicleType` (`VehicleTypeID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_User1`
    FOREIGN KEY (`CreatedByUserID` )
    REFERENCES `SERV`.`User` (`UserID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Member1`
    FOREIGN KEY (`RiderMemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Location1`
    FOREIGN KEY (`DeliverToLocationID` )
    REFERENCES `SERV`.`Location` (`LocationID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

DROP TABLE IF EXISTS `SERV`.`RunLog_Product`;
CREATE  TABLE IF NOT EXISTS `SERV`.`RunLog_Product` (
  `RunLog_ProductID` INT NOT NULL ,
  `RunLogID` INT NOT NULL ,
  `ProductID` INT NOT NULL ,
  `Quantity` INT NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`RunLog_ProductID`) ,
  INDEX `fk_RunLog_Product_RunLog1_idx` (`RunLogID` ASC) ,
  INDEX `fk_RunLog_Product_Product1_idx` (`ProductID` ASC) ,
  CONSTRAINT `fk_RunLog_Product_RunLog1`
    FOREIGN KEY (`RunLogID` )
    REFERENCES `SERV`.`RunLog` (`RunLogID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Product_Product1`
    FOREIGN KEY (`ProductID` )
    REFERENCES `SERV`.`Product` (`ProductID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;
*/
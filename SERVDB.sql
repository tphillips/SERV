SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema SERV
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `SERV` ;
CREATE SCHEMA IF NOT EXISTS `SERV` DEFAULT CHARACTER SET koi8r ;
USE `SERV` ;

-- -----------------------------------------------------
-- Table `SERV`.`Member`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Member` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Member` (
  `MemberID` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(45) NOT NULL,
  `LastName` VARCHAR(45) NOT NULL,
  `JoinDate` DATETIME NULL,
  `EmailAddress` VARCHAR(60) NOT NULL,
  `MobileNumber` VARCHAR(12) NOT NULL,
  `HomeNumber` VARCHAR(12) NULL,
  `Occupation` VARCHAR(45) NULL,
  `MemberStatusID` INT NOT NULL,
  `AvailabilityID` INT NULL,
  `RiderAssesmentPassDate` DATETIME NULL,
  `AdQualPassDate` DATETIME NULL,
  `AdQualType` VARCHAR(15) NULL,
  `BikeType` VARCHAR(45) NULL,
  `CarType` VARCHAR(45) NULL,
  `Notes` VARCHAR(400) NULL,
  `Address1` VARCHAR(45) NULL,
  `Address2` VARCHAR(45) NULL,
  `Address3` VARCHAR(45) NULL,
  `Town` VARCHAR(45) NULL,
  `County` VARCHAR(45) NULL,
  `PostCode` VARCHAR(10) NULL,
  `BirthYear` INT NULL,
  `NextOfKin` VARCHAR(80) NULL,
  `NextOfKinAddress` VARCHAR(200) NULL,
  `NextOfKinPhone` VARCHAR(45) NULL,
  `LegalConfirmation` TINYINT(1) NULL,
  `LeaveDate` DATETIME NULL,
  `LastGDPGMPDate` DATETIME NULL,
  PRIMARY KEY (`MemberID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`UserLevel`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`UserLevel` ;

CREATE TABLE IF NOT EXISTS `SERV`.`UserLevel` (
  `UserLevelID` INT NOT NULL AUTO_INCREMENT,
  `UserLevel` VARCHAR(45) NULL,
  PRIMARY KEY (`UserLevelID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`User` ;

CREATE TABLE IF NOT EXISTS `SERV`.`User` (
  `UserID` INT NOT NULL AUTO_INCREMENT,
  `MemberID` INT NOT NULL,
  `UserLevelID` INT NOT NULL,
  `PasswordHash` VARCHAR(45) NOT NULL,
  `LastLoginDate` TIMESTAMP NULL,
  PRIMARY KEY (`UserID`),
  INDEX `fk_User_Member_idx` (`MemberID` ASC),
  INDEX `fk_User_UserLevel1_idx` (`UserLevelID` ASC),
  CONSTRAINT `fk_User_Member`
    FOREIGN KEY (`MemberID`)
    REFERENCES `SERV`.`Member` (`MemberID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_UserLevel1`
    FOREIGN KEY (`UserLevelID`)
    REFERENCES `SERV`.`UserLevel` (`UserLevelID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Availability`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Availability` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Availability` (
  `AvailabilityID` INT NOT NULL,
  `DayNo` INT NULL,
  `EveningNo` INT NULL,
  `Available` TINYINT(1) NULL,
  PRIMARY KEY (`AvailabilityID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`MessageType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`MessageType` ;

CREATE TABLE IF NOT EXISTS `SERV`.`MessageType` (
  `MessageTypeID` INT NOT NULL AUTO_INCREMENT,
  `MessageType` VARCHAR(45) NULL,
  PRIMARY KEY (`MessageTypeID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Message`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Message` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Message` (
  `MessageID` INT NOT NULL AUTO_INCREMENT,
  `SenderUserID` INT NOT NULL,
  `SentDate` DATETIME NOT NULL,
  `Recipient` VARCHAR(4000) NOT NULL,
  `Message` VARCHAR(1000) NOT NULL,
  `RecipientMemberID` INT NULL,
  `MessageTypeID` INT NOT NULL,
  PRIMARY KEY (`MessageID`),
  INDEX `fk_Message_MessageType1_idx` (`MessageTypeID` ASC),
  INDEX `fk_Message_User1_idx` (`SenderUserID` ASC),
  CONSTRAINT `fk_Message_MessageType1`
    FOREIGN KEY (`MessageTypeID`)
    REFERENCES `SERV`.`MessageType` (`MessageTypeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Message_User1`
    FOREIGN KEY (`SenderUserID`)
    REFERENCES `SERV`.`User` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`MemberStatus`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`MemberStatus` ;

CREATE TABLE IF NOT EXISTS `SERV`.`MemberStatus` (
  `MemberStatusID` INT NOT NULL AUTO_INCREMENT,
  `MemberStatus` VARCHAR(45) NULL,
  PRIMARY KEY (`MemberStatusID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Tag`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Tag` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Tag` (
  `TagID` INT NOT NULL AUTO_INCREMENT,
  `Tag` VARCHAR(45) NULL,
  PRIMARY KEY (`TagID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Member_Tag`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Member_Tag` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Member_Tag` (
  `MemberID` INT NOT NULL,
  `TagID` INT NOT NULL,
  INDEX `fk_Member_Capability_Member1_idx` (`MemberID` ASC),
  INDEX `fk_Member_Capability_Capability1_idx` (`TagID` ASC),
  CONSTRAINT `fk_Member_Capability_Member1`
    FOREIGN KEY (`MemberID`)
    REFERENCES `SERV`.`Member` (`MemberID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Member_Capability_Capability1`
    FOREIGN KEY (`TagID`)
    REFERENCES `SERV`.`Tag` (`TagID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Location`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Location` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Location` (
  `LocationID` INT NOT NULL AUTO_INCREMENT,
  `Location` VARCHAR(45) NULL,
  `Lat` VARCHAR(45) NULL,
  `Lng` VARCHAR(45) NULL,
  `Hospital` TINYINT(1) NOT NULL DEFAULT false,
  `Changeover` TINYINT(1) NOT NULL DEFAULT false,
  `BloodBank` TINYINT(1) NOT NULL DEFAULT false,
  `Enabled` TINYINT(1) NOT NULL DEFAULT true,
  PRIMARY KEY (`LocationID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`RawRunLog`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`RawRunLog` ;

CREATE TABLE IF NOT EXISTS `SERV`.`RawRunLog` (
  `RawRunLogID` INT NOT NULL AUTO_INCREMENT,
  `CallDate` DATETIME NULL,
  `CallTime` VARCHAR(45) NULL,
  `Destination` VARCHAR(45) NULL,
  `CollectFrom` VARCHAR(45) NULL,
  `CollectTime` VARCHAR(45) NULL,
  `DeliveryTime` VARCHAR(45) NULL,
  `Consignment` VARCHAR(45) NULL,
  `Urgency` VARCHAR(45) NULL,
  `Controller` VARCHAR(45) NULL,
  `Rider` VARCHAR(45) NULL,
  `Notes` VARCHAR(2000) NULL,
  `CollectTime2` VARCHAR(45) NULL,
  `Vehicle` VARCHAR(45) NULL,
  PRIMARY KEY (`RawRunLogID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Product`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Product` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Product` (
  `ProductID` INT NOT NULL AUTO_INCREMENT,
  `Product` VARCHAR(100) NOT NULL,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`ProductID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`VehicleType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`VehicleType` ;

CREATE TABLE IF NOT EXISTS `SERV`.`VehicleType` (
  `VehicleTypeID` INT NOT NULL AUTO_INCREMENT,
  `VehicleType` VARCHAR(45) NULL,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`VehicleTypeID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`RunLog`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`RunLog` ;

CREATE TABLE IF NOT EXISTS `SERV`.`RunLog` (
  `RunLogID` INT NOT NULL AUTO_INCREMENT,
  `CreatedByUserID` INT NOT NULL,
  `CreateDate` TIMESTAMP NOT NULL,
  `DutyDate` DATETIME NULL,
  `CallDateTime` DATETIME NULL,
  `CollectionLocationID` INT NOT NULL,
  `CollectDateTime` DATETIME NULL,
  `DeliverDateTime` DATETIME NULL,
  `FinalDestinationLocationID` INT NOT NULL,
  `ControllerMemberID` INT NOT NULL,
  `Urgency` INT NOT NULL,
  `IsTransfer` TINYINT(1) NOT NULL DEFAULT 0,
  `VehicleTypeID` INT NULL,
  `Notes` VARCHAR(600) NULL,
  `OriginLocationID` INT NOT NULL,
  `CallFromLocationID` INT NOT NULL,
  `RiderMemberID` INT NULL,
  `DeliverToLocationID` INT NOT NULL,
  `HomeSafeDateTime` DATETIME NULL,
  `Boxes` INT NOT NULL DEFAULT 0,
  `Description` VARCHAR(300) NULL,
  PRIMARY KEY (`RunLogID`),
  INDEX `fk_RunLog_VehicleType1_idx` (`VehicleTypeID` ASC),
  INDEX `fk_RunLog_User1_idx` (`CreatedByUserID` ASC),
  INDEX `fk_RunLog_Member1_idx` (`RiderMemberID` ASC),
  INDEX `fk_RunLog_Location1_idx` (`DeliverToLocationID` ASC),
  CONSTRAINT `fk_RunLog_VehicleType1`
    FOREIGN KEY (`VehicleTypeID`)
    REFERENCES `SERV`.`VehicleType` (`VehicleTypeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_User1`
    FOREIGN KEY (`CreatedByUserID`)
    REFERENCES `SERV`.`User` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Member1`
    FOREIGN KEY (`RiderMemberID`)
    REFERENCES `SERV`.`Member` (`MemberID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Location1`
    FOREIGN KEY (`DeliverToLocationID`)
    REFERENCES `SERV`.`Location` (`LocationID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`RunLog_Product`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`RunLog_Product` ;

CREATE TABLE IF NOT EXISTS `SERV`.`RunLog_Product` (
  `RunLog_ProductID` INT NOT NULL AUTO_INCREMENT,
  `RunLogID` INT NOT NULL,
  `ProductID` INT NOT NULL,
  `Quantity` INT NOT NULL DEFAULT 1,
  PRIMARY KEY (`RunLog_ProductID`),
  INDEX `fk_RunLog_Product_Product1_idx` (`ProductID` ASC),
  INDEX `fk_RunLog_Product_RunLog1_idx` (`RunLogID` ASC),
  CONSTRAINT `fk_RunLog_Product_Product1`
    FOREIGN KEY (`ProductID`)
    REFERENCES `SERV`.`Product` (`ProductID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RunLog_Product_RunLog1`
    FOREIGN KEY (`RunLogID`)
    REFERENCES `SERV`.`RunLog` (`RunLogID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Karma`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Karma` ;

CREATE TABLE IF NOT EXISTS `SERV`.`Karma` (
  `KarmaID` INT NOT NULL AUTO_INCREMENT,
  `AllocationDateTime` TIMESTAMP NOT NULL,
  `MemberID` INT NOT NULL,
  `Reason` VARCHAR(100) NOT NULL,
  `Points` INT NOT NULL,
  PRIMARY KEY (`KarmaID`))
ENGINE = InnoDB;


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
  `SortOrder` INT NULL DEFAULT 0,
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


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `SERV`.`Member`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Tris', 'Phillips (Dev Box)', '2011-01-01', 'tris.phillips@gmail.com', '07429386911', NULL, 'Developer', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'RH69SD', 1980, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Louis', 'Lane', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Peter', 'Parker', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Stanley', 'Stroman', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Brendon', 'Bodden', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Zackary', 'Zawislak', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Jerrod', 'Junk', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Wilber', 'Welles', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Henry', 'Taylor', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Edward', 'Phillips', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Ernest', 'Patterson', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Norma', 'Kelly', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Beverly', 'James', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Charles', 'Martin', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Donna', 'Cox', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `SERV`.`Member` (`MemberID`, `FirstName`, `LastName`, `JoinDate`, `EmailAddress`, `MobileNumber`, `HomeNumber`, `Occupation`, `MemberStatusID`, `AvailabilityID`, `RiderAssesmentPassDate`, `AdQualPassDate`, `AdQualType`, `BikeType`, `CarType`, `Notes`, `Address1`, `Address2`, `Address3`, `Town`, `County`, `PostCode`, `BirthYear`, `NextOfKin`, `NextOfKinAddress`, `NextOfKinPhone`, `LegalConfirmation`, `LeaveDate`, `LastGDPGMPDate`) VALUES (NULL, 'Bonnie', 'Lopez', NULL, '@', '07', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`UserLevel`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`UserLevel` (`UserLevelID`, `UserLevel`) VALUES (NULL, 'Volunteer');
INSERT INTO `SERV`.`UserLevel` (`UserLevelID`, `UserLevel`) VALUES (NULL, 'Controller');
INSERT INTO `SERV`.`UserLevel` (`UserLevelID`, `UserLevel`) VALUES (NULL, 'Committee');
INSERT INTO `SERV`.`UserLevel` (`UserLevelID`, `UserLevel`) VALUES (NULL, 'Admin');

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`User`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 1, 4, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 2, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 3, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 4, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 5, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 6, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 7, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 8, 1, '', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 9, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 10, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 11, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 12, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 13, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 14, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 15, 1, ' ', NULL);
INSERT INTO `SERV`.`User` (`UserID`, `MemberID`, `UserLevelID`, `PasswordHash`, `LastLoginDate`) VALUES (NULL, 16, 1, ' ', NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`MessageType`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`MessageType` (`MessageTypeID`, `MessageType`) VALUES (NULL, 'Email');
INSERT INTO `SERV`.`MessageType` (`MessageTypeID`, `MessageType`) VALUES (NULL, 'SMS');

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`MemberStatus`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`MemberStatus` (`MemberStatusID`, `MemberStatus`) VALUES (NULL, 'Active');
INSERT INTO `SERV`.`MemberStatus` (`MemberStatusID`, `MemberStatus`) VALUES (NULL, 'Training');
INSERT INTO `SERV`.`MemberStatus` (`MemberStatusID`, `MemberStatus`) VALUES (NULL, 'Inactive');

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`Tag`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Rider');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Driver');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Controller');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'EmergencyList');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, '4x4');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Fundraiser');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Blood');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'AA');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Milk');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Water');

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`Member_Tag`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 2);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 3);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 4);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 5);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 6);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 7);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (1, 8);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (2, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (3, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (4, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (5, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (6, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (7, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (8, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (9, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (10, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (11, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (12, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (13, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (14, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (15, 1);
INSERT INTO `SERV`.`Member_Tag` (`MemberID`, `TagID`) VALUES (16, 1);

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`Location`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
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

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`Product`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
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

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`VehicleType`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'Bike', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'Car', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'TB1', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'TB2', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'TB3', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'The Pig', 1);
INSERT INTO `SERV`.`VehicleType` (`VehicleTypeID`, `VehicleType`, `Enabled`) VALUES (NULL, 'Florence', 1);

COMMIT;


-- -----------------------------------------------------
-- Data for table `SERV`.`Calendar`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Blood', 1, 14, NULL, 0, 7, 4, NULL, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'AA Night', 1, 14, NULL, 1, 8, 1, NULL, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Day Controller', 1, 14, NULL, 1, 3, 1, NULL, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Night Controller', 1, 14, NULL, 0, 3, 1, NULL, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'AA Night Standby', 1, 14, NULL, 1, 8, 1, NULL, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'AA Daytime', 1, 14, NULL, 1, 8, 2, NULL, NULL, NULL);
INSERT INTO `SERV`.`Calendar` (`CalendarID`, `Name`, `SimpleCalendar`, `SimpleDaysIncrement`, `SequentialDayCount`, `VolunteerRemainsFree`, `RequiredTagID`, `DefaultRequirement`, `SortOrder`, `LastGenerated`, `GeneratedUpTo`) VALUES (NULL, 'Hooleygan', 1, 14, NULL, 0, 7, 1, NULL, NULL, NULL);

COMMIT;


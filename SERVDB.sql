SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `SERV` DEFAULT CHARACTER SET koi8r ;
USE `SERV` ;

-- -----------------------------------------------------
-- Table `SERV`.`MemberStatus`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`MemberStatus` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`MemberStatus` (
  `MemberStatusID` INT NOT NULL AUTO_INCREMENT ,
  `MemberStatus` VARCHAR(45) NULL ,
  PRIMARY KEY (`MemberStatusID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Availability`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Availability` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Availability` (
  `AvailabilityID` INT NOT NULL ,
  `DayNo` INT NULL ,
  `EveningNo` INT NULL ,
  `Available` TINYINT(1) NULL ,
  PRIMARY KEY (`AvailabilityID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Member`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Member` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Member` (
  `MemberID` INT NOT NULL AUTO_INCREMENT ,
  `FirstName` VARCHAR(45) NOT NULL ,
  `LastName` VARCHAR(45) NOT NULL ,
  `JoinDate` DATETIME NULL ,
  `EmailAddress` VARCHAR(60) NOT NULL ,
  `MobileNumber` VARCHAR(12) NOT NULL ,
  `HomeNumber` VARCHAR(12) NULL ,
  `Occupation` VARCHAR(45) NULL ,
  `MemberStatusID` INT NOT NULL ,
  `AvailabilityID` INT NULL ,
  `RiderAssesmentPassDate` DATETIME NULL ,
  `AdQualPassDate` DATETIME NULL ,
  `AdQualType` VARCHAR(15) NULL ,
  `BikeType` VARCHAR(45) NULL ,
  `CarType` VARCHAR(45) NULL ,
  `Notes` VARCHAR(400) NULL ,
  `Address1` VARCHAR(45) NULL ,
  `Address2` VARCHAR(45) NULL ,
  `Address3` VARCHAR(45) NULL ,
  `Town` VARCHAR(45) NULL ,
  `County` VARCHAR(45) NULL ,
  `PostCode` VARCHAR(10) NULL ,
  `BirthYear` INT NULL ,
  `NextOfKin` VARCHAR(80) NULL ,
  `NextOfKinAddress` VARCHAR(200) NULL ,
  `NextOfKinPhone` VARCHAR(45) NULL ,
  `LegalConfirmation` TINYINT(1) NULL ,
  `LeaveDate` DATETIME NULL ,
  `LastGDPGMPDate` DATETIME NULL ,
  PRIMARY KEY (`MemberID`) ,
  INDEX `fk_Member_MemberStatus1_idx` (`MemberStatusID` ASC) ,
  INDEX `fk_Member_Availability1_idx` (`AvailabilityID` ASC) ,
  CONSTRAINT `fk_Member_MemberStatus1`
    FOREIGN KEY (`MemberStatusID` )
    REFERENCES `SERV`.`MemberStatus` (`MemberStatusID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Member_Availability1`
    FOREIGN KEY (`AvailabilityID` )
    REFERENCES `SERV`.`Availability` (`AvailabilityID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`UserLevel`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`UserLevel` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`UserLevel` (
  `UserLevelID` INT NOT NULL AUTO_INCREMENT ,
  `UserLevel` VARCHAR(45) NULL ,
  PRIMARY KEY (`UserLevelID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`User` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`User` (
  `UserID` INT NOT NULL AUTO_INCREMENT ,
  `MemberID` INT NOT NULL ,
  `UserLevelID` INT NOT NULL ,
  `PasswordHash` VARCHAR(45) NOT NULL ,
  `LastLoginDate` TIMESTAMP NULL ,
  PRIMARY KEY (`UserID`) ,
  INDEX `fk_User_Member_idx` (`MemberID` ASC) ,
  INDEX `fk_User_UserLevel1_idx` (`UserLevelID` ASC) ,
  CONSTRAINT `fk_User_Member`
    FOREIGN KEY (`MemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_User_UserLevel1`
    FOREIGN KEY (`UserLevelID` )
    REFERENCES `SERV`.`UserLevel` (`UserLevelID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Duty`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Duty` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Duty` (
  `DutyID` INT NOT NULL AUTO_INCREMENT ,
  `Duty` VARCHAR(45) NULL ,
  PRIMARY KEY (`DutyID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Calendar`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Calendar` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Calendar` (
  `CalendarID` INT NOT NULL ,
  `DutyID` INT NOT NULL ,
  PRIMARY KEY (`CalendarID`) ,
  INDEX `fk_Calendar_Duty1_idx` (`DutyID` ASC) ,
  CONSTRAINT `fk_Calendar_Duty1`
    FOREIGN KEY (`DutyID` )
    REFERENCES `SERV`.`Duty` (`DutyID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`MessageType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`MessageType` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`MessageType` (
  `MessageTypeID` INT NOT NULL AUTO_INCREMENT ,
  `MessageType` VARCHAR(45) NULL ,
  PRIMARY KEY (`MessageTypeID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Message`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Message` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Message` (
  `MessageID` INT NOT NULL AUTO_INCREMENT ,
  `SenderUserID` INT NOT NULL ,
  `SentDate` DATETIME NOT NULL ,
  `Recipient` VARCHAR(4000) NOT NULL ,
  `Message` VARCHAR(1000) NOT NULL ,
  `RecipientMemberID` INT NULL ,
  `MessageTypeID` INT NOT NULL ,
  PRIMARY KEY (`MessageID`) ,
  INDEX `fk_Message_Member1_idx` (`RecipientMemberID` ASC) ,
  INDEX `fk_Message_MessageType1_idx` (`MessageTypeID` ASC) ,
  INDEX `fk_Message_User1_idx` (`SenderUserID` ASC) ,
  CONSTRAINT `fk_Message_Member1`
    FOREIGN KEY (`RecipientMemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Message_MessageType1`
    FOREIGN KEY (`MessageTypeID` )
    REFERENCES `SERV`.`MessageType` (`MessageTypeID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Message_User1`
    FOREIGN KEY (`SenderUserID` )
    REFERENCES `SERV`.`User` (`UserID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Member_Duty`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Member_Duty` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Member_Duty` (
  `Member_DutyID` INT NOT NULL ,
  `MemberID` INT NOT NULL ,
  `DutyID` INT NOT NULL ,
  INDEX `fk_Member_Duty_Member1_idx` (`MemberID` ASC) ,
  INDEX `fk_Member_Duty_Duty1_idx` (`DutyID` ASC) ,
  PRIMARY KEY (`Member_DutyID`) ,
  CONSTRAINT `fk_Member_Duty_Member1`
    FOREIGN KEY (`MemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Member_Duty_Duty1`
    FOREIGN KEY (`DutyID` )
    REFERENCES `SERV`.`Duty` (`DutyID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`FleetVehicle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`FleetVehicle` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`FleetVehicle` (
  `FleetVehicleID` INT NOT NULL ,
  `MemberID` INT NOT NULL ,
  PRIMARY KEY (`FleetVehicleID`) ,
  INDEX `fk_FleetVehicle_Member1_idx` (`MemberID` ASC) ,
  CONSTRAINT `fk_FleetVehicle_Member1`
    FOREIGN KEY (`MemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Tag`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Tag` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Tag` (
  `TagID` INT NOT NULL AUTO_INCREMENT ,
  `Tag` VARCHAR(45) NULL ,
  PRIMARY KEY (`TagID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Member_Tag`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Member_Tag` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Member_Tag` (
  `MemberID` INT NOT NULL ,
  `TagID` INT NOT NULL ,
  INDEX `fk_Member_Capability_Member1_idx` (`MemberID` ASC) ,
  INDEX `fk_Member_Capability_Capability1_idx` (`TagID` ASC) ,
  CONSTRAINT `fk_Member_Capability_Member1`
    FOREIGN KEY (`MemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Member_Capability_Capability1`
    FOREIGN KEY (`TagID` )
    REFERENCES `SERV`.`Tag` (`TagID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`CalendarEntry`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`CalendarEntry` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`CalendarEntry` (
  `CalendarEntryID` INT NOT NULL AUTO_INCREMENT ,
  `DateTime` DATETIME NOT NULL ,
  `Notes` VARCHAR(200) NULL ,
  `CalendarID` INT NOT NULL ,
  `MemberID` INT NOT NULL ,
  `PrimaryRoleTagID` INT NOT NULL ,
  `CreatedByUserID` INT NOT NULL ,
  `ModdedByUserID` INT NOT NULL ,
  `Created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ,
  `Modded` TIMESTAMP NULL ,
  `Recurrance` VARCHAR(3) NULL ,
  PRIMARY KEY (`CalendarEntryID`) ,
  INDEX `fk_CalendarEntry_Calendar1_idx` (`CalendarID` ASC) ,
  INDEX `fk_CalendarEntry_Member1_idx` (`MemberID` ASC) ,
  INDEX `fk_CalendarEntry_Capability1_idx` (`PrimaryRoleTagID` ASC) ,
  INDEX `fk_CalendarEntry_User1_idx` (`CreatedByUserID` ASC) ,
  INDEX `fk_CalendarEntry_User2_idx` (`ModdedByUserID` ASC) ,
  CONSTRAINT `fk_CalendarEntry_Calendar1`
    FOREIGN KEY (`CalendarID` )
    REFERENCES `SERV`.`Calendar` (`CalendarID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CalendarEntry_Member1`
    FOREIGN KEY (`MemberID` )
    REFERENCES `SERV`.`Member` (`MemberID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CalendarEntry_Capability1`
    FOREIGN KEY (`PrimaryRoleTagID` )
    REFERENCES `SERV`.`Tag` (`TagID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CalendarEntry_User1`
    FOREIGN KEY (`CreatedByUserID` )
    REFERENCES `SERV`.`User` (`UserID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CalendarEntry_User2`
    FOREIGN KEY (`ModdedByUserID` )
    REFERENCES `SERV`.`User` (`UserID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Location`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Location` ;

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


-- -----------------------------------------------------
-- Table `SERV`.`RawRunLog`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`RawRunLog` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`RawRunLog` (
  `RawRunLogID` INT NOT NULL AUTO_INCREMENT ,
  `CallDate` DATETIME NULL ,
  `CallTime` VARCHAR(45) NULL ,
  `Destination` VARCHAR(45) NULL ,
  `CollectFrom` VARCHAR(45) NULL ,
  `CollectTime` VARCHAR(45) NULL ,
  `DeliveryTime` VARCHAR(45) NULL ,
  `Consignment` VARCHAR(45) NULL ,
  `Urgency` VARCHAR(45) NULL ,
  `Controller` VARCHAR(45) NULL ,
  `Rider` VARCHAR(45) NULL ,
  `Notes` VARCHAR(2000) NULL ,
  `CollectTime2` VARCHAR(45) NULL ,
  `Vehicle` VARCHAR(45) NULL ,
  PRIMARY KEY (`RawRunLogID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`Product`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`Product` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`Product` (
  `ProductID` INT NOT NULL AUTO_INCREMENT ,
  `Product` VARCHAR(100) NOT NULL ,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`ProductID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`VehicleType`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`VehicleType` ;

CREATE  TABLE IF NOT EXISTS `SERV`.`VehicleType` (
  `VehicleTypeID` INT NOT NULL AUTO_INCREMENT ,
  `VehicleType` VARCHAR(45) NULL ,
  `Enabled` TINYINT(1) NOT NULL DEFAULT 1 ,
  PRIMARY KEY (`VehicleTypeID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SERV`.`RunLog`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`RunLog` ;

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


-- -----------------------------------------------------
-- Table `SERV`.`RunLog_Product`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SERV`.`RunLog_Product` ;

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



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

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
-- Data for table `SERV`.`Duty`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`Duty` (`DutyID`, `Duty`) VALUES (NULL, 'Blood');
INSERT INTO `SERV`.`Duty` (`DutyID`, `Duty`) VALUES (NULL, 'Milk');
INSERT INTO `SERV`.`Duty` (`DutyID`, `Duty`) VALUES (NULL, 'Water');
INSERT INTO `SERV`.`Duty` (`DutyID`, `Duty`) VALUES (NULL, 'Air Ambulance');

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
-- Data for table `SERV`.`Tag`
-- -----------------------------------------------------
START TRANSACTION;
USE `SERV`;
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Rider');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Driver');
INSERT INTO `SERV`.`Tag` (`TagID`, `Tag`) VALUES (NULL, 'Controller');

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

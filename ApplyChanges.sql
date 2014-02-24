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
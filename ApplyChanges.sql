
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

CREATE  TABLE IF NOT EXISTS `SERV`.`RunLog` (
  `RunLogID` INT NOT NULL AUTO_INCREMENT ,
  `CreatedByUserID` INT NOT NULL ,
  `CreateDate` TIMESTAMP NOT NULL ,
  `DutyDate` DATETIME NULL ,
  `CallDateTime` VARCHAR(45) NULL ,
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

-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: easy_ticket
-- ------------------------------------------------------
-- Server version	8.0.46

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `event`
--

DROP TABLE IF EXISTS `event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `event` (
  `EventId` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Title` varchar(100) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `EventDate` datetime NOT NULL,
  `Location` varchar(200) DEFAULT NULL,
  `TicketPrice` decimal(10,2) DEFAULT '0.00',
  `MaxGuests` int NOT NULL,
  `CurrentGuests` int DEFAULT '0',
  PRIMARY KEY (`EventId`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `event_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
INSERT INTO `event` VALUES (3,3,'The Last of Us Special Night','Behind the scenes discussion and watch party.','2026-10-20 19:30:00','NDK Hall 1, Sofia',75.00,200,2),(4,13,'Stunt & Martial Arts Masterclass','Learn basic choreography, stunt work and self-defense techniques.','2026-09-10 14:00:00','Sofia Sports Hall',120.00,50,1),(5,13,'Dog Charity Walk & Meetup','Bring your dogs for a fun walk in the park! All funds go to animal shelters.','2026-09-18 10:00:00','South Park, Sofia',0.00,300,1),(6,6,'Tech & AI Innovation Summit','Keynote speech on the future of robotics and smart technology.','2026-11-05 09:30:00','Inter Expo Center, Sofia',150.00,500,1),(7,6,'Charity Winter Gala Dinner','An elegant evening with live jazz music, fine dining and fundraising.','2026-12-01 19:00:00','Grand Hotel Millennium, Sofia',250.00,100,1),(8,19,'Custom PC Building & Gaming Expo','Showcase customized PC rigs, benchmark hardware and play PC games.','2026-10-12 11:00:00','Burgas Expo Center',35.00,80,1),(9,19,'Fantasy Book & Lore Convention','Panel discussion about epic fantasy literature, worldbuilding and swords.','2026-11-20 15:00:00','NDK Hall 3, Sofia',45.00,250,1),(10,8,'Independent Cinema Workshop','Acting and directing masterclass for young aspiring filmmakers.','2026-10-02 16:00:00','Ancient Theatre, Plovdiv',60.00,120,1),(11,10,'Mindfulness & Yoga Mountain Retreat','A peaceful weekend yoga and wellness workshop in the mountains.','2026-09-25 08:00:00','Bansko Spa Resort',95.00,40,1);
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket`
--

DROP TABLE IF EXISTS `ticket`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket` (
  `TicketId` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `EventId` int NOT NULL,
  `PurchaseDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`TicketId`),
  KEY `UserId` (`UserId`),
  KEY `EventId` (`EventId`),
  CONSTRAINT `ticket_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `user` (`UserID`),
  CONSTRAINT `ticket_ibfk_2` FOREIGN KEY (`EventId`) REFERENCES `event` (`EventId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket`
--

LOCK TABLES `ticket` WRITE;
/*!40000 ALTER TABLE `ticket` DISABLE KEYS */;
INSERT INTO `ticket` VALUES (3,3,3,'2026-08-05 20:55:26'),(4,3,3,'2026-08-05 20:56:54'),(5,6,6,'2026-08-05 21:07:44'),(6,6,7,'2026-08-05 21:07:44'),(7,8,10,'2026-08-05 21:07:44'),(8,10,11,'2026-08-05 21:07:44'),(9,13,4,'2026-08-05 21:07:44'),(10,13,5,'2026-08-05 21:07:44'),(11,19,8,'2026-08-05 21:07:44'),(12,19,9,'2026-08-05 21:07:44');
/*!40000 ALTER TABLE `ticket` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Subscription` tinyint(1) NOT NULL DEFAULT '0',
  `FirstName` varchar(255) NOT NULL,
  `LastName` varchar(255) NOT NULL,
  `Age` int NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,1,'Chris','Evans',32,'Chris@gmail.com','Chris123'),(3,1,'Pedro','Pascal',54,'pepi@gmail.com','pedro123'),(4,0,'Peter','Parker',21,'spidey@gmail.com','SpiderMan32'),(5,0,'Chris','Peterson',23,'Chis11@gmail.com','123123123'),(6,1,'Robert','Downey',58,'rdj@gmail.com','IronMan123'),(7,0,'Tom','Holland',27,'tom.holland@gmail.com','Spider2024'),(8,1,'Scarlett','Johansson',39,'scarlett@gmail.com','Natasha1'),(9,0,'Mark','Ruffalo',56,'hulk@gmail.com','Smash99'),(10,1,'Elizabeth','Olsen',35,'wanda@gmail.com','HexMagic'),(11,0,'Benedict','Cumberbatch',47,'strange@gmail.com','TimeStone1'),(12,0,'Jenna','Ortega',21,'jenna@gmail.com','Wednesday1'),(13,1,'Keanu','Reeves',59,'johnwick@gmail.com','Pencil123'),(14,0,'Ryan','Reynolds',47,'deadpool@gmail.com','Chimichanga'),(15,1,'Margot','Robbie',33,'barbie@gmail.com','PinkWorld'),(16,0,'Cillian','Murphy',48,'cillian@gmail.com','Oppenheimer'),(17,1,'Ana','de Armas',35,'ana@gmail.com','Blonde321'),(18,0,'Zendaya','Coleman',27,'zendaya@gmail.com','Dune2026'),(19,1,'Henry','Cavill',40,'witcher@gmail.com','Geralt123'),(20,0,'Florence','Pugh',28,'florence@gmail.com','Yelena456');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-08-09 10:58:57

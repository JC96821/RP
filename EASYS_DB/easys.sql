CREATE DATABASE  IF NOT EXISTS `easys` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `easys`;
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: easys
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `m_class`
--

DROP TABLE IF EXISTS `m_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_class` (
  `class_id` int NOT NULL AUTO_INCREMENT,
  `class_cd` varchar(20) NOT NULL,
  `class_nm` varchar(50) NOT NULL,
  `profession_cd` varchar(20) NOT NULL,
  `user_cd` varchar(20) NOT NULL,
  PRIMARY KEY (`class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_class`
--

LOCK TABLES `m_class` WRITE;
/*!40000 ALTER TABLE `m_class` DISABLE KEYS */;
INSERT INTO `m_class` VALUES (1,'C001','自动化一班','P002','tech01'),(2,'C002','自动化二班','P002','tech02');
/*!40000 ALTER TABLE `m_class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_college`
--

DROP TABLE IF EXISTS `m_college`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_college` (
  `college_id` int NOT NULL AUTO_INCREMENT,
  `college_cd` varchar(20) NOT NULL,
  `college_nm` varchar(50) NOT NULL,
  PRIMARY KEY (`college_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_college`
--

LOCK TABLES `m_college` WRITE;
/*!40000 ALTER TABLE `m_college` DISABLE KEYS */;
INSERT INTO `m_college` VALUES (1,'C001','信息工程学院');
/*!40000 ALTER TABLE `m_college` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_curriculum`
--

DROP TABLE IF EXISTS `m_curriculum`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_curriculum` (
  `curriculum_id` int NOT NULL AUTO_INCREMENT,
  `curriculum_cd` varchar(20) NOT NULL,
  `curriculum_nm` varchar(50) NOT NULL,
  `user_cd` varchar(20) DEFAULT NULL,
  `term` int NOT NULL,
  `monday` time DEFAULT NULL,
  `tuesday` time DEFAULT NULL,
  `wednesday` time DEFAULT NULL,
  `thursday` time DEFAULT NULL,
  `friday` time DEFAULT NULL,
  `saturday` time DEFAULT NULL,
  `sunday` time DEFAULT NULL,
  `login_dt` datetime DEFAULT NULL,
  `login_user` varchar(20) DEFAULT NULL,
  `upd_dt` datetime DEFAULT NULL,
  `upd_user` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`curriculum_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_curriculum`
--

LOCK TABLES `m_curriculum` WRITE;
/*!40000 ALTER TABLE `m_curriculum` DISABLE KEYS */;
INSERT INTO `m_curriculum` VALUES (1,'CU001','PLC单片机','tech01',0,'09:00:00','00:00:00','09:00:00','00:00:00','13:30:00','00:00:00','00:00:00','2020-01-31 14:26:53','admin','2020-01-31 14:26:53','admin'),(2,'CU002','PLC单片机(实验)','tech01',0,'00:00:00','00:00:00','13:30:00','00:00:00','00:00:00','00:00:00','00:00:00','2020-01-31 14:27:46','admin','2020-01-31 14:27:54','tech01'),(3,'CU003','计算机概论','tech02',0,'08:30:00','08:30:00','08:30:00','08:30:00','08:30:00','00:00:00','00:00:00','2020-01-31 14:28:23','admin','2020-01-31 14:28:23','admin'),(4,'CU004','Java基础(上机)','tech02',0,'14:00:00','00:00:00','00:00:00','09:30:00','00:00:00','00:00:00','00:00:00','2020-01-31 14:29:05','admin','2020-01-31 14:29:05','admin');
/*!40000 ALTER TABLE `m_curriculum` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_profession`
--

DROP TABLE IF EXISTS `m_profession`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_profession` (
  `profession_id` int NOT NULL AUTO_INCREMENT,
  `profession_cd` varchar(20) NOT NULL,
  `profession_nm` varchar(50) NOT NULL,
  `college_cd` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`profession_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_profession`
--

LOCK TABLES `m_profession` WRITE;
/*!40000 ALTER TABLE `m_profession` DISABLE KEYS */;
INSERT INTO `m_profession` VALUES (1,'P001','信息通讯','C001'),(2,'P002','机械自动化','C001');
/*!40000 ALTER TABLE `m_profession` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_user`
--

DROP TABLE IF EXISTS `m_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_user` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `user_cd` varchar(20) NOT NULL,
  `user_nm` varchar(50) NOT NULL,
  `distinction` varchar(1) NOT NULL,
  `password` varchar(20) NOT NULL,
  `tell_phone` varchar(11) NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `img` longblob,
  `login_dt` datetime DEFAULT NULL,
  `login_user` varchar(20) DEFAULT NULL,
  `upd_dt` datetime DEFAULT NULL,
  `upd_user` varchar(20) NOT NULL,
  `del_flg` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_user`
--

LOCK TABLES `m_user` WRITE;
/*!40000 ALTER TABLE `m_user` DISABLE KEYS */;
INSERT INTO `m_user` VALUES (1,'admin','管理员','9','admin1','13478707150','13478707150@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(2,'st01','张三','0','st123','13478707151','13478707151@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(3,'st02','李四','0','st123','13478707152','13478707152@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(4,'st03','王五','0','st123','13478707153','13478707153@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(5,'st04','刘六','0','st123','13478707154','13478707154@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(6,'st05','李七','0','st123','13478707155','13478707155@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(7,'tech01','张老师','1','tech123','13478707156','13478707156@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(8,'tech02','李老师','1','tech123','13478707157','13478707157@163.com',NULL,'2020-01-31 00:00:00','admin','2020-02-01 00:00:00','admin','0'),(15,'cc','dd','1','dd','dd','dd',NULL,'2020-01-31 15:55:54','cc','2020-01-31 15:55:54','cc','1');
/*!40000 ALTER TABLE `m_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_user_class`
--

DROP TABLE IF EXISTS `m_user_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_user_class` (
  `user_class_id` int NOT NULL AUTO_INCREMENT,
  `user_cd` varchar(20) NOT NULL,
  `class_cd` varchar(20) NOT NULL,
  PRIMARY KEY (`user_class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_user_class`
--

LOCK TABLES `m_user_class` WRITE;
/*!40000 ALTER TABLE `m_user_class` DISABLE KEYS */;
INSERT INTO `m_user_class` VALUES (1,'tech01','C001'),(2,'tech02','C002'),(4,'st02','C001'),(5,'st03','C002'),(6,'st04','C002'),(7,'st05','C002'),(9,'st01','C001');
/*!40000 ALTER TABLE `m_user_class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `m_user_curriculum`
--

DROP TABLE IF EXISTS `m_user_curriculum`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `m_user_curriculum` (
  `user_curriculum_id` int NOT NULL AUTO_INCREMENT,
  `user_cd` varchar(20) NOT NULL,
  `curriculum_cd` varchar(20) NOT NULL,
  `score` int DEFAULT NULL,
  `evaluation_teacher` int DEFAULT NULL,
  `evaluation_students` int DEFAULT NULL,
  PRIMARY KEY (`user_curriculum_id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `m_user_curriculum`
--

LOCK TABLES `m_user_curriculum` WRITE;
/*!40000 ALTER TABLE `m_user_curriculum` DISABLE KEYS */;
INSERT INTO `m_user_curriculum` VALUES (1,'st01','CU001',61,3,2),(2,'st02','CU001',59,0,1),(3,'st03','CU001',0,0,4),(4,'st01','CU002',100,3,0),(5,'st02','CU002',0,0,0),(6,'st03','CU002',0,0,0),(9,'st01','CU003',80,5,4),(10,'st04','CU003',98,0,4),(11,'st05','CU003',78,0,5),(12,'st01','CU004',88,5,3),(13,'st04','CU004',89,0,5),(14,'st05','CU004',97,0,4),(17,'st02','CU004',0,0,0),(18,'st02','CU003',0,0,0);
/*!40000 ALTER TABLE `m_user_curriculum` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-02-15 20:50:55

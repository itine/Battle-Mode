/*
SQLyog Job Agent v12.3.2 (64 bit) Copyright(c) Webyog Inc. All Rights Reserved.


MySQL - 5.7.13-log : Database - workgame
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`workgame` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `workgame`;

/*Table structure for table `units` */

DROP TABLE IF EXISTS `units`;

CREATE TABLE `units` (
  `idUnit` int(11) NOT NULL DEFAULT '0',
  `attack` int(11) NOT NULL,
  `hp` int(11) NOT NULL,
  `defence` int(11) NOT NULL,
  `speed` int(11) NOT NULL,
  `unitName` varchar(50) NOT NULL,
  `currentLeftBox` int(11) DEFAULT NULL,
  `currentRightBox` int(11) DEFAULT NULL,
  `monstersRemainingOnLeft` double DEFAULT NULL,
  `monstersRemainingOnRight` double DEFAULT NULL,
  `classOfMonster` varchar(20) DEFAULT NULL,
  `monsterSize` int(11) DEFAULT NULL,
  `minDamage` int(11) DEFAULT NULL,
  `maxDamage` int(11) DEFAULT NULL,
  PRIMARY KEY (`idUnit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `units` */

insert  into `units` values 
(1,5,10,3,5,'Гоблин налетчик',1,NULL,-165.12000000000003,-20.75,'attacker',1,2,4),
(2,5,18,4,5,'Орк',NULL,NULL,-0.08333333333333333,0.5,'shooter',1,5,7),
(3,11,27,7,6,'Шаман',2,NULL,-20.370370370370377,-0.20833333333333334,'attacker',2,8,11),
(4,12,50,16,6,'Тролль',NULL,5,-0.10259259259259182,-2.5756903225806513,'attacker',2,8,12),
(5,20,60,14,14,'Циклоп',NULL,NULL,-0.013999999999999986,-0.3858,'shooter',2,15,20),
(6,24,60,17,17,'Смертоносный',NULL,NULL,-1.1800000000000002,-0.0022222222222222933,'flyer',3,24,30),
(7,26,200,40,11,'Гора',NULL,NULL,-0.05416666666666662,-0.06685805422647506,'attacker',3,21,28),
(8,3,11,4,5,'Копейщик',NULL,1,0.42250000000000226,-21.400000000000002,'attacker',1,3,3),
(9,5,16,5,6,'Арбалетчик',NULL,2,NULL,18.985795454545453,'shooter',1,4,6),
(10,9,21,11,8,'Несущий свет',NULL,4,NULL,-138.26571428571427,'flyer',2,7,10),
(11,14,45,14,7,'Клирик',NULL,NULL,NULL,-11.560075737126605,'attacker',2,8,12),
(12,16,75,18,9,'Инквизитор',NULL,NULL,NULL,-3.6077884687565156,'shooter',2,15,20),
(13,20,110,24,12,'Кавалерист',NULL,NULL,NULL,0.5517977283520127,'attacker',3,21,27),
(14,25,165,30,15,'Королевский палач',NULL,NULL,NULL,0.8620502645502645,'attacker',3,32,39),
(15,3,11,3,4,'Хоббит',1,NULL,-4.4,NULL,'shooter',1,2,4),
(16,6,14,4,7,'Фея',2,NULL,-2.6000000000000005,NULL,'flyer',1,4,6),
(17,8,30,12,4,'Голем',NULL,NULL,4.1066666666666665,10,'attacker',2,6,9),
(18,12,40,12,8,'Мастер магии',5,7,-0.09125000000000014,7.834857142857141,'shooter',2,12,16),
(19,15,70,17,11,'Джин',NULL,NULL,4.290000000000001,-10.091136363636366,'flyer',2,16,21),
(20,19,90,23,9,'Облачный странник',NULL,NULL,2,2.0260000000000002,'attacker',3,23,29),
(21,30,170,20,14,'Ледяной маг',NULL,NULL,NULL,NULL,'flyer',3,36,43),
(22,4,6,2,6,'Скелет',NULL,NULL,NULL,NULL,'attacker',1,3,4),
(23,6,20,5,2,'Ходячий',NULL,NULL,10,10,'attacker',1,3,5),
(24,12,24,9,7,'Дементр',NULL,NULL,10,-24.125,'flyer',2,6,8),
(25,13,35,12,9,'Вампир',3,2,-44.87975155279503,-34.92849584278156,'flyer',2,11,15),
(26,18,80,15,8,'Королева проклятий',NULL,3,-1.8079999999999996,-11.604166666666675,'attacker',2,16,21),
(27,22,80,18,10,'Лич',NULL,6,-2.2849999999999993,52.08843939393938,'shooter',3,25,31),
(28,22,180,32,13,'Ядовитый дракон',NULL,7,NULL,2.0555555555555554,'flyer',3,33,40),
(29,2,8,4,7,'Вор',NULL,NULL,NULL,-5.06460298594686,'attacker',1,3,5),
(30,5,12,6,9,'Кочевник',NULL,NULL,10,20,'attacker',1,3,6),
(31,10,18,9,9,'Смертельная игла',NULL,NULL,NULL,0.5333333333333333,'shooter',2,8,11),
(32,14,32,13,10,'Призрачная тень',NULL,NULL,7,4.1,'flyer',2,9,13),
(33,22,65,13,12,'Черный ниндзя',NULL,NULL,2.488,0.5333333333333333,'attacker',2,14,19),
(34,20,100,23,8,'Ифрит',4,NULL,-6.887812415288692,NULL,'flyer',3,22,29),
(35,30,190,30,12,'Ликан убийца',NULL,NULL,4.961037501084455,NULL,'shooter',3,28,35),
(999,-1,-1,-1,-1,'-1',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

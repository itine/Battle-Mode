
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
  `averageDamage` int(11) NOT NULL,
  PRIMARY KEY (`idUnit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `units` */

insert  into `units` values 
(1,5,10,3,5,'Гоблин налетчик',1,7,-0.4,-6.920000000000019,'attacker',1,2,4,5),
(2,5,18,4,5,'Орк',2,NULL,55.51999999999999,-0.16666666666666666,'shooter',1,5,7,5),
(3,11,27,7,6,'Шаман',NULL,NULL,-3.4955555555555553,-0.9199999999999996,'attacker',2,8,11,6),
(4,12,50,16,6,'Тролль',NULL,NULL,-0.7509090909090912,-3.7279999999999998,'attacker',2,8,12,6),
(5,20,60,14,14,'Циклоп',NULL,NULL,42.76275862068965,-0.6149999999999998,'shooter',2,15,20,14),
(6,24,60,17,17,'Смертоносный',NULL,NULL,100,-5.074966666666667,'flyer',3,24,30,17),
(7,26,200,40,11,'Гора',NULL,NULL,7.774105263157893,3.1524000000000005,'attacker',3,21,28,11),
(8,3,11,4,5,'Копейщик',7,1,-37.89090909090908,10,'attacker',1,3,3,3),
(9,5,16,5,6,'Арбалетчик',NULL,NULL,NULL,-22.175000000000008,'shooter',1,4,6,5),
(10,9,21,11,8,'Несущий свет',NULL,4,NULL,10,'flyer',2,7,10,8),
(11,14,45,14,7,'Клирик',NULL,NULL,8.531400966183575,19.05777777777778,'attacker',2,8,12,10),
(12,16,75,18,9,'Инквизитор',NULL,5,0.11730189810189642,10,'shooter',2,15,20,17),
(13,20,110,24,12,'Кавалерист',NULL,6,NULL,10,'attacker',3,21,27,24),
(14,25,165,30,15,'Королевский палач',7,NULL,8.780058651026392,0.3939393939393939,'attacker',3,32,39,35),
(15,3,11,3,4,'Хоббит',NULL,NULL,-0.12745454545454554,NULL,'shooter',1,2,4,4),
(16,6,14,4,7,'Фея',2,6,-9.428571428571429,-34.17934285714285,'flyer',1,4,6,5),
(17,8,30,12,4,'Голем',3,7,-1.2,-6.194470445688833,'attacker',2,6,9,8),
(18,12,40,12,8,'Мастер магии',NULL,5,100,5.32,'shooter',2,12,16,14),
(19,15,70,17,11,'Джин',NULL,6,6.121220238095238,7.634285714285715,'flyer',2,16,21,19),
(20,19,90,23,9,'Облачный странник',NULL,7,10,1.8722222222222222,'attacker',3,23,29,26),
(21,30,170,20,14,'Ледяной маг',NULL,NULL,10,4.500000000000001,'flyer',3,36,43,40),
(22,4,6,2,6,'Скелет',NULL,5,NULL,-71.72000000000003,'attacker',1,3,4,6),
(23,6,20,5,2,'Ходячий',5,4,-1.720119047619039,-22.57045,'attacker',1,3,5,2),
(24,12,24,9,7,'Дементр',3,3,5.304166666666667,4.633333333333334,'flyer',2,6,8,7),
(25,13,35,12,9,'Вампир',4,4,5.679999999999999,5.680000000000001,'flyer',2,11,15,9),
(26,18,80,15,8,'Королева проклятий',5,NULL,7.565217391304347,9.861739130434781,'attacker',2,16,21,8),
(27,22,80,18,10,'Лич',NULL,NULL,-6.2292000000000005,4.390480000000001,'shooter',3,25,31,10),
(28,22,180,32,13,'Ядовитый дракон',NULL,NULL,2.9226262626262622,9.43605791962175,'flyer',3,33,40,13),
(29,2,8,4,7,'Вор',NULL,2,NULL,-18.812750000000015,'attacker',1,3,5,7),
(30,5,12,6,9,'Кочевник',6,2,-20.100000000000005,6.666666666666667,'attacker',1,3,6,9),
(31,10,18,9,9,'Смертельная игла',NULL,NULL,-16.799999999999997,0.24799999999999991,'shooter',2,8,11,9),
(32,14,32,13,10,'Призрачная тень',NULL,NULL,1,-1.0017302489177502,'flyer',2,9,13,10),
(33,22,65,13,12,'Черный ниндзя',NULL,NULL,1.6478523076923073,-4.119200000000001,'attacker',2,14,19,12),
(34,20,100,23,8,'Ифрит',6,NULL,7.557142857142857,1.8814599999999997,'flyer',3,22,29,8),
(35,30,190,30,12,'Ликан убийца',NULL,NULL,3.026315789473684,11,'shooter',3,28,35,12),
(999,-1,-1,-1,-1,'-1',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0);
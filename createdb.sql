CREATE DATABASE `chess` /*!40100 DEFAULT CHARACTER SET utf8 */;
CREATE TABLE `games` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `playerWhite` varchar(255) DEFAULT NULL,
  `playerBlack` varchar(255) DEFAULT NULL,
  `timeCreateGame` datetime NOT NULL,
  `timeStartGame` datetime DEFAULT NULL,
  `act` int(11) NOT NULL,
  `turn` int(11) NOT NULL,
  `eatedWhites` varchar(16) NOT NULL,
  `eatedBlacks` varchar(16) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_playerWhite` (`playerWhite`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `moves` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `game` int(11) NOT NULL,
  `whiteMove` bit(1) NOT NULL,
  `from` varchar(2) NOT NULL,
  `to` varchar(2) NOT NULL,
  `inWhom` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `IX_FK_game1` (`game`),
  KEY `IX_FK_player` (`whiteMove`),
  CONSTRAINT `FK_game1` FOREIGN KEY (`game`) REFERENCES `games` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `users` (
  `name` varchar(30) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

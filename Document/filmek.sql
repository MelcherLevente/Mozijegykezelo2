-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Feb 02. 12:06
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `mozi`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `filmek`
--

CREATE TABLE `filmek` (
  `id` int(11) NOT NULL,
  `filmcim` varchar(255) NOT NULL,
  `filmkategoria` varchar(100) DEFAULT NULL,
  `rendezo` varchar(150) DEFAULT NULL,
  `megjelenesi_ev` int(11) DEFAULT NULL,
  `hossz` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `filmek`
--

INSERT INTO `filmek` (`id`, `filmcim`, `filmkategoria`, `rendezo`, `megjelenesi_ev`, `hossz`) VALUES
(1, 'A remény rabjai', 'Dráma', 'Frank Darabont', 1994, 142),
(2, 'Keresztapa', 'Krimi', 'Francis Ford Coppola', 1972, 175),
(3, 'Keresztapa II', 'Krimi', 'Francis Ford Coppola', 1974, 202),
(4, 'Sötét lovag', 'Akció', 'Christopher Nolan', 2008, 152),
(5, '12 dühös ember', 'Dráma', 'Sidney Lumet', 1957, 96),
(6, 'Schindler listája', 'Történelmi', 'Steven Spielberg', 1993, 195),
(7, 'Gyűrűk Ura: A Gyűrű Szövetsége', 'Fantasy', 'Peter Jackson', 2001, 178),
(8, 'Gyűrűk Ura: A két torony', 'Fantasy', 'Peter Jackson', 2002, 179),
(9, 'Gyűrűk Ura: A király visszatér', 'Fantasy', 'Peter Jackson', 2003, 201),
(10, 'Forrest Gump', 'Dráma', 'Robert Zemeckis', 1994, 142),
(11, 'Eredet', 'Sci-fi', 'Christopher Nolan', 2010, 148),
(12, 'Harcosok klubja', 'Dráma', 'David Fincher', 1999, 139),
(13, 'Mátrix', 'Sci-fi', 'Wachowski testvérek', 1999, 136),
(14, 'Hetedik', 'Thriller', 'David Fincher', 1995, 127),
(15, 'Interstellar', 'Sci-fi', 'Christopher Nolan', 2014, 169),
(16, 'Zöld mérföld', 'Dráma', 'Frank Darabont', 1999, 189),
(17, 'Gladiátor', 'Történelmi', 'Ridley Scott', 2000, 155),
(18, 'Titanic', 'Romantikus', 'James Cameron', 1997, 195),
(19, 'Avatar', 'Sci-fi', 'James Cameron', 2009, 162),
(20, 'Ponyvaregény', 'Krimi', 'Quentin Tarantino', 1994, 154),
(21, 'Django elszabadul', 'Western', 'Quentin Tarantino', 2012, 165),
(22, 'Becstelen brigantyk', 'Háborús', 'Quentin Tarantino', 2009, 153),
(23, 'Alkonyattól pirkadatig', 'Horror', 'Robert Rodriguez', 1996, 108),
(24, 'A nagy Lebowski', 'Vígjáték', 'Joel Coen', 1998, 117),
(25, 'Szárnyas fejvadász', 'Sci-fi', 'Ridley Scott', 1982, 117),
(26, 'Mad Max: A harag útja', 'Akció', 'George Miller', 2015, 120),
(27, 'Joker', 'Dráma', 'Todd Phillips', 2019, 122),
(28, 'Viharsziget', 'Thriller', 'Martin Scorsese', 2010, 138),
(29, 'A Wall Street farkasa', 'Dráma', 'Martin Scorsese', 2013, 180),
(30, 'Nagymenők', 'Krimi', 'Martin Scorsese', 1990, 146),
(31, 'Casino', 'Krimi', 'Martin Scorsese', 1995, 178),
(32, 'Donnie Darko', 'Sci-fi', 'Richard Kelly', 2001, 113),
(33, 'Az oroszlánkirály', 'Animáció', 'Roger Allers', 1994, 88),
(34, 'Toy Story', 'Animáció', 'John Lasseter', 1995, 81),
(35, 'Fel!', 'Animáció', 'Pete Docter', 2009, 96),
(36, 'Coco', 'Animáció', 'Lee Unkrich', 2017, 105),
(37, 'Shrek', 'Animáció', 'Andrew Adamson', 2001, 90),
(38, 'Jégkorszak', 'Animáció', 'Chris Wedge', 2002, 81),
(39, 'Verdák', 'Animáció', 'John Lasseter', 2006, 117),
(40, 'Így neveld a sárkányodat', 'Animáció', 'Dean DeBlois', 2010, 98),
(41, 'Pókember', 'Akció', 'Sam Raimi', 2002, 121),
(42, 'Pókember 2', 'Akció', 'Sam Raimi', 2004, 127);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `filmek`
--
ALTER TABLE `filmek`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `filmek`
--
ALTER TABLE `filmek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

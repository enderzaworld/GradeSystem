-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 25, 2017 at 12:34 PM
-- Server version: 5.7.14
-- PHP Version: 7.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `gradingsystem`
--

-- --------------------------------------------------------

--
-- Table structure for table `perf_task`
--

CREATE TABLE `perf_task` (
  `task_ID` int(11) NOT NULL,
  `perf_ID` int(11) NOT NULL,
  `perf_total_items` int(11) NOT NULL,
  `subject_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `quar_assess`
--

CREATE TABLE `quar_assess` (
  `assess_ID` int(11) NOT NULL,
  `quarterly_ID` int(11) NOT NULL,
  `assess_total_items` int(11) NOT NULL,
  `subject_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `student_perf`
--

CREATE TABLE `student_perf` (
  `perf_ID` int(11) NOT NULL,
  `student_ID` int(11) NOT NULL,
  `PTS1` int(11) NOT NULL,
  `PTS2` int(11) NOT NULL,
  `PTS3` int(11) NOT NULL,
  `PTS4` int(11) NOT NULL,
  `PTS5` int(11) NOT NULL,
  `PTS6` int(11) NOT NULL,
  `PTS7` int(11) NOT NULL,
  `PTS8` int(11) NOT NULL,
  `quarter_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `student_profile`
--

CREATE TABLE `student_profile` (
  `student_ID` int(11) NOT NULL,
  `student_FirstName` varchar(200) NOT NULL,
  `student_MI` varchar(2) NOT NULL,
  `student_LastName` varchar(20) NOT NULL,
  `student_Sex` varchar(45) NOT NULL,
  `student_Level` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student_profile`
--

INSERT INTO `student_profile` (`student_ID`, `student_FirstName`, `student_MI`, `student_LastName`, `student_Sex`, `student_Level`) VALUES
(1, 'Zy', 'S.', 'Gozar', 'Male', 'Grade 8');

-- --------------------------------------------------------

--
-- Table structure for table `student_qa`
--

CREATE TABLE `student_qa` (
  `quarterly_ID` int(11) NOT NULL,
  `student_ID` int(11) NOT NULL,
  `quarterly_score` int(11) NOT NULL,
  `quarter_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `student_subjgrade`
--

CREATE TABLE `student_subjgrade` (
  `subjgrade_ID` int(11) NOT NULL,
  `student_ID` int(11) NOT NULL,
  `word_ID` int(11) NOT NULL,
  `task_ID` int(11) NOT NULL,
  `assess_ID` int(11) NOT NULL,
  `initial_grade` int(11) NOT NULL,
  `quarterly_grade` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `student_ww`
--

CREATE TABLE `student_ww` (
  `written_ID` int(11) NOT NULL,
  `student_ID` int(11) NOT NULL,
  `WWS1` int(11) DEFAULT NULL,
  `WWS2` int(11) DEFAULT NULL,
  `WWS3` int(11) DEFAULT NULL,
  `WWS4` int(11) DEFAULT NULL,
  `WWS5` int(11) DEFAULT NULL,
  `WWS6` int(11) DEFAULT NULL,
  `WWS7` int(11) DEFAULT NULL,
  `WWS8` int(11) DEFAULT NULL,
  `quarter_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student_ww`
--

INSERT INTO `student_ww` (`written_ID`, `student_ID`, `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, `quarter_ID`) VALUES
(1, 1, 20, 15, 15, 10, 15, 20, 15, 25, 0);

-- --------------------------------------------------------

--
-- Table structure for table `teacher`
--

CREATE TABLE `teacher` (
  `User_ID` int(11) NOT NULL,
  `Teacher_FirstName` varchar(45) NOT NULL,
  `Teacher_LastName` varchar(45) NOT NULL,
  `Teacher_Sex` varchar(45) NOT NULL,
  `Teacher_Position` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `teacher`
--

INSERT INTO `teacher` (`User_ID`, `Teacher_FirstName`, `Teacher_LastName`, `Teacher_Sex`, `Teacher_Position`) VALUES
(1, 'Zyrus', 'Gozar', 'Male', 'Admin');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `User_ID` int(11) NOT NULL,
  `User_Username` varchar(45) NOT NULL,
  `User_Password` varchar(45) NOT NULL,
  `User_Status` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`User_ID`, `User_Username`, `User_Password`, `User_Status`) VALUES
(1, 'zyrus', '12345', 'Active'),
(2, 'admin', '12345', 'Inactive');

-- --------------------------------------------------------

--
-- Table structure for table `written_work`
--

CREATE TABLE `written_work` (
  `work_ID` int(11) NOT NULL,
  `written_ID` int(11) NOT NULL,
  `written_total_items` int(11) DEFAULT NULL,
  `subject_ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `written_work`
--

INSERT INTO `written_work` (`work_ID`, `written_ID`, `written_total_items`, `subject_ID`) VALUES
(1, 1, 135, 1),
(2, 3, NULL, NULL),
(3, 4, NULL, NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `perf_task`
--
ALTER TABLE `perf_task`
  ADD PRIMARY KEY (`task_ID`);

--
-- Indexes for table `quar_assess`
--
ALTER TABLE `quar_assess`
  ADD PRIMARY KEY (`assess_ID`);

--
-- Indexes for table `student_perf`
--
ALTER TABLE `student_perf`
  ADD PRIMARY KEY (`perf_ID`);

--
-- Indexes for table `student_profile`
--
ALTER TABLE `student_profile`
  ADD PRIMARY KEY (`student_ID`),
  ADD UNIQUE KEY `Student_ID_UNIQUE` (`student_ID`);

--
-- Indexes for table `student_qa`
--
ALTER TABLE `student_qa`
  ADD PRIMARY KEY (`quarterly_ID`);

--
-- Indexes for table `student_ww`
--
ALTER TABLE `student_ww`
  ADD PRIMARY KEY (`written_ID`);

--
-- Indexes for table `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`User_ID`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`User_ID`);

--
-- Indexes for table `written_work`
--
ALTER TABLE `written_work`
  ADD PRIMARY KEY (`work_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `perf_task`
--
ALTER TABLE `perf_task`
  MODIFY `task_ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `student_profile`
--
ALTER TABLE `student_profile`
  MODIFY `student_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `student_ww`
--
ALTER TABLE `student_ww`
  MODIFY `written_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `teacher`
--
ALTER TABLE `teacher`
  MODIFY `User_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `User_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `written_work`
--
ALTER TABLE `written_work`
  MODIFY `work_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

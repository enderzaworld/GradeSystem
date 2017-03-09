-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Mar 09, 2017 at 12:59 AM
-- Server version: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `gradingsystem`
--

-- --------------------------------------------------------

--
-- Table structure for table `perf_task`
--

CREATE TABLE IF NOT EXISTS `perf_task` (
  `task_ID` int(11) NOT NULL AUTO_INCREMENT,
  `perf_ID` int(11) NOT NULL,
  `perf_total_items` int(11) NOT NULL,
  `subject_ID` int(11) NOT NULL,
  PRIMARY KEY (`task_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `quar_assess`
--

CREATE TABLE IF NOT EXISTS `quar_assess` (
  `assess_ID` int(11) NOT NULL,
  `quarterly_ID` int(11) NOT NULL,
  `assess_total_items` int(11) NOT NULL,
  `subject_ID` int(11) NOT NULL,
  PRIMARY KEY (`assess_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `student_all_subject_grade`
--

CREATE TABLE IF NOT EXISTS `student_all_subject_grade` (
  `student_all_subject_grade_ID` int(11) NOT NULL AUTO_INCREMENT,
  `FILIPINO` int(11) NOT NULL,
  `ENGLISH` int(11) NOT NULL,
  `MATH` int(11) NOT NULL,
  `SCIENCE` int(11) NOT NULL,
  `AP` int(11) NOT NULL,
  `VALUES` int(11) NOT NULL,
  `MAPEH` int(11) NOT NULL,
  `TLE` int(11) NOT NULL,
  `student_id` int(11) NOT NULL,
  PRIMARY KEY (`student_all_subject_grade_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `student_all_subject_grade`
--

INSERT INTO `student_all_subject_grade` (`student_all_subject_grade_ID`, `FILIPINO`, `ENGLISH`, `MATH`, `SCIENCE`, `AP`, `VALUES`, `MAPEH`, `TLE`, `student_id`) VALUES
(1, 80, 85, 90, 89, 88, 87, 86, 85, 1),
(2, 89, 88, 85, 87, 86, 85, 84, 83, 2);

-- --------------------------------------------------------

--
-- Table structure for table `student_finalgrade`
--

CREATE TABLE IF NOT EXISTS `student_finalgrade` (
  `student_FinalGrade_ID` int(11) NOT NULL AUTO_INCREMENT,
  `1st_Grading` int(11) NOT NULL,
  `2nd_Grading` int(11) NOT NULL,
  `3rd_Grading` int(11) NOT NULL,
  `4th_Grading` int(11) NOT NULL,
  `student_ID` int(11) NOT NULL,
  `subject` varchar(255) NOT NULL,
  PRIMARY KEY (`student_FinalGrade_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `student_finalgrade`
--

INSERT INTO `student_finalgrade` (`student_FinalGrade_ID`, `1st_Grading`, `2nd_Grading`, `3rd_Grading`, `4th_Grading`, `student_ID`, `subject`) VALUES
(1, 90, 90, 90, 90, 1, 'MATH'),
(2, 90, 90, 80, 80, 2, 'MATH');

-- --------------------------------------------------------

--
-- Table structure for table `student_perf`
--

CREATE TABLE IF NOT EXISTS `student_perf` (
  `perf_ID` int(11) NOT NULL AUTO_INCREMENT,
  `student_ID` int(11) NOT NULL,
  `PTS1` int(11) NOT NULL,
  `PTS2` int(11) NOT NULL,
  `PTS3` int(11) NOT NULL,
  `PTS4` int(11) NOT NULL,
  `PTS5` int(11) NOT NULL,
  `PTS6` int(11) NOT NULL,
  `PTS7` int(11) NOT NULL,
  `PTS8` int(11) NOT NULL,
  `subject` varchar(255) NOT NULL,
  `quarter_ID` int(11) NOT NULL,
  PRIMARY KEY (`perf_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=13 ;

--
-- Dumping data for table `student_perf`
--

INSERT INTO `student_perf` (`perf_ID`, `student_ID`, `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, `subject`, `quarter_ID`) VALUES
(11, 1, 20, 15, 15, 10, 15, 20, 15, 25, 'Math', 1),
(12, 1, 0, 0, 0, 0, 0, 0, 0, 150, 'Math', 1);

-- --------------------------------------------------------

--
-- Table structure for table `student_profile`
--

CREATE TABLE IF NOT EXISTS `student_profile` (
  `student_ID` int(11) NOT NULL AUTO_INCREMENT,
  `student_FirstName` varchar(200) NOT NULL,
  `student_MI` varchar(2) NOT NULL,
  `student_LastName` varchar(20) NOT NULL,
  `student_Sex` varchar(45) NOT NULL,
  `student_Level` varchar(45) NOT NULL,
  `student_Section` varchar(20) NOT NULL,
  PRIMARY KEY (`student_ID`),
  UNIQUE KEY `Student_ID_UNIQUE` (`student_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `student_profile`
--

INSERT INTO `student_profile` (`student_ID`, `student_FirstName`, `student_MI`, `student_LastName`, `student_Sex`, `student_Level`, `student_Section`) VALUES
(1, 'Zy', 'S.', 'Gozar', 'Male', 'Grade 8', 'Section 1'),
(2, 'student', 'm', 'none', 'Female', 'Grade 8', 'Section 1'),
(3, 'SI', 'M', 'Fe', 'Female', 'Grade 8', 'Section 1');

-- --------------------------------------------------------

--
-- Table structure for table `student_qa`
--

CREATE TABLE IF NOT EXISTS `student_qa` (
  `quarterly_ID` int(11) NOT NULL,
  `student_ID` int(11) NOT NULL,
  `quarterly_score` int(11) NOT NULL,
  `quarter_ID` int(11) NOT NULL,
  `subject` varchar(255) NOT NULL,
  `student_qa_id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`student_qa_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `student_qa`
--

INSERT INTO `student_qa` (`quarterly_ID`, `student_ID`, `quarterly_score`, `quarter_ID`, `subject`, `student_qa_id`) VALUES
(0, 1, 0, 1, 'Math', 5),
(0, 1, 1, 1, 'Math', 6);

-- --------------------------------------------------------

--
-- Table structure for table `student_subjgrade`
--

CREATE TABLE IF NOT EXISTS `student_subjgrade` (
  `subjgrade_ID` int(11) NOT NULL AUTO_INCREMENT,
  `student_ID` int(11) NOT NULL,
  `word_ID` int(11) NOT NULL,
  `task_ID` int(11) NOT NULL,
  `assess_ID` int(11) NOT NULL,
  `initial_grade` float NOT NULL,
  `quarterly_grade` float NOT NULL,
  `quarter_ID` int(11) NOT NULL,
  `subject` varchar(255) NOT NULL,
  PRIMARY KEY (`subjgrade_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Dumping data for table `student_subjgrade`
--

INSERT INTO `student_subjgrade` (`subjgrade_ID`, `student_ID`, `word_ID`, `task_ID`, `assess_ID`, `initial_grade`, `quarterly_grade`, `quarter_ID`, `subject`) VALUES
(1, 1, 1, 1, 1, 84.27, 90, 1, 'Math'),
(2, 1, 1, 1, 1, 84.27, 90, 2, 'Math'),
(3, 1, 1, 1, 1, 84.27, 90, 3, 'Math'),
(4, 1, 1, 1, 1, 84.27, 90, 4, 'Math'),
(5, 2, 1, 1, 1, 84.27, 90, 1, 'Math'),
(6, 2, 1, 1, 1, 84.27, 90, 2, 'Math'),
(7, 2, 1, 1, 1, 84.27, 80, 3, 'Math'),
(8, 2, 1, 1, 1, 84.27, 80, 4, 'Math');

-- --------------------------------------------------------

--
-- Table structure for table `student_ww`
--

CREATE TABLE IF NOT EXISTS `student_ww` (
  `written_ID` int(11) NOT NULL AUTO_INCREMENT,
  `student_ID` int(11) NOT NULL,
  `WWS1` int(11) DEFAULT NULL,
  `WWS2` int(11) DEFAULT NULL,
  `WWS3` int(11) DEFAULT NULL,
  `WWS4` int(11) DEFAULT NULL,
  `WWS5` int(11) DEFAULT NULL,
  `WWS6` int(11) DEFAULT NULL,
  `WWS7` int(11) DEFAULT NULL,
  `WWS8` int(11) DEFAULT NULL,
  `subject` varchar(255) NOT NULL,
  `quarter_ID` int(11) NOT NULL,
  PRIMARY KEY (`written_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=20 ;

--
-- Dumping data for table `student_ww`
--

INSERT INTO `student_ww` (`written_ID`, `student_ID`, `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, `subject`, `quarter_ID`) VALUES
(2, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', 0),
(3, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', 0),
(4, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '', 0),
(18, 1, 20, 15, 15, 10, 15, 20, 15, 25, 'Math', 1),
(19, 1, 0, 0, 0, 0, 0, 0, 0, 150, 'Math', 1);

-- --------------------------------------------------------

--
-- Table structure for table `teacher`
--

CREATE TABLE IF NOT EXISTS `teacher` (
  `User_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Teacher_FirstName` varchar(45) NOT NULL,
  `Teacher_LastName` varchar(45) NOT NULL,
  `Teacher_Sex` varchar(45) NOT NULL,
  `Teacher_Position` varchar(45) NOT NULL,
  PRIMARY KEY (`User_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `teacher`
--

INSERT INTO `teacher` (`User_ID`, `Teacher_FirstName`, `Teacher_LastName`, `Teacher_Sex`, `Teacher_Position`) VALUES
(1, 'Zyrus', 'Gozar', 'Male', 'Admin'),
(3, 'Dean', 'Lean', 'Male', 'Teacher');

-- --------------------------------------------------------

--
-- Table structure for table `teacher_schedule`
--

CREATE TABLE IF NOT EXISTS `teacher_schedule` (
  `teacher_schedule_id` int(11) NOT NULL AUTO_INCREMENT,
  `User_ID` int(11) NOT NULL,
  `7_subject` varchar(10) NOT NULL,
  `7_GradeLevel` varchar(10) NOT NULL,
  `7_Section` varchar(10) NOT NULL,
  `8_subject` varchar(10) NOT NULL,
  `8_GradeLevel` varchar(10) NOT NULL,
  `8_Section` varchar(10) NOT NULL,
  `9_subject` varchar(10) NOT NULL,
  `9_GradeLevel` varchar(10) NOT NULL,
  `9_Section` varchar(10) NOT NULL,
  `10_subject` varchar(10) NOT NULL,
  `10_GradeLevel` varchar(10) NOT NULL,
  `10_Section` varchar(10) NOT NULL,
  `12_subject` varchar(10) NOT NULL,
  `12_GradeLevel` varchar(10) NOT NULL,
  `12_Section` varchar(10) NOT NULL,
  `1_subject` varchar(10) NOT NULL,
  `1_GradeLevel` varchar(10) NOT NULL,
  `1_Section` varchar(10) NOT NULL,
  `2_subject` varchar(10) NOT NULL,
  `2_GradeLevel` varchar(10) NOT NULL,
  `2_Section` varchar(10) NOT NULL,
  `advisory_class` varchar(10) NOT NULL,
  PRIMARY KEY (`teacher_schedule_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `teacher_schedule`
--

INSERT INTO `teacher_schedule` (`teacher_schedule_id`, `User_ID`, `7_subject`, `7_GradeLevel`, `7_Section`, `8_subject`, `8_GradeLevel`, `8_Section`, `9_subject`, `9_GradeLevel`, `9_Section`, `10_subject`, `10_GradeLevel`, `10_Section`, `12_subject`, `12_GradeLevel`, `12_Section`, `1_subject`, `1_GradeLevel`, `1_Section`, `2_subject`, `2_GradeLevel`, `2_Section`, `advisory_class`) VALUES
(3, 1, 'M.A.P.E.H', 'Grade 1', 'Section 1', 'T.L.E', 'Grade 2', 'Section 1', 'S.S.', 'Grade 6', 'Section 1', 'Filipino', 'Grade 5', 'Section 1', 'Science', 'Grade 4', 'Section 1', 'English', 'Grade 3', 'Section 1', 'Math', 'Grade 8', 'Section 1', 'Grade 8');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `User_ID` int(11) NOT NULL AUTO_INCREMENT,
  `User_Username` varchar(45) NOT NULL,
  `User_Password` varchar(45) NOT NULL,
  `User_Status` varchar(45) NOT NULL,
  PRIMARY KEY (`User_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`User_ID`, `User_Username`, `User_Password`, `User_Status`) VALUES
(1, 'zyrus', '12345', 'Active'),
(2, 'admin', '12345', 'Inactive'),
(3, 'dean', '12345', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `written_work`
--

CREATE TABLE IF NOT EXISTS `written_work` (
  `work_ID` int(11) NOT NULL AUTO_INCREMENT,
  `written_ID` int(11) NOT NULL,
  `written_total_items` int(11) DEFAULT NULL,
  `subject_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`work_ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `written_work`
--

INSERT INTO `written_work` (`work_ID`, `written_ID`, `written_total_items`, `subject_ID`) VALUES
(1, 1, 135, 1),
(2, 3, NULL, NULL),
(3, 4, NULL, NULL),
(4, 2, NULL, NULL),
(5, 3, NULL, NULL),
(6, 4, NULL, NULL);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe_Code_Test.Tests
{
    [TestClass()]
    public class TicTacToeBoard_Tests
    {
        [TestMethod()]
        public void PlayMove_ValidMove_ReturnsTrue()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);

            // Act
            var result = TestBoard.PlayMove(TicTacToeBoard.Player.Player_O, 1, 1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void PlayMove_InvalidMove_ReturnsFalse()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_O, 1, 1);

            // Act
            var result = TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 1, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayMove_OutOfBoundsX_ThrowsAssertion()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);

            // Act
            var result = TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 4, 1);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayMove_OutOfBoundsY_ThrowsAssertion()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);

            // Act
            var result = TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 1, 4);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        public void GetLongestLine_NoLine_Returns0()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);

            // Act
            var result = TestBoard.GetLongestLine(TicTacToeBoard.Player.Player_O, 1, 1, 5);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void GetLongestLine_LineExists_Returns3()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 0, 0);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 0, 1);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 0, 2);

            // Act
            var result = TestBoard.GetLongestLine(TicTacToeBoard.Player.Player_X, 0, 0, 6);

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLongestLine_OutOfBoardErrorX_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            var result = TestBoard.GetLongestLine(TicTacToeBoard.Player.Player_X, 4, 0, 5);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLongestLine_OutOfBoardErrorY_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            var result = TestBoard.GetLongestLine(TicTacToeBoard.Player.Player_X, 0, 4, 5);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLongestLine_BadDirection_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            var result = TestBoard.GetLongestLine(TicTacToeBoard.Player.Player_X, 0, 0, 8);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        public void PrintBoard_Default_DisplaysBoardToConsoleOut()
        {
            using (StringWriter sw = new StringWriter())
            {
                // Arrange
                var TestBoard = new TicTacToeBoard(3, 3, 3);
                Console.SetOut(sw);

                // Act
                TestBoard.PrintBoard();

                // Assert
                string expected = string.Format(
                    "-------{0}| | | |{0}-------{0}| | | |{0}-------{0}| | | |{0}-------{0}",
                    Environment.NewLine);

                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod()]
        public void PrintBoard_DefaultwithXandO_DisplaysBoardToConsoleOut()
        {
            using (StringWriter sw = new StringWriter())
            {
                // Arrange
                var TestBoard = new TicTacToeBoard(3, 3, 3);
                TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 0, 0);
                TestBoard.PlayMove(TicTacToeBoard.Player.Player_O, 0, 1);
                Console.SetOut(sw);

                // Act
                TestBoard.PrintBoard();

                // Assert
                string expected = string.Format(
                    "-------{0}|X| | |{0}-------{0}|O| | |{0}-------{0}| | | |{0}-------{0}",
                    Environment.NewLine);

                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod()]
        public void CheckWinFrom_NoWinner_ReturnsFalse()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(2, 2, 2);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 0, 0);

            // Act
            var result = TestBoard.CheckWinFrom(TicTacToeBoard.Player.Player_X, 0, 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckWinFrom_WinnerExists_ReturnsTrue()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 0, 0);

            // Act
            var result = TestBoard.CheckWinFrom(TicTacToeBoard.Player.Player_X, 0, 0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckWinFrom_OutOfBoardErrorX_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            var result = TestBoard.CheckWinFrom(TicTacToeBoard.Player.Player_X, 4, 0);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckWinFrom_OutOfBoardErrorY_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            var result = TestBoard.CheckWinFrom(TicTacToeBoard.Player.Player_X, 0, 4);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        public void ResetSquare_MakeInvalidMoveValid_ReturnsTrue()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(3, 3, 3);
            TestBoard.PlayMove(TicTacToeBoard.Player.Player_O, 1, 1);

            // Act
            TestBoard.ResetSquare(1, 1);
            var result = TestBoard.PlayMove(TicTacToeBoard.Player.Player_X, 1, 1);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ResetSquare_OutOfBoardErrorX_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            TestBoard.ResetSquare(4, 0);

            // Assert -- exception was thrown
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ResetSquare_OutOfBoardErrorY_ThrowsException()
        {
            // Arrange
            var TestBoard = new TicTacToeBoard(1, 1, 1);

            // Act
            TestBoard.ResetSquare(0, 4);

            // Assert -- exception was thrown
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe_Code_Test.Tests
{
    [TestClass()]
    public class TicTacToeGridSquare_Tests
    {
        [TestMethod()]
        public void Constructor_Valid_SetToSpaceCharacter()
        {
            // Arrange

            // Act
            var TestSquare = new TicTacToeGridSquare();

            // Assert
            Assert.AreEqual(' ', TestSquare.CurrentMark);
        }

        [TestMethod()]
        public void SetCurrentMark_SetToX_ReturnsXMark()
        {
            // Arrange
            var TestSquare = new TicTacToeGridSquare();

            // Act
            TestSquare.CurrentMark = 'X';

            // Assert
            Assert.AreEqual('X', TestSquare.CurrentMark);
        }

        [TestMethod()]
        public void SetCurrentMark_SetToO_ReturnsXMark()
        {
            // Arrange
            var TestSquare = new TicTacToeGridSquare();

            // Act
            TestSquare.CurrentMark = 'O';

            // Assert
            Assert.AreEqual('O', TestSquare.CurrentMark);
        }

        [TestMethod()]
        public void SetCurrentMark_SetToXAndBackToSpace_ReturnsSpace()
        {
            // Arrange
            var TestSquare = new TicTacToeGridSquare();
            TestSquare.CurrentMark = 'X';

            // Act
            TestSquare.CurrentMark = ' ';

            // Assert
            Assert.AreEqual(' ', TestSquare.CurrentMark);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCurrentMark_SetToInvalidType_ThrowsException()
        {
            // Arrange
            var TestSquare = new TicTacToeGridSquare();

            // Act
            TestSquare.CurrentMark = 'd';

            // Assert -- exception was thrown
        }
    }
}

namespace Tests.Matrix
{
    using Crosscutting.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Matrix;

    [TestClass]
    public class MatrixRotatorRightTest
    {
        [TestMethod]
        public void TestEmpty()
        {
            var arr = new int[,] { { } };
            var expectedArr = new int[,] { { } };

            this.CheckRightRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationRightOneRow()
        {
            var arr = new[,] { { 3 } };
            var expectedArr = new[,] { { 3 } };

            this.CheckRightRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationRightTwoRows()
        {
            var arr = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };
            var expectedArr = new[,]
            {
                { 3, 1 },
                { 4, 2 }
            };

            this.CheckRightRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationRightThreeRows()
        {
            var arr = new[,]
            {
                { 1, 2, 3 }, 
                { 4, 5, 6 }, 
                { 7, 8, 9}
            };
            var expectedArr = new[,]
            {
                { 7, 4, 1 },
                { 8, 5, 2 },
                { 9, 6, 3}
            };

            this.CheckRightRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationRightFourRows()
        {
            var arr = new[,]
            {
                {  1,  2,  3,  4 }, 
                {  5,  6,  7,  8 }, 
                {  9, 10, 11, 12 },
                { 13, 14, 15, 16 }
            };
            var expectedArr = new[,]
            {
                { 13,  9,  5,  1 },
                { 14, 10,  6,  2 },
                { 15, 11,  7,  3 },
                { 16, 12,  8,  4 }
            };

            this.CheckRightRotation(arr, expectedArr);
        }

        private void CheckRightRotation(int[,] arr, int[,] expectedArr)
        {
            var service = MatrixRotatorService.InitAndGetInstance(arr);
            var result = service.RorateLeft();

            Assert.IsTrue(result.IsEqual(expectedArr));
        }
    }
}
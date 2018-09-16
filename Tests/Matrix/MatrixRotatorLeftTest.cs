namespace Tests.Matrix
{
    using Crosscutting.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Matrix;

    [TestClass]
    public class MatrixRotatorLeftTest
    {
        [TestMethod]
        public void TestEmpty()
        {
            var arr = new int[,]{{}};
            var expectedArr = new int[,]{{}};

            this.CheckLeftRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationLeftOneRow()
        {
            var arr = new[,] { { 3 } };
            var expectedArr = new[,] { { 3 } };

            this.CheckLeftRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationLefttTwoRows()
        {
            var arr = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };
            var expectedArr = new[,]
            {
                { 2, 4 },
                { 1, 3 }
            };

            this.CheckLeftRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationLeftThreeRows()
        {
            var arr = new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9}
            };
            var expectedArr = new[,]
            {
                { 3, 6, 9 },
                { 2, 5, 8 },
                { 1, 4, 7}
            };

            this.CheckLeftRotation(arr, expectedArr);
        }

        [TestMethod]
        public void RotationLeftFourRows()
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
                {  4,  8, 12, 16 },
                {  3,  7, 11, 15 },
                {  2,  6, 10, 14 },
                {  1,  5,  9, 13 }
            };

            this.CheckLeftRotation(arr, expectedArr);
        }

        private void CheckLeftRotation(int[,] arr, int[,] expectedArr)
        {
            var service = MatrixRotatorService.InitAndGetInstance(arr);
            var result = service.RorateLeft();

            Assert.IsTrue(result.IsEqual(expectedArr));
        }
    }
}
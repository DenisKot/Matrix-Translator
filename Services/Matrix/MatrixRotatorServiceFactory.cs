namespace Services.Matrix
{
    internal class MatrixRotatorServiceFactory : IMatrixRotatorServiceFactory
    {
        public IMatrixRotatorService GetService(int[,] arr)
        {
            return MatrixRotatorService.InitAndGetInstance(arr);
        }
    }
}
namespace Services.Matrix
{
    public interface IMatrixRotatorServiceFactory
    {
        IMatrixRotatorService GetService(int[,] arr);
    }
}
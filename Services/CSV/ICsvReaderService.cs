namespace Services.CSV
{
    using System.IO;

    public interface ICsvReaderService
    {
        int[,] ReadFile(Stream stream);
    }
}
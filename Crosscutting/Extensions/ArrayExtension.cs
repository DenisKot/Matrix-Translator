namespace Crosscutting.Extensions
{
    using System.Linq;

    public static class ArrayExtension
    {
        public static bool IsEqual(this int[,] firstArr, int[,] secondArr)
        {
            return firstArr.Rank == secondArr.Rank 
                   && Enumerable.Range(0, firstArr.Rank).All(dimension => firstArr.GetLength(dimension) == secondArr.GetLength(dimension))
                   && firstArr.Cast<int>().SequenceEqual(secondArr.Cast<int>());
        }
    }
}
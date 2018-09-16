namespace Services.Matrix
{
    public class MatrixRotatorService : IMatrixRotatorService
    {
        public static IMatrixRotatorService InitAndGetInstance(int[,] arr)
        {
            return new MatrixRotatorService(arr);
        }

        private readonly int[,] arr;
        private readonly int rows;
        private readonly int columns;

        private MatrixRotatorService(int[,] arr)
        {
            this.arr = arr;
            this.rows = this.arr.GetLength(0);
            this.columns = this.arr.GetLength(1);
        }

        public int[,] RorateRight()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = i + 1; j < this.columns; j++)
                {
                    this.Swap(i, j, j, i);
                }
            }

            this.SwapHorisontaly();

            return this.arr;
        }
        public int[,] RorateLeft()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns - 1 - i; j++)
                {
                    this.Swap(i, j, this.rows - 1 - j, this.columns - 1 - i);
                }
            }

            this.SwapHorisontaly();

            return this.arr;
        }

        private void SwapHorisontaly()
        {
            var halfOfColumns = this.columns / 2;

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < halfOfColumns; j++)
                {
                    this.Swap(i, j, i, this.columns - 1 - j);
                }
            }
        }

        private void Swap(int row1, int col1, int row2, int col2)
        {
            var temp = this.arr[row1, col1];
            this.arr[row1, col1] = this.arr[row2, col2];
            this.arr[row2, col2] = temp;
        }
    }
}
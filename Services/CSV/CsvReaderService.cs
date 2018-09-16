namespace Services.CSV
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Crosscutting.Exceptions;

    public class CsvReaderService : ICsvReaderService
    {
        public int[,] ReadFile(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var list = new List<string[]>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    list.Add(values);
                }

                this.CheckIfValid(list);
                return this.ParseArray(list);
            }
        }

        private void CheckIfValid(List<string[]> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var row = list[i];
                if (row.Length != list.Count)
                {
                    throw new ValidationException($"Wrong length of row index: {i}");
                }

                for (var col = 0; col < row.Length; col++)
                {
                    var item = row[col];
                    int ignoreMe;
                    bool successfullyParsed = int.TryParse(item, out ignoreMe);
                    if (!successfullyParsed)
                    {
                        throw new ValidationException($"Parse exception, wrong element on row index: {i} and column index: {col}");
                    }
                }
            }
        }

        private int[,] ParseArray(List<string[]> list)
        {
            var arr = new int[list.Count, list.Count];

            for (var rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                var row = list[rowIndex];

                for (var colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    arr[rowIndex, colIndex] = Convert.ToInt32(row[colIndex]);
                }
            }

            return arr;
        }
    }
}
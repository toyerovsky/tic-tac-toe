namespace Assets.Scripts
{
    public static class StringExtensions
    {
        public static void FillWithNumbers(this string[,] matrix)
        {
            int k = 0;
            // We fill matrix instead of creating new one because in second case GC has to Dispose temp matrix what may be expensive
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = k++.ToString();
                }
            }
        }
    }
}
namespace TFG.CodeExtensions
{
    public static class Extensions
    {
        public static int IsCompliant<T>(this IList<T> list)
        {
            var searchItem = Convert.ToString(list.FirstOrDefault());
            bool continueCounting = true;
            int index = 0;
            while (continueCounting)
            {
                if (Convert.ToString(list[index]) == searchItem)
                    index++;
                else
                    continueCounting = false;
            }
            if ((index) > (list.Count / 2))
                return 1;
            else
                return -1;
        }
    }
}

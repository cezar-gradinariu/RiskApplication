namespace Domain.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string candidate)
        {
            int result;
            if (!int.TryParse(candidate, out result))
            {
                return null;
            }
            return result;
        }
    }
}
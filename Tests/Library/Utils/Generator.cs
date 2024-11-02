namespace Library.Utils
{
    public class Functions
    {
        public static string GenerateRandomNumber(int length)
        {
            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)('0' + random.Next(10));
            }

            return new string(chars);
        }
    }
}
namespace Assessment_1.Utils
{
    public static class OtpGenerator
    {
        private static readonly Random _random = new Random();

        public static string GenerateOtp()
        {
            return _random.Next(1000, 10000).ToString();
        }
    }
}

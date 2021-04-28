namespace Nano35.Identity.Api.Helpers
{
    public static class PhoneConverter
    {
        public static string RuPhoneConverter(string currentPhone)// Comes like +7(800)555-35-35
        {
            var result = currentPhone
                .Replace("+", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", ""); // 78005553535
            if(result.Length == 11)
                result = result.Substring(1); // 8005553535
            return result; 
        }
    }
    
}
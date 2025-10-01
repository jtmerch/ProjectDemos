using System.Reflection;
using System.Text;

namespace NuveiPayBlazorDemo.Shared.Utility
{
    public static class ObjectToString
    {
        public static string ConvObjectToString(object obj)
        {
            var sb = new StringBuilder();
            if (obj != null)
            {
                var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in properties)
                {
                    // Skip properties that are indexed or sensitive properties.
                    if (prop.GetIndexParameters().Length == 0 &&
                       prop.Name.ToLower() != "cvv" && prop.Name != "ssl_cvv2cvc2")
                    {
                        try
                        {

                            if ((prop.Name.ToLower() == "cardnumber" || prop.Name == "ssl_card_number" || prop.Name.ToLower() == "ccnumber") &&
                                prop.GetValue(obj) is string cardNumber && cardNumber.Length > 4)
                            {
                                string maskedCardNumber = new string('*', cardNumber.Length - 4) + cardNumber[^4..];
                                sb.AppendLine($"{prop.Name}: {maskedCardNumber}");
                            }
                            else
                            {
                                sb.AppendLine($"{prop.Name}: {prop.GetValue(obj)}");

                        }

                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"{prop.Name}: [Error retrieving value: {ex.Message}]");
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}

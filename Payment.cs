using System;
using System.Text.RegularExpressions;

namespace PaymentManager
{
    public enum Payment
    {
        MasterCard,
        Visa,
        IDeal
    }

    public static class PaymentExtension
    {
        private static Regex MasterCardVisaRegex = new Regex(@"^((\d{4} ){3}\d{4})$", RegexOptions.Compiled);
        private static Regex IDealRegex = new Regex(@"^([A-Z]{2}\d{2} [A-Z]{4} (\d{4} ){2}\d{2})$", RegexOptions.Compiled);
        // Master/Visa Card: 0123 4567 8901 2345
        // IDeal: AB12 ABCD 0123 4567 89

        public static bool IsValid(this Payment payment, string paymentInfo)
        {
            if (payment == Payment.MasterCard || payment == Payment.Visa)
                return MasterCardVisaRegex.IsMatch(paymentInfo);
            else // IDeal
                return IDealRegex.IsMatch(paymentInfo);
        }
    }
}


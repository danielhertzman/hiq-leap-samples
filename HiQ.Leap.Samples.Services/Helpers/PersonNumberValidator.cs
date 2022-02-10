namespace HiQ.Leap.Samples.Services.Helpers;

public static class PersonNumberValidator
{
    public static bool IsValidPersonNumber(string personNumber)
    {
        if (string.IsNullOrWhiteSpace(personNumber))
        {
            return false;
        }

        if (personNumber.Contains("-"))
        {
            personNumber = personNumber.Replace("-", string.Empty);
        }

        // formattera bort första siffrorna
        if (personNumber.Length >= 10)
        {
            personNumber = personNumber.Remove(0, personNumber.Length - 10);
        }

        if (!IsValidLuhn(personNumber))
        {
            return false;
        }

        return true;
    }

    internal static bool IsValidLuhn(string digits)
    {
        if (string.IsNullOrWhiteSpace(digits))
        {
            return false;
        }

        return digits.All(char.IsDigit) && digits.Reverse()
            .Select(c => c - 48)
            .Select((thisNum, i) => i % 2 == 0
                ? thisNum
                : (thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
            .Sum() % 10 == 0;
    }
}
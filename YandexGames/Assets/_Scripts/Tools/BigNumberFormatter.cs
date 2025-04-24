namespace _Scripts.Tools
{
    public class BigNumberFormatter
    {
        public static string FormatBigNumber(double number)
        {
            if (number < 1000)
                return number.ToString("0");

            int alphabetIndex = 0;
            while (number >= 1000)
            {
                number /= 1000;
                alphabetIndex++;
            }

            string suffix = GetSuffix(alphabetIndex);
            return number.ToString("0.#") + suffix;
        }

        private static string GetSuffix(int index)
        {
            if (index <= 0)
                return "";

            index--;

            string suffix = "";
            while (index >= 0)
            {
                suffix = (char)('a' + (index % 26)) + suffix;
                index = index / 26 - 1;
            }
            return suffix;
        }

    }
}
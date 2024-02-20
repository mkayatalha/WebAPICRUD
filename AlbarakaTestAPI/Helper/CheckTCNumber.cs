namespace AlbarakaTestAPI.Helper
{
    public class CheckTCNumber
    {
        public static bool ValidateTCNumber(string tcNumber)
        {

            if (tcNumber.Length != 11)
            {
                return false;
            }

            for (int i = 0; i < 11; i++)
            {
                if (!char.IsDigit(tcNumber[i]))
                {
                    return false;
                }
            }

            if (tcNumber[0] == '0')
            {
                return false;
            }

            int[] tcNumberArray = new int[11];
            for (int i = 0; i < 11; i++)
            {
                tcNumberArray[i] = int.Parse(tcNumber[i].ToString());
            }
            int sumOddDigits = tcNumberArray[0] + tcNumberArray[2] + tcNumberArray[4] + tcNumberArray[6] + tcNumberArray[8];
            int sumEvenDigits = tcNumberArray[1] + tcNumberArray[3] + tcNumberArray[5] + tcNumberArray[7];
            int control10Digit = (7*sumOddDigits - sumEvenDigits) % 10;
            if (control10Digit != tcNumberArray[9])
            {
                return false;
            }

            for (int i = 0; i < 11; i++)
            {
                tcNumberArray[i] = int.Parse(tcNumber[i].ToString());
            }
            int sumFirstTenDigits = tcNumberArray[0] + tcNumberArray[1] + tcNumberArray[2] + tcNumberArray[3] + tcNumberArray[4] + tcNumberArray[5] + tcNumberArray[6] + tcNumberArray[7] + tcNumberArray[8] + tcNumberArray[9];
            int controlDigit = (sumFirstTenDigits % 10);
            if (controlDigit != tcNumberArray[10])
            {
                return false;
            }

            return true;
        }
    }
}

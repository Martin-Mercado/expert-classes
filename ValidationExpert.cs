  public static class ValidationExpert
  {
      public static bool IsEAN13(string code)
      {
          if (string.IsNullOrWhiteSpace(code) || code.Length != 13 || !code.All(char.IsDigit))
              return false;

          int sum = 0;

          for (int i = 0; i < 12; i++)
          {
              int digit = code[i] - '0';
              sum += (i % 2 == 0) ? digit : digit * 3;
          }

          int checkDigit = (10 - (sum % 10)) % 10;
          int lastDigit = code[12] - '0';

          return checkDigit == lastDigit;
      }
  }

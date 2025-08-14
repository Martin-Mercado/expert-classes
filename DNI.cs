 public class DNI
 {
     public string NroTramite {  get; set; }
     public string FirstName {  get; set; }
     public string LastName { get; set; }
     public char Sex { get; set; }
     public string Number { get; set; }
     public string Ejemplar {  get; set; }
     public DateTime  BirthDate { get; set; }
     public DateTime IssueDate { get; set; }
     public string Cuit {  get; set; }


     public static bool TryParse(string input, out DNI dni)
     {
         dni = null;

         if (string.IsNullOrWhiteSpace(input))
             return false;

         string[] parts = input.Split('@');

         if (parts.Length != 9)
             return false;

         try
         {
             string nroTramite = parts[0];
             string lastName = parts[1];
             string firstName = parts[2];
             char sex = parts[3][0];
             string number = parts[4]; 
             string ejemplar = parts[5];
             DateTime birthDate = DateTime.ParseExact(parts[6], "dd/MM/yyyy", null);
             DateTime issueDate = DateTime.ParseExact(parts[7], "dd/MM/yyyy", null);
             string cuitPrefixAndSuffix = parts[8]; 

             if (cuitPrefixAndSuffix.Length != 3)
                 return false;

             string cuit = $"{cuitPrefixAndSuffix.Substring(0, 2)}{number}{cuitPrefixAndSuffix.Substring(2, 1)}";

             dni = new DNI
             {
                 NroTramite = nroTramite,
                 LastName = lastName,
                 FirstName = firstName,
                 Sex = sex,
                 Number = number,
                 Ejemplar = ejemplar,
                 BirthDate = birthDate,
                 IssueDate = issueDate,
                 Cuit = cuit
             };

             return true;
         }
         catch
         {
             dni = null;
             return false;
         }
     }

     public static bool IsValidDNI(string input)
     {
         return long.TryParse(input, out _) && (input.Length == 7 || input.Length == 8);
     }

     public static bool IsValidCUIT(string cuit)
     {
         if (cuit.Length != 11 || !long.TryParse(cuit, out _))
             return false;

         int[] multipliers = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
         int sum = 0;

         for (int i = 0; i < 10; i++)
         {
             sum += int.Parse(cuit[i].ToString()) * multipliers[i];
         }

         int mod11 = 11 - (sum % 11);
         int checkDigit = mod11 == 11 ? 0 : mod11 == 10 ? 9 : mod11;

         return checkDigit == int.Parse(cuit[10].ToString());
     }


 }

  public static class EncryptionExpert
  {
      // Clave AES 
      private static byte[] AesKey => Convert.FromBase64String("qweojqwe");

      // Vector de inicialización AES 
      private static byte[] AesIV => Convert.FromBase64String("12u123i");


      // Encripta texto plano usando AES en modo CBC con padding PKCS7
      public static string Encrypt(string plainText)
      {
          using (Aes aes = Aes.Create())
          {
              aes.Key = AesKey;                
              aes.IV = AesIV;                 
              aes.Mode = CipherMode.CBC;       
              aes.Padding = PaddingMode.PKCS7; 

              ICryptoTransform encryptor = aes.CreateEncryptor();
              byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); 
              byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length); 

              return Convert.ToBase64String(encrypted); 
          }
      }

      // Desencripta texto cifrado con AES
      // Devuelve el texto descifrado en UTF-8
      public static string Decrypt(string cipherText)
      {
          using (Aes aes = Aes.Create())
          {
              aes.Key = AesKey;                
              aes.IV = AesIV;                  
              aes.Mode = CipherMode.CBC;       
              aes.Padding = PaddingMode.PKCS7; 

              ICryptoTransform decryptor = aes.CreateDecryptor(); 
              byte[] cipherBytes = Convert.FromBase64String(cipherText); 
              byte[] decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length); 

              return Encoding.UTF8.GetString(decrypted); 
          }
      }
  }


    public static class AuthenticationExpert
    {
        public const string UserCookieName = "asdasd"; // Pooner cualquier nombre
    
        public static User CheckUserCookie(HttpRequest Request)
         {
             HttpCookie userAuthCookie = Request.Cookies[UserCookieName];
        
             if (userAuthCookie == null)
             {
                 Debug.WriteLine("Cookie no encontrada");
                 return null;
             }
        
             FormsAuthenticationTicket userAuthTicket = FormsAuthentication.Decrypt(userAuthCookie.Value);
             if (userAuthTicket == null)
             {
                 Debug.WriteLine("Cookie invalida");
                 return null;
             }
        
             string[] userData = userAuthTicket.UserData.Split('|');
             string username = userData[0];
             string password = userData[1];
        
             if (username.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
             {
                 Debug.WriteLine("Codigo de cliente en cookie invalido");
                 return null;
             }
        
        
        
             User user = DatabaseExpert.FetchUserByCredentials(username, password);
             if (user == null)
             {
                 Debug.WriteLine("Codigo de cliente en cookie invalido 2");
             }
        
        
             return user;
         }
      
         public static void ClearUserCookie( HttpResponse response)
         {
             HttpCookie cookie = new HttpCookie(UserCookieName);
             cookie.Expires = DateTime.Now.AddDays(-1);
             response.Cookies.Add(cookie);
         }


         public static void SaveUserCookie(string username, string password)
         {
        
             var userAuthTicket = new FormsAuthenticationTicket(
                 1,                    
                 username,             
                 DateTime.Now,             
                 DateTime.Now.AddDays(7),  // Se puede poner cualquier duracion   
                 true,                    
                 $"{username}|{password}"
             );
        
             string encryptedUserTicket = FormsAuthentication.Encrypt(userAuthTicket);
        
             HttpCookie userAuthCookie = new HttpCookie(AuthenticationExpert.UserCookieName, encryptedUserTicket)
             {
                 Expires = userAuthTicket.Expiration
             };
        
             Response.Cookies.Add(userAuthCookie);
        
         }
    }

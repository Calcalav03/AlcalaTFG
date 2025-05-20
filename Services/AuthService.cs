using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcalaTFG.Services
{
    public class AuthService
    {
        // Singleton - instancia única del servicio
        private static AuthService _instance;
        public static AuthService Instance => _instance ??= new AuthService();

        // Propiedad para almacenar el UserLogin
        public string UserLogin { get; set; }

        // Propiedad para almacenar el Token de autenticación (si lo necesitas)
        public string AuthToken { get; set; }

        // Método para establecer el UserLogin y Token
        public void SetUserCredentials(string userLogin, string token)
        {
            UserLogin = userLogin;
            AuthToken = token;
        }

        // Método para limpiar las credenciales (Logout)
        public void ClearCredentials()
        {
            UserLogin = string.Empty;
            AuthToken = string.Empty;
        }

        // Verificar si el usuario está autenticado
        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(AuthToken);
        }
    }
}


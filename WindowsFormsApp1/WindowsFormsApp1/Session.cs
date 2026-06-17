using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    public static class Session
    {
        public static string UserLogin { get; set; }          // логин или null для гостя
        public static string UserFullName { get; set; }       // ФИО
        public static int? UserRoleId { get; set; }           // id роли (1,2,3)

        public static bool IsGuest => string.IsNullOrEmpty(UserLogin);
        public static bool IsAdmin => UserRoleId == 1;        // админ = 1
        public static bool IsManager => UserRoleId == 2;      // менеджер = 2
        public static bool IsClient => UserRoleId == 3;       // клиент = 3

        public static void Logout()
        {
            UserLogin = null;
            UserFullName = null;
            UserRoleId = null;
        }
    }
}
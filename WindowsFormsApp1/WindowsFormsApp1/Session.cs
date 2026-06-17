namespace WindowsFormsApp1
{
    public static class Session
    {
        public static string UserLogin { get; set; }
        public static string UserFullName { get; set; }
        public static int? UserRoleId { get; set; }
        public static string UserRoleName { get; set; }

        public static bool IsGuest => string.IsNullOrEmpty(UserLogin);
        public static bool IsAdmin => HasRole("администратор", "админ", "admin") || (string.IsNullOrWhiteSpace(UserRoleName) && UserRoleId == 1);
        public static bool IsManager => HasRole("менеджер", "manager") || (string.IsNullOrWhiteSpace(UserRoleName) && UserRoleId == 2);
        public static bool IsClient => !IsGuest && (HasRole("клиент", "client", "покупатель") || (string.IsNullOrWhiteSpace(UserRoleName) && UserRoleId == 3));

        private static bool HasRole(params string[] names)
        {
            string role = (UserRoleName ?? string.Empty).Trim().ToLower();
            foreach (string name in names)
            {
                if (role.Contains(name)) return true;
            }
            return false;
        }

        public static string DisplayName => IsGuest ? "Гость" : string.IsNullOrWhiteSpace(UserFullName) ? UserLogin : UserFullName;

        public static void Logout()
        {
            UserLogin = null;
            UserFullName = null;
            UserRoleId = null;
            UserRoleName = null;
        }
    }
}

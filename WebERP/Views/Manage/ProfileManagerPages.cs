namespace WebERP.Views.Manage
{
    public static class ProfileManagerPages
    {
        public static ProfilePages ActivePage { get; set; }
        public static string ChangePassword => IsPageActive(ProfilePages.ChangePassword);
        public static string Settings => IsPageActive(ProfilePages.Settings);
        private static string IsPageActive(ProfilePages page) => ActivePage == page ? "active" : "";
    }

    public enum ProfilePages
    {
        Settings,
        ChangePassword
    }
}

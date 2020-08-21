namespace EngineerProject.Mobile.Utility
{
    public static class ConfigurationData
    {
        public static bool IsUserLoggedIn { get; internal set; }

        //public static string Url => "http://192.168.115.103:8000/api/";
        public static string Token { get; internal set; }

        public const string Url = "http://192.168.0.52:8000/api/";

        public const string ChatUrl = "http://192.168.0.52:8000/chatHub";
    }
}
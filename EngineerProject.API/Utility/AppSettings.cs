namespace EngineerProject.API.Utility
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string SmtpServerAddress { get; set; }

        public string SmtpUserName { get; set; }

        public string SmtpUserPassword { get; set; }

        public int MaxFileSizeInMB { get; set; }

        public string FilesPath { get; set; }
    }
}
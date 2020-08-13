namespace EngineerProject.Mobile.Services
{
    public abstract class BaseService
    {
        protected readonly HttpService client;

        public BaseService()
        {
            client = new HttpService();
        }

    }
}
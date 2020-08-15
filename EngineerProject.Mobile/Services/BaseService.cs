namespace EngineerProject.Mobile.Services
{
    public abstract class BaseService
    {
        protected readonly HttpService client;

        public BaseService()
        {
            client = new HttpService();
        }

        protected DataRequestResponse<ResponseType> MapResponse<ResponseType>(ResponseType requestResult)
        {
            var result = new DataRequestResponse<ResponseType>();

            if (requestResult != null)
            {
                result.IsSuccessful = true;
                result.Data = requestResult;
            }
            else
                result.ErrorMessage = client.error?.Message;

            return result;
        }

        protected RequestResponse MapResponse(bool requestResult)
        {
            var result = new RequestResponse();

            if (requestResult)
                result.IsSuccessful = true;
            else
                result.ErrorMessage = client.error?.Message;

            return result;
        }
    }
}
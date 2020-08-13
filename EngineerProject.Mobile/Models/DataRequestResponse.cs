namespace EngineerProject.Mobile.Services
{
    public class DataRequestResponse<ResponseDataType> : RequestResponse
    {
        public ResponseDataType Data { get; set; }
    }
}
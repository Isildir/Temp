using EngineerProject.Commons.Dtos.Groups;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class FileService : BaseService
    {
        public async Task<DataRequestResponse<List<FileDto>>> GetFiles(int groupId, int page, int pageSize)
        {
            var result = new DataRequestResponse<List<FileDto>>();

            var response = await client.GetAsync<List<FileDto>>($"Files/GetFiles?groupId={groupId}&page={page}&pageSize={pageSize}&filter=''", new CancellationToken());

            if (response != null)
            {
                result.IsSuccessful = true;
                result.Data = response;
            }
            else
                result.ErrorMessage = client.error.Message;

            return result;
        }

        //TODO DownloadFile
    }
}
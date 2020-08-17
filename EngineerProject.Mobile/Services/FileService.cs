using EngineerProject.Commons.Dtos.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngineerProject.Mobile.Services
{
    public class FileService : BaseService
    {
        public async Task<DataRequestResponse<List<FileDto>>> GetFiles(int groupId, int page, int pageSize)
        {
            var response = await client.GetAsync<List<FileDto>>($"Files/GetFiles?groupId={groupId}&page={page}&pageSize={pageSize}&filter=''");

            return MapResponse(response);
        }

        //TODO DownloadFile
    }
}
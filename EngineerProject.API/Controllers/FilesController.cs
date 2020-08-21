using EngineerProject.API.Entities;
using EngineerProject.API.Entities.Models;
using EngineerProject.API.Utility;
using EngineerProject.Commons.Dtos.Groups;
using EngineerProject.Commons.Dtos.Querying;
using HeyRed.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBFile = EngineerProject.API.Entities.Models.File;

namespace EngineerProject.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[action]")]
    public class FilesController : Controller
    {
        private readonly EngineerContext context;
        private readonly AppSettings appSettings;

        public FilesController(EngineerContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
        }

        //TODO DownloadFile

        [HttpGet]
        public ActionResult<IEnumerable<FileDto>> GetFiles([FromQuery] GroupQueryDto query)
        {
            var userId = ClaimsReader.GetUserId(Request);
            var formattedFilter = string.IsNullOrEmpty(query.Filter) ? string.Empty : query.Filter.ToLower();

            var result = context.Set<DBFile>()
                .Include(a => a.User)
                .OrderByDescending(a => a.DateAdded)
                .Where(a => a.GroupId == query.GroupId)
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .Select(a => new FileDto
                {
                    Id = a.Id,
                    Owner = a.User.Login,
                    DateAdded = a.DateAdded,
                    FileName = a.FileName,
                    IsOwner = a.UserId == userId,
                    Size = a.Size
                }).ToList();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile([FromQuery] int id)
        {
            var record = await context.Set<DBFile>().FirstOrDefaultAsync(a => a.Id == id);

            if (record == null)
                return NotFound();

            var userId = ClaimsReader.GetUserId(Request);

            if (record.UserId != userId)
                return BadRequest();

            try
            {
                System.IO.File.Delete(record.FilePath);

                await context.SaveChangesAsync();

                context.Set<DBFile>().Remove(record);

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                Logger.Log($"{nameof(FilesController)} {nameof(DeleteFile)}", ex.Message, NLog.LogLevel.Error, ex);

                return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.Log($"{nameof(FilesController)} {nameof(DeleteFile)}", ex.Message, NLog.LogLevel.Error, ex);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromQuery] int groupId, IFormFile file)
        {
            var size = file?.Length;
            var sizeInMB = (size / 1024f) / 1024f;

            if (size <= 0 || sizeInMB > appSettings.MaxFileSizeInMB)
                return BadRequest("Niepoprawny format pliku");

            var fileExtension = MimeTypesMap.GetExtension(file.ContentType);

            var availableFileFormats = new List<string> { "docx", "pdf", "jpeg", "png" };

            if (!availableFileFormats.Any(a => a.Equals(fileExtension)))
                return BadRequest("Niepoprawny format pliku");

            var userId = ClaimsReader.GetUserId(Request);

            var groupRecord = await context.Set<Group>().Include(a => a.Users).FirstOrDefaultAsync(a => a.Id == groupId);
            var userRecord = await context.Set<User>().FindAsync(userId);

            if (groupRecord == null || userRecord == null)
                return NotFound();

            if (!groupRecord.Users.Any(a => a.UserId == userId))
                return BadRequest();

            if (!Directory.Exists(appSettings.FilesPath))
                Directory.CreateDirectory(appSettings.FilesPath);

            var filePath = Path.Combine(appSettings.FilesPath, Path.GetRandomFileName());

            try
            {
                using var stream = System.IO.File.Create(filePath);

                await file.CopyToAsync(stream);

                var dbFile = new DBFile
                {
                    DateAdded = DateTime.UtcNow,
                    FilePath = filePath,
                    FileName = file.FileName,
                    User = userRecord,
                    Group = groupRecord,
                    Size = GetFileSizeAsString(size.Value),
                    FileType = fileExtension.Equals(".pdf") || fileExtension.Equals(".docx") ? Entities.Models.FileType.Document : Entities.Models.FileType.Photo
                };

                context.Set<DBFile>().Add(dbFile);

                await context.SaveChangesAsync();

                return Ok(new FileDto
                {
                    Id = dbFile.Id,
                    Owner = dbFile.User.Login,
                    DateAdded = dbFile.DateAdded,
                    FileName = dbFile.FileName,
                    IsOwner = true
                });
            }
            catch (ArgumentException ex)
            {
                Logger.Log($"{nameof(FilesController)} {nameof(UploadFile)}", ex.Message, NLog.LogLevel.Error, ex);

                return BadRequest();
            }
            catch (DbUpdateException ex)
            {
                System.IO.File.Delete(filePath);

                Logger.Log($"{nameof(FilesController)} {nameof(UploadFile)}", ex.Message, NLog.LogLevel.Error, ex);

                return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.Log($"{nameof(FilesController)} {nameof(UploadFile)}", ex.Message, NLog.LogLevel.Error, ex);

                return BadRequest();
            }
        }

        private string GetFileSizeAsString(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };

            int order = 0;
            var floatingSize = (double)size;

            while (floatingSize >= 1024 && order < sizes.Length - 1)
            {
                order++;
                floatingSize /= 1024;
            }

            return string.Format("{0:0.##} {1}", floatingSize, sizes[order]);
        }
    }
}
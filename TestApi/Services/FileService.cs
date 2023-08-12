using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Services;

public class FileService : IFileService
{

    private readonly byte[] _Data;

    public FileService(byte[] Data)
    {
        _Data= Data;
    }

    public FileService()
    {
    }

    public async Task PostFileAsync(IFormFile fileData, FileType fileType)
    {
        try
        {
            var fileDetails = new FileDetails()
            {
                ID = 0,
                FileName = fileData.FileName,
                FileType = fileType,
                FileData = _Data
            };

            using (var stream = new MemoryStream())
            {
                fileData.CopyTo(stream);
                fileDetails.FileData = stream.ToArray();
            }

            //    var result = _Data.FileDetails.Add(fileDetails);
            //    await _Data.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task PostMultiFileAsync(List<FileUploadModel> fileData)
    {
        try
        {
            foreach (FileUploadModel file in fileData)
            {
                var fileDetails = new FileDetails()
                {
                    ID = 0,
                    FileName = file.FileDetails.FileName,
                    FileType = file.FileType,
                    FileData = _Data
                };

                using (var stream = new MemoryStream())
                {
                    file.FileDetails.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                //var result = _Data.Add(fileDetails);
            }
            //await _Data.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    //public async Task DownloadFileById(int Id)
    //{
    //    try
    //    {
    //        var file = _Data.Where(x => x.ID == Id).FirstOrDefaultAsync();

    //        var content = new MemoryStream(file.Result.FileData);
    //        var path = Path.Combine(
    //           Directory.GetCurrentDirectory(), "FileDownloaded",
    //           file.Result.FileName);

    //        await CopyStream(content, path);
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    public async Task CopyStream(Stream stream, string downloadPath)
    {
        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
        {
            await stream.CopyToAsync(fileStream);
        }
    }
}

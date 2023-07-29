namespace GardenWEPAppDB1.Helper
{
    public static class FileUpload
    {
        public static async Task<string> saveFileasync(this IFormFile file, string WeebRootPath)
        {
            var path = "/uploads/" + Guid.NewGuid() + file.FileName;
            using FileStream fileStream = new(WeebRootPath + path,  FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
    }
 }                                        
 
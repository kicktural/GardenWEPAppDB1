namespace GardenWEPAppDB1.HelperApple
{
    public static class FileUpload
    {
        public static async Task<string> saveFileasync(this IFormFile file, string webrootpath)
        {
            var path = "/uploadsApple/" + Guid.NewGuid() + file.FileName;   
            using FileStream fileStream = new(webrootpath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
    }
}

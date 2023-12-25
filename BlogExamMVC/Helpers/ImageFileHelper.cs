namespace BlogExamMVC.Helpers
{
    public static class ImageFileHelper
    {
        public static bool IsValidSize(this IFormFile file, int validSize) 
            => file.Length / 1000 <= validSize;
        public static bool IsImage(this IFormFile file) 
            => file.ContentType.Contains("image");

        public async static Task<string> SaveImageFileAsync(this IFormFile file, string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName); 
            var fileExtension = Path.GetExtension(file.FileName);
            fileName = Path.Combine(path, Path.GetRandomFileName() + fileName + fileExtension);
            using (FileStream fs = File.Create(Path.Combine(PathConstants.RootPath, fileName)))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }

    }
}

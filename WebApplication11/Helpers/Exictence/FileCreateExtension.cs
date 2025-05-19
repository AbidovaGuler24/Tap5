namespace WebApplication11.Helpers.Exictence
{
    public static class FileCreateExtension
    {
        public static string CreateFile(this IFormFile file, string wwwroot, string FolderName)
        {
            string filename = Guid.NewGuid() + file.FileName;
            if (file.FileName.Length > 64)
            {
                filename = Guid.NewGuid() + file.FileName.Substring(file.FileName.Length - 64);

            }
            else
            {
                filename = Guid.NewGuid() + file.FileName;
            }
            string path = Path.Combine(wwwroot, FolderName, filename);

            using (FileStream fileStream = new FileStream( path, FileMode.Create))
            {
                file.CopyTo(fileStream);

            }
            return filename;
        }
        public static void RemoveFile(this IFormFile file, string wwwroot, string FolderName)
        {
            string path = Path.Combine(wwwroot, FolderName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }
}

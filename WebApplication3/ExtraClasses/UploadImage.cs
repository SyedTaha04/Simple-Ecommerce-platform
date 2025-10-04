namespace WebApplication3.ExtraClasses;

public class UploadImage
{
   

    public List<Images> SaveImage(List<IFormFile> files)
    {
        long size = 5 * 1024 * 1024;
        foreach (IFormFile file in files)
        {
            if (file.Length > size)
            {
                throw new Exception("File size cannot exceed 5MB");

            }
            
        }
        var uploadedFilePaths = new List<string>();
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine("wwwroot/images", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            uploadedFilePaths.Add("/images/" + fileName);
        }
        List<Images> uploadedImages = uploadedFilePaths
            .Select(path => new Images { Url = path })
            .ToList();
        return uploadedImages;

    }

    
}
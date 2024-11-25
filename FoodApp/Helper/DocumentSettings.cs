namespace FoodApp.Helper;

public class DocumentSettings
{
    public async static Task<string> UploadFileAsync(IFormFile formFile, string FolderName)
    {
        string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

        string FileName = $"{Guid.NewGuid()}{".jpg"}";

        string FilePath = Path.Combine(FolderPath, FileName);

        //  Save File in server at Streams : [Data Per Time]
        using var FileStream = new FileStream(FilePath, FileMode.Create);
        await formFile.CopyToAsync(FileStream);

        return FileName;
    }
    public static void DeleteFile(string FileName, string FolderName)
    {
        if (FileName is not null && FolderName is not null)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
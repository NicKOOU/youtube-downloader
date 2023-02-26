namespace DLYoutube
{
    public class Storage
    {
        public async Task<bool> SaveFile(Stream stream, string title)
        {
            Console.WriteLine($"Saving {title}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Dbo");
            Console.WriteLine($"Path: {path}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = $"{title}.mp4";
            var filePath = Path.Combine(path, fileName);
            using (var fileStream = File.Create(filePath))
            {
                await stream.CopyToAsync(fileStream);
            }
            return true;
        }
    }
}
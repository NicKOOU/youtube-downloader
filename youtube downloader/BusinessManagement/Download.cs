using YoutubeExplode;

namespace DLYoutube
{
    public class DownloadUtils
    {

        public async Task<bool> DownloadVideoUtils(string[] urlVideos)
        {
            YoutubeClient client = new YoutubeClient();
            Download download = new Download(client);
            foreach (var urlVideo in urlVideos)
            {
                try
                {
                    var (stream, title) = await download.DownloadVideo(urlVideo);
                    await _storage.SaveFile(stream, title);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while downloading video: {ex.Message}");
                    // gérer l'exception
                }
            }
            return true;
        }


        public async Task<bool> DownloadChannelUtils(string channelId)
        {
            YoutubeClient client = new YoutubeClient();
            Download download = new Download(client);
            var videos = download.DownloadChannel(channelId);
            await foreach (var (stream, title) in videos)
            {
                await _storage.SaveFile(stream, title);
            }
            return true;
        }

        public DownloadUtils(Storage storage)
        {
            _storage = storage;
        }

        private readonly Storage _storage;
    }
}
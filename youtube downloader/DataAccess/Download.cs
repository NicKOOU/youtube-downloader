using YoutubeExplode.Videos.Streams;

namespace DLYoutube
{
    public class Download
    {
        public async Task<(Stream stream, string title)> DownloadVideo(string urlVideo)
        {
            try
            {
                var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(urlVideo);
                if (streamManifest == null)
                {
                    throw new Exception("Video not found");
                }
                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                var stream = await _youtubeClient.Videos.Streams.GetAsync(streamInfo);
                await _youtubeClient.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
                if (stream == null)
                {
                    throw new Exception("Stream not found");
                }
                return (stream, streamInfo.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while downloading video: {ex.Message}");
                throw;
            }
        }
        
        public async IAsyncEnumerable<(Stream stream, string title)> DownloadChannel(string channelId)
        {
                var channel = await _youtubeClient.Channels.GetAsync(channelId);
                var videos = _youtubeClient.Channels.GetUploadsAsync(channelId);
                await foreach (var video in videos)
                {
                    var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
                    var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                    var stream = await _youtubeClient.Videos.Streams.GetAsync(streamInfo);
                    await _youtubeClient.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
                    yield return (stream, streamInfo.ToString());
                }

        }

        public Download(YoutubeExplode.YoutubeClient youtubeClient)
        {
            _youtubeClient = youtubeClient ?? throw new ArgumentNullException(nameof(youtubeClient));
        }

        private readonly YoutubeExplode.YoutubeClient _youtubeClient;
    }
}

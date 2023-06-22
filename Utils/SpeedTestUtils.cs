using System.Net;

namespace InternetWorldTimeApp
{
    public static class WorldTimeUtils
    {
        //fix and improve the code
        public static async Task<long> UploadBytes(byte[] buffer, int bufferSize)
        {
            long totalBytes = 0;

            using (var client = new WebClient())
            {
                using (var stream = new MemoryStream(buffer))
                {
                    while (true)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, bufferSize);

                        if (bytesRead == 0)
                            break;

                        totalBytes += bytesRead;
                    }
                }
            }

            return totalBytes;
        }

        //implement DownloadBytes method
        public static async Task<long> DownloadBytes(byte[] buffer, int bufferSize)
        {
            long totalBytes = 0;

            using (var client = new WebClient())
            {
                using (var stream = new MemoryStream(buffer))
                {
                    while (true)
                    {

                        int bytesRead = await client.OpenReadTaskAsync("https://example.com").Result
                            .ReadAsync(buffer, 0, bufferSize);

                        if (bytesRead == 0)
                            break;

                        totalBytes += bytesRead;
                    }
                }
            }

            return totalBytes;
        }




    }
}

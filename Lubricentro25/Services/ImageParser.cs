namespace Lubricentro25.Services
{
    public static class ImageParser
    {
        public static async Task<byte[]?> ImageSourceToBytes(ImageSource imageSource)
        {
            if (imageSource is FileImageSource fileImageSource)
            {
                // If the ImageSource is a FileImageSource (file path)
                // You can load the image file and read its bytes
                var filePath = fileImageSource.File;
                if (filePath.Contains("person.png")) return null;
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    return await File.ReadAllBytesAsync(filePath);
                }
            }
            if (imageSource is StreamImageSource streamImageSource)
            {
                CancellationToken cancellationToken = CancellationToken.None;
                var stream = await streamImageSource.Stream.Invoke(cancellationToken);
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
            return null;
        }

        public static ImageSource BytesToImageSource(byte[] bytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}

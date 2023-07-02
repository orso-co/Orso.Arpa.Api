using System.IO;
using System.Threading;
using System.Threading.Tasks;
using QRCoder;
using SixLabors.ImageSharp;

namespace Orso.Arpa.Misc
{
    public static class ArpaQRCodeGenerator
    {
        public static async Task<byte[]> GetQRCodeAsync(string textToEncode, CancellationToken cancellationToken)
        {
            var qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            using Image qrCodeImage = qrCode.GetGraphic(20);
            return await ImageToBytesAsync(qrCodeImage, cancellationToken);
        }

        private static async Task<byte[]> ImageToBytesAsync(Image image, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream();
            await image.SaveAsPngAsync(stream, cancellationToken);
            return stream.ToArray();
        }
    }
}

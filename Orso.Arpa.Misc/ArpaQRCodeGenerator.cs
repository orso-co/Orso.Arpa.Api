using System.IO;
using QRCoder;
using SixLabors.ImageSharp;

namespace Orso.Arpa.Misc
{
    public static class ArpaQRCodeGenerator
    {
        public static byte[] GetQRCode(string textToEncode)
        {
            var qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrCodeData);
            Image qrCodeImage = qrCode.GetGraphic(20);
            return BitmapToBytes(qrCodeImage);
        }

        private static byte[] BitmapToBytes(Image image)
        {
            using var stream = new MemoryStream();
            image.SaveAsPng(stream);
            return stream.ToArray();
        }
    }
}

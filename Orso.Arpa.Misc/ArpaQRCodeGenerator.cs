using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace Orso.Arpa.Misc
{
    public static class ArpaQRCodeGenerator
    {
        public static byte[] GetQRCode(string textToEncode)
        {
            var qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return BitmapToBytes(qrCodeImage);
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }
    }
}

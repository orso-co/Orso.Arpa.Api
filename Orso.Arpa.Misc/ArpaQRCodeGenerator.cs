using QrCodes;
using QrCodes.Renderers;

namespace Orso.Arpa.Misc
{
    public static class ArpaQRCodeGenerator
    {
        private static ImageSharpRenderer s_imageSharpRenderer = new ImageSharpRenderer();

        public static byte[] GetQRCode(string textToEncode)
        {
            QrCode qrCode = QrCodeGenerator.Generate(
                plainText: textToEncode,
                eccLevel: ErrorCorrectionLevel.High);
            return s_imageSharpRenderer.RenderToBytes(qrCode);
        }
    }
}

using QrCodes;
using QrCodes.Renderers;
using QrCodes.Renderers.Abstractions;

namespace Orso.Arpa.Misc
{
    public static class ArpaQRCodeGenerator
    {
        private static ImageSharpRenderer s_imageSharpRenderer = new ImageSharpRenderer();

        public static byte[] GetQRCode(string textToEncode)
        {
            QrCode qrCode = QrCodeGenerator.Generate(
                plainText: textToEncode,
                eccLevel: ErrorCorrectionLevel.Quartile);
            return s_imageSharpRenderer.RenderToBytes(qrCode, new RendererSettings { PixelsPerModule = 20 } );
        }
    }
}

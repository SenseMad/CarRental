using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace CarRental
{
  /// <summary>
  /// Преобразование Bitmap в
  /// </summary>
  public static class ConvertBitmapTo
  {
    public static BitmapImage GetBitmapImage(Image parBitmap)
    {
      var retBitmapImage = new BitmapImage();

      if (parBitmap != null)
      {
        using (var memory = new MemoryStream())
        {
          parBitmap.Save(memory, ImageFormat.Png);
          memory.Position = 0;
          retBitmapImage.BeginInit();
          retBitmapImage.StreamSource = memory;
          retBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
          retBitmapImage.EndInit();
        }
      }
      return retBitmapImage;
    }
  }
}

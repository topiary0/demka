using System.Drawing;
using System.IO;

namespace WindowsFormsApp1
{
    public static class ImageLoader
    {
        private static readonly string ImportFolder = @"C:\Users\Иван\OneDrive\Рабочий стол\ЗАДАНИЕ ДЭ\Модуль 1\import";
        private static readonly string DefaultImagePath = Path.Combine(ImportFolder, "picture.png");

        public static Image LoadImage(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try { return Image.FromFile(path); }
                catch { }
            }
            // Заглушка из папки import
            if (File.Exists(DefaultImagePath))
            {
                try { return Image.FromFile(DefaultImagePath); }
                catch { }
            }
            // Если ничего нет – возвращаем null (или создаём пустое изображение)
            return null;
        }
    }
}
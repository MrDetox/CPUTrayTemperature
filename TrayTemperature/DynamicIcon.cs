using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TrayTemperature
{
    class DynamicIcon
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        // Store the old icon
        private static Icon _oldIcon = null;

        public static Icon CreateIcon(string Line1Text, Color Line1Color)
        {
            Font font = new Font("Roboto Mono", 10);
            Bitmap bitmap = new Bitmap(16, 16);
            Icon icon;

            using (Graphics graph = Graphics.FromImage(bitmap))
            {
                SizeF stringSize = graph.MeasureString(Line1Text, font);
                float x = (bitmap.Width - stringSize.Width) / 2;
                float y = (bitmap.Height - stringSize.Height) / 2;

                graph.DrawString(Line1Text, font, new SolidBrush(Line1Color), new PointF(-3, 0));
            }

            IntPtr hIcon = bitmap.GetHicon();
            icon = Icon.FromHandle(hIcon);

            // Destroy old icon if it exists
            if (_oldIcon != null)
            {
                DestroyIcon(_oldIcon.Handle);
                _oldIcon.Dispose();
            }

            // Remember this icon for next time
            _oldIcon = icon;

            bitmap.Dispose();

            return icon;
        }
    }
}

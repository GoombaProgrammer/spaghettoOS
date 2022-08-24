using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Utils {
    public static class Utils {
        public static int[] ScaleImage(Image image, int newWidth, int newHeight) {
            int[] rawData = image.rawData;
            int width = (int)image.Width;
            int height = (int)image.Height;
            int[] array = new int[newWidth * newHeight];
            int num = (width << 16) / newWidth + 1;
            int num2 = (height << 16) / newHeight + 1;
            for (int i = 0; i < newHeight; i++) {
                for (int j = 0; j < newWidth; j++) {
                    int num3 = j * num >> 16;
                    int num4 = i * num2 >> 16;
                    array[i * newWidth + j] = rawData[num4 * width + num3];
                }
            }

            return array;
        }

        public static void DrawImageFromIntArr(this Canvas cv, int[] array, int x, int y, int w, int h) {
            Pen pen = new Pen(Color.White);
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    pen.Color = Color.FromArgb(array[i + j * w]);
                    cv.DrawPoint(pen, x + i, y + j);
                }
            }
        }

    }
}

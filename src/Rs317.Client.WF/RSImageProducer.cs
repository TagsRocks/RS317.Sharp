
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Rs317.Sharp
{
	public sealed class RSImageProducer
	{
		public int[] pixels;

		public int width;

		public int height;

		private Bitmap image;

		private FasterPixel fastPixel;

		public RSImageProducer(int width, int height, Form component)
		{
			this.width = width;
			this.height = height;
			pixels = new int[width * height];
			image = new Bitmap(width, height);
			fastPixel = new FasterPixel(image);
			initDrawingArea();
		}

		public void initDrawingArea()
		{
			DrawingArea.initDrawingArea(height, width, pixels);
		}

		public void drawGraphics(int y, Graphics g, int x)
		{
			method239();
			lock (g)
			{
				g.DrawImageUnscaled(image, x, y);
			}
		}

		private void method239()
		{
			fastPixel.Lock();
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					int value = pixels[x + y * width];
					//fastPixel.SetPixel(x, y, Color.FromArgb((value >> 16) & 0xFF, (value >> 8) & 0xFF, value & 0xFF));
					fastPixel.SetPixel(x, y, (byte)(value >> 16), (byte)(value >> 8), (byte)value, 255);
				}
			}
			fastPixel.Unlock(true);
		}

		public bool imageUpdate(Image image, int i, int j, int k, int l, int i1)
		{
			return true;
		}
	}
}

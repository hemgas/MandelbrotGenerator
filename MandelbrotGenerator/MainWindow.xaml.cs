using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MandelbrotGenerator
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var sizeX = 1024; // (Int32) (Image.ActualWidth / 96);
			var sizeY = 1024; // (Int32) (Image.ActualHeight / 96);
			var m = MandelbrotImage.Create(
										pixelWidth: sizeX,
										pixelHight: sizeY,
										center: Complex.Zero,
										zoomFactor: sizeX / 3.0,
										maxIterations: 16,
										escapeRadius: 10000
									);
			var bitmap = BitmapSource.Create(
										pixelWidth: sizeX,
										pixelHeight: sizeY,
										dpiX: 96,
										dpiY: 96,
										pixelFormat: PixelFormats.Gray8,
										palette: null,
										pixels: m,
										stride: 1024
									);
			Image.Source = bitmap;
		}

		private void Image_Loaded(Object sender, RoutedEventArgs e)
		{
        }
    }
}
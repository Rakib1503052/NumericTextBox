using System.Windows;
using NumericTextBox;

namespace Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			IntBox.ValueChanged += IntBox_ValueChanged;
			FloatBox.ValueChanged += FloatBox_ValueChanged;
			UIntBox.ValueChanged += UIntBox_ValueChanged;
			HexBox.ValueChanged += HexBox_ValueChanged;
		}

		private void HexBox_ValueChanged(object? sender, ulong e)
		{
			HexVal.Text = e.ToString();
		}

		private void UIntBox_ValueChanged(object? sender, uint e)
		{
			UIntVal.Text = e.ToString();
		}

		private void FloatBox_ValueChanged(object? sender, float e)
		{
			FloatVal.Text = e.ToString();
		}

		public void IntBox_ValueChanged(object? sender, int e)
		{
			IntVal.Text = e.ToString();
		}
	}
}
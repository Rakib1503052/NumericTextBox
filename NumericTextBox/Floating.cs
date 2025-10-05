using System.Windows.Input;
using System.Windows.Controls;
using System;
using System.Windows;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace NumericTextBox
{
	public class FloatTextBox : TextBox
	{
		private float _value = 0.0f;
		public bool _scientific = false;
		private const string Pattern_SC = @"^[+-]?(\d+(\.\d*)?|\.\d+)?([eE][+-]?\d*)?$";
		private const string Pattern_NSC = @"^[+-]?(\d+(\.\d*)?|\.\d+)?$";
		private string Prev_Text = "";
		private bool _valueChanged = false;

		public FloatTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public event EventHandler<float>? ValueChanged;
		protected virtual void OnValueChanged(float value)
		{
			ValueChanged?.Invoke(this, value);
		}

		public float Value
		{
			get { return _value; }
			set
			{
				_value = value;
				_valueChanged = true;
				if (Scientific)
					Text = value.ToString("E");
				else
					Text = value.ToString();
				_valueChanged = false;
				OnValueChanged(value);
			}
		}

		public bool Scientific
		{
			get { return _scientific; }
			set
			{
				_scientific = value;
			}
		}

		private bool RegMatch(string _text, string _pattern)
		{
			return Regex.IsMatch(_text, _pattern);
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string text = e.DataObject.GetData(DataFormats.Text) as string;
				string _newText = Text.Substring(0, SelectionStart) + text + Text.Substring(SelectionStart + SelectionLength);

				if (Scientific)
				{
					if (!RegMatch(_newText, Pattern_SC))
						e.CancelCommand();
				}
				else
				{
					if (!RegMatch(_newText, Pattern_NSC))
						e.CancelCommand();
				}
			}
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			string _newText = Text.Substring(0, SelectionStart) + e.Text + Text.Substring(SelectionStart + SelectionLength);

			if (Scientific)
			{
				if (!RegMatch(_newText, Pattern_SC))
					e.Handled = true;
				else
					base.OnPreviewTextInput(e);
			}
			else
			{
				if (!RegMatch(_newText, Pattern_NSC))
					e.Handled = true;
				else
					base.OnPreviewTextInput(e);
			}
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (_valueChanged)
			{
				return;
			}

			if(float.TryParse(Text, out float value))
			{
				_value = value;
				OnValueChanged(value);
				base.OnTextChanged(e);
			}
			else
			{
				base.OnTextChanged(e);
			}
		}
	}

	public class DoubleTextBox : TextBox
	{
		private double _value = 0.0f;
		public bool _scientific = false;
		private const string Pattern_SC = @"^[+-]?(\d+(\.\d*)?|\.\d+)?([eE][+-]?\d*)?$";
		private const string Pattern_NSC = @"^[+-]?(\d+(\.\d*)?|\.\d+)?$";
		private string Prev_Text = "";
		private bool _valueChanged = false;

		public DoubleTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public event EventHandler<double>? ValueChanged;
		protected virtual void OnValueChanged(double value)
		{
			ValueChanged?.Invoke(this, value);
		}

		public double Value
		{
			get { return _value; }
			set
			{
				_value = value;
				_valueChanged = true;
				if (Scientific)
					Text = value.ToString("E");
				else
					Text = value.ToString();
				_valueChanged = false;
				OnValueChanged(value);
			}
		}

		public bool Scientific
		{
			get { return _scientific; }
			set
			{
				_scientific = value;
			}
		}

		private bool RegMatch(string _text, string _pattern)
		{
			return Regex.IsMatch(_text, _pattern);
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string text = e.DataObject.GetData(DataFormats.Text) as string;
				string _newText = Text.Substring(0, SelectionStart) + text + Text.Substring(SelectionStart + SelectionLength);

				if (Scientific)
				{
					if (!RegMatch(_newText, Pattern_SC))
						e.CancelCommand();
				}
				else
				{
					if (!RegMatch(_newText, Pattern_NSC))
						e.CancelCommand();
				}
			}
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			string _newText = Text.Substring(0, SelectionStart) + e.Text + Text.Substring(SelectionStart + SelectionLength);

			if (Scientific)
			{
				if (!RegMatch(_newText, Pattern_SC))
					e.Handled = true;
				else
					base.OnPreviewTextInput(e);
			}
			else
			{
				if (!RegMatch(_newText, Pattern_NSC))
					e.Handled = true;
				else
					base.OnPreviewTextInput(e);
			}
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (_valueChanged)
			{
				return;
			}

			if (double.TryParse(Text, out double value))
			{
				_value = value;
				OnValueChanged(value);
				base.OnTextChanged(e);
			}
			else
			{
				base.OnTextChanged(e);
			}
		}
	}

	public class DecimalTextBox : TextBox
	{
		private decimal _value = 0.0m;
		public bool _scientific = false;
		private const string Pattern_SC = @"^[+-]?(\d+(\.\d*)?|\.\d+)?([eE][+-]?\d*)?$";
		private const string Pattern_NSC = @"^[+-]?(\d+(\.\d*)?|\.\d+)?$";
		private string Prev_Text = "";
		private bool _valueChanged = false;

		public DecimalTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public event EventHandler<decimal>? ValueChanged;
		protected virtual void OnValueChanged(decimal value)
		{
			ValueChanged?.Invoke(this, value);
		}

		public decimal Value
		{
			get { return _value; }
			set
			{
				_value = value;
				_valueChanged = true;
				if (Scientific)
					Text = value.ToString("E");
				else
					Text = value.ToString();
				_valueChanged = false;
				OnValueChanged(value);
			}
		}

		public bool Scientific
		{
			get { return _scientific; }
			set
			{
				_scientific = value;
			}
		}

		private bool RegMatch(string _text, string _pattern)
		{
			return Regex.IsMatch(_text, _pattern);
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string text = e.DataObject.GetData(DataFormats.Text) as string;
				string _newText = Text.Substring(0, SelectionStart) + text + Text.Substring(SelectionStart + SelectionLength);

				if (Scientific)
				{
					if (!RegMatch(_newText, Pattern_SC))
						e.CancelCommand();
				}
				else
				{
					if (!RegMatch(_newText, Pattern_NSC))
						e.CancelCommand();
				}
			}
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			string _newText = Text.Substring(0, SelectionStart) + e.Text + Text.Substring(SelectionStart + SelectionLength);

			if (Scientific)
			{
				if (!RegMatch(_newText, Pattern_SC))
					e.Handled = true;
				else
					base.OnPreviewTextInput(e);
			}
			else
			{
				if (!RegMatch(_newText, Pattern_NSC))
					e.Handled = true;
				else
					base.OnPreviewTextInput(e);
			}
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (_valueChanged)
			{
				return;
			}

			if (decimal.TryParse(Text, out decimal value))
			{
				_value = value;
				OnValueChanged(value);
				base.OnTextChanged(e);
			}
			else
			{
				base.OnTextChanged(e);
			}
		}
	}
}

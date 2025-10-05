using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NumericTextBox
{
	public class IntTextBox : TextBox
	{
		private int _value = 0;
		private const string _pattern = @"^[+-]?\d+$";
		private bool _valueChanged = false;

		public IntTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public event EventHandler<int>? ValueChanged;
		protected virtual void OnValueChanged(int value)
		{
			ValueChanged?.Invoke(this, value);
		}
			
		public int Value
		{
			get { return _value; }
			set
			{
				_value = value;
				_valueChanged = true;
				Text = Convert.ToString(value);
				_valueChanged = false;
				OnValueChanged(value);
			}
		}

		private bool RegMacth(string _text)
		{
			return Regex.IsMatch(_text, _pattern);
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			if (Text.Length == 0 && (e.Text == "-" || e.Text == "+"))
			{
				base.OnPreviewTextInput(e);
				return;
			}
			if (e.Text[0] < '0' || e.Text[0] > '9')
			{
				e.Handled = true;
			}
			else
			{
				base.OnPreviewTextInput(e);
			}
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if(_valueChanged)
			{
				return;
			}

			if (Text == "-" || Text == "+" || Text.Length == 0)
			{
				base.OnTextChanged(e);
			}
			else if (int.TryParse(Text, out int newValue))
			{
				_value = newValue;
				base.OnTextChanged(e);
				OnValueChanged(newValue);
			}
			else
			{
				//Text = Convert.ToString(_value);
				//e.Handled = true;
				base.OnTextChanged(e);
			}
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string text = e.DataObject.GetData(DataFormats.Text) as string;
				string _newText = Text.Substring(0, SelectionStart) + text + Text.Substring(SelectionStart + SelectionLength);
				if (!RegMacth(_newText))
					e.CancelCommand();
			}
			else
			{
				e.CancelCommand();
			}
		}
	}

	public class LongTextBox : TextBox
	{
		private long _value = 0;
		private const string _pattern = @"^[+-]?\d+$";

		public event EventHandler<long>? ValueChanged;
		protected virtual void OnValueChanged(long value)
		{
			ValueChanged?.Invoke(this, value);
		}

		public LongTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public long Value
		{
			get { return _value; }
			set
			{
				_value = value;
				Text = Convert.ToString(value);
				OnValueChanged(value);
			}
		}

		private bool RegMacth(string _text)
		{
			return Regex.IsMatch(_text, _pattern);
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			if (Text.Length == 0 && (e.Text == "-" || e.Text == "+"))
			{
				base.OnPreviewTextInput(e);
				return;
			}
			if (e.Text[0] < '0' || e.Text[0] > '9')
			{
				e.Handled = true;
			}
			else
			{
				base.OnPreviewTextInput(e);
			}
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (Text == "-" || Text == "+" || Text.Length == 0)
			{
				base.OnTextChanged(e);
			}
			else if (long.TryParse(Text, out long newValue))
			{
				_value = newValue;
				base.OnTextChanged(e);
				OnValueChanged(newValue);
			}
			else
			{
				//Text = Convert.ToString(_value);
				//e.Handled = true;
				base.OnTextChanged(e);
			}
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string text = e.DataObject.GetData(DataFormats.Text) as string;
				string _newText = Text.Substring(0, SelectionStart) + text + Text.Substring(SelectionStart + SelectionLength);
				if (!RegMacth(_newText))
					e.CancelCommand();
			}
			else
			{
				e.CancelCommand();
			}
		}
	}
}

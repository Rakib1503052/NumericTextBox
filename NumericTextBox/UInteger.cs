using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NumericTextBox
{
	public class UIntTextBox : TextBox
	{
		private uint _value = 0;
		private bool _valueChanged = false;

		public UIntTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public event EventHandler<uint>? ValueChanged;
		protected virtual void OnValueChanged(uint value)
		{
			ValueChanged?.Invoke(this, value);
		}

		public uint Value
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

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
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
			if (_valueChanged)
			{
				return;
			}

			if (uint.TryParse(Text, out uint newValue))
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
				if (text.Length > 0 && !text.All(char.IsDigit))
					e.CancelCommand();
			}
			else
			{
				e.CancelCommand();
			}
		}

		private bool IsTextValid(string? text)
		{
			return uint.TryParse(text, out _);
		}
	}

	public class ULongTextBox : TextBox
	{
		private ulong _value = 0;
		private bool _valueChanged = false;

		public ULongTextBox()
		{
			DataObject.AddPastingHandler(this, OnPaste);
		}

		public event EventHandler<ulong>? ValueChanged;
		protected virtual void OnValueChanged(ulong value)
		{
			ValueChanged?.Invoke(this, value);
		}

		public ulong Value
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

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
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
			if (_valueChanged)
			{
				return;
			}

			if (ulong.TryParse(Text, out ulong newValue))
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
				if (text.Length > 0 && !text.All(char.IsDigit))
					e.CancelCommand();
			}
			else
			{
				e.CancelCommand();
			}
		}
	}
}

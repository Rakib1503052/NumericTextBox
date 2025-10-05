using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Windows.Devices.Power;

namespace NumericTextBox
{
	public class HexTextBox : TextBox
	{
		private ulong _value = 0;
		private bool _8byte = false;
		private static string s2 = "abcdefABCDEF0123456789";
		private readonly HashSet<char> _allowedChars = new(s2);
		private bool _alawaysUpper = true;
		private bool _alawaysLower = false;
		private bool _valueChanged = false;

		public HexTextBox()
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
				if (_alawaysLower)
					Text = "0x" + value.ToString("x");
				else
					Text = "0x" + value.ToString("X");
				_valueChanged = false;
				OnValueChanged(value);
			}
		}

		public bool RestrictTo8Bytes
		{
			get { return _8byte; }
			set
			{
				_8byte = value;
				_restrict8bytes();
			}
		}

		public bool AlwaysUpper
		{
			get { return _alawaysUpper; }
			set
			{
				_alawaysUpper = value;
				if (value)
					_alawaysLower = !value;
				_prettyUp();
			}
		}

		public bool AlwaysLower
		{
			get { return _alawaysLower; }
			set
			{
				_alawaysLower = value;
				if (value)
					_alawaysUpper = !value;
				_prettyUp();
			}
		}

		private void _restrict8bytes()
		{
			if (Text.Length > 16)
			{
				string val;
				if (Text.Substring(0, 2) == "0x")
					val = Text.Substring(2);
				else
					val = Text;

				if (val.Length > 16)
				{
					_valueChanged = true;
					Text = val.Substring(0, 16);
					_valueChanged = false;
					_prettyUp();
				}
			}
		}

		private void _prettyUp()
		{
			if (Text.Length == 0)
				return;

			string val;
			if (Text.Length > 1 && Text.Substring(0, 2) == "0x")
				val = Text.Substring(2);
			else
				val = Text;

			if (_alawaysUpper)
				val = val.ToUpper();
			else if (_alawaysLower)
				val = val.ToLower();

			Text = "0x" + val;
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			if (_valueChanged)
				return;

			string _text = Text.Length > 2 && Text.Substring(0, 2) == "0x" ? Text.Substring(2) : Text;
			if (ulong.TryParse(_text, NumberStyles.HexNumber, CultureInfo.InvariantCulture ,out ulong value))
			{
				_value = value;
				OnValueChanged(value);
			}

			base.OnTextChanged(e);
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
			_prettyUp();
			base.OnLostFocus(e);
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			if (e.Text == "x" && Text[0] == '0' && CaretIndex == 1)
			{
				base.OnPreviewTextInput(e);
				return;
			}

			if (_8byte && Text.Length > 15)
			{
				int len = Text.Substring(0,2) == "0x" ? 18 : 16;
				if (Text.Length == len)
				{
					e.Handled = true;
					return;
				}
			}

			if (e.Text.Length == 1 && _allowedChars.Contains(e.Text[0]))
			{
				base.OnPreviewTextInput(e);
				return;
			}
			else
			{
				e.Handled = true;
			}
		}

		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				string _pattern = @"^(0x)?[0-9A-Fa-f]+$";
				string text = e.DataObject.GetData(DataFormats.Text) as string;
				string _newText = Text.Substring(0, SelectionStart) + text + Text.Substring(SelectionStart + SelectionLength);
				if (_8byte && _newText.Length > 16)
				{
					int len = _newText.Substring(0, 2) == "0x" ? 18 : 16;
					if (_newText.Length > len)
					{
						e.CancelCommand();
						return;
					}
				}
				if (!Regex.IsMatch(_newText, _pattern))
					e.CancelCommand();
			}
			else
			{
				e.CancelCommand();
			}
		}
	}
}

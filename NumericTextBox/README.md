This is a collection of TextBox derived classes for restricting user input to numeric values only.
The classes in this collection are-
	IntTextBox
	LongTextBox
	UIntTextBox
	ULongTextBox
	FloatTextBox
	DoubleTextBox
	DecimalTextBox
	HexTextBox

For each class, the type name preceding "TextBox" ([TypeName]TextBox) denotes the value type the
text will be converted to.
All classes provide an internal "Value" property that keeps the numeric value converted from the
text as the user types. If the text cannot be converted to a value, for example, due to exceeding
the type's limit, the last value will be kept; however, the user can still continue inputting
into the textbox.

When the value changes, the UIElement object raises a "ValueChanged" event.


# Integer types

The Int and Long textboxes only accept number inputs and optional preceding "+" or "-".
The UInt and ULong textboxes do not accept the preceding sign.

# Decimal types

The Float, Double and Decimal textboxes accepts preceding sign and decimal ".".
Optionally, the "Scientific" property can be set to "true" to accept scientific notation.
The textboxes accepts incomplete text as well but it will not raise ValueChanged event.
example:
"-1.3e", "-1.3e-" are both valid inputs but will not raise ValueChanged event.
"1.3", "-1.3e-2" will raise ValueChanged event.

# Hexadecimal
The HexTextBox only accepts hexadecimal strings with optional "0x" prefix. If the user input
does not have the prefix, it will be added automatically when the textbox loses focus.
Additionally the following properties are available-
AlwaysUpper: Formats the text to have upper case characters on losing focus.
AlwaysLower: Formats the text to have lower case characters on losing focus.
RestrictTo8Bytes: Restricts the user input to 8 byte value or 16 hexadecimal digits.

All the user input value can be accessed anytime from the "Value" property present in every
class.

XML Namespace URI: https://github.com/Rakib1503052/NumericTextBox/

Example usage:

```
<Window x:Class="DemoApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ntb="https://github.com/Rakib1503052/NumericTextBox/"
        Title="CoolTextBox Demo" Height="200" Width="300">
    
	<Grid>
		<ntb:FloatTextBox Scientific="True"/>
		<ntb:HexTextBox AlwaysUpper="True" RestrictTo8Bytes="True"/>
	</Grid>
</Window>
```
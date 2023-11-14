using System.Windows;
using System.Windows.Controls;

namespace Noty.Structure
{
    public static class TextBoxAssist
    {
        // This strange default value is on purpose it makes the initialization problem very unlikely.
        // If the default value matches the default value of the property in the ViewModel,
        // the propertyChangedCallback of the FrameworkPropertyMetadata is initially not called
        // and if the property in the ViewModel is not changed it will never be called.
        private const string SelectedTextPropertyDefault = "pxh3949%lm/kkoiladasvvbnnn///$$$$21235";

        public static string GetSelectedText(DependencyObject obj)
        {
            return (string)obj.GetValue(SelectedTextProperty);
        }

        public static void SetSelectedText(DependencyObject obj, string value)
        {
            obj.SetValue(SelectedTextProperty, value);
        }

        public static readonly DependencyProperty SelectedTextProperty =
            DependencyProperty.RegisterAttached(
                "SelectedText",
                typeof(string),
                typeof(TextBoxAssist),
                new FrameworkPropertyMetadata(
                    SelectedTextPropertyDefault,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    SelectedTextChanged));

        private static void SelectedTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is not TextBox textBox)
            {
                return;
            }

            var oldValue = eventArgs.OldValue as string;
            var newValue = eventArgs.NewValue as string;

            if (oldValue == SelectedTextPropertyDefault && newValue != SelectedTextPropertyDefault)
            {
                textBox.SelectionChanged += SelectionChangedForSelectedText;
            }
            else if (oldValue != SelectedTextPropertyDefault && newValue == SelectedTextPropertyDefault)
            {
                textBox.SelectionChanged -= SelectionChangedForSelectedText;
            }

            if (newValue is not null && newValue != textBox.SelectedText)
            {
                textBox.SelectedText = newValue;
            }
        }

        private static void SelectionChangedForSelectedText(object sender, RoutedEventArgs eventArgs)
        {
            if (sender is TextBox textBox)
            {
                SetSelectedText(textBox, textBox.SelectedText);
            }
        }
    }
}
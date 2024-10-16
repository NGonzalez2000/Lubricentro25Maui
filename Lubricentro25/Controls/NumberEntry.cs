namespace Lubricentro25.Controls;

public class NumberEntry : Entry
{
    public NumberEntry()
    {
        TextChanged += NumberEntry_TextChanged;
    }

    private void NumberEntry_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry) return;
        
        TextChanged -= NumberEntry_TextChanged;
        if (e.NewTextValue.All(c => char.IsDigit(c) || c == '-'))
        {
            entry.Text = e.NewTextValue;
        }
        else
        {
            entry.Text = e.OldTextValue;
        }
        TextChanged += NumberEntry_TextChanged;

    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Ded.Net.Editor
{
    public class Editor : UserControl
    {
        public Editor()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

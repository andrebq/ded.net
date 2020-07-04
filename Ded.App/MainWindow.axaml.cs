using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace Ded.App
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            EditorView ev = this.FindControl<EditorView>("Editor");
            ev.Lines = new List<string>()
            {
                "abc",
                "cde",
            }.AsReadOnly();
        }
    }
}

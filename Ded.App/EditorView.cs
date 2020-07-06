using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

using Ded.TextProcessing;
using Avalonia.Input;
using System.Linq;
using System.Collections.Immutable;

namespace Ded.App
{
    public delegate void TextChanged(EditorView self);

    /// <summary>
    /// EditorView renders a dynamic list of line to the UI.
    /// </summary>
    public class EditorView : Avalonia.Controls.Control
    {
        public static readonly AttachedProperty<IBrush> ForegroundProperty =
            AvaloniaProperty.RegisterAttached<EditorView, Control, IBrush>(nameof(Foreground), Brushes.DarkOliveGreen, inherits: true);
        public static readonly AttachedProperty<IBrush> BackgroundProperty =
            AvaloniaProperty.RegisterAttached<EditorView, Control, IBrush>(nameof(Background), Brushes.Black, inherits: true);

        public static readonly DirectProperty<EditorView, TextBuffer> LinesProperty =
            AvaloniaProperty.RegisterDirect<EditorView, TextBuffer>(nameof(Lines), o => o.Lines, (o, v) => o.Lines = v);

        private static readonly TextBuffer _Empty = TextBuffer.Empty;

        public IBrush Foreground
        {
            get => GetForeground(this);
            set => SetForeground(this, value);
        }

        public IBrush Background
        {
            get => GetBackground(this);
            set => SetForeground(this, value);
        }

        private TextBuffer _Lines;
        public TextBuffer Lines
        {
            get => _Lines ?? _Empty;
            set => SetAndRaise<TextBuffer>(LinesProperty, ref _Lines, value ?? _Empty);
        }

        private Size _canvasSize;

        public readonly Typeface defaultFont = new Typeface("monospace", 14, FontStyle.Normal, FontWeight.Normal);

        public static void SetForeground(Control c, IBrush value) { c.SetValue(ForegroundProperty, value); }
        public static IBrush GetForeground(Control c) { return c.GetValue(ForegroundProperty); }

        public static void SetBackground(Control c, IBrush value) { c.SetValue(BackgroundProperty, value); }
        public static IBrush GetBackground(Control c) { return c.GetValue(BackgroundProperty);  }

        static EditorView()
        {
            FocusableProperty.OverrideDefaultValue(typeof(EditorView), true);
            ClipToBoundsProperty.OverrideDefaultValue<EditorView>(true);
            AffectsRender<EditorView>(LinesProperty, ForegroundProperty);
        }

        public override void Render(DrawingContext context)
        {
            if (_canvasSize == Size.Empty)
            {
                return;
            }
            context.FillRectangle(Background, new Rect(_canvasSize));
            var p = new Point();
            var formattedLines = Lines.Text.SplitBy('\n').Select(r => FormatLine(r.ToString())).ToImmutableList();
            foreach(var ft in formattedLines)
            {
                context.DrawText(Foreground, p, ft);
                p += new Point(0, ft.Bounds.Height);
            }
            var cursorCoordinate = Lines.Text.IndexToLineColumn(Lines.Cursor, '\n');
            var cursorStartPoint = new Point(0, 0);
            var cursorEndPoint = new Point(0, 0);
            for(var i = cursorCoordinate.Row; i > 0; i--)
            {
                cursorStartPoint = cursorStartPoint.WithY(cursorStartPoint.Y + formattedLines[i].Bounds.Height);
                cursorEndPoint = cursorStartPoint.WithY(cursorStartPoint.Y + formattedLines[i].Bounds.Height);
            }
            var pen = new Pen(Foreground, 3);
            context.DrawLine(pen, cursorStartPoint, cursorEndPoint);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            _canvasSize = availableSize;
            return availableSize;
        }

        private FormattedText FormatLine(string line)
        {
            var ft = new FormattedText();
            ft.Text = line;
            ft.TextAlignment = TextAlignment.Left;
            ft.Wrapping = TextWrapping.NoWrap;
            ft.Typeface = defaultFont;
            return ft;
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
        }
    }
}

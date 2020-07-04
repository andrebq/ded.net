﻿using Avalonia;
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

        public static readonly DirectProperty<EditorView, IReadOnlyList<string>> LinesProperty =
            AvaloniaProperty.RegisterDirect<EditorView, IReadOnlyList<string>>(nameof(Lines), o => o.Lines, (o, v) => o.Lines = v);

        private static readonly IReadOnlyList<string> _EmptyList = new List<string>() { }.AsReadOnly();

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

        private IReadOnlyList<string> _Lines;
        public IReadOnlyList<string> Lines
        {
            get => _Lines ?? _EmptyList;
            set => SetAndRaise<IReadOnlyList<string>>(LinesProperty, ref _Lines, value ?? _EmptyList);
        }

        private Size _canvasSize;

        public readonly Typeface defaultFont = new Typeface("monospace", 14, FontStyle.Normal, FontWeight.Normal);

        public static void SetForeground(Control c, IBrush value) { c.SetValue(ForegroundProperty, value); }
        public static IBrush GetForeground(Control c) { return c.GetValue(ForegroundProperty); }

        public static void SetBackground(Control c, IBrush value) { c.SetValue(BackgroundProperty, value); }
        public static IBrush GetBackground(Control c) { return c.GetValue(BackgroundProperty);  }

        static EditorView()
        {
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
            foreach(var l in Lines)
            {
                var ft = FormatLine(l);
                Debug.Print($"Foreground: {Foreground}");
                context.DrawText(Foreground, p, ft);
                p += new Point(0, ft.Bounds.Height);
                Debug.Print("rendering...");
            }
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
    }
}
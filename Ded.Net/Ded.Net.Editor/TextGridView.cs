using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ded.Net.Editor
{
    /// <summary>
    /// TextGridView is composed by a grid of TextCell
    /// </summary>
    public class TextGridView : Avalonia.Controls.Control
    {
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            var b = Brush.Parse("red");
            context.FillRectangle(b, this.Bounds, 0.0f);
        }
    }
}

﻿// The MIT License(MIT)
//
// Copyright(c) 2021 Alberto Rodriguez Orozco & LiveCharts Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using LiveChartsCore.Drawing;
using LiveChartsCore.Drawing.Common;
using LiveChartsCore.Motion;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Drawing;

namespace LiveChartsCore.SkiaSharpView.Drawing.Geometries
{
    /// <inheritdoc cref="ILabelGeometry{TDrawingContext}" />
    public class LabelGeometry : Geometry, ILabelGeometry<SkiaSharpDrawingContext>
    {
        private readonly FloatMotionProperty _textSizeProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelGeometry"/> class.
        /// </summary>
        public LabelGeometry()
            : base(true)
        {
            _textSizeProperty = RegisterMotionProperty(new FloatMotionProperty(nameof(TextSize), 11));
            TransformOrigin = new PointF(0f, 0f);
        }

        /// <summary>
        /// Gets or sets the vertical align.
        /// </summary>
        /// <value>
        /// The vertical align.
        /// </value>
        public Align VerticalAlign { get; set; } = Align.Middle;

        /// <summary>
        /// Gets or sets the horizontal align.
        /// </summary>
        /// <value>
        /// The horizontal align.
        /// </value>
        public Align HorizontalAlign { get; set; } = Align.Middle;

        /// <inheritdoc cref="ILabelGeometry{TDrawingContext}.Text" />
        public string Text { get; set; } = string.Empty;

        /// <inheritdoc cref="ILabelGeometry{TDrawingContext}.TextSize" />
        public float TextSize { get => _textSizeProperty.GetMovement(this); set => _textSizeProperty.SetMovement(value, this); }

        /// <inheritdoc cref="ILabelGeometry{TDrawingContext}.Padding" />
        public Padding Padding { get; set; } = new Padding();

        /// <inheritdoc cref="Geometry.OnDraw(SkiaSharpDrawingContext, SKPaint)" />
        public override void OnDraw(SkiaSharpDrawingContext context, SKPaint paint)
        {
            paint.TextSize = TextSize;
            context.Canvas.DrawText(Text ?? "", GetPosition(context, paint), paint);
        }

        /// <inheritdoc cref="Geometry.OnMeasure(Paint)" />
        protected override SizeF OnMeasure(Paint drawable)
        {
            var p = new SKPaint
            {
                Color = drawable.Color,
                IsAntialias = drawable.IsAntialias,
                IsStroke = drawable.IsStroke,
                StrokeWidth = drawable.StrokeThickness,
                TextSize = TextSize
            };

            var bounds = new SKRect();

            _ = p.MeasureText(Text, ref bounds);
            return new SizeF(bounds.Size.Width + Padding.Left + Padding.Right, bounds.Size.Height + Padding.Top + Padding.Bottom);
        }

        protected override SKPoint GetPosition(SkiaSharpDrawingContext context, SKPaint paint)
        {
            return new SKPoint(X, Y);
        }

        /// <inheritdoc cref="Geometry.ApplyCustomGeometryTransform(SkiaSharpDrawingContext)" />
        protected override void ApplyCustomGeometryTransform(SkiaSharpDrawingContext context)
        {
            var size = new SKRect();
            _ = context.Paint.MeasureText(Text, ref size);
            const double toRadians = Math.PI / 180d;

            var p = Padding;
            float w = 0.5f, h = 0.5f;

            switch (VerticalAlign)
            {
                case Align.Start: h = 1f * size.Height + p.Top; break;
                case Align.Middle: h = 0.5f * (size.Height + p.Top - p.Bottom); break;
                case Align.End: h = 0f * size.Height - p.Bottom; break;
                default:
                    break;
            }
            switch (HorizontalAlign)
            {
                case Align.Start: w = 0f * size.Width - p.Left; break;
                case Align.Middle: w = 0.5f * (size.Width - p.Left + p.Right); break;
                case Align.End: w = 1 * size.Width + p.Right; break;
                default:
                    break;
            }

            var rotation = RotationTransform;
            rotation = (float)(rotation * toRadians);

            var xp = -Math.Cos(rotation) * w + -Math.Sin(rotation) * h;
            var yp = -Math.Sin(rotation) * w + Math.Cos(rotation) * h;

            // translate the label to the upper-left corner
            // just for consistency with the rest of the shapes in the library (and Skia??),
            // and also translate according to the vertical an horizontal alignment properties
            context.Canvas.Translate((float)xp, (float)yp);
        }
    }
}

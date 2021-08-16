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

using System.Drawing;

namespace LiveChartsCore.Drawing
{
    /// <summary>
    /// Defines a geometry in the user interface.
    /// </summary>
    /// <typeparam name="TDrawingContext">The type of the drawing context.</typeparam>
    /// <seealso cref="IDrawable{TDrawingContext}" />
    public interface IGeometry<TDrawingContext> : IDrawable<TDrawingContext>, IPaintable<TDrawingContext>
        where TDrawingContext : DrawingContext
    {
        /// <summary>
        /// Gets or sets the translate transform.
        /// </summary>
        /// <value>
        /// The translate in coordinates.
        /// </value>
        PointF TranslateTransform { get; set; }

        /// <summary>
        /// Gets or sets the rotation in degrees.
        /// </summary>
        /// <value>
        /// The rotation in degrees.
        /// </value>
        float RotationTransform { get; set; }

        /// <summary>
        /// Gets or sets the scale transform.
        /// </summary>
        /// <value>
        /// The scale see https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/scale.
        /// </value>
        PointF ScaleTransform { get; set; }

        /// <summary>
        /// Gets or sets the skew transform.
        /// </summary>
        /// <value>
        /// The skew see https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/transforms/skew.
        /// </value>
        PointF SkewTransform { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        float X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        float Y { get; set; }

        /// <summary>
        /// Measures the specified drawable task.
        /// </summary>
        /// <param name="drawableTask">The drawable task.</param>
        /// <returns></returns>
        SizeF Measure(IPaint<TDrawingContext> drawableTask);
    }
}

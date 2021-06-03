/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cube.Forms.Drawing
{
    /* --------------------------------------------------------------------- */
    ///
    /// GraphicsExtension
    ///
    /// <summary>
    /// Provides extended methods of the Graphics class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    internal static class GraphicsExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// FillBackground
        ///
        /// <summary>
        /// Fills the background with the specified color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void FillBackground(this Graphics gs, Color color)
        {
            if (color != Color.Empty) gs.Clear(color);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawBorder
        ///
        /// <summary>
        /// Draws a border with the specified color and width.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawBorder(this Graphics gs, Rectangle bounds, Color color, int width)
        {
            if (color == Color.Empty || width <= 0) return;

            ControlPaint.DrawBorder(gs, bounds,
                color, width, ButtonBorderStyle.Solid,
                color, width, ButtonBorderStyle.Solid,
                color, width, ButtonBorderStyle.Solid,
                color, width, ButtonBorderStyle.Solid);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawText
        ///
        /// <summary>
        /// Draws the text at the specified position with the specified
        /// font and color.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawText(this Graphics gs,
            Rectangle bounds, string text, Font font, Color color, ContentAlignment align)
        {
            using var brush = new SolidBrush(color);
            var format = GetStringFormat(align);
            gs.DrawString(text, font, brush, bounds, format);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawImage
        ///
        /// <summary>
        /// Draws an image at the specified position.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawImage(this Graphics gs,
            Rectangle bounds, Image image, ContentAlignment align)
        {
            if (image == null) return;
            var rect = GetDrawBounds(bounds, image, align);
            gs.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawImage
        ///
        /// <summary>
        /// Draws an image at the specified position.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawImage(this Graphics gs,
            Rectangle bounds, Image image, ImageLayout layout)
        {
            if (image == null) return;

            switch (layout)
            {
                case ImageLayout.None:
                    gs.DrawImage(bounds, image, ContentAlignment.TopLeft);
                    break;
                case ImageLayout.Center:
                    gs.DrawImage(bounds, image, ContentAlignment.MiddleCenter);
                    break;
                case ImageLayout.Stretch:
                    DrawStretchImage(gs, bounds, image);
                    break;
                case ImageLayout.Zoom:
                    DrawZoomImage(gs, bounds, image);
                    break;
                case ImageLayout.Tile:
                    DrawTileImage(gs, bounds, image);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// DrawStretchImage
        ///
        /// <summary>
        /// Draws the image as large as possible, ignoring the aspect ratio.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void DrawStretchImage(Graphics gs, Rectangle bounds, Image image)
        {
            var rect = new Rectangle(0, 0, image.Width, image.Height);
            gs.DrawImage(image, bounds, rect, GraphicsUnit.Pixel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawZoomImage
        ///
        /// <summary>
        /// Draw the image as large as possible while maintaining the
        /// aspect ratio.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void DrawZoomImage(Graphics gs, Rectangle bounds, Image image)
        {
            var h = bounds.Width / (double)image.Width;
            var v = bounds.Height / (double)image.Height;
            var ratio  = Math.Min(h, v);
            var width  = (int)Math.Floor(image.Width * ratio);
            var height = (int)Math.Floor(image.Height * ratio);

            var resize = new Bitmap(width, height);
            using (var igs = System.Drawing.Graphics.FromImage(resize))
            {
                igs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                igs.DrawImage(image, 0, 0, width, height);
            }
            gs.DrawImage(bounds, resize, ContentAlignment.MiddleCenter);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawTileImage
        ///
        /// <summary>
        /// Draws the image as a tile.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void DrawTileImage(Graphics gs, Rectangle bounds, Image image)
        {
            using var brush = new TextureBrush(image)
            {
                WrapMode = System.Drawing.Drawing2D.WrapMode.Tile
            };
            gs.FillRectangle(brush, bounds);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetClipBounds
        ///
        /// <summary>
        /// Get the drawing area of an image.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Rectangle GetDrawBounds(Rectangle bounds, Image image, ContentAlignment align)
        {
            var dest = new Rectangle(bounds.X, bounds.Y, image.Width, image.Height);

            var width  = bounds.Width;
            var height = bounds.Height;
            var offset = Point.Empty;

            switch (align)
            {
                case ContentAlignment.TopLeft:
                    offset.X = 0;
                    offset.Y = 0;
                    break;
                case ContentAlignment.TopCenter:
                    offset.X = (width - image.Width) / 2;
                    offset.Y = 0;
                    break;
                case ContentAlignment.TopRight:
                    offset.X = width - image.Width;
                    offset.Y = 0;
                    break;
                case ContentAlignment.MiddleLeft:
                    offset.X = 0;
                    offset.Y = (height - image.Height) / 2;
                    break;
                case ContentAlignment.MiddleCenter:
                    offset.X = (width - image.Width) / 2;
                    offset.Y = (height - image.Height) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    offset.X = width - image.Width;
                    offset.Y = (height - image.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                    offset.X = 0;
                    offset.Y = height - image.Height;
                    break;
                case ContentAlignment.BottomCenter:
                    offset.X = (width - image.Width) / 2;
                    offset.Y = height - image.Height;
                    break;
                case ContentAlignment.BottomRight:
                    offset.X = width - image.Width;
                    offset.Y = height - image.Height;
                    break;
                default:
                    break;
            }

            dest.Offset(offset);
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetStringFormat
        ///
        /// <summary>
        /// Get a format object to draw a string.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static StringFormat GetStringFormat(ContentAlignment align)
        {
            var dest = new StringFormat();
            switch (align)
            {
                case ContentAlignment.TopLeft:
                    dest.Alignment     = StringAlignment.Near;
                    dest.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    dest.Alignment     = StringAlignment.Center;
                    dest.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    dest.Alignment     = StringAlignment.Far;
                    dest.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    dest.Alignment     = StringAlignment.Near;
                    dest.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    dest.Alignment     = StringAlignment.Center;
                    dest.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    dest.Alignment     = StringAlignment.Far;
                    dest.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    dest.Alignment     = StringAlignment.Near;
                    dest.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    dest.Alignment     = StringAlignment.Center;
                    dest.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    dest.Alignment     = StringAlignment.Far;
                    dest.LineAlignment = StringAlignment.Far;
                    break;
                default:
                    break;
            }
            return dest;
        }

        #endregion
    }
}

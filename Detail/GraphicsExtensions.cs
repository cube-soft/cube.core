/* ------------------------------------------------------------------------- */
///
/// GraphicsExtensions.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cube.Extensions.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Extensions.Forms.GraphicsExtensions
    /// 
    /// <summary>
    /// System.Drawing.Graphics の拡張メソッドを定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    static class GraphicsExtensions
    {
        #region Extension methods

        /* ----------------------------------------------------------------- */
        ///
        /// FillBackground
        /// 
        /// <summary>
        /// 指定した色で背景を塗りつぶします。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void FillBackground(this Graphics gs, Rectangle rect, Color color)
        {
            if (color == Color.Empty) return;

            using (var brush = new SolidBrush(color))
            {
                gs.FillRectangle(brush, rect);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawBorder
        /// 
        /// <summary>
        /// 指定した色、幅で枠線を描画します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: ClipBounds の幅、高さで枠線を描画すると一部が切れてしまう。
        /// 暫定的に margin を設定しているが、適切な描画方法について要調査。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawBorder(this Graphics gs, Rectangle rect, Color color, int width)
        {
            if (color == Color.Empty || width <= 0) return;

            //var rectF = rect;
            //var rect = new Rectangle((int)rectF.X, (int)rectF.Y, (int)rectF.Width, (int)rectF.Height);
            ControlPaint.DrawBorder(gs, rect,
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
        /// 指定したフォント、色で指定された位置にテキストを描画します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawText(this Graphics gs, Rectangle rect, string text, Font font, Color color, ContentAlignment align)
        {
            using (var brush = new System.Drawing.SolidBrush(color))
            {
                var format = new StringFormat();
                switch (align)
                {
                    case ContentAlignment.TopRight:
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopLeft:
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopCenter:
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.MiddleRight:
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleLeft:
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleCenter:
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomRight:
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomLeft:
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomCenter:
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Far;
                        break;
                }
                gs.DrawString(text, font, brush, rect, format);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawImage
        /// 
        /// <summary>
        /// 指定した位置に画像を描画します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawImage(this Graphics gs, Rectangle rect, Image image, ContentAlignment align)
        {
            if (image == null) return;
            var rectangle = GetClipBounds(rect, image, align);
            gs.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawImage
        /// 
        /// <summary>
        /// 指定した位置に画像を描画します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawImage(this Graphics gs, Rectangle rect, Image image, ImageLayout layout)
        {
            if (image == null) return;

            switch (layout)
            {
                case ImageLayout.None:
                    gs.DrawImage(rect, image, ContentAlignment.TopLeft);
                    break;
                case ImageLayout.Center:
                    gs.DrawImage(rect, image, ContentAlignment.MiddleCenter);
                    break;
                case ImageLayout.Stretch:
                    gs.DrawImage(image, rect, new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                    break;
                case ImageLayout.Zoom:
                    int height = 0;
                    int width = 0;
                    var hrate = (double)(rect.Height / image.Height);
                    var wrate = (double)(rect.Width / image.Width);
                    if (hrate < wrate)
                    {
                        width = (int)Math.Floor(image.Width * hrate);
                        height = rect.Height;
                    }
                    else
                    {
                        width = rect.Width;
                        height = (int)Math.Floor(image.Height * wrate);
                    }
                    var resizeImage = new Bitmap(width, height);
                    using (var g = Graphics.FromImage(resizeImage))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, width, height);
                    }
                    gs.DrawImage(rect, resizeImage, ContentAlignment.MiddleCenter);
                    break;
                case ImageLayout.Tile:
                    using (var brush = new TextureBrush(image))
                    {
                        brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                        gs.FillRectangle(brush, rect);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Private methods

        /* ----------------------------------------------------------------- */
        ///
        /// GetClipBounds
        /// 
        /// <summary>
        /// 画像の描画領域を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static Rectangle GetClipBounds(RectangleF rect, Image image, ContentAlignment align)
        {
            var dest = new Rectangle(0, 0, image.Width, image.Height);

            var width  = (int)rect.Width;
            var height = (int)rect.Height;
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

        #endregion
    }
}

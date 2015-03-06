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
        public static void FillBackground(this Graphics gs, Color color)
        {
            if (color == Color.Empty) return;

            using (var brush = new SolidBrush(color))
            {
                gs.FillRectangle(brush, gs.ClipBounds);
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
        public static void DrawBorder(this Graphics gs, Color color, int width)
        {
            if (color == Color.Empty || width <= 0) return;

            const float margin = 0.5f;
            using (var pen = new Pen(color, width))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                var rect = gs.ClipBounds;
                gs.DrawRectangle(pen, rect.X, rect.Y, rect.Width - margin, rect.Height - margin);
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
        public static void DrawImage(this Graphics gs, Image image, ContentAlignment align)
        {
            if (image == null) return;
            var rect = GetClipBounds(gs.ClipBounds, image, align);
            gs.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawImage
        /// 
        /// <summary>
        /// 指定した位置に画像を描画します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: ImageLayout が Stretch, Zoom, Tile の場合の実装
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public static void DrawImage(this Graphics gs, Image image, ImageLayout layout)
        {
            if (image == null) return;

            switch (layout)
            {
                case ImageLayout.None:
                    gs.DrawImage(image, ContentAlignment.TopLeft);
                    break;
                case ImageLayout.Center:
                    gs.DrawImage(image, ContentAlignment.MiddleCenter);
                    break;
                case ImageLayout.Stretch:
                    break;
                case ImageLayout.Zoom:
                    break;
                case ImageLayout.Tile:
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

﻿using System;
using System.IO;

namespace SkiaSharp.Components
{
    public class ImageBrush : IBrush
    {
        public ImageBrush(SKBitmap source, float opacity)
        {
            this.Source = source;
            this.Opacity = opacity;
        }

        public SKBitmap Source { get; }

        public float Opacity { get; }

        public void Fill(SKCanvas canvas, SKPath path)
        {
            using (var paint = new SKPaint()
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            })
            {
                canvas.Save();
                try
                {
                    using (var img = SKImage.FromBitmap(Source))
                    {
                        paint.Color = SKColors.White.WithAlpha((byte)(this.Opacity * 255));
                        canvas.ClipPath(path);
                        canvas.DrawImage(img, path.Bounds, paint);
                    }
                }
                catch(Exception e)
                {
                    using (var errorPaint = new SKPaint
                    {
                        Color = SKColors.Red,
                        Style = SKPaintStyle.Fill,
                    })
                    {
                        canvas.DrawRect(path.Bounds, errorPaint);
                        errorPaint.Color = SKColors.White;
                        canvas.DrawText(e.Message, path.Bounds.MidX, path.Bounds.MidY, errorPaint);
                    }
                }
                canvas.Restore();
            }
        }

        public void Stroke(SKCanvas canvas, SKPath path, float size, StrokeStyle style, SKStrokeCap cap, SKStrokeJoin join)
        {
            throw new NotImplementedException();
        }

        public void Text(SKCanvas canvas, string text, SKRect frame, SKTypeface typeface, float size, TextDecoration decorations)
        {
            throw new NotImplementedException();
        }
    }
}

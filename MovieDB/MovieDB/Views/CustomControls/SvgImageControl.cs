using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

using SkiaSharp;
using SkiaSharp.Views.Forms;

using Svg;
using Svg.Model;
using Svg.Skia;

using Xamarin.Forms;

namespace MovieDB.Views.CustomControls
{
    public class SvgImageControl : Frame
    {
        #region Properties
        private readonly SKCanvasView _canvasView = new SKCanvasView();
        private readonly string _svgPath = "MovieDB.Resources.Assets.";
        #endregion

        #region Lifecycle
        public SvgImageControl()
        {
            Padding = new Thickness(0);

            HasShadow = false;
            BackgroundColor = Color.Transparent;

            Content = _canvasView;
            _canvasView.PaintSurface += CanvasViewOnPaintSurface;
        }
        #endregion

        #region InternalsMethods
        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            SvgImageControl svgIcon = bindable as SvgImageControl;
            svgIcon?._canvasView.InvalidateSurface();
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            if (string.IsNullOrEmpty(Source))
                return;

            using (Stream stream = GetType().Assembly.GetManifestResourceStream($"{_svgPath}{Source}"))
            {
                SvgDocument svgDocument = SvgDocument.Open<SvgDocument>(stream, null);

                if (svgDocument is { })
                {
                    var references = new HashSet<Uri> { svgDocument.BaseUri };

                    var drawable = SvgExtensions.ToDrawable(svgDocument, new SkiaAssetLoader(), references, out var bounds);

                    if (drawable is { } && bounds is { })
                    {
                        var picture = drawable.Snapshot(bounds.Value);

                        var skiaPicture = picture?.ToSKPicture();

                        if (skiaPicture is { })
                        {
                            float pwidth = skiaPicture.CullRect.Width;
                            float pheight = skiaPicture.CullRect.Height;

                            if (pwidth > 0f && pheight > 0f)
                            {
                                SKImageInfo info = args.Info;
                                canvas.Translate(info.Width / 2f, info.Height / 2f);

                                var skRect = bounds?.ToSKRect();

                                if (skRect is { })
                                {
                                    float xRatio = info.Width / skRect.Value.Width;
                                    float yRatio = info.Height / skRect.Value.Height;

                                    float ratio = Math.Min(xRatio, yRatio);

                                    canvas.Scale(ratio);
                                    canvas.Translate(-skRect.Value.MidX, -skRect.Value.MidY);
                                    canvas.DrawPicture(skiaPicture);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void OnClickedCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            SvgImageControl svgIcon = bindable as SvgImageControl;

            if (svgIcon?.ClickedCommand != null)
            {
                if (svgIcon.GestureRecognizers.Count > 0)
                    svgIcon.GestureRecognizers.Clear();

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, new Binding() { Path = nameof(ClickedCommand), Source = svgIcon });

                svgIcon.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }
        #endregion

        #region BindableProperties

        #region SourceProperty
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: nameof(Source),
            returnType: typeof(string),
            declaringType: typeof(SvgImageControl),
            defaultValue: default(string),
            propertyChanged: RedrawCanvas);

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        #endregion

        #region ClickedCommandProperty
        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(
            propertyName: nameof(ClickedCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(SvgImageControl),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnClickedCommandPropertyChanged);

        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }
        #endregion
        #endregion
    }
}


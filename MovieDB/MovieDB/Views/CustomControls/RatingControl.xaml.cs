using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MovieDB.Views.CustomControls
{
    public partial class RatingControl : ContentView
    {
        #region Lifecycle
        public RatingControl()
        {
            InitializeComponent();
        }
        #endregion

        #region BindableProperties
        public static readonly BindableProperty RatingProperty = BindableProperty.Create(
         propertyName: nameof(Rating),
         returnType: typeof(double),
         declaringType: typeof(RatingControl),
         propertyChanged: OnRatingChanged);
        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        private static void OnRatingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingControl)bindable;
            var ratingMod = Math.Round((decimal.Parse(newValue.ToString())) / 2);

            control.VwStar1.IsVisible = ratingMod >= 1;
            control.VwStar2.IsVisible = ratingMod >= 2;
            control.VwStar3.IsVisible = ratingMod >= 3;
            control.VwStar4.IsVisible = ratingMod >= 4;
            control.VwStar5.IsVisible = ratingMod >= 5;
        }
        #endregion
    }
}


using System;
using Android.App;
using Android.Content;
using Android.Gms.Location.Places.UI;
using Android.Widget;
using Android.OS;

namespace GooglePlacePicker.Droid
{
    [Activity(Label = "GooglePlacePicker.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static readonly int PLACE_PICKER_REQUEST = 1;

        private Button _pickAPlaceButton;
        private TextView _placeNameTextView;
        private TextView _placeAddressTextView;
        private TextView _placePhoneNumberTextView;
        private TextView _placeWebSiteTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            _placeNameTextView = FindViewById<TextView>(Resource.Id.main_placeName);
            _placeAddressTextView = FindViewById<TextView>(Resource.Id.main_placeAddress);
            _placePhoneNumberTextView = FindViewById<TextView>(Resource.Id.main_placePhoneNumber);
            _placeWebSiteTextView = FindViewById<TextView>(Resource.Id.main_placeWebSite);

            _pickAPlaceButton = FindViewById<Button>(Resource.Id.main_pickAPlaceButton);
            _pickAPlaceButton.Click += OnPickAPlaceButtonTapped;
        }

        private void OnPickAPlaceButtonTapped(object sender, EventArgs eventArgs)
        {
            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), PLACE_PICKER_REQUEST);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == PLACE_PICKER_REQUEST && resultCode == Result.Ok)
            {
                GetPlaceFromPicker(data);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void GetPlaceFromPicker(Intent data)
        {
            var placePicked = PlacePicker.GetPlace(this, data);

            _placeNameTextView.Text = placePicked?.NameFormatted?.ToString();
            _placeAddressTextView.Text = placePicked?.AddressFormatted?.ToString();
            _placePhoneNumberTextView.Text = placePicked?.PhoneNumberFormatted?.ToString();
            _placeWebSiteTextView.Text = placePicked?.WebsiteUri?.ToString();
        }
    }
}



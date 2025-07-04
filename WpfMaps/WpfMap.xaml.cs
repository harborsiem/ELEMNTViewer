using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MapControl;
using Microsoft.Extensions.Logging;
using MapControl.UiTools; //harbor: using added
using PropResources = WpfMaps.Properties.Resources; //harbor: using added

namespace WpfMaps
{
    /// <summary>
    /// Interaction logic for WpfMap.xaml
    /// </summary>
    public partial class WpfMap : Window
    {
        public WpfMap()
        {
            //var loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());
            //ImageLoader.LoggerFactory = loggerFactory;

            //var tileCache = new MapControl.Caching.ImageFileCache(TileImageLoader.DefaultCacheFolder, loggerFactory);
            //TileImageLoader.Cache = tileCache;
            //Closed += (s, e) => tileCache.Dispose();

            InitializeComponent();

            //sampleOverlayMenuItem.MapLayerFactory = async () => await GroundOverlay.CreateAsync("etna.kml");

            AddTestLayers();
        }

        //harbor: method added
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            ContextMenu menu = mapLayersMenuButton.ContextMenu;

            foreach (var item in menu.Items.OfType<MenuItem>())
            {
                if (((string)item.Header == PropResources.Graticule) || ((string)item.Header == PropResources.Scale))
                {
                    ((MapOverlayMenuItem)item).Execute(map);
                }
            }
        }

        partial void AddTestLayers();

        private void MapItemsControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("SelectedItems: " + string.Join(", ", ((MapItemsControl)sender).SelectedItems.OfType<PointItem>().Select(item => item.Name)));
        }

        private void ResetHeadingButtonClick(object sender, RoutedEventArgs e)
        {
            map.TargetHeading = 0d;
        }

        private async void MapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.Source == map)
            {
                map.TargetCenter = map.ViewToLocation(e.GetPosition(map));
            }
            else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) &&
                map.MapLayer is WmsImageLayer wmsLayer)
            {
                Debug.WriteLine(await wmsLayer.GetFeatureInfoAsync(e.GetPosition(map)));
            }
        }

        private void MapMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var location = map.ViewToLocation(e.GetPosition(map));

            if (location != null && map.CaptureMouse())
            {
                map.Cursor = Cursors.Cross;
                measurementLine.Visibility = Visibility.Visible;
                measurementLine.Locations = new LocationCollection(location);
            }
        }

        private void MapMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            map.ReleaseMouseCapture();
            map.Cursor = null;
            measurementLine.Visibility = Visibility.Collapsed;
            measurementLine.Locations = null;
        }

        private void MapMouseMove(object sender, MouseEventArgs e)
        {
            var location = map.ViewToLocation(e.GetPosition(map));

            if (location != null)
            {
                mouseLocation.Visibility = Visibility.Visible;
                mouseLocation.Text = GetLatLonText(location);

                var start = measurementLine.Locations?.FirstOrDefault();

                if (start != null)
                {
                    measurementLine.Locations = LocationCollection.OrthodromeLocations(start, location);
                    mouseLocation.Text += GetDistanceText(location.GetDistance(start));
                }
            }
            else
            {
                mouseLocation.Visibility = Visibility.Collapsed;
                mouseLocation.Text = string.Empty;
            }
        }

        private void MapMouseLeave(object sender, MouseEventArgs e)
        {
            mouseLocation.Visibility = Visibility.Collapsed;
            mouseLocation.Text = string.Empty;
        }

        private void MapManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 0.001;
        }

        //private void MapItemTouchDown(object sender, TouchEventArgs e)
        //{
        //    var mapItem = (MapItem)sender;
        //    mapItem.IsSelected = !mapItem.IsSelected;
        //    e.Handled = true;
        //}

        private static string GetLatLonText(Location location)
        {
            var latitude = (int)Math.Round(location.Latitude * 60000d);
            var longitude = (int)Math.Round(Location.NormalizeLongitude(location.Longitude) * 60000d);
            var latHemisphere = 'N';
            var lonHemisphere = 'E';

            if (latitude < 0)
            {
                latitude = -latitude;
                latHemisphere = 'S';
            }

            if (longitude < 0)
            {
                longitude = -longitude;
                lonHemisphere = 'W';
            }

            return string.Format(CultureInfo.InvariantCulture,
                "{0}  {1:00} {2:00.000}\n{3} {4:000} {5:00.000}",
                latHemisphere, latitude / 60000, (latitude % 60000) / 1000d,
                lonHemisphere, longitude / 60000, (longitude % 60000) / 1000d);
        }

        private static string GetDistanceText(double distance)
        {
            var unit = PropResources.Meter;

            if (distance >= 1000d)
            {
                distance /= 1000d;
                unit = PropResources.Km;
            }

            var distanceFormat = distance >= 100d ? "F0" : "F1";

            return string.Format(CultureInfo.InvariantCulture, "\n   {0:" + distanceFormat + "} {1}", distance, unit);
        }
    }
}

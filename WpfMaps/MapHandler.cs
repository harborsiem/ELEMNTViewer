using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapControl;

namespace WpfMaps
{
    public class MapHandler
    {
        private WpfMap map;

        public MapHandler(int width, int height)
        {
            map = new WpfMap();
            map.Loaded += Map_Loaded;
            if (width > 0 && height > 0)
            {
                Width = width;
                Height = height;
            }
            else
            {
                Width = map.Width;
                Height = map.Height;
            }
        }

        private void Map_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            map.Width = Width;
            map.Height = Height;
        }

        public void SetLocations(Location mapCenter, LocationCollection locations, List<PointItem> pointItems)
        {
            MapViewModel mapViewModel = map.DataContext as MapViewModel;
            if (mapViewModel != null)
            {
                mapViewModel.MapCenter = mapCenter;
                mapViewModel.Polylines.Clear();
                mapViewModel.Points.Clear();
                mapViewModel.Pushpins.Clear();
                for (int i = 0; i < pointItems.Count; i++)
                {
                    mapViewModel.Pushpins.Add(pointItems[i]);
                }
                mapViewModel.Polylines.Add(new Polyline() { Locations = locations });
            }
        }

        public bool? ShowDialog()
        {
            return map.ShowDialog();
        }

        public double Width { get; set; }
        public double Height { get; set; }
    }
}

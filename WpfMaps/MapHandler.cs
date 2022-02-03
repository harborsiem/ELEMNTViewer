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
        private WpfMap wpfMap;

        public MapHandler(int width, int height)
        {
            wpfMap = new WpfMap();
            wpfMap.Loaded += Map_Loaded;
            if (width > 0 && height > 0)
            {
                Width = width;
                Height = height;
            }
            else
            {
                Width = wpfMap.Width;
                Height = wpfMap.Height;
            }
        }

        private void Map_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            wpfMap.Width = Width;
            wpfMap.Height = Height;
        }

        public void SetLocations(Location mapCenter, LocationCollection locations, List<PointItem> pointItems)
        {
            MapViewModel mapViewModel = wpfMap.DataContext as MapViewModel;
            if (mapViewModel != null)
            {
                //wpfMap.map.MapProjection= new WebMercatorProjection(); //this is default
                wpfMap.map.Center = mapCenter;
                mapViewModel.Polylines.Clear();
                mapViewModel.Points.Clear();
                mapViewModel.Pushpins.Clear();
                for (int i = 0; i < pointItems.Count; i++)
                {
                    mapViewModel.Pushpins.Add(pointItems[i]);
                }
                mapViewModel.Polylines.Add(new PolylineItem() { Locations = locations });
            }
        }

        public bool? ShowDialog()
        {
            return wpfMap.ShowDialog();
        }

        public double Width { get; set; }
        public double Height { get; set; }
    }
}

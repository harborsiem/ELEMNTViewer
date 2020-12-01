using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapControl;
using ViewModel;

namespace WpfMaps
{
    public class MapHandler
    {
        private WpfMap map;

        public MapHandler()
        {
            map = new WpfMap();
        }

        public void SetLocations(Location mapCenter, LocationCollection locations, List<PointItem> pointItems)
        {
            ViewModel.MapViewModel mapViewModel = map.DataContext as ViewModel.MapViewModel;
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

    }

    public class PointCollection
    {
        public Location Location { get; private set; }
        public string Name { get; private set; }
        public PointCollection(Location location, string name)
        {
            Location = location;
            Name = name;
        }
    }

}

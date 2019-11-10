using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace System.Windows.Forms.DataVisualization.Charting {
    //This extension-methods are necessary to speed up the Clear() operation of most Chart collections
    //The performance bug is in the abstract class ChartElementCollection, function ClearItems()
    //The call of RemoveItem(0) should have corrected to RemoveItem(base.Count - 1)
    //For speed you have to call ClearFast.
    
    public static class MsChartExtension {
        /// <summary>
        /// Speed up MSChart data points clear operations.
        /// </summary>
        /// <param name="sender"></param>
        public static void ClearPoints(this Series sender) {
            DataPointCollection points = sender.Points;
            points.SuspendUpdates();
            while (points.Count > 0) {
                points.RemoveAt(points.Count - 1);
            }
            points.ResumeUpdates();
            points.Clear(); //Force refresh.
        }

        public static void ClearFast(this DataPointCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
            collection.Clear();
        }

        public static void ClearFast(this AnnotationPathPointCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this CustomLabelsCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this LegendItemsCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this StripLinesCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this AnnotationCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this ChartAreaCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this LegendCellCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this LegendCellColumnCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this LegendCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this NamedImagesCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this SeriesCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }

        public static void ClearFast(this TitleCollection collection) {
            collection.SuspendUpdates();
            while (collection.Count > 0) {
                collection.RemoveAt(collection.Count - 1);
            }
            collection.ResumeUpdates();
        }
    }
}

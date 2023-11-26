using System;
using System.Collections.Generic;
using System.Text;
using ELEMNTViewer.Properties;

namespace ELEMNTViewer
{
    internal static class SR
    {
        internal static string GetResourceString(string resourceKey)
        {
            //if (UsingResourceKeys())
            //{
            //    return resourceKey;
            //}

            string resourceString = null;
            try
            {
                resourceString =
                    Resources.ResourceManager.GetString(resourceKey);
            }
            catch (Exception) { }

            return resourceString; // only null if missing resources
        }

        internal static string GetResourceString(string resourceKey, string defaultString)
        {
            string resourceString = GetResourceString(resourceKey);

            return resourceKey == resourceString || resourceString == null ? defaultString : resourceString;
        }

    }
}

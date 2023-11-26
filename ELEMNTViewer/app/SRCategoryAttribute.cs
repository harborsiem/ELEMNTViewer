using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ELEMNTViewer
{
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRCategoryAttribute : CategoryAttribute
    {
        public SRCategoryAttribute(string category) : base(category) { }

        protected override string GetLocalizedString(string value)
        {
            return SR.GetResourceString(value);
        }
    }
}

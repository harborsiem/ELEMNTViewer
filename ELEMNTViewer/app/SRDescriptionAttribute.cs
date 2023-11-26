using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ELEMNTViewer
{
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
        private bool replaced;

        public SRDescriptionAttribute(string description) : base(description) { }

        public override string Description
        {
            get
            {
                if (!replaced)
                {
                    replaced = true;
                    DescriptionValue = SR.GetResourceString(base.Description);
                }
                return base.Description;
            }
        }
    }
}

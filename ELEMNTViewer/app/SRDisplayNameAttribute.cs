using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ELEMNTViewer
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Class |
                    AttributeTargets.Method)]
    internal sealed class SRDisplayNameAttribute : DisplayNameAttribute
    {
        private bool replaced;

        public SRDisplayNameAttribute(string displayName) : base(displayName) { }

        public override string DisplayName
        {
            get
            {
                if (!replaced)
                {
                    replaced = true;
                    DisplayNameValue = SR.GetResourceString(base.DisplayName);
                }
                return base.DisplayName;
            }
        }
    }
}

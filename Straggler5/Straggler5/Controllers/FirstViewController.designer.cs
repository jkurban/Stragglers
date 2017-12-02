// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Straggler5
{
    [Register("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        UIKit.UILabel pressureTrend { get; set; }

        [Outlet]
        UIKit.UILabel pressureValue { get; set; }

        [Outlet]
        UIKit.UIView PressureViewHandle { get; set; }

        [Outlet]
        UIKit.UILabel sandValue { get; set; }

        [Outlet]
        UIKit.UIView SandViewHandle { get; set; }

        [Outlet]
        UIKit.UILabel snadTrend { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (pressureTrend != null)
            {
                pressureTrend.Dispose();
                pressureTrend = null;
            }

            if (pressureValue != null)
            {
                pressureValue.Dispose();
                pressureValue = null;
            }

            if (PressureViewHandle != null)
            {
                PressureViewHandle.Dispose();
                PressureViewHandle = null;
            }

            if (sandValue != null)
            {
                sandValue.Dispose();
                sandValue = null;
            }

            if (SandViewHandle != null)
            {
                SandViewHandle.Dispose();
                SandViewHandle = null;
            }

            if (snadTrend != null)
            {
                snadTrend.Dispose();
                snadTrend = null;
            }
        }
    }
}

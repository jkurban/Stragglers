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
    [Register("SecondViewController")]
    partial class SecondViewController
    {
        [Outlet]
        UIKit.UIButton askButton { get; set; }

        [Outlet]
        UIKit.UITextField askText { get; set; }

        [Outlet]
        UIKit.UITextView responseText { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (askText != null)
            {
                askText.Dispose();
                askText = null;
            }

            if (askButton != null)
            {
                askButton.Dispose();
                askButton = null;
            }

            if (responseText != null)
            {
                responseText.Dispose();
                responseText = null;
            }
        }
    }
}

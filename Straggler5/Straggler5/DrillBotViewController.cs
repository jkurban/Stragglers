using System;
using System.Diagnostics;
using UIKit;

namespace Straggler5
{
    public partial class DrillBotViewController : UIViewController
    {
        private static string directLineSecret = "esrCB624bhqbXFBSM26|^:(";
        private static string botId = "urban-test-bot";
        private static string fromUser = "StragglerApp";

        public DrillBotViewController() : base("DrillBotViewController", null)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            Debug.WriteLine("ViewWillAppear called");
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            Debug.WriteLine("ViewDidLoad called");
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            // StartConversation();
        }

        private void AskButton_TouchUpInside(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            // EndConversation();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


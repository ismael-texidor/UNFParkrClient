using System;

using UIKit;

namespace ParkrUNF
{
	public partial class MainViewController : UIViewController
	{
		public UserDataService userDataService;
		public UserDataItem user;
		public MainViewController (IntPtr p) : base (p)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}



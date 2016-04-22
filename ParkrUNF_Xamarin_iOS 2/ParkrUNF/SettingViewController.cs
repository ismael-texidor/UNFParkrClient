using System;

using UIKit;

namespace ParkrUNF
{
	public partial class SettingViewController : UIViewController
	{
		public UserDataService userDataService;
		public UserDataItem user;
		public SettingViewController (IntPtr p) : base (p)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			grayLot.SetState(false, false);
			blueLot.SetState (true, true);
			facultyLot.SetState (false, false);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}



using System;
using UIKit;
using CoreLocation;

namespace ParkrUNF
{
	public partial class LotViewController : UIViewController
	{
		public static LocationManager Manager { get; set;}

		public LotViewController (IntPtr p) : base (p)
		{
			Manager = new LocationManager();
			Manager.startLocationUpdates();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CLLocation location = Manager.returnLocation();

			//CLLocation location = new CLLocation (30.26793078, -81.51236415);

			CLLocation lot14 = new CLLocation (30.26833386, -81.51331902);

			var distanceToLot14 = location.DistanceFrom (lot14);

			lotNumberLabel.Text = "Lot 14 " + distanceToLot14;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}



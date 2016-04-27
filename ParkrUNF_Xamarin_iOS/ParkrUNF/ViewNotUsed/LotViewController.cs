using System;
using System.Linq;
using UIKit;
using CoreLocation;
using System.Collections.Generic;
using System.Globalization;


namespace ParkrUNF
{
	public partial class LotViewController : UIViewController
	{
		public UserDataService userDataService;
		public Users user;
		public static LocationManager Manager { get; set;}

		public LotViewController (IntPtr p) : base (p)
		{
			Manager = new LocationManager();
			Manager.startLocationUpdates();
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			var checkTime = CheckTime ();

			if (checkTime)
			{
				var lotName = LocateLot ();

				var timeString = LocateTime ();

				var lotData = await userDataService.QueryByLotId (lotName);

				if (lotName == "lot14_1") 
				{
					lotImage.Image = UIImage.FromFile ("Images/lot14_1.png");

				} 
				else if (lotName == "lot14_2") 
				{
					lotImage.Image = UIImage.FromFile ("Images/lot14_1.png");
				} 
				else if (lotName == "lot1_1") 
				{
					lotImage.Image = UIImage.FromFile ("Images/lot1.png");
				}
				else if (lotName == "lot1_2") 
				{
					lotImage.Image = UIImage.FromFile ("Images/lot1.png");
				} 
				else if (lotName == "lot2_1") 
				{
					lotImage.Image = UIImage.FromFile ("Images/lot2.png");
				}
				else if (lotName == "lot4_1") 
				{
					lotImage.Image = UIImage.FromFile ("Images/lot4.png");
				}
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public string LocateLot()
		{
			CLLocation location = 
				Manager.returnLocation();

			if (location == null) {
				location = new CLLocation (30.272249, -81.508828);
			}

			CLLocation lot14_1 = new CLLocation (30.268352, -81.513324);

			CLLocation lot14_2 = new CLLocation (30.269416, -81.513954);

			//Art Center Lots
			CLLocation lot1_1 = new CLLocation (30.267611, -81.508983);

			CLLocation lot1_2 = new CLLocation (30.266722, -81.508586);

			//Library Lot
			CLLocation lot2 = new CLLocation (30.268751, -81.509305);

			//Student Union Lot
			CLLocation lot4 = new CLLocation (30.272128 , -81.508174);

			Dictionary<String, double> list = new Dictionary<String, double>();

			var distanceToLot14_1 = location.DistanceFrom (lot14_1);
			list.Add ("lot14_1", distanceToLot14_1);
			var distanceToLot14_2 = location.DistanceFrom (lot14_2);
			list.Add ("lot14_2", distanceToLot14_2);
			var distanceToLot1_1 = location.DistanceFrom (lot1_1);
			list.Add ("lot1_1", distanceToLot1_1);
			var distanceToLot1_2 = location.DistanceFrom (lot1_2);
			list.Add ("lot1_2", distanceToLot1_2);
			var distanceToLot2 = location.DistanceFrom (lot2);
			list.Add ("lot2_1", distanceToLot2);
			var distanceToLot4 = location.DistanceFrom (lot4);
			list.Add ("lot4_1", distanceToLot4);

			var min = list.Aggregate((l, r) => l.Value < r.Value ? l : r);
			return min.Key;
		}

		public string LocateTime()
		{
			var hour = DateTime.Now.Hour;
			var min = DateTime.Now.Minute;
			string timeString;

			if (min < 30) {
				timeString = "t" + hour + "_5";
			} else {
				timeString = "t" + hour;
			}
			return timeString;
		}

		public bool CheckTime ()
		{
			var dateTime = new DateTime ();
			var hour = dateTime.Hour;
			var amPm = dateTime.ToString("tt", CultureInfo.InvariantCulture);

			if (amPm == "am" && hour < 10) {
				return false;
			} else if (amPm == "pm" && hour > 6) {
				return false;
			}

			return true;
		}
	}
}



using System;
using System.Linq;
using UIKit;
using CoreLocation;
using System.Collections.Generic;
using System.Globalization;
using Foundation;
using System.Threading.Tasks;

namespace ParkrUNF
{
	public partial class MainViewController : UIViewController
	{
		public UserDataService userDataService;
		public Users user;

		public static LocationManager Manager { get; set;}

		public MainViewController (IntPtr p) : base (p)
		{
			Manager = new LocationManager();
			Manager.startLocationUpdates();
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

		async partial void checkOutClick (UIButton sender)
		{
			if (user == null)
			{
				var userName = "Hey";
				await getUser (userName);
				await userDataService.UpdateUserItem(user);
			}
			user.IsCheckedIn = false;

			checkOutText.Text = "Checked out!";
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "logOutSegue") 
			{
				var navViewController = segue.DestinationViewController as UINavigationController;


			}

			if (segue.Identifier == "checkInSegue") 
			{
				var lotView = segue.DestinationViewController as LotViewController;

				if (lotView != null) {
					lotView.userDataService = userDataService;
					lotView.user = user;
				}
			}

			if (segue.Identifier == "checkInSegue") 
			{
				var lotView = segue.DestinationViewController as LotViewController;

				if (lotView != null) {
					lotView.userDataService = userDataService;
					lotView.user = user;
				}
			}
		}

		public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
		{
			if (segueIdentifier == "checkInSegue") 
			{
				if (user == null) 
				{
					
				}
				if (user.IsCheckedIn == false) {
					return true;
				} 
				alredyCheckedIn.Text = "You've already checked in, please check out first!";
			}

			if (segueIdentifier == "logOutSegue") 
			{
				return true;
			}

			if (segueIdentifier == "settingsSegue") 
			{
				return true;
			}

			return false;
		}

		async partial void checkInClick (UIButton sender)
		{
			var checkTime = CheckTime ();

			//if (checkTime)
			if (true) //for testing.
			{
				if (user == null)
				{
				var userName = "Hey";
				await getUser (userName);
				}

				if (user.IsCheckedIn == false)
				{

				var lotName = LocateLot ();

				var timeString = LocateTime ();

				var lotData = await userDataService.QueryByLotId (lotName);

					if (lotData == null)
					{
						await userDataService.QueryByLotId ("lot_2");
					}

				checkIntoText.Text = "You've been checked into ...";
				if (lotName == "lot14_1") 
				{
					user.IsCheckedIn = true;
					lotImage.Image = UIImage.FromFile ("Images/lot14_1.png");

				} 
				else if (lotName == "lot14_2") 
				{
					user.IsCheckedIn = true;
					lotImage.Image = UIImage.FromFile ("Images/lot14_1.png");
				} 
				else if (lotName == "lot1_1") 
				{
					user.IsCheckedIn = true;
					lotImage.Image = UIImage.FromFile ("Images/lot1.png");
				}
				else if (lotName == "lot1_2") 
				{
					user.IsCheckedIn = true;
					lotImage.Image = UIImage.FromFile ("Images/lot1.png");
				} 
				else if (lotName == "lot2_1") 
				{
					user.IsCheckedIn = true;
					lotImage.Image = UIImage.FromFile ("Images/lot2.png");
				}
				else if (lotName == "lot4_1") 
				{
					user.IsCheckedIn = true;
					lotImage.Image = UIImage.FromFile ("Images/lot4.png");
				}

				await userDataService.UpdateUserItem(user);
				}
				else 
					alredyCheckedIn.Text = "You're already checked In!";
			}
		}

		private async Task getUser (string userName)
		{
			user = await gerUserAsync (userName);

		}
		private async Task<Users> gerUserAsync (string userName)
		{
			
			var user = await userDataService.QueryByUserName (userName);
			return user;
		}

		public string LocateLot()
		{
			CLLocation location = 
				Manager.returnLocation();

			if (location == null) {
				location = new CLLocation (30.268816 , -81.506794);
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

			switch (hour) {
			case 13:
				hour = 1;
				break;
			case 14:
				hour = 2;
				break;
			case 15:
				hour = 3;
				break;
			case 16:
				hour = 4;
				break;
			case 17:
				hour = 5;
				break;
			case 18:
				hour = 6;
				break;
			default:
				break;
			}

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
			var dateTime = DateTime.Now;
			var hour = dateTime.Hour;
			//var amPm = dateTime.ToString("tt", CultureInfo.InvariantCulture);

			if (hour < 10) {
				return false;
			} else if (hour > 18) {
				return false;
			}

			return true;
		}
	}

}



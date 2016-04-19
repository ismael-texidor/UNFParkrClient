using System;

using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ParkrUNF
{
	public partial class LoginViewController : UIViewController
	{
		private UserDataService userDataService;
		private UserDataItem userItem;
		public LoginViewController (IntPtr p) : base (p)
		{
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			userDataService = UserDataService.DefaultService;
			await userDataService.InitializeStoreAsync ();


			//this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle("loginBackground"));

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.

		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "registerSegue") {
				var registrationView = segue.DestinationViewController as RegisterViewController;

				if (registrationView != null) {
					registrationView.userDataService = userDataService;
				}
			}
			if (segue.Identifier == "loginSuccesfulSegue") 
			{
				var mainViewController = segue.DestinationViewController as MainViewController;

				if (mainViewController != null) {
					mainViewController.userDataService = userDataService;
					mainViewController.user = userItem;
				}
			}
			if (segue.Identifier == "bypassLoginSegue") 
			{
				var mainViewController = segue.DestinationViewController as MainViewController;

				if (mainViewController != null) {
					mainViewController.userDataService = userDataService;
					mainViewController.user = userItem;
				}
			}
		}
		public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
		{
			if (segueIdentifier == "loginSuccesfulSegue") {
				return true;

			} else if (segueIdentifier == "registerSegue") {

				return true;
			} else if (segueIdentifier == "bypassLoginSegue") 
			{
				return true;
			}
			else
			{
				return false;
			}

			//return base.ShouldPerformSegue (segueIdentifier, sender);
		}

		public void ShowAlert()
		{
			var alert = UIAlertController.Create("Incorrect Username/Password combination","",UIAlertControllerStyle.ActionSheet);
			// set up button event handlers
			alert.AddAction(UIAlertAction.Create("OK",UIAlertActionStyle.Default, null));
			// show it
			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController (alert, true, null);
		}

		async partial void loginClick (UIButton sender)
		{
			if (usernameText.Text.Length >0 && passwordHash.Text.Length > 0)
			{
				string userName = usernameText.Text;
				string enteredPassHash = passwordHash.Text;

				var user = await userDataService.QueryUserItem(userName);

				if (user == null)
				{
					ShowAlert();
				}
				else
				{
					var passed = retrivePasswordHash(enteredPassHash, user.PasswordHash);
					if(passed)
					{
						userItem = user;
						PerformSegue("loginSuccesfulSegue", sender);
					}
					else
					{
						ShowAlert();
					}
				}
			} 
			else 
			{
				ShowAlert();
			}
		}

		private bool retrivePasswordHash(string enteredPassHash, string passHash)
		{
			/* Fetch the stored value */
			string savedPasswordHash = passHash;
			/* Extract the bytes */
			byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
			/* Get the salt */
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			/* Compute the hash on the password the user entered */
			var pbkdf2 = new Rfc2898DeriveBytes(enteredPassHash, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);
			/* Compare the results */

			for (int i = 0; i < 20; i++)
				if (hashBytes [i + 16] != hash [i])
					return false;
			return true;
		}

		async partial void bypassLoginClick (UIButton sender)
		{
			var userName = "hello";
			var user = await userDataService.QueryUserItem(userName);
			userItem = user;
			PerformSegue("bypassLoginSegue", sender);
		}
	}
}



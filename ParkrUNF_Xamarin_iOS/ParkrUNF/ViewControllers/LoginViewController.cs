using System;

using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ParkrUNF
{
	public partial class LoginViewController : UIViewController
	{
		public UserDataService userDataService;
		public Users userItem;
		public bool shouldLogin = false;

		public LoginViewController (IntPtr p) : base (p)
		{
			this.userItem = userItem;
		}

		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//TODO: add better background image. 
			//load background Image <-- this works! 
			//this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/loginView.png"));


			// Perform any additional setup after loading the view, typically from a nib.
			userDataService = UserDataService.DefaultService;
			await userDataService.InitializeStoreAsync ();

			//refresh local store
			await userDataService.SyncAsync (true);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.

		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier == "registerSegue") 
			{
				var registrationView = segue.DestinationViewController as RegisterViewController;

				if (registrationView != null) {
					registrationView.userDataService = userDataService;
				}
			}
			if (segue.Identifier == "loginSuccesfulSegue") 
			{
				var mainView = segue.DestinationViewController as MainViewController;

				if (mainView != null) 
				{
					mainView.userDataService = userDataService;
					mainView.user = userItem;
				}

			}
			if (segue.Identifier == "bypassLoginSegue") 
			{
				
				var mainView = segue.DestinationViewController as MainViewController;

				if (mainView != null) 
				{
					mainView.userDataService = userDataService;
					mainView.user = userItem;
				}
			}
		}
		public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
		{
			if (segueIdentifier == "loginSuccesfulSegue") 
			{
				returnLoginClickBoolAsync ();
				return shouldLogin;

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
//			var alert = UIAlertController.Create("Incorrect Username/Password combination","",UIAlertControllerStyle.ActionSheet);
//			// set up button event handlers
//			alert.AddAction(UIAlertAction.Create("OK",UIAlertActionStyle.Default, null));
//			// show it
//			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController (alert, true, null);

			loginLabel.Text = "Incorrect Username/Password combination";
		}

		private async Task returnLoginClickBoolAsync ()
		{
			shouldLogin = await loginClick ();
		}

		private async Task<bool> loginClick ()
		{
			if (usernameText.Text.Length >0 && passwordHash.Text.Length > 0)
			{
				string userName = usernameText.Text;
				string enteredPassHash = passwordHash.Text;

				var user = await userDataService.QueryByUserName(userName);

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
						PerformSegue("loginSuccesfulSegue", this);
						return true;
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

			return false;
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
			var userName = "ish";
			var user = await userDataService.QueryByUserName(userName);
			this.userItem = user;
			PerformSegue("bypassLoginSegue", this);
		}
	}
}



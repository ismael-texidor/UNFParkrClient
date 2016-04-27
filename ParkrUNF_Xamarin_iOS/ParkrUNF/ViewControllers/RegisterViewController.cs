using System;

using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ParkrUNF
{
	public partial class RegisterViewController : UIViewController
	{
		public UserDataService userDataService;
		public RegisterViewController (IntPtr p) : base (p)
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

		public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
		{
			if (segueIdentifier == "registerComplete") 
			{		
				return true;
			}
			else
			{
				return false;
			}

			//return base.ShouldPerformSegue (segueIdentifier, sender);
		}
		public async Task<string> returnInsert(Users item)
		{
			return await userDataService.InsertUserItem (item);
		}
		public void ShowAlert(string msg)
		{
			var alert = UIAlertController.Create(msg,"",UIAlertControllerStyle.ActionSheet);
			// set up button event handlers
			alert.AddAction(UIAlertAction.Create("OK",UIAlertActionStyle.Default, null));
			// show it
			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController (alert, true, null);
		}

		async partial void registerClick (UIButton sender)
		{

			//validate form fields have data
			if (userName.Text.Length > 0 && passwordHash.Text.Length > 0 && firstName.Text.Length > 0 && lastName.Text.Length > 0) 
			{
				string pHash = createPasswordHash(passwordHash.Text);
				//create data object to send to db
				var userItem = new Users {
					UserName = userName.Text,
					PasswordHash = pHash,
					FirstName = firstName.Text,
					LastName = lastName.Text,
					IsCheckedIn = false,
					Password = passwordHash.Text,
					BluePass = false,
					GreyPass = false,
					FacultyPass = false
				};
				string insertSucessful = await returnInsert(userItem);

				if (insertSucessful.Equals("InsertSucessful"))
				PerformSegue("registerSegue", sender);
				else
				{
					ShowAlert(insertSucessful);
				}
			} else 
			{
				ShowAlert ("All fields must be completed");
			}
		}

		private string createPasswordHash (string password)
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			string savedPasswordHash = Convert.ToBase64String (hashBytes);

			return savedPasswordHash;
		}
	}
}



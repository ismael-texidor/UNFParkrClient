// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ParkrUNF
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField passwordHash { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField usernameText { get; set; }

		[Action ("bypassLoginClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void bypassLoginClick (UIButton sender);

		[Action ("loginClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void loginClick (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (passwordHash != null) {
				passwordHash.Dispose ();
				passwordHash = null;
			}
			if (usernameText != null) {
				usernameText.Dispose ();
				usernameText = null;
			}
		}
	}
}

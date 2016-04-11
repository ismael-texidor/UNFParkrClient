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
	[Register ("RegisterViewController")]
	partial class RegisterViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField firstName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField lastName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField passwordHash { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton registerButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField userName { get; set; }

		[Action ("registerClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void registerClick (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (firstName != null) {
				firstName.Dispose ();
				firstName = null;
			}
			if (lastName != null) {
				lastName.Dispose ();
				lastName = null;
			}
			if (passwordHash != null) {
				passwordHash.Dispose ();
				passwordHash = null;
			}
			if (registerButton != null) {
				registerButton.Dispose ();
				registerButton = null;
			}
			if (userName != null) {
				userName.Dispose ();
				userName = null;
			}
		}
	}
}

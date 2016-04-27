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
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel alredyCheckedIn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel checkIntoText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel checkOutText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView lotImage { get; set; }

		[Action ("checkInClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void checkInClick (UIButton sender);

		[Action ("checkOutClick:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void checkOutClick (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (alredyCheckedIn != null) {
				alredyCheckedIn.Dispose ();
				alredyCheckedIn = null;
			}
			if (checkIntoText != null) {
				checkIntoText.Dispose ();
				checkIntoText = null;
			}
			if (checkOutText != null) {
				checkOutText.Dispose ();
				checkOutText = null;
			}
			if (lotImage != null) {
				lotImage.Dispose ();
				lotImage = null;
			}
		}
	}
}

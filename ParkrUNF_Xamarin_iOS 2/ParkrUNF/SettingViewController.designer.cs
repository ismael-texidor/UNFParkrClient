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
	[Register ("SettingViewController")]
	partial class SettingViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch blueLot { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch facultyLot { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch grayLot { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (blueLot != null) {
				blueLot.Dispose ();
				blueLot = null;
			}
			if (facultyLot != null) {
				facultyLot.Dispose ();
				facultyLot = null;
			}
			if (grayLot != null) {
				grayLot.Dispose ();
				grayLot = null;
			}
		}
	}
}

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
	[Register ("TestView")]
	partial class TestView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField itemText { get; set; }

		[Action ("OnAdd:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnAdd (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (itemText != null) {
				itemText.Dispose ();
				itemText = null;
			}
		}
	}
}

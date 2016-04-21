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
	[Register ("LotViewController")]
	partial class LotViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lotNumberLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lotNumberLabel != null) {
				lotNumberLabel.Dispose ();
				lotNumberLabel = null;
			}
		}
	}
}

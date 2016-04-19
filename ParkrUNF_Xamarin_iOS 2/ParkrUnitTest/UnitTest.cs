using ParkrUNF;
using System;
using NUnit.Framework;

namespace ParkrUnitTest
{
	[TestFixture]
	public class UnitTest
	{
		[SetUp]
		public void SetUp()
		{
			
		}
		//Testing that QueryUserItem method in the dataService class returns the
		//correct UserDataItem (Class that contains properties to store the data.)
		[Test]
		public async void Pass ()
		{	
			//Arrange.
			//Arrange my expected result and set up my class
			var userDataQuery = new UserDataService();

			//GUID is the random ID used when the record is inserted. 
			//Random enough to never be the same.
			var expectedGuid = "a188944c-538c-4ca7-8377-23cbe65bb308";

			//Create my result boolean variable. Will switch to false
			//if we recieve the wrong data. 
			var result = true;

			//Act
			//Call the userData service and query the database for the username hello
			var userObject = await userDataQuery.QueryUserItem ("hello");
			var dbGuid = userObject.Id.ToString();

			//Assert
			//If the expectedGUID DOES NOT equal the db GUID
			//Change result to FALSE AND FAIL MY TEST!
			if (expectedGuid != dbGuid) {
				result = false;
			}


			Assert.True (result);
		}

		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}

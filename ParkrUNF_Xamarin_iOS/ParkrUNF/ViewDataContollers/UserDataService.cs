using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;


namespace ParkrUNF
{
	public class UserDataService 
	{
		static UserDataService instance = new UserDataService ();

		const string applicationURL = @"https://parkrunf.azurewebsites.net";
		const string localDbPath    = "localstore.db";

		private MobileServiceClient client;
		private IMobileServiceSyncTable<Users> userTable;
		private IMobileServiceSyncTable<LotData> lotData;
		private IMobileServiceSyncTable<UserDataItem> userDataTable;
		private IMobileServiceSyncTable<ToDoItem> todoTable;

		public UserDataService ()
		{
			CurrentPlatform.Init ();
			SQLitePCL.CurrentPlatform.Init(); 

			// Initialize the Mobile Service client with the Mobile App URL, Gateway URL and key
			client = new MobileServiceClient (applicationURL);

			// Create an MSTable instance to allow us to work with the TodoItem table
			userTable = client.GetSyncTable <Users> ();
			lotData = client.GetSyncTable <LotData> ();
			userDataTable = client.GetSyncTable<UserDataItem> ();
			todoTable = client.GetSyncTable<ToDoItem> ();


		}

		public static UserDataService DefaultService {
			get {
				return instance;
			}
		}

		public List<UserDataService> Items { get; private set;}

		public async Task InitializeStoreAsync()
		{
			var store = new MobileServiceSQLiteStoreWithLogging(localDbPath, true, true);

			store.DefineTable<Users>();
			store.DefineTable<LotData>();
			store.DefineTable<UserDataItem>();
			store.DefineTable<ToDoItem> ();

			var syncHandler = new MySyncHandler(client, this);
			// Uses the default conflict handler, which fails on conflict
			// To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
			await client.SyncContext.InitializeAsync(store, syncHandler);
			//await userTable.PurgeAsync (true);
			//await lotData.PurgeAsync(true);

				
		}
		public async Task SyncAsync(bool pullData = false)
		{
			try
			{
				await client.SyncContext.PushAsync();

				if (pullData) {
					await userTable.PullAsync("getUsers", userTable.CreateQuery()); // query ID is used for incremental sync
					await lotData.PullAsync("getLots", lotData.CreateQuery());
				}
			}

			catch (MobileServiceInvalidOperationException e)
			{
				Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}

		public async Task<string> InsertUserItem (Users userItem)
		{
			try { 

				await userTable.InsertAsync (userItem); // Insert a new UserItem into the local database. 
				await SyncAsync(); // send changes to the mobile service 
				return "InsertSucessful";
			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
			return "fail";
		}

		public async Task<Users> QueryByUserName (string userName)
		{
			try {     
				
				var findUser = await userTable.
					Where(userFields => userFields.UserName == userName).ToListAsync();
				
				if(findUser.Count == 0)
				{
					return null;
				}
				//return the first user
				else 
				{
					return findUser[0];
				}

			} 
			catch (MobileServiceInvalidOperationException e) 
			{
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
			return null;
		}

		public async Task UpdateUserItem (Users item)
		{
			try {
				await userTable.UpdateAsync (item); // update todo item in the local database
				await SyncAsync(); // send changes to the mobile service



			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}

		public async Task<LotData> QueryByLotId (string lotId)
		{
			try {                
				var findLot = await lotData.
					Where(u => u.lotId == lotId).ToListAsync();
				if(findLot.Count == 0)
				{
					return null;
				}
				//return the first lot
				else 
				{
					return findLot[0];
				}

			} 
			catch (MobileServiceInvalidOperationException e) 
			{
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
			return null;
		}

		public async Task UpdateLotItem (LotData item)
		{
			try {
				await lotData.UpdateAsync (item); // update todo item in the local database
				await SyncAsync(); // send changes to the mobile service



			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}
	}
}


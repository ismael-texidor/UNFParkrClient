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
		private IMobileServiceSyncTable<UserDataItem> userTable;

		private UserDataService ()
		{
			CurrentPlatform.Init ();
			SQLitePCL.CurrentPlatform.Init(); 

			// Initialize the Mobile Service client with the Mobile App URL, Gateway URL and key
			client = new MobileServiceClient (applicationURL);

			// Create an MSTable instance to allow us to work with the TodoItem table
			userTable = client.GetSyncTable <UserDataItem> ();
		}

		public static UserDataService DefaultService {
			get {
				return instance;
			}
		}

		public List<UserDataItem> Items { get; private set;}

		public async Task InitializeStoreAsync()
		{
			var store = new MobileServiceSQLiteStore(localDbPath);
			store.DefineTable<UserDataItem>();

			// Uses the default conflict handler, which fails on conflict
			// To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
			await client.SyncContext.InitializeAsync(store);
		}

		public async Task SyncAsync(bool pullData = false)
		{
			try
			{
				await client.SyncContext.PushAsync();

				if (pullData) {
					await userTable.PullAsync("allTodoItems", userTable.CreateQuery()); // query ID is used for incremental sync
				}
			}

			catch (MobileServiceInvalidOperationException e)
			{
				Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}

		public async Task<List<UserDataItem>> RefreshDataAsync ()
		{
			try {
				// update the local store
				// all operations on todoTable use the local database, call SyncAsync to send changes
				await SyncAsync(pullData: true); 							

				// This code refreshes the entries in the list view by querying the local TodoItems table.
				// The query excludes completed TodoItems
				Items = await userTable
					.Where (todoItem => todoItem.Complete == false).ToListAsync ();

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
				return null;
			}

			return Items;
		}

		public async Task InsertTodoItemAsync (UserDataItem todoItem)
		{
			try {                
				await userTable.InsertAsync (todoItem); // Insert a new TodoItem into the local database. 
				await SyncAsync(); // send changes to the mobile service

				Items.Add (todoItem); 

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}

		public async Task CompleteItemAsync (UserDataItem item)
		{
			try {
				item.Complete = true; 
				await userTable.UpdateAsync (item); // update todo item in the local database
				await SyncAsync(); // send changes to the mobile service

				Items.Remove (item);

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}

		public async Task<string> InsertUserItem (UserDataItem userItem)
		{
			try { 
				//check to see if the userName exist
				var findUser = await userTable.
					Where(u => u.Username == userItem.Username).ToListAsync();
				if(findUser.Count == 0)
				{
					
					await userTable.InsertAsync (userItem); // Insert a new UserItem into the local database. 
					await SyncAsync(); // send changes to the mobile service 
					return "InsertSucessful";
				}
				else
				{
					return "This username already exist";
				}


			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
			return "This Username already exist";
		}

		public async Task<UserDataItem> QueryUserItem (string userName)
		{
			try {                
				var findUser = await userTable.
					Where(u => u.Username == userName).ToListAsync();
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
	}
}


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
	public class LotDataService 
	{
		static LotDataService instance = new LotDataService ();

		const string applicationURL = @"https://parkrunf.azurewebsites.net";
		const string localDbPath    = "localstore.db";

		private MobileServiceClient client;
		private IMobileServiceSyncTable<LotData> lotDataTable;

		public LotDataService ()
		{
			CurrentPlatform.Init ();
			SQLitePCL.CurrentPlatform.Init(); 

			// Initialize the Mobile Service client with the Mobile App URL, Gateway URL and key
			client = new MobileServiceClient (applicationURL);

			// Create an MSTable instance to allow us to work with the TodoItem table
			lotDataTable = client.GetSyncTable <LotData> ();
		}

		public static LotDataService DefaultService {
			get {
				return instance;
			}
		}

		public List<LotData> Items { get; private set;}

		public async Task InitializeStoreAsync()
		{
			var store = new MobileServiceSQLiteStore(localDbPath);
			store.DefineTable<LotData>();

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
					await lotDataTable.PullAsync("allLotItems", lotDataTable.CreateQuery()); // query ID is used for incremental sync
				}
			}

			catch (MobileServiceInvalidOperationException e)
			{
				Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}

		public async Task<List<LotData>> RefreshDataAsync ()
		{
			try {
				// update the local store
				// all operations on todoTable use the local database, call SyncAsync to send changes
				await SyncAsync(pullData: true); 							

				Items = await lotDataTable
					.Where (todoItem => todoItem.Deleted == false).ToListAsync ();

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
				return null;
			}

			return Items;
		}

		public async Task InsertLotItem (LotData lotItem)
		{
			try { 
				//Insert Lot

					await lotDataTable.InsertAsync (lotItem); // Insert a new UserItem into the local database. 
					await SyncAsync(); // send changes to the mobile service 

			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}

		public async Task<LotData> QueryByLotId (string lotId)
		{
			try {                
				var findUser = await lotDataTable.
					Where(u => u.lotId == lotId).ToListAsync();
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

		public async Task CompleteItemAsync (LotData item)
		{
			try {
				await lotDataTable.UpdateAsync (item); // update todo item in the local database
				await SyncAsync(); // send changes to the mobile service



			} catch (MobileServiceInvalidOperationException e) {
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
			}
		}
	}
}


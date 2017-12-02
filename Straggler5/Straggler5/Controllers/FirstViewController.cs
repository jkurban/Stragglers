using System;

using UIKit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Intents;
using System.Diagnostics;
using Microsoft.Azure.Documents;
using CoreImage;

namespace Straggler5
{
    public partial class FirstViewController : UIViewController
    {
        const string accountURL = @"https://pichelper.documents.azure.com:443/";
        const string accountKey = @"pKlxnxoMWdiTwynNp3rg344C4z1mvqFDu8wTob1rbfZoezDPz5fFOeDHNXOIP5dAfBJU8gH8e6XI51Upg4151g==";
        const string databaseId = @"KeyAttributes";
        const string collectionId = @"keyattributes";

        private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

        private DocumentClient client;

        public List<PiAttribute> Items { get; private set; }

        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            // InitCosmos();
            GetCosmosDataAsync();
        }

        public async void InitCosmos()
        {
            try
            {
                DocumentCollection collection = new DocumentCollection { Id = "KeyAttributes" };
                collection.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });
                collection.IndexingPolicy.IndexingMode = IndexingMode.Consistent;
                await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri("db"), collection);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        public async void GetCosmosDataAsync()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);

            try
            {
                // The query excludes completed TodoItems
                var query = client.CreateDocumentQuery<PiAttribute>(collectionLink, new FeedOptions { MaxItemCount = 10 })
                                  .Where(todoItem => todoItem.Id != null)
                      .AsDocumentQuery();

                Items = new List<PiAttribute>();
                while (query.HasMoreResults)
                {
                    Items.AddRange(await query.ExecuteNextAsync<PiAttribute>());
                }

                String pressureColor = Items[0].Measures[1].Status; // grab pressure
                switch (pressureColor)
                {
                    case "red":
                        PressureViewHandle.BackgroundColor = UIColor.Red;
                        break;
                    case "green":
                        PressureViewHandle.BackgroundColor = UIColor.Green;
                        break;
                    case "yellow":
                        PressureViewHandle.BackgroundColor = UIColor.Yellow;
                        break;
                    case "gray":
                        PressureViewHandle.BackgroundColor = UIColor.Gray;
                        break;
                    default:
                        PressureViewHandle.BackgroundColor = UIColor.Red;
                        break;
                }
                pressureValue.Text = Items[0].Measures[1].Value;

                String sandColor = Items[0].Measures[3].Status; // grab pressure
                switch (sandColor)
                {
                    case "red":
                        SandViewHandle.BackgroundColor = UIColor.Red;
                        break;
                    case "green":
                        SandViewHandle.BackgroundColor = UIColor.Green;
                        break;
                    case "yellow":
                        SandViewHandle.BackgroundColor = UIColor.Yellow;
                        break;
                    case "gray":
                        SandViewHandle.BackgroundColor = UIColor.Gray;
                        break;
                    default:
                        SandViewHandle.BackgroundColor = UIColor.Red;
                        break;
                }
                sandValue.Text = Items[0].Measures[3].Value;


                Debug.WriteLine($"items count: {Items.Count()}");
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

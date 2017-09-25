using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Net;
using System.Web;

namespace DocumentDBFrontEnd
{

    public static class DocumentDBRepository<T> where T : class
    {
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
        private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];
        private static DocumentClient client;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> orderPredicate, int maxCount)
        {
            FeedOptions options = new FeedOptions { MaxItemCount = 500 };
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), options, "CustomerName")
                .Where(predicate).OrderByDescending(orderPredicate).Take(maxCount)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            
            while (query.HasMoreResults && results.Count < maxCount)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public static async Task<IEnumerable<T>> GetItemsAsyncAll(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> orderPredicate)
        {
            try
            {
                FeedOptions options = new FeedOptions { MaxItemCount = 500 };
                //FeedOptions options = new FeedOptions { MaxItemCount = 500, PartitionKey = new PartitionKey("CustomerName") };
                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), options, "CustomerName")
                    .Where(predicate).OrderByDescending(orderPredicate)
                    .AsDocumentQuery();

                List<T> results = new List<T>();
                
                while (query.HasMoreResults)
                {
                    results = new List<T>();
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                int count = results.Count;
                return results;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
    }
}
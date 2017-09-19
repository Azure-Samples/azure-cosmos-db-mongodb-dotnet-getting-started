using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTaskListApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using System.Security.Authentication;

namespace MyTaskListApp
{
    public class Dal : IDisposable
    {
        private bool disposed = false;

        //private MongoClient
        private readonly MongoClient client;

        //Copy connection string from Azure portal
        //Go to Quick Start pane, Choose .NET platform
        //Copy the connection string provided there below
        private readonly string connectionString = @"";

        //ReadPreference setting
        //For LOAD DISTRIBUTION SCENARIO
        //we set this to SecondaryPreferred
        //The idea is to use secondaries for Read loads
        //And Primary/Write region is only used for Write requests
        //If there are no Read regions configured for the account
        //then read load also goes to Write region.
        private readonly ReadPreference readPreference = ReadPreference.SecondaryPreferred;

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private readonly string dbName = "Tasks";
        private readonly string collectionName = "TasksList";

        // Default constructor.        
        public Dal()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(this.connectionString));
            settings.SslSettings =
                new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            //Initialize the client once and reuse it for all requests
            this.client = new MongoClient(settings);
        }

        // Gets all Task items from the MongoDB server.        
        public List<MyTask> GetAllTasks()
        {
            try
            {
                var collection = this.GetTasksCollection();
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (MongoConnectionException)
            {
                return new List<MyTask>();
            }
        }

        // Creates a Task and inserts it into the collection in MongoDB.
        public void CreateTask(MyTask task)
        {
            var collection = this.GetTasksCollection();
            try
            {
                collection.InsertOne(task);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        private IMongoCollection<MyTask> GetTasksCollection()
        {
            var database = this.client.GetDatabase(this.dbName);
            var todoTaskCollection = database.GetCollection<MyTask>(this.collectionName).WithReadPreference(this.readPreference);
            return todoTaskCollection;
        }

        # region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }

            this.disposed = true;
        }

        # endregion
    }
}
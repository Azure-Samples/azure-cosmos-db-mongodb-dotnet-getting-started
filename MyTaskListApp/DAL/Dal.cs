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
        //private MongoServer mongoServer = null;
        private bool disposed = false;

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net
        private string userName = "pp-mongo-test";
        private string host = "pp-mongo-test.documents.azure.com";
        private string password = "AV52QlGiD5xfLP6TqNwik8rI3EvBbzCDW5y9QUahoY4JXmaSNaXEP6Jt9463AVDH3rcBaXvHttyh9ZXF8VkVpw==";

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private string dbName = "Tasks";
        private string collectionName = "TasksList";

        // Default constructor.        
        public Dal()
        {
        }

        // Gets all Task items from the MongoDB server.        
        public List<MyTask> GetAllTasks()
        {
            try
            {
                var collection = GetTasksCollection();
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (MongoConnectionException)
            {
                return new List<MyTask>();
            }
        }

        public void DeleteTask(Guid id)
        {
            var collection = GetTasksCollectionForEdit();

            collection.FindOneAndDelete(t => t.Id == id);
        }

        public void UpdateTask(MyTask task)
        {
            var collection = GetTasksCollectionForEdit();

            collection.FindOneAndReplace(t => t.Id == task.Id, task);
        }
        
        // Creates a Task and inserts it into the collection in MongoDB.
        public void CreateTask(MyTask task)
        {
            var collection = GetTasksCollectionForEdit();
            try
            {
                collection.InsertOne(task);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        public MyTask GetTask(Guid id)
        {
            return GetTasksCollection().FindSync(t => t.Id == id).FirstOrDefault();
        }

        private IMongoCollection<MyTask> GetTasksCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<MyTask>(collectionName);
            return todoTaskCollection;
        }

        private IMongoCollection<MyTask> GetTasksCollectionForEdit()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };
            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<MyTask>(collectionName);
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
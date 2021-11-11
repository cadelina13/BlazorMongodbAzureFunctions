using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShared
{
    public class MongoDatabase
    {
        private readonly static string CONSTRING = "mongodb+srv://user1:User12345678@cluster0.r9dp8.mongodb.net/test";
        private readonly static string DBNAME = "MongodbSample";
        private IMongoDatabase db;
        public MongoDatabase()
        {
            var client = new MongoClient(new MongoUrl(CONSTRING));
            //var client = new MongoClient();
            db = client.GetDatabase(DBNAME);
        }
        public void InsertRecord<T>(T record)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            collection.InsertOne(record);
        }

        public List<T> LoadRecord<T>()
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            return collection.Find(new BsonDocument()).ToList();
        }
        public T FindRecordById<T>(Guid id)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var f = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(f).First();
        }
        public void UpdateRecord<T>(Guid id, T record)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var rs = collection.ReplaceOne(new BsonDocument("_id", id), record, new UpdateOptions { IsUpsert = true });
        }
        public void DeleteRecord<T>(Guid id)
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var f = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(f);
        }
    }
}

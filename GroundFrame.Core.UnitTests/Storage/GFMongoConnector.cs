using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GroundFrame.Core.UnitTests.Storage
{
    public class GFMongoConnector
    {
        #region Methods

        /// <summary>
        /// Checks connecting to a local MongoDB database with servername and port  
        /// </summary>
        [Fact]
        public async Task GFMongoConnector_Constructor_ServerPort()
        {
            GroundFrame.Core.GFMongoConnector MongoConnector = new Core.GFMongoConnector("localhost", "27017");
            IMongoDatabase db = MongoConnector.MongoClient.GetDatabase("groundframeControl");
            IMongoCollection < BsonDocument> collection = db.GetCollection<BsonDocument>("environments");
            string TestJSON = @"{
            ""environment"": ""localhost"",
            ""config"":{
                ""gfSqlServer"": ""(localdb)\\MSSQLLocalDB"",
                ""gfDbName"": ""GroundFrame.SQL""
                }
            }";

            using IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument());
            while (await cursor.MoveNextAsync())
            {
                IEnumerable<BsonDocument> batch = cursor.Current;
                foreach (BsonDocument document in batch)
                {
                    string Test = JsonConvert.SerializeObject(BsonTypeMapper.MapToDotNetValue(document));
                    Console.WriteLine(Test);
                    Console.WriteLine();
                }
            }

        }

        #endregion Methods
    }
}

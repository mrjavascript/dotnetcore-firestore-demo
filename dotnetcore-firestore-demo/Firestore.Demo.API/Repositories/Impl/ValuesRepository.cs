using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firestore.Demo.API.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using Serilog;

namespace Firestore.Demo.API.Repositories.Impl
{
    public class ValuesRepository : IValuesRepository
    {
        private const string PROJECT_ID = "miketest-409fc";
        private readonly FirestoreDb _db;

        public ValuesRepository()
        {
            var credential = GoogleCredential
                .FromFile(Path.Combine(AppContext.BaseDirectory + "/google.json"));
            var channelCredentials = credential.ToChannelCredentials();
            var channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            var firestoreClient = FirestoreClient.Create(channel);
            _db = FirestoreDb.Create(PROJECT_ID, client: firestoreClient);
            Log.Logger.Information("Created Firestore connection");
        }

        public async Task<IEnumerable<Cafe>> GetValues()
        {
            var usersRef = _db.Collection("cafes");
            var snapshot = await usersRef.GetSnapshotAsync();

            return snapshot.Documents.Select(document => document.ToDictionary()).Select(dictionary => new Cafe {Name = dictionary["name"].ToString(), City = dictionary["city"].ToString()}).ToList();
        }

        public async Task CreateValue(Cafe value)
        {
            var docRef = _db.Collection("cafes").Document();
            var user = new Dictionary<string, object>
            {
                { "name", value.Name },
                { "city", value.City }
            };
            await docRef.SetAsync(user);
        }
    }
}
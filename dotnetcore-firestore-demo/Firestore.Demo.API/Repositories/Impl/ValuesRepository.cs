using System;
using System.Collections.Generic;
using System.IO;
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
                .FromFile(Path.Combine(AppContext.BaseDirectory + "/miketest-2d444baa8876.json"));
            var channelCredentials = credential.ToChannelCredentials();
            var channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            var firestoreClient = FirestoreClient.Create(channel);
            _db = FirestoreDb.Create(PROJECT_ID, client: firestoreClient);
            Log.Logger.Information("Created Firestore connection");
        }

        public async Task<IEnumerable<Cafe>> GetValues()
        {
            var result = new List<Cafe>();
            var usersRef = _db.Collection("cafes");
            var snapshot = await usersRef.GetSnapshotAsync();
            foreach (var document in snapshot.Documents)
            {
//                Console.WriteLine("User: {0}", document.Id);
//                Dictionary<string, object> documentDictionary = document.ToDictionary();
//                Console.WriteLine("First: {0}", documentDictionary["First"]);
//                if (documentDictionary.ContainsKey("Middle"))
//                {
//                    Console.WriteLine("Middle: {0}", documentDictionary["Middle"]);
//                }
//
//                Console.WriteLine("Last: {0}", documentDictionary["Last"]);
//                Console.WriteLine("Born: {0}", documentDictionary["Born"]);
//                Console.WriteLine();
                var dictionary = document.ToDictionary();
                result.Add(new Cafe
                {
                    Name = dictionary["name"].ToString(),
                    City = dictionary["city"].ToString()
                });
            }

            return result;
        }

        public Task CreateValue(Cafe value)
        {
            throw new NotImplementedException();
        }
    }
}
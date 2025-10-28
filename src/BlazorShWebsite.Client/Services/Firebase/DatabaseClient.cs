// namespace BlazorShWebsite.Client.Services.Firebase;
//
// public class DatabaseClient(FirestoreDb db)
// {
//     private async Task DoWork(CancellationToken stoppingToken)
//     {
//         var blogCollection = db.Collection(BlogCollectionNames.Posts);
//         if ((await blogCollection.GetSnapshotAsync(stoppingToken)).Documents.Count == 0)
//         {
//             var blogData = await File.ReadAllTextAsync("Data/posts.json", stoppingToken);
//             var blog = await JsonSerializer.DeserializeAsync<Blog>(new MemoryStream(Encoding.UTF8.GetBytes(blogData)),
//                 cancellationToken: stoppingToken);
//             logger.LogInformation("Found {postCount} posts", blog.posts.Count);
//             foreach (var post in blog.posts)
//             {
//                 await blogCollection.AddAsync(post, stoppingToken);
//             }
//
//             logger.LogInformation("Posts added to firestore");
//         }
//         else
//         {
//             logger.LogInformation("Posts not added to firestore because they already exist");
//         }
//
//         var metaCollection = db.Collection(BlogCollectionNames.Metadata);
//         if ((await metaCollection.GetSnapshotAsync(stoppingToken)).Documents.Count == 0)
//         {
//             var metadataData = await File.ReadAllTextAsync("Data/metadata.json", stoppingToken);
//             var metadata = await JsonSerializer.DeserializeAsync<Metadata>(
//                 new MemoryStream(Encoding.UTF8.GetBytes(metadataData)), cancellationToken: stoppingToken);
//
//             await metaCollection.AddAsync(metadata, stoppingToken);
//             logger.LogInformation("Metadata added to firestore");
//         }
//         else
//         {
//             logger.LogInformation("Metadata not added to firestore because it already exist");
//         }
//     }
//     
//     public async Task<List<Post>> GetNewest(int quantity, int page)
//     {
//         var newPage = Math.Max(1, page); 
//         var collection = db.Collection(BlogCollectionNames.Posts);
//         var query = await OnceAfterOneSecond(
//             async () => await collection.OrderByDescending("date").Offset((newPage - 1) * quantity).Limit(quantity).GetSnapshotAsync()
//         );
//         return query.Documents.Select(doc => doc.ConvertTo<Post>()).ToList();
//     }
//
//     public async Task<Post?> GetPost(string id)
//     {
//         var collection = db.Collection(BlogCollectionNames.Posts);
//         return await OnceAfterOneSecond(
//             async () => (await collection.GetSnapshotAsync()).Documents.FirstOrDefault(doc => doc.GetValue<string>("id") == id)?.ConvertTo<Post>()
//         );
//     }
// }
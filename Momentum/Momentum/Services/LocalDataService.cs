using Momentum.Platform;
using System.Collections.Generic;
using System.Threading.Tasks;
using PCLStorage;
using System.IO;
using SQLite;
using Momentum.Models;
using Momentum.Extensions;
using System.Linq;
using Momentum.Models.Sql;

namespace Momentum.Services
{
    public class LocalDataService
    {
        private static LocalDataService instance;
        private SQLiteAsyncConnection database;
        private IFolder folder;

        public static LocalDataService Instance
        {
            get { return instance ?? (instance = new LocalDataService()); }
        }

        private LocalDataService()
        {
            
        }

        public async Task<SQLiteAsyncConnection> GetDatabaseAsync()
        {
            if (database == null)
            {
                database = Database.Instance.GetAsyncConnection();
                await database.CreateTableAsync<SentMoment>();
            }

            return database;
        }

        public async Task<IFolder> GetMomentFolderAsync()
        {
            if(folder == null)
            {
                var path = File.Instance.GetPicturesFolderPath("Moments");
                folder = await FileSystem.Current.GetFolderFromPathAsync(path);
            }

            return folder;
        }

        public async Task SaveMomentAsync(Moment moment, byte[] momentImage)
        {
            var localMoment = new SentMoment();
            moment.MoveCorresponding<IMoment>(localMoment);

            var database = await GetDatabaseAsync();
            await database.InsertAsync(localMoment);

            var folder = await GetMomentFolderAsync();
            var file = await folder.CreateFileAsync(moment.MomentId + ".jpg", CreationCollisionOption.OpenIfExists);

            using (Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.Write(momentImage, 0, momentImage.Length);
            }

            File.Instance.NotifyMediaScanner(file.Path);
        }

        public async Task<IEnumerable<SentMoment>> GetSentMomentsAsync()
        {
            var database = await GetDatabaseAsync();
            var moments = await database.Table<SentMoment>().ToListAsync();

            var folder = await GetMomentFolderAsync();
            var files = await folder.GetFilesAsync();
            //var images = new List<string>(files.Count);

            foreach (var moment in moments)
            {
                moment.ImageSource = files.Where(f => f.Name.StartsWith(moment.MomentId)).FirstOrDefault()?.Path;
            }

            return moments;
        }

        public async Task<IEnumerable<string>> GetAllMomentImages()
        {
            var folder = await GetMomentFolderAsync();
            var files = await folder.GetFilesAsync();
            var momentImages = new List<string>(files.Count);

            foreach (var file in files)
            {
                momentImages.Add(file.Path);
            }

            return momentImages;
        }
    }
}

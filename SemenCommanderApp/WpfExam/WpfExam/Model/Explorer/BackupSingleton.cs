using Caliburn.Micro;
using Newtonsoft.Json;
using SemenCommanderApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfExam.Model.Exporer;

namespace WpfExam.Model
{
    public class BackupSingleton : PropertyChangedBase
    {
        // Singleton
        static BackupSingleton instance;
        private BackupSingleton()
        {
            LoadFiles();
        }
        public static BackupSingleton Instance => instance ?? (instance = new BackupSingleton());
        public static BackupSingleton getInstance()
        {
            if (instance == null)
                instance = new BackupSingleton();
            return instance;
        }
        //

        private ObservableCollection<IExplorerItem> content = new ObservableCollection<IExplorerItem>();
        public ObservableCollection<IExplorerItem> Content
        {
            set
            {
                content = value;
                NotifyOfPropertyChange();
            }
            get => content;
        }
        private ObservableCollection<ExplorerItemGroup> sortedContent = new ObservableCollection<ExplorerItemGroup>();
        public ObservableCollection<ExplorerItemGroup> SortedContent
        {
            set
            {
                sortedContent = value;
                NotifyOfPropertyChange();
            }
            get => sortedContent;
        }

        private int margin = 5;
        public string Margin
        {
            get => $"{margin},5,{margin},2";
            set
            {
                margin = Convert.ToInt32(value);
                NotifyOfPropertyChange();
            }
        }
        private int windowWidth = 800;
        private int windowHeight = 450;

        public async void LoadFiles()
        {
            var files = await GetBackupFilesAsync();

            Content = new ObservableCollection<IExplorerItem>(files.Select(f => (IExplorerItem)f));

            Sort(windowWidth, windowHeight);
        }
        private async Task<List<ExplorerFile>> GetBackupFilesAsync()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://localhost:44340/backup/{UserSingleton.Instance.UserId}/all");

            var filesWe = JsonConvert.DeserializeObject<BackupFileWe[]>(result);
            var files = filesWe.Select(f => new ExplorerFile(f));

            return files.ToList();
        }
        public async Task LoadFile(string fileId)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://localhost:44340/backup/{UserSingleton.Instance.UserId}/{fileId}");
            var fileWe = JsonConvert.DeserializeObject<FileWe>(result);
            if (!File.Exists(ExplorerSingleton.Instance.LastPath + "\\" + fileWe.Name))
            {
                var file = File.Create(ExplorerSingleton.Instance.LastPath + "\\" + fileWe.Name);
                file.Write(fileWe.Content, 0, fileWe.Content.Length);
            }

            ExplorerSingleton.Instance.LoadDirectory(ExplorerSingleton.Instance.LastPath, false);
        }

        public async Task<string> DeleteFile(string fileId)
        {
            var client = new HttpClient();
            var result = await client.DeleteAsync($"https://localhost:44340/backup/{UserSingleton.Instance.UserId}/{fileId}");
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> BackupFile(ExplorerFile file)
        {
            var client = new HttpClient();
            using (var fileStreamContent = new StreamContent(File.OpenRead(file.Path)))
            {
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    multipartFormContent.Add(fileStreamContent, name: "file", fileName: file.Name);

                    var response = await client.PostAsync($"https://localhost:44340/backup/{UserSingleton.Instance.UserId}", multipartFormContent);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    return result;
                }
            }
        }


        public void Sort(int width, int height)
        {
            windowHeight = height;
            windowWidth = width;

            SortedContent.Clear();
            ObservableCollection<ExplorerItemGroup> items = new ObservableCollection<ExplorerItemGroup>();
            int rowSize = (width / (110));
            int margin = 5 + (width - rowSize * (110)) / rowSize / 2;
            Margin = margin.ToString();

            items.Add(new ExplorerItemGroup());
            int j = 0;
            int i = 0;
            while (i < Content.Count)
            {
                items[items.Count - 1].Group.Add(Content[i]);
                j++;

                if (j % rowSize == 0)
                    items.Add(new ExplorerItemGroup());
                i++;
            }
            SortedContent = items;
        }
    }
}

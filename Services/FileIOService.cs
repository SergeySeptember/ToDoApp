using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    class FileIOService
    {
        private readonly string _filePath;

        public FileIOService(string path)
        {
            _filePath = path;
        }
        public BindingList<ToDoModel> LoadData()
        {
            var fileExists = File.Exists(_filePath);
            if (!fileExists)
            {
                File.CreateText(_filePath).Dispose();
                return new BindingList<ToDoModel>();
            }
            using (var reader = File.OpenText(_filePath))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public void SaveData(object ToDoDataList)
        {
            using (StreamWriter writer = File.CreateText(_filePath))
            {
                string output = JsonConvert.SerializeObject(ToDoDataList);
                writer.Write(output);
            }
        }
    }
}

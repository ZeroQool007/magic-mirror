﻿using Acme.Generic;
using MagicMirror.Business.Models;
using Newtonsoft.Json;
using System;

namespace MagicMirror.Business.Services
{
    public class FileWriterService : IFileWriterService
    {
        public UserSettings ReadSettings(string folder, string fileName)
        {
            string json = FileWriter.ReadFromFile(folder, fileName);
            var result = JsonConvert.DeserializeObject<UserSettings>(json);

            return result;
        }

        public void SaveSettings(string path, string USERSETTINGS, UserSettings settings)
        {
            string json = settings.ToJson();
            FileWriter.WriteJsonToFile(json, USERSETTINGS, path);
        }
    }
}
﻿using Acme.Generic;
using MagicMirror.Business.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicMirror.Business.Services
{
    public class SettingsService : ISettingsService
    {

        public UserSettings ReadSettings(string folder, string fileName)
        {
            string json = FileWriter.ReadFromFile(folder, fileName);
            var result = JsonConvert.DeserializeObject<UserSettings>(json);

            return result;
        }

        public void SaveSettings(string path, string USERSETTINGS, UserSettings settings)
        {
            string json = JsonConvert.SerializeObject(settings);
            FileWriter.WriteJsonToFile(json, USERSETTINGS, path);
        }
    }
}
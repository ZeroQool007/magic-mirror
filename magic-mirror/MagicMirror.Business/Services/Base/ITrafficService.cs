﻿using System.Threading.Tasks;
using MagicMirror.Business.Models;

namespace MagicMirror.Business.Services
{
    public interface ITrafficService
    {
        Task<TrafficModel> GetModelAsync(string homeAddress, string homeCity, string workAddress, DistanceUOM distanceUOM = DistanceUOM.Metric);

        TrafficModel GetOfflineModel(string path);

        void SaveOfflineModel(TrafficModel model, string path);
    }
}
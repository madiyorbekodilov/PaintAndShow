﻿using Newtonsoft.Json;

namespace PaintAndShow.Service.Helpers;

public class PathHelper
{
    public static string WebRootPath { get; set; }
    [JsonProperty("CountryFilePaths")]
    public static string CountryPath { get; set; }
    [JsonProperty("RegionFilePaths")]
    public static string RegionPath { get; set; }
    [JsonProperty("DictrictsFilePaths")]
    public static string DistrictPath { get; set; }
}
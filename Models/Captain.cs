﻿using System.Text.Json.Serialization;

namespace project_new.Models
{
    public class Captain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Team>? Teams { get; set; }
    }
}

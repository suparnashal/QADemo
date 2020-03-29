using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace APITesting.API.DropBoxApi.Models
{
    public class FileMetaData
    {
        public DateTime client_modified { get; set; }
        [DeserializeAs(Name = ".tag")]
        public string tag { get; set; }
        public int size { get; set; }
        public bool is_downloadable { get; set; }    
        public string name { get; set; } 
    }
}
    
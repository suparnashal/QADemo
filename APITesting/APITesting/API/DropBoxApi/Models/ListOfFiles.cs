using System;
using System.Collections.Generic;
using System.Text;

namespace APITesting.API.DropBoxApi.Models
{
  public class ListOfFiles
    {
        public List<FileMetaData> entries { get; set; }
        public bool has_more { get; set; }
    }
}

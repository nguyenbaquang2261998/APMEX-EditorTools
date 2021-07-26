using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MexicoEditorTool.Models
{
    class Articles
    {
        public Articles(int id, string content)
        {
            this.Id = id;
            this.OptimizedContent = content;
        }
        public Articles(DataRow row)
        {
            this.Id = (int)row["id"];
            this.OptimizedContent = row["optimizedcontent"].ToString() ;
        }
        public int Id { get; set; }
        public string OptimizedContent { get; set; }
    }
}

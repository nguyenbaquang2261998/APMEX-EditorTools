using System;
using System.Collections.Generic;
using System.Text;

namespace MexicoEditorTool.DTO
{
    class ProjectInfomation
    {
        public int Id { get; set; }
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
    }
}

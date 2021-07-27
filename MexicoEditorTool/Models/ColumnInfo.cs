using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MexicoEditorTool.Models
{
    class ColumnInfo
    {
        public ColumnInfo(string name)
        {
            this.ColumnName = name;
        }
        public ColumnInfo(DataRow row)
        {
            this.ColumnName = row["column_name"].ToString();
        }
        public string ColumnName { get; set; }
    }
}

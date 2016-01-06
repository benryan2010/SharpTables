using SharpTablesCore.TableComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SharpTablesCore
{
    public class SharpTable<T>
    {

        /*-- Properties --*/

        public TableConfig Config { get; set; }

        private Type itemType { get; set; }
        public Type ItemType { get { return itemType; } }

        private List<Column> columns { get; set; }
        public List<Column> Columns 
        { 
            get 
            { 
                return columns.OrderBy(c => c.ColumnID).ThenBy(c => c.Name).ToList(); 
            } 
        }

        private List<T> listItems { get; set; }
        public List<T> ListItems { 
            get
            {
                return listItems;
            } 
            
            set
            {
                listItems = value;
            } 
        }

        /*-- public methods --*/

        public SharpTable(List<T> Items, TableConfig tableConfig)
        {
            ListItems = Items;
            itemType = listItems.GetType().GetGenericArguments()[0];
            MakeColumns();
            Config = tableConfig;
        }

        public SharpTable(Type T, TableConfig tableConfig)
        {
            itemType = T;
            MakeColumns();
            Config = tableConfig;
        }

        public string GetColumnPlaceHolders()
        {
            string result = "<tr>";
            for (var i = 1; i <= Columns.Count(); i++)
                result += "<td></td>";
            result += "</tr>";
            return result;
        }

        public string GetTableScripts()
        {
            string result = GetColumns();
            result += GetDataConfiguration();

            return result;
        }

        
        /*-- Private Methods --*/
        
        private string GetColumns()
        {
            string result = "\"columns\" : [";

            var count = columns.Count();
            foreach (Column c in Columns)
            {
                result += "{\'data\':\'" + c.Name + "\', \'title\':\'" + c.Label + "\'}";
                if (count > 1)
                {
                    result += ", ";
                    count--;
                }
            }
            result += "]";
            return result;
        }

        private string GetDataConfiguration()
        {
            if (Config.DataConfig.IsAjaxData)
            {
                
                string result = ",\"processing\" : true \n";

                if (Config.DataConfig.IsServerSide)
                {
                    result += ",\"serverSide\" : true \n";
                    result += ",\"ajax\" : {\n";

                    result += "\"url\" : \"" + Config.DataConfig.URL + "\"";
                    result += ",\"type\" : \"" + Config.DataConfig.Action.ToString() + "\"";
                    result += "}\n";
                }
                else
                {
                    result += ",\"ajax\" : \"" + Config.DataConfig.URL + "\""; 
                }

                return result;
            }
            else
            {
                return "";
            }
        }
        
        private void MakeColumns()
        {
            columns = new List<Column>();
            foreach (var p in itemType.GetProperties())
            {
                var dp = (SharpTablesCore.DataAnnotations.SharpTable)p.GetCustomAttributes(typeof(SharpTablesCore.DataAnnotations.SharpTable), true).Single();
                columns.Add(new Column
                {
                    ColumnID = dp.ColumnIndex,
                    Name = p.Name,
                    Label = dp.ColumnLabel,
                    IsOrderable = dp.IsSortable,
                    IsSearchable = dp.IsFilterable
                });
            }
        }
    }

    public class TableConfig
    {
        public string TableID { get; set; }
        public DataConfiguration DataConfig { get; set; }

        public TableConfig()
        {
            DataConfig = new DataConfiguration();
        }
    }

    public class DataConfiguration
    {
        public bool IsAjaxData { get; set; }
        public bool IsServerSide { get; set; }
        public string URL { get; set; }
        public ActionType Action { get; set; }
    }
    
}

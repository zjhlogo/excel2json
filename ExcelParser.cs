using System;
using System.Collections.Generic;
using System.Data;

namespace excel2json
{
    public class ExcelParser
    {
        public enum TableType
        {
            Unknown,
            Config,
            Table,
        }

        public class FieldInfo
        {
            public string type;
            public string name;
            public string comment;
            public List<object> datas = new List<object>();
        }

        public class TableInfo
        {
            public TableType parseMode = TableType.Unknown;
            public string tableName;
            public int startRow;
            public int endRow;
            public int numRows;
            public int numFields;
            public List<FieldInfo> fieldInfos = new List<FieldInfo>();
        }

        public static List<TableInfo> ReadSheetData(DataTable sheet)
        {
            List<TableInfo> tableDatas = new List<TableInfo>();

            int firstDataRow = 0;
            TableInfo tableInfo = null;

            // 1st pass, to collect table/config informations
            for (int i = firstDataRow; i < sheet.Rows.Count; i++)
            {
                DataRow row = sheet.Rows[i];

                var firstColumn = row[0].ToString();
                if (firstColumn == "[CONFIG_BEGIN]")
                {
                    if (tableInfo != null) throw new Exception(string.Format("Unclosed Config/Table {0}", tableInfo.tableName));

                    tableInfo = new TableInfo();
                    tableInfo.parseMode = TableType.Config;
                    tableInfo.startRow = i;
                    tableInfo.tableName = row[1].ToString();
                }
                else if (firstColumn == "[CONFIG_END]")
                {
                    if (tableInfo == null || tableInfo.parseMode != TableType.Config) throw new Exception("Can not place CONFIG_END tag here");
                    tableInfo.endRow = i;
                    tableDatas.Add(tableInfo);
                    tableInfo = null;
                }
                else if (firstColumn == "[TABLE_BEGIN]")
                {
                    if (tableInfo != null) throw new Exception(string.Format("Unclosed Config/Table {0}", tableInfo.tableName));

                    tableInfo = new TableInfo();
                    tableInfo.parseMode = TableType.Table;
                    tableInfo.startRow = i;
                    tableInfo.tableName = row[1].ToString();
                }
                else if (firstColumn == "[TABLE_END]")
                {
                    if (tableInfo == null || tableInfo.parseMode != TableType.Table) throw new Exception("Can not place TABLE_END tag here");
                    tableInfo.endRow = i;
                    tableDatas.Add(tableInfo);
                    tableInfo = null;
                }
            }

            // 2nd pass, for each table/config parse it separately
            foreach (var info in tableDatas)
            {
                if (info.parseMode == TableType.Config)
                {
                    ParseRowMajorTable(info, sheet);
                }
                else if (info.parseMode == TableType.Table)
                {
                    ParseColumnMajorTable(info, sheet);
                }
            }

            return tableDatas;
        }

        private static void ParseRowMajorTable(TableInfo tableInfo, DataTable sheet)
        {
            for (int i = tableInfo.startRow + 1; i < tableInfo.endRow; ++i)
            {
                FieldInfo field = new FieldInfo();
                field.comment = sheet.Rows[i][0].ToString();
                field.type = sheet.Rows[i][1].ToString();
                field.name = sheet.Rows[i][2].ToString();

                field.datas.Add(ConvertData(field.type, sheet.Rows[i][3].ToString()));

                tableInfo.fieldInfos.Add(field);
            }

            tableInfo.numRows = 1;
            tableInfo.numFields = tableInfo.fieldInfos.Count;
        }

        private static void ParseColumnMajorTable(TableInfo tableInfo, DataTable sheet)
        {
            int descIndex = tableInfo.startRow + 1;
            int typeIndex = tableInfo.startRow + 2;
            int nameIndex = tableInfo.startRow + 3;

            tableInfo.numRows = tableInfo.endRow - tableInfo.startRow - 4;
            tableInfo.numFields = 0;
            if (tableInfo.numRows < 0)
            {
                tableInfo.numRows = 0;
                return;
            }

            for (int columnIndex = 0; columnIndex < sheet.Columns.Count; ++columnIndex)
            {
                var type = sheet.Rows[typeIndex][columnIndex].ToString();
                var name = sheet.Rows[nameIndex][columnIndex].ToString();
                if (name == "" || type == "") break;

                FieldInfo field = new FieldInfo();
                field.comment = sheet.Rows[descIndex][columnIndex].ToString();
                field.name = name;
                field.type = type;

                for (int i = tableInfo.startRow + 4; i < tableInfo.endRow; ++i)
                {
                    field.datas.Add(ConvertData(field.type, sheet.Rows[i][columnIndex].ToString()));
                }

                tableInfo.fieldInfos.Add(field);
            }

            tableInfo.numFields = tableInfo.fieldInfos.Count;
        }

        private static object ConvertData(string type, string value)
        {
            if (type == "bool")
            {
                return bool.Parse(value);
            }
            else if (type == "byte")
            {
                return byte.Parse(value);
            }
            else if (type == "short")
            {
                return short.Parse(value);
            }
            else if (type == "ushort")
            {
                return ushort.Parse(value);
            }
            else if (type == "int" || type == "int32")
            {
                return int.Parse(value);
            }
            else if (type == "uint" || type == "uint32")
            {
                return uint.Parse(value);
            }
            else if (type == "long" || type == "int64")
            {
                return long.Parse(value);
            }
            else if (type == "ulong" || type == "uint64")
            {
                return ulong.Parse(value);
            }
            else if (type == "float" || type == "Fix64")
            {
                return float.Parse(value);
            }
            else if (type == "double" || type == "Fix128")
            {
                return double.Parse(value);
            }
            else if (type == "string")
            {
                return value;
            }
            else if (int.TryParse(value, out var number))
            {
                return number;
            }
            else if (float.TryParse(value, out var fnum))
            {
                return fnum;
            }

            return value;
        }
    }
}

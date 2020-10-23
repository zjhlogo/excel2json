using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace excel2json
{
    /// <summary>
    /// 将DataTable对象，转换成JSON string，并保存到文件中
    /// </summary>
    class JsonExporter
    {
        string mContext = "";
        int mHeaderRows = 0;

        public enum ParseMode
        {
            Unknown,
            RowMajor,
            ColumnMajor,
        }

        public class ColumnData
        {
            public string type;
            public string name;
            public string desc;
            public List<string> datas = new List<string>();
        }

        public class TableInfo
        {
            public ParseMode parseMode = ParseMode.Unknown;
            public string tableName;
            public int startRow;
            public int endRow;
            public List<ColumnData> columnDatas = new List<ColumnData>();
        }

        public string context {
            get {
                return mContext;
            }
        }

        /// <summary>
        /// 构造函数：完成内部数据创建
        /// </summary>
        /// <param name="excel">ExcelLoader Object</param>
        public JsonExporter(ExcelLoader excel, bool lowcase, bool exportArray, string dateFormat, bool forceSheetName, int headerRows, string excludePrefix, bool cellJson)
        {
            mHeaderRows = headerRows - 1;
            List<DataTable> validSheets = new List<DataTable>();
            for (int i = 0; i < excel.Sheets.Count; i++)
            {
                DataTable sheet = excel.Sheets[i];

                // 过滤掉包含特定前缀的表单
                string sheetName = sheet.TableName;
                if (excludePrefix.Length > 0 && sheetName.StartsWith(excludePrefix))
                    continue;

                if (sheet.Columns.Count > 0 && sheet.Rows.Count > 0)
                    validSheets.Add(sheet);
            }

            var jsonSettings = new JsonSerializerSettings
            {
                DateFormatString = dateFormat,
                Formatting = Formatting.Indented
            };

            if (!forceSheetName && validSheets.Count == 1)
            {   // single sheet

                //-- convert to object
                object sheetValue = convertSheet(validSheets[0], exportArray, lowcase, excludePrefix, cellJson);

                //-- convert to json string
                mContext = JsonConvert.SerializeObject(sheetValue, jsonSettings);
            }
            else
            { // mutiple sheet

                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var sheet in validSheets)
                {
                    object sheetValue = convertSheet(sheet, exportArray, lowcase, excludePrefix, cellJson);
                    data.Add(sheet.TableName, sheetValue);
                }

                //-- convert to json string
                mContext = JsonConvert.SerializeObject(data, jsonSettings);
            }
        }

        private object convertSheet(DataTable sheet, bool exportArray, bool lowcase, string excludePrefix, bool cellJson)
        {
            if (exportArray)
                return convertSheetToArray(sheet, lowcase, excludePrefix, cellJson);
            else
                return convertSheetToDict(sheet, lowcase, excludePrefix, cellJson);
        }

        private object convertSheetToArray(DataTable sheet, bool lowcase, string excludePrefix, bool cellJson)
        {
            List<TableInfo> tableDatas = new List<TableInfo>();

            int firstDataRow = 0;
            TableInfo tableInfo = null;

            // first pass to collect table/config informations
            for (int i = firstDataRow; i < sheet.Rows.Count; i++)
            {
                DataRow row = sheet.Rows[i];

                var firstColumn = row[0].ToString();
                if (firstColumn == "[CONFIG_BEGIN]")
                {
                    if (tableInfo != null) throw new Exception(string.Format("Unclosed Config/Table {0}", tableInfo.tableName));

                    tableInfo = new TableInfo();
                    tableInfo.parseMode = ParseMode.RowMajor;
                    tableInfo.startRow = i;
                    tableInfo.tableName = row[1].ToString();
                }
                else if (firstColumn == "[CONFIG_END]")
                {
                    if (tableInfo == null || tableInfo.parseMode != ParseMode.RowMajor) throw new Exception("Can not place CONFIG_END tag here");
                    tableInfo.endRow = i;
                    tableDatas.Add(tableInfo);
                    tableInfo = null;
                }
                else if (firstColumn == "[TABLE_BEGIN]")
                {
                    if (tableInfo != null) throw new Exception(string.Format("Unclosed Config/Table {0}", tableInfo.tableName));

                    tableInfo = new TableInfo();
                    tableInfo.parseMode = ParseMode.ColumnMajor;
                    tableInfo.startRow = i;
                    tableInfo.tableName = row[1].ToString();
                }
                else if (firstColumn == "[TABLE_END]")
                {
                    if (tableInfo == null || tableInfo.parseMode != ParseMode.ColumnMajor) throw new Exception("Can not place TABLE_END tag here");
                    tableInfo.endRow = i;
                    tableDatas.Add(tableInfo);
                    tableInfo = null;
                }
            }

            // for each table/config parse it separately
            foreach (var info in tableDatas)
            {
                if (info.parseMode == ParseMode.RowMajor)
                {
                    parseRowMajorTable(info, sheet, lowcase, excludePrefix, cellJson);
                }
                else if (info.parseMode == ParseMode.ColumnMajor)
                {
                    parseColumnMajorTable(info, sheet, lowcase, excludePrefix, cellJson);
                }
            }

            return tableDatas;
        }

        private void parseRowMajorTable(TableInfo tableInfo, DataTable sheet, bool lowcase, string excludePrefix, bool cellJson)
        {
            for (int i = tableInfo.startRow + 1; i < tableInfo.endRow; ++i)
            {
                ColumnData column = new ColumnData();
                column.desc = sheet.Rows[i][0].ToString();
                column.type = sheet.Rows[i][1].ToString();
                column.name = sheet.Rows[i][2].ToString();
                column.datas.Add(sheet.Rows[i][3].ToString());
                tableInfo.columnDatas.Add(column);
            }
        }

        private void parseColumnMajorTable(TableInfo tableInfo, DataTable sheet, bool lowcase, string excludePrefix, bool cellJson)
        {
            int descIndex = tableInfo.startRow + 1;
            int typeIndex = tableInfo.startRow + 2;
            int nameIndex = tableInfo.startRow + 3;

            for (int columnIndex = 0; columnIndex < sheet.Columns.Count; ++columnIndex)
            {
                var type = sheet.Rows[typeIndex][columnIndex].ToString();
                var name = sheet.Rows[nameIndex][columnIndex].ToString();
                if (name == "" || type == "") break;

                ColumnData column = new ColumnData();
                column.desc = sheet.Rows[descIndex][columnIndex].ToString();
                column.name = name;
                column.type = type;

                for (int i = tableInfo.startRow + 4; i < tableInfo.endRow; ++i)
                {
                    column.datas.Add(sheet.Rows[i][columnIndex].ToString());
                }

                tableInfo.columnDatas.Add(column);
            }
        }

        /// <summary>
        /// 以第一列为ID，转换成ID->Object的字典对象
        /// </summary>
        private object convertSheetToDict(DataTable sheet, bool lowcase, string excludePrefix, bool cellJson)
        {
            Dictionary<string, object> importData =
                new Dictionary<string, object>();

            int firstDataRow = mHeaderRows;
            for (int i = firstDataRow; i < sheet.Rows.Count; i++)
            {
                DataRow row = sheet.Rows[i];
                string ID = row[sheet.Columns[0]].ToString();
                if (ID.Length <= 0)
                    ID = string.Format("row_{0}", i);

                var rowObject = convertRowToDict(sheet, row, lowcase, firstDataRow, excludePrefix, cellJson);
                // 多余的字段
                // rowObject[ID] = ID;
                importData[ID] = rowObject;
            }

            return importData;
        }

        /// <summary>
        /// 把一行数据转换成一个对象，每一列是一个属性
        /// </summary>
        private Dictionary<string, object> convertRowToDict(DataTable sheet, DataRow row, bool lowcase, int firstDataRow, string excludePrefix, bool cellJson)
        {
            var rowData = new Dictionary<string, object>();
            int col = 0;
            foreach (DataColumn column in sheet.Columns)
            {
                // 过滤掉包含指定前缀的列
                string columnName = column.ToString();
                if (excludePrefix.Length > 0 && columnName.StartsWith(excludePrefix))
                    continue;

                object value = row[column];

                // 尝试将单元格字符串转换成 Json Array 或者 Json Object
                if (cellJson)
                {
                    string cellText = value.ToString().Trim();
                    if (cellText.StartsWith("[") || cellText.StartsWith("{"))
                    {
                        try
                        {
                            object cellJsonObj = JsonConvert.DeserializeObject(cellText);
                            if (cellJsonObj != null)
                                value = cellJsonObj;
                        }
                        catch (Exception exp)
                        {
                        }
                    }
                }

                if (value.GetType() == typeof(System.DBNull))
                {
                    value = getColumnDefault(sheet, column, firstDataRow);
                }
                else if (value.GetType() == typeof(double))
                { // 去掉数值字段的“.0”
                    double num = (double)value;
                    if ((int)num == num)
                        value = (int)num;
                }

                string fieldName = column.ToString();
                // 表头自动转换成小写
                if (lowcase)
                    fieldName = fieldName.ToLower();

                if (string.IsNullOrEmpty(fieldName))
                    fieldName = string.Format("col_{0}", col);

                rowData[fieldName] = value;
                col++;
            }

            return rowData;
        }

        /// <summary>
        /// 对于表格中的空值，找到一列中的非空值，并构造一个同类型的默认值
        /// </summary>
        private object getColumnDefault(DataTable sheet, DataColumn column, int firstDataRow)
        {
            for (int i = firstDataRow; i < sheet.Rows.Count; i++)
            {
                object value = sheet.Rows[i][column];
                Type valueType = value.GetType();
                if (valueType != typeof(System.DBNull))
                {
                    if (valueType.IsValueType)
                        return Activator.CreateInstance(valueType);
                    break;
                }
            }
            return "";
        }

        /// <summary>
        /// 将内部数据转换成Json文本，并保存至文件
        /// </summary>
        /// <param name="jsonPath">输出文件路径</param>
        public void SaveToFile(string filePath, Encoding encoding)
        {
            //-- 保存文件
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter writer = new StreamWriter(file, encoding))
                    writer.Write(mContext);
            }
        }
    }
}

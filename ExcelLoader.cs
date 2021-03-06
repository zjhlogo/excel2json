﻿using System;
using System.IO;
using System.Data;
using ExcelDataReader;

namespace excel2json
{
    /// <summary>
    /// 将 Excel 文件(*.xls 或者 *.xlsx)加载到内存 DataSet
    /// </summary>
    class ExcelLoader
    {
        private DataSet _data;

        // TODO: add Sheet Struct Define

        public ExcelLoader(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Use the AsDataSet extension method
                    // The result of each spreadsheet is in result.Tables
                    var result = reader.AsDataSet(CreateDataSetReadConfig());
                    this._data = result;
                }
            }

            if (this.Sheets.Count < 1)
            {
                throw new Exception("Excel file is empty: " + filePath);
            }
        }

        public DataTableCollection Sheets
        {
            get
            {
                return this._data.Tables;
            }
        }

        private ExcelDataSetConfiguration CreateDataSetReadConfig()
        {
            var tableConfig = new ExcelDataTableConfiguration()
            {
                // Gets or sets a value indicating whether to use a row from the 
                // data as column names.
                UseHeaderRow = false,
            };

            return new ExcelDataSetConfiguration()
            {
                // Gets or sets a value indicating whether to set the DataColumn.DataType
                // property in a second pass.
                UseColumnDataType = true,

                // Gets or sets a callback to obtain configuration options for a DataTable. 
                ConfigureDataTable = (tableReader) => { return tableConfig; },
            };
        }
    }
}

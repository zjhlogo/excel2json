using System.Collections.Generic;
using System.IO;
using System.Text;

namespace excel2json.GUI
{
    /// <summary>
    /// 为GUI模式提供的整体数据管理
    /// </summary>
    class DataManager
    {
        // 数据导入设置
        private Program.Options _options;
        private Encoding _encoding;

        // 导出数据
        private JsonExporter _json;
        private XmlExporter _xml;
        private CSharpExporter _csharp;

        /// <summary>
        /// 导出的Json文本
        /// </summary>
        public string jsonContext
        {
            get
            {
                if (_json != null)
                {
                    return _json.context;
                }
                else
                {
                    return "";
                }
            }
        }

        public string xmlContext
        {
            get
            {
                if (_xml != null)
                {
                    return _xml.context;
                }
                else
                {
                    return "";
                }
            }
        }

        public string csharpCode
        {
            get
            {
                if (_csharp != null)
                {
                    return _csharp.code;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 保存Json
        /// </summary>
        /// <param name="filePath">保存路径</param>
        public void saveJson(string filePath)
        {
            if (_json != null)
            {
                _json.SaveToFile(filePath, _encoding);
            }
        }

        public void saveXml(string filePath)
        {
            if (_xml != null)
            {
                _xml.SaveToFile(filePath, _encoding);
            }
        }

        public void saveCSharp(string filePath)
        {
            if (_csharp != null)
            {
                _csharp.SaveToFile(filePath, _encoding);
            }
        }

        /// <summary>
        /// 加载Excel文件
        /// </summary>
        /// <param name="options">导入设置</param>
        public void loadExcel(Program.Options options)
        {
            _options = options;

            //-- Excel File
            string excelPath = options.excelPath;
            string excelName = Path.GetFileNameWithoutExtension(excelPath);

            //-- Encoding
            _encoding = new UTF8Encoding(false);

            //-- Load Excel
            ExcelLoader excel = new ExcelLoader(excelPath);

            //-- Parse Excel
            List<ExcelParser.TableInfo> tableInfos = ExcelParser.ReadSheetData(excel.Sheets[0]);

            //-- 导出 json
            _json = new JsonExporter(tableInfos);

            //-- 导出 xml
            _xml = new XmlExporter(tableInfos);

            //-- C# 结构体定义
            _csharp = new CSharpExporter(tableInfos, excelName);
        }
    }
}

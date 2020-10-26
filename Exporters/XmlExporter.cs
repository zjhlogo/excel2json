using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace excel2json
{
    class XmlExporter
    {
        private string _context = "";

        public string context {
            get {
                return _context;
            }
        }

        public XmlExporter(List<ExcelParser.TableInfo> tableInfos)
        {
            _context = ConvertSheet(tableInfos);
        }

        public string ConvertSheet(List<ExcelParser.TableInfo> tableInfos)
        {
            ExcelParser.TableInfo attrs = null;
            ExcelParser.TableInfo table = null;
            foreach (var tableInfo in tableInfos)
            {
                if (tableInfo.parseMode == ExcelParser.TableType.Config) attrs = tableInfo;
                if (tableInfo.parseMode == ExcelParser.TableType.Table) table = tableInfo;
            }

            XmlDocument doc = new XmlDocument();
            var xmlExData = doc.CreateElement("ExData");
            doc.AppendChild(xmlExData);

            if (table != null)
            {
                var xmlTable = doc.CreateElement("Table");
                xmlTable.SetAttribute("Name", table.tableName);
                xmlExData.AppendChild(xmlTable);

                var xmlColumnList = doc.CreateElement("ColumnList");
                xmlTable.AppendChild(xmlColumnList);

                foreach (var fieldInfo in table.fieldInfos)
                {
                    var xmlColumn = doc.CreateElement("Column");
                    xmlColumn.SetAttribute("Name", fieldInfo.name);
                    xmlColumn.SetAttribute("DataType", fieldInfo.type);
                    xmlColumnList.AppendChild(xmlColumn);
                }

                if (attrs != null)
                {
                    foreach(var fieldInfo in attrs.fieldInfos)
                    {
                        var xmlAttribute = doc.CreateElement("Attribute");
                        xmlAttribute.SetAttribute("Name", fieldInfo.name);
                        xmlAttribute.SetAttribute("DataType", fieldInfo.type);
                        xmlAttribute.SetAttribute("Value", fieldInfo.datas[0].ToString());
                    }
                }

                for (int i = 0; i < table.numRows; ++i)
                {
                    var xmlRecord = doc.CreateElement("Record");
                    for (int j = 0; j < table.numFields; ++j)
                    {
                        xmlRecord.SetAttribute(table.fieldInfos[j].name, table.fieldInfos[j].datas[i].ToString());
                    }
                    xmlTable.AppendChild(xmlRecord);
                }
            }

            var stringBuilder = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                doc.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        public void SaveToFile(string filePath, Encoding encoding)
        {
            //-- 保存文件
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter writer = new StreamWriter(file, encoding))
                {
                    writer.Write(_context);
                }
            }
        }
    }
}

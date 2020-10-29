using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace excel2json
{
    class XmlExporter
    {
        private string _context = "";

        class TypeMappingInfo
        {
            public TypeMappingInfo(string pref, string index)
            {
                prefix = pref;
                dataTypeIndex = index;
            }

            public string prefix;
            public string dataTypeIndex;
        }

        private Dictionary<string, TypeMappingInfo> _typeMap = new Dictionary<string, TypeMappingInfo>()
        {
            {"bool", new TypeMappingInfo("n_", "1") },
            {"int", new TypeMappingInfo("n_", "1") },
            {"int64", new TypeMappingInfo("n_", "2")},
            {"float", new TypeMappingInfo("f_", "3")},
            {"string", new TypeMappingInfo("s_", "4")},
        };

        public string context
        {
            get
            {
                return _context;
            }
        }

        public XmlExporter(List<ExcelParser.TableInfo> tableInfos, string excelName)
        {
            _context = ConvertSheet(tableInfos, excelName);
        }

        public string ConvertSheet(List<ExcelParser.TableInfo> tableInfos, string excelName)
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

            XmlElement xmlTable = null;

            if (table != null || attrs != null)
            {
                xmlTable = doc.CreateElement("Table");
                xmlTable.SetAttribute("Name", NameFormater.FormatName(excelName, false));
                xmlExData.AppendChild(xmlTable);
            }

            if (table != null)
            {
                var xmlColumnList = doc.CreateElement("ColumnList");
                xmlTable.AppendChild(xmlColumnList);

                foreach (var fieldInfo in table.fieldInfos)
                {
                    var xmlColumn = doc.CreateElement("Column");

                    string finalName = fieldInfo.name;
                    string finalDataType = fieldInfo.type;
                    if (_typeMap.TryGetValue(fieldInfo.type, out var mappingInfo))
                    {
                        finalName = mappingInfo.prefix + fieldInfo.name;
                        finalDataType = mappingInfo.dataTypeIndex;
                    }

                    xmlColumn.SetAttribute("Name", finalName);
                    xmlColumn.SetAttribute("DataType", finalDataType);
                    xmlColumnList.AppendChild(xmlColumn);
                }
            }

            if (attrs != null)
            {
                foreach (var fieldInfo in attrs.fieldInfos)
                {
                    var xmlAttribute = doc.CreateElement("Attribute");
                    xmlTable.AppendChild(xmlAttribute);

                    string finalName = fieldInfo.name;
                    string finalDataType = fieldInfo.type;
                    if (_typeMap.TryGetValue(fieldInfo.type, out var mappingInfo))
                    {
                        finalName = mappingInfo.prefix + fieldInfo.name;
                        finalDataType = mappingInfo.dataTypeIndex;
                    }

                    xmlAttribute.SetAttribute("Name", finalName);
                    xmlAttribute.SetAttribute("DataType", finalDataType);
                    xmlAttribute.SetAttribute("Value", fieldInfo.datas[0].ToString());
                }
            }

            if (table != null)
            {
                for (int i = 0; i < table.numRows; ++i)
                {
                    var xmlRecord = doc.CreateElement("Record");
                    for (int j = 0; j < table.numFields; ++j)
                    {
                        string finalName = table.fieldInfos[j].name;
                        if (_typeMap.TryGetValue(table.fieldInfos[j].type, out var mappingInfo))
                        {
                            finalName = mappingInfo.prefix + table.fieldInfos[j].name;
                        }

                        xmlRecord.SetAttribute(finalName, table.fieldInfos[j].datas[i].ToString());
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

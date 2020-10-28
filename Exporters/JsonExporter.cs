using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static excel2json.ExcelParser;

namespace excel2json
{
    class JsonExporter
    {
        private string _context = "";

        public string context {
            get {
                return _context;
            }
        }

        public JsonExporter(List<ExcelParser.TableInfo> tableInfos, string excelName)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            //-- convert to object
            var fullDatas = ConvertSheet(tableInfos, excelName);
            if (fullDatas.Count == 1)
            {
                //-- convert to json string
                foreach(var table in fullDatas)
                {
                    _context = JsonConvert.SerializeObject(table, jsonSettings);
                }
            }
            else
            {
                //-- convert to json string
                _context = JsonConvert.SerializeObject(fullDatas, jsonSettings);
            }
        }

        private List<List<Dictionary<string, object>>> ConvertSheet(List<ExcelParser.TableInfo> tableInfos, string excelName)
        {
            List<List<Dictionary<string, object>>> fullDatas = new List<List<Dictionary<string, object>>>();
            foreach (var tableInfo in tableInfos)
            {
                List<Dictionary<string, object>> rowDatas = new List<Dictionary<string, object>>();

                for (int i = 0; i < tableInfo.numRows; ++i)
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    for (int j = 0; j < tableInfo.numFields; ++j)
                    {
                        var finalData = ConvertDataType(tableInfo.fieldInfos[j], tableInfo.fieldInfos[j].datas[i]);
                        rowData.Add(tableInfo.fieldInfos[j].name, finalData);
                    }

                    rowDatas.Add(rowData);
                }

                fullDatas.Add(rowDatas);
            }

            return fullDatas;
        }

        private object ConvertDataType(FieldInfo fieldInfo, object value)
        {
            if (fieldInfo.type == "Vector2")
            {
                var values = JsonConvert.DeserializeObject<float[]>(value.ToString());
                Dictionary<string, float> newValue = new Dictionary<string, float>();
                newValue.Add("X", values[0]);
                newValue.Add("Y", values[1]);
                return newValue;
            }
            else if (fieldInfo.type == "Vector3")
            {
                var values = JsonConvert.DeserializeObject<float[]>(value.ToString());
                Dictionary<string, float> newValue = new Dictionary<string, float>();
                newValue.Add("X", values[0]);
                newValue.Add("Y", values[1]);
                newValue.Add("Z", values[2]);
                return newValue;
            }
            else if (fieldInfo.type == "Vector4")
            {
                var values = JsonConvert.DeserializeObject<float[]>(value.ToString());
                Dictionary<string, float> newValue = new Dictionary<string, float>();
                newValue.Add("X", values[0]);
                newValue.Add("Y", values[1]);
                newValue.Add("Z", values[2]);
                newValue.Add("W", values[3]);
                return newValue;
            }
            else if (fieldInfo.type == "int[]")
            {
                var values = JsonConvert.DeserializeObject<int[]>(value.ToString());
                return values;
            }
            else if (fieldInfo.type == "uint[]")
            {
                var values = JsonConvert.DeserializeObject<uint[]>(value.ToString());
                return values;
            }
            else if (fieldInfo.type == "Fix64[]" || fieldInfo.type == "float[]")
            {
                var values = JsonConvert.DeserializeObject<float[]>(value.ToString());
                return values;
            }
            else if (fieldInfo.type == "string[]")
            {
                var values = JsonConvert.DeserializeObject<string[]>(value.ToString());
                return values;
            }
            else if (fieldInfo.type == "Color")
            {
                var values = JsonConvert.DeserializeObject<byte[]>(value.ToString());
                Dictionary<string, byte> newValue = new Dictionary<string, byte>();
                newValue.Add("r", values[0]);
                newValue.Add("g", values[1]);
                newValue.Add("b", values[2]);
                newValue.Add("a", values[3]);
                return newValue;
            }
            else if (fieldInfo.type == "Color[]")
            {
                var colors = JsonConvert.DeserializeObject<List<List<byte>>>(value.ToString());
                List<Dictionary<string, byte>> newValue = new List<Dictionary<string, byte>>();
                foreach (var color in colors)
                {
                    var dic = new Dictionary<string, byte>();
                    dic.Add("r", color[0]);
                    dic.Add("g", color[1]);
                    dic.Add("b", color[2]);
                    dic.Add("a", color[3]);
                    newValue.Add(dic);
                }
                return newValue;
            }

            return value;
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

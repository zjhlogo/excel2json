using System.Collections.Generic;
using System.IO;
using System.Text;

namespace excel2json
{
    /// <summary>
    /// 根据表头，生成C#类定义数据结构
    /// 表头使用三行定义：字段名称、字段类型、注释
    /// </summary>
    class CSharpExporter
    {
        private string _code;

        public string code {
            get {
                return _code;
            }
        }

        public CSharpExporter(List<ExcelParser.TableInfo> tableInfos, string excelName)
        {
            //-- 创建代码字符串
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("//");
            sb.AppendLine("// Auto Generated Code By excel2json");
            sb.AppendLine("// https://neil3d.gitee.io/coding/excel2json.html");
            sb.AppendLine();
            sb.AppendFormat("// Generate From {0}", excelName);
            sb.AppendLine();
            sb.AppendLine();

            foreach (var tableInfo in tableInfos)
            {
                sb.AppendFormat("public class {0}", tableInfo.tableName);
                sb.AppendLine();
                sb.Append("{");
                sb.AppendLine();
                for (int i = 0; i < tableInfo.numFields; i++)
                {
                    var fieldData = tableInfo.fieldInfos[i];

                    var comment = FormatComment(fieldData.comment);
                    sb.AppendFormat("    public {0} {1}; // {2}", fieldData.type, fieldData.name, comment);
                    sb.AppendLine();
                }
                sb.Append('}');
                sb.AppendLine();
                sb.AppendLine();
            }

            sb.AppendLine("// End of Auto Generated Code");

            _code = sb.ToString();
        }

        private string FormatComment(string comment)
        {
            StringBuilder sb = new StringBuilder(comment);
            sb = sb.Replace("\r\n", " ");
            sb = sb.Replace("\r", " ");
            sb = sb.Replace("\n", " ");
            return sb.ToString();
        }

        public void SaveToFile(string filePath, Encoding encoding)
        {
            //-- 保存文件
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter writer = new StreamWriter(file, encoding))
                {
                    writer.Write(_code);
                }
            }
        }
    }
}

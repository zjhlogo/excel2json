using System;
using System.Text;

namespace excel2json
{
    public class NameFormater
    {
        private static StringBuilder _sbFormatName = new StringBuilder();

        public static string FormatCamelName(string name, bool lowerCaseFirstChar)
        {
            bool upperCase = !lowerCaseFirstChar;

            _sbFormatName.Clear();
            for (int i = 0; i < name.Length; ++i)
            {
                if (name[i] == '_')
                {
                    upperCase = true;
                    continue;
                }

                if (lowerCaseFirstChar)
                {
                    _sbFormatName.Append(Char.ToLower(name[i]));
                    lowerCaseFirstChar = false;
                    continue;
                }

                if (upperCase)
                {
                    _sbFormatName.Append(Char.ToUpper(name[i]));
                    upperCase = false;
                    continue;
                }

                _sbFormatName.Append(name[i]);
            }

            return _sbFormatName.ToString();
        }

        public static string FormatFileName(string name)
        {
            var lowerName = name.ToLower().Replace(' ', '_');

            _sbFormatName.Clear();
            for (int i = 0; i < lowerName.Length; ++i)
            {
                if (lowerName[i] == '_' && _sbFormatName.Length <= 0)
                {
                    continue;
                }

                if (Char.IsDigit(lowerName[i]) && _sbFormatName.Length <= 0)
                {
                    continue;
                }

                _sbFormatName.Append(lowerName[i]);
            }

            return _sbFormatName.ToString();
        }

        public static string FormatComment(string comment)
        {
            StringBuilder sb = new StringBuilder(comment);
            sb = sb.Replace("\r\n", " ");
            sb = sb.Replace("\r", " ");
            sb = sb.Replace("\n", " ");
            return sb.ToString();
        }

    }
}

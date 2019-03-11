using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MultiLauncher.Core
{
    public class BatCreator
    {
        //string filename = "path-to-file";
        //Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filename);

        //location = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public void CreateBat(string filename, string location, StartGroup startGroup)
        {
            var stringBuilder = new StringBuilder("@echo off");

            foreach (var items in startGroup.StartItems)
            {
                var arguments = string.Empty;
                if (items.Arguments.Any())
                {
                    foreach (var arg in items.Arguments)
                        arguments += $" \"{arg}\"";
                }

                stringBuilder.Append($"\r\nstart \"\" \"{items.FullName}\"{arguments}");
            }

            using (var file = File.Open($"{location}\\{filename}.bat", FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(file))
            {
                writer.Write(stringBuilder.ToString());
            }
        }

        public void StartProcess(IEnumerable<string> files)
        {
            foreach (var file in files)
                Process.Start(file);
        }

        public IEnumerable<string> ReadStartFiles(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File could not be found", path);

            return File.ReadAllLines(path);
        }
    }
}

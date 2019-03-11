using System.Collections.Generic;

namespace MultiLauncher.Core
{
    public class StartItem
    {
        public string Path { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public IEnumerable<string> Arguments { get; set; } = new string[0];

        public string FullName => $@"{Path}\{FileName}.{FileType}";
    }
}

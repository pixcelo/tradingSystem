using System.Text;

namespace Zaku
{
    public class Logger
    {
        private readonly string path;
        public List<string> logs;

        public Logger(string path)
        {
            this.path = path;
            this.logs = new List<string>();
        }

        public void WriteLog()
        {
            var infoString = new StringBuilder();
            foreach (string log in this.logs)
            {
                infoString.Append(log);
                infoString.Append(Environment.NewLine);
            }

            string now = (DateTime.Now).ToString("yyyy-MM-dd-HH-mm-ss");
            File.AppendAllText(Path.Combine(path,  now + ".log"), infoString.ToString());
            Console.WriteLine(infoString.ToString());
        }
    }
}

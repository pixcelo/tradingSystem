using System.Text;

namespace Zaku
{
    public class Logger
    {
        private readonly string path;

        public Logger(string path)
        {
            this.path = path;
        }

        public void WriteLog(List<string> logs)
        {
            var infoString = new StringBuilder();
            foreach (string log in logs)
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

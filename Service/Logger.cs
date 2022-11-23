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

        public void WriteLog(string[] strArray)
        {
            var infoString = new StringBuilder();
            foreach (var str in strArray)
            {
                infoString.Append(str);
                infoString.Append(Environment.NewLine);
            }

            string now = (DateTime.Now).ToString("yyyy-MM-dd-HH-mm-ss");
            File.AppendAllText(Path.Combine(path,  now + ".log"), infoString.ToString());
            Console.WriteLine(infoString.ToString());
        }
    }
}

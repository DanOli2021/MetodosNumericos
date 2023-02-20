using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador1
{
    public class ConsoleWriter : TextWriter
    {
        public string log_file = "";
        TextBox c;

        public ConsoleWriter(TextBox c)
        {
            this.c = c;
        }

        public override void Write(char value)
        {
            c.AppendText("" + value);
        }

        public override void Write(string value = "")
        {
            c.AppendText("" + value);
        }

        public override void WriteLine(char value)
        {
            c.AppendText("" + value + "\r\n");
        }

        public override void WriteLine(string value = "")
        {
            c.AppendText("" + value + "\r\n");
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}

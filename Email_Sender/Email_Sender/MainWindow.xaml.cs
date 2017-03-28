using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;

namespace Email_Sender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            MailAddress from = new MailAddress("cvobodne@yandex.ru");
            MailAddress to = new MailAddress("cvobodn@yandex.ru");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "hello";
            m.Body = "<h2>Привет как там дела?</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
            smtp.Credentials = new NetworkCredential("cvobodne@yandex.ru", "JasdoFy123");
            smtp.EnableSsl = true;
            smtp.Timeout = 5;
            smtp.Send(m); 
        }
    }
}

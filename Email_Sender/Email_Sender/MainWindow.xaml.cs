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
using Microsoft.Win32;
using System.Threading;

namespace Email_Sender
{


    public partial class MainWindow : Window
    {
        Attachment att = null;
        bool SSL = false;
        SmtpClient smtp;
        public MainWindow()
        {
            InitializeComponent();
            tbPass.PasswordChar = '*';
        }

        
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress(tbLogin.Text);
                MailAddress to = new MailAddress(tbTo.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = tbSubject.Text;
                m.Body = tbText.Text;
                m.IsBodyHtml = true;
                smtp = new SmtpClient(tbSmtp.Text, 25);
                smtp.Credentials = new NetworkCredential(tbLogin.Text, tbPass.Password);
                smtp.EnableSsl = SSL;
                if (att != null)
                {
                    m.Attachments.Add(att);
                }
                Thread myth = new Thread(new ParameterizedThreadStart(StartSend));
                myth.Start(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                         
        }

        public void StartSend(object m)
        {
            try
            {
                smtp.Send((MailMessage)m);
                att.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAttach_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Все файлы (*.*)| *.*";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true)
            {
                if (myDialog.FileName != "")
                {
                    att = new Attachment(myDialog.FileName);
                }
            }
        }

        private void cbSSL_Click(object sender, RoutedEventArgs e)
        {
            if (SSL == false)
            {
                SSL = true;
            }
            else
                SSL = false;
        }
    }
}

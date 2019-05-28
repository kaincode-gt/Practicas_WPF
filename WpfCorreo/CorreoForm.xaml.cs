using System.Windows;
using System.Windows.Controls;
using System.Net.Mail;
using Microsoft.Win32;

namespace WpfCorreo
{
    /// <summary>
    /// Lógica de interacción para CorreoForm.xaml
    /// </summary>
    public partial class CorreoForm : UserControl
    {
        readonly string HOST = "localhost";
        readonly int PORT = 25;

        private MailMessage mail = null;
        SmtpClient cliente = null;

        public CorreoForm()
        {
            InitializeComponent();

            cliente = NuevoClienteSmtp();
        }

        private void BotónEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (mail == null)
                mail = CreateDefaultEMail();
            
            //SmtpClient cliente = NuevoClienteSmtp();

            mail.Subject = TextBoxAsunto.Text;
            mail.To.Add(TextBoxPara.Text);
            var dog = mail.Attachments;
            cliente.Send(mail);

            MessageBox.Show("Enviando correo!");
        }

        private SmtpClient NuevoClienteSmtp()
        {
            return new SmtpClient()
            {
                Host = HOST,
                Port = PORT,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
        }
        private MailMessage CreateDefaultEMail()
        {
            return new MailMessage()
            {
                Subject = "Sin asunto",
                Body = "Cuerpo del mensaje",
                From = new MailAddress("porkulero@condom.com")
            };
        }

        private void BotonAdjuntar_Click(object sender, RoutedEventArgs e)
        {
            if (mail == null)
                mail = CreateDefaultEMail();

            OpenFileDialog dialog = new OpenFileDialog();
            ;
            if (dialog.ShowDialog() == true)
            {
                mail.Attachments.Add(new Attachment(dialog.FileName));
                MessageBox.Show(dialog.FileName.ToString());
                //MessageBox.Show(dialog.FileName.LastIndexOf('\\').ToString());
                //MessageBox.Show(dialog.FileName.Length.ToString());
                string fileName = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\')+1);
                //MessageBox.Show(s);
               TextBlockAdjunto.Text = fileName;
            }

        }
    }
}

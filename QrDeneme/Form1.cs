using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QrDeneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            lblCopyrightYear.Text = DateTime.Now.Year.ToString();
            SetDinamikVersiyon();
        }

        void SetDinamikVersiyon()
        {
            //Öncelikle AssemblyInfo.cs içindeki kısmı [assembly: AssemblyVersion(“1.0.*”)] şeklinde değiştirin

            //string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2023, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            string displayableVersion = version.ToString();
            lblVersionNumarasi.Text = version.ToString() + " " + "|";

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Regex validate_emailaddress = email_validation();
            try
            {
                if (txtName.Text != "" && txtSurname.Text != "")
                {
                    if (validate_emailaddress.IsMatch(txtMail.Text) != true)
                    {
                        MessageBox.Show("Geçersiz Mail Adresi Girdiniz!", "Geçersiz", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMail.Focus();
                        return;
                    }

                    var options = new QrCodeEncodingOptions
                    {
                        Height = pictureBox1.Height,
                        Width = pictureBox1.Width
                    };
                    //create instance of BarcodeWrider
                    var writer = new BarcodeWriter();
                    //set format
                    writer.Format = BarcodeFormat.QR_CODE;
                    writer.Options = options;
                    var text = $"BEGIN:VCARD\n" +
                    $"VERSION: 3.0\n" +
                        $"N: {txtName.Text} {txtSurname.Text};\n" +
                        $"ORG:{txtCompany.Text}\n" +
                        $"TITLE:{txtUnvan.Text}\n" +
                        $"URL; WORK: {txtWeb.Text}\n" +
                        $"TEL: {txtTelefon.Text}\n" +
                        $"TEL: {txtTelefon1.Text}\n" +
                        $"TEL: {txtFax.Text}\n" +
                        $"TEL: {txtMobile.Text}\n" +
                        $"EMAIL; INTERNET: {txtMail.Text}\n" +
                        $"ADR; INTL; PARCEL; WORK; CHARSET = utf - 8:; ; {txtAdres.Text};\n" +
                        $"END:VCARD";
                    var result = writer.Write(text);
                    pictureBox1.Image = result;

                    lblAdSoyad.Text = txtName.Text + " " + txtSurname.Text;
                    lblFirmaBilgi.Text = txtCompany.Text;
                    lblEmail.Text = txtMail.Text;
                    lblAdres.Text = txtAdres.Text;

                }
                else
                {
                    MessageBox.Show("Yetkili Adınız ve Soyadını Boş Bırakmayınız");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex.Message);
            }

        }


        private Regex email_validation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string initialDIR = @"C:\Users\Maverick\Desktop\QRfiles";
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = initialDIR;
            dialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|GIF|*.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(dialog.FileName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();
        }

        private void txtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTelefon1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}

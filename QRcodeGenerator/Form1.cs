using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spire.Barcode;

namespace QRcodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            BarcodeSettings.ApplyKey("your key");//you need a key from e-iceblue, otherwise the watermark 'E-iceblue' will be shown in barcode
            BarcodeSettings settings = new BarcodeSettings();
            settings.Type = BarCodeType.QRCode;
            settings.Unit = GraphicsUnit.Pixel;
            settings.ShowText = false;
            settings.ResolutionType = ResolutionType.UseDpi;
            //input data
            string data = "12345";
            settings.Data = data;
            if (this.richTextBox1.Text != null && this.richTextBox1.Text.Length > 0)
            {
                data = this.richTextBox1.Text;
                settings.Data = data;
            }
            //set fore color
            if (this.comboBoxForeColor.SelectedItem != null)
            {
                string foreColor = this.comboBoxForeColor.SelectedItem.ToString();
                settings.ForeColor = Color.FromName(foreColor);
            }
            //set back color
            if (this.comboBoxBackColor.SelectedItem != null)
            {
                string backColor = this.comboBoxBackColor.SelectedItem.ToString();
                settings.BackColor = Color.FromName(backColor);
            }
            //set x
            short barWidth;
            if (this.textBoxX.Text != null && this.textBoxX.Text.Length > 0 && Int16.TryParse(this.textBoxX.Text, out barWidth))
            {
                settings.X = barWidth;

            }
            //set left margin
            short leftMargin = 1;
            if (this.textBoxLeft.Text != null && this.textBoxLeft.Text.Length > 0 && Int16.TryParse(this.textBoxLeft.Text, out leftMargin))
            {
                settings.LeftMargin = leftMargin;

            }
            //set right margin
            short rightMargin = 1;
            if (this.textBoxRight.Text != null && this.textBoxRight.Text.Length > 0 && Int16.TryParse(this.textBoxRight.Text, out rightMargin))
            {
                settings.RightMargin = rightMargin;

            }
            //set top margin
            short topMargin = 1;
            if (this.textBoxTop.Text != null && this.textBoxTop.Text.Length > 0 && Int16.TryParse(this.textBoxTop.Text, out topMargin))
            {
                settings.TopMargin = topMargin;

            }
            //set bottom margin
            short bottomMargin = 1;
            if (this.textBoxBottom.Text != null && this.textBoxBottom.Text.Length > 0 && Int16.TryParse(this.textBoxBottom.Text, out bottomMargin))
            {
                settings.BottomMargin = bottomMargin;

            }
            //set correction level
            if (this.comboBoxCorrectionLevel.SelectedItem != null)
            {
                int correctionLevel = this.comboBoxCorrectionLevel.SelectedIndex;
                switch (correctionLevel)
                {
                   case 0:
                     settings.QRCodeECL = QRCodeECL.L;
                     break;
                   case 1:
                     settings.QRCodeECL = QRCodeECL.M;
                     break;
                   case 2:
                     settings.QRCodeECL = QRCodeECL.Q;
                     break;
                   case 3:
                     settings.QRCodeECL = QRCodeECL.H;
                     break;
                }
                
            }
            //generate QR code
            BarCodeGenerator generator = new BarCodeGenerator(settings);
            Image QRbarcode = generator.GenerateImage();
            //display QR code image in picture box
            pictureBox1.Image = QRbarcode;
        }

        private void textBoxAll_Validating(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            int numberEntered;
            if (tb.Name == "textBoxX")
            {
                if (int.TryParse(tb.Text, out numberEntered))
                {
                    if (0>=numberEntered)
                    {
                        MessageBox.Show("You must enter a number greater than 0");
                    }
                }
                else
                {
                    MessageBox.Show("You need to enter an integer");
                }
            }
            else
            {
                if (int.TryParse(tb.Text, out numberEntered))
                {
                    if (numberEntered < 1)
                    {
                        MessageBox.Show("You must enter a number greater than 1");
                    }
                }
                else
                {
                    MessageBox.Show("You need to enter an integer");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "png file(*.png)|*.png";
            if (DialogResult.OK == sfd.ShowDialog())
            {
                switch (sfd.Filter)
                {
                    case "png file(*.png)|*.png":
                        pictureBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        pictureBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
            }
        }



    }
}

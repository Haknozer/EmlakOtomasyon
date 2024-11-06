using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmlakEv;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace EmlakForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            FormClosing += Form2_FormClosing;
        }

        string satilik = "satilik.txt";
        string kiralik = "kiralik.txt";
        string emlakFile = "emlak_no.txt";
        int emlakNo = 0;
        static Ev[] evKayitlari = new Ev[1000];
        Dictionary<int, int> indexMap = new Dictionary<int, int>();
        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.DataSource = Enum.GetValues(typeof(tur));
            KayitGetir(satilik, typeof(SatilikEv));
            KayitGetir(kiralik, typeof(KiralikEv));

            if (File.Exists(emlakFile))
            {
                string[] no = File.ReadAllLines(emlakFile);
                emlakNo = int.Parse(no[0]);
            }
            textBox1.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox5.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox6.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox9.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox10.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
            textBox12.KeyPress += new KeyPressEventHandler(txtSayi_KeyPress);
        }

        private static void KayitGetir(string dosyaAdi, Type evTipi)
        {
            if (File.Exists(dosyaAdi))
            {
                string[] satirlar = File.ReadAllLines(dosyaAdi);
                foreach (string satir in satirlar)
                {
                    string[] bilgiler = satir.Split(',');
                    if (bilgiler[0] != "")
                    {
                        int odaSayisi = int.Parse(bilgiler[0]);
                        int katNumarasi = int.Parse(bilgiler[1]);
                        string il = bilgiler[2];
                        string semt = bilgiler[3];
                        double alani = double.Parse(bilgiler[4]);
                        int yapimTarihi = int.Parse(bilgiler[5]);
                        tur evTuru = (tur)Enum.Parse(typeof(tur), bilgiler[6]);
                        bool aktif = bool.Parse(bilgiler[7]);
                        int emlakNumarasi = int.Parse(bilgiler[8]);

                        if (evTipi.Name.ToString() == "KiralikEv")
                        {
                            double depozito = double.Parse(bilgiler[9]);
                            double kira = double.Parse(bilgiler[10]);
                            KiralikEv kiralikEv = new KiralikEv(odaSayisi, katNumarasi, il, semt, alani, yapimTarihi, evTuru, aktif, depozito, kira, emlakNumarasi);
                            evKayitlari[emlakNumarasi - 1] = kiralikEv;
                            evKayitlari[emlakNumarasi - 1].Aktif = aktif;
                        }
                        else if (evTipi.Name.ToString() == "SatilikEv")
                        {
                            double fiyat = double.Parse(bilgiler[9]);
                            SatilikEv satilikEv = new SatilikEv(odaSayisi, katNumarasi, il, semt, alani, yapimTarihi, evTuru, aktif, fiyat, emlakNumarasi);
                            evKayitlari[emlakNumarasi - 1] = satilikEv;
                            evKayitlari[emlakNumarasi - 1].Aktif = aktif;
                        }
                    }
                }
                File.WriteAllText(dosyaAdi, string.Empty);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
            textBox5.Visible = false;
            label8.Visible = true;
            textBox6.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label7.Visible = true;
            textBox5.Visible = true;
            label8.Visible = false;
            textBox6.Visible = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int odaSayisi = 0;
            int katNumarasi = 0;
            int alani = 0;
            int yapimTarihi = 0;
            try
            {
                odaSayisi = Convert.ToInt32(textBox1.Text);
                katNumarasi = Convert.ToInt32(textBox2.Text);
                alani = Convert.ToInt32(textBox3.Text);
                yapimTarihi = Convert.ToInt32(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Alanlar Boş Olamaz");
            }
            tur turu = tur.Daire;
            string il;
            string semt = "";


            il = comboBox5.SelectedItem.ToString();
            semt = comboBox1.SelectedItem.ToString();
            switch (comboBox2.SelectedIndex)
            {
                case 0: turu = tur.Daire; break;
                case 1: turu = tur.Bahceli; break;
                case 2: turu = tur.Dubleks; break;
                case 3: turu = tur.Mustakil; break;
            }
            if (radioButton1.Checked)
            {
                int depozito = 0;
                try
                {
                    depozito = Convert.ToInt32(textBox5.Text);
                }
                catch
                {
                    MessageBox.Show("Depozito Boş Olamaz");
                }
                KiralikEv kiralikEv = new KiralikEv(odaSayisi, katNumarasi, il, semt, alani, yapimTarihi, turu, true, depozito, 0, emlakNo);

                //KiralikEv ev = new KiralikEv(
                //    odaSayisi,
                //    katNumarasi,
                //    semt,
                //    alani,
                //    yapimTarihi,
                //    turu,
                //    true,
                //    depozito,
                //    0
                //);
                string kayıtEv = $"{kiralikEv.OdaSayisi},{kiralikEv.KatNumarasi},{kiralikEv.Il},{kiralikEv.Semt},{kiralikEv.Alani},{kiralikEv.YapimTarihi},{kiralikEv.Turu},{kiralikEv.Aktif},{kiralikEv.Depozito},{kiralikEv.Kirasi},{kiralikEv.EmlakNumarasi}";
                evKayitlari[(kiralikEv.EmlakNumarasi - 1)] = kiralikEv;
                evKayitlari[(kiralikEv.EmlakNumarasi - 1)].Aktif = true;
            }
            else if (radioButton2.Checked)
            {
                int fiyat = 0;

                try
                {
                    fiyat = Convert.ToInt32(textBox6.Text);
                }
                catch
                {
                    MessageBox.Show("Fiyat Boş Olamaz");
                }
                SatilikEv satilikEv = new SatilikEv(odaSayisi, katNumarasi, il, semt, alani, yapimTarihi, turu, true, fiyat, emlakNo);

                //SatilikEv ev = new SatilikEv(
                //    odaSayisi,
                //    katNumarasi,
                //    semt,
                //    alani,
                //    yapimTarihi,
                //    turu,
                //    true,
                //    fiyat
                //);
                string kayıtEv = $"{satilikEv.OdaSayisi},{satilikEv.KatNumarasi},{satilikEv.Il},{satilikEv.Semt},{satilikEv.Alani},{satilikEv.YapimTarihi},{satilikEv.Turu},{satilikEv.Aktif},{satilikEv.Fiyati},{satilikEv.EmlakNumarasi}";
                evKayitlari[(satilikEv.EmlakNumarasi - 1)] = satilikEv;
                evKayitlari[(satilikEv.EmlakNumarasi - 1)].Aktif = true;

            }
            emlakNo++;
            File.WriteAllText(emlakFile, emlakNo.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string durum = "";
            if (radioButton4.Checked)
            {
                durum = "KiralikEv";
            }
            else
            {
                durum = "SatilikEv";
            }
            bool aktif = true;
            if (radioButton5.Checked)
            {
                aktif = false;
            }
            else
            {
                aktif = true;
            }
            int odaSayisi = Convert.ToInt32(textBox12.Text);
            string semt = "";
            string il = "";
            int alani = Convert.ToInt32(textBox10.Text);
            int yapimTarihi = Convert.ToInt32(textBox9.Text);
            semt = comboBox4.SelectedItem.ToString();
            il = comboBox6.SelectedItem.ToString();
            tur turu = tur.Daire;

            switch (comboBox3.SelectedIndex)
            {
                case 0: turu = tur.Daire; break;
                case 1: turu = tur.Bahceli; break;
                case 2: turu = tur.Dubleks; break;
                case 3: turu = tur.Mustakil; break;
            }

            if (il == "")
            {
                MessageBox.Show("İl Boş Olamaz");
            }
            else
            {
                foreach (var item in evKayitlari)
                {
                    if (item != null)
                    {
                        if (item.GetType().Name.ToString() == durum)
                        {
                            if (item.Il == il && item.Semt == semt)
                            {
                                if (item.OdaSayisi >= odaSayisi && item.Alani >= alani && item.YapimTarihi >= yapimTarihi && item.Turu == turu)
                                {
                                    int index = listBox1.Items.Add(item.EvBilgiGetir());
                                    indexMap[index] = item.EmlakNumarasi;
                                }
                            }
                        }
                    }

                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            var item = evKayitlari[(indexMap[selectedIndex] - 1)];
            textBox1.Text = item.OdaSayisi.ToString();
            textBox2.Text = item.KatNumarasi.ToString();
            comboBox1.SelectedItem = item.Semt;
            textBox3.Text = item.Alani.ToString();
            textBox4.Text = item.YapimTarihi.ToString();
            comboBox2.SelectedItem = item.Turu;

            if (item is SatilikEv satilikEv)
            {
                textBox6.Text = satilikEv.Fiyati.ToString();
                radioButton2.Checked = true;
                radioButton2_CheckedChanged(sender, e);
                MessageBox.Show(satilikEv.EvBilgiGetir());

            }
            else if (item is KiralikEv kiralikEv)
            {
                textBox5.Text = kiralikEv.Depozito.ToString();
                radioButton1.Checked = true;
                radioButton1_CheckedChanged(sender, e);
                MessageBox.Show(kiralikEv.EvBilgiGetir());
            }
            DialogResult sonuc = MessageBox.Show("Evin fotoğraflarını görmek ister misiniz?",
                                                  "Fotoğraflar",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                string klasorYolu = $@"Images\{item.EmlakNumarasi}";
                try
                {
                    Process.Start("explorer.exe", klasorYolu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Klasör açılamadı veya Klasör Yok: " + ex.Message);
                }
            }
            button1.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            var item = evKayitlari[(indexMap[selectedIndex] - 1)];

            try
            {
                item.OdaSayisi = int.Parse(textBox1.Text);
                item.KatNumarasi = int.Parse(textBox2.Text);
                item.Il = comboBox5.SelectedItem.ToString();
                item.Semt = comboBox1.SelectedItem.ToString();
                item.Alani = int.Parse(textBox3.Text);
                item.YapimTarihi = int.Parse(textBox4.Text);
                item.Turu = (tur)comboBox2.SelectedItem;

            }
            catch
            {
                MessageBox.Show("Alanlar Boş Olamaz");
            }

            if (item is SatilikEv satilikEv)
            {
                try
                {
                    satilikEv.Fiyati = int.Parse(textBox6.Text);
                }
                catch
                {
                    MessageBox.Show("Fiyat Boş Olamaz");
                }

            }
            else if (item is KiralikEv kiralikEv)
            {
                try
                {
                    kiralikEv.Depozito = int.Parse(textBox5.Text);
                }
                catch
                {
                    MessageBox.Show("Depozito Boş Olamaz");
                }
            }

            evKayitlari[(indexMap[selectedIndex] - 1)] = item;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            var item = evKayitlari[(indexMap[selectedIndex] - 1)];
            item.Aktif = !item.Aktif;
            evKayitlari[(indexMap[selectedIndex] - 1)] = item;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult result = MessageBox.Show("Formu kapatmak istiyor musunuz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            String kiralikEvTxt = "";
            String satilikEvTxt = "";
            if (result == DialogResult.Yes)
            {
                foreach (var item in evKayitlari)
                {
                    if (item != null)
                    {
                        if (item is KiralikEv kiralikEv)
                        {
                            string kayıtEv = $"{kiralikEv.OdaSayisi},{kiralikEv.KatNumarasi},{kiralikEv.Il},{kiralikEv.Semt},{kiralikEv.Alani},{kiralikEv.YapimTarihi},{kiralikEv.Turu},{kiralikEv.Aktif},{kiralikEv.EmlakNumarasi},{kiralikEv.Depozito},{kiralikEv.Kirasi},";
                            kiralikEvTxt += kayıtEv + "\n";
                        }
                        else if (item is SatilikEv satilikEv)
                        {
                            string kayıtEv = $"{satilikEv.OdaSayisi},{satilikEv.KatNumarasi},{satilikEv.Il},{satilikEv.Semt},{satilikEv.Alani},{satilikEv.YapimTarihi},{satilikEv.Turu},{satilikEv.Aktif},{satilikEv.EmlakNumarasi},{satilikEv.Fiyati}";
                            satilikEvTxt += kayıtEv + "\n";
                        }
                    }
                }

                File.WriteAllText(kiralik, kiralikEvTxt);
                File.WriteAllText(satilik, satilikEvTxt);

                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            var item = evKayitlari[(indexMap[selectedIndex] - 1)];
            evKayitlari[(indexMap[selectedIndex] - 1)] = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSayi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedIl = comboBox5.SelectedItem.ToString();
            string filePath = $"{selectedIl}.txt";


            if (File.Exists(filePath))
            {
                comboBox1.Items.Clear();
                string[] semtler = File.ReadAllLines(filePath);
                comboBox1.Items.AddRange(semtler);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Enabled = true;
            string selectedIl = comboBox6.SelectedItem.ToString();
            string filePath = $"{selectedIl}.txt";

            if (File.Exists(filePath))
            {
                comboBox4.Items.Clear();
                string[] semtler = File.ReadAllLines(filePath);
                comboBox4.Items.AddRange(semtler);
                comboBox4.SelectedIndex = 0;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamboFirstConfig
{
    public partial class Form1 : Form
    {
        const string CFG_FILE = "RamboFirst.ini";
        CultureInfo ci;

        public Form1()
        {
            InitializeComponent();
            ci = CultureInfo.InvariantCulture;
            try
            {
                using (StreamReader sr = new StreamReader(CFG_FILE, Encoding.Default))
                {
                    float vol;
                    int fps;
                    bool fps_lim;
                    float.TryParse(sr.ReadLine(), NumberStyles.Float, ci, out vol);
                    int.TryParse(sr.ReadLine(), out fps);
                    bool.TryParse(sr.ReadLine(), out fps_lim);
                    nm_volume.Value = (decimal)vol * 100;
                    nm_fps.Value = fps;
                    nm_fps.Enabled = fps_lim;
                    ch_fps.Checked = fps_lim;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error wile loading {CFG_FILE}:" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ch_fps_CheckedChanged(object sender, EventArgs e)
        {
            nm_fps.Enabled = ch_fps.Checked;
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(CFG_FILE, false, Encoding.Default))
                {
                    sw.WriteLine(((float)nm_volume.Value / 100).ToString(ci));
                    sw.WriteLine((int)nm_fps.Value);
                    sw.WriteLine(ch_fps.Checked);
                }
                MessageBox.Show("Successfully saved.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error wile loading {CFG_FILE}:" + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
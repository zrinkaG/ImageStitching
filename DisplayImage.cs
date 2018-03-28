using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stichingFirstOne
{
    public partial class DisplayImage : Form
    {
        public DisplayImage()
        {
            InitializeComponent();
        }

        internal void SetImage(string resultPath)
        {
            pbResult.Image = Image.FromFile(resultPath);
        }
    }
}

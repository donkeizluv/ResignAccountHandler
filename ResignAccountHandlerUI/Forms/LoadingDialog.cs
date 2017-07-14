using System.Drawing;
using System.Windows.Forms;

namespace ResignAccountHandlerUI.Forms
{
    public partial class LoadingDialog : Form
    {
        public Form DisableParent { get; set; }

        public LoadingDialog()
        {
            InitializeComponent();
        }

        public void CalculateCenterPosition()
        {
            StartPosition = FormStartPosition.Manual;
            int offset = 10;
            Point p = new Point(DisableParent.Left + DisableParent.Width / 2 - Width / 2 + offset, DisableParent.Top + DisableParent.Height / 2 - Height / 2 + offset);
            this.Location = p;
        }
    }
}
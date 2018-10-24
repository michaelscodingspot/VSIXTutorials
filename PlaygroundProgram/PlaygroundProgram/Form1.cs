using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaygroundProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormValidation().ValidateDetails(new FormDetails()
            {
                Age = 35,
                AgreeToTermsAndConditions = true,
                FirstName = "Bill",
                LastName = "Smith",
                Password = "aSafas234SDF2"
            }

                );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Discount().CalcDiscount();
        }
    }
}

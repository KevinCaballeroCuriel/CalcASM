using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathAssemblyInterface
{
    public partial class Calculator : Form
    {
        int memory = 0, lastNumber = 0;
        bool finished = false;
        Func<int, int, int> lastAction;

        [DllImport("MathAssembly.dll", EntryPoint = "Add", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(int a, int b);
        [DllImport("MathAssembly.dll", EntryPoint = "Divide", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Divide(int a, int b);
        [DllImport("MathAssembly.dll", EntryPoint = "Substract", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Substract(int a, int b);
        [DllImport("MathAssembly.dll", EntryPoint = "Multiply", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Multiply(int a, int b);
        [DllImport("MathAssembly.dll", EntryPoint = "Power", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Power(int a, int b);
        [DllImport("MathAssembly.dll", EntryPoint = "Absolute", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Absolute(int a);

        [DllImport("MathAssembly.dll", EntryPoint = "Factorial", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Factorial(int a);

        [DllImport("MathAssembly.dll", EntryPoint = "Sign", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sign(int a);
        public Calculator()
        {
            InitializeComponent();
        }

        private bool checkContent()
        {
            if (txtInput.Text == "" || txtInput.Text=="-")
            {
                return false;
            }
            return true;
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                txtInput.Text = Power(inputNumber, 2).ToString();
            }
        }

        private void btnCube_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                txtInput.Text = Power(inputNumber, 3).ToString();
            }
        }


        private void btnAbs_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                txtInput.Text = Absolute(inputNumber).ToString();
            }
        }

        private void btnFact_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                txtInput.Text = Factorial(inputNumber).ToString();
            }
        }

        private void btnSgn_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                txtInput.Text = Sign(inputNumber).ToString();
            }
        }


        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (finished)
            {
                finished = false;
                txtInput.Text = "";
            }
            txtInput.Text += ((Button)sender).Text;
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            txtInput.Text = memory.ToString();
        }

        private void btnMS_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                memory = inputNumber;
            }
        }

        private void btnMPlus_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                memory += inputNumber;
            }
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Length > 0)
            {
                txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length-1);
            }
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                DoLastOperation(inputNumber);
                lastAction = Divide;
                lblOperationStack.Text += inputNumber.ToString() + "/";
                txtInput.Text = "";
            }
            
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            lblOperationStack.Text = "";
            lastNumber = 0;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                DoLastOperation(inputNumber);
                lastAction = Multiply;
                lblOperationStack.Text += inputNumber.ToString() + "*";
                txtInput.Text = "";
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                DoLastOperation(inputNumber);
                lastAction = Substract;
                lblOperationStack.Text += inputNumber.ToString() + "-";
                txtInput.Text = "";
            }
            else
            {
                txtInput.Text = "-";
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                DoLastOperation(inputNumber);
                lastAction =Add;
                lblOperationStack.Text += inputNumber.ToString() + "+";
                txtInput.Text = "";
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                DoLastOperation(inputNumber);
                lastAction = null;
                finished = true;
                txtInput.Text = lastNumber.ToString();
                lblOperationStack.Text = "";
            }
        }

        private void btnAns_Click(object sender, EventArgs e)
        {
            txtInput.Text = lastNumber.ToString();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            if (checkContent())
            {
                int inputNumber = Convert.ToInt32(txtInput.Text);
                DoLastOperation(inputNumber);
                lastAction = Power;
                lblOperationStack.Text += inputNumber.ToString() + "^";
                txtInput.Text = "";
            }
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        private void DoLastOperation(int number)
        {
            if (lastAction == null)
            {
                lastNumber = number;
                return;
            }
            lastNumber = lastAction(lastNumber, number);
        }
    }

}

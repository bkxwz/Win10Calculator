using System;
using System.Windows.Forms;

namespace Win10Calculator
{
    public partial class CalculatorForm : Form
    {
        private double _firstNumber = 0;
        private double _secondNumber = 0;
        private string _operation = "";
        private bool _isOperationPerformed = false;
        private bool _isResultDisplayed = false;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (txtDisplay.Text == "0" || _isOperationPerformed || _isResultDisplayed)
            {
                txtDisplay.Text = button.Text;
                _isOperationPerformed = false;
                _isResultDisplayed = false;
            }
            else
            {
                txtDisplay.Text += button.Text;
            }
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (_isOperationPerformed)
            {
                _operation = button.Text;
                return;
            }

            if (!string.IsNullOrEmpty(_operation) && !_isResultDisplayed)
            {
                Calculate();
            }

            _firstNumber = double.Parse(txtDisplay.Text);
            _operation = button.Text;
            _isOperationPerformed = true;
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_operation))
                return;

            _secondNumber = double.Parse(txtDisplay.Text);
            Calculate();
            _operation = "";
            _isResultDisplayed = true;
        }

        private void Calculate()
        {
            double result = 0;

            switch (_operation)
            {
                case "+":
                    result = _firstNumber + _secondNumber;
                    break;
                case "-":
                    result = _firstNumber - _secondNumber;
                    break;
                case "×":
                    result = _firstNumber * _secondNumber;
                    break;
                case "÷":
                    if (_secondNumber != 0)
                        result = _firstNumber / _secondNumber;
                    else
                    {
                        MessageBox.Show("不能除以零！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        BtnClear_Click(null, null);
                        return;
                    }
                    break;
            }

            txtDisplay.Text = result.ToString();
            _firstNumber = result;
            _secondNumber = double.Parse(txtDisplay.Text);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            _firstNumber = 0;
            _secondNumber = 0;
            _operation = "";
            _isOperationPerformed = false;
            _isResultDisplayed = false;
        }

        private void BtnClearEntry_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
            else
            {
                txtDisplay.Text = "0";
            }
        }

        private void BtnNegate_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                if (txtDisplay.Text.StartsWith("-"))
                    txtDisplay.Text = txtDisplay.Text.Substring(1);
                else
                    txtDisplay.Text = "-" + txtDisplay.Text;
            }
        }

        private void BtnPercent_Click(object sender, EventArgs e)
        {
            double number = double.Parse(txtDisplay.Text);
            txtDisplay.Text = (number / 100).ToString();
        }

        private void BtnSquareRoot_Click(object sender, EventArgs e)
        {
            double number = double.Parse(txtDisplay.Text);
            if (number >= 0)
            {
                txtDisplay.Text = Math.Sqrt(number).ToString();
                _isResultDisplayed = true;
            }
            else
            {
                MessageBox.Show("负数不能开平方根！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSquare_Click(object sender, EventArgs e)
        {
            double number = double.Parse(txtDisplay.Text);
            txtDisplay.Text = (number * number).ToString();
            _isResultDisplayed = true;
        }

        private void BtnInverse_Click(object sender, EventArgs e)
        {
            double number = double.Parse(txtDisplay.Text);
            if (number != 0)
            {
                txtDisplay.Text = (1 / number).ToString();
                _isResultDisplayed = true;
            }
            else
            {
                MessageBox.Show("零没有倒数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDecimal_Click(object sender, EventArgs e)
        {
            if (_isOperationPerformed || _isResultDisplayed)
            {
                txtDisplay.Text = "0.";
                _isOperationPerformed = false;
                _isResultDisplayed = false;
            }
            else if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }
    }
}

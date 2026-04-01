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

        private static readonly Color _numberColor = Color.FromArgb(59, 59, 59);
        private static readonly Color _numberHover = Color.FromArgb(75, 75, 75);
        private static readonly Color _funcColor = Color.FromArgb(50, 50, 50);
        private static readonly Color _funcHover = Color.FromArgb(65, 65, 65);
        private static readonly Color _equalsColor = Color.FromArgb(76, 194, 255);
        private static readonly Color _equalsHover = Color.FromArgb(100, 210, 255);

        public CalculatorForm()
        {
            InitializeComponent();
            SetupButtonFeedback();
            SetupKeyboard();
        }

        private void SetupButtonFeedback()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.MouseDown += (s, e) => btn.BackColor = DarkenColor(btn.BackColor);
                    btn.MouseUp += (s, e) => btn.BackColor = GetOriginalColor(btn);
                    btn.MouseEnter += (s, e) => btn.BackColor = GetHoverColor(btn);
                    btn.MouseLeave += (s, e) => btn.BackColor = GetOriginalColor(btn);
                    btn.TabStop = false;
                }
            }
        }

        private void SetupKeyboard()
        {
            this.KeyPreview = true;
            this.KeyDown += CalculatorForm_KeyDown;
        }

        private void CalculatorForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                TriggerButton(btn0, e.KeyCode - Keys.D0);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                TriggerButton(btn0, e.KeyCode - Keys.NumPad0);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                btnSubtract.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Multiply || e.KeyCode == Keys.OemQuestion)
            {
                btnMultiply.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.Oem2)
            {
                btnDivide.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.OemPeriod)
            {
                btnEquals.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnClear.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                btnBackspace.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnClearEntry.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Oemcomma)
            {
                btnDecimal.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void TriggerButton(Button baseBtn, int digit)
        {
            Button[] numberBtns = { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
            if (digit >= 0 && digit <= 9)
                numberBtns[digit].PerformClick();
        }

        private static Color DarkenColor(Color c) => Color.FromArgb((int)(c.R * 0.7), (int)(c.G * 0.7), (int)(c.B * 0.7));

        private static Color GetHoverColor(Button btn)
        {
            if (btn.BackColor == _equalsColor) return _equalsHover;
            if (btn.BackColor == _numberColor) return _numberHover;
            if (btn.BackColor == _funcColor) return _funcHover;
            return btn.BackColor;
        }

        private static Color GetOriginalColor(Button btn)
        {
            if (btn.BackColor == _equalsHover) return _equalsColor;
            if (btn.BackColor == _numberHover) return _numberColor;
            if (btn.BackColor == _funcHover) return _funcColor;
            return btn.BackColor;
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
                        txtDisplay.Text = "Cannot divide by zero";
                        _operation = "";
                        _isResultDisplayed = true;
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
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            else
                txtDisplay.Text = "0";
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
                txtDisplay.Text = "Invalid input";
                _isResultDisplayed = true;
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
                txtDisplay.Text = "Cannot divide by zero";
                _isResultDisplayed = true;
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

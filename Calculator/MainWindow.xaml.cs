using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private double _result;
        private string _operation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ResultTextBox.Text += button.Content.ToString();
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultTextBox.Text, out _result))
            {
                Button button = (Button)sender;
                _operation = button.Content.ToString();
                ResultTextBox.Clear();
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double secondOperand;

            if (_operation == "^" && double.TryParse(ResultTextBox.Text, out secondOperand))
            {
                _result = Math.Pow(_result, secondOperand);
                ResultTextBox.Text = _result.ToString();
                _operation = string.Empty;
            }
            else if (double.TryParse(ResultTextBox.Text, out secondOperand))
            {
                switch (_operation)
                {
                    case "+":
                        _result += secondOperand;
                        break;
                    case "-":
                        _result -= secondOperand;
                        break;
                    case "*":
                        _result *= secondOperand;
                        break;
                    case "/":
                        if (secondOperand != 0)
                            _result /= secondOperand;
                        else
                            ResultTextBox.Text = "Ошибка! Деление на ноль.";
                        return;
                }
                ResultTextBox.Text = _result.ToString();
                _operation = string.Empty;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Clear();
            _result = 0;
            _operation = string.Empty;
        }

        private void TrigonometricFunction_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultTextBox.Text, out double angle))
            {
                Button button = (Button)sender;
                switch (button.Content.ToString())
                {
                    case "sin":
                        ResultTextBox.Text = Math.Sin(angle * Math.PI / 180).ToString();
                        break;
                    case "cos":
                        ResultTextBox.Text = Math.Cos(angle * Math.PI / 180).ToString();
                        break;
                    case "tan":
                        if (angle % 90 == 0 && angle % 180 != 0)
                            ResultTextBox.Text = "Ошибка: Тангенс не определен!";
                        else
                            ResultTextBox.Text = Math.Tan(angle * Math.PI / 180).ToString();
                        break;
                    case "cotan":
                        if (angle % 180 == 0)
                            ResultTextBox.Text = "Ошибка: Котангенс не определен!";
                        else
                            ResultTextBox.Text = (1 / Math.Tan(angle * Math.PI / 180)).ToString();
                        break;
                }
            }
            else
            {
                ResultTextBox.Text = "Ошибка: Некорректное число!";
            }
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultTextBox.Text, out double baseValue))
            {
                _result = baseValue;
                ResultTextBox.Clear();
                _operation = "^";
            }
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultTextBox.Text, out double value))
            {
                if (value < 0)
                {
                    ResultTextBox.Text = "Ошибка: Отрицательное число!";
                    return;
                }
                ResultTextBox.Text = Math.Sqrt(value).ToString();
            }
            else
            {
                ResultTextBox.Text = "Ошибка: Некорректное число!";
            }
        }
    }
}

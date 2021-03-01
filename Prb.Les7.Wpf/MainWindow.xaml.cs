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

namespace Prb.Les7.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoStartup();
        }
        void DoStartup()
        {
            // code voor opdracht 1
            for(int i = -10; i <= 10; i++)
            {
                cmbNumber1.Items.Add(i);
                cmbNumber2.Items.Add(i);
            }
            cmbNumber1.SelectedItem = 1;
            cmbNumber2.SelectedItem = 1;
            cmbCalculation.Items.Add("+");
            cmbCalculation.Items.Add("-");
            cmbCalculation.Items.Add("*");
            cmbCalculation.Items.Add("/");
            cmbCalculation.SelectedItem = "+";
            lblResult.Content = "";
            cmbNumber1.Focus();

            // code voor opdracht 2
            lstNumbers.Items.Clear();
            lblSum.Content = 0;

            // code voor opdracht 4
            lblAvgAmounts.Content = 0;
            lblCountAmounts.Content = 0;
            lblSumAmounts.Content = 0;

            // code voor opdracht 5
            dtpDayOfBirth.SelectedDate = new DateTime(1961, 11, 27);
            BuildBirthdayInfo();
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (cmbNumber1.SelectedItem == null) return;
            if (cmbNumber2.SelectedItem == null) return;
            if (cmbCalculation.SelectedItem == null) return;

            int number1 = (int)cmbNumber1.SelectedItem;
            int number2 = (int)cmbNumber2.SelectedItem;
            string calculation = cmbCalculation.SelectedItem.ToString();
            if(calculation == "+")
            {
                lblResult.Content = number1 + number2;
            }
            else if(calculation == "-")
            {
                lblResult.Content = number1 - number2;
            }
            else if (calculation == "*")
            {
                lblResult.Content = number1 * number2;
            }
            else
            {
                lblResult.Content = (1.0 * number1 / number2).ToString("0.00");
            }

        }

        Random rnd = new Random();
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int number = rnd.Next(0, 1001);
            lstNumbers.Items.Add(number);
            lblSum.Content = CalculateSum();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(lstNumbers.SelectedItem == null)
            {
                return;
            }
            int place = lstNumbers.SelectedIndex;
            lstNumbers.Items.RemoveAt(place);
            lblSum.Content = CalculateSum();
        }
        int CalculateSum()
        {
            int sum = 0;
            foreach(int number in lstNumbers.Items)
            {
                sum += number;
            }
            return sum;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            lstNumbers.Items.Clear();
            lblSum.Content = 0;
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            lstAnalyse.Items.Clear();
            foreach(char letter in txtText.Text)
            {
                if(letter == '\n' || letter == '\r')
                {
                    continue;
                }
                if(!IsAlreadyUsed(letter.ToString()))
                {
                    int numberOffApearances = 0;
                    foreach (char searchletter in txtText.Text)
                    {
                        if(searchletter == letter)
                        {
                            numberOffApearances++;
                        }
                    }
                    lstAnalyse.Items.Add($"{letter} : {numberOffApearances}");
                }
            }
        }
        bool IsAlreadyUsed(string letter)
        {
            foreach(string line in lstAnalyse.Items)
            {
                if(line.Substring(0,1) == letter)
                {
                    return true;
                }
            }
            return false;
        }

        decimal sumAmounts = 0M;
        decimal avgAmounts= 0M;
        int countAmounts = 0;
        private void btnAddAmount_Click(object sender, RoutedEventArgs e)
        {
            decimal newAmount;
            decimal.TryParse(txtAmount.Text, out newAmount);
            if(newAmount > 0M)
            {
                sumAmounts += newAmount;
                countAmounts++;
                avgAmounts = sumAmounts / countAmounts;

                lblSumAmounts.Content = sumAmounts;
                lblCountAmounts.Content = countAmounts;
                lblAvgAmounts.Content = avgAmounts;

                lstAmounts.Items.Add(newAmount);
                txtAmount.Text = "";
                txtAmount.Focus();
            }
        }

        private void btnRemoveAmount_Click(object sender, RoutedEventArgs e)
        {
            if(lstAmounts.SelectedItem == null)
            {
                return;
            }
            decimal selectedAmount = (decimal)lstAmounts.SelectedItem;
            sumAmounts -= selectedAmount;
            countAmounts--;
            avgAmounts = sumAmounts / countAmounts;

            lblSumAmounts.Content = sumAmounts;
            lblCountAmounts.Content = countAmounts;
            lblAvgAmounts.Content = avgAmounts;

            lstAmounts.Items.RemoveAt(lstAmounts.SelectedIndex);
        }

        private void dtpDayOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            BuildBirthdayInfo();
        }
        void BuildBirthdayInfo()
        {
            DateTime thisDate = DateTime.Today;
            DateTime firstDate = new DateTime(1, 1, 1);
            tblInfo.Text = "";
            DateTime birthday = (DateTime)dtpDayOfBirth.SelectedDate;
            tblInfo.Text += $"Geboortedatum = {birthday.ToString("dd/MM/yyyy")}\n";
            tblInfo.Text += $"Geboortedag = {birthday.ToString("dddd")}\n";
            tblInfo.Text += $"Geboortemaand = {birthday.ToString("MMMM")}\n";
            TimeSpan difference = thisDate - birthday;

            tblInfo.Text += $"Je bent { (firstDate + difference).Year - 1} jaar, ";
            tblInfo.Text += $"{(firstDate + difference).Month - 1} maand(en) en ";
            tblInfo.Text += $"{(firstDate + difference).Day - 1} dag(en)\n";

            DateTime hundredYears = birthday.AddYears(100);
            tblInfo.Text += $"Nog {(hundredYears - thisDate).Days} dagen naar je honderste ...\n";
            tblInfo.Text += $"Voor wat het waard is, je bent een {GetZodiac(birthday)}";


        }
        string GetZodiac(DateTime birthDate)
        {
            int month = birthDate.Month;
            int day = birthDate.Day;
            string zodiac = "";
            int together = month * 100 + day;
            if (together >= 121 && together <= 219)
                zodiac = "Waterman";
            else if (together <= 320)
                zodiac = "Vissen";
            else if (together <= 420)
                zodiac = "Ram";
            else if (together <= 520)
                zodiac = "Stier";
            else if (together <= 621)
                zodiac = "Tweelingen";
            else if (together <= 722)
                zodiac = "Kreeft";
            else if (together <= 823)
                zodiac = "Leeuw";
            else if (together <= 922)
                zodiac = "Maagd";
            else if (together <= 1023)
                zodiac = "Weegschaal";
            else if (together <= 1122)
                zodiac = "Schorpioen";
            else if (together <= 1221)
                zodiac = "Boogschutter";
            else
                zodiac = "Steenbok";
            return zodiac;
        }
    }
}

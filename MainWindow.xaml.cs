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

namespace Jk_Buh_Prof
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataTicket.examTickets.Add(new ExamTicket(1, 21));
            DataTicket.examTickets[0].Question = "Вопрос на засыпку";
            DataTicket.examTickets[0].Answer = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3"};

            DataTicket.examTickets.Add(new ExamTicket(1, 23));
            DataTicket.examTickets[1].Question = "Вопрос на засыпку 2";
            DataTicket.examTickets[1].Answer = new List<string> { "Ответ 1", "Ответ 2", "Ответ 3" };

        }

        private void ExitProgramm(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenEditForm(object sender, RoutedEventArgs e)
        {
            Window1 EditForm = new Window1();
            EditForm.ShowDialog();
        }

    }
}

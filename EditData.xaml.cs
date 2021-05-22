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
using System.Windows.Shapes;

namespace Jk_Buh_Prof
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ExamTicket ExamTicket { get; set; } 
        public Window1()
        {
            InitializeComponent();

            InitializeTreeView();
        }

        private void InitializeTreeView()
        {
            TreeQuestion.Items.Clear();

            var sections = DataTicket.examTickets.Select(t => t.Section).Distinct().ToList();

            foreach (int st in sections)
            {
                AddSectorTreeView(st);
            }
        }

        private void TreeViewItem_Group_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem SelectedTreeViewItem = (TreeViewItem)sender;
            SelectedTreeViewItem.Items.Clear();

            var questions = DataTicket.examTickets.Where(t => t.Section.ToString() == (string)SelectedTreeViewItem.Header).Select(t => t.Number);
            foreach(int qst in questions)
            {
                AddQuestionTreeView(qst, SelectedTreeViewItem);
            }
            DelSelection.IsEnabled = true;
            DellQuestion.IsEnabled = false;
        }

        private TreeViewItem AddSectorTreeView(int sector)
        {
            TreeViewItem treeViewItem = new TreeViewItem { Header = sector.ToString() };
            treeViewItem.Selected += TreeViewItem_Group_Selected;
            TreeQuestion.Items.Add(treeViewItem);
            return treeViewItem;
        }

        private void AddQuestionTreeView(int numberQst, TreeViewItem parents)
        {
            TreeViewItem treeViewItem = new TreeViewItem { Header = numberQst.ToString() };
            treeViewItem.Selected += TreeViewItem_Selected;
            parents.Items.Add(treeViewItem);
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {            
            e.Handled = true; //не передаем событие наверх
            TreeViewItem SelectedTreeViewItem = (TreeViewItem)sender;
            var parent = (TreeViewItem)SelectedTreeViewItem.Parent;
            ExamTicket = DataTicket.examTickets.Find(t => t.Section.ToString() == (string)parent.Header
                && t.Number.ToString() == (string)SelectedTreeViewItem.Header);

            if(ExamTicket == null)
            {
                ExamTicket = new ExamTicket((int)parent.Header, Int32.Parse((string)SelectedTreeViewItem.Header));
            }
            HeadingQuestion.Content = $"{ExamTicket.Section}.{ExamTicket.Number}";
            Question.Text = ExamTicket.Question;

            StackAnswer.Children.Clear();

            foreach(string st in ExamTicket.Answer)
            {
                TextBox textBox = new TextBox();
                textBox.Text = st;
                StackAnswer.Children.Add(textBox);
            }
            DelSelection.IsEnabled = false;
            DellQuestion.IsEnabled = true;
        }

        private void Add_Sector(object sender, RoutedEventArgs e)
        {
            int newSectionsNumber = 0;
            if (DataTicket.examTickets.Count() == 0)
                newSectionsNumber = 1;
            else
                newSectionsNumber = DataTicket.examTickets.Select(t => t.Section).Max() + 1;

            var newSector = AddSectorTreeView(newSectionsNumber);

            ExamTicket = new ExamTicket(newSectionsNumber, 1);
            DataTicket.examTickets.Add(ExamTicket);
            AddQuestionTreeView(1, newSector);
        }

        private void Dell_Sector(object sender, RoutedEventArgs e)
        {
            object selectedItem = TreeQuestion.SelectedItem;
            if(selectedItem != null)
            {
                TreeQuestion.Items.Remove(selectedItem);
                string indexSector = (string)((TreeViewItem)selectedItem).Header;              
                DataTicket.RemoveSector(Int32.Parse(indexSector));
            }

        }

        private void Add_Question(object sender, RoutedEventArgs e)
        {
            if (DataTicket.examTickets.Count() == 0)
            {
                Add_Sector(sender, e);
                return;
            }
            else
            {
                TreeViewItem SelectedTreeViewItem = (TreeViewItem)TreeQuestion.SelectedItem;

                if (SelectedTreeViewItem == null)
                    return;

                TreeViewItem parent;
                if (SelectedTreeViewItem.Parent is TreeView)
                    parent = SelectedTreeViewItem;
                else
                    parent = (TreeViewItem)SelectedTreeViewItem.Parent;


                string SectionNumber = (string)parent.Header;
                int newQuestionsNumber = DataTicket.examTickets.Where(t => t.Section.ToString() == SectionNumber).Select(c => c.Number).Max() + 1;

                DataTicket.examTickets.Add(new ExamTicket(Int32.Parse(SectionNumber), newQuestionsNumber));
                AddQuestionTreeView(newQuestionsNumber, parent);
            }
        }

        private void Del_Question(object sender, RoutedEventArgs e)
        {
            var SelectedTreeViewItem = (TreeViewItem)TreeQuestion.SelectedItem;
            if (SelectedTreeViewItem == null)
                return;

            var parent = (TreeViewItem)SelectedTreeViewItem.Parent;         

            string indexSector = (string)parent.Header;
            string indexQuestion = (string)SelectedTreeViewItem.Header;
            DataTicket.RemoveQuestion(Int32.Parse(indexSector), Int32.Parse(indexQuestion));
            parent.Items.Remove((object)SelectedTreeViewItem);
        }
    }
}

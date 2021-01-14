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
using VolunteersModel;
using System.Data.Entity;
using System.Data;

namespace Anghel_Otilia_Alexandra_Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        Add,
        Edit,
        Delete,
        Nothing
    }

    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        VolunteersEntitiesModel ctx = new VolunteersEntitiesModel();
        CollectionViewSource volunteerViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            volunteerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("volunteersViewSource1")));
            volunteerViewSource.Source = ctx.Volunteers.Local;
            ctx.Volunteers.Load();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Volunteers volunteers = null;
                try
                {
                    volunteers = new Volunteers()
                    { First_Name = first_NameTextBox.Text.Trim(),
                        Last_Name = last_NameTextBox.Text.Trim(),
                        Phone_number = phone_numberTextBox.Text.Trim(),
                        Date_of_birth = (System.DateTime)date_of_birthDatePicker.SelectedDate,
                        Member_status = member_statusTextBox.Text.Trim()
                    };
                    ctx.Volunteers.Add(volunteers);
                    volunteerViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Volunteers volunteers = null;
            try
            {
                volunteers = (Volunteers)volunteersDataGrid.SelectedItem;
                ctx.Volunteers.Remove(volunteers);
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            volunteerViewSource.View.Refresh();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            volunteerViewSource.View.MoveCurrentToNext();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            volunteerViewSource.View.MoveCurrentToPrevious();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Volunteers volunteers = null;
            try
            {
                volunteers = (Volunteers)volunteersDataGrid.SelectedItem;
                volunteers.First_Name = first_NameTextBox.Text.Trim();
                volunteers.Last_Name = last_NameTextBox.Text.Trim();
                volunteers.Phone_number = phone_numberTextBox.Text.Trim();
                volunteers.Date_of_birth = (System.DateTime)date_of_birthDatePicker.SelectedDate;
                volunteers.Member_status = member_statusTextBox.Text.Trim();
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            volunteerViewSource.View.Refresh();
            volunteerViewSource.View.MoveCurrentTo(volunteers);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            first_NameTextBox.Clear();
            last_NameTextBox.Clear();
            phone_numberTextBox.Clear();
            member_statusTextBox.Clear();
        }
    }
}

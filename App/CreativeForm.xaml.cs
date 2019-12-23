using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// Логика взаимодействия для CreativeForm.xaml
    /// </summary>
    public partial class CreativeForm : Window
    {
        public CreativeForm()
        {
            InitializeComponent();

            CurrentValue = new PairMasterSlave();
        }

        public CreativeForm(PairMasterSlave pair)
        {
            InitializeComponent();

            CurrentValue = pair;

            master.Text = CurrentValue.Master;
            slave.Text = CurrentValue.Slave;

            if (CurrentValue.NoDelete)
                noDelete.SelectedItem = noDelete.Items[1];
            else noDelete.SelectedItem = noDelete.Items[1];

            switch (CurrentValue.TypeLogger)
            {
                case LoggerTypes.Silent: logType.SelectedItem = logType.Items[0]; break;
                case LoggerTypes.Summary: logType.SelectedItem = logType.Items[1]; break;
                default: logType.SelectedItem = logType.Items[2]; break;
            }

        }

        public PairMasterSlave CurrentValue { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FindFolder(master);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FindFolder(slave);
        }

        private void FindFolder(System.Windows.Controls.TextBox infoBox)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.SelectedPath == null)
                    System.Windows.Forms.MessageBox.Show("Вы что-то ничего не выбрали");
                else infoBox.Text = dlg.SelectedPath;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (master.Text != string.Empty && slave.Text != string.Empty)
            {
                if (!Directory.Exists(master.Text) && !Directory.Exists(slave.Text))
                {
                    System.Windows.MessageBox.Show(string.Format("Папки " + master.Text + " и " + slave.Text + " не существует"));
                }
                else if (!Directory.Exists(master.Text))
                {
                    System.Windows.MessageBox.Show(string.Format("Папки " + master.Text + " не существует"));
                }
                else if (!Directory.Exists(slave.Text))
                {
                    System.Windows.MessageBox.Show(string.Format("Папки " + slave.Text + " не существует"));
                }
                else if(slave.Text==master.Text)
                {
                    System.Windows.MessageBox.Show(string.Format("Нельзя выбрать одну папку в качестве главной и подчиненной"));
                }
                else
                {
                    CurrentValue.Master = master.Text;
                    CurrentValue.Slave = slave.Text;
                    CurrentValue.NoDelete = Convert.ToBoolean(noDelete.SelectedIndex);

                    switch (logType.SelectedIndex)
                    {
                        case 0: CurrentValue.TypeLogger = LoggerTypes.Silent; break;
                        case 2: CurrentValue.TypeLogger = LoggerTypes.Verbose; break;
                        default: CurrentValue.TypeLogger = LoggerTypes.Summary; break;
                    }

                    DialogResult = true;
                }
            }
            else System.Windows.Forms.MessageBox.Show("Вы не выбрали папки.Проверьте поля Главная папка и Подчиненная папка");
        }
    }
}

using SyncLib;
using SyncLib.Seeker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private  string master;
        private  string slave;
        private  bool noDeleteOption;

        private BaseSeeker conflictSeeker;
        private  ILogger logger;

        private void Master_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.SelectedPath == null)
                    System.Windows.Forms.MessageBox.Show("Вы что-то ничего не выбрали");
                else master=dlg.SelectedPath;
            }
        }

        private void Slave_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.SelectedPath == null)
                    System.Windows.Forms.MessageBox.Show("Вы что-то ничего не выбрали");
                else slave = dlg.SelectedPath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((NoLog.IsChecked == true || Summary.IsChecked == true || Verbose.IsChecked == true) &&
                (NoDeleteOption.IsChecked == true || DeleteOption.IsChecked == true))
            {
                if (NoLog.IsChecked == true)
                { logger = LoggerFactory.GetLogger(LoggerType.Silent, txt); }
                if (Summary.IsChecked == true)
                { logger = LoggerFactory.GetLogger(LoggerType.Summary, txt); }
                else
                { logger = LoggerFactory.GetLogger(LoggerType.Verbose, txt); }
                if (NoDeleteOption.IsChecked == true)
                { noDeleteOption = true; }
                else { noDeleteOption = false; }
                conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(noDeleteOption, master, slave);

                conflictSeeker.GetMasterConflict().ForEach(x =>
                {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                conflictSeeker.GetSlaveConflicts().ForEach(x => {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                logger.Log();
            }
            else System.Windows.Forms.MessageBox.Show("Вы не все выбрали");
        }

    }
}

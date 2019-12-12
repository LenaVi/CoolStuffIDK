using SyncLib;
using SyncLib.Seeker;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            master = @"C:\Users\stalk\Documents\GitHub\Students\CoolStuffIDK\Sync\App\bin\Debug\master";
            slave = @"C:\Users\stalk\Documents\GitHub\Students\CoolStuffIDK\Sync\App\bin\Debug\slave";
            noDeleteOption = true;
            logger = LoggerFactory.GetLogger(LoggerType.Verbose, txt);
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
    }
}

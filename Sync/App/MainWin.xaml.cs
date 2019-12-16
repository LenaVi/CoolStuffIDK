using SyncLib;
using SyncLib.Seeker;
using System.Windows;
using System.Windows.Input;

namespace App
{
    /// <summary>
    /// Логика взаимодействия для MainWin.xaml
    /// </summary>
    public partial class MainWin : Window
    {


        public MainWin()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private BaseSeeker conflictSeeker;
        private PairMasterSlave pair;
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreativeForm form = new CreativeForm();

            if (form.ShowDialog() == true)
                data.Items.Add(form.CurrentValue);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(data.SelectedIndex != -1)
                data.Items.RemoveAt(data.SelectedIndex);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in data.Items)
            {
                pair = (PairMasterSlave)item;
                logger = LoggerFactory.GetLogger(pair.TypeLogger, log);

                conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(pair.NoDelete, pair.Master, pair.Slave);

                conflictSeeker.GetSlaveConflicts().ForEach(x => {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                logger.Log();
            }

            foreach (var item in data.Items)
            {
                pair = (PairMasterSlave)item;
                logger = LoggerFactory.GetLogger(pair.TypeLogger, log);

                conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(pair.NoDelete, pair.Master, pair.Slave);

                conflictSeeker.GetMasterConflict().ForEach(x => {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                logger.Log();
            }
        }

        private void Change(object sender, MouseButtonEventArgs e)
        {
            CreativeForm form = new CreativeForm((PairMasterSlave)data.SelectedItem);

            if (form.ShowDialog() == true)
            {
                int index = data.SelectedIndex;
                data.Items.Insert(index, form.CurrentValue);
                data.Items.RemoveAt(index + 1);
            }
        }
    }
}

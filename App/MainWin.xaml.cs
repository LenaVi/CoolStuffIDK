using SyncLib;
using SyncLib.Seeker;
using System.Windows;
using System.Windows.Input;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System;

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
            if (data.SelectedIndex != -1)
                data.Items.RemoveAt(data.SelectedIndex);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in data.Items)
            {
                pair = (PairMasterSlave)item;
                logger = LoggerFactory.GetLogger(pair.TypeLogger, log);

                conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(pair.NoDelete, pair.Master, pair.Slave);

                conflictSeeker.GetSlaveConflicts().ForEach(x =>
                {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                log.Text += string.Format("Синхронезируем " + pair.Master + " и " + pair.Slave +"\n");
                logger.Log();
            }

            foreach (var item in data.Items)
            {
                pair = (PairMasterSlave)item;
                logger = LoggerFactory.GetLogger(pair.TypeLogger, log);

                conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(pair.NoDelete, pair.Master, pair.Slave);

                conflictSeeker.GetMasterConflict().ForEach(x =>
                {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                log.Text += string.Format("Синхронезируем " + pair.Slave + " и " + pair.Master + "\n");
                logger.Log();
            }
        }

        private void Change(object sender, MouseButtonEventArgs e)
        {
            if (data.SelectedIndex != -1)
            {
                CreativeForm form = new CreativeForm((PairMasterSlave)data.SelectedItem);

                if (form.ShowDialog() == true)
                {
                    int index = data.SelectedIndex;
                    data.Items.Insert(index, form.CurrentValue);
                    data.Items.RemoveAt(index + 1);
                }
            }
            else System.Windows.MessageBox.Show("Вы не выбрали, что хотите изменить");
        }

        private void RemoveData_Click(object sender, RoutedEventArgs e)
        {
            data.Items.Clear();
        }

        private void RemoveLog_Click(object sender, RoutedEventArgs e)
        {
            log.Clear();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files(*.xml)|*.xml|All files(*.*)|*.xml";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.FileName == null)
                    System.Windows.MessageBox.Show("Вы что-то ничего не выбрали");
                else
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(string));

                    using (Stream fStream = File.OpenRead(dlg.FileName))
                    {
                        string logger =
                          (string)xmlFormat.Deserialize(fStream);
                        log.Text += string.Format(logger);
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(string));
            string fileName = string.Format("result{0}", DateTime.Now.Date.Day);

            using (Stream fStream = new FileStream(fileName + ".xml",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, log.Text);
            }
            System.Windows.Forms.MessageBox.Show("Сохранено в " + fileName);
        }
    }
}

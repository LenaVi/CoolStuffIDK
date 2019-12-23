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
using System.Collections.Generic;

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

            _data = new List<PairMasterSlave>();
        }

        private ILogger logger;
        private BaseSeeker conflictSeeker;
        private PairMasterSlave pair;
        private List<PairMasterSlave> _data;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreativeForm form = new CreativeForm();

            if (form.ShowDialog() == true)
            {
                bool isValid = true;

                foreach (var item in _data)
                {
                    if (item.Slave.Equals(form.CurrentValue.Slave) || item.Master.Equals(form.CurrentValue.Slave) ||
                        item.Slave.Equals(form.CurrentValue.Master))
                    {
                        isValid = false;

                        break;
                    }
                }

                if (isValid)
                {
                    data.Items.Add(form.CurrentValue);
                    _data.Add(form.CurrentValue);
                }
                else System.Windows.Forms.MessageBox.Show("К сожалению, выбранная вами подчиненная папка уже занята");
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (data.SelectedIndex != -1)
            {
                int index = data.SelectedIndex;

                data.Items.RemoveAt(index);
                _data.RemoveAt(index);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var pair in _data)
            {
                logger = LoggerFactory.GetLogger(pair.TypeLogger, log);

                conflictSeeker = ConflictSeekerFactory.GetConflictSeeker(pair.NoDelete, pair.Master, pair.Slave);

                conflictSeeker.GetSlaveConflicts().ForEach(x =>
                {
                    x.Accept(new ResolverVisitor());
                    x.Accept(logger);
                });

                log.Text += string.Format("Синхронезируем " + pair.Master + " и " + pair.Slave + "\n");
                logger.Log();
            }

            foreach (var pair in _data)
            {
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

                    _data.Insert(index, form.CurrentValue);
                    _data.RemoveAt(index + 1);
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

            dlg.Filter = "Text files(result*.xml)|result*.xml|All files(*.*)|result*.xml";

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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<PairMasterSlave>));

            string fileName = string.Format("scenario{0}", DateTime.Now.Date.Day);

            using (Stream fStream = new FileStream(fileName + ".xml",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, _data);
            }
            System.Windows.Forms.MessageBox.Show("Сохранено в " + fileName);

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Text files(scenario*.xml)|scenario*.xml|All files(*.*)|scenario*.xml";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.FileName == null)
                    System.Windows.MessageBox.Show("Вы что-то ничего не выбрали");
                else
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<PairMasterSlave>));

                    using (Stream fStream = File.OpenRead(dlg.FileName))
                    {
                        _data = (List<PairMasterSlave>)xmlFormat.Deserialize(fStream);
                    }

                    data.Items.Clear();

                    _data.ForEach(x => data.Items.Add(x));
                }
            }
        }
    }
}

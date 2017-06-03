using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TimerViewer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-AU");
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e) {
            var dlg = new StandardFileDialog();

            if(dlg.GetOpenFileName(".iif","Timer Files|*.iif")) {
                var file = dlg.FileName;

                var rows = IFFReader.ImportFile(file);

                // for now, just list billable items
                if ( ShowBilling ) {
                    this.Times.ItemsSource = rows;
                } else {
                    this.Times.ItemsSource = rows.Where(r => r.billable == true);
                }
            }
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        bool billing = true;
        private bool ShowBilling {
            get { return billing; }
            set {
                if(billing != value) {
                    billing = value;
                }
            }
        }

        private void Row_Click(object sender, MouseButtonEventArgs e) {
            var row = sender as DataGridRow;
            var rec = row.DataContext as IFFReader.TimerRec;

            Clipboard.SetText(rec.notes+Environment.NewLine);
        }
    }
}

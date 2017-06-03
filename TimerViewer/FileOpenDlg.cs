using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerViewer {

    public class StandardFileDialog {
        public bool GetSaveFileName(string AsName, string DefaultExt, string Filter) {
            var dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.FileName = AsName;
            dlg.DefaultExt = DefaultExt;
            dlg.Filter = Filter;

            var res = dlg.ShowDialog();

            if ( res == true ) {
                FileName = dlg.FileName;
                FilterIndex = dlg.FilterIndex;
                return true;
            }

            return false;
        }

        public bool GetOpenFileName(string DefaultExt, string Filter) {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = DefaultExt;
            dlg.Filter = Filter;

            var res = dlg.ShowDialog();

            if ( res == true ) {
                FileName = dlg.FileName;
                FilterIndex = dlg.FilterIndex;
                return true;
            }

            return false;
        }

        public string FileName {
            get;
            private set;
        }

        public int FilterIndex {
            get;
            private set;
        }
    }
}

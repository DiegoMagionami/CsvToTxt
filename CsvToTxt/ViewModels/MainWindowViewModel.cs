using CsvHelper;
using CsvToTxt.Entities;
using CsvToTxt.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CsvToTxt.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private string _destination;
        private List<FileModel> _files;
        private List<LogModel> _logs;
        
        public List<FileModel> Files
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }

        public string Destination
        {
            get { return _destination; }
            set { SetProperty(ref _destination, value); }
        }

        public List<LogModel> Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }
                        
        public DelegateCommand SelectedFiles { get; set; }
        public DelegateCommand SelectedDestination { get; set; }    
        public DelegateCommand ElaborateFile { get; set; }    

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _files = new List<FileModel>();
            SelectedFiles = new DelegateCommand(SelectFiles).ObservesProperty(() => Files);
            SelectedDestination = new DelegateCommand(SelectDestination).ObservesProperty(() => Destination);
            ElaborateFile = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => Files).ObservesProperty(() => Destination);
        }

        /// <summary>
        /// Open a dialog to select a list of files to be loaded
        /// </summary>
        private void SelectFiles()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel files (*.xls;*.xlsx;*.csv;)|*.xls;*.xlsx;*.csv;";
            List<FileModel> fileList = new List<FileModel>();

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                if (dlg.FileNames.Count() > 0)
                {
                    foreach (var fileName in dlg.FileNames)
                    {
                        fileList.Add(new FileModel
                        {
                            FileName = Path.GetFileName(fileName),
                            FilePath = Path.GetFullPath(fileName),
                            FileExtension = Path.GetExtension(fileName),
                            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName)
                        });
                    }

                    Files = fileList;                                                            
                }
            }
        }

        /// <summary>
        /// Open a dialog to select a destination for the processed files
        /// </summary>
        private void SelectDestination()
        {
            // Create OpenFileDialog 
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            DialogResult result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Destination = dlg.SelectedPath;
            }
        }

        /// <summary>
        /// Check if files and the destination folder have been selected
        /// </summary>
        /// <returns>Boolean</returns>
        private bool CanExecute()
        {
            if(Files.Any() && !String.IsNullOrWhiteSpace(Destination))
            {
                return true;
            }

            return false;            
        }
        
        /// <summary>
        /// Loop the selected files and write each row on a .txt file, 
        /// </summary>
        private void Execute()
        {
            List<LogModel> logList = new List<LogModel>();
            
            foreach (var file in Files)
            {
                using (TextReader reader = File.OpenText(file.FilePath))
                {
                    var csv = new CsvReader(reader);
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.QuoteNoFields = true;
                    int counter = 0;

                    using (StreamWriter outputFile = new StreamWriter(Destination + @"\" + file.FileNameWithoutExtension + DateTime.UtcNow.ToString("_yyyyMMddHHmmssfff") + ".txt"))
                    {
                        while (csv.Read())
                        {
                            if (counter > 6)
                            {
                                try
                                {
                                    var csvFileRow = new CsvFileRowModel
                                    {
                                        // Get the fields you need and make some operations with them
                                        Col1 = csv.GetField<string>(0),
                                        Col2 = csv.GetField<string>(1).Replace("=", "").Replace("\"", ""),
                                        Col3 = csv.GetField<string>(2),
                                        Col4 = csv.GetField<string>(3).Replace("CAT", "").Trim(),
                                        Col5 = csv.GetField<string>(4).Replace("=", "").Replace("\"", "")
                                    };
                                    var merged = csvFileRow.Col1 + csvFileRow.Col2 + csvFileRow.Col4;
                                    // Write the row in the output file
                                    outputFile.WriteLine(merged.PadRight(19, ' ') + csvFileRow.Col3.PadRight(60, ' ') + csvFileRow.Col5);
                                }
                                catch (Exception ex)
                                {
                                    logList.Add(new LogModel
                                    {
                                        Time = DateTime.Now,
                                        Message = "Error at row " + csv.Row + " of file " + file.FileName 
                                    });
                                }
                            }
                            counter++;
                        }
                    }
                }
            }

            if (logList.Count == 0)
            {
                logList.Add(new LogModel
                {
                    Time = DateTime.Now,
                    Message = "All files have been processed correctly"
                });
            }
            Logs = logList;           
        }        
    }
}

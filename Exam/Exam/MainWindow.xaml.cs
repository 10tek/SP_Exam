using Exam.Domain;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isCounting = false;
        private StringBuilder stringBuilder = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnClick(object sender, RoutedEventArgs e)
        {
            if (isCounting) return;
            isCounting = true;
            for (var i = 0; i < 1000; i++)
            {
                await Task.Run(() => stringBuilder.Append($" {i}"));
            }
            var result = new Result { Text = stringBuilder.ToString() };

            using (var context = new ExamContext())
            {
                context.Add(result);
                await context.SaveChangesAsync();
            }

            var path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Documents\CounterResults";
            Directory.CreateDirectory(path);
            path += @$"\{result.CreationDate.ToString("ddMMyyyy - hhmmss")}.txt";

            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(result.CreationDate.ToString());
                await sw.WriteLineAsync(result.Text);
            }

            MessageBox.Show(stringBuilder.ToString());
            stringBuilder.Clear();
            isCounting = false;
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                BtnClick(sender, e);
            }
        }
    }
}

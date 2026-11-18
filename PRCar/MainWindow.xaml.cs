using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace PRCar;

public partial class MainWindow : Window
{
    private static readonly string ApplicationsFolder = @"C:\CarApplications";
    private readonly CarConfiguration _configuration = new();
    private readonly string[] _titles = { "Модель и двигатель", "Цвет и опции", "Стоимость", "Кредит", "Заявка" };
    private int _step;
    public MainWindow()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
        InitializeComponent();
        DataContext = _configuration;
    }
    private void GoNext(object sender, RoutedEventArgs e)
    {
        ChangeStep(_step + 1);
    }
    private void GoBack(object sender, RoutedEventArgs e)
    {
        if (_step == 4 && !_configuration.ContactsValid &&
            MessageBox.Show(this, "Контакты заполнены не полностью. Вернуться назад? Введённые данные сохранятся при переходе, но будут потеряны при закрытии приложения.", "Незавершённая заявка", MessageBoxButton.YesNo,MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes)
        {
            return;
        }
        ChangeStep(_step - 1);
    }
    private void ChangeStep(int step)
    {
        _step = Math.Clamp(step, 0, 4);
        Steps.SelectedIndex = _step;
        StepTitle.Text = $"Шаг {_step + 1} из 5 · {_titles[_step]}";
        Progress.Value = _step + 1;
        BackButton.IsEnabled = _step > 0;
        NextButton.Visibility = _step == 4 ? Visibility.Collapsed : Visibility.Visible;
        SubmitButton.Visibility = _step == 4 ? Visibility.Visible : Visibility.Collapsed;
        NextButton.Content = _step == 3 ? "Оформить заявку" : "Далее";
    }
    private void Submit(object sender, RoutedEventArgs e)
    {
        if (!_configuration.ContactsValid) return;
        try
        {
            Directory.CreateDirectory(ApplicationsFolder);
            string file = Path.Combine(ApplicationsFolder, $"Заявка-{Guid.NewGuid():N}.txt");
            File.WriteAllText(file, $"Заявка от {DateTime.Now:g}\nИмя — {_configuration.Name.Trim()}\n" + $"Телефон — {_configuration.Phone}\nПочта — {_configuration.Email}\n\n" + _configuration.CarSummary + "\n\n" + _configuration.LoanSummary);
            MessageBox.Show(this, $"Заявка оформлена и сохранена на этом компьютере.\n\n{file}", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(this, "Не удалось сохранить заявку. Проверьте доступ к папке данных приложения.", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private void WindowClosing(object? sender, CancelEventArgs e)
    {
        if (_step == 4 && !_configuration.ContactsValid)
        {
            e.Cancel = MessageBox.Show(this, "Заявка заполнена не полностью. При закрытии введённые данные будут потеряны. Закрыть приложение?", "Незавершённая заявка", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes;
        }
    }
}

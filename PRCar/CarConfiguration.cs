using System.ComponentModel;
using System.Text.RegularExpressions;
namespace PRCar;
public record PriceItem(string Name, decimal Price)
{
    public override string ToString() => $"{Name} — {Price:N0} ₽";
}
public class CarConfiguration : INotifyPropertyChanged
{
    public List<PriceItem> Models { get; } = new()
    {
        new("Городской", 1_500_000),
        new("Седан", 2_000_000),
        new("Кроссовер", 2_800_000)
    };
    public List<PriceItem> Engines { get; } = new()
    {
        new("Бензиновый", 0),
        new("Дизельный", 150_000),
        new("Гибридный", 350_000)
    };
    public List<PriceItem> Colors { get; } = new()
    {
        new("Белый", 0),
        new("Чёрный", 30_000),
        new("Синий", 45_000)
    };
    private PriceItem _model;
    private PriceItem _engine;
    private PriceItem _color;
    private bool _climate, _camera, _heatedSeats;
    private int _downPercent = 20, _months = 36;
    private string _name = "", _phone = "", _email = "";
    public CarConfiguration()
    {
        _model = Models[0];
        _engine = Engines[0];
        _color = Colors[0];
    }
    public PriceItem Model { get => _model; set { _model = value; Refresh(); } }
    public PriceItem Engine { get => _engine; set { _engine = value; Refresh(); } }
    public PriceItem Color { get => _color; set { _color = value; Refresh(); } }
    public bool Climate { get => _climate; set { _climate = value; Refresh(); } }
    public bool Camera { get => _camera; set { _camera = value; Refresh(); } }
    public bool HeatedSeats { get => _heatedSeats; set { _heatedSeats = value; Refresh(); } }
    public int DownPercent { get => _downPercent; set { _downPercent = Math.Clamp(value, 0, 100); Refresh(); } }
    public int Months { get => _months; set { _months = Math.Clamp(value, 12, 96); Refresh(); } }
    public string Name { get => _name; set { _name = value; Refresh(); } }
    public string Phone { get => _phone; set { _phone = value; Refresh(); } }
    public string Email { get => _email; set { _email = value; Refresh(); } }
    public decimal OptionsPrice => (Climate ? 70_000 : 0) + (Camera ? 40_000 : 0) + (HeatedSeats ? 30_000 : 0);
    public decimal Total => Model.Price + Engine.Price + Color.Price + OptionsPrice;
    public decimal DownPayment => Total * DownPercent / 100;
    public decimal Loan => Total - DownPayment;
    public decimal Interest => Loan * 0.075m * Months / 12;
    public decimal MonthlyPayment => (Loan + Interest) / Months;
    public bool ContactsValid => Name.Trim().Length >= 2 && Phone.Length is >= 10 and <= 15 && Phone.All(char.IsAsciiDigit) && Email.Length >= 5 && Regex.IsMatch(Email, @"^[^@\s]+@[^@\s.]+(?:\.[^@\s.]+)+$");
    public string OptionsText
    {
        get
        {
            var selected = new List<string>();
            if (Climate) selected.Add("Климат-контроль");
            if (Camera) selected.Add("Камера заднего вида");
            if (HeatedSeats) selected.Add("Подогрев сидений");
            return selected.Count == 0 ? "Нет" : string.Join(", ", selected);
        }
    }
    public string CarSummary => $"Модель — {Model.Name}\nДвигатель — {Engine.Name}\nЦвет — {Color.Name}\nОпции — {OptionsText}\n\n" + $"Базовая цена — {Model.Price:N0} ₽\nДвигатель — {Engine.Price:N0} ₽\nЦвет — {Color.Price:N0} ₽\n" + $"Опции — {OptionsPrice:N0} ₽\nИтого за автомобиль — {Total:N0} ₽";
    public string LoanSummary => $"Первоначальный взнос ({DownPercent}%) — {DownPayment:N2} ₽\n" + $"Сумма кредита — {Loan:N2} ₽\nСрок — {Months} мес.\nСтавка — 7,5% годовых\n" + $"Проценты за весь срок — {Interest:N2} ₽\nЕжемесячный платёж — {MonthlyPayment:N2} ₽";
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Refresh() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
}

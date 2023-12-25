using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CarRental.Scripts;
using CarRental.Scripts.Database;
using EasyDox;

namespace CarRental.Windows.Cars
{
  /// <summary>
  /// Логика взаимодействия для SearchOrderCarWindows.xaml
  /// </summary>
  public partial class SearchOrderCarWindows : Window
  {
    /// <summary>
    /// BlurEffect окна родителя
    /// </summary>
    public BlurEffect BlurEffect { get; }

    /// <summary>
    /// Начальное размытие
    /// </summary>
    public double LastBlurRadius { get; }

    public SearchOrderCarWindows(Window parOwner)
    {
      InitializeComponent();

      Clear();

      Owner = parOwner;
      ShowInTaskbar = false;

      if (Owner != null)
      {
        if (Owner.Effect is BlurEffect blurEffect)
        {
          BlurEffect = blurEffect;
          LastBlurRadius = BlurEffect.Radius;
          BlurEffect.Radius = 5;
        }
      }

      PassportDateOfIssued.Text = DateTime.Now.ToString();
      CurrentDate.Text = DateTime.Now.ToString();
      LastDate.Text = DateTime.Now.ToString();

      ButtonClose.Click += (parSender, parArgs) => ButtonClose_Click();

      ButtonBuy.Click += (parSender, parArgs) => ButtonBuy_Click();

      FullName.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = IsInt(parArgs.Text);
      Telephone.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
      PassportSeries.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
      PassportNumber.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
      Mileage.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
      Fuel.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
    }

    private bool IsInt(string parAddText)
    {
      return char.IsDigit(parAddText, 0);
    }

    private void ButtonClose_Click()
    {
      BlurEffect.Radius = LastBlurRadius;
      Hide();
    }

    private void ButtonBuy_Click()
    {
      if (StringNull(FullName.Text) && StringNull(Telephone.Text) && StringNull(PassportSeries.Text) && StringNull(PassportNumber.Text)
        && StringNull(PassportIssued.Text) && StringNull(NameCar.Text) && StringNull(CurrentDate.Text) && StringNull(LastDate.Text)
        && StringNull(PlaceOfIssue.Text) && StringNull(Mileage.Text) && StringNull(Fuel.Text))
      {
        DateTime currentDate = CurrentDate.SelectedDate.Value.Date;
        DateTime lastDate = LastDate.SelectedDate.Value.Date;

        if (LastMoreCurrentDate(currentDate, lastDate))
        {
          AddContract();
          Dogovor();
          Clear();
          ButtonClose_Click();
        }
        else
          MessageBox.Show("Ошибка: Текущая дата не может быть меньше последней!");
      }
      else
      {
        Warning(FullName.Text, FullName);
        Warning(Telephone.Text, Telephone);
        Warning(PassportSeries.Text, PassportSeries);
        Warning(PassportNumber.Text, PassportNumber);
        Warning(PassportIssued.Text, PassportIssued);
        Warning(NameCar.Text, NameCar);
        Warning(PlaceOfIssue.Text, PlaceOfIssue);
        Warning(Mileage.Text, Mileage);
        Warning(Fuel.Text, Fuel);
      }
    }

    public void AddContract()
    {
      string fio = FullName.Text;
      string telephone = Telephone.Text;
      string passportSeries = PassportSeries.Text;
      string passportNumber = PassportNumber.Text;
      string passportDateOfIssued = PassportDateOfIssued.Text;
      string passportIssued = PassportIssued.Text;
      string currentDate = CurrentDate.Text;
      string lastDate = LastDate.Text;
      string nameCar = NameCar.Text;
      string placeOfIssue = PlaceOfIssue.Text;
      string mileage = Mileage.Text;
      string fuel = Fuel.Text;
      int price = Convert.ToInt32(Price.Text.Remove(Price.Text.IndexOf('₽')));
      int carId = Convert.ToInt32(TextBlockCarId.Text);

      string orderStatus = "";
      if (Convert.ToDateTime(lastDate) >= DateTime.Now)
        orderStatus = "АКТИВЕН";
      else if (Convert.ToDateTime(lastDate) < DateTime.Now)
        orderStatus = "ЗАКРЫТ";

      var contract = new Contract(fio, telephone, passportSeries, passportNumber, passportDateOfIssued, passportIssued, Convert.ToDateTime(currentDate),
        Convert.ToDateTime(lastDate), nameCar, placeOfIssue, mileage, fuel, price, carId, orderStatus);

      DatabaseTableContract.AddContract(contract);
    }

    private bool StringNull(string parText)
    {
      return !string.IsNullOrEmpty(parText);
    }

    /// <summary>
    /// Проверка на заполнение полей (TextBox)
    /// </summary>
    private void Warning(string text, TextBox textBox)
    {
      textBox.BorderBrush = text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : new SolidColorBrush(Color.FromRgb(241, 173, 113));
    }

    /// <summary>
    /// Договор
    /// </summary>
    private void Dogovor()
    {
      string fio = FullName.Text;
      string passportSeries = PassportSeries.Text;
      string passportNumber = PassportNumber.Text;
      string passportDateOfIssued = PassportDateOfIssued.Text;
      string passportIssued = PassportIssued.Text;
      string currentDate = CurrentDate.Text;
      string lastDate = LastDate.Text;
      string nameCar = NameCar.Text;
      int price = Convert.ToInt32(Price.Text.Remove(Price.Text.IndexOf('₽')));

      var fieldValues = new Dictionary<string, string>
      {
          {"Текущая_дата", currentDate},
          {"ФИО", fio},
          {"Паспорт_серия", passportSeries},
          {"Паспорт_номер", passportNumber},
          {"Паспорт_выдан", passportDateOfIssued},
          {"Проживающий_по_адресу", passportIssued},
          {"Название_машины", nameCar},
          {"Сумма_аренды", price.ToString()},
          {"Конечная_дата", lastDate},
      };

      using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
      {
        saveFileDialog.Title = "Сохранение документа";
        saveFileDialog.Filter = "Word: (*.docx)|*.docx|(*.doc)|*.doc";

        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          var engine = new Engine();

          engine.Merge("Договор.docx", fieldValues, saveFileDialog.FileName);
        }
      }
    }

    public void Clear()
    {
      FullName.Clear();
      Telephone.Clear();
      PassportSeries.Clear();
      PassportNumber.Clear();
      PassportIssued.Clear();
      NameCar.Clear();
      PlaceOfIssue.Clear();
      Mileage.Clear();
      Fuel.Clear();

      PassportDateOfIssued.Text = DateTime.Now.ToString();
      CurrentDate.Text = DateTime.Now.ToString();
      LastDate.Text = DateTime.Now.ToString();
    }

    private void NumberDays()
    {
      DateTime currentDate = CurrentDate.SelectedDate.Value.Date;
      DateTime lastDate = LastDate.SelectedDate.Value.Date;

      if (LastMoreCurrentDate(currentDate, lastDate))
      {
        int currentPrice = Convert.ToInt32(CurrentPriceText.Text);

        int lastPrice = currentPrice;

        if (lastPrice < 0)
          return;

        if (lastDate == currentDate)
        {
          Price.Text = Convert.ToString($"{currentPrice}₽");
          return;
        }

        int day = (lastDate - currentDate).Days;

        if (lastPrice > 0)
          Price.Text = $"{Convert.ToString(lastPrice *= day + 1)}₽";
        else
          MessageBox.Show("Ошибка: Оплата не может быть отрицательной");
      }
      else
        MessageBox.Show("Ошибка: Текущая дата не может быть меньше последней");
    }

    /// <summary>
    /// Проверка на дату
    /// </summary>
    private bool LastMoreCurrentDate(DateTime currentDate, DateTime lastDate)
    {
      return lastDate >= currentDate;
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      NumberDays();
    }
  }
}
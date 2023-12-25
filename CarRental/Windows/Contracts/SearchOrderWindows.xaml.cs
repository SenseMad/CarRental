using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CarRental.Scripts;
using CarRental.Scripts.Database;
using CarRental.Windows.Contracts;
using EasyDox;

namespace CarRental.Windows.Cars
{
  /// <summary>
  /// Логика взаимодействия для SearchOrderWindows.xaml
  /// </summary>
  public partial class SearchOrderWindows : Window
  {
    /// <summary>
    /// BlurEffect окна родителя
    /// </summary>
    public BlurEffect BlurEffect { get; }

    /// <summary>
    /// Начальное размытие
    /// </summary>
    public double LastBlurRadius { get; }

    public SearchOrderWindows(Window parOwner)
    {
      InitializeComponent();

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

      ButtonClose.Click += (parSender, parArgs) => ButtonClose_Click();

      Print.Click += (parSender, parArgs) => Dogovor();

      ButtonEdit.Click += (parSender, parArgs) => ButtonEdit_Click();

      ButtonCloseOrder.Click += (parSender, parArgs) => ButtonCloseOrder_Click();

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
      Clear();
      Hide();
    }

    private void ButtonCloseOrder_Click()
    {
      if (OrderText.Text == "АКТИВЕН")
      {
        OrderText.Text = "ЗАКРЫТ";
        BorderOrder.BorderBrush = new SolidColorBrush(Colors.Red);
        OrderText.Foreground = new SolidColorBrush(Colors.Red);
        ButtonCloseOrder.Content = "АКТИВ. ЗАКАЗ";
      }
      else if (OrderText.Text == "ЗАКРЫТ")
      {
        OrderText.Text = "АКТИВЕН";
        BorderOrder.BorderBrush = new SolidColorBrush(Colors.Green);
        OrderText.Foreground = new SolidColorBrush(Colors.Green);
        ButtonCloseOrder.Content = "ЗАКРЫТЬ ЗАКАЗ";
      }
    }

    private void ButtonEdit_Click()
    {
      if (StringNull(FullName.Text) && StringNull(Telephone.Text) && StringNull(PassportSeries.Text) && StringNull(PassportNumber.Text) 
        && StringNull(PassportIssued.Text) && StringNull(NameCar.Text) && StringNull(CurrentDate.Text) && StringNull(LastDate.Text) 
        && StringNull(PlaceOfIssue.Text) && StringNull(Mileage.Text) && StringNull(Fuel.Text))
      {
        UpdateContract();
        Clear();
        ButtonClose_Click();
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

    public void UpdateContract()
    {
      int id = Convert.ToInt32(ContractId.Text);
      string fio = FullName.Text;
      string telephone = Telephone.Text;
      string passportSeries = PassportSeries.Text;
      string passportNumber = PassportNumber.Text;
      string passportDateOfIssued = PassportDateOfIssued.Text;
      string passportIssued = PassportIssued.Text;
      string currentDate = CurDate.Text;
      string lastDate = LasDate.Text;
      string nameCar = NameCar.Text;
      string placeOfIssue = PlaceOfIssue.Text;
      string mileage = Mileage.Text;
      string fuel = Fuel.Text;
      int price = Convert.ToInt32(Price.Text.Remove(Price.Text.IndexOf('₽')));
      int carId = Convert.ToInt32(TextBlockCarId.Text);
      string orderStatus = OrderText.Text;

      var contract = new Contract(id, fio, telephone, passportSeries, passportNumber, passportDateOfIssued, passportIssued, Convert.ToDateTime(currentDate),
        Convert.ToDateTime(lastDate), nameCar, placeOfIssue, mileage, fuel, price, carId, orderStatus);

      DatabaseTableContract.UpdateContract(contract);
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
    }
  }
}
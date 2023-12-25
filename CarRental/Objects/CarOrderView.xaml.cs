using CarRental.Scripts;
using CarRental.Scripts.Database;
using CarRental.Windows.Cars;
using System;
using System.Windows;
using System.Windows.Media;

namespace CarRental.Objects
{
  /// <summary>
  /// Логика взаимодействия для CarOrderView.xaml
  /// </summary>
  public partial class CarOrderView
  {
    private Contract _contract;

    /// <summary>
    /// Заказаные машины
    /// </summary>
    public Contract Contract
    {
      get => _contract;
      set
      {
        _contract = value;
        Update();
      }
    }

    public CarOrderView(Contract parContract)
    {
      InitializeComponent();

      Contract = parContract;

      ButtonInform.Click += (parSender, parArgs) => ButtonInform_Click();
    }

    //=====================================================

    public void Update()
    {
      if (Contract == null) 
        return;

      NameCar.Text = $"{Contract.NameCar}";
      Order.Text = $"{Contract.OrderStatus}";

      if(Contract.OrderStatus == "ЗАКРЫТ")
      {
        Border_Red();
      }
      else if (Contract.OrderStatus == "АКТИВЕН")
      {
        Border_Green();
      }
    }

    private void Border_Green()
    {
      BorderOrder.BorderBrush = new SolidColorBrush(Colors.Green);
      Order.Foreground = new SolidColorBrush(Colors.Green);
    }

    private void Border_Red()
    {
      BorderOrder.BorderBrush = new SolidColorBrush(Colors.Red);
      Order.Foreground = new SolidColorBrush(Colors.Red);
    }

    private void ButtonInform_Click()
    {
      SearchOrderWindows searchOrderWindows = new SearchOrderWindows(Application.Current.MainWindow);

      searchOrderWindows.ContractId.Text = $"{Contract.Id}";
      searchOrderWindows.FullName.Text = $"{Contract.FIO}";
      searchOrderWindows.Telephone.Text = $"{Contract.Telephone}";
      searchOrderWindows.PassportSeries.Text = $"{Contract.Series}";
      searchOrderWindows.PassportNumber.Text = $"{Contract.Number}";
      searchOrderWindows.PassportDateOfIssued.Text = $"{Contract.DateOfIssued}";
      searchOrderWindows.PassportIssued.Text = $"{Contract.Issued}";
      searchOrderWindows.CurrentDate.Text = $"{Contract.CurrentDate}";
      searchOrderWindows.CurDate.Text = $"{Contract.CurrentDate}";
      searchOrderWindows.LastDate.Text = $"{Contract.LastDate}";
      searchOrderWindows.LasDate.Text = $"{Contract.LastDate}";
      searchOrderWindows.NameCar.Text = $"{Contract.NameCar}";
      searchOrderWindows.PlaceOfIssue.Text = $"{Contract.PlaceOfIssue}";
      searchOrderWindows.Mileage.Text = $"{Contract.Mileage}";
      searchOrderWindows.Fuel.Text = $"{Contract.Fuel}";
      searchOrderWindows.CurrentPriceText.Text = $"{Contract.Price}";
      searchOrderWindows.Price.Text = $"{Contract.Price}₽";
      searchOrderWindows.OrderText.Text = $"{Contract.OrderStatus}";
      searchOrderWindows.TextBlockCarId.Text = $"{Contract.CarId}";

      if (Contract.OrderStatus == "АКТИВЕН")
      {
        searchOrderWindows.BorderOrder.BorderBrush = new SolidColorBrush(Colors.Green);
        searchOrderWindows.OrderText.Foreground = new SolidColorBrush(Colors.Green);
        searchOrderWindows.ButtonCloseOrder.Content = "ЗАКРЫТЬ ЗАКАЗ";
      }
      else if (Contract.OrderStatus == "ЗАКРЫТ")
      {
        searchOrderWindows.BorderOrder.BorderBrush = new SolidColorBrush(Colors.Red);
        searchOrderWindows.OrderText.Foreground = new SolidColorBrush(Colors.Red);
        searchOrderWindows.ButtonCloseOrder.Content = "АКТИВ. ЗАКАЗ";
      }

      /*if (Contract.LastDate >= DateTime.Now)
      {
        Order.Text = "АКТИВЕН";

        Border_Green();

        DatabaseTableContract.UpdateContract(Contract, Order.Text);
      }*/
      if (Contract.LastDate < DateTime.Now)
      {
        Order.Text = "ЗАКРЫТ";

        Border_Red();

        DatabaseTableContract.UpdateContract(Contract, Order.Text);
      }

      searchOrderWindows.ShowDialog();
    }

    //=====================================================
  }
}
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using CarRental.Scripts.Database;
using CarRental.Scripts;
using System;

namespace CarRental.Windows.Cars
{
  /// <summary>
  /// Логика взаимодействия для SearchCarsPanel.xaml
  /// </summary>
  public partial class SearchCarsPanel : UserControl
  {
    public Vector Price { get; private set; } = new Vector(0, 10000000);

    public string BrandCar { get; private set; }
    public string EngineType { get; private set; }
    public string DriveUnit { get; private set; }
    
    public SearchCarsPanel()
    {
      InitializeComponent();

      IsVisibleChanged += OnIsVisibleChanged;

      AllBrand.Checked += (parSender, parArgs) => { BrandCar = ""; };
      KIA.Checked += (parSender, parArgs) => { BrandCar = "KIA"; };
      AUDI.Checked += (parSender, parArgs) => { BrandCar = "AUDI"; };
      LEXUS.Checked += (parSender, parArgs) => { BrandCar = "LEXUS"; };

      AllPrice.Checked += (parSender, parArgs) => { Price = new Vector(0, 10000000); };
      Price1.Checked += (parSender, parArgs) => { Price = new Vector(0, 2000); };
      Price2.Checked += (parSender, parArgs) => { Price = new Vector(2000, 4000); };
      Price3.Checked += (parSender, parArgs) => { Price = new Vector(4000, 8000); };
      Price4.Checked += (parSender, parArgs) => { Price = new Vector(8000, 16000); };
      Price5.Checked += (parSender, parArgs) => { Price = new Vector(16000, 32000); };

      All.Checked += (parSender, parArgs) => { EngineType = ""; };
      Gasoline.Checked += (parSender, parArgs) => { EngineType = "Бензин"; };
      Diesel.Checked += (parSender, parArgs) => { EngineType = "Дизель"; };
      Gas.Checked += (parSender, parArgs) => { EngineType = "Газ"; };

      AllDrive.Checked += (parSender, parArgs) => { DriveUnit = ""; };
      FrontDrive.Checked += (parSender, parArgs) => { DriveUnit = "Передний"; };
      BackDrive.Checked += (parSender, parArgs) => { DriveUnit = "Задний"; };
      FullDrive.Checked += (parSender, parArgs) => { DriveUnit = "Полный"; };

      PriceFrom.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
      PriceBefore.PreviewTextInput += (parSender, parArgs) => parArgs.Handled = !IsInt(parArgs.Text);
    }

    private bool IsInt(string parAddText)
    {
      return char.IsDigit(parAddText, 0);
    }

    /// <summary>
    /// Обновление списка
    /// </summary>
    public void Update()
    {
      List<Car> cars = DatabaseTableCars.GetCars();
      Update(SortByParameters(cars));
    }

    /// <summary>
    /// Обновление списка
    /// </summary>
    /// <param name="parCars">Машины</param>
    private void Update(List<Car> parCars)
    {
      Cars.Clear();
      Cars.AddCars(parCars);
    }


    /// <summary>
    /// Сортировка по параметрам поиска
    /// </summary>
    /// <param name="parCars">Машины</param>
    private List<Car> SortByParameters(List<Car> parCars)
    {
      return parCars.FindAll(parCar =>
      {
        if (!string.IsNullOrEmpty(TextBoxNameCar.Text) && !parCar.NameCar.ToUpper().Contains(TextBoxNameCar.Text.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(TextBoxNameCar.Text) && !parCar.BrandCar.ToUpper().Contains(TextBoxNameCar.Text.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(BrandCar) && !parCar.BrandCar.ToUpper().Contains(BrandCar.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(EngineType) && !parCar.EngineType.ToUpper().Contains(EngineType.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(DriveUnit) && !parCar.DriveUnit.ToUpper().Contains(DriveUnit.ToUpper()))
          return false;

      if (!string.IsNullOrEmpty(PriceFrom.Text) && !string.IsNullOrEmpty(PriceBefore.Text) && CheckPrice(AllPrice) && CheckPrice(Price1) && CheckPrice(Price2) && CheckPrice(Price3)
        && CheckPrice(Price4) && CheckPrice(Price4) && CheckPrice(Price5))
          return parCar.Price >= Convert.ToInt32(PriceFrom.Text) && parCar.Price < Convert.ToInt32(PriceBefore.Text);

        return parCar.Price >= Price.X && parCar.Price < Price.Y;
      });
    }

    private void OnIsVisibleChanged(object parSender, DependencyPropertyChangedEventArgs parE)
    {
      Update();
    }

    private void ButtonApply_OnClick(object parSender, RoutedEventArgs parE)
    {
      Update();
    }

    private void PriceFrom_TextChanged(object sender, TextChangedEventArgs e)
    {
      AllPrice.IsChecked = false;
      Price1.IsChecked = false;
      Price2.IsChecked = false;
      Price3.IsChecked = false;
      Price4.IsChecked = false;
      Price5.IsChecked = false;
    }

    private bool CheckPrice(RadioButton radioButton)
    {
      return radioButton.IsChecked == false;
    }
  }
}
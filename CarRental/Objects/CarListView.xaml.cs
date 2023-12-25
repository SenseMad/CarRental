using System.Collections.Generic;
using System.Windows.Controls;

using CarRental.Scripts;

namespace CarRental.Objects
{
  /// <summary>
  /// Логика взаимодействия для CarListView.xaml
  /// </summary>
  public partial class CarListView : UserControl
  {
    public CarListView()
    {
      InitializeComponent();
    }

    //=====================================================

    public CarView this[int parIndex]
    {
      get => (CarView)Cars.Children[parIndex];
      set => Cars.Children[parIndex] = value;
    }

    //=====================================================

    public void AddCar(Car parCar)
    {
      var productView = new CarView(parCar);
      Cars.Children.Add(productView);
    }

    public void AddCars(List<Car> parCars)
    {
      foreach (var car in parCars)
      {
        AddCar(car);
      }
    }

    //=====================================================

    public void Clear()
    {
      Cars.Children.Clear();
    }

    //=====================================================
  }
}
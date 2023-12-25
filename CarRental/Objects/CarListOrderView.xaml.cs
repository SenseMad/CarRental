using System.Collections.Generic;
using System.Windows.Controls;

using CarRental.Scripts;

namespace CarRental.Objects
{
  /// <summary>
  /// Логика взаимодействия для CarListOrderView.xaml
  /// </summary>
  public partial class CarListOrderView : UserControl
  {
    public CarListOrderView()
    {
      InitializeComponent();
    }

    //=====================================================

    public CarOrderView this[int parIndex]
    {
      get => (CarOrderView)Contracts.Children[parIndex];
      set => Contracts.Children[parIndex] = value;
    }

    //=====================================================

    public void AddContract(Contract parContract)
    {
      var productView = new CarOrderView(parContract);
      Contracts.Children.Add(productView);
    }

    public void AddContracts(List<Contract> parContracts)
    {
      foreach (var contract in parContracts)
      {
        AddContract(contract);
      }
    }

    //=====================================================

    public void Clear()
    {
      Contracts.Children.Clear();
    }

    //=====================================================
  }
}
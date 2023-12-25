using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using CarRental.Scripts.Database;
using CarRental.Scripts;

namespace CarRental.Windows.Contracts
{
  /// <summary>
  /// Логика взаимодействия для SearchContractsPanel.xaml
  /// </summary>
  public partial class SearchContractsPanel : UserControl
  {
    public string OrderContract { get; private set; }

    public SearchContractsPanel()
    {
      InitializeComponent();

      IsVisibleChanged += OnIsVisibleChanged;

      All.Checked += (parSender, parArgs) => { OrderContract = ""; };
      Active.Checked += (parSender, parArgs) => { OrderContract = "АКТИВЕН"; };
      Close.Checked += (parSender, parArgs) => { OrderContract = "ЗАКРЫТ"; };
    }

    /// <summary>
    /// Обновление списка
    /// </summary>
    public void Update()
    {
      List<Contract> contracts = DatabaseTableContract.GetContracts();
      Update(SortByParameters(contracts));
    }

    /// <summary>
    /// Обновление списка
    /// </summary>
    private void Update(List<Contract> parContracts)
    {
      Contracts.Clear();
      Contracts.AddContracts(parContracts);
    }

    /// <summary>
    /// Сортировка по параметрам поиска
    /// </summary>
    private List<Contract> SortByParameters(List<Contract> parContracts)
    {
      return parContracts.FindAll(parContract =>
      {
        if (!string.IsNullOrEmpty(TextBoxNameCar.Text) && !parContract.NameCar.ToUpper().Contains(TextBoxNameCar.Text.ToUpper()))
          return false;

        if (!string.IsNullOrEmpty(OrderContract) && !parContract.OrderStatus.ToUpper().Contains(OrderContract.ToUpper()))
          return false;

        return true;
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
  }
}
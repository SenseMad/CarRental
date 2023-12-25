using System.Windows;
using System.Windows.Media.Effects;

namespace CarRental.Windows.Cars
{
  /// <summary>
  /// Логика взаимодействия для SearchCarWindows.xaml
  /// </summary>
  public partial class SearchCarWindows : Window
  {
    /// <summary>
    /// BlurEffect окна родителя
    /// </summary>
    public BlurEffect BlurEffect { get; }

    /// <summary>
    /// Начальное размытие
    /// </summary>
    public double LastBlurRadius { get; }

    public SearchCarWindows(Window parOwner)
    {
      InitializeComponent();

      Owner = parOwner;
      ShowInTaskbar = false;

      ButtonClose.Click += (parSender, parArgs) => ButtonClose_Click();
      ButtonCancel.Click += (parSender, parArgs) => ButtonClose_Click();

      if (Owner != null)
      {
        if (Owner.Effect is BlurEffect blurEffect)
        {
          BlurEffect = blurEffect;
          LastBlurRadius = BlurEffect.Radius;
          BlurEffect.Radius = 5;
        }
      }

      ButtonBuy.Click += (parSender, parArgs) =>
      {
        ButtonClose_Click();

        SearchOrderCarWindows searchOrderCarWindows = new SearchOrderCarWindows(Application.Current.MainWindow);

        searchOrderCarWindows.Price.Text = $"{Price.Text.Remove(Price.Text.IndexOf('₽'))}";
        searchOrderCarWindows.CurrentPriceText.Text = $"{Price.Text.Remove(Price.Text.IndexOf('₽'))}";
        searchOrderCarWindows.NameCar.Text = $"{TextBlockName.Text}";

        searchOrderCarWindows.ShowDialog();
      };
    }

    private void ButtonClose_Click()
    {
      BlurEffect.Radius = LastBlurRadius;
      Hide();
    }
  }
}
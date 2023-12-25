using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

using CarRental.Scripts;
using CarRental.Scripts.Database;

namespace CarRental.Windows.Cars
{
  /// <summary>
  /// Логика взаимодействия для AddCarPanel.xaml
  /// </summary>
  public partial class AddCarPanel : System.Windows.Controls.UserControl
  {
    private System.Drawing.Image _currentImage;

    private System.Drawing.Image CurrentImage
    {
      get { return _currentImage; }
      set
      {
        _currentImage?.Dispose();
        _currentImage = value;
        ImageCar.Source = ConvertBitmapTo.GetBitmapImage(_currentImage);
      }
    }

    public AddCarPanel()
    {
      InitializeComponent();

      Clear();

      ButtonAddImage.Visibility = Visibility.Visible;

      ButtonAccept.Click += (parSender, parArgs) =>
      {
        if (StringNull(TextBoxBrandCar.Text) && StringNull(TextBoxNameCar.Text) && StringNull(TextBoxEngineType.Text) && StringNull(TextBoxEngineVolume.Text) && StringNull(TextBoxPower.Text)
          && StringNull(TextBoxAcceleration.Text) && StringNull(TextBoxVolumeTank.Text) && StringNull(TextBoxMaxSpeed.Text) && StringNull(TextBoxTransmission.Text) && StringNull(TextBoxDriveUnit.Text)
          && StringNull(TextBoxPrice.Text) && StringNull(TextBoxYearOfIssue.Text) && StringNull(TextBoxEngine.Text) && StringNull(TextBoxGearbox.Text) && CurrentImage != null)
        {
          AddCar();
          Clear();
        }
        else
        {
          //Warning(TextBoxBrandCar.Text, TextBoxBrandCar);
          Warning(TextBoxNameCar.Text, TextBoxNameCar);
          //Warning(TextBoxEngineType.Text, TextBoxEngineType);
          Warning(TextBoxEngineVolume.Text, TextBoxEngineVolume);
          Warning(TextBoxPower.Text, TextBoxPower);
          Warning(TextBoxAcceleration.Text, TextBoxAcceleration);
          Warning(TextBoxVolumeTank.Text, TextBoxVolumeTank);
          Warning(TextBoxMaxSpeed.Text, TextBoxMaxSpeed);
          Warning(TextBoxTransmission.Text, TextBoxTransmission);
          //Warning(TextBoxDriveUnit.Text, TextBoxDriveUnit);
          Warning(TextBoxPrice.Text, TextBoxPrice);
          Warning(TextBoxYearOfIssue.Text, TextBoxYearOfIssue);
          Warning(TextBoxEngine.Text, TextBoxEngine);
          //Warning(TextBoxGearbox.Text, TextBoxGearbox);
        }
      };

      ButtonAddImage.Click += (parSender, parArgs) => FileDialog();

      ButtonEdit.Click += (parSender, parArgs) => FileDialog();

      ButtonDelete.Click += (parSender, parArgs) =>
      {
        CurrentImage = null;
        ButtonAddImage.Visibility = Visibility.Visible;
        ButtonDelete.Visibility = Visibility.Collapsed;
        ButtonEdit.Visibility = Visibility.Collapsed;
      };

      ButtonCancel.Click += (parSender, parArgs) => Clear();
    }

    /// <summary>
    /// Открыть меню выбора картинки
    /// </summary>
    private void FileDialog()
    {
      using (var dialog = new OpenFileDialog())
      {
        dialog.Multiselect = true;
        dialog.ShowHelp = false;
        dialog.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|All files (*.*)|*.*";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          CurrentImage = System.Drawing.Image.FromFile(dialog.FileName);

          if (ImageCar != null)
          {
            ButtonAddImage.Visibility = Visibility.Collapsed;
            ButtonDelete.Visibility = Visibility.Visible;
            ButtonEdit.Visibility = Visibility.Visible;
          }
        }
      }
    }

    private void AddCar()
    {
      string BrandCar = TextBoxBrandCar.Text;
      string NameCart = TextBoxNameCar.Text;
      string EngineType = TextBoxEngineType.Text;
      string EngineVolume = TextBoxEngineVolume.Text;
      string Power = TextBoxPower.Text;
      string Acceleration = TextBoxAcceleration.Text;
      string VolumeTank = TextBoxVolumeTank.Text;
      string MaxSpeed = TextBoxMaxSpeed.Text;
      string Transmission = TextBoxTransmission.Text;
      string DriveUnit = TextBoxDriveUnit.Text;
      int Price = Convert.ToInt32(TextBoxPrice.Text);
      string YearOfIssue = TextBoxYearOfIssue.Text;
      string Engine = TextBoxEngine.Text;
      string Gearbox = TextBoxGearbox.Text;

      DatabaseTableCars.AddCars(new Car(BrandCar, NameCart, EngineType, EngineVolume, Power, Acceleration, VolumeTank, MaxSpeed, Transmission,
        DriveUnit, CurrentImage, Price, YearOfIssue, Engine, Gearbox));
    }

    private bool StringNull(string parText)
    {
      return !string.IsNullOrEmpty(parText);
    }

    /// <summary>
    /// Проверка на заполнение полей (TextBox)
    /// </summary>
    private void Warning(string text, System.Windows.Controls.TextBox textBox)
    {
      textBox.BorderBrush = text == "" ? new SolidColorBrush(Color.FromRgb(211, 71, 46)) : new SolidColorBrush(Color.FromRgb(241, 173, 113));
    }

    private void Clear()
    {
      TextBoxBrandCar.Clear();
      TextBoxNameCar.Clear();
      TextBoxYearOfIssue.Clear();
      TextBoxEngine.Clear();
      TextBoxGearbox.Text = "";
      TextBoxEngineType.Text = "";
      TextBoxEngineVolume.Clear();
      TextBoxPower.Clear();
      TextBoxAcceleration.Clear();
      TextBoxVolumeTank.Clear();
      TextBoxMaxSpeed.Clear();
      TextBoxTransmission.Clear();
      TextBoxDriveUnit.Text = "";
      TextBoxPrice.Clear();

      if (CurrentImage != null)
      {
        CurrentImage = null;
        ButtonAddImage.Visibility = Visibility.Visible;
        ButtonDelete.Visibility = Visibility.Collapsed;
        ButtonEdit.Visibility = Visibility.Collapsed;
      }
    }
  }
}
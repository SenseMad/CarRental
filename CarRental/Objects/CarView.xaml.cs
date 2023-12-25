using System;
using System.Windows;
using System.Windows.Controls;
using CarRental.Scripts;
using CarRental.Windows.Cars;
using Word = Microsoft.Office.Interop.Word;

namespace CarRental.Objects
{
  /// <summary>
  /// Логика взаимодействия для CarView.xaml
  /// </summary>
  public partial class CarView : UserControl
  {
    private Car _car;

    /// <summary>
    /// Машина
    /// </summary>
    public Car Car
    {
      get => _car;
      set
      {
        _car = value;
        Update();
      }
    }

    public CarView(Car parCar)
    {
      InitializeComponent();

      Car = parCar;

      ButtonOrder.Click += (parSender, parArgs) =>
      {
        SearchOrderCarWindows searchOrderCarWindows = new SearchOrderCarWindows(Application.Current.MainWindow);

        searchOrderCarWindows.Price.Text = $"{Price.Text.Remove(Price.Text.IndexOf('₽'))}";
        searchOrderCarWindows.CurrentPriceText.Text = $"{Price.Text.Remove(Price.Text.IndexOf('₽'))}";
        searchOrderCarWindows.NameCar.Text = $"{Car.NameCar}";
        searchOrderCarWindows.TextBlockCarId.Text = $"{Car.Id}";

        searchOrderCarWindows.ShowDialog();
        //Doc();
        //FileDialog();
      };

      ButtonInform.Click += (parSender, parArgs) =>
      {
        SearchCarWindows searchCarWindows = new SearchCarWindows(Application.Current.MainWindow);

        searchCarWindows.BrandCar.Text = $"{Car.BrandCar}";
        searchCarWindows.YearOfIssue.Text = $"{Car.YearOfIssue}";
        searchCarWindows.Engine.Text = $"{Car.Engine} л";
        searchCarWindows.Gearbox.Text = $"{Car.Gearbox}";

        searchCarWindows.TextBlockName.Text = $"{Car.NameCar}";
        searchCarWindows.TextBlockEngineType.Text = $"{Car.EngineType}";
        searchCarWindows.TextBlockEngineVolume.Text = $"{Car.EngineVolume}";
        searchCarWindows.TextBlockPower.Text = $"{Car.Power} л.с";
        searchCarWindows.TextBlockAcceleration.Text = $"{Car.Acceleration} с";
        searchCarWindows.TextBlockVolumeTank.Text = $"{Car.VolumeTank} л";
        searchCarWindows.TextBlockMaxSpeed.Text = $"{Car.MaxSpeed} км/ч";
        searchCarWindows.TextBlockTransmission.Text = $"{Car.Transmission}";
        searchCarWindows.TextBlockDriveUnit.Text = $"{Car.DriveUnit}";
        searchCarWindows.ImageCar.Source = ConvertBitmapTo.GetBitmapImage(Car.ImageCar);
        searchCarWindows.Price.Text = $"{Car.Price}₽";

        searchCarWindows.ShowDialog();
      };
    }

    private void Doc()
    {
      Word.Document doc = null;
      try
      {
        // Создаём объект приложения
        Word.Application app = new Word.Application();
        // Путь до шаблона документа
        string source = @"C:\\Users\\Андрей\\Desktop\\Договор.docx";
        // Открываем
        doc = app.Documents.Open(source);
        doc.Activate();

        Word.Bookmarks wBookmarks = doc.Bookmarks;
        Word.Range wRange;
        int i = 0;
        string[] data = new string[3] { "1", "02", "2020" };
        foreach (Word.Bookmark mark in wBookmarks)
        {

          wRange = mark.Range;
          wRange.Text = data[i];
          i++;
        }

        
        doc.Close();
        doc = null;
      }
      catch (Exception ex)
      {
        doc.Close();
        doc = null;
        MessageBox.Show(ex + "");
      }
    }

    private void FileDialog()
    {
      using (var dialog = new System.Windows.Forms.SaveFileDialog())
      {
        dialog.Title = "Save text Files";
        dialog.ShowHelp = false;
        dialog.Filter = "Word (*.docx)|*.docx|All files (*.*)|*.*";
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          //CurrentImage = System.Drawing.Image.FromFile(dialog.FileName);
        }
      }
    }

    //=====================================================

    public void Update()
    {
      if (Car == null) return;
      
      img.Source = ConvertBitmapTo.GetBitmapImage(Car.ImageCar);
      NameCar.Text = $"{Car.NameCar}";
      Price.Text = $"{Car.Price}₽";
    }

    //=====================================================
  }
}
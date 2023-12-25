using System;
using System.Drawing;
using System.IO;

namespace CarRental.Scripts
{
  public class Car
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Марка машины
    /// </summary>
    public string BrandCar { get; set; }

    /// <summary>
    /// Название машины
    /// </summary>
    public string NameCar { get; set; }

    /// <summary>
    /// Тип двигателя
    /// </summary>
    public string EngineType { get; set; }

    /// <summary>
    /// Объем двигателя
    /// </summary>
    public string EngineVolume { get; set; }

    /// <summary>
    /// Мощность
    /// </summary>
    public string Power { get; set; }

    /// <summary>
    /// Разгон до 100
    /// </summary>
    public string Acceleration { get; set; }

    /// <summary>
    /// Объем топливного бака
    /// </summary>
    public string VolumeTank { get; set; }

    /// <summary>
    /// Максимальная скорость
    /// </summary>
    public string MaxSpeed { get; set; }

    /// <summary>
    /// Трансмиссия
    /// </summary>
    public string Transmission { get; set; }

    /// <summary>
    /// Привод
    /// </summary>
    public string DriveUnit { get; set; }

    /// <summary>
    /// Картинка машины
    /// </summary>
    public Image ImageCar { get; set; }

    /// <summary>
    /// Цена машины
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Год выпуска
    /// </summary>
    public string YearOfIssue { get; set; }

    /// <summary>
    /// Двигатель
    /// </summary>
    public string Engine { get; set; }

    /// <summary>
    /// Коробка передачь
    /// </summary>
    public string Gearbox { get; set; }

    //=====================================================

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public Car() :
      this(-1, "", "", "", "", "", "", "", "", "", "", null, 0, "", "", "")
    { }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parNameCar">Название машины</param>
    public Car(string parBrandCar, string parNameCar, string parEngineType, string parEngineVolume, string parPower, string parAcceleration, string parVolumeTank, string parMaxSpeed, string parTransmission, string parDriveUnit, Image parImageCar, int parPrice, string parYearOfIssue, string parEngine, string parGearbox) :
      this(-1, parBrandCar, parNameCar, parEngineType, parEngineVolume, parPower, parAcceleration, parVolumeTank, parMaxSpeed, parTransmission, parDriveUnit, parImageCar, parPrice, parYearOfIssue, parEngine, parGearbox)
    { }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parId">Идентификатор</param>
    /// <param name="parNameCar">Название машины</param>
    public Car(int parId, string parBrandCar, string parNameCar, string parEngineType, string parEngineVolume, string parPower, string parAcceleration, string parVolumeTank, string parMaxSpeed, string parTransmission, string parDriveUnit, Image parImageCar, int parPrice, string parYearOfIssue, string parEngine, string parGearbox)
    {
      Id = parId;
      BrandCar = parBrandCar;
      NameCar = parNameCar;
      EngineType = parEngineType;
      EngineVolume = parEngineVolume;
      Power = parPower;
      Acceleration = parAcceleration;
      VolumeTank = parVolumeTank;
      MaxSpeed = parMaxSpeed;
      Transmission = parTransmission;
      DriveUnit = parDriveUnit;
      ImageCar = parImageCar;
      Price = parPrice;
      YearOfIssue = parYearOfIssue;
      Engine = parEngine;
      Gearbox = parGearbox;
    }

    ~Car()
    {
      ImageCar?.Dispose();
    }
    //=====================================================
  }
}
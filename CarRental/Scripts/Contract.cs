using System;

namespace CarRental.Scripts
{
  public class Contract
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public string FIO { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    public string Telephone { get; set; }

    /// <summary>
    /// Серия паспорта
    /// </summary>
    public string Series { get; set; }

    /// <summary>
    /// Нормер паспорта
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата выдачи
    /// </summary>
    public string DateOfIssued { get; set; }

    /// <summary>
    /// Где выдан
    /// </summary>
    public string Issued { get; set; }

    /// <summary>
    /// Текущая дата
    /// </summary>
    public DateTime CurrentDate { get; set; }

    /// <summary>
    /// Аренда до:
    /// </summary>
    public DateTime LastDate { get; set; }

    /// <summary>
    /// Название машины
    /// </summary>
    public string NameCar { get; set; }

    /// <summary>
    /// Место выдачи автомобиля
    /// </summary>
    public string PlaceOfIssue { get; set; }

    /// <summary>
    /// Пробег
    /// </summary>
    public string Mileage { get; set; }

    /// <summary>
    /// Топливо
    /// </summary>
    public string Fuel { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Идентификатор машины
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public string OrderStatus { get; set; }

    //=====================================================

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public Contract() :
      this(-1, "", "", "", "", "", "", DateTime.Now, DateTime.Now, "", "", "", "", 0, -1, "")
    { }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Contract(string parFIO, string parTelephone, string parSeries, string parNumber, string parDateOfIssued, string parIssued, DateTime parCurrentDate, DateTime parLastDate, 
      string parNameCar, string parPlaceOfIssue, string parMileage, string parFuel, int parPrice, int parCarId, string parOrderStatus) :
      this(-1, parFIO, parTelephone, parSeries, parNumber, parDateOfIssued, parIssued, parCurrentDate, parLastDate, parNameCar, parPlaceOfIssue, parMileage, parFuel, parPrice, parCarId, parOrderStatus)
    { }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parId">Идентификатор</param>
    public Contract(int parId, string parFIO, string parTelephone, string parSeries, string parNumber, string parDateOfIssued, string parIssued, DateTime parCurrentDate, DateTime parLastDate, 
      string parNameCar, string parPlaceOfIssue, string parMileage, string parFuel, int parPrice, int parCarId, string parOrderStatus)
    {
      Id = parId;
      FIO = parFIO;
      Telephone = parTelephone;
      Series = parSeries;
      Number = parNumber;
      DateOfIssued = parDateOfIssued;
      Issued = parIssued;
      CurrentDate = parCurrentDate;
      LastDate = parLastDate;
      NameCar = parNameCar;
      PlaceOfIssue = parPlaceOfIssue;
      Mileage = parMileage;
      Fuel = parFuel;
      Price = parPrice;
      CarId = parCarId;
      OrderStatus = parOrderStatus;
    }

    //=====================================================
  }
}
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;

namespace CarRental.Scripts.Database
{
  public static class DatabaseTableContract
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "Contract";

    /// <summary>
    /// Поле ID
    /// </summary>
    public const string FILED_ID = "Id";

    /// <summary>
    /// ФИО
    /// </summary>
    public const string FILED_FIO = "FIO";

    /// <summary>
    /// Телефон
    /// </summary>
    public const string FILED_TELEPHONE = "Telephone";

    /// <summary>
    /// Серия паспорта
    /// </summary>
    public const string FILED_SERIES = "Series";

    /// <summary>
    /// Нормер паспорта
    /// </summary>
    public const string FILED_NUMBER = "Number";

    /// <summary>
    /// Выдачи
    /// </summary>
    public const string FILED_DATE_OF_ISSUED = "DateOfIssued";

    /// <summary>
    /// Где выдан
    /// </summary>
    public const string FILED_ISSUED = "Issued";

    /// <summary>
    /// Текущая дата
    /// </summary>
    public const string FILED_CURRENT_DATE = "CurrentDate";

    /// <summary>
    /// Аренда до:
    /// </summary>
    public const string FILED_LAST_DATE = "LastDate";

    /// <summary>
    /// Название машины
    /// </summary>
    public const string FILED_NAME_CAR = "NameCar";

    /// <summary>
    /// Место выдачи автомобиля
    /// </summary>
    public const string FILED_PLACE_OF_ISSUE = "PlaceOfIssue";

    /// <summary>
    /// Пробег
    /// </summary>
    public const string FILED_MILEAGE = "Mileage";

    /// <summary>
    /// Топливо
    /// </summary>
    public const string FILED_FUEL = "Fuel";

    /// <summary>
    /// Цена
    /// </summary>
    public const string FILED_PRICE = "Price";

    /// <summary>
    /// Поле Машины
    /// </summary>
    public const string FILED_CAR_ID = "CarId";

    /// <summary>
    /// Статус заказа
    /// </summary>
    public const string FILED_ORDER_STATUS = "OrderStatus";

    //=====================================================

    public static bool AddContract(Contract parContract)
    {
      using(SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"INSERT INTO {DATABASE_NAME}({FILED_FIO}, {FILED_TELEPHONE}, {FILED_SERIES}, {FILED_NUMBER}, {FILED_DATE_OF_ISSUED}, {FILED_ISSUED}, {FILED_CURRENT_DATE}, " +
                              $"{FILED_LAST_DATE}, {FILED_NAME_CAR}, {FILED_PLACE_OF_ISSUE}, {FILED_MILEAGE}, {FILED_FUEL}, {FILED_PRICE}, {FILED_CAR_ID}, {FILED_ORDER_STATUS})" +
                              $" VALUES('{parContract.FIO}', '{parContract.Telephone}', '{parContract.Series}', '{parContract.Number}', '{parContract.DateOfIssued}', '{parContract.Issued}', " +
                              $"'{parContract.CurrentDate}', '{parContract.LastDate}', '{parContract.NameCar}', '{parContract.PlaceOfIssue}', '{parContract.Mileage}', '{parContract.Fuel}', " +
                              $"'{parContract.Price}', '{parContract.CarId}', '{parContract.OrderStatus}')";
        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }
    
    public static List<Contract> GetContracts()
    {
      var retContracts = new List<Contract>();
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} ORDER BY {FILED_ID}, {FILED_FIO}, {FILED_TELEPHONE}, {FILED_SERIES}, {FILED_NUMBER}, " +
            $"{FILED_DATE_OF_ISSUED}, {FILED_ISSUED}, {FILED_CURRENT_DATE}, {FILED_LAST_DATE}, {FILED_NAME_CAR}, {FILED_PLACE_OF_ISSUE}, {FILED_MILEAGE}, " +
            $"{FILED_FUEL}, {FILED_PRICE}, {FILED_CAR_ID}, {FILED_ORDER_STATUS}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              while (dataReader.Read())
              {
                var contract = new Contract
                (
                  Convert.ToInt32(dataReader[FILED_ID]),
                  Convert.ToString(dataReader[FILED_FIO]),
                  Convert.ToString(dataReader[FILED_TELEPHONE]),
                  Convert.ToString(dataReader[FILED_SERIES]),
                  Convert.ToString(dataReader[FILED_NUMBER]),
                  Convert.ToString(dataReader[FILED_DATE_OF_ISSUED]),
                  Convert.ToString(dataReader[FILED_ISSUED]),
                  Convert.ToDateTime(dataReader[FILED_CURRENT_DATE]),
                  Convert.ToDateTime(dataReader[FILED_LAST_DATE]),
                  Convert.ToString(dataReader[FILED_NAME_CAR]),
                  Convert.ToString(dataReader[FILED_PLACE_OF_ISSUE]),
                  Convert.ToString(dataReader[FILED_MILEAGE]),
                  Convert.ToString(dataReader[FILED_FUEL]),
                  Convert.ToInt32(dataReader[FILED_PRICE]),
                  Convert.ToInt32(dataReader[FILED_CAR_ID]),
                  Convert.ToString(dataReader[FILED_ORDER_STATUS])
                );

                retContracts.Add(contract);
              }

              dataReader.Close();
            }

            connect.Close();
          }
        }
      }
      catch { }

      return retContracts;
    }

    public static bool UpdateContract(Contract parContract, string parValue)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {FILED_FIO}='{parContract.FIO}', {FILED_TELEPHONE}='{parContract.Telephone}', {FILED_SERIES}='{parContract.Series}', {FILED_NUMBER}='{parContract.Number}', " +
          $"{FILED_DATE_OF_ISSUED}='{parContract.DateOfIssued}', {FILED_ISSUED}='{parContract.Issued}', {FILED_CURRENT_DATE}='{parContract.CurrentDate}', {FILED_LAST_DATE}='{parContract.LastDate}', " +
          $"{FILED_NAME_CAR}='{parContract.NameCar}', {FILED_PLACE_OF_ISSUE}='{parContract.PlaceOfIssue}', {FILED_MILEAGE}='{parContract.Mileage}', " +
          $"{FILED_FUEL}='{parContract.Fuel}', {FILED_PRICE}='{parContract.Price}', {FILED_CAR_ID}='{parContract.CarId}', {FILED_ORDER_STATUS}='{parValue}' WHERE {FILED_ID} = '{parContract.Id}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    public static bool UpdateContract(Contract parContract)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"UPDATE {DATABASE_NAME} SET {FILED_FIO}='{parContract.FIO}', {FILED_TELEPHONE}='{parContract.Telephone}', {FILED_SERIES}='{parContract.Series}', {FILED_NUMBER}='{parContract.Number}', " +
          $"{FILED_DATE_OF_ISSUED}='{parContract.DateOfIssued}', {FILED_ISSUED}='{parContract.Issued}', {FILED_CURRENT_DATE}='{parContract.CurrentDate}', {FILED_LAST_DATE}='{parContract.LastDate}', " +
          $"{FILED_NAME_CAR}='{parContract.NameCar}', {FILED_PLACE_OF_ISSUE}='{parContract.PlaceOfIssue}', {FILED_MILEAGE}='{parContract.Mileage}', " +
          $"{FILED_FUEL}='{parContract.Fuel}', {FILED_PRICE}='{parContract.Price}', {FILED_CAR_ID}='{parContract.CarId}', {FILED_ORDER_STATUS}='{parContract.OrderStatus}' WHERE {FILED_ID} = '{parContract.Id}'";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    //=====================================================

    /// <summary>
    /// Получить изображение
    /// </summary>
    public static Image GetImage(string parBase)
    {
      byte[] imageData = Convert.FromBase64String(parBase);
      MemoryStream ms = new MemoryStream(imageData);
      return Image.FromStream(ms);
    }
  }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;

namespace CarRental.Scripts.Database
{
  public static class DatabaseTableCars
  {
    /// <summary>
    /// Название таблицы
    /// </summary>
    public const string DATABASE_NAME = "Cars";

    /// <summary>
    /// Поле ID
    /// </summary>
    public const string FILED_ID = "Id";

    /// <summary>
    /// Поле Марка машины
    /// </summary>
    public const string FILED_BRAND_CAR = "BrandCar";

    /// <summary>
    /// Поле Название машины
    /// </summary>
    public const string FILED_NAME_CAR = "NameCar";

    /// <summary>
    /// Поле Тип двигателя
    /// </summary>
    public const string FILED_ENGINE_TYPE = "EngineType";

    /// <summary>
    /// Поле Объем двигателя
    /// </summary>
    public const string FILED_ENGINE_VOLUME = "EngineVolume";

    /// <summary>
    /// Поле Мощность
    /// </summary>
    public const string FILED_POWER = "Power";

    /// <summary>
    /// Поле Разгон до 100
    /// </summary>
    public const string FILED_ACCELERATION = "Acceleration";

    /// <summary>
    /// Поле Объем топливного бака
    /// </summary>
    public const string FILED_VOLUME_TANK = "VolumeTank";

    /// <summary>
    /// Поле Максимальная скорость
    /// </summary>
    public const string FILED_MAX_SPEED = "MaxSpeed";

    /// <summary>
    /// Поле Трансмиссия
    /// </summary>
    public const string FILED_TRANSMISSION = "Transmission";

    /// <summary>
    /// Поле Привод
    /// </summary>
    public const string FILED_DRIVE_UNIT = "DriveUnit";

    /// <summary>
    /// Картинка машины
    /// </summary>
    public const string FILED_IMAGE_CAR = "ImageCar";

    /// <summary>
    /// Цена машины
    /// </summary>
    public const string FILED_PRICE = "Price";

    /// <summary>
    /// Год выпуска
    /// </summary>
    public const string FILED_YEAR_OF_ISSUE = "YearOfIssue";

    /// <summary>
    /// Двигатель
    /// </summary>
    public const string FILED_ENGINE = "Engine";

    /// <summary>
    /// Коробка передачь
    /// </summary>
    public const string FILED_GEARBOX = "Gearbox";

    //=====================================================

    public static bool AddCars(Car parCar)
    {
      using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
      {
        string base64String = null;
        using (Image image = parCar.ImageCar)
        {
          using (MemoryStream m = new MemoryStream())
          {
            image.Save(m, image.RawFormat);
            byte[] imageBytes = m.ToArray();
            base64String = Convert.ToBase64String(imageBytes);
          }
        }

        connect.Open();

        SQLiteCommand command = connect.CreateCommand();
        command.CommandText = $"INSERT INTO {DATABASE_NAME}({FILED_BRAND_CAR}, {FILED_NAME_CAR}, {FILED_ENGINE_TYPE}, {FILED_ENGINE_VOLUME}, {FILED_POWER}, {FILED_ACCELERATION}, {FILED_VOLUME_TANK}, {FILED_MAX_SPEED}, {FILED_TRANSMISSION}, {FILED_DRIVE_UNIT}, {FILED_IMAGE_CAR}, {FILED_PRICE}, {FILED_YEAR_OF_ISSUE}, {FILED_ENGINE}, {FILED_GEARBOX})" +
                              $" VALUES('{parCar.BrandCar}', '{parCar.NameCar}', '{parCar.EngineType}', '{parCar.EngineVolume}', '{parCar.Power}', '{parCar.Acceleration}', '{parCar.VolumeTank}', '{parCar.MaxSpeed}', '{parCar.Transmission}', '{parCar.DriveUnit}', '{base64String}', '{parCar.Price}', '{parCar.YearOfIssue}', '{parCar.Engine}', '{parCar.Gearbox}')";

        command.Prepare();
        command.ExecuteNonQuery();

        connect.Close();
      }

      return false;
    }

    public static List<Car> GetCars()
    {
      var retUsers = new List<Car>();
      try
      {
        using (SQLiteConnection connect = new SQLiteConnection(Database.CONNECTION_STRING))
        {
          connect.Open();

          SQLiteCommand command = connect.CreateCommand();
          command.CommandText = $"SELECT * FROM {DATABASE_NAME} ORDER BY {FILED_ID}, {FILED_BRAND_CAR}, {FILED_NAME_CAR}, {FILED_ENGINE_TYPE}, {FILED_ENGINE_VOLUME}, {FILED_POWER}, " +
            $"{FILED_ACCELERATION}, {FILED_VOLUME_TANK}, {FILED_MAX_SPEED}, {FILED_TRANSMISSION}, {FILED_DRIVE_UNIT}, {FILED_IMAGE_CAR}, {FILED_PRICE}, " +
            $"{FILED_YEAR_OF_ISSUE}, {FILED_ENGINE}, {FILED_GEARBOX}";

          using (SQLiteDataReader dataReader = command.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              while (dataReader.Read())
              {
                var car = new Car
                (
                  Convert.ToInt32(dataReader[FILED_ID]),
                  Convert.ToString(dataReader[FILED_BRAND_CAR]),
                  Convert.ToString(dataReader[FILED_NAME_CAR]),
                  Convert.ToString(dataReader[FILED_ENGINE_TYPE]),
                  Convert.ToString(dataReader[FILED_ENGINE_VOLUME]),
                  Convert.ToString(dataReader[FILED_POWER]),
                  Convert.ToString(dataReader[FILED_ACCELERATION]),
                  Convert.ToString(dataReader[FILED_VOLUME_TANK]),
                  Convert.ToString(dataReader[FILED_MAX_SPEED]),
                  Convert.ToString(dataReader[FILED_TRANSMISSION]),
                  Convert.ToString(dataReader[FILED_DRIVE_UNIT]),
                  GetImage(dataReader[FILED_IMAGE_CAR].ToString().TrimStart().TrimEnd()),
                  Convert.ToInt32(dataReader[FILED_PRICE]),
                  Convert.ToString(dataReader[FILED_YEAR_OF_ISSUE]),
                  Convert.ToString(dataReader[FILED_ENGINE]),
                  Convert.ToString(dataReader[FILED_GEARBOX])
                );

                retUsers.Add(car);
              }

              dataReader.Close();
            }

            connect.Close();
          }
        }
      }
      catch { }

      return retUsers;
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
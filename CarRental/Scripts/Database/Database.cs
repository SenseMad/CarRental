namespace CarRental.Scripts.Database
{
  public static class Database
  {
    /// <summary>
    /// Источник
    /// </summary>
    public const string DB_SOURCE = "CarRentalDB.db";

    /// <summary>
    /// Версия
    /// </summary>
    public const string DB_VERSION = "3";

    /// <summary>
    /// Подключение
    /// </summary>
    public static readonly string CONNECTION_STRING = $"Data Source={DB_SOURCE}; Version={DB_VERSION}";
  }
}
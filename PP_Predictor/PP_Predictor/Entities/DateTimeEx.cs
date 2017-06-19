using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PP_Predictor.Entities
{
    public class DateTimeEx
    {

        #region Constants

        private readonly string[] SupportedDateFormats = new string[] { "yyyy/MM/dd", "dd/MM/yyyy", "yyyy-MM-dd", "dd-MM-yyyy" };
        private readonly string[,] SupportedTimeFormats = new string[,] { 
            { "HH:mm:ss", @"^([0-1]?[0-9]|[2][0-3]):([0-5][0-9]):([0-5][0-9])$" }, 
            { "HH:mm" , @"^([0-1]?[0-9]|[2][0-3]):([0-5][0-9])$" } };

        #endregion

        #region Private Properties

        private DateTime _Date = new DateTime();
        private DateStatus _DatetimeStatus = DateStatus.Empty;

        #endregion

        #region Public Properties

        public DateTime Date
        {
            get
            {
                return this._Date;
            }
        }

        public DateStatus DateTimeStatus
        {
            get
            {
                return this._DatetimeStatus;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Evaluates the date and time input strings and returns a Datetime instance containing them
        /// </summary>
        /// <param name="Date">Date. Supported String Formats: yyyy/MM/dd, dd/MM/yyyy, yyyy-MM-dd and dd-MM-yyyy</param>
        /// <param name="Time">Time. Supported String Format: HH:mm:ss</param>
        public DateTimeEx(string Date, string Time)
        {
            if (ExtractDate(Date))
                if (ExtractTime(Time))
                    this._DatetimeStatus = DateStatus.Ok;
        }

        public DateTimeEx(DateTime Date)
        {
            this._Date = Date;
            this._DatetimeStatus = DateStatus.Ok;
        }

        #endregion

        #region Private methods

        private bool ExtractDate(string value)
        {
            try
            {
                DateTime dt = new DateTime();
                bool isValid = false;
                foreach (string s in SupportedDateFormats)
                {
                    isValid = DateTime.TryParseExact(value, s, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                    if (isValid) break;
                }
                if (isValid)
                {
                    _Date = dt;
                    return true;
                }
                else
                {
                    _DatetimeStatus = DateStatus.InvalidDate;
                    return false;
                }
            }
            catch
            {
                _DatetimeStatus = DateStatus.InvalidDate;
                return false;
            }
        }

        private bool ExtractTime(string value)
        {
            try {
                DateTime dt = new DateTime();
                for (int i = 0; i < SupportedTimeFormats.GetLength(0); i++)
                {
                    Regex r = new Regex(SupportedTimeFormats[i, 1]);
                    Match m = r.Match(value);
                    if (m.Success)
                    {
                        dt = DateTime.ParseExact(m.Value, SupportedTimeFormats[i, 0], CultureInfo.InvariantCulture);
                        this._Date = this._Date.AddHours(dt.Hour);
                        this._Date = this._Date.AddMinutes(dt.Minute);
                        this._Date = this._Date.AddSeconds(dt.Second);
                        return true;
                    }
                }
            } catch { }
            _DatetimeStatus = DateStatus.InvalidTime;
            return false;
            }


        #endregion
    }

    [Flags]
    public enum DateStatus
    {
        Empty,
        Ok,
        InvalidDate,
        InvalidTime
    }
}
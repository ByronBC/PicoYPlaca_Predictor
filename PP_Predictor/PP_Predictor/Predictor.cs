using System;
using PP_Predictor.Entities;
namespace PP_Predictor
{
    public class Predictor
    {

        #region Constants

        private readonly string[] Days = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private readonly bool[] RestrictedDays = new bool[] { true, true, true, true, true, false, false };
        private readonly int[,] RestrictedPlates = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 }, { 9, 0 }, { -1, -1 }, { -1, -1 } };

        private readonly float[,] RestrictedHours = new float[,] { { 7f, 9.5f }, { 16f, 19.5f } };

        #endregion

        #region Private Properties

        private Plate _Plate;
        private DateTimeEx _Date;
        private PredictorStatus _PredictorStatus = PredictorStatus.Empty;

        #endregion

        #region Public Properties

        public string Plate
        {
            get
            {
                return this._Plate.FullPlate;
            }
            set
            {
                this._Plate = new Plate(value);
                this.Validate();
            }
        }

        public DateTime Date
        {
            get
            {
                return this._Date.Date;
            }
            set
            {
                this._Date = new DateTimeEx(value);
                this.Validate();
            }
        }

        public PredictorStatus PredictorStatus
        {
            get
            {
                return this._PredictorStatus;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Evaluates the Plate and returns an instance containing it.
        /// </summary>
        /// <param name="value">Plate. Suported String Format: 3 letters + hyphen (optional) + 3-4 digits</param>
        public Predictor(string Plate, string Date, string Time)
        {
            this._Plate = new Plate(Plate);
            this._Date = new DateTimeEx(Date, Time);
            this.Validate();
        }

        /// <summary> Determines Whether or not that car can be on the road </summary>
        /// <returns>True or False</returns>
        public bool CanBeOnTheRoad()
        {
            // Check if the data provided is incorrect
            if (_PredictorStatus != PredictorStatus.Ok)
                return false;

            // Check the day has restriction          
            string dateDayName = _Date.Date.DayOfWeek.ToString();
            int dateDay = Array.IndexOf(Days, dateDayName);
            if (!RestrictedDays[dateDay])
                return true;

            // Check the plate is restricted
            bool bRestricted = false;
            for (int i = 0; i < RestrictedPlates.GetLength(1); i++)
            {
                if (_Plate.LastDigit == RestrictedPlates[dateDay, i])
                {
                    bRestricted = true;
                    break;
                }
            }
            if (!bRestricted)
                return true;

            // Check the time is restricted
            bRestricted = false;
            float dateHour = _Date.Date.Hour + (_Date.Date.Minute + (_Date.Date.Second / 60f)) / 60f;
            for (int i = 0; i < RestrictedHours.GetLength(0); i++)
            {
                if (dateHour >= RestrictedHours[i, 0] && dateHour <= RestrictedHours[i, 1])
                {
                    bRestricted = true;
                    break;
                }
            }
            if (!bRestricted)
                return true;
            return false;
        }

        #endregion

        #region Private Methods

        private void Validate()
        {
            if (_Plate.PlateStatus == PlateStatus.InvalidPlate)
                this._PredictorStatus = PredictorStatus.InvalidPlate;
            else if (_Date.DateTimeStatus == DateStatus.InvalidDate)
                this._PredictorStatus = PredictorStatus.InvalidDate;
            else if (_Date.DateTimeStatus == DateStatus.InvalidTime)
                this._PredictorStatus = PredictorStatus.InvalidTime;
            else
                this._PredictorStatus = PredictorStatus.Ok;
        }

        #endregion
    }

    [Flags]
    public enum PredictorStatus
    {
        Empty,
        Ok,
        InvalidPlate,
        InvalidDate,
        InvalidTime
    }
}

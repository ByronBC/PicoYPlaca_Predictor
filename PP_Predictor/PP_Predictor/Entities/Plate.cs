using System;
using System.Text.RegularExpressions;

namespace PP_Predictor.Entities
{
    public class Plate
    {

        #region Constants

        private readonly string[,] SupportedFormats = new string[,] { { @"^([a-z]|[A-Z]){3}(-?)\d{3,4}$", @"^([a-z]]|[A-Z]){3}", @"\d{3,4}$" } };

        #endregion

        #region Private Properties

        private string _FullPlate = string.Empty;
        private string _LiteralPart = string.Empty;
        private int _NumeralPart = -1;
        private PlateStatus _PlateStatus = PlateStatus.Empty;

        #endregion

        #region Public Properties

        public string FullPlate
        {
            get
            {
                return this._FullPlate;
            }
        }

        public string LiteralParte
        {
            get
            {
                return this._LiteralPart;
            }
        }

        public int NumeralParte
        {
            get
            {
                return this._NumeralPart;
            }
        }

        public int LastDigit
        {
            get
            {
                return Math.Abs(this._NumeralPart % 10);
            }
        }

        public PlateStatus PlateStatus
        {
            get
            {
                return this._PlateStatus;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Evaluates the Plate and returns an instance containing it.
        /// </summary>
        /// <param name="Plate">Plate. Suported String Format: 3 letters + hyphen (optional) + 3-4 digits</param>
        public Plate(string Plate)
        {
            if (ExtractPlate(Plate))
                this._PlateStatus = PlateStatus.Ok;
        }

        #endregion

        #region Private Methods

        private bool ExtractPlate(string value)
        {
            try
            {
                for (int i = 0; i < SupportedFormats.GetLength(0); i++)
                {
                    Regex r = new Regex(SupportedFormats[i, 0]);
                    Match m = r.Match(value);
                    if (m.Success)
                    {
                        this._LiteralPart = m.Groups[0].Value.Substring(0, 3);
                        r = new Regex(SupportedFormats[i, 2]);
                        m = r.Match(value);
                        string RightP = m.Groups[0].Value;
                        if (RightP.Length == 3)
                            RightP = "0" + RightP;
                        this._FullPlate = LiteralParte + '-' + RightP;
                        Int32.TryParse(RightP,out this._NumeralPart);
                        return true;
                    }
                }
            }
            catch { }
            this._PlateStatus = PlateStatus.InvalidPlate;
            return false;
        }


        #endregion
    }
}

[Flags]
public enum PlateStatus
{
    Empty,
    Ok,
    InvalidPlate
}
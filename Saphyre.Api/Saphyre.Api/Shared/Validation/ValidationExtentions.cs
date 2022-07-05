using System.Text.RegularExpressions;

namespace Saphyre.Api.Shared.Validation
{
    public static class ValidationExtensions
    {
        private const string _httpProtocolPrefix = "http://";
        private const string _httpsProtocolPrefix = "https://";
        private const int _minimumYear = 1901;
        private const int _maximumYear = 2999;
        private const char _blankCharacter = ' ';
        private const string _numeric = "^[0-9]*$";
        private const string _alpha = "^[A-Za-z]*$";
        private const string _alphaNumeric = "^[A-Za-z0-9]*$";
        private const string _alphaNumericUpperCase = "^[A-Z0-9]*$";
        private const string _text = "^[-' A-Za-z0-9]*$";
        private const string _blankSpace = " ";
        private const string _email = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}$";
        private const string _url = @"[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
        private const string _password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";

        private static readonly Regex _regNumeric;
        private static readonly Regex _regAlphabetic;
        private static readonly Regex _regAlphaNumeric;
        private static readonly Regex _regAlphaNumericUpper;
        private static readonly Regex _regText;
        private static readonly Regex _regEmail;
        private static readonly Regex _regUrl;
        private static readonly Regex _regPassword;

        static ValidationExtensions()
        {
            _regNumeric = new Regex(_numeric, RegexOptions.Compiled | RegexOptions.Singleline);
            _regAlphabetic = new Regex(_alpha, RegexOptions.Compiled | RegexOptions.Singleline);
            _regAlphaNumeric = new Regex(_alphaNumeric, RegexOptions.Compiled | RegexOptions.Singleline);
            _regAlphaNumericUpper = new Regex(_alphaNumericUpperCase, RegexOptions.Compiled | RegexOptions.Singleline);
            _regText = new Regex(_text, RegexOptions.Compiled | RegexOptions.Singleline);
            _regEmail = new Regex(_email, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
            _regUrl = new Regex(_url, RegexOptions.Compiled | RegexOptions.Singleline);
            _regPassword = new Regex(_password, RegexOptions.Compiled | RegexOptions.Singleline);
        }

        #region String extensions

        public static bool MustNotBeNull<T>(this T? value) where T : struct
        {
            return value.HasValue;
        }

        public static bool MustNotBeNullIfOtherFieldIsNotNull<T1, T2>(this T1? value, T2? other) where T1 : struct where T2 : struct
        {
            return value.HasValue || !other.HasValue;
        }

        public static bool MustNotBeNullIfOtherFieldIsNull<T1, T2>(this T1? value, T2? other) where T1 : struct where T2 : struct
        {
            return value.HasValue || other.HasValue;
        }

        public static bool MustNotBeNull(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool MustBeAlphabetic(this string value)
        {
            return string.IsNullOrEmpty(value) || _regAlphabetic.IsMatch(value);
        }

        public static bool MustBeAlphabeticOrSpace(this string value)
        {
            if (value == _blankSpace)
            {
                return true;
            }
            return string.IsNullOrEmpty(value) || _regAlphabetic.IsMatch(value);
        }

        public static bool MustBeAlphaNumeric(this string value)
        {
            return string.IsNullOrEmpty(value) || _regAlphaNumeric.IsMatch(value);
        }

        public static bool MustBeAlphaNumericUpperCase(this string value)
        {
            return string.IsNullOrEmpty(value) || _regAlphaNumericUpper.IsMatch(value);
        }

        public static bool MustBeAllDigits(this string value)
        {
            return string.IsNullOrEmpty(value) || _regNumeric.IsMatch(value);
        }

        public static bool MustBeStandardText(this string value)
        {
            return string.IsNullOrEmpty(value) || _regText.IsMatch(value);
        }

        public static bool MustBeValidEmailAddress(this string value)
        {
            return string.IsNullOrEmpty(value) || _regEmail.IsMatch(value);
        }

        public static bool MustBeValidUrl(this string value)
        {
            return string.IsNullOrEmpty(value) || _regUrl.IsMatch(value);
        }

        public static bool MustBeValidUrlProtocol(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.ToLower().StartsWith(_httpProtocolPrefix) || value.ToLower().StartsWith(_httpsProtocolPrefix);
        }

        public static bool MustBeValidSocialSecurityNumber(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.MustBeAllDigits() && value.MustHaveLengthBetween(9, 9) && value.MustNotBeAllZeroes();
        }

        public static bool MustBeValidPhoneNumber(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.MustBeAllDigits() && value.MustHaveLengthBetween(10, 10);
        }

        public static bool FirstCharacterCannotBeNumeric(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            char stringFirstCharacter = value[0];
            return !char.IsDigit(stringFirstCharacter);
        }

        public static bool MustHaveLengthAtLeast(this string value, int min)
        {
            return string.IsNullOrEmpty(value) || value.Length >= min;
        }

        public static bool MustHaveLengthAtMost(this string value, int max)
        {
            return string.IsNullOrEmpty(value) || value.Length <= max;
        }

        public static bool MustHaveLengthBetween(this string value, int min, int max)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.Length >= min && value.Length <= max;
        }

        public static bool MustNotBeAllZeroes(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                for (int index = 0; index < value.Length; index++)
                {
                    if (value[index] != '0')
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public static bool MustNotContainSpaces(this string value)
        {
            if (value != null)
            {
                for (int index = 0; index < value.Length; index++)
                {
                    if (value[index] == _blankCharacter)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool MustMatch(this string value1, string value2)
        {
            return value1 == value2;
        }

        public static bool MustBeValidPassword(this string value)
        {
            return string.IsNullOrEmpty(value) || _regPassword.IsMatch(value);
        }

        #endregion

        #region Integer extensions (int, short, byte)

        public static bool MustNotBeNull(this int? value)
        {
            return value.HasValue;
        }

        public static bool MustNotBeNull(this byte? value)
        {
            return value.HasValue;
        }

        public static bool MustNotBeNull(this short? value)
        {
            return value.HasValue;
        }

        public static bool MustBeWithin(this int? value, int min, int max)
        {
            if (value.HasValue)
            {
                return value >= min && value <= max;
            }
            return true;
        }

        #endregion

        #region Decimal extensions

        public static bool MustNotBeNull(this decimal? value)
        {
            return value.HasValue;
        }

        public static bool MustNotBeNullIfOtherFieldIsNotNull(this decimal? value, decimal? other)
        {
            return value.HasValue || !other.HasValue;
        }

        public static bool MustBeAtLeast(this decimal? value, decimal min)
        {
            return !value.HasValue || value.Value >= min;
        }

        public static bool MustBeAtMost(this decimal? value, decimal max)
        {
            return !value.HasValue || value.Value <= max;
        }

        public static bool MustBeAtMost(this decimal value, decimal max)
        {
            return value <= max;
        }

        public static bool MustBeGreaterThan(this decimal? value, decimal min)
        {
            return !value.HasValue || value.Value > min;
        }

        public static bool MustBeAtMostIfOtherFieldIsNotNull(this decimal? value, decimal? max)
        {
            return !max.HasValue || !value.HasValue || value.Value <= max;
        }

        public static bool MustBeWithin(this decimal? value, decimal min, decimal max)
        {
            if (value.HasValue)
            {
                return value >= min && value <= max;
            }
            return true;
        }

        #endregion

        #region DateTime extensions

        public static bool MustNotBeNull(this DateTime? value)
        {
            return value.HasValue;
        }

        public static bool MustBeInPast(this DateTime value)
        {
            return value < DateTime.Today;
        }

        public static bool MustBeInFuture(this DateTime value)
        {
            return value > DateTime.Today;
        }

        public static bool MustBeOlderOrEqualTo(this DateTime? value1, DateTime? value2)
        {
            if (value1.HasValue && value2.HasValue)
            {
                return ((DateTime)value1).CompareTo(((DateTime)value2)) <= 0;
            }
            return true;
        }

        public static bool MustBeLaterThan(this DateTime? value1, DateTime? value2)
        {
            if (value1.HasValue && value2.HasValue)
            {
                DateTime compareDate = value2.Value.AddDays(1);
                return (value1.Value).CompareTo(compareDate) >= 0;
            }
            return true;
        }

        public static bool MustBeLaterThan(this DateTime value1, DateTime? value2)
        {
            if (value2.HasValue)
            {
                DateTime compareDate = value2.Value.AddDays(1);
                return value1.CompareTo(compareDate) >= 0;
            }
            return true;
        }

        public static bool MustBeWithinDaysAfter(this DateTime? value1, DateTime? date2, int numberOfDays)
        {
            if (value1.HasValue && date2.HasValue)
            {
                return ((DateTime)value1).CompareTo(((DateTime)date2).AddDays(numberOfDays)) <= 0;
            }
            return true;
        }

        public static bool MustBeWithinDaysAfter(this DateTime value1, DateTime? date2, int numberOfDays)
        {
            if (date2.HasValue)
            {
                return (value1).CompareTo(((DateTime)date2).AddDays(numberOfDays)) <= 0;
            }
            return true;
        }

        public static bool MustBeWithinDaysPriorTo(this DateTime? value1, DateTime? value2, int numberOfDays)
        {
            if (value1.HasValue && value2.HasValue)
            {
                return ((DateTime)value1).CompareTo(((DateTime)value2).AddDays(-numberOfDays)) >= 0;
            }
            return true;
        }

        public static bool MustBeWithinDaysPriorTo(this DateTime value1, DateTime? value2, int numberOfDays)
        {
            if (value2.HasValue)
            {
                return (value1).CompareTo(((DateTime)value2).AddDays(-numberOfDays)) >= 0;
            }
            return true;
        }

        public static bool MustBeValidYear(this DateTime? value, int minYear = _minimumYear, int maxYear = _maximumYear)
        {
            if (value == null)
            {
                return true;
            }
            int selectedYear = value.Value.Year;
            return selectedYear >= _minimumYear && selectedYear <= _maximumYear;
        }

        public static bool MustBeValidYear(this DateTime value, int minYear = _minimumYear, int maxYear = _maximumYear)
        {
            int selectedYear = value.Year;
            return selectedYear >= _minimumYear && selectedYear <= _maximumYear;
        }

        public static bool MustPassConcurrencyCheck(this DateTime? date1, DateTime? date2)
        {
            return date1 == date2;
        }

        #endregion

        #region Entity extensions (generics)

        public static bool MustNotBeNull<BaseEntity>(this BaseEntity entity)
        {
            return entity != null;
        }

        public static bool MustBeNull<BaseEntity>(this BaseEntity entity)
        {
            return entity == null;
        }

        #endregion
    }
}

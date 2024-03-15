using System.Globalization;
using System.Windows.Controls;

namespace LoginApp.Model
{
    internal class PasswordValidationRule : ValidationRule
    {
        private readonly int _minLenth;
        private readonly int _maxLenth;
        public PasswordValidationRule(/* Тут нужно передавать параметры валидации через di */)
        {
            _minLenth = 2;
            _maxLenth = 5;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = (string)value;

            if (stringValue == null || stringValue.Length < _minLenth || stringValue.Length > _maxLenth)
            {
                return new ValidationResult(false,
                  $"длина пароля должна быть от {_minLenth} да {_maxLenth}");
            }
            return ValidationResult.ValidResult;
        }
    }
}

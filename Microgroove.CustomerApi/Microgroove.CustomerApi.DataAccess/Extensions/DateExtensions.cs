namespace Microgroove.CustomerApi.DataAccess.Extensions
{
    public static class DateOfBirthExtensions
    {
        public static (DateOnly MinDateOfBirth, DateOnly MaxDateOfBirth) GetBirthDateRange(this int age)
        {
            return GetBirthDateRange(age, () => DateOnly.FromDateTime(DateTime.Now));
        }

        public static (DateOnly MinDateOfBirth, DateOnly MaxDateOfBirth) GetBirthDateRange(this int age, Func<DateOnly> GetCurrentDateFunc)
        {
            var minDateOfBirth = GetCurrentDateFunc().AddYears(-age-1).AddDays(1);
            var maxDateOfBirth = GetCurrentDateFunc().AddYears(-age);

            return (minDateOfBirth, maxDateOfBirth);
        }
    }
}

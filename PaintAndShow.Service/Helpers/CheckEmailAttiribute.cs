using System.Text.RegularExpressions;
using PaintAndShow.Service.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace PaintAndShow.Service.Helpers;

public class CheckEmailAttiribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string email = value.ToString()
            ?? throw new CustomException(403, "Invalid email");
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, pattern))
            throw new CustomException(403, "Invalid email");
        return true;
    }
}

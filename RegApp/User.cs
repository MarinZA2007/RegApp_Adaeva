using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegApp
{
    public class User
    {
        static public string TestPassword(string password)
        {
            if (password.Length>=7 && password.Length<=10)
            {
                bool IsDigit = false;
                bool IsLower = false;
                bool IsUpper = false;
                bool IsSpec = false;
                string alf = "!@#$%^&*";
                foreach (var item in password)
                {
                    if (char.IsDigit(item))
                    {
                        IsDigit = true;
                    }
                    if (char.IsLower(item))
                    {
                        IsLower = true;
                    }
                    if (char.IsUpper(item))
                    {
                        IsUpper = true;
                    }
                    if (alf.Contains(item))
                    {
                        IsSpec = true;
                    }
                }
                if (IsDigit)
                {
                    if (IsLower)
                    {
                        if (IsUpper)
                        {
                            if (IsSpec)
                            {
                                return "Пароль нормальный";
                            }
                            else
                            {
                                return $"Пароль должен содержать специальные символы {alf}";
                            }
                        }
                        else
                        {
                            return "Пароль должен содержать буквы верхнего регистра";
                        }
                    }
                    else
                    {
                        return "Пароль должен содержать буквы нижнего регистра";
                    }
                }
                else
                {
                    return "Пароль должен содержать цифры";
                }
            }
            else
            {
                return "Неверная длина пароля";
            }
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace RegAppAutoTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Запуск тестирования");
                //Запуск приложения
                Console.WriteLine("Запуск приложения");
                Process p = null;
                p = Process.Start("D:\\_МАРИНА\\_ОБУЧЕНИЕ\\Занятие 13(24.09)\\ДЗ\\RegApp\\bin\\Debug\\RegApp.exe");
                //Ожидание процесса
                int ct = 0;
                do
                {
                    Console.WriteLine("Поиск процесса ...");
                    ct++;
                    Thread.Sleep(1000);
                } while (p == null && ct < 100);
                // Проверка существования процесса
                if (p == null)
                {
                    throw new Exception("Процесс не был найден");
                }
                else
                {
                    Console.WriteLine($"Процесс {p.ProcessName} найден");
                }
                // Получение корневого элемента
                AutomationElement aeDesktop = null;
                aeDesktop = AutomationElement.RootElement;
                if (aeDesktop == null)
                {
                    throw new Exception("Не удалось получить рабочий стол");
                }
                else
                {
                    Console.WriteLine("Рабочий стол найден");
                }
                //Получение окна приложения
                AutomationElement aeApp = null;
                int numWaits = 0;
                do
                {
                    Console.WriteLine("Получение окна программы...");
                    aeApp = aeDesktop.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "MainWindow"));
                    numWaits++;
                    Thread.Sleep(200);
                } while (aeApp == null && numWaits < 50);
                if (aeApp == null)
                {
                    throw new Exception("Не удалось найти окно приложения");
                }
                else
                {
                    Console.WriteLine("Найдено окно приложения");
                }
                // получение элементов управления
                Console.WriteLine("Получение пользовательских элементов управления");
                // получение кнопки
                AutomationElement aeButton = null;
                aeButton = aeApp.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Проверить пароль"));
                if (aeButton == null)
                {
                    throw new Exception("Кнопка не найдена");

                }
                else
                {
                    Console.WriteLine("Кнопка найдена");
                }
                //
                AutomationElementCollection aeAllTextBoxes = null;
                aeAllTextBoxes = aeApp.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                if (aeAllTextBoxes == null)
                {
                    throw new Exception("Коллекция текстовых полей не найдена");
                }
                else
                {
                    Console.WriteLine("Коллекция текстовых полей найдена");
                }
                // Получение TextBox1 и TextBox2
                AutomationElement aeTextBox1 = null;
                AutomationElement aeTextBox2 = null;
                aeTextBox1 = aeAllTextBoxes[0];
                aeTextBox2 = aeAllTextBoxes[1];
                if (aeTextBox1 == null)
                {
                    throw new Exception("TextBox1 не найден");
                }
                else 
                {
                    Console.WriteLine("TextBox1 найден");
                }
                if (aeTextBox2 == null)
                {
                    throw new Exception("TextBox2 не найден");
                }
                else
                {
                    Console.WriteLine("TextBox2 найден");
                }
              
                Console.WriteLine("1.Ввод валидного пароля, длина 7 символов - '!1Qwert'");
                ValuePattern vpTextBox1 = (ValuePattern)aeTextBox1.GetCurrentPattern(ValuePattern.Pattern);
                vpTextBox1.SetValue("!1Qwert");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                InvokePattern ipClickButton1 = (InvokePattern)aeButton.GetCurrentPattern(InvokePattern.Pattern);
                ipClickButton1.Invoke();
                Thread.Sleep(1500);
                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                TextPattern vpTextBox2 = (TextPattern)aeTextBox2.GetCurrentPattern(TextPattern.Pattern);
                string actual = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected = "Пароль нормальный";
                if (actual != expected)
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }
                else
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }

                Console.WriteLine("2.Ввод валидного пароля, длина 10 символов - '!1Qwertyui'");
                vpTextBox1.SetValue("!1Qwertyui");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual2 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected2 = "Пароль нормальный";
                if (actual2 != expected2)
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }
                else
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }

                Console.WriteLine("3.Ввод невалидного пароля, длина 6 символов - '!1Qwer'");
                vpTextBox1.SetValue("!1Qwer");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual3 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected3 = "Пароль нормальный";
                if (actual3 != expected3)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("4.Ввод невалидного пароля, длина 11 символов - '!1Qwertyuio'");
                vpTextBox1.SetValue("!!1Qwertyuio");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual4 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected4 = "Пароль нормальный";
                if (actual4 != expected4)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("5.Ввод пустого пароля - ''");
                vpTextBox1.SetValue("");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual5 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected5 = "Пароль нормальный";
                if (actual5 != expected5)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("6.Ввод пароля, содержащего только цифры - '1234567'");
                vpTextBox1.SetValue("1234567");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual6 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected6 = "Пароль нормальный";
                if (actual6 != expected6)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("7.Ввод пароля, содержащего только буквы - 'Qwertyu'");
                vpTextBox1.SetValue("Qwertyu");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual7 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected7 = "Пароль нормальный";
                if (actual7 != expected7)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("8.Ввод пароля, содержащего только спец символы - '!@#$%^&*'");
                vpTextBox1.SetValue("!@#$%^&*");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual8 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected8 = "Пароль нормальный";
                if (actual8 != expected8)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("9.Ввод пароля без заглавых букв - '!1qwerty'");
                vpTextBox1.SetValue("!1qwerty");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual9 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected9 = "Пароль нормальный";
                if (actual9 != expected9)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("10.Ввод пароля без строчных букв - '!1QWERTY'");
                vpTextBox1.SetValue("!1QWERTY");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual10 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected10 = "Пароль нормальный";
                if (actual10 != expected10)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("11.Ввод пароля без спец символов - '1Qwerty'");
                vpTextBox1.SetValue("1Qwerty");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual11 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected11 = "Пароль нормальный";
                if (actual11 != expected11)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("12.Ввод пароля с пробелом в начале - ' !1Qwert'");
                vpTextBox1.SetValue(" !1Qwert");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual12 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected12 = "Пароль нормальный";
                if (actual12 != expected12)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("13.Ввод пароля с пробелом по середине - '!1Qw ert'");
                vpTextBox1.SetValue("!1Qw ert");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual13 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected13 = "Пароль нормальный";
                if (actual13 != expected13)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("14.Ввод пароля с пробелом в конце - '!1Qwert '");
                vpTextBox1.SetValue("!1Qwert ");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual14 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected14 = "Пароль нормальный";
                if (actual14 != expected14)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("15.Ввод пароля с точкой - '!1Qwert.'");
                vpTextBox1.SetValue("!1Qwert.");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual15 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected15 = "Пароль нормальный";
                if (actual15 != expected15)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                Console.WriteLine("16.Ввод пароля с собакой - '!1Qwert@'");
                vpTextBox1.SetValue("!1Qwert@");
                // Нажатие на кнопку Вычислить
                Console.WriteLine("Щелчок левой кнопки по кнопке 'Проверить пароль'");
                ipClickButton1.Invoke();
                Thread.Sleep(1500);

                // Сравнение фактического результата с ожидаемым
                Console.WriteLine("Проверка фактического результата с ожидаемым");
                Console.WriteLine("Проверка TextBox2 на наличие результата 'Пароль нормальный'");
                string actual16 = (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);
                string expected16 = "Пароль нормальный";
                if (actual16 != expected16)
                {
                    Console.WriteLine("Результат совпал");
                    Console.WriteLine("Сценарий тестирования: 'Pass'");
                }
                else
                {
                    Console.WriteLine("Результат не совпал");
                    Console.WriteLine("Сценарий тестирования: 'Fail'");
                }

                AutomationElement aeFileExit = null;
                aeFileExit = aeApp.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Закрыть"));
                InvokePattern ipFileExit = (InvokePattern)aeFileExit.GetCurrentPattern(InvokePattern.Pattern);
                ipFileExit.Invoke();
                Console.WriteLine("Остановка тестирования");


                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: " + ex.Message);
            }
}
    }
}

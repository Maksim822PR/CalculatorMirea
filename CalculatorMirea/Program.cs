using System;
using static System.Console;
using static System.Math;

class Calculator
{
    static double memory = 0;

    // Функция для проверки продолжения работы
    static bool AskToContinue()
    {
        Write("Хотите продолжить? (да/нет): ");
        string answer = ReadLine().ToLower();
        return (answer == "да" || answer == "д" || answer == "yes" || answer == "y");
    }

    // Функция для безопасного ввода числа
    static bool TryReadNumber(out double result)
    {
        string input = ReadLine();
        if (!double.TryParse(input, out result))
        {
            WriteLine("Ошибка: введите корректное число");
            return false;
        }
        return true;
    }

    // Функция для бинарных операций
    static double BinaryOperation(double firstNum, char mathOperation, double secondNum)
    {
        double result = 0;
        bool error = false;

        switch (mathOperation)
        {
            case '+':
                result = firstNum + secondNum;
                break;
            case '-':
                result = firstNum - secondNum;
                break;
            case '*':
                result = firstNum * secondNum;
                break;
            case '/':
                if (secondNum == 0)
                {
                    WriteLine("Ошибка: нельзя делить на 0");
                    error = true;
                }
                else
                {
                    result = firstNum / secondNum;
                }
                break;
            case '%':
                result = firstNum % secondNum;
                break;
            default:
                WriteLine("Неизвестная операция");
                error = true;
                break;
        }

        if (!error)
        {
            result = Round(result, 3);
        }
        return error ? double.NaN : result;
    }

    // Функция для унарных операций
    static double UnaryOperation(double num, string operation)
    {
        double result = 0;
        bool error = false;

        switch (operation.ToLower())
        {
            case "1/x":
                if (num == 0)
                {
                    WriteLine("Ошибка: деление на ноль невозможно");
                    error = true;
                }
                else
                {
                    result = 1 / num;
                }
                break;
            case "x^2":
                result = num * num;
                break;
            case "sqrt(x)":
                if (num < 0)
                {
                    WriteLine("Ошибка: извлечение корня из отрицательного числа невозможно");
                    error = true;
                }
                else
                {
                    result = Sqrt(num);
                }
                break;
            case "m+":
                memory += num;
                result = memory;
                WriteLine($"Значение в памяти: {memory}");
                break;
            case "m-":
                memory -= num;
                result = memory;
                WriteLine($"Значение в памяти: {memory}");
                break;
            case "mr":
                result = memory;
                WriteLine($"Значение в памяти: {memory}");
                break;
            case "mc":
                memory = 0;
                result = 0;
                WriteLine("Память очищена");
                break;
            default:
                WriteLine("Неизвестная операция, попробуйте еще раз");
                error = true;
                break;
        }

        if (!error)
        {
            result = Round(result, 3);
        }
        return error ? double.NaN : result;
    }

    static void Main()
    {
        WriteLine("Это консольный калькулятор на C#");
        bool isContinue = true;

        while (isContinue)
        {
            double result = 0;
            Write("Выберите вид операции бинарная/унарная: ");
            string typeOfOperation = ReadLine().ToLower();

            if (typeOfOperation != "унарная" && typeOfOperation != "бинарная")
            {
                WriteLine("Неверный тип операции. Пожалуйста, введите 'унарная' или 'бинарная'.");
                isContinue = AskToContinue();
                Clear();
                continue;
            }

            if (typeOfOperation == "унарная")
            {
                Write("Введите операцию (1/x, x^2, sqrt(x), M+, M-, MR, MC): ");
                string mathOption1 = ReadLine().ToLower();

                // Проверка корректности операции
                string[] validUnaryOperations = { "1/x", "x^2", "sqrt(x)", "m+", "m-", "mr", "mc" };
                if (Array.IndexOf(validUnaryOperations, mathOption1) == -1)
                {
                    WriteLine("Неизвестная операция");
                    isContinue = AskToContinue();
                    Clear();
                    continue;
                }

                double num = 0;
                if (mathOption1 != "mr" && mathOption1 != "mc")
                {
                    Write("Введите число: ");
                    if (!TryReadNumber(out num))
                    {
                        isContinue = AskToContinue();
                        Clear();
                        continue;
                    }
                }

                result = UnaryOperation(num, mathOption1);

                // Для обычных операций сохраняем результат в память
                if (mathOption1 != "m+" && mathOption1 != "m-" && mathOption1 != "mr" && mathOption1 != "mc")
                {
                    memory = result;
                }
            }
            else
            {
                Write("Введите первое число: ");
                if (!TryReadNumber(out double firstNum))
                {
                    isContinue = AskToContinue();
                    Clear();
                    continue;
                }

                Write("Введите операцию (+, -, /, *, %): ");
                string input = ReadLine();
                if (input.Length != 1)
                {
                    WriteLine("Ошибка: введите один символ операции");
                    isContinue = AskToContinue();
                    Clear();
                    continue;
                }

                char mathOption2 = input[0];
                char[] validBinaryOperations = { '+', '-', '*', '/', '%' };
                if (Array.IndexOf(validBinaryOperations, mathOption2) == -1)
                {
                    WriteLine("Неизвестная операция");
                    isContinue = AskToContinue();
                    Clear();
                    continue;
                }

                Write("Введите второе число: ");
                if (!TryReadNumber(out double secondNum))
                {
                    isContinue = AskToContinue();
                    Clear();
                    continue;
                }

                result = BinaryOperation(firstNum, mathOption2, secondNum);

                // Сохраняем результат бинарной операции в память
                if (!double.IsNaN(result))
                {
                    memory = result;
                }
            }

            // Выводим результат
            if (!double.IsNaN(result))
            {
                WriteLine($"Результат: {result}");
                WriteLine($"Текущее значение в памяти: {memory}");
            }

            // Спрашиваем у пользователя хочет ли он продолжить
            isContinue = AskToContinue();
            Clear();
        }

        WriteLine("Программа завершена. До свидания!");
    }
}
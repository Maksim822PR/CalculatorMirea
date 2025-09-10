using System;
using static System.Console;
using static System.Math;

class Calculator
{
    static double memory = 0;

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
            return result;
            WriteLine(result);
        }
        return double.NaN;
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
            return result;
            WriteLine(result);
        }
        return double.NaN;
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

            if (typeOfOperation == "унарная")
            {
                Write("Введите операцию (1/x, x^2, sqrt(x), M+, M-, MR, MC): ");
                string mathOption1 = ReadLine().ToLower();

                double num = 0;
                if (mathOption1 != "mr" && mathOption1 != "mc")
                {
                    Write("Введите число: ");
                    num = Convert.ToDouble(ReadLine());
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
                double firstNum = Convert.ToDouble(ReadLine());

                Write("Введите операцию (+, -, /, *, %): ");
                string input = ReadLine();
                char mathOption2 = input.Length > 0 ? input[0] : '\0';

                Write("Введите второе число: ");
                double secondNum = Convert.ToDouble(ReadLine());

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
            Write("Хотите продолжить? (да/нет): ");
            string answer = ReadLine().ToLower();

            if (answer != "да" && answer != "д" && answer != "yes" && answer != "y")
            {
                isContinue = false;
            }

            Clear();
        }

        WriteLine("Программа завершена. До свидания!");
    }
}
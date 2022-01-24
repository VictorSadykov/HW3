using System;

namespace HW3
{
    internal class Program
    {

/*  
1. Необходимо создать метод, который заполняет данные с клавиатуры по пользователю(возвращает кортеж) :
* Имя;
* Фамилия;
* Возраст;
* Наличие питомца;
* Если питомец есть, то запросить количество питомцев;
* Если питомец есть, вызвать метод, принимающий на вход количество питомцев и возвращающий массив их кличек(заполнение с клавиатуры);
* Запросить количество любимых цветов;
* Вызвать метод, который возвращает массив любимых цветов по их количеству(заполнение с клавиатуры);
* Сделать проверку, ввёл ли пользователь корректные числа: возраст, количество питомцев, количество цветов в отдельном методе;
* Требуется проверка корректного ввода значений и повтор ввода, если ввод некорректен;
* Корректный ввод: ввод числа типа int больше 0.

2. Метод, который принимает кортеж из предыдущего шага и показывает на экран данные.
3. Вызов методов из метода Main.*/

        static void Main(string[] args)
        {
            PrintInfo(GetInfo());
        }


        static void PrintInfo((string firstName, string lastName, int age, bool isHavingPet, int petsAmount, string[] petNames, int favColorsAmount, string[] favColorNames) inputInfo)
        {
            Console.WriteLine($"Имя пользователя: {inputInfo.firstName}");
            Console.WriteLine($"Фамилия пользователя: {inputInfo.lastName}");
            Console.WriteLine($"Возраст пользователя: {inputInfo.age}");
            Console.WriteLine($"Наличие питомца у пользователя: {inputInfo.isHavingPet}");

            if (inputInfo.isHavingPet == true)
            {
                Console.WriteLine($"Количество питомцев у пользователя: {inputInfo.petsAmount}");
                Console.WriteLine("Имена питомцев");

                for (int i = 0; i < inputInfo.petsAmount; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {inputInfo.petNames[i]}");
                }
            }

            Console.WriteLine($"Количество любимых цветов: {inputInfo.favColorsAmount}");

            if (inputInfo.favColorsAmount != 0)
            {
                Console.WriteLine("Название цветов");

                for (int i = 0; i < inputInfo.favColorsAmount; i++)
                {
                    Console.WriteLine($"\t{i + 1}. {inputInfo.favColorNames[i]}");
                }
            }
        }

        /// <summary>
        /// Получает данные о пользователе.
        /// </summary>
        /// <returns></returns>
        static (string firstName, string lastName, int age, bool isHavingPet, int petsAmount, string[] petNames, int favColorsAmount, string[] favColorNames) GetInfo() // 1ое задание
        {
            

            (string firstName, string lastName, int age, bool isHavingPet, int petsAmount, string[] petNames, int favColorsAmount, string[] favColorNames) outputInfo = (null, null, 0, false, 0, null, 0, null);
            Console.WriteLine("Введите своё имя."); // Имя
            outputInfo.firstName = Console.ReadLine();

            Console.WriteLine("Введите свою фамилию."); // Фамилия
            outputInfo.lastName = Console.ReadLine();

            string ageConsoleString = "Введите свой возраст.";
            Console.WriteLine(ageConsoleString); // Возраст
            outputInfo.age = int.Parse(Console.ReadLine());
            while (outputInfo.age < 0) 
            { 

                IncorrectDataChecking(out outputInfo.age, "Неккоректные данные. Возраст должен быть целым числом больше нуля.", ageConsoleString);
            }

            string havingPetConsoleString = "У вас есть питомец?\n1. Да.\n2. Нет.";
            Console.WriteLine(havingPetConsoleString); // Наличие питомца
            int petResult = int.Parse(Console.ReadLine());
            
            do
            {
                if (petResult != 2 && petResult != 1)
                {
                    IncorrectDataChecking(out petResult, "Некорректный ввод данных о питомце.", havingPetConsoleString);
                }

                if (petResult == 1)
                {
                    outputInfo.isHavingPet = true;
                }

            } while (petResult != 1 && petResult != 2);

            if (outputInfo.isHavingPet == true) // Количество питомцев
            {
                GetPetsAmount(ref outputInfo.isHavingPet); 
            }

            if (outputInfo.petsAmount != 0) // Имена питомцев
            {
                FillStringArray(ref outputInfo.petNames, outputInfo.petsAmount, "Введите имя любимого питомца под номером");
            }

            string colorsAmountConsoleString = "Введите количество любимых цветов"; //Любимые цвета
            Console.WriteLine(colorsAmountConsoleString);
            outputInfo.favColorsAmount = int.Parse(Console.ReadLine());
            while (outputInfo.favColorsAmount < 0)
            {
                IncorrectDataChecking(out outputInfo.favColorsAmount, "Некорректные данные. Количество цветов должно быть неотрицательным числом.", colorsAmountConsoleString);
            }

            if (outputInfo.favColorsAmount != 0) // Название любимых цветов
            {
                FillStringArray(ref outputInfo.favColorNames, outputInfo.favColorsAmount, "Введите название любимого цвета под номером");
            }


            return outputInfo;


            // ВСПОМОГОТАТЕЛЬНЫЕ МЕТОДЫ

            void IncorrectDataChecking(out int result, string str1, string str2)
            {
                Console.WriteLine(str1); // Уведомление о некорректных данных
                Console.WriteLine(str2); // Повтор о запросе на дополнение данных
                result = int.Parse(Console.ReadLine());
            }


            void GetPetsAmount(ref bool isHavingPet)
            {
                string petsAmountConsoleString = "Введите количество питомцев.";
                string havingPetChangeConsoleString = "Вы указали, что у вас имеется питомец. Хотите изменить эти данные?\n1. Да.\n2. Нет.";

                Console.WriteLine(petsAmountConsoleString);                

                outputInfo.petsAmount = int.Parse(Console.ReadLine());

                if (outputInfo.petsAmount == 0)
                {
                    Console.WriteLine(havingPetChangeConsoleString);
                    int result = int.Parse(Console.ReadLine());

                    if (result == 1)
                    {
                        isHavingPet = false;
                    }
                    else if (result != 2)
                    {
                        IncorrectDataChecking(out result, "Некорректные данные.", havingPetChangeConsoleString);
                    }

                    if (result == 2)
                    {
                        GetPetsAmount(ref isHavingPet);
                    }

                }
                else if (outputInfo.petsAmount < 0)
                {
                    IncorrectDataChecking(out outputInfo.petsAmount, "Некорректные данные. Количество питомцев должно быть больше нуля.", petsAmountConsoleString);
                }
            }

            void FillStringArray(ref string[] array, int length, string message)
            {
                array = new string[length];

                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine($"{message} {i + 1}");
                    array[i] = Console.ReadLine().Trim();
                }
            }
        }
    }
}

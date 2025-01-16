using System;

public class Runner
{
    public double Distance { get; private set; }
    public double WaterLoss { get; private set; }
    public event Action DrinkEvent;

    private const double DistancePerHalfHour = 5.0; // км за 30 минут
    private const double MaxWaterLoss = 2.0; // литры, при которых нужно пить

    public Runner()
    {
        Distance = 0;
        WaterLoss = 0;
    }

    // Метод, который обновляет состояние бегуна (пробежал дистанцию и потерял воду)
    public void RunHalfHour()
    {
        // Бегун пробегает 5 км
        Distance += DistancePerHalfHour;

        // Бегун теряет случайное количество влаги от 0.5 до 1 литра
        var random = new Random();
        var waterLost = random.NextDouble() * (1.0 - 0.5) + 0.5; // от 0.5 до 1 литра
        WaterLoss += waterLost;

        // Выводим информацию о текущем состоянии
        Console.WriteLine($"Бегун пробежал {Distance} км. Потеря воды: {waterLost:F2} л. Общий расход: {WaterLoss:F2} л.");

        // Если потеряно 2 литра воды, вызываем событие пить
        if (WaterLoss >= MaxWaterLoss)
        {
            DrinkEvent?.Invoke();
            WaterLoss = 0; // Сбросить расход воды после события
        }
    }
}

public class Coach
{
    public void OnDrinkEvent()
    {
        Console.WriteLine("Тренер: Время пить! Помогаю бегуну.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем объекты
        var runner = new Runner();
        var coach = new Coach();

        // Подписываемся на событие
        runner.DrinkEvent += coach.OnDrinkEvent;

        // Бегун должен пробежать 30 км
        while (runner.Distance < 30)
        {
            runner.RunHalfHour();
        }

        Console.WriteLine("Бегун завершил тренировку.");
    }
}
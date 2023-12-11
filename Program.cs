// See https://aka.ms/new-console-template for more information

// Паттерн Стратегия
// поведенческий паттерн
// является основой для многих других паттернов
// позволяет изменить поведение клиентского объекта без
// изменения класса клиента
// внедрение зависимости (интерфейс это зависимость)
// может происходить через конструктор, метод или свойство
// SOLID 

/*
MoonPsysics moonPsysics = new MoonPsysics();
Human human = new Human(moonPsysics);
human.CalculateJumpMath();

EarthPsysics earthPsysics = new EarthPsysics();
human.ChangeJumpPhysics(earthPsysics);
human.CalculateJumpMath();

BlackHole blackHole = new BlackHole();
human.ChangeJumpPhysics(blackHole);
human.CalculateJumpMath();


public class Human
{
    IJumpPhysics jumpPhysics;

    public Human(IJumpPhysics jumpPhysics)
    {
        this.jumpPhysics = jumpPhysics;
    }

    public void CalculateJumpMath()
    {
        Console.WriteLine($"Высота прыжка {jumpPhysics.GetHightJump()}");
        Console.WriteLine($"Длина прыжка {jumpPhysics.GetLenghtJump()}");
    }

    public void ChangeJumpPhysics(IJumpPhysics jumpPhysics)
    {
        this.jumpPhysics = jumpPhysics;
    }
}

public interface IJumpPhysics
{
    float GetLenghtJump();
    float GetHightJump();
}

public class EarthPsysics : IJumpPhysics
{
    public float GetHightJump()
    {
        return 1.5f;
    }

    public float GetLenghtJump()
    {
        return 3f;
    }
}

public class MoonPsysics : IJumpPhysics
{
    public float GetHightJump()
    {
        return 100;
    }

    public float GetLenghtJump()
    {
        return 200;
    }
}

public class BlackHole : IJumpPhysics
{
    public float GetHightJump()
    {
        return float.NaN;
    }

    public float GetLenghtJump()
    {
        return float.NaN;
    }
}
*/


// паттерн Команда
// поведенческий паттерн
// паттерн полезен в случае, когда нужно инкапсулировать
// действия над объектом в виде отдельных объектов
// с помощью команд можно организовать очередь выполнения или
// историю выполнения некоторых действий
// команды могут сопровождаться действием "отмена"

Driver driver = new Driver();
Car car = new Car();
CommandTurnOn command = new CommandTurnOn(car);
car.PrintStatus();
driver.RegisterCommand(command);
driver.RunCommand();
car.PrintStatus();
CommandHeadlightsOn command1 = new CommandHeadlightsOn(car);
driver.RegisterCommand(command1);
driver.RunCommand();
car.PrintStatus();

driver.UndoLastCommand();
car.PrintStatus();
driver.UndoLastCommand();
car.PrintStatus();
driver.UndoLastCommand();
car.PrintStatus();
driver.UndoLastCommand();
car.PrintStatus();

public class Car
{
    bool isTurnedOn = false;   
    bool isTurnedHeadlightsOn = false;
    internal void TurnOn()
    {
        Console.WriteLine("Включено зажигание");
        isTurnedOn = true;
    }

    internal void TurnOff()
    {
        Console.WriteLine("Выключено зажигание");
        isTurnedOn = false;
    }

    internal void TurnHeadlightsOn()
    {
        Console.WriteLine("Включены фары");
        isTurnedHeadlightsOn = true;
    }

    internal void TurnHeadlightsOff()
    {
        Console.WriteLine("Выключены фары");
        isTurnedHeadlightsOn = false;
    }

    public void PrintStatus()
    {
        Console.WriteLine($"Зажигание: {isTurnedOn}. Фары: {isTurnedHeadlightsOn}");
    }
}

public class Driver
{
    Stack<ICommand> historyCommands = new Stack<ICommand>();

    ICommand command = new Command();
    public void RegisterCommand(ICommand command)
    {
        this.command = command;
    }

    public void RunCommand()
    {
        command.Execute();
        if (command is not Command)
            historyCommands.Push(command);
    }

    public void UndoLastCommand()
    {
        if (historyCommands.Count > 0)
            historyCommands.Pop().Undo();
    }
}

public interface ICommand
{ 
    void Execute();
    void Undo();
}

public class Command : ICommand
{
    public void Execute()
    {
    }
    public void Undo()
    {
    }
}

public class CommandTurnOn : ICommand
{
    private readonly Car car;

    public CommandTurnOn(Car car)
    {
        this.car = car;
    }

    public void Execute()
    {
        car.TurnOn();
    }
    public void Undo()
    {
        car.TurnOff();
    }
}

public class CommandHeadlightsOn : ICommand
{
    private readonly Car car;

    public CommandHeadlightsOn(Car car)
    {
        this.car = car;
    }

    public void Execute()
    {
        car.TurnHeadlightsOn();
    }
    public void Undo()
    {
        car.TurnHeadlightsOff();
    }
}
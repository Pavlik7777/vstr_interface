//Встроенные интерфейсы
//Вариант 14
Console.WriteLine("Введите информацию про 8 человек:");
NOTE<string>[] notes = new NOTE<string>[8];
for (int i = 0; i < notes.Length; i++)
{
    Console.WriteLine($"Человек {i + 1}");
    Console.Write("Фамилия: ");
    string lastName = Console.ReadLine()!;
    Console.Write("Имя: ");
    string firstName = Console.ReadLine()!;
    Console.Write("Номер телефона: ");
    string phone = Console.ReadLine()!;
    Console.Write("День рождения: ");
    int day = int.Parse(Console.ReadLine()!);
    Console.Write("Месяц рождения: ");
    int month = int.Parse(Console.ReadLine()!);
    Console.Write("Год рождения: ");
    int year = int.Parse(Console.ReadLine()!);
    notes[i] = new NOTE<string>(lastName, firstName, phone, new int[] { day, month, year });
}
Array.Sort(notes);
Console.WriteLine("Отсортировано по первым 3 цифрам номера:");
foreach (var note in notes)
    Console.WriteLine(note);
Array.Sort(notes, new NoteCompareByName<string>());
Console.WriteLine("Отсортировано по фамилии и имени:");
foreach (var note in notes)
    Console.WriteLine(note);
Console.Write("Введите фамилию для поиска: ");
string search = Console.ReadLine()!;
bool found = false;
foreach (var note in notes)
{
    if (note.LastName.ToLower() == search.ToLower())
    {
        Console.WriteLine(note);
        found = true;
    }
}
if (!found)
    Console.WriteLine("Человек с такой фамилией нет");
class NOTE<T> : ICloneable, IComparable
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public T PhoneNumber { get; set; }
    public int[] BirthDate { get; set; }

    public NOTE(string lastName, string firstName, T phoneNumber, int[] birthDate)
    {
        LastName = lastName;
        FirstName = firstName;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
    public object Clone()
    {
        return new NOTE<T>(LastName, FirstName, PhoneNumber, (int[])BirthDate.Clone());
    }
    public int CompareTo(object? obj)
    {
        if (obj is NOTE<T> anot)
        {
            string p1 = PhoneNumber!.ToString()!.Substring(0, 3);
            string p2 = anot.PhoneNumber!.ToString()!.Substring(0, 3);
            return p1.CompareTo(p2);
        }
        throw new ArgumentException("Некорректное значение");
    }
    public override string ToString()
    {
        return $"{LastName} {FirstName}, Телефон: {PhoneNumber}, Дата рождения: " +
               $"{BirthDate[0]:00}.{BirthDate[1]:00}.{BirthDate[2]}";
    }
}
class NoteCompareByName<T> : IComparer<NOTE<T>>
{
    public int Compare(NOTE<T>? x, NOTE<T>? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Некорректные значения");
        int result = x.LastName.CompareTo(y.LastName);
        if (result == 0)
            return x.FirstName.CompareTo(y.FirstName);
        return result;
    }
}

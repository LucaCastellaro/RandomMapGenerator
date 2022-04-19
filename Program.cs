Console.Clear();

const int size = 50;

var matrix = new int[size, size];

var possibleValues = new[] {
    new {value = 1, next = new[]{2}, color = ConsoleColor.DarkBlue},
    new {value = 2, next = new[]{1,3}, color = ConsoleColor.Blue},
    new {value = 3, next = new[]{2,4}, color = ConsoleColor.Yellow},
    new {value = 4, next = new[]{3,5}, color = ConsoleColor.Green},
    new {value = 5, next = new[]{4}, color = ConsoleColor.DarkGreen},
};

var random = new Random((int)DateTime.Now.Ticks);

for (var currentRow = 0; currentRow < size; currentRow++)
{
    for (var currentColumn = 0; currentColumn < size; currentColumn++)
    {
        var previousRow = Math.Max(currentRow - 1, 0);
        var previousColumn = Math.Max(currentColumn - 1, 0);

        var surroundings = new int[2];
        surroundings[0] = matrix[previousRow, currentColumn]; // top
        surroundings[1] = matrix[currentRow, previousColumn]; // left

        var newValue = 0;
        if (surroundings[0] == 0)
        {
            if (surroundings[1] == 0)
            {
                newValue = random.Next(0, possibleValues.Max(xx => xx.value));
            }
            else
            {
                var possibleNextValues = possibleValues.First(xx => xx.value == surroundings[1]);
                var newValueIndex = random.Next(0, possibleNextValues.next.Length);
                newValue = possibleNextValues.next[newValueIndex];
            }
        }
        else
        {
            if (surroundings[1] == 0)
            {
                var possibleNextValues = possibleValues.First(xx => xx.value == surroundings[0]).next;
                var newValueIndex = random.Next(0, possibleNextValues.Length);
                newValue = possibleNextValues[newValueIndex];
            }
            else
            {
                var topNextValues = possibleValues.First(xx => xx.value == surroundings[0]).next;
                var leftNextValues = possibleValues.First(xx => xx.value == surroundings[1]).next;

                var possibleNextValues = (from t in topNextValues
                                          join l in leftNextValues
                                          on t equals l
                                          select t).ToArray();

                var newValueIndex = random.Next(0, possibleNextValues.Length);
                newValue = possibleNextValues[newValueIndex];
            }
        }

        matrix[currentRow, currentColumn] = newValue;
        Console.BackgroundColor = possibleValues.First(xx => xx.value == newValue).color;
        Console.ForegroundColor = possibleValues.First(xx => xx.value == newValue).color;
        // Console.Write($"{newValue}");
        Console.Write($"  ");
    }
    Console.WriteLine();
}
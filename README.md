# ConsoleFarkle
This a Console Farkle game. You can play two player or vs a computer. 


BUG 1

In RollResults enum, when `Pair & Nothing` were on the same line, but `Nothing` was first on the line. When I would Add `RollResult.Pair`. It would add `RollResult.Nothing`. When I flipped them, it worked as intened. Even when the code read `rollResult.Add(RollResult.Pair);`
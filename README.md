<h1 align="center">
Doubly-linked list
</h1>
<p align="center">
A doubly-linked list data structure implementation written in C#. Provides Enumerable support, constant-time append/prepend and bidirectional traversal.
</p>

## Task
**9** % 4 = 1

| Task | Initial implementation | Second implementation               |
| :--: | :--------------------: | :---------------------------------: |
| 1    | Doubly-linked list     | List based on built-in arrays/lists |

## How to use:

You will need to have [dotnet 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed first.

  1. Open a terminal window in `mtsd-labs\`
  2. Execute `dotnet test` to run all the tests

## Commit on which CI tests failed:

[f090d83](/../../commit/f090d83eb43c9a0fb3972beec90542528eeff39c)

## Conclusion

This kind of project is the perfect application of unit tests: the behaviour is simple and strictly defined, so writing tests barely takes any time.
They have caught multiple bugs in the node traversal logic during the development process, while also making sure there is no functionality regression after each code refactor.
CI itself wasn't particularly useful, as you would run tests locally before committing anyway, but it's very easy to set up, so there aren't any downsides to using it.